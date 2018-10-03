using BecaDotNet.Domain.Model;
using System;

namespace BecaDotNet.UI.WebForms.Pages
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected User AuthUser
        {
            get => Session["AuthUser"] != null ?
                            (User)Session["AuthUser"] :
                                new User();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}