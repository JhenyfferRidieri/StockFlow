using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockFlowAPI.Data;
using StockFlowAPI.Interfaces.IRepository;
using StockFlowAPI.Interfaces.IServices;
using StockFlowAPI.Repositories;
using StockFlowAPI.Services;
using System.Text;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// JWT Configuração
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
var jwtAudience = builder.Configuration["Jwt:Audience"]!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// String de conexão
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Injeção de Dependências
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleItemRepository, SaleItemRepository>();
builder.Services.AddScoped<IAccountPayableRepository, AccountPayableRepository>();
builder.Services.AddScoped<IAccountReceivableRepository, AccountReceivableRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();

builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ISaleItemService, SaleItemService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ISaleStateService, SaleStateService>();
builder.Services.AddScoped<IAccountPayableService, AccountPayableService>();
builder.Services.AddScoped<IAccountReceivableService, AccountReceivableService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IInventoryMovementService, InventoryMovementService>();

// Serviço de autenticação
builder.Services.AddScoped<AuthService>();

// Controllers
builder.Services.AddControllers();

// Swagger + Autenticação JWT no Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "StockFlow API",
        Version = "v1",
        Description = "Documentação da API StockFlow"
    });

    // Swagger com Autenticação
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT no campo abaixo. Exemplo: Bearer {seu token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
            Array.Empty<string>()
        }
    });
});

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

/// Swagger SEM restrição de ambiente
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockFlow API V1");
    c.RoutePrefix = string.Empty;
});

// HTTPS opcional
// app.UseHttpsRedirection();

app.UseCors("AllowAll");

// Ativar autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
