<%@ Page Title="User Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="UserRegister.aspx.cs" Inherits="EAuctionProj.UserRegister" %>

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
        }

        .style5 {
            width: 237px;
            height: 88px;
        }

        .style6 {
            width: 508px;
            height: 88px;
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

        div.button {
            position: absolute;
            top: 125px;
            left: 52%;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Label ID="Label7" runat="server" Text="ลงทะเบียนบริษัท" CssClass="lblHeader"></asp:Label>
            <p>
                <table class="tbStyle">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label10" runat="server" Text="กรุณากรอกข้อมูลเพื่อลงทะเบียนเข้าร่วมการประมูล จัดซื้อ/จัดจ้าง"
                                Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                                ValidationGroup="RegisterUserValidationGroup" />
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <asp:Label ID="Label18" runat="server" Text="ประเภทผู้เข้าร่วมประมูล :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlUserType" runat="server" Width="30%" CssClass="TextAreaRemark">
                                <asp:ListItem Value="0">== กรุณาเลือก ==</asp:ListItem>
                                <asp:ListItem Value="1">นิติบุคคล</asp:ListItem>
                                <asp:ListItem Value="2">บุคคลธรรมดา</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label19" runat="server" Text="รายการที่เข้าร่วมประมูล :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBDProject" runat="server" Width="50%" DataTextField="ProjectName"
                                DataValueField="ProjectNo" CssClass="TextAreaRemark">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="เลขประจำตัวผู้เสียภาษี : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaxID" runat="server" Width="30%" CssClass="TextAreaRemark"></asp:TextBox>
                            &nbsp;<asp:ImageButton ID="ibtnSearch" runat="server" Height="22px" ImageUrl="~/Images/search_icon.png"
                                OnClick="ibtnSearch_Click" ToolTip="ค้นหาข้อมูลบริษัท" CssClass="ImageBtn" />
                            &nbsp;
                            <asp:RequiredFieldValidator ID="TaxIDRequire" runat="server" ControlToValidate="txtTaxID"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุเลขประจำตัวผู้เสียภาษี"
                                ToolTip="กรุณาระบุเลขประจำตัวผู้เสียภาษี" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="valTaxNumeric" runat="server" ErrorMessage="กรุณาระบุเลขประจำตัวผู้เสียภาษี เป็นตัวเลขเท่านั้น"
                                ControlToValidate="txtTaxID" CssClass="failureNotification" ValidationExpression="^\d+$"
                                ValidationGroup="RegisterUserValidationGroup" SetFocusOnError="True" Display="Dynamic">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label20" runat="server" Text="(กรณีที่เคยลงทะเบียนแล้ว สามารถค้นหาข้อมูลบริษัทจากเลขประจำตัวผู้เสียภาษีได้)"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="ชื่อบริษัท/ชื่อ-นามสกุลผู้เข้าร่วมประมูล :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyName" runat="server" Width="50%" CssClass="TextAreaRemark"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CompanyRequired" runat="server" ControlToValidate="txtCompanyName"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุชื่อบริษัท/ชื่อ-นามสกุลผู้เข้าร่วมประมูล"
                                ToolTip="กรุณาระบุชื่อบริษัท/ชื่อ-นามสกุลผู้เข้าร่วมประมูล" ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="ValidateTxt" runat="server" CssClass="failureNotification" OnServerValidate="ValidateTxt_ServerValidate" ValidationGroup="RegisterUserValidationGroup">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="Label3" runat="server" Text="ที่อยู่บริษัท :"></asp:Label>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtCompanyAdd" runat="server" CssClass="TextAreaRemark" Height="70px" TextMode="MultiLine" Width="50%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CompanyAddRequire" runat="server" ControlToValidate="txtCompanyAdd"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุที่อยู่บริษัท" ToolTip="กรุณาระบุที่อยู่บริษัท"
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="ชื่อผู้ติดต่อ :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactPerson" runat="server" Width="50%" CssClass="TextAreaRemark"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ContactRequire" runat="server" ControlToValidate="txtContactPerson"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุชื่อผู้ติดต่อ" ToolTip="กรุณาระบุชื่อผู้ติดต่อ"
                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="เบอร์โทรศัพท์มือถือ :"></asp:Label>
                        </td>
                        <td>
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
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="เบอร์โทรศัพท์สำนักงาน :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhoneNo" runat="server" Width="30%" CssClass="TextAreaRemark"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="กรุณาระบุเบอร์โทรศัพท์สำนักงาน เป็นตัวเลขเท่านั้น"
                                ControlToValidate="txtPhoneNo" CssClass="failureNotification" ValidationExpression="^\d+$"
                                ValidationGroup="RegisterUserValidationGroup" SetFocusOnError="True" Display="Dynamic">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="อีเมล์ :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" Width="30%" CssClass="TextAreaRemark"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="validEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="กรุณาระบุอีเมล์ที่ถูกต้อง" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ForeColor="Red" CssClass="failureNotification" ValidationGroup="RegisterUserValidationGroup"
                                Display="Dynamic">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="EmailRequire" runat="server" ControlToValidate="txtEmail"
                                CssClass="failureNotification" ErrorMessage="กรุณาระบุอีเมล์" ToolTip="กรุณาระบุอีเมล์"
                                ValidationGroup="RegisterUserValidationGroup" Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="อีเมล์เพื่อแจ้งทราบ CC :"></asp:Label>
                        </td>
                        <td>
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
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="เว็บไซต์ของบริษัท :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyWebsite" runat="server" Width="50%" CssClass="TextAreaRemark"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="เอกสารแนบ"></asp:Label>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="1. ภพ.20"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fuVat20" runat="server" Width="50%" CssClass="TextAreaRemark" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="2. หนังสือรับรองบริษัท"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fuCompanyCert" runat="server" Width="50%" CssClass="TextAreaRemark" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="Label16" runat="server" Text="3. บัตรประจำตัวประชาชน</br>(สำหรับผู้เข้าร่วมประมูลในนามบุคคลธรรมดา)</br>  พร้อมลงนามรับรองกำกับ"></asp:Label>
                        </td>
                        <td style="vertical-align: top">
                            <asp:FileUpload ID="fuIDCard" runat="server" Width="50%" CssClass="TextAreaRemark" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnRegister" runat="server" Text="ขั้นตอนต่อไป" OnClick="btnRegister_Click"
                                ValidationGroup="RegisterUserValidationGroup" Width="100px" />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td class="style3">&nbsp;
                        </td>
                    </tr>
                </table>
            </p>
            <!-- Modal popup "Popup Message" <p></p> -->
            <asp:LinkButton ID="lbtnPopupCtrl" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lbtnTargerCtrl" runat="server"></asp:LinkButton>
            <act:ModalPopupExtender ID="lbtnPopup_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="lbtnPopupCtrl" DynamicServicePath="" Enabled="True" PopupControlID="pnPopup"
                TargetControlID="lbtnTargerCtrl">
            </act:ModalPopupExtender>
            <!--   -->
            <asp:Panel ID="pnPopup" runat="server" CssClass="PanelPopup" Width="40%" Height="30%"
                Style="display: none">
                <table style="width: 100%;" align="center">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style2" align="left">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMsgResult1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMsgResult2" runat="server"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">&nbsp;
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
                            <asp:HiddenField ID="hdfCompanyNo" runat="server" />
                            <asp:HiddenField ID="hdfProjectNo" runat="server" />
                            <asp:HiddenField ID="hdfRetCompanyNo" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnRegister" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
