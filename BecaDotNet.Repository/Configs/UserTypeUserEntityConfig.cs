using BecaDotNet.Domain.Model;

namespace BecaDotNet.Repository.Configs
{
    public class UserTypeUserEntityConfig : BaseIdentifiedEntityConfig<UserTypeUser>
    {
        public UserTypeUserEntityConfig()
        {
            this.ToTable("TB_USER_TYPE_USER");
            this.Property(p => p.UserId).HasColumnName("USER_ID").HasColumnType("int").IsRequired();
            this.Property(p => p.UserTypeId).HasColumnName("USER_TYPE_ID").HasColumnType("int").IsRequired();
            this.Property(p => p.CreatedDate).HasColumnName("CREATED_DATE").HasColumnType("DATETIME").IsRequired();
            this.Property(p => p.StartDate).HasColumnName("START_DATE").HasColumnType("DATETIME").IsRequired();
            this.Property(p => p.EndDate).HasColumnName("END_DATE").HasColumnType("DATETIME").IsOptional();
        }
    }
}
