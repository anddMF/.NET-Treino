using BecaDotNet.ApplicationService;
using BecaDotNet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BecaDotNet.UI.MVC.RazorView.Models.ViewModel
{
    public class ProjetoUserViewModel
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public string ProjNome { get; set; }
        public int UserId { get; set; }
        public string UserNome { get; set; }
        public bool IsResponsavel { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public bool IsActive { get; set; }

        public string GetNomeUser(int idU)
        {
            if (idU == 0)
                return "";

            var user = new UserAppSvcGeneric().Get(idU);
            return user.Name;
        }

        public string GetNomeProjeto(int idP)
        {
            if (idP == 0)
                return "";

            var proj = new ProjetoAppSvcGeneric().Get(idP);
            return proj.Nome;
        }

        public ProjetoUser GetEntity()
        {
            return new ProjetoUser
            {
                Id = Id,
                ProjetoId = ProjetoId,
                UserId = UserId,
                IsResponsavel = IsResponsavel,
                DateInicio = DataInicio,
                DataFim = DataFinal,
                IsActive = IsActive
            };
        }
    }
}