using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using SWP391.ChildGrowthTracking.Repository.Model;
using SWP391.ChildGrowthTracking.Service;
using SWP391.ChildGrowthTracking.Repository;
using SWP391.ChildGrowthTracking.Repository.Services;
using SWP391.ChildGrowthTracking.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64; // Optional: Increase the max depth if needed
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ?? Database Context
builder.Services.AddDbContext<Swp391ChildGrowthTrackingContext>(op =>
   op.UseSqlServer(builder.Configuration.GetConnectionString("Swp391ChildGrowthTracking")));

// ?? CORS Configuration
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// ?? Dependency Injection for Services
builder.Services.AddScoped<IUseraccount, UseraccountService>();
builder.Services.AddScoped<IBlog, BlogService>();
builder.Services.AddScoped<IMembershipPackage, MembershipPackageService>();
builder.Services.AddScoped<IDoctor, DoctorService>();
builder.Services.AddScoped<IUserMembership, UserMembershipService>();
// ?? JWT Authentication Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// ?? Add Authorization (Fixes your error)
builder.Services.AddAuthorization();

// ?? Swagger Configuration with JWT Support
builder.Services.AddSwaggerGen(option =>
{
    option.DescribeAllParametersInCamelCase();
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// ?? Middleware Configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyCors");

app.UseAuthentication();  // ? Required for JWT Authentication
app.UseAuthorization();   // ? Required for Authorization

app.MapControllers();

app.Run();
