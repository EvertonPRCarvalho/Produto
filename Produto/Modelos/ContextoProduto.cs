using Microsoft.EntityFrameworkCore;
using Produto_API.ContextoDB;

namespace Produto_API.Modelos
{
    public class ContextoProduto : DbContext
    {
        public ContextoProduto(DbContextOptions<ContextoProduto> options) : base(options) 
        { 
        }

        public DbSet<ProdutoDb> Produtos { get; set; } = null!;
    }
}
