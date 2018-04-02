<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoAccess.aspx.cs" Inherits="GroceryChallenge.NoAccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

<title><%: Page.Title %>No Access</title>

</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/notfound.png"/>
        
            <h1><b>You do NOT have access to the Grocery Challenge Dashboard.</b></h1>
        <br/>
            <h1><b>Please verify that you are logged into H-E-B Applications.</b></h1>
        <br/>
            <h4>If you continue to have problems, please submit a Shelf Edge Issue Form.</h4>

    </div>
    </form>
</body>
</html>