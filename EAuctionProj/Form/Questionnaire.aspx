<%@ Page Title="Bidding Project" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Questionnaire.aspx.cs" Inherits="EAuctionProj.Questionnaire" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/jquery-1.8.3.js" type="text/jscript"></script>
    <script src="../Scripts/jquery-ui-1.10.0.js" type="text/jscript"></script>
    <link rel="stylesheet" href="../Scripts/jquery-ui-1.10.0.css" />
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style16 {
            text-align: left;
        }

        .style17 {
            text-align: left;
        }

        .style18 {
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

        .tbStyle {
            width: 100%;
            position: relative;
            left: 5%;
        }

        .lblHeader {
            font-size: 1.2em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .TextAreaRemark {
            font-size: 1.0em;
            font-family: "DB Heavent Light","Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            z-index: 99;
            min-height: 100%;
            width: 100%;
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .center {
            z-index: 1000;
            margin: 10% auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=70);
            opacity: 1;
        }

        /*.center img {
                height: 128px;
                width: 128px;
            }*/
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updGrid">
        <ProgressTemplate>
            <div class="modal">
                <div class="center">
                    <img alt="" src="../Images/loading_bar.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Label ID="Label2" runat="server" Text="คุณสมบัติผู้เข้าร่วมประมูล" CssClass="lblHeader"></asp:Label>
            <p>
                <table class="tbStyle">
                    <tr>
                        <td class="style16">
                            <asp:Label ID="Label1" runat="server" Text="1.ผู้ประกวดราคาเป็นนิติบุคคลที่จดทะเบียนตามกฎหมายไทย"
                                Font-Bold="False" Width="80%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:RadioButtonList ID="rdoQuestion1" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">ใช่</asp:ListItem>
                                <asp:ListItem Value="0">ไม่ใช่</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:Label ID="Label17" runat="server" Text="2.ผู้ประกวดราคาเป็นนิติบุคคลมีประสบการณ์ทั้งสิ้นกี่ปี (โปรดระบุวันที่จดทะเบียนตามหนังสือรับรองบริษัท)"
                                Font-Bold="False" Width="80%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">วันที่จดทะเบียน:
                            <asp:TextBox ID="txtRegisterDate" runat="server" SkinID="ReadOnly" TabIndex="5" ReadOnly="false"
                                OnTextChanged="txtRegisterDate_TextChanged" AutoPostBack="true" Width="100px" CssClass="TextAreaRemark"></asp:TextBox>
                            <act:CalendarExtender ID="txtRegisterDate_CalendarExtender" runat="server" Enabled="True"
                                TargetControlID="txtRegisterDate" Format="dd/MM/yyyy">
                            </act:CalendarExtender>
                            &nbsp;ประสบการณ์ :
                            <asp:Label ID="lblTotalYear" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:Label ID="Label3" runat="server" Text="3.ผู้ประกวดราคามีประสบการณ์การให้บริการในโรงงานอุตสาหกรรม เช่น โรงไฟฟ้า,  อุตสาหกรรมปิโตรเคมี, โรงงานอุตสาหกรรมขนาดใหญ่ เป็นต้น"
                                Font-Bold="False" Width="80%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:RadioButtonList ID="rdoQuestion3" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">ใช่</asp:ListItem>
                                <asp:ListItem Value="0">ไม่ใช่</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style16">
                            <asp:Label ID="Label11" runat="server" Text="4.จากข้อ 2 ผู้ประกวดราคามีประสบการณ์ ไม่น้อยกว่า 4 แห่ง โดยมีอายุสัญญาจ้างไม่น้อยกว่า 1 (หนึ่ง) ปี และมีหนังสือรับรองผลงาน"
                                Font-Bold="False" Width="80%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style16">
                            <asp:RadioButtonList ID="rdoQuestion4" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">ใช่</asp:ListItem>
                                <asp:ListItem Value="0">ไม่ใช่</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:Label ID="Label7" runat="server" Text="5.มีระบบบริหารงานที่น่าเชื่อถือ เช่น ได้การรับรองมาตรฐาน ISO หรือมาตรฐานอื่นที่ยอมรับได้"
                                Font-Bold="False" Width="80%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:RadioButtonList ID="rdoQuestion5" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">ใช่</asp:ListItem>
                                <asp:ListItem Value="0">ไม่ใช่</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:Label ID="Label8" runat="server" Text="6.ผู้ประกวดราคามีสาขา หรือศูนย์บริการตั้งอยู่ในเขตจังหวัดที่โรงไฟฟ้า (ผู้ว่าจ้าง) ตั้งอยู่ หรือจังหวัดใกล้เคียงแต่มี ระยะห่างจากสาขาหรือศูนย์บริการ กับที่ตั้งโรงไฟฟ้า (ผู้ว่าจ้าง) ไม่เกิน 50 กิโลเมตร"
                                Font-Bold="False" Width="80%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:RadioButtonList ID="rdoQuestion6" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">ใช่</asp:ListItem>
                                <asp:ListItem Value="0">ไม่ใช่</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:Label ID="Label9" runat="server" Text="7.ผู้ประกวดราคาสามารถวางหนังสือค้ำประกันที่ออกโดยธนาคารไว้เพื่อเป็นหลักประกันการปฏิบัติหน้าที่ตามสัญญานี้ให้แก่ผู้ว่าจ้างในวงเงินจำนวน 2 (สอง) เท่าของอัตราค่าบริการรายเดือน"
                                Font-Bold="False" Width="80%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:RadioButtonList ID="rdoQuestion7" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">ใช่</asp:ListItem>
                                <asp:ListItem Value="0">ไม่ใช่</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:Label ID="Label10" runat="server" Text="8.คุณสมบัติอื่นๆ (ถ้ามี) โปรดระบุ" Font-Bold="False"
                                Width="80%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:TextBox ID="txtQuestion8" runat="server" Rows="3" TextMode="MultiLine" Width="60%" CssClass="TextAreaRemark"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style17">
                            <asp:Label ID="Label16" runat="server" Text="หมายเหตุ: ข้อมูลที่ท่านกรอกบริษัทฯจะนำมาประกอบการพิจารณาคัดเลือก ทั้งนี้ไม่ตัดสิทธิ์ ผู้ที่มีคุณสมบัติไม่ครบ"
                                Font-Bold="False" Width="80%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style17">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style16">
                            <asp:Button ID="btnAccept" runat="server" OnClick="btnAccept_Click" Text="บันทึก"
                                Width="150px" />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </p>

            <p>
                <!-- Modal popup "Popup Message" -->
                <asp:LinkButton ID="lbtnPopupCtrl" runat="server"></asp:LinkButton>
                <asp:LinkButton ID="lbtnTargerCtrl" runat="server"></asp:LinkButton>
                <act:ModalPopupExtender ID="lbtnPopup_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="lbtnPopupCtrl" DynamicServicePath="" Enabled="True" PopupControlID="pnPopup"
                    TargetControlID="lbtnTargerCtrl">
                </act:ModalPopupExtender>
                <!--   -->
                <asp:Panel ID="pnPopup" runat="server" CssClass="PanelPopup" Width="40%" Height="30%"
                    Style="display: none">
                    <table style="width: 100%;" align="center">
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="style2" align="left">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblMsgResult1" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnOK" runat="server" CausesValidation="False" Text="ตกลง" Width="100px"
                                    OnClick="btnOK_Click" />
                                <asp:HiddenField ID="hdfCompanyNo" runat="server" />
                                <asp:HiddenField ID="hdfProjectNo" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </p>
        </ContentTemplate>
        <%--   <Triggers>
            <asp:PostBackTrigger ControlID="btnAccept" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
