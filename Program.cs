

using ApiBeginner;
using ApiBeginner.Authentication;
using ApiBeginner.Data;
using ApiBeginner.Filters;
using ApiBeginner.Middlewares;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option =>
{
    option.Filters.Add<LogActivityFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<ApplicationDbContext>(
//    builder => builder.UseSqlServer("server=DESKTOP-GNA35PN;database=products;Trusted_Connection=True;Integrated Security=True;Trust Server Certificate=true;")
//    );

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtOptions = builder.Configuration.GetSection("jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtOptions);
builder.Services.AddAuthentication().
    AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer=jwtOptions.Issuer,
            ValidateAudience= true,
            ValidAudience=jwtOptions.Audience,
            ValidateIssuerSigningKey= true,
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
        };
    });

    //AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic",null);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseIpRateLimiting();
app.UseMiddleware<RateLimitingMiddelware>();
app.UseMiddleware<ProfilingMiddelware>();
app.MapControllers();

app.Run();
