using BecaDotNet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecaDotNet.Repository.Configs
{
    public class ClienteEntityConfig : BaseIdentifiedEntityConfig<Cliente>
    {
        public ClienteEntityConfig():base()
        {
            this.ToTable("TB_CLIENTE");
            this.Property(p => p.Nome).HasColumnName("NOME").HasColumnType("varchar").HasMaxLength(200).IsRequired();
            this.Property(p => p.Cnpj).HasColumnName("CNPJ").HasColumnType("bigint").IsRequired();
            this.Property(p => p.Contato).HasColumnName("CONTATO").HasColumnType("varchar").HasMaxLength(200).IsRequired();
        }
    }
}
