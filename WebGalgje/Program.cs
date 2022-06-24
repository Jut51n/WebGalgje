using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();

builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseSqlServer("Server=.; Database=todoDb; Integrated Security=true;");
});

builder.Services.AddIdentity<TodoUser, IdentityRole>().AddEntityFrameworkStores<TodoContext>();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddTransient<IItemRepository, ItemDbRepository>();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/auth/nossing");

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseSession();

app.MapHub<ItemHub>("/itemHub");
app.MapControllers();
app.MapRazorPages();

app.Run();
