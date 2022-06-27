using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebGalgje.Entities;


public class Woord
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vul dit in aub")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Alleen letters graag")]
    [MaxLength(20, ErrorMessage = "Maximaal 20 tekens")]
    [MinLength(10, ErrorMessage = "Minimaal 10 tekens")]   
    public string Word { get; set; }
}
