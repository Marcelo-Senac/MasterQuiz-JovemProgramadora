using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GptQuiz.Models
{
    public class Pergunta
    {
        [Key]
        public int Id { get; set; }
        public string Enunciado { get; set; }
        public string Alternativa1 { get; set; }
        public string Alternativa2 { get; set; }
        public string Alternativa3 { get; set; }
        public string Alternativa4 { get; set; }
        public string Resposta_Correta { get; set; }
        //public string Dica { get; set; }

        // Chave estrangeira para Categoria
        public int Fk_Categoria_Id { get; set; }

        [ForeignKey("Fk_Categoria_Id")]
        public Categoria Categoria { get; set; }
    }
}
