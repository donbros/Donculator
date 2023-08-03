<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Calculator.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="Calculator.css" rel="stylesheet" />

</head>
<body><table class="calculator">
        <tr>
            <td colspan="4" style="text-align:center;">
                <asp:Button ID="Button12" runat="server" Text="All History" Width="125px" OnClick="Button12_Click" />
            </td>
        </tr>
        <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label11" runat="server" Text="ID "></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style3">
                    <asp:Button ID="Button8" runat="server" Text="Search by ID" Width="250px" OnClick="Button8_Click"/>
                </td>
                <%--<td class="auto-style3">
                    <asp:Button ID="Button10" runat="server" Text="Remove By ID" Width="250px" OnClick="Button10_Click" />
                </td>--%>
        </tr>
        <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label12" runat="server" Text="Results "></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style3">
                    <asp:Button ID="Button9" runat="server" Text="Search by Result" Width="250px" OnClick="Button9_Click"/>
                </td>
               <%-- <td class="auto-style3">
                    <asp:Button ID="Button11" runat="server" Text="Remove By results" Width="250px" OnClick="Button11_Click" />
                </td>--%>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center;">
                <asp:Button ID="Button7" runat="server" Text="Hide History" Width="125px" OnClick="Button7_Click" />
            </td>
            <%--<td>
            <asp:TableCell AssociatedHeaderCellID="Column1Header Column2Header Column3Header"  
            ColumnSpan="3"  
            HorizontalAlign="Center">
            </asp:TableCell>  
            </td>--%>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center"";>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center;">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
             </td>
        </tr>
        <tr>
            <td colspan="4" class="calc_td_result">
            <asp:gridview id="GridView1" 
                HeaderStyle-BackColor="#3AC0F2"
                HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"
                RowStyle-ForeColor="#3A3A3A" 
                visible="true" runat="server" AllowPaging="true" PagerStyle-CssClass="paging" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10" CssClass="calc_other">
            </asp:gridview>
            </td>
        </tr>
        </table>
        <table></table>
    <asp:HiddenField ID="HiddenField7" runat="server" Value='True'/>
    <asp:HiddenField ID="HiddenField8" runat="server" Value='False'/> 
    </body>
    </html>
</asp:Content>