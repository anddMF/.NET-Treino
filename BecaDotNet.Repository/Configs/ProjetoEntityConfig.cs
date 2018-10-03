using BecaDotNet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecaDotNet.Repository.Configs
{
    public class ProjetoEntityConfig : BaseIdentifiedEntityConfig<Projeto>
    {
        public ProjetoEntityConfig():base()
        {
            this.ToTable("TB_PROJETO");
            this.Property(p => p.Nome).HasColumnName("NOME").HasColumnType("varchar").HasMaxLength(200).IsRequired();
            this.Property(p => p.DataInicio).HasColumnName("DATA_INICIO").HasColumnType("DATETIME").IsRequired();
            this.Property(p => p.DataFinal).HasColumnName("DATA_FINAL").HasColumnType("DATETIME").IsRequired();
            this.Property(p => p.ClienteId).HasColumnName("CLIENTE_ID").HasColumnType("int").IsRequired();
        }
    }
}
