using System.ComponentModel.DataAnnotations;

namespace PROJET.Model
{
    public class ApplicationUserSocioProfessionalCategory
    {

        //Clé primaire
        [Key]
        public int Id { get; set; }
        //Lien vers l'utilisateur
        [Required]
        public string? ApplicationUserId { get; set; }

        //Lien de composition vers une catégorie
        [Required]
        public int SocioProfessionalCategoryId { get; set; }
        public SocioProfessionalCategory? SocioProfessionalCategory { get; set; }

        // Lien de navigation ManyToMany 
        [Display(Name = "Les humeurs de l'utilisateur")]
        public ICollection<Mood> ListMoods { get; set; }


    }
}
