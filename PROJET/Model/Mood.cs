using System.ComponentModel.DataAnnotations;

namespace PROJET.Model
{
    public class Mood
    {
        //Clé primaire
        [Key]
        public int Id { get; set; }

        //Lien de composition vers une catégorie
        [Required]
        public int RefMoodId { get; set; }
        public RefMood? RefMood { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser? User { get; set; }



    }
}
