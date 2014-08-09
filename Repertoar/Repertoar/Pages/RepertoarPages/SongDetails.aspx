<%@ Page Title="Detaljer" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="SongDetails.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.SongDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <%-- ListView som presenterar detaljer för en låt. --%>
    <asp:ListView ID="MateriaListView" runat="server"
        ItemType="Repertoar.MODEL.Material"
        DataKeyNames="MID, KaID, KompID"
        SelectMethod="MaterialListView_GetItem"
        OnItemDataBound="MaterialListView_ItemDataBound">
        <LayoutTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Följande fel inträffade:"
                CssClass="validation-summary-errors" ShowModelStateErrors="False" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="Följande fel inträffade"
                CssClass="validation-summary-errors" ValidationGroup="vgContactInsert" ShowModelStateErrors="False" />
            <ul>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
            </ul>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <p>
                Uppgifter saknas.
            </p>
        </EmptyDataTemplate>

        <ItemTemplate>
                 <%-- Mall för nya rader --%>
                        <h1>
                            <asp:Label ID="NamnLabel" runat="server" Text="<%# Item.Namn %>" />
                        </h1>
            <div id="anteckningar">
                <h3>Anteckningar</h3>
                <p> <%# Item.Anteckning %></p>
            </div>
            <div id="details">
                <ul id="detailsUL">
                         <li>
                             <h3>Instrument:</h3>
                            <asp:Label ID="InstrumentLabel" runat="server" Text="<%# Item.Instrument %> " />
                        </li> 
                        <li>
                            <h3>Status:</h3>
                            <asp:Label ID="StatusLabel" runat="server" Text="<%# Item.Status %> " />
                        </li>
                        <li>
                            <h3>Genre:</h3>
                            <asp:Label ID="GenreLabel" runat="server" Text="<%# Item.Genre %> " />
                        </li>

                        <li>
                            <h3>Kategori:</h3>
                            <asp:Label ID="KategoryNameLabel" runat="server" Text="{0}" /></span>
                        </li> 
            </ul>
                <p>Här ska kompositör finnas...</p>
               </div>
            
             <asp:HyperLink ID="HyperLink1" runat="server" Text="Redigera" 
                            NavigateUrl='<%# GetRouteUrl("EditSong", new { id= Item.MID}) %>' CssClass="nyKund smaller" />
                       
             <asp:LinkButton ID="LinkButton1"  runat="server" CommandName="Delete" Text="Radera"  CssClass="nyKund smaller"
                             OnClientClick='<%# String.Format("return confirm(\"Ta bort kontakten {0}?\")", Item.Namn) %>'/>  
            <hr />   
             <asp:HyperLink ID="HyperLink2" runat="server" Text="Tillbaka" 
                           NavigateUrl="<%$ RouteUrl:routename=Default %>" CssClass="nyKund smaller" />

             </ItemTemplate>        
           
    </asp:ListView>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
