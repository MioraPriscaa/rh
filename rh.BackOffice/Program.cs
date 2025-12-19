using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using rh.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter Razor Pages et définir Dashboard/Index comme page par défaut
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Dashboard/Index", "");
});


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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API RH v1");
    });
}

app.UseRouting();

app.UseAuthorization();

// Map Controllers
app.MapControllers();

// Map Razor Pages
app.MapRazorPages();
app.MapGet("/", context =>
{
    context.Response.Redirect("/Dashboard/Index");
    return Task.CompletedTask;
});

app.Run();
