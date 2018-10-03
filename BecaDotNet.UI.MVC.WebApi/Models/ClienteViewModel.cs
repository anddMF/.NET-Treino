using BecaDotNet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BecaDotNet.UI.MVC.WebApi.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public long Cnpj { get; set; }
        public string Contato { get; set; }
        public bool IsActive { get; set; }

        public Cliente GetEntity()
        {
            return new Cliente
            {
                Nome = Nome,
                //Id = Id,
                Cnpj = Cnpj,
                Contato = Contato,
                IsActive = IsActive
            };
        }
    }
}