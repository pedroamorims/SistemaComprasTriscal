using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.ProdutoAggregate.Events;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }

        public CondicaoPagamento condicaoPagamento { get; set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            Itens= new List<Item>();
            TotalGeral = new Money(0);
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
      
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(SolicitacaoCompra solicitacaoCompra)
        {
            if (solicitacaoCompra.Itens.Count() <= 0)
            {
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");
            }


            foreach (Item item in Itens)
            {
                TotalGeral = TotalGeral.Add(item.Subtotal);
            }



            if (TotalGeral.Value > 50000 && condicaoPagamento.Valor != 30)
            {
                CondicaoPagamento condicao30 = new CondicaoPagamento(30);
                condicaoPagamento = condicao30;
            }

           // AddEvent(new CompraRegistradaEvent(Id, Itens, TotalGeral));
        }
    }
}
