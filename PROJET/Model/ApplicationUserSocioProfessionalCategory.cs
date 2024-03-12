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
        [Required]
        public string ApplicationUserLastName { get; set; }
        [Required]
        public string ApplicationUserFirstName { get; set; }


        //Lien de composition vers une catégorie
        [Required]
        public int SocioProfessionalCategoryId { get; set; }
        public SocioProfessionalCategory? SocioProfessionalCategory { get; set; }


    }
}
