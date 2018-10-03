using BecaDotNet.ApplicationService;
using BecaDotNet.Domain.Model;
using BecaDotNet.UI.MVC.RazorView.Models.Filter;
using BecaDotNet.UI.MVC.RazorView.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BecaDotNet.UI.MVC.RazorView.Controllers
{
    [CustomAuthorize]
    public class ProjetoController : Controller
    {
        // GET: Projeto
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult List()
        {
            return View(DoList());
        }

        public List<ProjetoViewModel> DoList()
        {
            var list = new List<ProjetoViewModel>();
            var svc = new ProjetoAppSvcGeneric();
            var listaProjetos = svc.FindBy(null);
            foreach (var cli in listaProjetos)
            {
                var model = new ProjetoViewModel
                {
                    Id = cli.Id,
                    ClienteId = cli.ClienteId,
                    StartDate = cli.DataInicio,
                    Nome = cli.Nome,
                    IsActive = cli.IsActive,
                    DataFinal = cli.DataFinal
                };
                model.ClienteNome = model.GetNomeCliente(model.ClienteId);
                list.Add(model);
            }
            return list;
        }
        [HttpGet]
        public ActionResult New(ProjetoViewModel model)
        {
            var svc = new ProjetoAppSvcGeneric();
            var vm = svc.Get(model.Id);
            if (vm == null)
            {
                var vim = new ProjetoViewModel();
                return View("New", vim);
            }

            if (vm.Id > 0)
                return View("New", model);

            var viewm = new ProjetoViewModel();
            return View("New", viewm);
        }

        [HttpGet]
        public ActionResult Users()
        {
            return View();
        }
        
        public ActionResult AddUser(int projetoId)
        {
            var modelList = new List<UserViewModel>();
            var svc = new UserAppSvcGeneric();
            var listaUsers = svc.FindBy(null);
            foreach(var item in listaUsers)
            {
                if (item.ProjetoAtualId == null)
                {
                    var model = new UserViewModel
                    {
                        Id = item.Id,
                        NomeUser = item.Name,
                        LoginUser = item.Login,
                        IsActive = item.IsActive,
                        IdSuperior = item.SuperiorId,
                        ProjetoAtualId = projetoId,
                        EmailUser = item.Email
                    };
                    modelList.Add(model);
                }
            }
            return View("Users",modelList);
        }

        [HttpGet]
        public ActionResult MakeEdit(int id)
        {
            var model = GetViewModelForEdit(id);
            return View("New", model);
        }

        private ProjetoViewModel GetViewModelForEdit(int id)
        {
            var user = new ProjetoAppSvcGeneric().Get(id);
            return new ProjetoViewModel
            {
                Id = user.Id,
                ClienteId = user.ClienteId,
                StartDate = user.DataInicio,
                DataFinal = user.DataFinal,
                IsActive = user.IsActive,
                Nome = user.Nome
            };
        }

        [HttpPost]
        public ActionResult SaveProjeto(ProjetoViewModel model)
        {
            if (model.Id == 0)
                return CreateProjetoAction(model);
            return EditProjetoAction(model);
        }

        [HttpGet]
        public ActionResult RemoveProjeto(int id)
        {
            var svc = new ProjetoAppSvcGeneric();
            var result = svc.Delete(id);
            return RedirectToAction("List");
        }

        private ActionResult CreateProjetoAction(ProjetoViewModel model)
        {
            var createResult = DoCreateProjeto(model);
            if (createResult)
                return RedirectToAction("List");
            ViewBag.ErrorMessage = "Erro ao Criar o Projeto";
            return View("New", model);
        }

        private ActionResult EditProjetoAction(ProjetoViewModel model)
        {
            var updateResult = DoUpdateProjeto(model);
            if (updateResult)
                return RedirectToAction("List");
            ViewBag.ErrorMessage = "Erro ao Editar o Projeto";
            return View("New", model);
        }

        private bool DoCreateProjeto(ProjetoViewModel model)
        {
            var svc = new ProjetoAppSvcGeneric();
            var created = svc.Create(model.GetEntity());
            return created.Id > 0;
        }
        private bool DoUpdateProjeto(ProjetoViewModel model)
        {
            var svc = new ProjetoAppSvcGeneric();
            var updated = svc.Update(model.GetEntity());
            return updated != null;
        }

    }
}