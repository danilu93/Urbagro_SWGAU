using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SWGAU.Models;
using SWGAU.Models.Modelos;
using SWGAU.Models.Enums;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Configurar la cadena de conexión a la base de datos

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Configurar la ruta de inicio de sesión
        options.AccessDeniedPath = "/AccessDenied"; // Configurar la ruta de acceso denegado
        options.ExpireTimeSpan = TimeSpan.FromHours(2); // Configurar el tiempo de expiración de la cookie de autenticación
    });

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Aplicar migraciones pendientes a la base de datos

    if (!dbContext.Usuarios.Any()) // Verificar si no existen usuarios en la base de datos
    {
        // Crear un usuario administrador predeterminado si no existen usuarios en la base de datos
        dbContext.Usuarios.Add(new Usuario
        {
            NombreUsuario = "admin", // Cambiar el nombre de usuario predeterminado por uno más seguro
            ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("admin123"), // Cambiar la contraseña predeterminada por una más segura y almacenarla como hash
            CorreoElectronico = "admin@swgau.com", // Cambiar el correo electrónico predeterminado por uno válido
            Rol = RolUsuario.Administrador
        });
        dbContext.SaveChanges(); // Guardar los cambios en la base de datos
    }
}

//Configurar el request de pipeline HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Configurar la página de error para entornos de producción
    app.UseHsts(); // Habilitar HTTP Strict Transport Security (HSTS) para mejorar la seguridad
}

app.UseHttpsRedirection(); // Redirigir automáticamente las solicitudes HTTP a HTTPS

app.UseRouting(); // Habilitar el enrutamiento de solicitudes

app.UseAuthentication(); // Habilitar la autenticación de usuarios para acceder a recursos protegidos
app.UseAuthorization(); //  Habilitar la autorización de usuarios para acceder a recursos protegidos

app.MapStaticAssets(); // Habilitar el mapeo de archivos estáticos para que puedan ser accedidos desde la web
app.MapRazorPages() // Habilitar el mapeo de páginas Razor para que puedan ser accedidas desde la web
   .WithStaticAssets(); // Habilitar el mapeo de archivos estáticos para que puedan ser accedidos desde la web

app.Run();
