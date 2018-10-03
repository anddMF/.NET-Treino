using BecaDotNet.ApplicationService;
using BecaDotNet.Domain.Model;
using BecaDotNet.UI.MVC.RazorView.Models.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BecaDotNet.UI.MVC.RazorView.Controllers
{
    [CustomAuthorize]
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult List()
        {
            return View(DoList());
        }
        public List<Cliente> DoList()
        {
            var list = new List<Cliente>();
            var svc = new ClienteAppSvcGeneric();
            var listaUsers = svc.FindBy(null);
            foreach (var cli in listaUsers)
            {
                var model = new Cliente
                {
                    Id = cli.Id,
                    Cnpj = cli.Cnpj,
                    Contato = cli.Contato,
                    Nome = cli.Nome,
                    IsActive = cli.IsActive,
                    Projetos = cli.Projetos
                };
                list.Add(model);
            }
            return list;
        }

        [HttpGet]
        public ActionResult New(int id)
        {
            var svc = new ClienteAppSvcGeneric();
            var vm = svc.Get(id);
            if (vm == null)
            {
                var vim = new Cliente();
                return View("New", vim);
            }

            if (vm.Id > 0)
                return View("New", vm);

            var viewm = new Cliente();
            return View("New", viewm);
        }

        [HttpPost]
        public ActionResult SaveCliente(Cliente model)
        {
            if (model.Id == 0)
                return CreateClienteAction(model);
            return EditClienteAction(model);
        }

        [HttpGet]
        public ActionResult RemoveCliente(int id)
        {
            var svc = new ClienteAppSvcGeneric();
            var result = svc.Delete(id);
            return RedirectToAction("List");
        }

        private ActionResult CreateClienteAction(Cliente model)
        {
            var createResult = DoCreateCliente(model);
            if (createResult)
                return RedirectToAction("List");
            ViewBag.ErrorMessage = "Erro ao Criar o cliente";
            return View("New", model);
        }

        private ActionResult EditClienteAction(Cliente model)
        {
            var updateResult = DoUpdateCliente(model);
            if (updateResult)
                return RedirectToAction("List");
            ViewBag.ErrorMessage = "Erro ao Editar o cliente";
            return View("New", model);
        }

        private bool DoCreateCliente(Cliente model)
        {
            var svc = new ClienteAppSvcGeneric();
            var created = svc.Create(model);
            return created.Id > 0;
        }

        private bool DoUpdateCliente(Cliente model)
        {
            var svc = new ClienteAppSvcGeneric();
            var updated = svc.Update(model);
            return updated != null;
        }

    }
}