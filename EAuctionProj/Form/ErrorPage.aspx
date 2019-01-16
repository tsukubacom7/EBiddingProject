<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="EAuctionProj.Form.ErrorPage"
    MasterPageFile="~/Site.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <h2>
      <br />
    </h2>
    <fieldset>
        <div align="center" style="font-size: 14px; font-weight: bold;">
            <br />
            There is a problem, Please try again or reload the entire page.
            <br />
            <br />
        </div>
    </fieldset>
</asp:Content>
