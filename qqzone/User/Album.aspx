<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" MasterPageFile="MasterPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">   

    <asp:ImageButton ID="ibtAddAlbum" runat="server" Height="80px" Width="80px" OnClick="ibtAddAlbum_Click" /><br />

    <asp:Repeater ID="rptAlbum" runat="server" OnItemCommand="rptAlbum_ItemCommand">
     <ItemTemplate>
          <div style="float: left; width: 33%;">
               <asp:Image ID="ibtAlbum" runat="server" Height="80px" Width="80px" ImageUrl='<%# Eval("albumcover") %>' />
              <asp:LinkButton ID="lbtPhotos" runat="server" Text='<%# Eval("albumname") %>' CommandName="photo" CommandArgument='<%#Eval("albumid") %>'></asp:LinkButton><br />
              <asp:LinkButton ID="lbtDelete" runat="server" Text="删除" CommandName="delete" CommandArgument='<%#Eval("albumid") %>'></asp:LinkButton>
          </div>
    </ItemTemplate>
    </asp:Repeater>

    </asp:Content>
