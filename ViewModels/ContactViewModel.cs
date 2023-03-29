using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        [EmailAddress]
        public string Subject { get; set; }

        [MaxLength(250)]
        public string Message { get; set; } // Add this property
    }

}
