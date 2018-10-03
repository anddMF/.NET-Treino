using System.Web.Mvc;

namespace BecaDotNet.UI.MVC.RazorView.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}