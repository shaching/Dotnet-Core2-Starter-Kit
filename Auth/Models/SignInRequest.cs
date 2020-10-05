using System.ComponentModel.DataAnnotations;

namespace BackendWebService.Auth.Models {
    public class SignInRequest {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength (20, MinimumLength = 8)]
        public string Password { get; set; }
    }
}