using System.ComponentModel.DataAnnotations;

namespace WePayOffer.BL.Models
{
    public class LoginVM
    {

        [Required(ErrorMessage = "User Name Required")]
        public string UserName { get; set; }

        [MinLength(6, ErrorMessage = "Min len 6")]
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
