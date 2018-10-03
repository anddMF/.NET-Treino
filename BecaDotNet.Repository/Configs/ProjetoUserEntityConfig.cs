using BecaDotNet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecaDotNet.Repository.Configs
{
    class ProjetoUserEntityConfig : BaseIdentifiedEntityConfig<ProjetoUser>
    {
        public ProjetoUserEntityConfig()
        {
            this.ToTable("TB_PROJETO_USER");
            this.Property(p => p.UserId).HasColumnName("USER_ID").HasColumnType("int").IsRequired();
            this.Property(p => p.ProjetoId).HasColumnName("PROJETO_ID").HasColumnType("int").IsRequired();
            this.Property(p => p.IsResponsavel).HasColumnName("RESPONSAVEL").HasColumnType("bit").IsRequired();
            this.Property(p => p.DateInicio).HasColumnName("DATA_INICIO").HasColumnType("DateTime").IsRequired();
            this.Property(p => p.DataFim).HasColumnName("DATA_FIM").HasColumnType("DateTime").IsOptional();
        }
    }
}
