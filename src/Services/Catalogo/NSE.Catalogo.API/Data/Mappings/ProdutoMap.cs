using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Models;

namespace NSE.Catalogo.API.Data.Mappings;

public class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome).IsRequired().HasColumnType("varchar(250)");
        builder.Property(p => p.Descricao).IsRequired().HasColumnType("varchar(500)");
        builder.Property(p => p.Imagem).IsRequired().HasColumnType("varchar(250)");
        builder.Property(p => p.Valor).IsRequired().HasColumnType("decimal(10,2)");
        builder.Property(p => p.DataCadastro).IsRequired().HasColumnType("datetime");
        builder.Property(p => p.Ativo).IsRequired();
        builder.Property(p => p.QuantidadeEstoque).IsRequired();
    }
}
