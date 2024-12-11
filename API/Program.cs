using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
    options.AddPolicy("Acesso Total", configs => configs
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    )
);

var app = builder.Build();

//endpoints
app.MapPost("/api/aluno/cadastrar", ([FromBody] Aluno aluno, [FromServices] AppDataContext ctx) => 
{
    var verificar = ctx.Alunos.ToList();
    int op = 0;

    foreach (var item in verificar)
    {
        if(item.Nome == aluno.Nome && item.Sobrenome == aluno.Sobrenome)
        {
            op = 1;
        }
    }

    if(op == 0)
    {
        ctx.Alunos.Add(aluno);
        ctx.SaveChanges();
        return Results.Ok(aluno);
    }

    return Results.NotFound();
});

app.MapPost("/api/imc/cadastrar", ([FromBody] Imc imc, [FromServices] AppDataContext ctx) => 
{
    
    imc.ResultImc = imc.Peso/(imc.Altura * imc.Altura);

    if(imc.ResultImc < 18.5){
        imc.Classificacao = "MAGREZA";
    }
    if(imc.ResultImc >= 18.5 && imc.ResultImc <= 24.9){
        imc.Classificacao = "NORMAL";
    }
    if(imc.ResultImc >= 25 && imc.ResultImc <= 29.9){
        imc.Classificacao = "SOBREPESO";
    }
    if(imc.ResultImc >= 30 && imc.ResultImc <= 39.9){
        imc.Classificacao = "OBESIDADE";
    }
    if(imc.ResultImc >= 40){
        imc.Classificacao = "OBESIDADE GRAVE";
    }

    imc.Aluno = ctx.Alunos.Find(imc.AlunoId);

    ctx.Imcs.Add(imc);
    ctx.SaveChanges();
    return Results.Ok(imc);
});

app.MapGet("/api/imc/listar", ([FromServices] AppDataContext ctx) => 
{
    if(ctx.Imcs.Any())
    {
        var lista = ctx.Imcs.ToList();
        foreach (var item in lista)
        {
            item.Aluno = ctx.Alunos.Find(item.AlunoId);
        }
        return Results.Ok(lista);
    }

    return Results.NotFound();
});

app.MapGet("/api/imc/listarporaluno", ([FromBody] Aluno aluno, [FromServices] AppDataContext ctx) => 
{
    var lista = ctx.Imcs.Where(x => x.AlunoId == aluno.Id);

    if(lista != null)
    {
        foreach (var item in lista)
        {
            item.Aluno = ctx.Alunos.Find(item.AlunoId);
        }
        return Results.Ok(lista);
    }

    return Results.NotFound();
});

app.MapPut("api/imc/alterar/{id}", ([FromRoute] int id, [FromBody] Imc imc, [FromServices] AppDataContext ctx) => 
{
    Imc imcx = ctx.Imcs.Find(id);

    if(imc == null)
    {
        return Results.NotFound();
    } 

    imcx.ResultImc = imc.ResultImc;
    imcx.Altura = imc.Altura;
    imcx.Peso = imc.Peso;
    imcx.Classificacao = imc.Classificacao;
    imcx.DataCriacao = imc.DataCriacao;
    imcx.AlunoId = imc.AlunoId;
    imcx.Aluno = ctx.Alunos.Find(imcx.AlunoId);

    ctx.Imcs.Update(imcx);
    ctx.SaveChanges();
    return Results.Ok(imcx);
});

app.UseCors("Acesso Total");

app.Run();