<%@ Page Title="User Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="EditUserRegister.aspx.cs" Inherits="EAuctionProj.EditUserRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
        }

        .style3 {
            width: 508px;
        }

        .style4 {
            width: 160px;
        }

        .TextAreaRemark {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .tbStyle {
            width: 100%;
            position: relative;
            left: 5%;
        }

        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .PanelPopup {
            background-color: White;
            border-color: Black;
            border-style: solid;
            border-width: 2px;
        }
    </style>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function CloseWindow() {
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:Label ID="Label7" runat="server" Text="แก้ไขข้อมูลบริษัท" CssClass="lblHeader"></asp:Label>
    <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <p>
                <table class="tbStyle">
                    <tr>
                        <td class="style2" colspan="2">
                            <asp:Label ID="Label10" runat="server" Text="แก้ไขข้อมูลบริษัทเพื่อเข้าร่วมการประมูลโครงการ จัดซื้อ/จัดจ้าง" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                                ValidationGroup="RegisterUserValidationGroup" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label18" runat="server" Text="ประเภทผู้เข้าร่วมประมูล :"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:DropDownList ID="ddlUserType" runat="server" Width="30%" CssClass="TextAreaRemark">
                                <asp:ListItem Value="0">== กรุณาเลือก ==</asp:ListItem>
                                <asp:ListItem Value="1">บริษัท</asp:ListItem>
                                <asp:ListItem Value="2">บุคคลธรรมดา</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label1" runat="server" Text="ชื่อบริษัท/ชื่อ-นามสกุลผู้เข้าร่วมประมูล :"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtCompanyName" runat="server" Width="70%" CssClass="TextAreaRemark"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CompanyRequired" runat="server" ControlToValidate="txtCompanyName"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุชื่อบริษัท/ชื่อ-นามสกุลผู้เข้าร่วมประมูล"
                                ToolTip="กรุณาระบุชื่อบริษัท/ชื่อ-นามสกุลผู้เข้าร่วมประมูล" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator runat="server" ID="ValidateTxt" ValidationGroup="RegisterUserValidationGroup"
                                CssClass="failureNotification" OnServerValidate="ValidateTxt_ServerValidate">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label2" runat="server" Text="เลขที่ผู้เสียภาษี :"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtTaxID" runat="server" Width="70%" CssClass="TextAreaRemark"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TaxIDRequire" runat="server" ControlToValidate="txtTaxID"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุเลขที่ผู้เสียภาษี" ToolTip="กรุณาระบุเลขที่ผู้เสียภาษี"
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="valTaxNumeric" runat="server" ErrorMessage="กรุณาระบุเลขที่ผู้เสียภาษี เป็นตัวเลขเท่านั้น"
                                ControlToValidate="txtTaxID" CssClass="failureNotification" ValidationExpression="^\d+$"
                                ValidationGroup="RegisterUserValidationGroup" SetFocusOnError="True" Display="Dynamic">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" valign="top">
                            <asp:Label ID="Label3" runat="server" Text="ที่อยู่บริษัท :"></asp:Label>
                        </td>
                        <td class="style3" valign="top">
                            <asp:TextBox ID="txtCompanyAdd" runat="server" Height="49px" TextMode="MultiLine"
                                Width="70%" CssClass="TextAreaRemark"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CompanyAddRequire" runat="server" ControlToValidate="txtCompanyAdd"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุที่อยู่บริษัท" ToolTip="กรุณาระบุที่อยู่บริษัท"
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label4" runat="server" Text="ชื่อผุ้ติดต่อ :"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtContactPerson" runat="server" Width="70%" CssClass="TextAreaRemark"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ContactRequire" runat="server" ControlToValidate="txtContactPerson"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุชื่อผุ้ติดต่อ" ToolTip="กรุณาระบุชื่อผุ้ติดต่อ"
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label14" runat="server" Text="เบอร์โทรศัพท์มือถือ :"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtMobilePhone" runat="server" Width="30%" CssClass="TextAreaRemark"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="MobileRequire" runat="server" ControlToValidate="txtMobilePhone"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุเบอร์โทรศัพท์มือถือ" ToolTip="กรุณาระบุเบอร์โทรศัพท์มือถือ"
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="กรุณาระบุเบอร์โทรศัพท์มือถือ เป็นตัวเลขเท่านั้น"
                                ControlToValidate="txtMobilePhone" CssClass="failureNotification" ValidationExpression="^\d+$"
                                ValidationGroup="RegisterUserValidationGroup" SetFocusOnError="True" Display="Dynamic">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label5" runat="server" Text="เบอร์โทรศัพท์สำนักงาน :"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtPhoneNo" runat="server" Width="30%" CssClass="TextAreaRemark"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PhoneNoRequire" runat="server" ControlToValidate="txtPhoneNo"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุเบอร์โทรศัพท์สำนักงาน"
                                ToolTip="กรุณาระบุเบอร์โทรศัพท์สำนักงาน" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="กรุณาระบุเบอร์โทรศัพท์สำนักงาน เป็นตัวเลขเท่านั้น"
                                ControlToValidate="txtPhoneNo" CssClass="failureNotification" ValidationExpression="^\d+$"
                                ValidationGroup="RegisterUserValidationGroup" SetFocusOnError="True" Display="Dynamic">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label6" runat="server" Text="อีเมล์ :"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label15" runat="server" Text="อีเมล์เพื่อแจ้งทราบ CC :"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtEmailCC" runat="server" Width="30%" CssClass="TextAreaRemark"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="validEmailCC" runat="server" ControlToValidate="txtEmailCC"
                                ErrorMessage="กรุณาระบุอีเมล์ CC ที่ถูกต้อง" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ForeColor="Red" CssClass="failureNotification" ValidationGroup="RegisterUserValidationGroup"
                                Display="Dynamic">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="EmailCCRequire" runat="server" ControlToValidate="txtEmailCC"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุอีเมล์เพื่อแจ้งทราบ" ToolTip="กรุณาระบุอีเมล์เพื่อแจ้งทราบ"
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label17" runat="server" Text="เว็บไซต์ของบริษัท :"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtCompanyWebsite" runat="server" Width="70%" CssClass="TextAreaRemark"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </p>
            <p>
                <asp:LinkButton ID="lbtnPopupCtrl" runat="server"></asp:LinkButton>
                <asp:LinkButton ID="lbtnTargerCtrl" runat="server"></asp:LinkButton>
                <act:ModalPopupExtender ID="lbtnPopup_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="lbtnPopupCtrl" DynamicServicePath="" Enabled="True" PopupControlID="pnPopup"
                    TargetControlID="lbtnTargerCtrl">
                </act:ModalPopupExtender>
                <!--  -->
                <asp:Panel ID="pnPopup" runat="server" CssClass="PanelPopup" Width="30%" Height="25%"
                    Style="display: none">
                    <table style="width: 100%;" align="center">
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="center">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2" align="center">&nbsp;
                                    <asp:Label ID="lblMsgResult" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">&nbsp;
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <p>
        <table class="tbStyle">
            <tr>
                <td class="style4">&nbsp;
                </td>
                <td class="style3">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:Label ID="Label11" runat="server" Text="เอกสารแนบ"></asp:Label>
                </td>
                <td class="style3">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:Label ID="Label12" runat="server" Text="1. ภพ.20" CssClass="TextAreaRemark"></asp:Label>
                </td>
                <td class="style3">
                    <asp:FileUpload ID="fuVat20" runat="server" Width="50%" CssClass="TextAreaRemark" />
                </td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:Label ID="Label13" runat="server" Text="2. หนังสือรับรองบริษัท"></asp:Label>
                </td>
                <td class="style3">
                    <asp:FileUpload ID="fuCompanyCert" runat="server" Width="50%" CssClass="TextAreaRemark" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top" class="style4">
                    <asp:Label ID="Label16" runat="server" Text="3. บัตรประจำตัวประชาชน"></asp:Label><br />
                    <asp:Label ID="Label19" runat="server"
                        Text="(สำหรับผู้เข้าร่วมประมูลในนามบุคคลธรรมดา)"></asp:Label>
                </td>
                <td style="vertical-align: top">
                    <asp:FileUpload ID="fuIDCard" runat="server" Width="50%" CssClass="TextAreaRemark" />
                </td>
            </tr>
            <tr>
                <td class="style4">&nbsp;
                </td>
                <td class="style3">
                    <asp:Button ID="btnRegister" runat="server" Text="บันทึก" Width="100px" OnClick="btnRegister_Click"
                        ValidationGroup="RegisterUserValidationGroup" />
                    &nbsp;<asp:Button ID="btnCancel" runat="server" 
                        Text="ปิด" Width="100px" OnClientClick="window.close(); return false;" />
                </td>
            </tr>

        </table>
    </p>
    <asp:HiddenField ID="hdfUserName" runat="server" />
    <asp:HiddenField ID="hdfUsersNo" runat="server" />
    <asp:HiddenField ID="hdfRoleNo" runat="server" />
    <asp:HiddenField ID="hdfCompanyNo" runat="server" />
</asp:Content>
