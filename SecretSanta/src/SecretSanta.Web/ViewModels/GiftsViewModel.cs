using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels
{
    public class GiftViewModel
    {
        public int Id { get; set;}

        [Required]
        [Display(Name ="Name")]
        public string GiftName { get; set; } = "";

        [Required]
        [Display(Name = "Description")]
        public string GiftDescription { get; set; } = "";

        [Required]
        [Display(Name = "URL")]
        public string GiftURL { get; set; } = "";

        [Required]
        [Display(Name = "Priority")]
        public int GiftPriority { get; set; }


        [Required]
        [Display(Name = "User")]
        public string GiftUser { get; set; } = "";

    }
}