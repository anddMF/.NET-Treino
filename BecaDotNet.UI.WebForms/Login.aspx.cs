using BecaDotNet.ApplicationService;
using BecaDotNet.Domain.Model;
using System;
using System.Web.Script.Services;
using System.Web.Services;

namespace BecaDotNet.UI.WebForms
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DoLogin(object s, EventArgs e)
        {
            var login = txtLogin.Text;
            var password = txtPassword.Text;
            var authResult = Authenticate(login, password);
            if (authResult.IsSuccess)
            {
                Session["AuthUser"] = ((ResultModelSingle<User>)authResult).ResultObject;
                Response.Redirect("~/Pages/Home.aspx");
            }
            else
                Response.Write(authResult.Messages[0]);
        }

        private static ResultModel Authenticate(string login, string password)
        {
            var userAppService = new UserAppService();
            var authResult = userAppService.AuthenticateUser(login, password);
            return authResult;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object AuthFromAjax(string login, string password)
        {
            var authResult = Authenticate(login, password);
            return authResult.IsSuccess ? 
                    (ResultModelSingle<User>)authResult :
                     authResult;
        }
    }
}