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
    public class UserViewModel
    {
        
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Nome")]
        public string NomeUser { get; set; }

        [Required(ErrorMessage = "Informe o email")]
        [Display(Name = "Email")]
        public string EmailUser { get; set; }

        [Required(ErrorMessage = "Informe o login")]
        [Display(Name = "Login")]
        public string LoginUser { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [Display(Name = "Senha")]
        public string PasswordUser { get; set; }

        [Display(Name = "ID do projeto")]
        public int? ProjetoAtualId { get; set; }

        [Display(Name = "Tipo de usuário")]
        public int UserTypeId { get; set; }
        
        [Display(Name = "Data final")]
        public DateTime DataFinal { get; set; }

        public bool IsResponsavel { get; set; }
        public int? IdSuperior { get; set; }
        public string EndDate
        {
            get => DataFinal.ToString("yyyy-MM-dd");
            set
            {
                DateTime.TryParse(value, out DateTime newEnd);
                if (newEnd.Date == DateTime.MinValue.Date)
                    newEnd = DateTime.Now.Date;
                DataFinal = newEnd;
            }
        }

        public string DataInicio
        {
            get => StartDate.ToString("yyyy-MM-dd");
            set
            {
                DateTime.TryParse(value, out DateTime newDate);
                if (newDate.Date == DateTime.MinValue.Date)
                    newDate = DateTime.Now.Date;
                StartDate = newDate;
            }
        }

        public string SuperiorNome { get; set; }
        public bool IsEdit { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public string ProjNome { get; set; }
        public IEnumerable<SelectListItem> DdlUser { get; set; }
        public IEnumerable<SelectListItem> DdlProj { get; set; }

        public UserViewModel()
        {
            DdlUser = HelperForDropdown<User>.GetDropDownListFrom(
                new UserAppSvcGeneric().FindBy(
                    new User { UserTypeId = 0 }).Where(w => w.Id != Id),
                "Name");

            DdlProj = HelperForDropdown<Projeto>.GetDropDownListFrom(
                new ProjetoAppSvcGeneric().FindBy(
                    new Projeto()).Where(w => w.Id != Id),
                "Nome");
        }

        public string GetNomeProjeto(int idP)
        {
            if (idP == 0)
                return "";

            var proj = new ProjetoAppSvcGeneric().Get(idP);
            return proj.Nome;
        }
        public string GetSuperiorNome(int idP)
        {
            if (idP == 0)
                return "";

            var proj = new UserAppSvcGeneric().Get(idP);
            return proj.Name;
        }

        public User GetEntity()
        {
            return new User
            {
                Name = NomeUser,
                Email = EmailUser,
                Login = LoginUser,
                Password = PasswordUser,
                Id = Id,
                RegisterDate = DateTime.Now,
                UserTypeId = UserTypeId,
                ProjetoAtualId = ProjetoAtualId,
                SuperiorId = IdSuperior,
                IsActive = IsActive
            };
        }

    }
}