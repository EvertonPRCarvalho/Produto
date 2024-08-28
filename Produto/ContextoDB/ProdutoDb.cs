using Microsoft.EntityFrameworkCore;
using Produto_API.Entidade;

namespace Produto_API.ContextoDB
{
    class ProdutoDb : DbContext
    {
        public ProdutoDb(DbContextOptions<ProdutoDb> options) : base(options) { }
        public DbSet<Produto> Produtos => Set<Produto>();
                           
    }
}
