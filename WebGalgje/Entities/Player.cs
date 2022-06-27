using Microsoft.AspNetCore.Identity;

namespace WebGalgje.Entities;

public class Player : IdentityUser
{

    public List<Stat> PlayerStats { get; set; } //Navigation
}
