<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        账号：<asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
        <br />
        密码：<asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
        <br />
        验证码：<asp:TextBox ID="txtValidCode" runat="server"></asp:TextBox>
        <asp:ImageButton ID="imgbtnValid" runat="server" OnClick="imgbtnValid_Click" ImageURL="~/ValidCode.aspx" />
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" />
        <asp:Button ID="btnRegister" runat="server" Text="注册" OnClick="btnRegister_Click" />
    
    </div>
    </form>
</body>
</html>
