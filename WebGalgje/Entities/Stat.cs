namespace WebGalgje.DataAccess;

public class Stat
{
    public int Id { get; set; }
    public DateTime PlayDate { get; set; }
    public bool Won { get; set; }
    public int Tries { get; set; }
    public int WrongLettersGuessed { get; set; }
    
    public virtual Player Speler { get; set; }

}
