<%@ Page Title="Bidding History" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="VendorBiddingHistory.aspx.cs" Inherits="EAuctionProj.VendorBiddingHistory" %>

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
            width: 274px;
            text-align: right;
        }

          .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }
        .TextAreaRemark {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;           
        }

         .td1{
             width:13%;
         }
         .td2{
             width:14%;
              text-align: left;
         }
         .td3{
             width:28%;
         }
         .td4{
           
             width:30%;
             text-align: left;
         }
        .td5 {
            width: 15%;
        }

    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%-- <h2>
        Vendor history
    </h2>--%>
    <asp:Label ID="Label1" runat="server" Text="ประวัติการเสนอราคา จัดซื้อ/จัดจ้างของบริษัท หรือบุคคลที่เข้าร่วมประมูล" CssClass="lblHeader"></asp:Label>
    <p>
        <table class="style1">          
            <tr>
                <td style="text-align: center">
                    <table class="style1">
                        <tr>
                            <td class="td1">
                                <asp:Label ID="Label2" runat="server" Text="ประเภทการค้นหา:"></asp:Label>
                            </td>
                            <td class="td2">
                                <asp:DropDownList ID="ddlSearch" runat="server" Width="100%" CssClass="TextAreaRemark">
                                    <asp:ListItem Value="0">== เลือกข้อมูล ==</asp:ListItem>
                                    <asp:ListItem Value="1">ชื่อบริษัท</asp:ListItem>
                                    <asp:ListItem Value="2">เลขประจำตัวผู้เสียภาษี</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="td3">
                                <asp:TextBox ID="txtSearch" runat="server" Width="100%" CssClass="TextAreaRemark"></asp:TextBox>
                            </td>
                            <td class="td4">
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="100px" OnClick="btnSearch_Click" />
                                &nbsp;<asp:Button ID="btnClear" runat="server" Text="ยกเลิก" Width="100px" OnClick="btnClear_Click" />
                            </td>
                            <td class="td5"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="5">
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click"
                                    Visible="False" CssClass="btnExportExcel" Width="140px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvCompanyBidding" runat="server" CellPadding="4" GridLines="Vertical"
                        AutoGenerateColumns="False" ForeColor="#333333" AllowPaging="True" PageSize="20"
                        Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="ไม่พบข้อมูล" AllowSorting="true"
                        OnPageIndexChanging="gvCompanyBidding_PageIndexChanging" OnSorting="gvCompanyBidding_Sorting">
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="18px"/>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                            PageButtonCount="4" />
                        <Columns>
                            <asp:BoundField DataField="CompanyName" HeaderText="ชื่อบริษัท" ItemStyle-Width="50%"
                                ItemStyle-HorizontalAlign="Left" SortExpression="CompanyName" ItemStyle-Font-Size="16px" />
                            <asp:BoundField DataField="TaxID" HeaderText="เลขประจำตัวผุ้เสียภาษี" ItemStyle-Width="20%"
                                ItemStyle-HorizontalAlign="Center" SortExpression="TaxID" ItemStyle-Font-Size="16px" />
                            <asp:TemplateField HeaderText="รายละเอียด">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplSelectCompNo" runat="server" Text="Click"
                                        NavigateUrl='<%#String.Format("VendorBidingDetail.aspx?CompanyNo={0}",Eval("CompanyNo"))%>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Font-Size="16px" HorizontalAlign="Center" Width="10%" />
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
</asp:Content>
