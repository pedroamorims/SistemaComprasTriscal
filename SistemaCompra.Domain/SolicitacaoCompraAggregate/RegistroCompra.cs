using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class RegistroCompra : Entity
    {
        public SolicitacaoCompra solicitacaoCompra { get; private set; }
        public Produto produto { get; private set; }
        public int quantidade { get; private set; }

        public CondicaoPagamento condicaoPagamento { get; set; }

        private RegistroCompra() { }

        public RegistroCompra(SolicitacaoCompra solicitacaoCompra, Produto produto, int quantidade)
        {
            this.solicitacaoCompra = solicitacaoCompra;
            this.produto = produto;
            this.quantidade = quantidade;
        }

    }
}
