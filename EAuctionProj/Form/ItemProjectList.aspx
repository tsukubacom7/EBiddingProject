<%@ Page Title="Bidding History" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ItemProjectList.aspx.cs" Inherits="EAuctionProj.ItemProjectList" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style3 {
            width: 211px;
            text-align: left;
        }

        .style5 {
            width: 274px;
            text-align: left;
        }

        .style6 {
            width: 14px;
            text-align: left;
        }

        .style7 {
            width: 125px;
            text-align: left;
        }

        .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .lblGridDetail {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p></p>
    <asp:Label ID="Label1" runat="server" Text="รายการคอลัมน์ข้อมูล จัดซื้อ/จัดจ้าง (Template)" CssClass="lblHeader"></asp:Label>
    <table width="100%">
        <tr>
            <td style="text-align: right">
                <asp:Button ID="btnAddTemplate" runat="server" OnClick="btnAddTemplate_Click" Text="เพิ่มรายการ" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvListTemplate" runat="server" CellPadding="4" GridLines="None"
                    AutoGenerateColumns="False" ForeColor="#333333" AllowPaging="True" OnRowCommand="gvListTemplate_RowCommand"
                    Width="100%" OnRowDeleting="gvListTemplate_RowDeleting" DataKeyNames="TemplateNo"
                    OnRowDataBound="gvListTemplate_RowDataBound">
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="TemplateName" HeaderText="ชื่อรายการ" ItemStyle-Width="15%"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="lblGridDetail" />
                        <asp:BoundField DataField="ColumnName1" HeaderText="คอลัมน์ 1" ItemStyle-Width="7%"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="lblGridDetail" />
                        <asp:BoundField DataField="ColumnName2" HeaderText="คอลัมน์ 2" ItemStyle-Width="7%"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="lblGridDetail" />
                        <asp:BoundField DataField="ColumnName3" HeaderText="คอลัมน์ 3" ItemStyle-Width="12%"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="lblGridDetail" />
                        <asp:BoundField DataField="ColumnName4" HeaderText="คอลัมน์ 4" ItemStyle-Width="12%"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="lblGridDetail" />
                        <asp:BoundField DataField="ColumnName5" HeaderText="คอลัมน์ 5" ItemStyle-Width="12%"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="lblGridDetail" />
                        <asp:BoundField DataField="ColumnName6" HeaderText="คอลัมน์ 6" ItemStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="lblGridDetail" />
                        <asp:BoundField DataField="ColumnName7" HeaderText="คอลัมน์ 7" ItemStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="lblGridDetail" />
                        <asp:BoundField DataField="ColumnName8" HeaderText="คอลัมน์ 8" ItemStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="lblGridDetail" />
                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lblDelete" CommandName="Delete" Text="ลบ" CssClass="lblGridDetail" />
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
</asp:Content>
