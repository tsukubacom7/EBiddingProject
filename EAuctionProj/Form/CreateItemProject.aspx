<%@ Page Title="Create Template Project" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="CreateItemProject.aspx.cs" Inherits="EAuctionProj.CreateItemProject" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/jquery-1.8.3.js" type="text/jscript"></script>
    <script src="../Scripts/jquery-ui-1.10.0.js" type="text/jscript"></script>
    <link rel="stylesheet" href="../Scripts/jquery-ui-1.10.0.css" />
    <style type="text/css">
        .ui-datepicker-trigger {
            padding: 0px;
            padding-left: 0px;
            vertical-align: baseline;
            position: relative;
            top: 6px;
            height: 22px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("[id$=txtStartDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy",
                inline: true,
                buttonImage: '../Images/CalendarEdit.png'
            });
        });

        $(function () {
            $("[id$=txtEndDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy",
                inline: true,
                buttonImage: '../Images/CalendarEdit.png'
            });
        });

    </script>
    <style type="text/css">
        .GridView1, .GridView1 th, .GridView1 td {
            border: 1px solid white;
        }

            .GridView1 a {
                font-weight: normal;
                color: black;
            }

        .GridView2, .GridView2 th, .GridView2 td {
            border: 1px solid white;
        }

            .GridView2 a {
                /*font-weight: normal;*/
                color: black;
            }

        .TextAreaRemark {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <p></p>
    <asp:Label ID="Label14" runat="server" Text="รายการคอลัมน์ข้อมูล จัดซื้อ/จัดจ้าง (Template) : สร้างคอลัมน์รายการ" CssClass="lblHeader"></asp:Label>
    <asp:Label ID="Label15" runat="server" Text="ชื่อหัวข้อ :" Visible="False" CssClass="lblHeader"></asp:Label>
    <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>
                        <%--  <asp:CustomValidator runat="server" ID="ValidateDDL" ValidationGroup="valColumnName"
                            ErrorMessage="*กรุณาระบุชื่อรายการ ลำดับและชื่อหัวข้อ" OnServerValidate="ValidateDDL_ServerValidate"
                            ForeColor="Red"></asp:CustomValidator>
                        <asp:CustomValidator runat="server" ID="ValidateTxt" ValidationGroup="valColumnName"
                            ErrorMessage="*กรุณาระบุชื่อรายการ" ForeColor="Red" OnServerValidate="ValidateTxt_ServerValidate"></asp:CustomValidator>--%>
                      <%--  <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                            ValidationGroup="valColumnName" Style="padding-left: 16px" /> --%>
                           <asp:CustomValidator runat="server" ID="ValidateDll" ValidationGroup="valColumnName"
                            ErrorMessage="" ForeColor="Red" OnServerValidate="ValidateDll_ServerValidate"  CssClass="failureNotification"></asp:CustomValidator>                    
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label18" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="ชื่อรายการ :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProjectName" runat="server" Width="70%" MaxLength="255" CssClass="TextAreaRemark"></asp:TextBox>
                     <%--   <asp:RequiredFieldValidator ID="ProjectNameRequired" runat="server" ControlToValidate="txtProjectName"
                            CssClass="failureNotification" ErrorMessage="กรุณาระบุชื่อรายการ" ToolTip="กรุณาระบุชื่อรายการ"
                            ValidationGroup="valColumnName">*</asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label17" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                        <asp:Label ID="Label16" runat="server" Text="ชื่อหัวข้อ :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlColunnNo" runat="server" DataTextField="ColumnName" DataValueField="ColumnRunNo"
                            Width="20%" CssClass="TextAreaRemark">
                            <asp:ListItem Value="0">== ระบุลำดับ ==</asp:ListItem>
                            <asp:ListItem Value="1">Colomn 1</asp:ListItem>
                            <asp:ListItem Value="2">Colomn 2</asp:ListItem>
                            <asp:ListItem Value="3">Colomn 3</asp:ListItem>
                            <asp:ListItem Value="4">Colomn 4</asp:ListItem>
                            <asp:ListItem Value="5">Colomn 5</asp:ListItem>
                            <asp:ListItem Value="6">Colomn 6</asp:ListItem>
                            <asp:ListItem Value="7">Colomn 7</asp:ListItem>
                            <asp:ListItem Value="8">Colomn 8</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlHeaderName" runat="server" DataTextField="ColumnName" DataValueField="ColumnRunNo"
                            Width="20%" CssClass="TextAreaRemark">
                        </asp:DropDownList>                        
                        &nbsp;<asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="เพิ่ม"
                            Width="100px" ValidationGroup="valColumnName" />
                        &nbsp;<asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="ดูตัวอย่าง"
                            Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td class="style8">&nbsp;
                    </td>
                    <td class="style13">
                        <asp:GridView ID="gvAddColumn" runat="server" AutoGenerateColumns="false" PageSize="10"
                            AllowPaging="true" EnableViewState="true" Width="50%" ForeColor="#333333" ShowHeaderWhenEmpty="true"
                            ShowHeader="true" AllowSorting="false" GridLines="Vertical" CssClass="GridView2"
                            OnRowDataBound="gvAddColumn_RowDataBound" OnRowCommand="gvAddColumn_RowCommand"
                            OnRowDeleted="gvAddColumn_RowDeleted" OnRowDeleting="gvAddColumn_RowDeleting">
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="ColumnNo" HeaderText="ลำดับที่" ItemStyle-Width="20%"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ColumnName" HeaderText="ชื่อหัวข้อ" ItemStyle-Width="20%"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lblDelete" CommandName="Delete" Text="ลบ" />
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
                    <td colspan="2" align="left">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <asp:Label ID="lblPreview" runat="server" Text="ตัวอย่างตาราง รายละเอียดสินค้า/บริการ เพื่อประกวดราคา :"
                            Visible="False" CssClass="lblHeader"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style8" colspan="2">
                        <asp:GridView ID="gvTemplate" runat="server" AutoGenerateColumns="false" PageSize="10"
                            AllowPaging="true" EnableViewState="true" Width="100%" ForeColor="#3333333" ShowHeaderWhenEmpty="true"
                            ShowHeader="true" AllowSorting="false" GridLines="Vertical" CssClass="GridView2"
                            OnRowDataBound="gvTemplate_RowDataBound" OnRowCommand="gvTemplate_RowCommand">
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
                    <td class="style8">&nbsp;
                    </td>
                    <td class="style13">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style8">&nbsp;
                    </td>
                    <td class="style13">
                        <asp:Panel ID="pnSubmitBtn" runat="server">
                            <table class="style1">
                                <tr>
                                    <td align="left">
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="บันทึก" Width="100px"
                                            ValidationGroup="valTxtName" />
                                        &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="ยกเลิก"
                                            Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style7" colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
