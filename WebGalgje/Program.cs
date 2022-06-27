using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebGalgje.DataAccess;
using WebGalgje.DataAccess.Repositories;
using WebGalgje.Entities;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
builder.Services.AddMvc();

builder.Services.AddDbContext<GalgContext>(options =>
{
    options.UseSqlServer("Server=.; Database=WebGalgje; Integrated Security=true;");
});

builder.Services.AddIdentity<Player, IdentityRole>().AddEntityFrameworkStores<GalgContext>();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddTransient<IGameRepository, GameRepository>();
builder.Services.AddTransient<IStatsRepository, StatsRepository>();
builder.Services.AddTransient<IWoordRepository, WoordRepository>();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/auth/nossing");

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseSession();


// app.MapHub<ItemHub>("/itemHub"); Voor SignalR
app.MapControllers();
app.MapRazorPages();

app.Run();
