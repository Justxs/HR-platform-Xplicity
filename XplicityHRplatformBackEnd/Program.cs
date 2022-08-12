
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using XplicityHRplatformBackEnd.DB;
using XplicityHRplatformBackEnd.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


builder.Services.AddScoped<DatabaseUtilities>();
builder.Services.AddMvc();
builder.Services.AddDbContext<HRplatformDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HRplatformDbContext")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})

.AddEntityFrameworkStores<HRplatformDbContext>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "https://localhost:7241",
        ValidAudience = "https://localhost:7241",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyUltraSecretKeyForHrApp"))
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).WithExposedHeaders("content-disposition"));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// init roles and admin user creation
var serviceProvider = app.Services.CreateScope().ServiceProvider;


var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
if (!roleManager.RoleExistsAsync("User").Result)
{
    var role = new IdentityRole { Name = "User" };
    roleManager.CreateAsync(role).Wait();
}
if (!roleManager.RoleExistsAsync("Admin").Result)
{
    var role = new IdentityRole { Name = "Admin" };
    roleManager.CreateAsync(role).Wait();
}


var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
if (!userManager.Users.Any(x => x.Id == InitAdminConstants.Id))
{
    var user = new User
    {
        UserName = InitAdminConstants.Email,
        Email = InitAdminConstants.Email
    };
    userManager.CreateAsync(user, InitAdminConstants.Password).Wait();
    userManager.AddToRoleAsync(user, "Admin");
}

app.Run();

static void ApplyMigrations(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var databaseUtilities = services.GetRequiredService<DatabaseUtilities>();
    databaseUtilities.EnsureDatabaseCreated().Wait();
}
