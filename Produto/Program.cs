using Microsoft.EntityFrameworkCore;
using Produto_API.ContextoDB;
using Produto_API.Entidade;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProdutoDb>(opt => opt.UseInMemoryDatabase("ProdutosDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/", () => "Olá, mundo!");

app.MapGet("/produtos", async (ProdutoDb db) =>
    await db.Produtos.Where(p => p.Disponivel).ToListAsync());
app.MapGet("/produto/{id}", async (string id, ProdutoDb db) =>
    await db.Produtos.FindAsync(id)
        is Produto produto
            ? Results.Ok(produto)
            : Results.NotFound());
app.MapPost("/produtos", async (Produto produto, ProdutoDb db) => {
    db.Produtos.Add(produto);
    await db.SaveChangesAsync();
});
app.MapPut("/produto/{id}", async (string id, Produto produtoEntrada, ProdutoDb db) =>
{
    var produto = await db.Produtos.FindAsync(id);

    if (produto is null)  return Results.NotFound();
    produto.Nome = produtoEntrada?.Nome;
    produto.Disponivel = produtoEntrada.Disponivel;
    produto.Categoria = produtoEntrada?.Categoria;
    produto.Validade = produtoEntrada?.Validade;
    produto.Preco = produtoEntrada?.Preco;
    
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapDelete("/produto/{id}", async (string id, ProdutoDb db) =>
{
    if (await db.Produtos.FindAsync(id) is Produto produto)
    {
        db.Produtos.Remove(produto);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});


app.Run();