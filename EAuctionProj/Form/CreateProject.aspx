<%@ Page Title="Create Bidding Project" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="CreateProject.aspx.cs" Inherits="EAuctionProj.CreateProject" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/jquery-1.8.3.js" type="text/jscript"></script>
    <script src="../Scripts/jquery-ui-1.10.0.js" type="text/jscript"></script>
    <link rel="stylesheet" href="../Scripts/jquery-ui-1.10.0.css" />
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
        }

        .style3 {
            width: 359px;
            text-align: left;
            vertical-align: top;
        }

        .style4 {
            width: 116px;
            height: 25px;
            vertical-align: top;
        }

        .style5 {
            width: 359px;
            height: 25px;
            vertical-align: top;
        }

        .style7 {
            text-align: left;
            vertical-align: top;
        }

        .ui-datepicker-trigger {
            padding: 0px;
            padding-left: 0px;
            vertical-align: baseline;
            position: relative;
            top: 6px;
            height: 22px;
        }

        .style8 {
        }

        .style13 {
            width: 79px;
        }

        .TextAreaRemark {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .lblGridDetail {
            font-size: 0.8em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .lblGridLink {
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            color: white;
        }

        .TxtGridDetail {
            font-size: 0.9em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            width: 90%;
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
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <p>
        <asp:Label ID="Label14" runat="server" Text="สร้างรายการ จัดซื้อ/จัดจ้าง" CssClass="lblHeader"></asp:Label>
    </p>
    <p>
        <table class="style1">
            <tr>
                <td class="style2" colspan="2">
                    <asp:ValidationSummary ID="ProjValidationSummary" runat="server" CssClass="failureNotification"
                        ValidationGroup="ProjValidationGroup" />
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="Label2" runat="server" Text="เรื่อง :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtProjectName" runat="server" Width="60%" CssClass="TextAreaRemark"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CompanyRequired" runat="server" ControlToValidate="txtProjectName"
                        CssClass="failureNotification" ErrorMessage="กรุณาระบุเรื่อง" ToolTip="กรุณาระบุเรื่อง"
                        ValidationGroup="ProjValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="Label6" runat="server" Text="สถานที่ติดต่อเพื่อขอรับทราบข้อมูลเพิ่มเติม :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtContactAdd" runat="server" Width="60%" Height="100px" Rows="6" TextMode="MultiLine" CssClass="TextAreaRemark"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContactAdd"
                        CssClass="failureNotification" ErrorMessage="กรุณาระบุสถานที่ติดต่อเพื่อขอรับทราบข้อมูลเพิ่มเติม"
                        ToolTip="กรุณาระบุสถานที่ติดต่อเพื่อขอรับทราบข้อมูลเพิ่มเติม" ValidationGroup="ProjValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="Label16" runat="server" Text="แผนกที่ จัดซิ้อ/จัดจ้าง :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="250px"
                        DataTextField="DepartmentName" DataValueField="DepartmentName" CssClass="TextAreaRemark">
                    </asp:DropDownList>
                       <asp:CustomValidator runat="server" ID="ValidateDll" ValidationGroup="ProjValidationGroup"
                        CssClass="failureNotification" OnServerValidate="ValidateDll_ServerValidate" >*</asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="Label11" runat="server" Text="วันที่ประกาศ :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtStartDate" runat="server" SkinID="ReadOnly" TabIndex="5" ReadOnly="false" CssClass="TextAreaRemark"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtAvailableDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfAvailableDate" runat="server" ControlToValidate="txtStartDate"
                        ToolTip="กรุณาระบุวันที่ประกาศ" ErrorMessage="กรุณาระบุวันที่ประกาศ" CssClass="failureNotification"
                        ValidationGroup="ProjValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="Label7" runat="server" Text="วันที่สิ้นสุดประกาศ :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtEndDate" runat="server" SkinID="ReadOnly" TabIndex="5" ReadOnly="false" CssClass="TextAreaRemark"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtEndDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                        ToolTip="กรุณาระบุวันที่สิ้นสุดประกาศ" ErrorMessage="กรุณาระบุวันที่สิ้นสุดประกาศ"
                        CssClass="failureNotification" ValidationGroup="ProjValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="Label8" runat="server" Text="ชื่อผู้ติดต่อ :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtContactPers" runat="server" CssClass="TextAreaRemark"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactPers"
                        CssClass="failureNotification" ErrorMessage="กรุณาระบุชื่อผู้ติดต่อ" ToolTip="กรุณาระบุชื่อผู้ติดต่อ"
                        ValidationGroup="ProjValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:Label ID="Label9" runat="server" Text="อีเมล์ :"></asp:Label>
                </td>
                <td class="style5">
                    <asp:TextBox ID="txtContactEmail" runat="server" CssClass="TextAreaRemark"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtContactEmail"
                        CssClass="failureNotification" ErrorMessage="กรุณาระบุอีเมล์" ToolTip="กรุณาระบุอีเมล์"
                        ValidationGroup="ProjValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="Label10" runat="server" Text="เบอร์โทรศัพท์ :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtContactPhone" runat="server" CssClass="TextAreaRemark"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContactPhone"
                        CssClass="failureNotification" ErrorMessage="กรุณาระบุเบอร์โทรศัพท์" ToolTip="กรุณาระบุเบอร์โทรศัพท์"
                        ValidationGroup="ProjValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style7">
                    <asp:Label ID="Label12" runat="server" Text="ไฟล์แนบ :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:FileUpload ID="fuTOR" runat="server" Width="60%" CssClass="TextAreaRemark" />
                    <asp:CustomValidator runat="server" ID="ValidateProj" ValidationGroup="ProjValidationGroup"
                        CssClass="failureNotification" OnServerValidate="ValidateProj_ServerValidate">*</asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="style7" colspan="2">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style7" colspan="2">
                    <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>

                            <table class="style1" id="tbListItem">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label15" runat="server" Text="เลือกรายการ :"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddlItemTemplate" runat="server" AutoPostBack="True" DataTextField="TemplateName"
                                            DataValueField="TemplateNo" OnSelectedIndexChanged="ddlItemTemplate_SelectedIndexChanged"
                                            Width="30%" CssClass="TextAreaRemark">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style8" colspan="2">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="รายละเอียดสินค้า/บริการ เพื่อประกวดราคา :"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnListItem" runat="server" Visible="false">
                                            <asp:GridView ID="gvListItem" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CellPadding="4" EnableViewState="true" ForeColor="#333333" GridLines="Vertical"
                                                OnRowCancelingEdit="gvListItem_RowCancelingEdit" OnRowCommand="gvListItem_RowCommand"
                                                OnRowCreated="gvListItem_RowCreated" OnRowDataBound="gvListItem_RowDataBound"
                                                OnRowDeleting="gvListItem_RowDeleting" OnRowEditing="gvListItem_RowEditing" OnRowUpdating="gvListItem_RowUpdating"
                                                PageSize="10" ShowFooter="true" Width="100%">
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                                                <EditRowStyle BackColor="#8EAACA" />
                                                <FooterStyle BackColor="#8EAACA" Font-Bold="false" Font-Size="0.8em" ForeColor="White" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                                </Columns>
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style7" colspan="2">&nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="บันทึก" Width="100px"
                        ValidationGroup="ProjValidationGroup" />
                    &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="ยกเลิก"
                        Width="100px" />
                </td>
            </tr>
        </table>
    </p>

    <!-- Modal popup "Popup Message" -->
    <asp:LinkButton ID="lbtnPopupCtrl" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="lbtnTargerCtrl" runat="server"></asp:LinkButton>
    <cc1:ModalPopupExtender ID="lbtnPopup_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="lbtnPopupCtrl" DynamicServicePath="" Enabled="True" PopupControlID="pnPopup" TargetControlID="lbtnTargerCtrl">
    </cc1:ModalPopupExtender>
    <!--  Style="display: none" -->
    <asp:Panel ID="pnPopup" runat="server" CssClass="PanelPopup" Width="35%" Height="30%">
        <table style="width: 100%;" align="center">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="style2" align="left"></td>
                        </tr>
                        <tr>
                            <td align="center">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblMsgResult" runat="server" CssClass="lblQuestionDesc"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">&nbsp;
                                        <asp:Button ID="btnOK" runat="server" CssClass="PopupButton" OnClick="btnOK_Click" Text="OK" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>

    <!-- --------------------------- -->
    <asp:HiddenField ID="hdfUserName" runat="server" />
</asp:Content>
