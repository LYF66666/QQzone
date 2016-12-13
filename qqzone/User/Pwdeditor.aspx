<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pwdeditor.aspx.cs" Inherits="Pwdeditor" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server"> 

    原密码：<asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    新密码：<asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    手机号：<asp:TextBox ID="txtPhonenumber" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="btnsave" runat="server" Text="保存" OnClick="btnsave_Click" style="height: 27px" />
    </asp:Content>
