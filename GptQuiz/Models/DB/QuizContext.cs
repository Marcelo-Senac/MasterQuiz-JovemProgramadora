using Microsoft.EntityFrameworkCore;

namespace GptQuiz.Models.DB
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {

        }

        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
