<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="HomePage.aspx.cs" Inherits="EAuctionProj.HomePage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .tbStyle {
            width: 100%;
        }

        .style3 {
            width: 211px;
        }

        .style4 {
            width: 171px;
        }

        .style5 {
            width: 10%;
            text-align: right;
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
            font-size: 1em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .lblWording {
            font-size: 1em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .auto-style1 {
            width: 193px;
        }

        .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .tdStyleExport {
            text-align: left;
            width: 100%;
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
            width: 15%;
        }

        .td5 {
            width: 30%;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Label ID="Label1" runat="server" Text="ประกาศงาน จัดซื้อ/จัดจ้าง" CssClass="lblHeader"></asp:Label>
            <p>
                <table class="tbStyle">
                    <tr>
                        <td style="text-align: center">
                            <table class="tbStyle">
                                <tr>
                                    <td class="td1">
                                        <asp:Label ID="Label2" runat="server" Text="ประเภทการค้นหา :" CssClass="lblWording"></asp:Label>
                                    </td>
                                    <td class="td2">
                                        <asp:DropDownList ID="ddlSearch" runat="server" Width="100%" CssClass="TextAreaRemark">
                                            <asp:ListItem Value="0">== เลือกข้อมูล ==</asp:ListItem>
                                            <asp:ListItem Value="1">รหัสการประมูล</asp:ListItem>
                                            <asp:ListItem Value="2">รายการ</asp:ListItem>
                                            <asp:ListItem Value="3">แผนก</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td3">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="100%" CssClass="TextAreaRemark"></asp:TextBox>
                                    </td>
                                    <td class="td4">
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
                                    <td class="td5">&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="ค้นหา" Width="100px" />
                                        &nbsp;<asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="ยกเลิก" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" class="tdStyleExport">
                                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btnExportExcel"
                                            OnClick="btnExport_Click" Visible="False" Width="140px" />
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
                                ShowHeaderWhenEmpty="true" EmptyDataText="ไม่พบข้อมูล" OnRowDataBound="gvListProject_RowDataBound">
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="18px" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    PageButtonCount="4" />
                                <Columns>
                                    <asp:BoundField DataField="BiddingCode" HeaderText="รหัสการประมูล" ItemStyle-Width="9%"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="16px" />
                                    <asp:BoundField DataField="ProjectName" HeaderText="รายการ จัดซื้อ/จัดจ้าง" ItemStyle-Width="36%"
                                        ItemStyle-Font-Size="16px" />
                                    <asp:BoundField DataField="DepartmentName" HeaderText="แผนก" ItemStyle-Width="17%"
                                        ItemStyle-Font-Size="16px" />
                                    <asp:BoundField DataField="StartDate" HeaderText="วันที่เริ่ม" ItemStyle-Width="10%"
                                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd\/MM\/yyyy}" HtmlEncode="false" ItemStyle-Font-Size="16px" />
                                    <asp:BoundField DataField="EndDate" HeaderText="วันสิ้นสุด" ItemStyle-Width="10%"
                                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd\/MM\/yyyy}" HtmlEncode="false" ItemStyle-Font-Size="16px" />
                                    <asp:TemplateField HeaderText="สถานะ" ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# CheckProjectStatus(Eval("EndDate"),Eval("StartDate"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="รายละเอียด" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdfProjectNo" runat="server" Value='<%# Eval("ProjectNo")%>' />
                                            <%--   <asp:HyperLink ID="hplSelectProj" runat="server" Text="Click"
                                                NavigateUrl='<%# "~/Form/BiddingProject.aspx?ProjectNo=" + HttpUtility.HtmlEncode(Eval("ProjectNo")) %>'></asp:HyperLink>--%>
                                            <asp:HyperLink ID="hplClick" runat="server">Click</asp:HyperLink>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
