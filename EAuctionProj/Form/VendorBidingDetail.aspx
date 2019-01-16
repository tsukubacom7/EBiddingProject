<%@ Page Title="Bidding History" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="VendorBidingDetail.aspx.cs" Inherits="EAuctionProj.VendorBidingDetail" %>

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

        .td1 {
            width: 13%;
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
  <%--  <h2>
        vendor bidding history
    </h2>--%>
    <asp:Label ID="Label1" runat="server" Text="ประวัติการเสนอราคา จัดซื้อ/จัดจ้าง : " CssClass="lblHeader"></asp:Label>
    <asp:Label ID="lblCompany" runat="server" CssClass="lblHeader"></asp:Label>
    <p>
        <table width="100%">         
            <tr>
                <td style="text-align: center">
                    <table width="100%">
                        <tr>
                            <td class="td1">
                                <asp:Label ID="Label2" runat="server" Text="ประเภทการค้นหา:"></asp:Label>
                            </td>
                            <td class="td2" style="text-align: right">
                                <asp:DropDownList ID="ddlSearch" runat="server" Width="100%" CssClass="TextAreaRemark">
                                    <asp:ListItem Value="0">== เลือกข้อมูล ==</asp:ListItem>
                                    <asp:ListItem Value="1">รหัสการประมูล</asp:ListItem>
                                    <asp:ListItem Value="2">รายการ</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="td3">
                                <asp:TextBox ID="txtSearch" runat="server" Width="100%" CssClass="TextAreaRemark"></asp:TextBox>
                            </td>
                            <td class="td4">
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="100px" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="ยกเลิก" Width="100px" OnClick="btnClear_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click"
                                    Visible="False" CssClass="btnExportExcel" width="140px"/>
                            </td>
                            <td class="style4" style="text-align: right">
                                &nbsp;
                            </td>
                            <td class="style3" style="text-align: left">
                                &nbsp;
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
              <!--  --> 
                <td>
                    <asp:GridView ID="gvListProject" runat="server" CellPadding="4" GridLines="Vertical"
                        AutoGenerateColumns="False" ForeColor="#333333" PageSize="20" AllowSorting="true"  AllowPaging="true"
                        OnRowCommand="gvListProject_RowCommand" Width="100%" OnPageIndexChanging="gvListProject_PageIndexChanging"
                        ShowHeaderWhenEmpty="true" EmptyDataText="ไม่พบข้อมูล" OnSorting="gvListProject_Sorting">
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
                                ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="16px" />
                            <asp:BoundField DataField="ProjectName" SortExpression="ProjectName" HeaderText="รายการ"
                                ItemStyle-Width="45%" ItemStyle-Font-Size="16px" />
                            <asp:BoundField DataField="CreatedDate" SortExpression="CreatedDate" HeaderText="วันที่สนอราคา"
                                ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd\/MM\/yyyy}"
                                HtmlEncode="false" ItemStyle-Font-Size="16px" />
                            <asp:BoundField DataField="BiddingPrice" SortExpression="BiddingPrice" HeaderText="ราคาที่เสนอ"
                                ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n}" ItemStyle-Font-Size="16px" />
                            <asp:TemplateField HeaderText="รายละเอียด">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplSelectProj" runat="server" Text="Click"
                                        NavigateUrl='<%#String.Format("VendorBiddingPriceDetail.aspx?ProjectNo={0}&BiddingNo={1}", Eval("ProjectNo"), Eval("BiddingsNo"))%>' Target="_blank">
                                    </asp:HyperLink>
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
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="ย้อนกลับ" Width="100px" />
                </td>
            </tr>
        </table>
    </p>
    <p>
        <asp:HiddenField ID="hdfCompanyNo" runat="server" />
    </p>
</asp:Content>
