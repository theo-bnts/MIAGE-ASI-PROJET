using System.ComponentModel.DataAnnotations;

namespace PROJET.Model
{
    public class Mood
    {
        //Clé primaire
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

    }
}
