<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FriendPhotos.aspx.cs" Inherits="FriendPhotos" MasterPageFile="FriendMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">   

    <asp:Repeater ID="rptPhoto" runat="server" >
     <ItemTemplate>
          <div style="float: left; width: 33%;">
               <asp:Image ID="ibtPhoto" runat="server" Height="80px" Width="80px" ImageUrl='<%# Eval("photopath") %>' />
              <asp:Label ID="lbName" runat="server" Text='<%# Eval("photoname") %>' Visible="False"></asp:Label>
          </div>
    </ItemTemplate>
    </asp:Repeater>
    <br />

    <asp:Repeater ID="RptComment" runat="server"  >
                    <ItemTemplate>
                        <td><asp:Image ID="imgFriendHead" runat="server" Height="30px" Width="30px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                        <td><asp:LinkButton ID="lbtVisitFriend" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                        <asp:TextBox ID="txtCommentContent" runat="server" Text ='<%#Eval("comment") %>' Height="20px" Width="500px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" Enabled="false"  ></asp:TextBox>                  
                        <asp:Label ID="lbCommenttime" runat="server" Text='<%#Eval("publishtime") %>' Font-Size ="12px"></asp:Label><br />                       
                        <asp:TextBox ID="txtReplycomment" runat="server" Height="20px" Width="500px" Visible ="false" ></asp:TextBox>
                        <br />  
                     </ItemTemplate>
                </asp:Repeater>         

    <asp:TextBox ID="txtComment" runat="server" Height="20px" Width="590px"></asp:TextBox>
    <asp:Button ID="btnReply" runat="server" Text="回复"  OnClick="btnReply_Click"/>
    <br />            

    <asp:Button ID="btnUp" runat="server" Text="上一页" OnClick="btnUp_Click" />
        <asp:Button ID="btnDrow" runat="server" Text="下一页"  OnClick="btnDrow_Click"/>
        <asp:Button ID="btnFirst" runat="server" Text="首页" OnClick="btnFirst_Click" />
        <asp:Button ID="btnLast" runat="server" Text="尾页"  OnClick="btnLast_Click"/>
        页次：<asp:Label ID="lbNow" runat="server" Text="1"></asp:Label>
        /<asp:Label ID="lbTotal" runat="server" Text="1"></asp:Label>

        转<asp:TextBox ID="txtJump" Text="1" runat="server" Width="25px"></asp:TextBox>
        <asp:Button ID="btnJump" runat="server" Text="Go"  OnClick="btnJump_Click"/>

        


  </asp:Content>
