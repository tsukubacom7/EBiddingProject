<%@ Page Title="Bidding History" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="BidingCompanyHistory.aspx.cs" Inherits="EAuctionProj.BidingCompanyHistory" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style3 {
            width: 211px;
        }

        .style4 {
            width: 171px;
        }

        .style5 {
            width: 274px;
            text-align: right;
        }

        .tdRight {
            text-align: right;
        }

        .TextAreaRemark {
            font-size: 1em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Project bidding history
    </h2>
    <p>
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="ประวัติการเสนอราคา จัดซื้อ/จัดจ้าง"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <table class="style1">
                        <tr>
                            <td class="style5">
                                <asp:Label ID="Label2" runat="server" Text="ประเภทการค้นหา:"></asp:Label>
                            </td>
                            <td class="style4" style="text-align: right">
                                <asp:DropDownList ID="ddlSearch" runat="server" Width="100%" CssClass="TextAreaRemark">
                                    <asp:ListItem Value="0">== เลือกข้อมูล ==</asp:ListItem>
                                    <asp:ListItem Value="1">เลขที่การประมูล</asp:ListItem>
                                    <asp:ListItem Value="2">รายการ</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style3" style="text-align: left">
                                <asp:TextBox ID="txtSearch" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td class="tdRight">
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
                            <td style="text-align: center">
                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="50px" />
                                <asp:Button ID="btnClear" runat="server" Text="ยกเลิก" Width="50px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvListCompany" runat="server" CellPadding="4" GridLines="None"
                        AutoGenerateColumns="False" OnSelectedIndexChanged="gvListCompany_SelectedIndexChanged"
                        ForeColor="#333333" OnSelectedIndexChanging="gvListCompany_SelectedIndexChanging"
                        AllowPaging="True" OnRowCommand="gvListCompany_RowCommand" OnPageIndexChanging="gvListCompany_PageIndexChanging"
                        PageSize="20" Width="100%">
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="CompanyName" HeaderText="ชื่อบริษัท" ItemStyle-Width="30%" />
                            <asp:BoundField DataField="TaxID" HeaderText="เลขที่ผู้เสียภาษี" ItemStyle-Width="15%" />
                            <asp:TemplateField HeaderText="รายละเอียด" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplSelcompany" runat="server" Text="Click"
                                        NavigateUrl='<%#String.Format("BidingProjectHistory.aspx?ProjectNo={0}", Eval("CompanyNo"))%>'></asp:HyperLink>
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
    </p>
    <p>
    </p>
</asp:Content>
