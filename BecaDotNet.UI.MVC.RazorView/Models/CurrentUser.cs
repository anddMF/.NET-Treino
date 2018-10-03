using BecaDotNet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BecaDotNet.UI.MVC.RazorView.Models
{
    public static class CurrentUser
    {
        public static User UserDate {
            get
            {
                if (HttpContext.Current.Session["CurrentUser"] == null)
                    return new User();
                return (User)HttpContext.Current.Session["CurrentUser"];
            }
        }
    }
}