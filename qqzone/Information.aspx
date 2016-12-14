<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Information.aspx.cs" Inherits="Information" MasterPageFile="MasterPage.master" %>


    <asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">    
 
 
        <br />
        基本资料&nbsp;&nbsp;
        <br />
        性别：<asp:Label ID="lbSex" runat="server" Text="Label"></asp:Label>
        <br />
        年龄：<asp:Label ID="lbAge" runat="server" Text="Label"></asp:Label>
        <br />
        生日：<asp:Label ID="lbBirthday" runat="server" Text="Label"></asp:Label>
        <br />
        手机号：<asp:Label ID="lbPhone" runat="server" Text="Label"></asp:Label>
        <br />
        现居地：<asp:Label ID="lbAddress" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="btnEditor" runat="server" Text="修改" OnClick="btnEditor_Click" />
        <asp:Button ID="btnPwd" runat="server" Text="重置密码" OnClick="btnPwd_Click" />
        <br />
       
            
       
    
 
    </asp:Content>


