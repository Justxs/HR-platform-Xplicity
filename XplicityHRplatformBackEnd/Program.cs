using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XplicityHRplatformBackEnd.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddDbContext<HRplatformDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("HRplatformDbContext")));

var app = builder.Build();

void CondifureServices(IServiceCollection services)
{
    services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<HRplatformDbContext>();

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
