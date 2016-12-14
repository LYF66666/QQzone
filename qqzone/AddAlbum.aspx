<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddAlbum.aspx.cs" Inherits="AddAlbum" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">   

    <p>
        相册名称：<asp:TextBox ID="txtName" runat="server" ></asp:TextBox></p>
    <br />
        相册封面：
    <asp:FileUpload ID="myFileUpload" runat="server"  />
    <asp:Label ID="lbImage" runat="server" Text="Label" Visible="false" ></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnUpload" runat="server" Text="上传图片" OnClick="btnUpload_Click" /><br />
    <asp:Image ID="Image" runat="server" Height="100px" Width="100px"  />
    <br />
    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />


    </asp:Content>
