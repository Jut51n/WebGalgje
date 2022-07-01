using System.ComponentModel.DataAnnotations;

namespace WebGalgje.Entities
{
    public class GameInput
    {
        [Required(ErrorMessage = "Geef een letter in!")]
        [MaxLength(1, ErrorMessage="Maximaal 1 letter in geven!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "In echte woorden zitten alleen letters")]
        public string Guess { get; set; }


        public char ReturnGuessAsChar()
        {
            return char.ToLower(Guess[0]);
        }
    }
}
