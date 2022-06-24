using Microsoft.AspNetCore.Identity;

namespace WebGalgje.DataAccess;

public class Player : IdentityUser
{

    public int Id { get; set; }
    public string UserName { get; set; }

    public List<Stat> PlayerStats { get; set; }
}
