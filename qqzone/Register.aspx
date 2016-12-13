<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        昵称：<asp:TextBox ID="txtNickName" runat="server"></asp:TextBox>
        <br />
        密码：<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Height="23px"></asp:TextBox>
        <br />
        确认密码：<asp:TextBox ID="txtCheck" runat="server" TextMode="Password" Height="23px"></asp:TextBox>
        <br />
        性别：<asp:RadioButton ID="rbMan" runat="server" GroupName="sex" OnCheckedChanged="rbMan_CheckedChanged" Text="男" />
        <asp:RadioButton ID="rbWoman" runat="server" GroupName="sex" OnCheckedChanged="rbWoman_CheckedChanged" Text="女" />
        <br />
        出生年：<asp:TextBox ID="txtYear" runat="server" Width="40px"></asp:TextBox>
        月：<asp:TextBox ID="txtMonth" runat="server" Width="29px"></asp:TextBox>
        日：<asp:TextBox ID="txtDay" runat="server" Width="30px"></asp:TextBox>
        <br />
        手机号：<asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="立即注册" />
        <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="返回" />
    
    </div>
    </form>
</body>
</html>
