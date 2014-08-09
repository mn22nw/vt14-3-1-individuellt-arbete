<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="SongDetails.aspx.cs" Inherits="Repertoar.Pages.RepertoarPages.SongDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

      <%-- ListView som presenterar detaljer för en låt. --%>
    <asp:ListView ID="MateriaListView" runat="server"
        ItemType="Repertoar.MODEL.Material"
        DataKeyNames="MID, KaID, KompID"
        SelectMethod="MaterialListView_GetData"
        OnItemCreated="MaterialListView_ItemCreated"
        OnItemDataBound="MaterialListView_ItemDataBound"
        OnPreRender="MaterialListView_PreRender"
        InsertItemPosition="LastItem">
        <LayoutTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Följande fel inträffade:"
                CssClass="validation-summary-errors" ShowModelStateErrors="False" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="<%$ Resources:Strings, Validation_Header %>"
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
            <asp:MultiView ID="ContactMultiView" runat="server" ActiveViewIndex="0">
                <asp:View ID="ReadOnlyView" runat="server">
                    <%-- ListView som presenterar en kunds kontaktuppgifter. --%>
                    <li>
                        <%-- Label-kontrollen uppgift är att visa vilken typ kontaktinformationen är av. --%>
                        <asp:Label ID="ContactTypeNameLabel" runat="server" Text="{0}: " /><span><%#: Item.Namn %></span>
                    </li>
                </asp:View>
                
            </asp:MultiView>
        </ItemTemplate>
        <InsertItemTemplate>
            <li>
                <asp:DropDownList ID="ContactTypeDropDownList" runat="server"
                    ItemType="GeekCustomer.Model.ContactType"
                    SelectMethod="ContactTypeDropDownList_GetData"
                    DataTextField="Name"
                    DataValueField="ContactTypeId"
                    SelectedValue='<%# BindItem.KaID %>' />
                <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# BindItem.Namn %>' MaxLength="50" ValidationGroup="vgContactInsert" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Strings, Contact_Value_Required %>"
                    ControlToValidate="ValueTextBox" CssClass="field-validation-error" ValidationGroup="vgContactInsert"
                    Display="Dynamic">*</asp:RequiredFieldValidator>

            </li>
        </InsertItemTemplate>
    </asp:ListView>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
