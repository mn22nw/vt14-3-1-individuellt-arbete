<%@ Page Title="Redigera" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Följande fel inträffade:" 
            CssClass="validation-summary-errors"/>
        <asp:PlaceHolder ID="MessagePlaceHolder" runat="server" Visible="false">
            
        </asp:PlaceHolder>
     <h1>Instrument</h1>
    <asp:RadioButtonList ID="rb" runat="server"></asp:RadioButtonList>
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
        <div class="editor-label">
            <label for="LastName">Efternamn</label>
        </div>
        <div class="editor-field">
            <asp:TextBox ID="LastName" runat="server" MaxLength="50" Text="<%# BindItem.Namn %>"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Ett efternamn måste anges." ControlToValidate="LastName" Display="None"></asp:RequiredFieldValidator>
        </div>
        <div class="editor-label">
            <label for="Email">Email</label>
        </div>
        <div class="editor-field">
            <asp:TextBox ID="Email" runat="server" MaxLength="50" Text="<%# BindItem.Namn %>"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Email-fältet får ej lämnas tomt." ControlToValidate="Email" Display="None"></asp:RequiredFieldValidator>
                         <br />
             <asp:LinkButton ID="LinkButton2"  runat="server" CommandName="Update" Text="SPARA" CssClass="nyKund" />
             <asp:HyperLink ID="HyperLink1"  runat="server" Text="Avbryt" NavigateUrl="<%$ RouteUrl:routename=SongListing %>" CssClass="nyKund" />
            </EditItemTemplate>
           
        </asp:FormView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
