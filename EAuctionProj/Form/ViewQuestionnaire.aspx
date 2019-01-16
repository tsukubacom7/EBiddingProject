<%@ Page Title="Bidding Project" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ViewQuestionnaire.aspx.cs" Inherits="EAuctionProj.ViewQuestionnaire" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/jquery-1.8.3.js" type="text/jscript"></script>
    <script src="../Scripts/jquery-ui-1.10.0.js" type="text/jscript"></script>
    <link rel="stylesheet" href="../Scripts/jquery-ui-1.10.0.css" />
    <style type="text/css">
        .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .tbStyle {
            width: 100%;
            position: relative;
            left: 5%;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function CloseWindow() {
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%--  <h2>
        Questionaire
    </h2>--%>
    <p>
        <asp:Label ID="Label15" runat="server" Text="คุณสมบัติผู้เข้าร่วมประมูล:" CssClass="lblHeader"></asp:Label>
        &nbsp;<asp:Label ID="lblCompany" runat="server" CssClass="lblHeader"></asp:Label>
    </p>
    <p>
        <table class="tbStyle">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label16" runat="server" Text="1.ผู้ประกวดราคาเป็นนิติบุคคลที่จดทะเบียนตามกฎหมายไทย"
                        Width="80%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="3%">&nbsp;
                    <asp:Label ID="lblAnswer1" runat="server" Text="ตอบ" Font-Bold="True"></asp:Label>
                </td>
                <td class="style21">&nbsp;<asp:Label ID="lblQ1" runat="server" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label17" runat="server" Text="2.ผู้ประกวดราคาเป็นนิติบุคคลมีประสบการณ์ทั้งสิ้นกี่ปี (โปรดระบุวันที่จดทะเบียนตามหนังสือรับรองบริษัท)"
                        Width="80%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" width="3%">&nbsp;
                     <asp:Label ID="Label1" runat="server" Width="100%" Text="ตอบ" Font-Bold="true"></asp:Label>
                </td>
                <td class="style21">&nbsp;<asp:Label ID="lblQ2" runat="server" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="3.ผู้ประกวดราคามีประสบการณ์การให้บริการในโรงงานอุตสาหกรรม เช่น โรงไฟฟ้า,&nbsp; อุตสาหกรรมปิโตรเคมี, โรงงานอุตสาหกรรมขนาดใหญ่ เป็นต้น"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">&nbsp;
                     <asp:Label ID="Label2" runat="server" Width="100%" Text="ตอบ" Font-Bold="true"></asp:Label>
                </td>
                <td class="style21">&nbsp;<asp:Label ID="lblQ3" runat="server" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2">
                    <asp:Label ID="Label11" runat="server" Text="4.จากข้อ 2 ผู้ประกวดราคามีประสบการณ์ ไม่น้อยกว่า 4 แห่ง โดยมีอายุสัญญาจ้างไม่น้อยกว่า 1 (หนึ่ง) ปี และมีหนังสือรับรองผลงาน"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" class="style3">&nbsp;
                     <asp:Label ID="Label4" runat="server" Width="100%" Text="ตอบ" Font-Bold="true"></asp:Label>
                </td>
                <td valign="top" class="style21">&nbsp;<asp:Label ID="lblQ4" runat="server" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label7" runat="server" Text="5.มีระบบบริหารงานที่น่าเชื่อถือ เช่น ได้การรับรองมาตรฐาน ISO หรือมาตรฐานอื่นที่ยอมรับได้"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">&nbsp;
                     <asp:Label ID="Label5" runat="server" Width="100%" Text="ตอบ" Font-Bold="true"></asp:Label>
                </td>
                <td class="style21">&nbsp;<asp:Label ID="lblQ5" runat="server" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label8" runat="server" Text="6.ผู้ประกวดราคามีสาขา หรือศูนย์บริการตั้งอยู่ในเขตจังหวัดที่โรงไฟฟ้า (ผู้ว่าจ้าง) ตั้งอยู่ หรือจังหวัดใกล้เคียงแต่มี ระยะห่างจากสาขาหรือศูนย์บริการ กับที่ตั้งโรงไฟฟ้า (ผู้ว่าจ้าง) ไม่เกิน 50 กิโลเมตร"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">&nbsp;
                     <asp:Label ID="Label6" runat="server" Text="ตอบ" Font-Bold="True"></asp:Label>
                </td>
                <td class="style21">&nbsp;<asp:Label ID="lblQ6" runat="server" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label9" runat="server" Text="7.ผู้ประกวดราคาสามารถวางหนังสือค้ำประกันที่ออกโดยธนาคารไว้เพื่อเป็นหลักประกันการปฏิบัติหน้าที่ตามสัญญานี้ให้แก่ผู้ว่าจ้างในวงเงินจำนวน 2 (สอง) เท่าของอัตราค่าบริการรายเดือน"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">&nbsp;
                     <asp:Label ID="Label12" runat="server" Text="ตอบ" Font-Bold="True"></asp:Label>
                </td>
                <td class="style21">&nbsp;<asp:Label ID="lblQ7" runat="server" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label10" runat="server" Text="8.คุณสมบัติอื่นๆ (ถ้ามี) โปรดระบุ"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">&nbsp;
                     <asp:Label ID="Label13" runat="server" Width="100%" Text="ตอบ" Font-Bold="true"></asp:Label>
                </td>
                <td class="style21">&nbsp;<asp:Label ID="lblQ8" runat="server" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
    </p>
    <p>
        <asp:Button ID="btnBack" runat="server" OnClientClick="window.close(); return false;"
            Text="ปิด" Width="100px" />
    </p>
    <asp:HiddenField ID="hdfProjectNo" runat="server" />
    <asp:HiddenField ID="hdfCompanyNo" runat="server" />
</asp:Content>
