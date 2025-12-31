using Microsoft.EntityFrameworkCore;
using InvCore360_App.DA.Data;
using InvCore360_App.DA.Repositories;
using InvCore360_App.BL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext (set connection string in appsettings.json)
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Producto}/{action=Index}/{id?}");

app.Run();
