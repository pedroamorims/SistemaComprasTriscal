using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class ItemCommand
    {
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
