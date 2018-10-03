using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecaDotNet.Domain.Model
{
    public class ProjetoUser : IdentifiedEntity
    {
        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsResponsavel { get; set; }
        public DateTime DateInicio { get; set; }
        public DateTime? DataFim { get; set; }

    }
}
