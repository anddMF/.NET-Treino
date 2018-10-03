using BecaDotNet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BecaDotNet.UI.MVC.RazorView.Models
{
    public static class HelperForDropdown<T> where T : IdentifiedEntity
    {
        public static IEnumerable<SelectListItem> GetDropDownListFrom(IEnumerable<T> list, string propName, string defaultText = "Selecione")
        {
            var result = new List<SelectListItem> { new SelectListItem { Text = defaultText, Value = "-1" } };
            foreach (var i in list)
                result.Add(new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.GetType().GetProperty(propName).GetValue(i).ToString()
                });
            return result;
        }
    }
}