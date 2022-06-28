using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebGalgje.DataAccess;
using WebGalgje.DataAccess.Repositories;
using WebGalgje.Entities;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
//builder.Services.AddMvc();

builder.Services.AddDbContext<GalgContext>(options =>
{
    options.UseSqlServer("Server=.; Database=WebGalgje; Integrated Security=true;");
});

builder.Services.AddIdentity<Player, IdentityRole>().AddEntityFrameworkStores<GalgContext>();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Speler");

builder.Services.AddTransient<IGameRepository, GameRepository>();
builder.Services.AddTransient<IStatsRepository, StatsRepository>();
builder.Services.AddTransient<IWoordRepository, WoordRepository>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseSession();

// app.MapHub<ItemHub>("/itemHub"); Voor SignalR
app.MapControllers();
app.MapRazorPages();

app.Run();
