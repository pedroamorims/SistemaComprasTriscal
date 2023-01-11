using MediatR;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly ISolicitacaoCompraRepository solicitacaoCompraRepository;
        private readonly IProdutoRepository produtoRepository;


        public RegistrarCompraCommandHandler(ISolicitacaoCompraRepository solicitacaoCompraRepository, IProdutoRepository produtoRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            this.solicitacaoCompraRepository = solicitacaoCompraRepository;
            this.produtoRepository = produtoRepository;

        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var solicitacaoCompra = new SolicitacaoCompraAgg.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor);

                foreach (ItemCommand item in request.Itens)
                {

                    var produto = produtoRepository.Obter(item.IdProduto);

                    if (produto != null)
                    {
                        solicitacaoCompra.AdicionarItem(produto, item.Quantidade);
         
                    }
                    else
                    {
                        return Task.FromResult(false);

                    }

                }

                CondicaoPagamento condicao = new CondicaoPagamento(request.condicaoPagamento);

                solicitacaoCompra.condicaoPagamento = condicao;



                solicitacaoCompra.RegistrarCompra(solicitacaoCompra);

                solicitacaoCompraRepository.Criar(solicitacaoCompra);

                Commit();

                PublishEvents(solicitacaoCompra.Events);

                return Task.FromResult(true);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
