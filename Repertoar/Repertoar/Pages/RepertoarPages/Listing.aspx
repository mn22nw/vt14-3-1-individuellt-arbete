<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.Listing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
   <h1>REPERTOAR</h1>

    <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false" CssClass="icon-ok">
                <asp:Literal runat="server" ID="SuccessMessageLiteral" />

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Följande fel inträffade:" 
                 CssClass="validation-summary-errors"/>
     </asp:Panel>

     <asp:ListView ID="ContactListView" runat="server" 
            ItemType="Repertoar.MODEL.Material"
            SelectMethod="ContactListView_GetData"
            DeleteMethod="ContactListView_DeleteSong"
            DataKeyNames="MID"
            >
            
            <LayoutTemplate>
                <table class="HeaderTb">
                    <tr>
                        <th class="firstName">Namn:
                        </th>
                        <th class="lastName">Kategori:
                        </th>
                        <th class="emailTb">Genré:
                        </th>
                         <th class="emailTb">Status:
                        </th>
                        </th>
                         <th class="emailTb">Intrument:
                        </th>
                    </tr>
                <%-- Platshallare för nya rader --%>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </table>
                

            </LayoutTemplate>
            <ItemTemplate>
                 <%-- Mall för nya rader --%>
                    <table id="allContactsTable" >
                    <tr>
                        <th class="firstName">
                            <asp:Label ID="NamnLabel" runat="server" Text="<%# Item.Namn %>" />
                        </th>
                        <th class="lastName">
                            <asp:Label ID="KategoryLabel" runat="server" Text="<%# Item.KaID %>" />
                        </th>
                        <th class="emailTb">
                            <asp:Label ID="GenreLabel" runat="server" Text="<%# Item.Genre %> " />
                        </th>
                        <th class="firstName">
                            <asp:Label ID="StatusLabel" runat="server" Text="<%# Item.Status %> " />
                        </th>
                         <th class="firstName">
                            <asp:Label ID="InstrumentLabel" runat="server" Text="<%# Item.Instrument %> " />
                        </th>
                         <th>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text="Redigera" NavigateUrl='<%# GetRouteUrl("EditSong", new { id= Item.MID}) %>' CssClass="nyKund smaller" />
                        </th>
                        <th>
                             <asp:LinkButton ID="LinkButton1"  runat="server" CommandName="Delete" Text="Radera"  CssClass="nyKund smaller"
                              OnClientClick='<%# String.Format("return confirm(\"Ta bort kontakten {0}?\")", Item.Namn) %>'/>
                        </th>
                       
                    </tr>
                        <hr />
                        </table>
            </ItemTemplate>        
            <EmptyDataTemplate>
                <%-- Om kunduppgifter saknas --%>
                <table class="grid">
                    <tr>

                        <td>
                            Uppgifter saknas.
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
 </asp:ListView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
