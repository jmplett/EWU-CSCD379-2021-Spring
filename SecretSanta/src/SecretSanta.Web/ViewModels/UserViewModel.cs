using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; } = "";
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = "";
    }
}