using CompanyManagement.Data;
using CompanyManagement.Helpers;
using CompanyManagement.Interface;
using CompanyManagement.Middleware;
using CompanyManagement.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add DbContext with Npgsql (PostgreSQL)  
builder.Services.AddDbContext<CompanyDbContext>(opts =>
{
    opts.UseNpgsql(configuration["ConnectionString"], options => options.EnableRetryOnFailure());
});

// Register the repository  
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ICompaniesInfoRepository, CompaniesInfoRepository>();

// Configure routing options  
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

// Add controllers  
builder.Services.AddControllers();

// Add authorization
builder.Services.AddAuthentication("Basic")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
builder.Services.AddAuthorization();

// Set up Swagger  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Company Management", Version = "v1" });

    // Configure JWT authorization in Swagger  
    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Description = "Basic Authentication header using the Basic scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Basic"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Basic" }
            }, new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company Management");
        c.DocumentTitle = "Company Management";
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();