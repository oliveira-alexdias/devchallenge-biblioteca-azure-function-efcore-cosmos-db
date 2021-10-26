using DevChallenge.Biblioteca.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevChallenge.Biblioteca.Data.Mapping
{
    public class ObraMapping : IEntityTypeConfiguration<Obra>
    {
        public ObraMapping()
        {
        }

        public void Configure(EntityTypeBuilder<Obra> builder)
        {
            builder.ToContainer("Obras");
            builder.HasNoDiscriminator();

            builder.Property(i => i.Id)
                .ToJsonProperty("id")
                .HasConversion(new GuidToStringConverter());

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Titulo);
            builder.Property(i => i.Editora);
            builder.Property(i => i.Foto);
            builder.OwnsMany(i => i.Autores);
            builder.Property(i => i.CriadoEm);
        }
    }
}