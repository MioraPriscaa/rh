using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using rh.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter Razor Pages
builder.Services.AddRazorPages();

// Ajouter Controllers (API)
builder.Services.AddControllers();

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
    app.UseSwaggerUI();
}

// Routing
app.MapControllers();

// Razor Pages
app.MapRazorPages();

// Optionnel : fallback pour tout le reste (SPA ou URLs inconnues)
app.MapFallbackToPage("/Index");

app.Run();
