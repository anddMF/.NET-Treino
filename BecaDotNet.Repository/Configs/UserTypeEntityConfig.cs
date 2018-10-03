using BecaDotNet.Domain.Model;

namespace BecaDotNet.Repository.Configs
{
    public class UserTypeEntityConfig : BaseIdentifiedEntityConfig<UserType>
    {
        public UserTypeEntityConfig():base()
        {
            this.ToTable("TB_USER_TYPE");
            this.Property(p => p.Description).HasColumnName("DESC_USER_TYPE").HasColumnType("varchar").HasMaxLength(200).IsRequired();
        }
    }
}
