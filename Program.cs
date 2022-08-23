using Fiap.Api.AspNet3.Data;
using Fiap.Api.AspNet3.Repository;
using Fiap.Api.AspNet3.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("databaseUrl");
builder.Services.AddDbContext<DataContext>(o => 
    o.UseSqlServer(connectionString).EnableSensitiveDataLogging(true)
);

builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
