using GptQuiz.Models;
using GptQuiz.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GptQuiz.Controllers
{
    public class QuizController : Controller
    {
        private readonly QuizContext _context;

        public QuizController(QuizContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            var categorias = _context.Categorias.ToList();
            return View(categorias);
        }

        [HttpPost]
        public IActionResult Welcome(string nome, int categoriaId)
        {
            HttpContext.Session.SetString("Nome", nome);
            HttpContext.Session.SetInt32("Pontuacao", 0);
            HttpContext.Session.SetInt32("PerguntaAtual", 0);
            HttpContext.Session.SetInt32("CategoriaSelecionada", categoriaId);
            HttpContext.Session.SetString("TempoInicial", DateTime.Now.ToString());

            var perguntas = _context.Perguntas
                .Where(p => p.Fk_Categoria_Id == categoriaId)
                .OrderBy(x => EF.Functions.Random()) // Embaralhar as perguntas
                .Take(5)
                .Select(p => p.Id)
                .ToList();

            HttpContext.Session.SetString("PerguntasIds", string.Join(",", perguntas));

            return RedirectToAction("Quiz");
        }

        public IActionResult Quiz()
        {
            int? perguntaAtual = HttpContext.Session.GetInt32("PerguntaAtual") ?? 0;

            var perguntasIdsString = HttpContext.Session.GetString("PerguntasIds");
            var perguntasIds = perguntasIdsString.Split(',').Select(int.Parse).ToList();

            if (perguntaAtual >= perguntasIds.Count)
            {
                return RedirectToAction("Thanks");
            }

            var perguntaId = perguntasIds[perguntaAtual.Value];
            var pergunta = _context.Perguntas.Find(perguntaId);

            if (pergunta == null)
            {
                return RedirectToAction("Thanks");
            }

            return View(pergunta);
        }

        [HttpPost]
        public IActionResult Quiz(int id, string resposta, string acao)
        {
            var pergunta = _context.Perguntas.Find(id);
            int? pontuacao = HttpContext.Session.GetInt32("Pontuacao") ?? 0;
            int? perguntaAtual = HttpContext.Session.GetInt32("PerguntaAtual") ?? 0;

            //if (acao == "Dica")
            //{
            //    ViewBag.Dica = pergunta.Dica;
            //    return View(pergunta);
            //}
            if(acao == "Responder")
            {
                if (string.IsNullOrEmpty(resposta))
                {
                    // Retornar a mesma view com uma mensagem de erro
                    ViewBag.Mensagem_obrigatoria = "Por favor, selecione uma resposta.";
                    return View(pergunta);
                }

                if (resposta == pergunta.Resposta_Correta)
                {
                    pontuacao++;
                    HttpContext.Session.SetInt32("Pontuacao", pontuacao.Value);
                    ViewBag.Mensagem_correta = "Resposta correta!";
                }
                else
                {
                    ViewBag.Mensagem_incorreta = "Resposta incorreta!";
                }

                perguntaAtual++;
                HttpContext.Session.SetInt32("PerguntaAtual", perguntaAtual.Value);

                var perguntasIdsString = HttpContext.Session.GetString("PerguntasIds");
                var perguntasIds = perguntasIdsString.Split(',').Select(int.Parse).ToList();

                if (perguntaAtual >= perguntasIds.Count)
                {
                    // Recuperar o tempo inicial da sessão
                    var tempoInicialString = HttpContext.Session.GetString("TempoInicial");
                    DateTime tempoInicial = DateTime.Parse(tempoInicialString);

                    // Calcular o tempo total em segundos
                    TimeSpan tempoTotal = DateTime.Now - tempoInicial;
                    int tempoEmSegundos = (int)tempoTotal.TotalSeconds;

                    var ranking = new Ranking
                    {
                        Nome = HttpContext.Session.GetString("Nome"),
                        Pontuacao = pontuacao.Value,
                        Tempo = tempoEmSegundos
                    };
                    _context.Rankings.Add(ranking);
                    _context.SaveChanges();

                    // Limpar a sessão
                    HttpContext.Session.Clear();

                    // Usar TempData para passar os dados
                    TempData["Nome"] = ranking.Nome;
                    TempData["Pontuacao"] = ranking.Pontuacao;
                    TempData["Tempo"] = ranking.Tempo;

                    return RedirectToAction("Thanks");
                }

                var proximaPerguntaId = perguntasIds[perguntaAtual.Value];
                var proximaPergunta = _context.Perguntas.Find(proximaPerguntaId);

                return View(proximaPergunta);
            }
            else
            {
                // Ação desconhecida, redirecionar ou tratar conforme necessário
                return RedirectToAction("Quiz");
            }
        }

        public IActionResult Thanks()
        {
            // Obter os dados do TempData
            string nome = TempData["Nome"] as string;
            int pontuacao = (int)(TempData["Pontuacao"] ?? 0);
            int tempo = (int)(TempData["Tempo"] ?? 0);

            // Obter os top 10 jogadores
            var topRankings = _context.Rankings
                .OrderByDescending(r => r.Pontuacao)
                .ThenBy(r => r.Tempo)
                .ThenBy(r => r.Nome)
                .Take(10)
                .ToList();

            ViewBag.Nome = nome;
            ViewBag.Pontuacao = pontuacao;
            ViewBag.Tempo = tempo;
            ViewBag.Rankings = topRankings;

            return View();
        }
    }
}
