using System.ComponentModel.DataAnnotations;

namespace GptQuiz.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        // Relação com Perguntas
        public ICollection<Pergunta> Perguntas { get; set; }
    }
}
