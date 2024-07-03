using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SecurityInREST.Security;
using SecurityInREST.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option => option.AddPolicy("allow", builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();


    /*
     * http://www.hepsiburada.com
     * https://www.hepsiburada.com
     * https://store.hepsiburada.com
     * https://store.hepsiburada.com:8080
     * 
     * 
     * 
     * 
     */
}));

//builder.Services.AddAuthentication("Basic")
//                .AddScheme<BasicOption, BasicHandler>("Basic", null);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "server.btakademi",
                    ValidateAudience = true,
                    ValidAudience = "client.btakademi",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bu-ifade-token-onayi-icindir-ona-gore")),
                    ValidateIssuerSigningKey = true

                });


builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("allow");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
