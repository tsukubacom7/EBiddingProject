<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupPage.aspx.cs" Inherits="EAuctionProj.Popup.PopupPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 200px; height: 100px;">
            <tr>
                <td align="center" colspan="3">
                    Modify Name
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxName" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Modify" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>       
    </div>
    </form>
</body>
</html>
