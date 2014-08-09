<%@ Page Title="Create" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.Create" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <asp:ListView ID="MateriaListView" runat="server"
        ItemType="Repertoar.MODEL.Material"
        DataKeyNames="MID, KaID, KompID"
        SelectMethod="MaterialListView_GetData"
        OnItemCreated="MaterialListView_ItemCreated"
        OnItemDataBound="MaterialListView_ItemDataBound"
        OnPreRender="MaterialListView_PreRender"
        InsertItemPosition="LastItem">
     <InsertItemTemplate>
            <li>
                <asp:DropDownList ID="ContactTypeDropDownList" runat="server"
                    ItemType="Repertoar.MODEL.Kategori"
                    SelectMethod="KategoriDropDownList_GetData"
                    DataTextField="Name"
                    DataValueField="KaID"
                    SelectedValue='<%# BindItem.KaID %>' />
                <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# BindItem.Namn %>' MaxLength="50" ValidationGroup="vgContactInsert" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Strings, Song_Value_Required %>"
                    ControlToValidate="ValueTextBox" CssClass="field-validation-error" ValidationGroup="vgContactInsert"
                    Display="Dynamic">*</asp:RequiredFieldValidator>

            </li>
        </InsertItemTemplate>
         </asp:ListView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
