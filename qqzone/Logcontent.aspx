<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logcontent.aspx.cs" Inherits="Logcontent" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">

    标题：<asp:TextBox ID="txtHeadline" runat="server"  Height="20px" Width="300px" Enabled="false"  ></asp:TextBox><br />
    正文：<asp:TextBox ID="txtContent" runat="server"  Height="115px" Width="590px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" Enabled="false"></asp:TextBox><br />
    发表时间：<asp:Label ID="lbTime" runat="server" ></asp:Label><br />

    <asp:Repeater ID="RptComment" runat="server"  >
                    <ItemTemplate>
                        <td><asp:Image ID="imgFriendHead" runat="server" Height="30px" Width="30px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                        <td><asp:LinkButton ID="lbtVisitFriend" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                        <asp:TextBox ID="txtCommentContent" runat="server" Text ='<%#Eval("comment") %>' Height="20px" Width="500px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" Enabled="false"  ></asp:TextBox>                  
                        <asp:Label ID="lbCommenttime" runat="server" Text='<%#Eval("publishtime") %>' Font-Size ="12px"></asp:Label><br />                       
                        <br />  
                     </ItemTemplate>
                </asp:Repeater>         

    <asp:TextBox ID="txtComment" runat="server" Height="20px" Width="590px"></asp:TextBox>
    <asp:Button ID="btnReply" runat="server" Text="回复"  OnClick="btnReply_Click"/>
    <br />            
     </asp:Content>
