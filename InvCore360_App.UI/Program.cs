using Microsoft.EntityFrameworkCore;
using InvCore360_App.DA.Data;
using InvCore360_App.DA.Repositories;
using InvCore360_App.BL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Require authenticated users by default
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

// Configure DbContext (set connection string in appsettings.json)
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication - Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

// Register repositories
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<ProveedorRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<HistorialPrecioRepository>();
builder.Services.AddScoped<VentaRepository>();
builder.Services.AddScoped<DetalleVentaRepository>();
builder.Services.AddScoped<MovimientoInventarioRepository>();
builder.Services.AddScoped<GastoRepository>();
builder.Services.AddScoped<AlertaInventarioRepository>();
builder.Services.AddScoped<ConfiguracionNegocioRepository>();

// Register business services
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<DetalleVentaService>();
builder.Services.AddScoped<MovimientoInventarioService>();
builder.Services.AddScoped<GastoService>();
builder.Services.AddScoped<AlertaInventarioService>();
builder.Services.AddScoped<ConfiguracionNegocioService>();
builder.Services.AddScoped<HistorialPrecioService>();

var app = builder.Build();

// Apply migrations and seed admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DataContext>();
        context.Database.Migrate();

        var usuarioService = services.GetRequiredService<UsuarioService>();
        var adminUser = usuarioService.GetByNombreUsuarioAsync("admin").GetAwaiter().GetResult();
        if (adminUser == null)
        {
            var admin = new InvCore360_App.Model.Models.Usuario
            {
                NombreUsuario = "admin",
                NombreCompleto = "Administrador",
                Contrasena = "admin123",
                Email = "admin@local",
                Rol = "Admin",
                Activo = true
            };
            usuarioService.AddAsync(admin).GetAwaiter().GetResult();
        }
    }
    catch (Exception ex)
    {
        // Log error - in this simple setup just write to console
        Console.WriteLine($"Error applying migrations or seeding data: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
