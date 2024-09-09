using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SportsManagerAPI.Database;
using SportsManagerAPI.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("http://*:5001");

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("Default");
////Server=DESKTOP-HK3OPF6\\SQLEXPRESS; Database=3; User Id=as; Password=253962Aa; TrustServerCertificate=True;
var dbName = Environment.GetEnvironmentVariable("MY_DB_NAME") ?? @"sportdatabase";
var dbHost = Environment.GetEnvironmentVariable("MY_DB_HOST") ?? @"DESKTOP-HK3OPF6\\SQLEXPRESS";
var dbPort = Environment.GetEnvironmentVariable("MY_DB_PORT") ?? @"1433";
var dbUsername = Environment.GetEnvironmentVariable("MY_DB_USERNAME") ?? @"as";
var dbPassowrd= Environment.GetEnvironmentVariable("MY_DB_PASSWORD") ?? @"253962Aa";


//kendim çalýþmak için yoruma aldým 
//var connectionString = $"Server={dbHost}; Database={dbName}; User Id={dbUsername}; Password={dbPassowrd}; TrustServerCertificate=True; MultipleActiveResultSets=true;;";
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddSingleton<IDatabaseConnectionFactory>(new DatabaseConnectionFactory(connectionString));

// Repositories
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//mapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<String>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<String>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
	 options.TokenValidationParameters = new TokenValidationParameters
	 {
		 ValidateIssuer = true,
		 ValidateAudience = true,
		 ValidateLifetime = true,
		 ValidateIssuerSigningKey = true,
		 ValidIssuer = jwtIssuer,
		 ValidAudience = jwtIssuer,
		 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
	 };
 });
//Jwt configuration ends here

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		builder => builder.AllowAnyOrigin()
						  .AllowAnyMethod()
						  .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAllOrigins");


app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();