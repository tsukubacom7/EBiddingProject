<%@ Page Title="Activate User" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CompanyUserDetail.aspx.cs" Inherits="EAuctionProj.CompanyUserDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .tbStyle {
            width: 100%;
            position: relative;
            left: 30px;
        }

        .style3 {
            width: 508px;
        }

        .style4 {
        }

        .style5 {
        }

        .style6 {
        }

        .style7 {
            width: 171px;
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

        .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <%--  <h2>
                Company User Detail&nbsp;</h2>--%>
            <asp:Label ID="Label6" runat="server" Text="รายละเอียด ข้อมูลบริษัท" CssClass="lblHeader"></asp:Label>
            <p></p>
            <table class="tbStyle">
                <tr>
                    <td class="style5" colspan="3"></td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label4" runat="server" Text="UserName :" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label5" runat="server" Text="Password :" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblPassword" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label20" runat="server" Text="แก้ไขข้อมูล :" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style3">
                        <a href="#" runat="server" id="linkChangePass">เปลี่ยนรหัสผ่าน</a>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">&nbsp;
                    </td>
                    <td class="style3">
                        <a href="#" runat="server" id="linkChangeProfile" target="_blank">แก้ไขข้อมูลบริษัท</a>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">&nbsp;
                    </td>
                    <td align="left">
                        <a href="#" runat="server" id="linkQuestionaire" target="_blank">คุณสมบัติผู้เข้าร่วมประมูล</a>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label22" runat="server" Text="รายการที่เลือกประมูล :"></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label1" runat="server" Text="ชื่อบริษัท :"></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label2" runat="server" Text="เลขประจำตัวผู้เสียภาษี :"></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblTaxID" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label12" runat="server" Text="ที่อยู่บริษัท :"></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label13" runat="server" Text="ชื่อผู้ติดต่อ : "></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblContactName" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label14" runat="server" Text="เบอร์โทรศัพท์มือถือ : "></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblMobieNo" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label15" runat="server" Text="เบอร์โทรศัพท์ : "></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblPhoneNo" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label11" runat="server" Text="อีเมล์ :"></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label16" runat="server" Text="อีเมล์เพื่อแจ้งทราบ CC : "></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblEmailCC" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label21" runat="server" Text="เว็บไซต์ของบริษัท : "></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblWebsite" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label3" runat="server" Text="สถานะ : "></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <asp:Label ID="Label19" runat="server" Text="เอกสารแนบ :" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style3">&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style4" colspan="3">
                        <asp:GridView ID="gvAttachFile" runat="server" AllowPaging="true" AllowSorting="false"
                            AutoGenerateColumns="false" CssClass="GridView2" EnableViewState="true" ForeColor="#3333333"
                            GridLines="Vertical" ShowHeader="true" ShowHeaderWhenEmpty="true" Width="70%"
                            OnRowDataBound="gvAttachFile_RowDataBound" EmptyDataText="ไม่มีข้อมูล">
                            <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black" />
                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                PageButtonCount="4" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="18px" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="Description" HeaderText="รายละเอียด">
                                    <ItemStyle HorizontalAlign="Left" Width="40%" Font-Size="16px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="ชื่อไฟล์">
                                    <ItemTemplate>
                                        <%-- <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("AttachFilePath") %>'
                                    CommandName="Download" Text='<%# Eval("FileName") %>' OnClick="lnkDownload_Click" />--%>
                                        <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("AttachFilePath") %>'
                                            CommandName="Download" Text='<%# Eval("FileName") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="40%" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreatedDate" HeaderText="วันที่สร้างไฟล์" ItemStyle-Width="20%"
                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd\/MM\/yyyy}"
                                    HtmlEncode="false" ItemStyle-Font-Size="16px" />
                            </Columns>
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="style7">&nbsp;
                    </td>
                    <td class="style3">&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style7">&nbsp;
                    </td>
                    <td class="style3">&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style6" colspan="3">
                        <asp:Button ID="btnApprove" runat="server" Text="อนุมัติ" Width="100px" OnClick="btnApprove_Click"
                            ToolTip="Approve" Visible="False" />
                        &nbsp;<asp:Button ID="btnVerify" runat="server" Text="ตรวจสอบ" Width="100px" OnClick="btnVerify_Click"
                            ToolTip="Verify" Visible="False" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="ย้อนกลับ" Width="100px" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
            <p>
                <asp:LinkButton ID="lbtnPopupCtrl" runat="server"></asp:LinkButton>
                <asp:LinkButton ID="lbtnTargerCtrl" runat="server"></asp:LinkButton>
                <act:ModalPopupExtender ID="lbtnPopup_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="lbtnPopupCtrl" DynamicServicePath="" Enabled="True" PopupControlID="pnPopup"
                    TargetControlID="lbtnTargerCtrl">
                </act:ModalPopupExtender>
                <!-- -->
                <asp:Panel ID="pnPopup" runat="server" CssClass="PanelPopup" Width="40%" Height="25%"
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
                                            <asp:Label ID="lblMsgResult1" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblMsgResult2" runat="server"></asp:Label>
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
                                <asp:HiddenField ID="hdfUserName" runat="server" />
                                <asp:HiddenField ID="hdfCompanyNo" runat="server" />
                                <asp:HiddenField ID="hdfRoleNo" runat="server" />
                                <asp:HiddenField ID="hdfUserNo" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <p>
                </p>
            </p>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
