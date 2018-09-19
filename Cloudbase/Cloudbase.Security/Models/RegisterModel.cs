namespace Cloudbase.Security.Models
{
    public class RegisterModel : LoginModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordConfirmation { get; set; }
    }
}