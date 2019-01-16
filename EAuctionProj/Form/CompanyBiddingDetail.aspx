<%@ Page Title="Process Bidding" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CompanyBiddingDetail.aspx.cs" Inherits="EAuctionProj.CompanyBiddingDetail" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/jquery-1.8.3.js" type="text/jscript"></script>
    <script src="../Scripts/jquery-ui-1.10.0.js" type="text/jscript"></script>
    <link rel="stylesheet" href="../Scripts/jquery-ui-1.10.0.css" />
    <style type="text/css">        
        .style1 {
            width: 100%;
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

        .style29 {
            width: 158px;
            text-align: left;
        }

        .style34 {
        }

        .style35 {
            width: 607px;
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
            font-size: 0.9em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            text-align: center;
        }
        .auto-style1 {
            width: 85%;
        }
        .auto-style2 {
            width: 15%;
        }

         .lblGridDetailAttach {
            font-size: 0.9em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            text-align: left;
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
    <%--<h2>
        Bidding Project Detail
    </h2>--%>
       <asp:Label ID="Label15" runat="server" Text="ประวัติการเสนอราคา จัดซื้อ/จัดจ้าง : รายละเอียด" CssClass="lblHeader"></asp:Label>
    <p>
        <table class="style1">
            <tr>
                <td class="style34" colspan="2">                 
                </td>
            </tr>
            <tr>
                <td class="style29">
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Text="รหัสการประมูล :"></asp:Label>
                </td>
                <td class="style9">
                    <asp:Label ID="lblBiddingCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style29">
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Text="เรื่อง :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style29" valign="top">
                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Text="ชื่อบริษัทผู้เข้าร่วมประมูล : "></asp:Label>
                </td>
                <td valign="top" height="24px">
                    <asp:Label ID="lblCompany" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style29" valign="top">
                    <asp:Label ID="Label3" runat="server" Text="คุณสมบัติผู้เข้าร่วมประมูล:"></asp:Label>
                </td>
                <td valign="top" height="24px">
                    <a href="#" runat="server" id="linkViewQuestionaire" target="_blank">รายละเอียด</a>
                </td>
            </tr>
            <tr>
                <td class="style15" colspan="2">
                    <asp:Label ID="Label19" runat="server" Font-Bold="False" Text="เอกสารแนบ :"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style15" colspan="2">
                    <asp:GridView ID="gvAttachFile" runat="server" AllowPaging="true" AllowSorting="false"
                        AutoGenerateColumns="false" CssClass="GridView2" EnableViewState="true" ForeColor="#3333333"
                        GridLines="Vertical" OnRowDataBound="gvAttachFile_RowDataBound" ShowHeader="true"
                        ShowHeaderWhenEmpty="true" Width="65%" EmptyDataText="ไม่มีข้อมูล">
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
                            <asp:BoundField DataField="Description" HeaderText="รายละเอียด" ItemStyle-Width="60%" ItemStyle-CssClass="lblGridDetailAttach"/>
                            <asp:TemplateField HeaderText="เอกสารแนบ" ItemStyle-Width="40%">
                                <ItemStyle CssClass="lblGridDetailAttach" />
                                <ItemTemplate>
                                    <%-- <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("AttachFilePath") %>'
                                                CommandName="Download" OnClick="lnkDownload_Click" Text='<%# Eval("FileName") %>' />--%>
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
                        Visible="False" CssClass="btnExportExcel" Width="140px"/>
                </td>
            </tr>
            <tr>
                <td class="style12" colspan="2">
                    <asp:GridView ID="gvItem" runat="server" AllowPaging="true" AllowSorting="false"
                        AutoGenerateColumns="false" CssClass="GridView2" EnableViewState="true" ForeColor="#3333333"
                        GridLines="Vertical" OnRowDataBound="gvItem_RowDataBound" PageSize="20" ShowHeader="true"
                        ShowHeaderWhenEmpty="true" Width="100%">
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
                <td class="style3">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style29">&nbsp;
                </td>
                <td class="style3">
                    <table class="style1">
                        <tr>
                            <td align="right" class="auto-style1">
                                <asp:Label ID="Label16" runat="server" Font-Bold="True" Text="จำนวนเงิน (ไม่รวมภาษีมูลค่าเพิ่ม) :"></asp:Label>
                            </td>
                            <td align="right" class="auto-style2">
                                <asp:Label ID="lblSummaryPrice" runat="server"></asp:Label>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">&nbsp;
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="left">
                    <%--   <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="ย้อนกลับ" Width="100px" />--%>
                    <asp:Button ID="btnBack" runat="server" OnClientClick="window.close(); return false;"
                        Text="ปิด" Width="100px" />
                </td>
            </tr>
            <asp:HiddenField ID="hdfProjectItemNo" runat="server" />
            <asp:HiddenField ID="hdfProjectNo" runat="server" />
            <asp:HiddenField ID="hdfBiddingCode" runat="server" />
            <asp:HiddenField ID="hdfBiddingNo" runat="server" />
            <asp:HiddenField ID="hdfCompanyNo" runat="server" />
        </table>
    </p>
</asp:Content>
