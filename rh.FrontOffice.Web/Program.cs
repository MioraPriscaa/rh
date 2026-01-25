using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using rh.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter Razor Pages et définir FirstPages/Index comme page par défaut
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/FirstPages/Index", "");
});

//pour la session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Ajouter Controllers (API)
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API RH", Version = "v1" });
});




var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API RH v1");
    });
}

app.UseRouting();

app.UseSession();


app.UseAuthorization();

// Map Controllers
app.MapControllers();

// Map Razor Pages
app.MapRazorPages();

// Map Controller Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FirstPage}/{action=GetActiveAnnonces}/{id?}");


app.Run();
