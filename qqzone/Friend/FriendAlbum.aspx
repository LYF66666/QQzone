<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FriendAlbum.aspx.cs" Inherits="FriendAlbum" MasterPageFile="FriendMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">   

    <asp:Repeater ID="rptAlbum" runat="server" OnItemCommand="rptAlbum_ItemCommand">
     <ItemTemplate>
          <div style="float: left; width: 33%;">
               <asp:Image ID="ibtAlbum" runat="server" Height="80px" Width="80px" ImageUrl='<%# Eval("albumcover") %>' />
              <asp:LinkButton ID="lbtPhotos" runat="server" Text='<%# Eval("albumname") %>' CommandName="photo" CommandArgument='<%#Eval("albumid") %>'></asp:LinkButton>
              <asp:LinkButton ID="lbtSpread" runat="server" Text="转发" CommandName="spread" CommandArgument='<%#Eval("albumid") %>' ></asp:LinkButton><br /><br />
          </div>
    </ItemTemplate>
    </asp:Repeater>

    </asp:Content>
