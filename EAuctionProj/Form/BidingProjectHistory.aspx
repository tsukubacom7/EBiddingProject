<%@ Page Title="Bidding History" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="BidingProjectHistory.aspx.cs" Inherits="EAuctionProj.BidingProjectHistory" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
            width: 211px;
        }
        .style4
        {
            width: 171px;
        }
        .style5
        {
            width: 12%;
            text-align: right;
        }

        .style6 {
            width: 193px;
        }

           .lblWording{
            font-size: 1em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .lblGridDetail {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            text-align: center;
        }
        .TextAreaRemark {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .tdExport{
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%-- <h2>
        Bidding History
    </h2>--%>
    <p>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="ประวัติการเสนอราคา จัดซื้อ/จัดจ้าง" CssClass="lblHeader"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <table class="style1">
                        <tr>
                            <td class="style5">
                                <asp:Label ID="Label2" runat="server" Text="ประเภทการค้นหา:"></asp:Label>
                            </td>
                            <td class="style5">
                                <asp:DropDownList ID="ddlSearch" runat="server" Width="100%" CssClass="TextAreaRemark">
                                    <asp:ListItem Value="0">== เลือกข้อมูล ==</asp:ListItem>
                                    <asp:ListItem Value="1">รหัสการประมูล</asp:ListItem>
                                    <asp:ListItem Value="2">รายการ</asp:ListItem>
                                    <asp:ListItem Value="3">ชื่อบริษัท</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; width:300px">
                                <asp:TextBox ID="txtSearch" runat="server" Width="300px" CssClass="TextAreaRemark"></asp:TextBox>
                            </td>
                            <td class="style6" style="text-align: left">
                                <asp:DropDownList ID="ddlSelMonth" runat="server" Width="150px" CssClass="TextAreaRemark">
                                    <asp:ListItem Value="0">== เดือนที่ประกาศ ==</asp:ListItem>
                                    <asp:ListItem Value="1">มกราคม</asp:ListItem>
                                    <asp:ListItem Value="2">กุมภาพันธ์</asp:ListItem>
                                    <asp:ListItem Value="3">มีนาคม</asp:ListItem>
                                    <asp:ListItem Value="4">เมษายน</asp:ListItem>
                                    <asp:ListItem Value="5">พฤษภาคม</asp:ListItem>
                                    <asp:ListItem Value="6">มิถุนายน</asp:ListItem>
                                    <asp:ListItem Value="7">กรกฎาคม</asp:ListItem>
                                    <asp:ListItem Value="8">สิงหาคม</asp:ListItem>
                                    <asp:ListItem Value="9">กันยายน</asp:ListItem>
                                    <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                                    <asp:ListItem Value="11">พฤศจิกายน</asp:ListItem>
                                    <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left">
                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="100px" OnClick="btnSearch_Click" />
                                &nbsp;<asp:Button ID="btnClear" runat="server" Text="ยกเลิก" Width="100px" OnClick="btnClear_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" class="tdExport">
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel"
                                    onclick="btnExport_Click" Visible="False" CssClass="btnExportExcel" Width="140px"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvListProject" runat="server" CellPadding="4" GridLines="Vertical"
                        AutoGenerateColumns="False" ForeColor="#333333" AllowPaging="True" PageSize="20"
                        OnRowCommand="gvListProject_RowCommand" Width="100%" OnPageIndexChanging="gvListProject_PageIndexChanging"
                        ShowHeaderWhenEmpty="true" EmptyDataText="ไม่พบข้อมูล" AllowSorting="true" OnSorting="gvListProject_Sorting" OnRowDataBound="gvListProject_RowDataBound">
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="18px" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                            PageButtonCount="4" />
                        <Columns>
                            <asp:BoundField DataField="BiddingCode" SortExpression="BiddingCode" HeaderText="รหัสการประมูล"
                                ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"  ItemStyle-Font-Size="16px"/>
                            <asp:BoundField DataField="ProjectName" SortExpression="ProjectName" HeaderText="รายการ"
                                ItemStyle-Width="37%" ItemStyle-Font-Size="16px"/>
                            <asp:BoundField DataField="CompanyName" HeaderText="ชื่อบริษัท" ItemStyle-Width="21%"
                                ItemStyle-HorizontalAlign="Left" SortExpression="CompanyName" ItemStyle-Font-Size="16px"/>
                            <asp:BoundField DataField="EndDate" HeaderText="วันที่เสนอราคา" SortExpression="EndDate"
                                ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd\/MM\/yyyy}"
                                HtmlEncode="false" ItemStyle-Font-Size="16px"/>
                            <asp:BoundField DataField="BiddingPrice" HeaderText="ราคาที่เสนอ" SortExpression="BiddingPrice"
                                ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" ItemStyle-Font-Size="16px"/>
                            <asp:TemplateField HeaderText="รายละเอียด" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdfProjectNo" runat="server" Value='<%# Eval("ProjectNo") %>' />
                                    <asp:HiddenField ID="hdfBiddingsNo" runat="server" Value='<%# Eval("BiddingsNo") %>' />
                                    <%-- <asp:HyperLink ID="hplSelectProj" runat="server" Text="Click"
                                        NavigateUrl='<%#String.Format("CompanyBiddingDetail.aspx?ProjectNo={0}&BiddingNo={1}", Eval("ProjectNo"), Eval("BiddingsNo"))%>'></asp:HyperLink>--%>
                                    <asp:HyperLink ID="hplSelectProj" runat="server" Text="Click" Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Font-Size="16px" />
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
    </p>
    <p>
    </p>
    <asp:HiddenField ID="hdfCompanyNo" runat="server" />
    <asp:HiddenField ID="hdfUserName" runat="server" />
    <asp:HiddenField ID="hdfUserNo" runat="server" />
    <asp:HiddenField ID="hdfRoleNo" runat="server" />
</asp:Content>
