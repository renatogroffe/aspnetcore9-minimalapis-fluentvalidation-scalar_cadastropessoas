using APIPessoas.Data;
using APIPessoas.Models;
using APIPessoas.Validators;
using FluentValidation;
using Scalar.AspNetCore;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<IValidator<DadosPessoa>, DadosPessoaValidator>();
builder.Services.AddSingleton<PessoasRepository>();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Title = "API de Cadastramento de Pessoas";
    options.Theme = ScalarTheme.BluePlanet;
    options.DarkMode = true;
});

app.UseHttpsRedirection();

app.MapGet("/pessoas", (PessoasRepository repository) =>
{
    var pessoas = repository.ListarTodos();
    app.Logger.LogInformation($"No. de pessoas cadastradas = {pessoas.Count()}");
    return pessoas;
})
.WithOpenApi();

app.MapGet("/pessoas/count", (PessoasRepository repository) =>
{
    var count = repository.ListarTodos().Count();
    app.Logger.LogInformation($"No. de pessoas cadastradas = {count}");
    return count;
})
.WithOpenApi();

app.MapPost("/pessoas", (PessoasRepository repository, DadosPessoa pessoa) =>
{
    repository.Incluir(pessoa);
    app.Logger.LogInformation(
        $"Novo cadastro realizado com sucesso: {pessoa.Nome} {pessoa.Sobrenome} - {pessoa.Empresa}");
    return Results.Ok();
})
.WithOpenApi()
.AddFluentValidationAutoValidation()
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);

app.Run();