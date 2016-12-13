<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Getnumber.aspx.cs" Inherits="Getnumber" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        您好，您的账号为：<asp:Label ID="lbNumber" runat="server" Text="Label"></asp:Label>
        ，可用于登录空间<br />
        注意:10秒钟后页面将自动跳转到登录页面!<br />
        <asp:Button ID="btnturn" runat="server" Text="点此直接跳转" OnClick="btnturn_Click" />
    </form>
</body>
</html>
