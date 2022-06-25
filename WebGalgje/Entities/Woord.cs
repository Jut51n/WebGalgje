using System.ComponentModel.DataAnnotations;

namespace WebGalgje.Entities;


public class Woord
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vul dit in aub")]
    [RegularExpression("^[a-zA-Z]", ErrorMessage = "Alleen letters graag")]
    [MaxLength(40, ErrorMessage = "Maximaal 40 tekens")]
    public string Word { get; set; }

}
