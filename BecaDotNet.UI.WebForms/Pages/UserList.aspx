<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="BecaDotNet.UI.WebForms.Pages.UserList" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form id="form1" runat="server">
        <h2>User List</h2>
        <asp:GridView ID="gvUserList" runat="server" AutoGenerateColumns="false"
            ItemType="BecaDotNet.Domain.Model.User" CssClass="table" 
            OnRowCommand="gvUserList_RowCommand"
            OnRowDataBound="gvUserList_RowDataBound">
            <HeaderStyle CssClass="thead-dark" />
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Nome" />
                <asp:BoundField DataField="Login" HeaderText="Login" />
                <asp:BoundField DataField="RegisterDate" HeaderText="Data de Cadastro" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                <asp:TemplateField HeaderText="Usuário Ativo">
                    <ItemTemplate>
                        <%# (Item.IsActive ? "Sim" : "Não" ) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo de Usuário">
                    <ItemTemplate>
                        <%#Item.UserType.Description %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Superior">
                    <ItemTemplate>
                        <%#GetSuperior(Item.SuperiorId) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Button" CommandName="EditUser" Text="Editar" ControlStyle-CssClass="btn btn-success"/>
                <asp:ButtonField ButtonType="Button" CommandName="RemoveUser" Text="Remover" ControlStyle-CssClass="btn btn-danger"/>

            </Columns>
        </asp:GridView>
    </form>
</asp:Content>
