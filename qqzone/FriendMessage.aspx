<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FriendMessage.aspx.cs" Inherits="FriendMessage" MasterPageFile="FriendMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server"> 

    <asp:TextBox ID="txtMessage" runat="server" Height="115px" Width="590px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" ></asp:TextBox>
    <br />
    <asp:Button ID="btnPublish" runat="server" Text="留言" OnClick="btnPublish_Click" />
    <br />

    <asp:Repeater ID="RptMessage" runat="server" OnItemCommand="RptMessage_ItemCommand" OnItemDataBound="RptMessage_ItemDataBound">
            <ItemTemplate>
                <td><asp:Image ID="imgHead" runat="server" Height="50px" Width="50px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                <td><asp:LinkButton ID="lbtVisit" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                <br />
                    <asp:TextBox ID="txtContent" runat="server" Text ='<%#Eval("messagecontent") %>' Height="115px" Width="590px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" Enabled="false" Font-Size ="16px"  ></asp:TextBox>
                    <asp:Label ID="lbSayingtime" runat="server" Text='<%#Eval("publishtime") %>' Font-Size ="14px"></asp:Label>
                    <br />
                <asp:Repeater ID="RptComment" runat="server" OnItemCommand="RptComment_ItemCommand"  >
                    <ItemTemplate>
                        <td><asp:Image ID="imgFriendHead" runat="server" Height="30px" Width="30px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                        <td><asp:LinkButton ID="lbtVisitFriend" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                        <asp:TextBox ID="txtCommentContent" runat="server" Text ='<%#Eval("comment") %>' Height="20px" Width="500px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" Enabled="false"  ></asp:TextBox>                  
                        <asp:Label ID="lbCommenttime" runat="server" Text='<%#Eval("publishtime") %>' Font-Size ="12px"></asp:Label><br />                       
                        <br />  
                     </ItemTemplate>
                </asp:Repeater>               
                <asp:TextBox ID="txtComment" runat="server" Height="20px" Width="590px"></asp:TextBox>
                <asp:LinkButton ID="lbtReply" runat="server" Text="回复" CommandName="reply" CommandArgument='<%#Eval("messageid") %>' ></asp:LinkButton><br />
            </ItemTemplate>
       </asp:Repeater>

    </asp:Content>
