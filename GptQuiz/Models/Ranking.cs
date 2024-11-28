using System.ComponentModel.DataAnnotations;

namespace GptQuiz.Models
{
    public class Ranking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public int Pontuacao { get; set; }
        public int Tempo { get; set; }
    }
}


