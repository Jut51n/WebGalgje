using System.ComponentModel.DataAnnotations;

namespace WebGalgje.Entities
{
    public class FormUser
    {
        [Required(ErrorMessage = "UserName is verplicht")]
        [MinLength(4, ErrorMessage = "Een Username moet minstens 4 tekens lang zijn")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Alleen letters en cijfers graag")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is verplicht")]
        [MinLength(8, ErrorMessage = "Een wachtwoord moet minstens 8 tekens lang zijn")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Hoofdletter, kleine letter en cijfer verplicht.")]
        public string Password { get; set; }

        public string Email {  get; set; }

        public string? Action { get; set; }
    }
}
