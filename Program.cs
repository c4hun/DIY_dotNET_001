using DIY_dotNET_001.Repositories;  // S'assurer les bons namespaces pour IUserRepository et ses implémentations
using DIY_dotNET_001.Services;  // S'assurer les bons namespaces pour AuthService
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.DBContexts;

var builder = WebApplication.CreateBuilder(args);

// Ajoute les services de contrôleurs et vues
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configure DbContext avec SQLite
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TodoContext") ?? throw new InvalidOperationException("Connection string 'TodoContext' not found.")));

// Enregistrer AuthService et IUserRepository dans le conteneur DI
builder.Services.AddScoped<IUserRepository, InMemoryUserRepository>();  // Ou remplacer par SQLiteUserRepository si besoin
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Configure la pipeline de requêtes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Fichiers statiques et redirection HTTP
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TodoItems}/{action=Index}/{id?}");

app.Run();
