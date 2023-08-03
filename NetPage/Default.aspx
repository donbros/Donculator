<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Calculator._Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<%--<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
<html>
<head>
    <title>Domantas Calculator</title>
    <link href="Calculator.css" rel="stylesheet" />
     
</head>
<body>

    <asp:Panel ID="LoginPanel" runat="server" DefaultButton="btLogin">
    <div class="calculator-holder">
    <table class="calculator" id="calc">
        <tr>
            <td colspan="4" class="calc_td_result">
            <asp:TextBox ID="TextBox1" runat="server" Text="" ReadOnly="false" CssClass="calc_result"
                placeholder="left-hand side of an equation"
                Wrap="true"
                MaxLength="200"
                TextMode="MultiLine" 
                Width="350px"
                Height="100px"
                ></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td colspan="4" class="calc_td_result">
            <asp:TextBox ID="TextBox3" AccessKey="Q" runat="server" Text="" ReadOnly="false" CssClass="calc_result"
                placeholder="right-hand side of an equation"
                Wrap="true"
                MaxLength="200"
                TextMode="MultiLine" 
                Width="350px"
                Height="100px"
                ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="calc_td_btn">
            <asp:Button ID="SwitchTextBoxButton" runat="server" Text="Line 1" CssClass="calc_btn" OnClick="SwitchTextBoxButton_Click" BackColor="#19EAEE" />
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="Button1" runat="server" Text="C" CssClass="calc_btn" OnClick="Button1_Click" BackColor="#19EAEE" />
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="Button7" runat="server" Text="HIS" CssClass="calc_btn" OnClick="Button7_Click" BackColor="#19EAEE" />
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="ButtonDatabase" runat="server" Text="Net Off" CssClass="calc_btn" OnClick="ButtonDatabase_Click" BackColor="#19EAEE" />
            </td>
        </tr>
        <tr>
            <td class="calc_td_btn">
            <asp:Button ID="UnknownVariable" runat="server" Text="x" CssClass="calc_btn" OnClick="UnknownVariable_Click" BackColor="#19EAEE" />
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="ParenthesisOpen" runat="server" Text="(" CssClass="calc_btn" OnClick="ParenthesisOpen_Click" BackColor="#19EAEE"/>
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="ParenthesisClose" runat="server" Text=")" CssClass="calc_btn" OnClick="ParenthesisClose_Click" BackColor="#19EAEE" />
            </td>
            <%-- Negalime naudoti nes nerealizuoti ArrayHiddenList (į stringą surašyti duomenys) --%>
            <%--<td class="calc_td_btn">
            <asp:Button ID="Button23" runat="server" Text="<-" CssClass="calc_btn" OnClick="Button23_Click" BackColor="#19EAEE"/>
            </td>--%>
        </tr>
        <tr>
            <td class="calc_td_btn">
            <asp:Button ID="Button11" runat="server" Text="1" CssClass="calc_btn" OnClick="Button11_Click"/>
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="Button12" runat="server" Text="2" CssClass="calc_btn" OnClick="Button12_Click"/>
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="Button13" runat="server" Text="3" CssClass="calc_btn" OnClick="Button13_Click"/>  
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="Button3" runat="server" Text="+" CssClass="calc_btn" OnClick="Button3_Click" BackColor="#19EAEE"/>
            </td>
            <td class="calc_td_btn">
            </td>
        </tr>
        <tr>
            <td class="calc_td_btn">
            <asp:Button ID="Button14" runat="server" Text="4" CssClass="calc_btn" OnClick="Button14_Click"/>
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="Button15" runat="server" Text="5" CssClass="calc_btn" OnClick="Button15_Click"/>
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="Button16" runat="server" Text="6" CssClass="calc_btn" OnClick="Button16_Click"/>
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="Button4" runat="server" Text="-" CssClass="calc_btn" OnClick="Button4_Click" BackColor="#19EAEE" />
            </td>
         </tr>
         <tr>
            <td class="calc_td_btn">
            <asp:Button ID="Button17" runat="server" Text="7" CssClass="calc_btn" OnClick="Button17_Click" />
            </td>
             <td class="calc_td_btn">
            <asp:Button ID="Button18" runat="server" Text="8" CssClass="calc_btn" OnClick="Button18_Click"/>
            </td>
             <td class="calc_td_btn">
            <asp:Button ID="Button19" runat="server" Text="9" CssClass="calc_btn" OnClick="Button19_Click"/>
            </td>
             <td class="calc_td_btn">
            <asp:Button ID="Button5" runat="server" Text="*" CssClass="calc_btn" OnClick="Button5_Click" BackColor="#19EAEE" />
            </td>
         </tr>
         <tr>
            <td class="calc_td_btn">
            <asp:Button ID="Button20" runat="server" Text="0" CssClass="calc_btn" OnClick="Button20_Click"/>
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="Button24" runat="server" Text="00" CssClass="calc_btn" OnClick="Button24_Click"/>
            </td>
            <td class="calc_td_btn">
            <asp:Button ID="Button21" runat="server" Text="." CssClass="calc_btn" OnClick="Button21_Click"/>
                    </td>
             <td class="calc_td_btn">
            <asp:Button ID="Button6" runat="server" Text="/" CssClass="calc_btn" OnClick="Button6_Click" BackColor="#19EAEE" />
                        </td>
        </tr>
        <tr>
            <td class="calc_td_result" colspan="4" >
            <asp:Button ID="btLogin" runat="server" Text="=" CssClass="calc_btn" OnClick="Button2_Click" BackColor="#19EAEE" Width="346px" />
            </td>
        </tr>
        <tr>
            <td class="calc_td_result" colspan="4">
                <asp:TextBox ID="TextBox2" runat="server" Text="" ReadOnly="true" CssClass="calc_result"
                placeholder="results"
                Wrap="true"
                MaxLength="1000"
                TextMode="MultiLine" 
                Width="350px"
                Height="100px"
                ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="calc_td_result" colspan="4">
            <asp:TextBox ID="TextBox4" runat="server" Text="" ReadOnly="true" CssClass="calc_result"
                placeholder="results metadata"
                Wrap="true"
                MaxLength="1000"
                TextMode="MultiLine" 
                Width="350px"
                Height="100px"
                ></asp:TextBox>
            </td>
        </tr>
        </table>
        <table class="calculator">
        <tr>
            <td class="calc_td_result">
            <asp:TextBox ID="TextBoxIntervalStart" placeholder="interval start" runat="server" Text="" ReadOnly="false" CssClass="calc_result"></asp:TextBox>
            </td>
            <td class="calc_td_result">
            <asp:TextBox ID="TextBoxIntervalEnd" placeholder="interval end" runat="server" Text="" ReadOnly="false" CssClass="calc_result"></asp:TextBox>
            </td>
         </tr>
        <tr>
            <td class="calc_td_result">
            <asp:TextBox ID="TextBoxIntervalStep" placeholder="step" runat="server" Text="" ReadOnly="false" CssClass="calc_result"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td colspan="2" class="calc_td_result">
                <%-- Chart'as --%>
                <asp:Chart ID="Chart1" runat="server">
                    <Series >
                        <asp:Series Name="Series1" ChartType="Line" XValueMember="X" YValueMembers="Y" ></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" >
                            <AxisY Interval="5" Maximum="20" Minimum="-20">
                            </AxisY>
                            <AxisX Interval="5" Maximum="20" Minimum="-20">
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="calc_td_result">
            <asp:gridview id="GridView1" 
                HeaderStyle-BackColor="#3AC0F2"
                HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"
                RowStyle-ForeColor="#3A3A3A" 
                visible="false" runat="server" AllowPaging="true" PagerStyle-CssClass="paging" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10" CssClass="calc_other">
            </asp:gridview>
            </td>
        </tr>
        </table>
    </div>
    <%-- Gerai būtų konvertinti į Array, kad galėtume naudoti "<-" --%>
    <%-- ----------------------- BUTONS MODES -------------------------- --%>
        <%-- Equals Clear Things --%>
    <asp:HiddenField ID="HiddenField6" runat="server" Value='False'/> 
        <%-- History Show/Hide --%>
    <asp:HiddenField ID="HiddenField7" runat="server" Value='False'/> 
        <%-- Online Features --%>
    <asp:HiddenField ID="HiddenField8" runat="server" Value='False'/> 
        <%-- Switch TextBox --%>
    <asp:HiddenField ID="SwitchToOther" runat="server" Value='False'/> 
    <%-- ----------------------- VALIDATION TABLE 1 -------------------------- --%>
    <asp:HiddenField ID="BracketsControlField" runat="server" Value='0'/> 
    <asp:HiddenField ID="Number" runat="server" Value='True'/> 
    <asp:HiddenField ID="Addition" runat="server" Value='False'/> 
    <asp:HiddenField ID="Subtraction" runat="server" Value='True' /> 
    <asp:HiddenField ID="MultiplicationDivision" runat="server" Value='False'/> 
    <asp:HiddenField ID="Comma" runat="server" Value='False'/> 
    <asp:HiddenField ID="OpenParenthesis" runat="server" Value='True'/> 
    <asp:HiddenField ID="CloseParenthesis" runat="server" Value='False'/>
    <asp:HiddenField ID="UnknownVar" runat="server" Value='True'/>

    <%-- žymi kada kablelis galimas t.y. jei buvo įvykdyta daugyba ir dalyba (pradžioje galimas irgi) --%>
    <asp:HiddenField ID="CommaLegitField" runat="server" Value='True'/>
    <%-- realiai tikrina ar geru simboliu užbaigiama veiksmų seka (prieš pat lygybę), true kad leistų įvesti ctrl + v būdu --%>
    <asp:HiddenField ID="EqualsLegitField" runat="server" Value='True'/>
    <%-- ------------------------------------------------------------------- --%>
    <%-- ----------------------- VALIDATION TABLE 2 -------------------------- --%>
    <asp:HiddenField ID="BracketsControlField2" runat="server" Value='0'/> 
    <asp:HiddenField ID="Number2" runat="server" Value='True'/> 
    <asp:HiddenField ID="Addition2" runat="server" Value='False'/> 
    <asp:HiddenField ID="Subtraction2" runat="server" Value='True' /> 
    <asp:HiddenField ID="MultiplicationDivision2" runat="server" Value='False'/> 
    <asp:HiddenField ID="Comma2" runat="server" Value='False'/> 
    <asp:HiddenField ID="OpenParenthesis2" runat="server" Value='True'/> 
    <asp:HiddenField ID="CloseParenthesis2" runat="server" Value='False'/>
    <asp:HiddenField ID="UnknownVar2" runat="server" Value='True'/>

    <asp:HiddenField ID="CommaLegitField2" runat="server" Value='True'/>
    <%-- Legit dėl grafiko --%>
    <asp:HiddenField ID="EqualsLegitField2" runat="server" Value='True'/>
    <%-- ------------------------------------------------------------------- --%>
    
    </asp:Panel>
</body>
</html>
</asp:Content>

<%-- 4*-3 --%>