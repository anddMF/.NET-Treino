using BecaDotNet.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BecaDotNet.Repository.Configs
{
    public abstract class BaseIdentifiedEntityConfig<T> : EntityTypeConfiguration<T> where T:IdentifiedEntity
    {
        public BaseIdentifiedEntityConfig()
        {
            this.HasKey<int>(pk => pk.Id);
            this.Property(p => p.Id).HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.IsActive).HasColumnName("IS_ACTIVE").HasColumnType("bit").IsRequired();
        }
    }
}
