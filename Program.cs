using Fiap.Api.AspNet3;
using Fiap.Api.AspNet3.Data;
using Fiap.Api.AspNet3.Repository;
using Fiap.Api.AspNet3.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Acesso ao banco
var connectionString = builder.Configuration.GetConnectionString("databaseUrl");
builder.Services.AddDbContext<DataContext>(o => 
    o.UseSqlServer(connectionString).EnableSensitiveDataLogging(true)
);

builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
#endregion


#region Autenticação
var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication( x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer( j =>
{
    j.RequireHttpsMetadata = false;
    j.SaveToken = true;
    j.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion


builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options => { 
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
