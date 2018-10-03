<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BecaDotNet.UI.WebForms.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/Default.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
    <div class="jumbotron">
        <div class="mx-auto col-5">
            <h1>Login</h1>
            <form id="form1" runat="server">
                <div class="form-group">
                    <asp:TextBox runat="server" ID="txtLogin" placeholder="Login" required></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Senha" required></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button runat="server" ID="btnLogin" Text="Logar" OnClick="DoLogin" CssClass="btn btn-success" />
                </div>
                <div class="form-group">
                    <button id="btnLoginAjax" class="btn btn-success">Login Ajax</button>
                </div>
            </form>
            <script>
                $(function () {
                    $('#btnLoginAjax').on('click', function (e) {
                        var login = $('#txtLogin').val();
                        var password = $('#txtPassword').val();
                        $.ajax({
                            type: 'POST',
                            url: 'Login.aspx/AuthFromAjax',
                            data: '{"login":"'+login+'","password":"'+password+'"}',
                            contentType: 'application/json',
                            dataType:'json',
                            success: function (r) {
                                if (r.d.IsSuccess) {
                                    window.location = '/Pages/Home.aspx';
                                } else {
                                    alert(r.d.Messages[0]);
                                }
                            },
                            error: function (err) {
                                alert('Deu muito ruim');
                            }
                        });
                    });
                });


            </script>
        </div>
    </div>
</body>
</html>
