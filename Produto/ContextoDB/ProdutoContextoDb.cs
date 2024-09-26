using Microsoft.EntityFrameworkCore;
using Produto_API.Entidade;

namespace Produto_API.ContextoDB
{
    public class ProdutoDbContext : DbContext
    {
        public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }

    }
}
