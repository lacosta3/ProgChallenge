<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true"  MasterPageFile="~/Site2.Master" CodeBehind="GroceryChallenge.aspx.cs" Inherits="GroceryChallenge._default" %>

<asp:Content runat="server" ID="MainContent1" ContentPlaceHolderID="MainContent">
    <asp:HiddenField ID="hfPartnerID" runat="server" Visible="False" />
    <asp:Panel ID="pnlRegister" Visible="false" runat="server">
        <%-- Title --%>
    <table style="width:100%;text-align:left">
        <tr>
            <td>
                <asp:Label ID="lblTitle" runat="server" ForeColor="White" Font-Names="Arial,Courier New" Font-Size="2.3em" Text="New User Registration"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="1" style="font-family:Arial,Courier New;width:45%; margin-top:10px; font-size:9pt;color:black; border-collapse:collapse;height:100%;float:left;">
        <caption class="caption">Create a Profile</caption>
        <tr style="border:solid;border-width:1px;">
            <td style ="text-align:left;font-weight:600;font-size:8pt;padding-left:10px">
                <asp:Label ID="Label1" runat="server" Width="175px" Text="FIRST NAME:"></asp:Label>
            </td>
            <td style ="text-align:left">
                <asp:TextBox ID="txtFirstName" runat="server" Width="150px" ToolTip="Your Preferred First Name." ></asp:TextBox>
            </td>
        </tr>
        <tr style="border:solid;border-width:1px;">
            <td style ="text-align:left;font-weight:600;font-size:8pt;padding-left:10px">
                <asp:Label ID="Label2" runat="server" Width="175px" Text="LAST NAME:"></asp:Label>
            </td>
            <td style ="text-align:left">
                <asp:TextBox ID="txtLastName" runat="server" Width="150px" ToolTip="Your Preferred Last Name." ></asp:TextBox>
            </td>
        </tr>

       <tr style="border:solid;border-width:1px;">
            <td style ="text-align:left;font-weight:600;font-size:8pt;padding-left:10px">
               
                <asp:Label ID="Label3" runat="server" Width="175px" Text="HEB EMAIL ADDRESS:(smith.john@heb.com)"></asp:Label>
            </td>
            <td style ="text-align:left">
                <asp:TextBox ID="txtEmail" runat="server" Width="225px" ToolTip="Your H-E-B Email Like: smith.john@heb.com" ></asp:TextBox>
            </td>
        </tr>
         <tr  style="font-family:Arial,Courier New; background-color:#5D7B9D; border-collapse:collapse;height:25px;">
            <td colspan="2"style ="text-align:center">
                <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Register" />
            </td>
        </tr>
    </table>

    <table style="width:45%; margin-top:200px;text-align:left;border:solid; border-width:3px;border-top-color:white;border-bottom-color:white;border-left:none;border-right:none;">
        <tr style="font-family:Arial,Courier New;">
            <td>
               <p>
                   WARNING!<br />
                   All systems are subject to monitoring for unauthorized access and verification of 
                   security procedures. Use of the system constitutes consent to monitoring for this
                   purpose. Unauthorized use of this system may subject you to criminal prosecution and penalties.
               </p>
            </td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>
