<%@ Page Title="Log In" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"
    CodeBehind="Login.aspx.cs" Inherits="EAuctionProj.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        function clickButton(e, buttonid) {
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (navigator.appName.indexOf("Netscape") > (-1)) {
                    if (e.keyCode == 13) {
                        bt.click();
                        return false;
                    }
                }
                if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                    if (event.keyCode == 13) {
                        bt.click();
                        return false;
                    }
                }
            }
        }
    </script>
    <style type="text/css">
        .TextAreaRemark {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            width:320px;
        }
    </style>

    <div style="text-align: left;">
        <div style="width: 400px; margin-left: auto; margin-right: auto;">
            <p>
            </p>
        </div>
    </div>
    <div style="text-align: left;">
        <div style="width: 400px; margin-left: auto; margin-right: auto;">
            <%--  <layouttemplate>
             </layouttemplate>--%>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>ลงชื่อเข้าใช้งาน</legend><span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                        ValidationGroup="LoginUserValidationGroup" />
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUserName">Username:</asp:Label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="TextAreaRemark"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                            CssClass="failureNotification" ErrorMessage="กรุณาระบุชื่อผู้ใช้งาน" ToolTip="กรุณาระบุชื่อผู้ใช้งาน"
                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtPassword">Password:</asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="TextAreaRemark" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                            CssClass="failureNotification" ErrorMessage="กรุณาระบุรหัสผู้ใช้งาน" ToolTip="กรุณาระบุรหัสผู้ใช้งาน"
                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator runat="server" ID="ValidatePass" ValidationGroup="LoginUserValidationGroup"
                            ErrorMessage="ชื่อผู้ใช้งานหรือรหัสไม่ถูกต้อง" CssClass="failureNotification"
                            ForeColor="Red" OnServerValidate="ValidatePass_ServerValidate">*</asp:CustomValidator>
                    </p>
                    <p>
                        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false"
                            NavigateUrl="~/Form/UserRegister.aspx">สร้างบัญชีผู้ใช้งาน</asp:HyperLink>
                        &nbsp;<br />
                        ลืมรหัสผ่านกรุณาติดต่อที่เบอร์ 02-0804499
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup"
                        OnClick="LoginButton_Click" Width="100px"/>
                </p>
            </div>
        </div>
    </div>
</asp:Content>
