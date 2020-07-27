using System.ComponentModel.DataAnnotations;

namespace Microsoft.eShopWeb.Web.ViewModels.Manage
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Phone]
        [Display(Name = "ID Card")]
        public byte[] IDCard { get; set; }

        [Phone]
        [Display(Name = "Medical Letter")]
        public byte[] MedicalLetter { get; set; }

        public string StatusMessage { get; set; }
    }
}
