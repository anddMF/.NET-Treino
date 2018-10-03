using BecaDotNet.ApplicationService;
using BecaDotNet.Domain.Model;
using System;

namespace BecaDotNet.UI.WebForms.Pages
{
    public partial class UserAccount : BasePage
    {
        private User EditingUser
        {
            get {
                if (Session["EditingUser"] == null)
                    Session["EditingUser"] = new User();
                return (User)Session["EditingUser"];
            }
            set => Session["EditingUser"] = value;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SetDataForEdit();
        }

        private void SetDataForEdit()
        {
            try
            {
                if (Request.QueryString.HasKeys())
                {
                    int.TryParse(Request.QueryString["UserId"], out int userId);
                    var user = GetUserById(userId);
                    if (user.Id > 0)
                    {
                        EditingUser = user;
                        txtName.Text = user.Name;
                        txtEmail.Text = user.Email;
                        txtEmail.Enabled = false;
                        txtLogin.Text = user.Login;
                        txtLogin.Enabled = false;
                        txtPassword.Visible = false;
                        btnRegister.Visible = false;
                        btnUpdate.Visible = true;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private User GetUserById(int userId)
        {
            return ((ResultModelSingle<User>)new UserAppService().GetUser(userId)).ResultObject;
        }
        
        protected void DoRegister(object s, EventArgs e)
        {
            var userAppService = new UserAppService();
            var newUser = new User
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Login = txtLogin.Text,
                Password = txtPassword.Text
            };
            var resultUser = userAppService.RegisterUser(newUser);
            if (resultUser.IsSuccess)
                Response.Redirect("~/Pages/UserList.aspx");
        }

        protected void DoUpdate(object s, EventArgs e)
        {
            if (!EditingUser.Name.Equals(txtName.Text))
            {
                EditingUser.Name = txtName.Text;
                var appService = new UserAppService();
                var result = appService.UpdateUser(EditingUser);
                if (!result.IsSuccess)
                {
                    Response.Write(result.Messages[0]);
                    return;
                }
                Response.Redirect("~/Pages/UserList.aspx");
            }
            return;
        }
    }
}