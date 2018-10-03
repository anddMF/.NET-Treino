using BecaDotNet.ApplicationService;
using BecaDotNet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace BecaDotNet.UI.WebForms.Pages
{
    public partial class UserList : BasePage
    {
        protected List<User> ResultSet
        {
            get
            {
                if (Session["ResultSet"] == null)
                    Session["ResultSet"] = new List<User>();
                return (List<User>)Session["ResultSet"];
            }
            set => Session["ResultSet"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindData(!IsPostBack);
        }

        private void BindData(bool loadData=true)
        {
            if (loadData)
                LoadData();
            LoadDataForGridView();
        }

        private void LoadDataForGridView()
        {
            gvUserList.DataSource = ResultSet;
            gvUserList.DataBind();
        }

        private void LoadData()
        {
            var userAppService = new UserAppService();
            var result = userAppService.GetUser(new User());
            if (result.IsSuccess)
                ResultSet = ((ResultModelMany<User>)result).ResultObject;
        }

        protected string GetSuperior(int? superiorId)
        {
            if (!superiorId.HasValue)
                return "N/A";

            var result = new UserAppService().GetUser(superiorId.Value);
            return ((ResultModelSingle<User>)result).ResultObject.Name;
        }

        protected void gvUserList_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditUser":
                    {
                        var itemIndex = int.Parse(e.CommandArgument.ToString());
                        var item = ResultSet[itemIndex];
                        Response.Redirect($"~/Pages/UserAccount.aspx?UserId={item.Id}");
                        break;
                    }
                case "RemoveUser":
                    {
                        var itemIndex = int.Parse(e.CommandArgument.ToString());
                        var item = ResultSet[itemIndex];
                        RemoveUser(item);
                        break;
                    }
                default:
                    break;
            }
        }

        private void RemoveUser(User toDelete)
        {
            var svc = new UserAppService();
            var result = svc.RemoveUser(toDelete.Id);
            if (!result.IsSuccess)
            {
                Response.Write($"Erro ao excluir {toDelete.Name}.{result.Messages[0]}");
                return;
            }
            BindData();


        }

        protected void gvUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ResultSet[e.Row.DataItemIndex].UserTypeId == 1)
                    ((Button)e.Row.Controls[7].Controls[0]).Enabled = false;
            }
        }
    }
}