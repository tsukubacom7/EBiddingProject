<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopupMessage.ascx.cs"
    Inherits="EAuctionProj.Popup.PopupMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!-- style="display:none" -->
<asp:Panel ID="pnMaster" runat="server" style="display:none" Width="400px">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnHeader" runat="server" Font-Bold="True">
                    <asp:Label ID="lblHeader" runat="server" Text="Warning" SkinID="H1"></asp:Label>
                </asp:Panel>
                <cc1:RoundedCornersExtender ID="pnHeader_RoundedCornersExtender" runat="server" Enabled="True"
                    TargetControlID="pnHeader">
                </cc1:RoundedCornersExtender>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Panel ID="panelDetail" runat="server">
                    <asp:ListBox ID="lMessage" runat="server" Width="300px"></asp:ListBox>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="center">
            <td>
                <asp:Panel ID="pnTail" runat="server">
                    <asp:LinkButton ID="lnkClose" runat="server" ForeColor="Red">ปิด</asp:LinkButton>
                </asp:Panel>
            </td>
        </tr>
        <tr align="center">
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:LinkButton ID="lnkDummy" runat="server" Style="display: none"></asp:LinkButton>
<cc1:ModalPopupExtender ID="lnkDummy_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
    CancelControlID="lnkClose" DynamicServicePath="" Enabled="True" PopupControlID="pnMaster"
    TargetControlID="lnkDummy">
</cc1:ModalPopupExtender>
