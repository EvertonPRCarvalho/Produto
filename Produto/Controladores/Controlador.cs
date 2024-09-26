using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Produto_API.ContextoDB;
using Produto_API.Entidade;
using System;

namespace Produto_API.Controladores
{
    [ApiController]
    [Route("/produto")]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoDbContext _context;

        public ProdutosController(ProdutoDbContext context)
        {
            _context = context;
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<Produto>> ObterProduto(string id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> RegistrarProdutos(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterProduto), new { id = produto.Id }, produto);
        }
    }
}
