using Microsoft.EntityFrameworkCore;
using Produto_API.ContextoDB;
using Produto_API.Entidade;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProdutoDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();


var app = builder.Build();
app.MapGet("/home", () => "Olá, mundo");
var produtos = app.MapGroup("/produto");

static async Task<IResult> ObterProdutos(ProdutoDbContext db)
{
    return TypedResults.Ok( await db.Produtos.ToArrayAsync());
}

static async Task<IResult> ObterProduto(string id, ProdutoDbContext db)
{
    return await db.Produtos.FindAsync(id)
        is Produto produto
            ? TypedResults.Ok(produto)
            : TypedResults.NotFound();
}

static async Task<IResult> RegistrarProdutos (Produto produto, ProdutoDbContext db)
{
    db.Add(produto);
    await db.SaveChangesAsync();

    return Results.Created($"/produto/{produto.Id}", produto);
}

static async Task<IResult> AtualizarProduto(string id, Produto produtoAtualizado, ProdutoDbContext db)
{
    var produto = await db.Produtos.FindAsync(id);

    if(produto is null) return Results.NotFound();
    produto.Nome = produtoAtualizado?.Nome;
    produto.Disponivel = produtoAtualizado.Disponivel;
    produto.Categoria = produtoAtualizado?.Categoria;
    produto.Validade = produtoAtualizado?.Validade;
    produto.Preco = produtoAtualizado?.Preco;
    
    await db.SaveChangesAsync();
    
    return Results.NoContent();
}

static async Task<IResult> ExcluirProduto (string id, ProdutoDbContext db)
{
    if (await db.Produtos.FindAsync(id) is Produto  produto)
    {
        db.Produtos.Remove(produto);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
}

produtos.MapGet("/", ObterProdutos);
produtos.MapGet("/{id}", ObterProduto);
produtos.MapPost("/", RegistrarProdutos);
produtos.MapPut("/{id}", AtualizarProduto);
produtos.MapDelete("/{id}", ExcluirProduto);


app.Run();