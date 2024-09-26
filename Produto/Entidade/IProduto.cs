
namespace Produto_API.Entidade
{
    public interface IProduto
    {
        string? Categoria { get; set; }
        bool Disponivel { get; set; }
        string Id { get; set; }
        string? Nome { get; set; }
        decimal? Preco { get; set; }
        DateTime? Validade { get; set; }
    }
}