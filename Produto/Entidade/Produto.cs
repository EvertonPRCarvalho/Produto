namespace Produto_API.Entidade
{
    public class Produto : IProduto

    {
        public Produto(string id, string? nome, decimal? preco, string? categoria, bool disponivel, DateTime? validade)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Categoria = categoria;
            Disponivel = disponivel;
            Validade = validade;
        }

        public string Id { get; set; }
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public string? Categoria { get; set; }
        public bool Disponivel { get; set; }
        public DateTime? Validade { get; set; }


    }
}
