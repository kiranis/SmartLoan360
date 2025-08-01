using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Features.ApplyLoan;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLoanApiServices();
builder.Services.AddHttpClients();

var key = Encoding.UTF8.GetBytes("supersecretkey123!supersecretkey123!"); // 32+ bytes for HS256
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

    // Add event handlers for debugging
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated successfully");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

// Add CORS policy for Angular app
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
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Make sure CORS is enabled BEFORE authentication
app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();

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
.WithDescription("Requires authentication. Submit a loan application for processing.");


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
.WithDescription("Requires authentication. Get risk score for a loan application.");

app.MapPost("/api/token", (UserLogin login) =>
{
    if (login == null)
        return Results.BadRequest("Login data is required");
    
    if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
        return Results.BadRequest("Username and password are required");
    
    if (login.Username != "admin" || login.Password != "password")
        return Results.Unauthorized();
    
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes("supersecretkey123!supersecretkey123!"); // 32+ bytes for HS256
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
.WithDescription("Login with username 'admin' and password 'password' to get a JWT token.");

app.Run();

record UserLogin(string Username, string Password);
