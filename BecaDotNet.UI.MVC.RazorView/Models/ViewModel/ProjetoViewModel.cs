using BecaDotNet.ApplicationService;
using BecaDotNet.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BecaDotNet.UI.MVC.RazorView.Models.ViewModel
{
    public class ProjetoViewModel
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Data inicial")]
        public DateTime StartDate { get; set; }

        public bool IsActive { get; set; }
        [Display(Name = "Data final")]
        public DateTime DataFinal { get; set; }

        public string EndDate {
            get => DataFinal.ToString("yyyy-MM-dd");
            set
            {
                DateTime.TryParse(value, out DateTime newEnd);
                if (newEnd.Date == DateTime.MinValue.Date)
                    newEnd = DateTime.Now.Date;
                DataFinal = newEnd;
            }
        }

        public string DataInicio {
            get => StartDate.ToString("yyyy-MM-dd");
            set
            {
                DateTime.TryParse(value, out DateTime newDate);
                if (newDate.Date == DateTime.MinValue.Date)
                    newDate = DateTime.Now.Date;
                StartDate = newDate;
            }
        }

        [Display(Name = "Id cliente")]
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public int Id { get; set; }

        public IEnumerable<SelectListItem> DdCliente { get; set; }
        public IEnumerable<SelectListItem> DdUser { get; set; }

        public ProjetoViewModel()
        {
            DdCliente = HelperForDropdown<Cliente>.GetDropDownListFrom(
                new ClienteAppSvcGeneric().FindBy(
                    new Cliente { Cnpj = 0 }).Where(w => w.Id != Id),
                "Nome");

            DdUser = HelperForDropdown<ProjetoUser>.GetDropDownListFrom(
                new ProjetoUserAppSvcGeneric().FindBy(
                    new ProjetoUser { Id = 0 }).Where(w => w.Id != Id),
                "ProjetoId");
        }

        public string GetNomeCliente(int idU)
        {
            if (idU == 0)
                return "";

            var cliente = new ClienteAppSvcGeneric().Get(idU);
            return cliente.Nome;
        }

        public Projeto GetEntity()
        {
            return new Projeto
            {
                Nome = Nome,
                ClienteId = ClienteId,
                Id = Id,
                DataInicio = StartDate,
                DataFinal = DataFinal,
                IsActive = true
            };
        }

    }
}