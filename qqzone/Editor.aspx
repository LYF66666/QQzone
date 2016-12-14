<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Editor.aspx.cs" Inherits="Editor" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">    
    空间名称： 
    <asp:TextBox ID="txtzonename" runat="server"></asp:TextBox>
    <br />
    昵称：<asp:TextBox ID="txtnickname" runat="server"></asp:TextBox>
    <br />
    性别：<asp:RadioButton ID="rbMan" runat="server" GroupName="sex" Text="男" />
    <asp:RadioButton ID="rbWoman" runat="server" GroupName="sex" Text="女" />
    <br />
    生日：<asp:TextBox ID="txtbirthday" runat="server" Width="120px"></asp:TextBox>
    <br />
    手机号：<asp:TextBox ID="txtphonenumber" runat="server"></asp:TextBox>
    <br />
    现居地：<asp:TextBox ID="txtaddress" runat="server" Width="177px"></asp:TextBox>
    <br />
    <asp:Button ID="btnsave" runat="server" Text="保存" OnClick="btnsave_Click" style="height: 27px" />

</asp:Content>