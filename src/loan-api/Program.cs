using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Features.ApplyLoan;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Extensions;
using Features.ScoreLoan;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoanApiServices();
builder.Services.AddHttpClients();

var key = Encoding.UTF8.GetBytes("supersecretkey123!supersecretkey123!");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
});

builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/apply", async (
    ApplyLoanCommand command,
    IValidator<ApplyLoanCommand> validator,
    IMediator mediator) =>
{
    var validation = await validator.ValidateAsync(command);
    if (!validation.IsValid)
        return Results.BadRequest(validation.Errors);

    var result = await mediator.Send(command);
    return Results.Ok(result);
})
.RequireAuthorization()
.WithName("ApplyLoan")
.WithSummary("Apply for a loan")
.WithDescription("Submit a loan application for processing");

app.MapPost("/api/score", async (
    ScoreLoanCommand command,
    IMediator mediator) =>
{
    var result = await mediator.Send(command);
    return Results.Ok(result);
})
.RequireAuthorization()
.WithName("ScoreLoan")
.WithSummary("Score a loan application")
.WithDescription("Get risk score for a loan application");

app.MapPost("/api/token", (UserLogin login) =>
{
    if (login?.Username != "admin" || login?.Password != "password")
        return Results.Unauthorized();

    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, login.Username),
            new Claim(ClaimTypes.NameIdentifier, login.Username)
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key), 
            SecurityAlgorithms.HmacSha256)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    var tokenString = tokenHandler.WriteToken(token);
    
    return Results.Ok(new { token = tokenString });
})
.AllowAnonymous()
.WithName("GetToken")
.WithSummary("Get authentication token")
.WithDescription("Login to get a JWT token");

app.Run();

record UserLogin(string Username, string Password);
