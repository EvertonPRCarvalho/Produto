namespace Produto_API.Entidade
{
    public class Produto

    {
        public string Id { get; set; }
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public string? Categoria { get; set; }
        public bool Disponivel { get; set; }
        public DateTime? Validade { get; set; }
        

    }
}
