using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.Models.Login
{
    public class LoginDto
    {

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        //public string ErrorMessage { get; internal set; }
    }
}
