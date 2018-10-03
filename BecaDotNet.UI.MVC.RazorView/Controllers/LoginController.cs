using BecaDotNet.ApplicationService;
using BecaDotNet.UI.MVC.RazorView.Models;
using System;
using System.Web.Mvc;

namespace BecaDotNet.UI.MVC.RazorView.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //Session.Clear para logout, depois redirect
            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            return View();
        }

        [HttpPost]
        public ActionResult Index(string login, string password, string returnUrl)
        {
            var svc = new UserAppSvcGeneric();
            var result = svc.Authenticate(login, password);
            if (result != null)
            {
                HttpContext.Session["CurrentUser"] = result;
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("Index", "Home");
                var newRoute = UrlToActionHelper.GetRouteFromUrl(returnUrl);
                return RedirectToAction(newRoute["Action"], newRoute["Controller"]);
            }
            ViewBag.LoginError = "Usuário ou senha inválidos";
            return View();
        }

        [HttpGet]
        public ActionResult DoLogout()
        {
            HttpContext.Session["CurrentUser"] = null;
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}