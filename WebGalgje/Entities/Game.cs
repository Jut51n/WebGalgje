namespace WebGalgje.Entities;

public class Game
{
    public int Id { get; set; }

    public string WordToGuess { get; set; }
    public int Tries { get; set; }
    public int WrongTries { get; set; }
    public string LettersGuessed { get; set; }

    public virtual Player Speler { get; set; } //Navigation
}
