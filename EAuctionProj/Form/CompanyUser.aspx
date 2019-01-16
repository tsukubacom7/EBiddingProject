<%@ Page Title="Activate Company" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CompanyUser.aspx.cs" Inherits="EAuctionProj.CompanyUser" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
        }

        .style3 {
            width: 508px;
        }

        .style4 {
        }

        .style6 {
            width: 415px;
        }

        .style8 {
            width: 195px;
            text-align: right;
        }

        .style9 {
            width: 147px;
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

        .lblGridDetail {
            font-size: 0.9em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            text-align: center;
        }

        .lblGridDetailAttach {
            font-size: 0.9em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            text-align: left;
        }

        .td1 {
            width: 13%;
        }

        .td2 {
            width: 14%;
            text-align: left;
        }

        .td3 {
            width: 28%;
        }

        .td4 {
            width: 30%;
            text-align: left;
        }

        .td5 {
            width: 15%;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%-- <h2>
        Company User</h2>--%>
    <asp:Label ID="Label10" runat="server" Text="รายชื่อบริษัทที่ลงทะเบียน" CssClass="lblHeader"></asp:Label>
    <table class="style1">
        <tr>
            <td class="style4" colspan="3">
                <table class="style1">
                    <tr>
                        <td class="td1">
                            <asp:Label ID="Label2" runat="server" Text="ประเภทการค้นหา:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:DropDownList ID="ddlSearch" runat="server" Width="150px" CssClass="TextAreaRemark">
                                <asp:ListItem Value="0">== เลือกข้อมูล ==</asp:ListItem>
                                <asp:ListItem Value="1">ชื่อบริษัท</asp:ListItem>
                                <asp:ListItem Value="2">เลขที่ผู้เสียภาษี</asp:ListItem>
                                <asp:ListItem Value="3">ชื่อผู้ใช้งาน</asp:ListItem>
                                <asp:ListItem Value="4">งานบริการ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td3">
                            <asp:TextBox ID="txtSearch" runat="server" Width="250px" Style="margin-right: 0px" CssClass="TextAreaRemark"></asp:TextBox>
                        </td>
                        <td class="td4">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="100px" Style="margin-left: 0px"
                            OnClick="btnSearch_Click" />
                            &nbsp;<asp:Button ID="btnClear" runat="server" Text="ยกเลิก" Width="100px" OnClick="btnClear_Click" />
                        </td>
                        <td class="td5">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btnExportExcel"
                                OnClick="btnExport_Click" Visible="False" Width="140px"/>
                        </td>
                        <td class="style9">&nbsp;
                        </td>
                        <td class="style6" style="text-align: right">&nbsp;
                        </td>
                        <td style="text-align: left">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style4" colspan="3">
                <asp:GridView ID="gvListCompany" runat="server" CellPadding="4" GridLines="Vertical"
                    AutoGenerateColumns="False" OnSelectedIndexChanged="gvListCompany_SelectedIndexChanged"
                    ForeColor="#333333" OnSelectedIndexChanging="gvListCompany_SelectedIndexChanging"
                    AllowPaging="True" OnRowCommand="gvListCompany_RowCommand"
                    OnPageIndexChanging="gvListCompany_PageIndexChanging"
                    PageSize="20" DataKeyNames="UserName" OnSorting="gvListCompany_Sorting" Width="100%"
                    AllowSorting="true">
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="CompanyName" SortExpression="CompanyName" HeaderText="ชื่อบริษัท"
                            ItemStyle-Width="24%" />
                        <asp:BoundField DataField="ProjectName" SortExpression="ProjectName" HeaderText="งานบริการ"
                            ItemStyle-Width="26%" />
                        <asp:BoundField DataField="TaxID" SortExpression="TaxID" HeaderText="เลขที่ผู้เสียภาษี"
                            ItemStyle-Width="12%" />
                        <asp:BoundField DataField="UserName" SortExpression="UserName" HeaderText="ชื่อผู้ใช้งาน"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Password" HeaderText="รหัสผู้ใช้งาน" ItemStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="สถานะ" ItemStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:CommandField ShowEditButton="true" ItemStyle-Width="8%" HeaderText="รายละเอียด"
                            ItemStyle-HorizontalAlign="Center" EditText="Click" />
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
            <td class="style4">&nbsp;
            </td>
            <td class="style3">&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="style4">&nbsp;
            </td>
            <td class="style3">&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
