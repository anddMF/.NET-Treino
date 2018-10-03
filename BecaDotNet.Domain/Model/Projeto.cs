using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecaDotNet.Domain.Model
{
    public class Projeto : IdentifiedEntity
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }

        public Projeto()
        {
            //DataInicio = DateTime.Now;
            //DataFinal = DateTime.Now;
        }
    }
}
