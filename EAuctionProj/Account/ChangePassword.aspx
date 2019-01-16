<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="EAuctionProj.Account.ChangePassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .PanelPopup
        {
            background-color: White;
            border-color: Black;
            border-style: solid;
            border-width: 2px;
        }

        .TextAreaRemark {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            width:320px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <div style="text-align: left;">
        <div style="width: 400px; margin-left: auto; margin-right: auto;">
            <div class="accountInfo">
                <fieldset class="changePassword">
                    <legend>เปลี่ยนรหัสผ่าน</legend><span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification"
                        ValidationGroup="ChangeUserPasswordValidationGroup" />
                    <p>
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="txtCurrentPassword">รหัสผ่านเดิม:</asp:Label>
                        <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="TextAreaRemark" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="txtCurrentPassword"
                            CssClass="failureNotification" ErrorMessage="กรุณาระบุรหัสผ่านเดิม" ToolTip="Old Password is required."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        กรุณาระบุรหัสผ่านใหม่อย่างน้อย
                        <%= Membership.MinRequiredPasswordLength %>
                        ตัวอักษร.
                    </p>
                    <p>
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="txtNewPassword">รหัสผ่านใหม่:</asp:Label>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="TextAreaRemark" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="txtNewPassword"
                            CssClass="failureNotification" ErrorMessage="กรุณาระบุรหัสผ่านใหม่" ToolTip="New Password is required."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewPassword"
                            ID="regValMinLength" ValidationExpression="^[\s\S]{6,}$" runat="server" ValidationGroup="ChangeUserPasswordValidationGroup"
                            ErrorMessage="กรุณาระบุรหัสผ่านใหม่อย่างน้อย 6 ตัวอักษร." CssClass="failureNotification">*</asp:RegularExpressionValidator>
                    </p>
                    <p>
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="txtConfirmNewPassword">ยืนยันรหัสผ่าน:</asp:Label>
                        <asp:TextBox ID="txtConfirmNewPassword" runat="server" CssClass="TextAreaRemark" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="txtConfirmNewPassword"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="กรุณายืนยันรหัสผ่าน"
                            ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="txtNewPassword"
                            ControlToValidate="txtConfirmNewPassword" CssClass="failureNotification" Display="Dynamic"
                            ErrorMessage="ยืนยันรหัสผ่านไม่ถูกต้อง" ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                        <asp:CustomValidator runat="server" ID="ValidatePass" ValidationGroup="ChangeUserPasswordValidationGroup"
                            CssClass="failureNotification" ForeColor="Red" OnServerValidate="ValidatePass_ServerValidate">*</asp:CustomValidator>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                        Text="เปลี่ยนรหัส" ValidationGroup="ChangeUserPasswordValidationGroup" OnClick="ChangePasswordPushButton_Click" Width="100px" />
                    &nbsp;<asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="ยกเลิก" OnClick="CancelPushButton_Click" Width="100px" />
                </p>
            </div>
        </div>
    </div>
    <p>
        <asp:LinkButton ID="lbtnPopupCtrl" runat="server"></asp:LinkButton>
        <asp:LinkButton ID="lbtnTargerCtrl" runat="server"></asp:LinkButton>
        <act:ModalPopupExtender ID="lbtnPopup_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
            CancelControlID="lbtnPopupCtrl" DynamicServicePath="" Enabled="True" PopupControlID="pnPopup"
            TargetControlID="lbtnTargerCtrl">
        </act:ModalPopupExtender>
        <!-- -->
        <asp:Panel ID="pnPopup" runat="server" CssClass="PanelPopup" Width="30%" Height="25%"
           Style="display: none">
            <table style="width: 100%;" align="center">
                <tr>
                    <td>
                        <table style="width: 100%;">
                          <tr>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style2" align="center">
                                    &nbsp;
                                    <asp:Label ID="lblMsgResult1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMsgResult2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>                                                      
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnOK" runat="server" CausesValidation="False" OnClick="btnOK_Click"
                            Text="ตกลง" Width="100px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </p>
    <asp:HiddenField ID="hdfUserName" runat="server" />
    <asp:HiddenField ID="hdfUsersNo" runat="server" />
    <asp:HiddenField ID="hdfRoleNo" runat="server" />
</asp:Content>
