using System.ComponentModel.DataAnnotations;

namespace PROJET.Model
{
    public class RefMood
    {

        //Clé primaire
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Mood> Moods { get; set; }
    }
}
