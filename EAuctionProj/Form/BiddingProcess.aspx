<%@ Page Title="Process Bidding" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="BiddingProcess.aspx.cs" Inherits="EAuctionProj.BiddingProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
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
            text-align: left;
        }

        .style5 {
            width: 359px;
            height: 25px;
        }

        .style9 {
            text-align: left;
            height: 25px;
        }

        .style12 {
            text-align: left;
        }

        .style15 {
            text-align: left;
        }

        .style19 {
            text-align: left;
        }

        .style27 {
            width: 375px;
        }

        .style29 {
            width: 123px;
            text-align: left;
        }

        .style30 {
            width: 112px;
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

        .lblGridDetail {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            text-align: center;
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
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <%-- <h2>
        Bidding project
    </h2>--%>
    <p>
        <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table class="style1">
                    <tr>
                        <td class="style2" colspan="2">
                            <asp:Label ID="Label15" runat="server" Text="รายละเอียดงานจัดซื้อ/จัดจ้าง" CssClass="lblHeader"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style29">
                            <asp:Label ID="Label1" runat="server" Text="รหัสการประมูล :" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="style9">
                            <asp:Label ID="lblBiddingCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style29">
                            <asp:Label ID="Label2" runat="server" Text="เรื่อง :" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style29">
                            <asp:Label ID="Label11" runat="server" Text="วันที่ประกาศ :" Font-Bold="True"></asp:Label>
                        </td>
                        <td valign="top" height="24px">
                            <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="1" class="style29">
                            <asp:Label ID="Label7" runat="server" Text="วันที่สิ้นสุดประกาศ :" Font-Bold="True"></asp:Label>
                        </td>
                        <td align="left" rowspan="1">
                            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style29">
                            <asp:Label ID="Label8" runat="server" Text="ชื่อผู้ติดต่อ :" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblContactName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style29">
                            <asp:Label ID="Label9" runat="server" Text="อีเมล์ :" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style29">
                            <asp:Label ID="Label10" runat="server" Text="เบอร์โทรศัพท์ :" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblPhoneNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style15" colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style12" colspan="2">
                            <asp:Label ID="lblPreview" runat="server" Text="รายละเอียดสินค้า/บริการ เพื่อประกวดราคา :"
                                Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style12" colspan="2">
                            <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="false" PageSize="20"
                                AllowPaging="true" EnableViewState="true" Width="100%" ForeColor="#3333333" ShowHeaderWhenEmpty="true"
                                ShowHeader="true" AllowSorting="false" GridLines="Vertical" CssClass="GridView2"
                                OnRowCommand="gvItem_RowCommand" OnRowDataBound="gvItem_RowDataBound" DataKeyNames="ProjectItemNo">
                                <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black"></AlternatingRowStyle>
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    PageButtonCount="4" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
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
                        </td>
                    </tr>
                    <tr>
                        <td class="style29">&nbsp;
                        </td>
                        <td class="style3">
                            <table class="style1">
                                <tr>
                                    <td align="right" class="style27">
                                        <asp:Label ID="Label16" runat="server" Text="จำนวนเงิน(ไม่รวมภาษีมูลค่าเพิ่ม) :"
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td align="right">&nbsp;
                                        <asp:Label ID="lblSummaryPrice" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="style19" colspan="2" style="vertical-align: top">

                            <table class="style1">
                                <tr>
                                    <td class="style30">
                                        <asp:Label ID="Label19" runat="server" Font-Bold="True" Text="เอกสารแนบ :"></asp:Label>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style30">
                                        <asp:Label ID="Label20" runat="server" Font-Bold="True" Text="รายละเอียด :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFileDesc" runat="server" Style="margin-left: 0px" Width="30%" CssClass="TextAreaRemark"></asp:TextBox>
                                        <asp:CustomValidator runat="server" ID="ValidateTxt" ValidationGroup="valTxt" ErrorMessage="*กรุณาระบุรายละเอียด"
                                            ForeColor="Red" OnServerValidate="ValidateTxt_ServerValidate"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style30">
                                        <asp:Label ID="Label21" runat="server" Font-Bold="True" Text="เลือกไฟล์ :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fuAttachFile" runat="server" CssClass="TextAreaRemark" />
                                        &nbsp;<asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="เพิ่ม"
                                            Width="60px" ValidationGroup="valTxt" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style19" colspan="2">
                                        <asp:GridView ID="gvAttachFile" runat="server" AllowPaging="true" AllowSorting="false"
                                            AutoGenerateColumns="false" CssClass="GridView2" EnableViewState="true" ForeColor="#3333333"
                                            GridLines="Vertical" OnRowCommand="gvAttachFile_RowCommand" ShowHeader="true"
                                            ShowHeaderWhenEmpty="true" Width="70%" OnRowDeleting="gvAttachFile_RowDeleting"
                                            OnRowDataBound="gvAttachFile_RowDataBound">
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
                                                <asp:TemplateField HeaderText="เอกสารแนบ" ItemStyle-Width="30%">
                                                    <ItemStyle Font-Size="16px" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("AttachFilePath") %>'
                                                            CommandName="Download" Text='<%# Eval("FileName") %>' OnClick="lnkDownload_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Description" HeaderText="รายละเอียด" ItemStyle-Width="50%"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="16px">
                                                    <ItemStyle Font-Size="16px" HorizontalAlign="Left" Width="50%" />
                                                </asp:BoundField>
                                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Font-Size="16px" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lblDelete" CommandArgument='<%# Eval("AttachFilePath") %>'
                                                            CommandName="Delete" Text="ลบ" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">&nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnAccept" runat="server" OnClick="btnAccept_Click" Text="เสนอราคา"
                                Width="100px" />
                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="ยกเลิก"
                                Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left"></td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
            </Triggers>
        </asp:UpdatePanel>
    </p>
    <p>
        <!-- Modal popup "Popup Message" -->
        <asp:LinkButton ID="lbtnPopupCtrl" runat="server"></asp:LinkButton>
        <asp:LinkButton ID="lbtnTargerCtrl" runat="server"></asp:LinkButton>
        <act:ModalPopupExtender ID="lbtnPopup_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
            CancelControlID="lbtnPopupCtrl" DynamicServicePath="" Enabled="True" PopupControlID="pnPopup"
            TargetControlID="lbtnTargerCtrl">
        </act:ModalPopupExtender>
        <!-- -->
        <asp:Panel ID="pnPopup" runat="server" CssClass="PanelPopup" Width="35%" Height="40%"
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
                                    <asp:Label ID="lblMsgResult1" runat="server" Text="ระบบได้รับการเสนอราคาเรียบร้อยแล้ว"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">&nbsp;
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
    <asp:HiddenField ID="hdfCompanyNo" runat="server" />
    <asp:HiddenField ID="hdfUserName" runat="server" />
    <asp:HiddenField ID="hdfUserNo" runat="server" />
    <asp:HiddenField ID="hdfRoleNo" runat="server" />
    <asp:HiddenField ID="hdfProjectItemNo" runat="server" />
    <asp:HiddenField ID="hdfProjectNo" runat="server" />
    <asp:HiddenField ID="hdfBiddingCode" runat="server" />
</asp:Content>
