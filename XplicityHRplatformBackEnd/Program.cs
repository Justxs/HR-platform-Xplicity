
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddIdentity<User, IdentityRole>()
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void ApplyMigrations(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var databaseUtilities = services.GetRequiredService<DatabaseUtilities>();
    databaseUtilities.EnsureDatabaseCreated().Wait();
}
