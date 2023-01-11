using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using RegistrarCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.RegistrarCompra
{
    public class RegistroCompraConfiguration : IEntityTypeConfiguration<RegistrarCompraAgg.RegistroCompra>
    {
        public void Configure(EntityTypeBuilder<RegistrarCompraAgg.RegistroCompra> builder)
        {
            builder.ToTable("Item");

        }
    }
}
