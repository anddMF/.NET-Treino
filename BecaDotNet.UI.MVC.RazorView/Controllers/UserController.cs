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
    public class UserController : Controller
    {
        
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Entry()
        {
            return View(DoEntryList());
        }

        [HttpGet]
        public ActionResult MostrarProjetos(int id)
        {
            var listProj = DoEntryList();
            var listaFinal = new List<ProjetoUserViewModel>();
            foreach(var item in listProj)
            {
                if (item.UserId == id)
                    listaFinal.Add(item);
            }
            return View("Entry", listaFinal);

        }

        public List<ProjetoUserViewModel> DoEntryList()
        {
            var modellist = new List<ProjetoUserViewModel>();
            var svc = new ProjetoUserAppSvcGeneric();
            var listModel = svc.FindBy(null);
            foreach(var entry in listModel)
            {
                var model = new ProjetoUserViewModel
                {
                    Id = entry.Id,
                    ProjetoId = entry.ProjetoId,
                    UserId = entry.UserId,
                    IsResponsavel = entry.IsResponsavel,
                    IsActive = entry.IsActive,
                    DataInicio = entry.DateInicio,
                    DataFinal = entry.DataFim??DateTime.MinValue
                };
                model.UserNome = model.GetNomeUser(model.UserId);
                model.ProjNome = model.GetNomeProjeto(model.ProjetoId);
                modellist.Add(model);
            }
            return modellist;
        }
        [HttpGet]
        public ActionResult List()
        { 
            //ViewBag.Lista = DoModelList();
            return View(DoModelList());
        }

        public List<UserViewModel> DoModelList()
        {
            var modelList = new List<UserViewModel>();
            var svc = new UserAppSvcGeneric();
            var listaUsers = svc.FindBy(null);
            foreach (var user in listaUsers)
            {
                var model = new UserViewModel
                {
                    Id = user.Id,
                    NomeUser = user.Name,
                    EmailUser = user.Email,
                    LoginUser = user.Login,
                    PasswordUser = user.Password,
                    ProjetoAtualId = user.ProjetoAtualId,
                    UserTypeId = user.UserTypeId,
                    IsActive = user.IsActive,
                    IdSuperior = user.SuperiorId
                };
                model.ProjNome = model.GetNomeProjeto(model.ProjetoAtualId ?? 0);
                model.SuperiorNome = model.GetSuperiorNome(model.IdSuperior??0);
                modelList.Add(model);
            }
            return modelList;
        }

        [HttpGet]
        public ActionResult New(UserViewModel vm)
        {
            if(vm.Id > 0)
                return View("New", vm);

            var viewm = new UserViewModel();
            return View("New", viewm);
        }

        [HttpGet]
        public ActionResult MakeEdit(int id)
        {
            var model = GetViewModelForEdit(id);
            model.IsEdit = true;
            return View("New", model);
        }

        [HttpGet]
        public ActionResult RemoveUser(int id)
        {
            var svc = new UserAppSvcGeneric();
            var result = svc.Delete(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult FinalizaProjeto(int id)
        {
            var svc = new ProjetoUserAppSvcGeneric();
            var proj = svc.Get(id);
            proj.DataFim = DateTime.Now;
            proj.IsActive = false;
            var update = svc.Update(proj);
            if (svc == null)
                RedirectToAction("Entry");
            return RedirectToAction("Entry");
        }

        [HttpPost]
        public ActionResult SaveUser(UserViewModel model)
        {
            if (model.ProjetoAtualId > 0 && model.Id > 0)
            {
                CreateProjetoUser(model);
                return RedirectToAction("List");
            }
            if (model.Id == 0)
            {
                model.UserTypeId = 2;
                return CreateUserAction(model);
            }
            return EditUserAction(model);
        }

        [HttpGet]
        public ActionResult AdicionarUser(int userId, int projetoId)
        {
            var projetou = new UserViewModel
            {
                ProjetoAtualId = projetoId,
                Id = userId,
            };
            var adicionado = CreateProjetoUser(projetou);
            var svc = new UserAppSvcGeneric();
            var user = svc.Get(userId);
            svc.Update(user);
            return RedirectToAction("List");
        }

        private bool CreateProjetoUser(UserViewModel model)
        {
            var projeto = new ProjetoUser
            {
                ProjetoId = model.ProjetoAtualId??0,
                UserId = model.Id,
                DateInicio = DateTime.Now,
                DataFim = null,
                IsResponsavel = false
            };
            var updateResult = DoUpdateUser(model);
            var svc = new ProjetoUserAppSvcGeneric();
            var created = svc.Create(projeto);
            return created.Id>0;
        }

        private ActionResult CreateUserAction(UserViewModel model)
        {
            var createResult = DoCreateUser(model);
            if (createResult)
                return RedirectToAction("List");
            ViewBag.ErrorMessage = "Erro ao Criar o usuário";
            return View("New", model);
        }
        
        private ActionResult EditUserAction(UserViewModel model)
        {
            var updateResult = DoUpdateUser(model);
            if (updateResult)
                return RedirectToAction("List");
            ViewBag.ErrorMessage = "Erro ao Editar o usuário";
            return View("New", model);
        }

        private bool DoCreateUser(UserViewModel model)
        {
            var svc = new UserAppSvcGeneric();
            var created = svc.Create(model.GetEntity());
            return created.Id > 0;
        }

        private bool DoUpdateUser(UserViewModel model)
        {
            var svc = new UserAppSvcGeneric();
            var updated = svc.Update(model.GetEntity());
            return updated != null;
        }

        private UserViewModel GetViewModelForEdit(int id)
        {
            var user = new UserAppSvcGeneric().Get(id);
            return new UserViewModel
            {
                EmailUser = user.Email,
                LoginUser = user.Login,
                NomeUser = user.Name,
                IsEdit = true,
                Id = id,
                ProjetoAtualId = user.ProjetoAtualId,
                UserTypeId = user.UserTypeId
            };
        }
    }
}