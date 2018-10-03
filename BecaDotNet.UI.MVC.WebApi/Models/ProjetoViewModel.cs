using BecaDotNet.ApplicationService;
using BecaDotNet.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BecaDotNet.UI.MVC.WebApi.Models
{
    public class ProjetoViewModel
    {
        public string Nome { get; set; }
        public DateTime StartDate { get; set; }

        public bool IsActive { get; set; }
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
       
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public int Id { get; set; }

        public ProjetoViewModel()
        {
            
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