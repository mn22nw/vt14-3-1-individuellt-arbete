<%@ Page Title="Redigera" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Följande fel inträffade:" 
            CssClass="validation-summary-errors"/>
        <asp:PlaceHolder ID="MessagePlaceHolder" runat="server" Visible="false">
            
        </asp:PlaceHolder>
     <h1>Instrument</h1>
      <asp:ListView ID="MaterialListView" runat="server"
        ItemType="Repertoar.MODEL.Material"
        DataKeyNames="MID, KaID, KompID"
        SelectMethod="MaterialListView_GetData"
        InsertMethod="MaterialListView_InsertItem"
        UpdateMethod="MaterialListView_UpdateItem"
        OnItemDataBound="MaterialListView_ItemDataBound" >
    <EditItemTemplate>
    <asp:RadioButtonList ID="rb" runat="server"></asp:RadioButtonList>
       
     <asp:View ID="EditView" runat="server">
    <asp:DropDownList ID="DropDownList1" runat="server"
                    ItemType="Repertoar.MODEL.Kategori"
                    DataKeyNames="KaID"
                    SelectMethod="ContactTypeDropDownList_GetData"
                    DataTextField="Name"
                    DataValueField="KaID"
                    SelectedValue='<%# BindItem.KaID %>' />
                <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Value") %>' MaxLength="50" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Error"
                    ControlToValidate="ValueTextBox" CssClass="field-validation-error" Display="Dynamic">*</asp:RequiredFieldValidator>
         </asp:View>                   
    
        <asp:View ID="View1" runat="server">
                    <li>
                        <asp:DropDownList ID="KategoryDropDownList" runat="server"
                            ItemType="Repertoar.MODEL.Kategori"
                            SelectMethod="ContactTypeDropDownList_GetData"
                            DataTextField="Name"
                            DataValueField="KaID"
                            SelectedValue='<%# Item.KaID %>'
                            Enabled="false" />
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Item.Namn %>' MaxLength="50" Enabled="false" />
                        <%-- "Kommandknappar" för att redigera och ta bort en kunduppgift . Kommandonamnen är VIKTIGA! --%>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                        <asp:HyperLink ID="HyperLink1" runat="server" Text="Ta bort" NavigateUrl='<%# GetRouteUrl("DeleteSong", new { id = Item.MID }) %>' />
                    </li>
                </asp:View>
    
    
     <asp:FormView ID="FormView1" runat="server" 
            ItemType="Repertoar.MODEL.Material"
            DefaultMode="Edit"
            RenderOuterTable="false" 
            SelectMethod="ContactFormView_GetSong"
            UpdateMethod="ContactFormView_UpdateSong"
            DataKeyNames="MID"
                >
         <EditItemTemplate>
        <div class="editor-label">
            <label for="Name">Namn</label>
        </div>
            
            
          
        <div class="editor-field">
            <asp:TextBox ID="Name" runat="server" MaxLength="50" Text="<%# BindItem.Namn %>"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Ett förnamn måste anges." ControlToValidate="Name" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
       
            </EditItemTemplate>
           
        </asp:FormView>
        </EditItemTemplate>
          </asp:ListView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
