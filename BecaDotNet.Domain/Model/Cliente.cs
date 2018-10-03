using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecaDotNet.Domain.Model
{
    public class Cliente : IdentifiedEntity
    {
        public string Nome { get; set; }
        public long Cnpj { get; set; }
        public string Contato { get; set; }

        public ICollection<Projeto> Projetos { get; set; }
    }
}
