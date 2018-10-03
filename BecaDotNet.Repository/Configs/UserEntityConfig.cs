using BecaDotNet.Domain.Model;

namespace BecaDotNet.Repository.Configs
{
    public class UserEntityConfig : BaseIdentifiedEntityConfig<User>
    {
        public UserEntityConfig():base()
        {
            this.ToTable("TB_USER");
            this.Property(p => p.Name).HasColumnName("FULL_NAME").HasColumnType("varchar").HasMaxLength(200).IsRequired();
            this.Property(p => p.Email).HasColumnName("EMAIL").HasColumnType("varchar").HasMaxLength(200).IsRequired();
            this.Property(p => p.Login).HasColumnName("LOGIN").HasColumnType("varchar").HasMaxLength(100).IsRequired();
            this.Property(p => p.Password).HasColumnName("PASSWORD").HasColumnType("varchar").HasMaxLength(150).IsRequired();
            this.Property(p => p.UserTypeId).HasColumnName("USER_TYPE_ID").HasColumnType("int").IsRequired();
            this.Property(p => p.SuperiorId).HasColumnName("SUPERIOR_ID").HasColumnType("int").IsOptional();
            this.Property(p => p.RegisterDate).HasColumnName("REGISTER_DATE").HasColumnType("DateTime").IsRequired();
            this.Property(p => p.ProjetoAtualId).HasColumnName("PROJETO_ID").HasColumnType("int").IsOptional();
        }
    }
}
