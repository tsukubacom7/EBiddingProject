<%@ Page Title="Process Bidding" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="VendorBiddingPriceDetail.aspx.cs" Inherits="EAuctionProj.VendorBiddingPriceDetail" %>

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

        .style26 {
            width: 152px;
        }

        .style29 {
            width: 123px;
            text-align: left;
        }

        .style32 {
            width: 194px;
        }

        .style33 {
            width: 961px;
        }

        .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .TextAreaRemark {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .lblGridDetail {
            font-size: 0.9em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            text-align: center;
        }
         .lblGridDetailLeft {
            font-size: 0.9em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            text-align: left;
        }
         .lblGridAmount {
            font-size: 0.9em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            text-align: right;
        }

        .lblGridDetailAttach {
            font-size: 0.9em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            text-align: left;
        }
        .auto-style1 {
            width: 1019px;
        }
              
        div.relative {
            position: relative;
            left: 190px;
            width: 60%;
            text-align:right;
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
    <%-- <h2>
        vendor Bidding project
    </h2>--%>

    <asp:Label ID="Label15" runat="server" Text="ประวัติการเสนอราคา จัดซื้อ/จัดจ้าง : รายละเอียด" CssClass="lblHeader"></asp:Label>
    <p>
        <table width="100%">
            <tr>
                <td class="style29">
                    <asp:Label ID="Label1" runat="server" Text="รหัสการประมูล :" Font-Bold="False"></asp:Label>
                </td>
                <td class="style9">
                    <asp:Label ID="lblBiddingCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style29">
                    <asp:Label ID="Label2" runat="server" Text="เรื่อง :" Font-Bold="False"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style29">
                    <asp:Label ID="Label11" runat="server" Text="วันที่ประกาศ :" Font-Bold="False"></asp:Label>
                </td>
                <td height="24px">
                    <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style29">
                    <asp:Label ID="Label7" runat="server" Text="วันที่สิ้นสุดประกาศ :" Font-Bold="False"></asp:Label>
                </td>
                <td height="24px">
                    <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style15" colspan="2">
                    <asp:Label ID="Label19" runat="server" Font-Bold="False" Text="ไฟล์แนบ :"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style15" colspan="2">
                    <asp:GridView ID="gvAttachFile" runat="server" AllowPaging="true" AllowSorting="false"
                        AutoGenerateColumns="false" CssClass="GridView2" EnableViewState="true" ForeColor="#3333333"
                        GridLines="Vertical" ShowHeader="true" ShowHeaderWhenEmpty="true" Width="65%"
                        OnRowDataBound="gvAttachFile_RowDataBound" EmptyDataText="ไม่มีข้อมูล">
                        <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black" />
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                            PageButtonCount="4" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="Description" HeaderText="รายละเอียด" ItemStyle-Width="60%"
                                ItemStyle-CssClass="lblGridDetailAttach"></asp:BoundField>
                            <asp:TemplateField HeaderText="ชื่อไฟล์">
                                <ItemStyle CssClass="lblGridDetailAttach" />
                                <ItemTemplate>
                                    <%-- <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("AttachFilePath") %>'
                                        CommandName="Download" Text='<%# Eval("FileName") %>' OnClick="lnkDownload_Click" />--%>
                                    <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("AttachFilePath") %>'
                                        CommandName="Download" Text='<%# Eval("FileName") %>' />
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
            <tr>
                <td class="style12" colspan="2">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style12" colspan="2">
                    <asp:Label ID="lblPreview" runat="server" Font-Bold="True" Text="รายละเอียดสินค้า/บริการ เพื่อประกวดราคา :"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style12" colspan="2">
                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click"
                        Visible="False" CssClass="btnExportExcel" ToolTip="Export to Excel" Width="140px" />
                </td>
            </tr>
            <tr>
                <td class="style12" colspan="2">
                    <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="false" PageSize="20"
                        AllowPaging="true" EnableViewState="true" Width="100%" ForeColor="#3333333" ShowHeaderWhenEmpty="true"
                        ShowHeader="true" AllowSorting="false" GridLines="Vertical" CssClass="GridView2"
                        OnRowDataBound="gvItem_RowDataBound">
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
                            <td align="right" width="80%">
                                <asp:Label ID="Label16" runat="server" Font-Bold="True" Text="จำนวนเงิน (ไม่รวมภาษีมูลค่าเพิ่ม) :"></asp:Label>
                            </td>
                            <td align="right" width="20%">
                                <asp:Label ID="lblSummaryPrice" runat="server"></asp:Label>                               
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
                    <%-- <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="ย้อนกลับ" Width="100px" />--%>
                    <asp:Button ID="btnBack" runat="server" OnClientClick="window.close(); return false;"
                        Text="ปิด" Width="100px" />
                </td>
            </tr>
            <asp:HiddenField ID="hdfProjectNo" runat="server" />
            <asp:HiddenField ID="hdfBiddingNo" runat="server" />
            <asp:HiddenField ID="hdfCompanyNo" runat="server" />
        </table>
    </p>
</asp:Content>
