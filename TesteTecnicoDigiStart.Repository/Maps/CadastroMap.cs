using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteTecnicoDigiStart.Domain;

namespace TesteTecnicoDigiStart.Repository
{
    public class CadastroMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("t_users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar").HasMaxLength(40).IsRequired();
            builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Password).HasColumnName("password").HasColumnType("varchar").HasMaxLength(255).IsRequired();
            builder.Property(x => x.CreatedIn).HasColumnName("created_in").HasColumnType("varchar").HasMaxLength(255);
            builder.Property(x => x.LastAccess).HasColumnName("last_access").HasColumnType("varchar").HasMaxLength(255);
            builder.Property(x => x.UserRank).HasColumnName("user_rank").HasColumnType("int").IsRequired(); 
        }
    }
}
