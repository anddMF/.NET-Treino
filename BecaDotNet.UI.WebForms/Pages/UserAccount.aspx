<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="UserAccount.aspx.cs" Inherits="BecaDotNet.UI.WebForms.Pages.UserAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form id="formCadastro" runat="server">
        <h2>User Account</h2>
        <div class="form-group">
            <asp:TextBox ID="txtName" runat="server" placeholder="Nome Completo" required></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtEmail" runat="server" placeholder="E-Mail" required TextMode="Email"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtLogin" runat="server" placeholder="Login" required></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtPassword" runat="server" placeholder="Senha" required TextMode="Password"></asp:TextBox>
        </div>
        <asp:Button id="btnRegister" Text="Cadastrar" runat="server" OnClick="DoRegister"/>
        <asp:Button ID="btnUpdate" Text="Atualizar" runat="server" OnClick="DoUpdate" Visible="false" />
    </form>
</asp:Content>
