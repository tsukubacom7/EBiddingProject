<%@ Page Title="Bidding History" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ProjectHistoryDetail.aspx.cs" Inherits="EAuctionProj.ProjectHistoryDetail" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
            width: 211px;
            text-align: left;
        }
        .style7
        {
            width: 105px;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Bidding Project History
    </h2>
    <p>
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="รายละเอียดประวัติการเสนอราคา"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <table class="style1">
                        <tr>
                            <td class="style7">
                                <asp:Label ID="Label16" runat="server" Text="รหัสการประมูล :" Font-Bold="False"></asp:Label>
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblBiddingCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                <asp:Label ID="Label2" runat="server" Text="เรื่อง :" Font-Bold="False"></asp:Label>
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                <asp:Label ID="Label15" runat="server" Text="วันที่ประกาศ :" Font-Bold="False"></asp:Label>
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                <asp:Label ID="Label7" runat="server" Text="วันที่สิ้นสุดประกาศ :" 
                                    Font-Bold="False"></asp:Label>
                            </td>
                            <td class="style3">
                                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                &nbsp;
                            </td>
                            <td class="style3">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvListProject" runat="server" CellPadding="4" GridLines="Vertical"
                        AutoGenerateColumns="False" ForeColor="#333333" AllowPaging="True" PageSize="20"
                        OnRowCommand="gvListProject_RowCommand" Width="100%" ShowHeaderWhenEmpty="true"
                        EmptyDataText="ไม่พบข้อมูล" AllowSorting="true" OnSorting="gvListProject_Sorting">
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                            PageButtonCount="4" />
                        <Columns>
                            <asp:BoundField DataField="CompanyName" HeaderText="ชื่อบริษัท" ItemStyle-Width="35%"
                                ItemStyle-HorizontalAlign="Left" SortExpression="CompanyName" />
                            <asp:BoundField DataField="TaxID" HeaderText="เลขประจำตัวผุ้เสียภาษี" ItemStyle-Width="15%"
                                ItemStyle-HorizontalAlign="Center" SortExpression="TaxID" />
                            <asp:BoundField DataField="CreatedDate" HeaderText="วันที่เสนอราคา" ItemStyle-Width="15%"
                                ItemStyle-HorizontalAlign="Center" SortExpression="CreatedDate" />
                            <asp:BoundField DataField="BiddingPrice" SortExpression="BiddingPrice" HeaderText="ราคาที่เสนอ"
                                ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n}" />
                            <asp:TemplateField HeaderText="รายละเอียด" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplSelectProj" runat="server" Text="Click" NavigateUrl='<%#String.Format("CompanyBiddingDetail.aspx?ProjectNo={0}&BiddingNo={1}", Eval("ProjectNo"),Eval("BiddingsNo"))%>'></asp:HyperLink>
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
                <td>
                    <asp:HiddenField ID="hdfProjectNo" runat="server" />
                </td>
            </tr>
             <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="ย้อนกลับ" Width="100px" />
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
