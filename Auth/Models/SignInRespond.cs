using System.ComponentModel.DataAnnotations;

namespace BackendWebService.Auth.Models {
    public class SignInRespond {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength (20, MinimumLength = 8)]
        public string Token { get; set; }
    }
}