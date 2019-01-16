<%@ Page Title="Bidding Project" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="BiddingProject.aspx.cs" Inherits="EAuctionProj.BiddingProject" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/jquery-1.8.3.js" type="text/jscript"></script>
    <script src="../Scripts/jquery-ui-1.10.0.js" type="text/jscript"></script>
    <link rel="stylesheet" href="../Scripts/jquery-ui-1.10.0.css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
        }
        .style3
        {
            text-align: left;
        }
        .style5
        {
            width: 359px;
            height: 25px;
        }
        .style9
        {
            text-align: left;
            height: 25px;
        }
        
        .style12
        {
            text-align: left;
        }
        .style15
        {
            text-align: left;
        }
        .style16
        {
            text-align: left;
        }
        .style17
        {
            width: 178px;
            text-align: left;
        }

          .lblWording{
            font-size: 1em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            font-weight:bold;
        }

        .lblDetail {
            font-size: 1em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;         
        }
        .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;           
        }

         .lblGridDetail {
            font-size: 0.8em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;   
            text-align: center;        
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <%-- <h2>
        Bidding project
    </h2>--%>
    <asp:Label ID="Label15" runat="server" Text="ประกาศงานจัดซื้อ/จัดจ้าง : รายละเอียด" CssClass="lblHeader"></asp:Label>
    <p>
        <table width="100%">
            <tr>
                <td class="style2" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="style17">
                    <asp:Label ID="Label1" runat="server" Text="รหัสการประมูล :" CssClass="lblWording"></asp:Label>
                </td>
                <td class="style9">
                    <asp:Label ID="lblBiddingCode" runat="server" CssClass="lblDetail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style17">
                    <asp:Label ID="Label2" runat="server" Text="เรื่อง :" CssClass="lblWording"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="lblProjectName" runat="server" CssClass="lblDetail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style17" style="vertical-align: top" align="left">
                    <asp:Label ID="Label6" runat="server" Text="สถานที่ติดต่อผู้ว่าจ้าง :" CssClass="lblWording"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="lblAddress" runat="server" Width="65%" CssClass="lblDetail"></asp:Label>
                </td>
            </tr>
              <tr>
                <td class="style17" style="vertical-align: top" align="left">
                    <asp:Label ID="Label3" runat="server" Text="แผนกที่ จัดซื้อ/จัดจ้าง :" CssClass="lblWording"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="lblDepartment" runat="server" Width="65%" CssClass="lblDetail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="style17">
                    <asp:Label ID="Label11" runat="server" Text="วันที่ประกาศ :" CssClass="lblWording"></asp:Label>
                </td>
                <td valign="top" height="24px">
                    <asp:Label ID="lblStartDate" runat="server" CssClass="lblDetail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td rowspan="1" class="style17">
                    <asp:Label ID="Label7" runat="server" Text="วันที่สิ้นสุดประกาศ :" CssClass="lblWording"></asp:Label>
                </td>
                <td align="left" rowspan="1">
                    <asp:Label ID="lblEndDate" runat="server" CssClass="lblDetail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style17">
                    <asp:Label ID="Label8" runat="server" Text="ชื่อผู้ติดต่อ :" CssClass="lblWording"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="lblContactName" runat="server" CssClass="lblDetail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style17">
                    <asp:Label ID="Label9" runat="server" Text="อีเมล์ :" CssClass="lblWording"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="lblEmail" runat="server" CssClass="lblDetail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style17">
                    <asp:Label ID="Label10" runat="server" Text="เบอร์โทรศัพท์ :" CssClass="lblWording"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="lblPhoneNo" runat="server" CssClass="lblDetail"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style17">
                    <asp:Label ID="Label12" runat="server" Text="ขอบเขตการประกวดราคา :" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td class="style3">
                    <asp:UpdatePanel runat="server" ID="updTable">
                        <ContentTemplate>
                            <asp:LinkButton ID="lbtnViewPDF" runat="server" OnClick="lbtnViewPDF_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style15" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="style15" colspan="2">
                    <asp:Label ID="Label14" runat="server" Text="*หมายเหตุ กรุณาศึกษาข้อกำหนดการประกวดราคา(TOR) ก่อนเข้าร่วมประมูล"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style15" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style12" colspan="2">
                    <asp:Label ID="lblPreview" runat="server" Text="รายละเอียดสินค้า/บริการ เพื่อประกวดราคา :"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style12" colspan="2">
                    <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="false" PageSize="20"
                        AllowPaging="true" EnableViewState="true" Width="100%" ForeColor="#3333333" ShowHeaderWhenEmpty="true"
                        ShowHeader="true" AllowSorting="false" GridLines="Vertical" CssClass="GridView2"
                        OnRowCommand="gvItem_RowCommand" OnRowDataBound="gvItem_RowDataBound">
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
                <td class="style17">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style16" colspan="2">
                    <asp:Button ID="btnAccept" runat="server" OnClick="btnAccept_Click" Text="สนใจเข้าร่วมประมูล"
                        Width="150px" />
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="ไม่สนใจ"
                        Width="150px" />
                    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="ย้อนกลับ" Width="150px"
                        Visible="False" />
                </td>
            </tr>
        </table>
    </p>
    <asp:HiddenField ID="hdfProjectNo" runat="server" />
    <asp:HiddenField ID="hdfCompanyNo" runat="server" />
    <asp:HiddenField ID="hdfUserName" runat="server" />
    <asp:HiddenField ID="hdfUserNo" runat="server" />
    <asp:HiddenField ID="hdfRoleNo" runat="server" />
</asp:Content>
