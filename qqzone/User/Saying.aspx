<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Saying.aspx.cs" Inherits="Saying" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">   

    <asp:TextBox ID="txtSaying" runat="server" Height="115px" Width="590px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" ></asp:TextBox>
    <br />
    <asp:Button ID="btnPublish" runat="server" Text="发表说说" OnClick="btnPublish_Click" />
    <br />

    <div>
    <asp:Repeater ID="RptSaying" runat="server" OnItemCommand="RptSaying_ItemCommand" OnItemDataBound="RptSaying_ItemDataBound">
            <ItemTemplate>
                <td><asp:Image ID="imgHead" runat="server" Height="50px" Width="50px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                <td><asp:LinkButton ID="lbtVisit" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                <br />
                    <asp:TextBox ID="txtContent" runat="server" Text ='<%#Eval("sayingcontent") %>' Height="115px" Width="590px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" Enabled="false" Font-Size ="16px"  ></asp:TextBox>
                    <asp:Label ID="lbSayingtime" runat="server" Text='<%#Eval("publishtime") %>' Font-Size ="14px"></asp:Label><br />
                <asp:Repeater ID="RptComment" runat="server" OnItemCommand="RptComment_ItemCommand" OnItemDataBound="RptComment_ItemDataBound" >
                    <ItemTemplate>
                        <td><asp:Image ID="imgFriendHead" runat="server" Height="30px" Width="30px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                        <td><asp:LinkButton ID="lbtVisitFriend" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                        <asp:TextBox ID="txtCommentContent" runat="server" Text ='<%#Eval("comment") %>' Height="20px" Width="500px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" Enabled="false"  ></asp:TextBox>
                        <asp:LinkButton ID="lbtReply" runat="server" Text="回复" CommandName="reply"  ></asp:LinkButton>                      
                        <asp:Label ID="lbCommenttime" runat="server" Text='<%#Eval("publishtime") %>' Font-Size ="12px"></asp:Label><br />                       
                        <asp:TextBox ID="txtReplycomment" runat="server" Height="20px" Width="500px" Visible ="false" ></asp:TextBox>
                        <asp:LinkButton ID="lbtPublish" runat="server" Text="发表" CommandName="publish" CommandArgument='<%#Eval("commentid") %>' Visible ="false"  ></asp:LinkButton>
                        <asp:LinkButton ID="lbtReturn" runat="server" Text="取消" CommandName="return" Visible ="false"  ></asp:LinkButton>
                        <br />
                 <asp:Repeater ID="RptReply" runat="server" OnItemCommand="RptReply_ItemCommand" >
                     <ItemTemplate>
                            <asp:Image ID="imgReply" runat="server" Height="25px" Width="25px" ImageUrl='<%# Eval("headimagepath") %>' />
                            <asp:LinkButton ID="lbtReplyuser" runat="server" Text='<%#Eval("nickname") %>' CommandName="replyuser" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton>
                            <asp:TextBox ID="txtReplyContent" runat="server" Text ='<%#Eval("reply") %>' Height="15px" Width="400px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" Enabled="false"  ></asp:TextBox>
                            <asp:Label ID="lbReplytime" runat="server" Text='<%#Eval("publishtime") %>' Font-Size ="10px"></asp:Label><br />
                     </ItemTemplate>
                 </asp:Repeater>     
                     </ItemTemplate>
                </asp:Repeater>               
                <asp:TextBox ID="txtComment" runat="server" Height="20px" Width="590px"></asp:TextBox>
                <asp:LinkButton ID="lbtReply" runat="server" Text="回复" CommandName="reply" CommandArgument='<%#Eval("sayingid") %>' ></asp:LinkButton><br />
                <asp:LinkButton ID="lbtSpread" runat="server" Text="转发" CommandName="spread" CommandArgument='<%#Eval("sayingid") %>' ></asp:LinkButton>
                <asp:LinkButton ID="lbtDelete" runat="server" Text="删除" CommandName="delete" CommandArgument='<%#Eval("sayingid") %>' ></asp:LinkButton><br />
            </ItemTemplate>
       </asp:Repeater>
    </div>
</asp:Content>
