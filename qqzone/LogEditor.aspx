<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogEditor.aspx.cs" Inherits="LogEditor" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">

    标题：<asp:TextBox ID="txtHeadline" runat="server"  Height="20px" Width="300px" ></asp:TextBox><br />
    正文：<asp:TextBox ID="txtContent" runat="server"  Height="115px" Width="590px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" ></asp:TextBox><br />
    发表时间：<asp:Label ID="lbTime" runat="server" ></asp:Label><br /> 
    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" style="height: 27px" />

    </asp:Content>
