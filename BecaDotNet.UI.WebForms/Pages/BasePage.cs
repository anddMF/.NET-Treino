using System;
using System.Web;

namespace BecaDotNet.UI.WebForms.Pages
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            CheckUserIsAuth();
        }

        private void CheckUserIsAuth()
        {
            if (Session["AuthUser"] == null)
                Server.Transfer("~/Login.aspx");
        }
    }
}