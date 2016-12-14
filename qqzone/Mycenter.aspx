<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mycenter.aspx.cs" Inherits="Mycenter" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">   

    <asp:TextBox ID="txtSaying" runat="server" Height="115px" Width="590px" MaxLength ="0" TextMode = "MultiLine" Wrap ="true" ></asp:TextBox>
    <br />
    <asp:Button ID="btnPublish" runat="server" Text="发表说说" OnClick="btnPublish_Click" />
    谁能看见：<asp:DropDownList ID="DropDownList" runat="server" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem>所有人</asp:ListItem>
        <asp:ListItem>QQ好友</asp:ListItem>
        <asp:ListItem>仅自己</asp:ListItem>
        <asp:ListItem>自定义</asp:ListItem>
    </asp:DropDownList>
&nbsp;<br />

    <div id="divList" runat="server" visible="false">
        <asp:Repeater ID="rptList" runat="server" >
            <ItemTemplate>
                    <td><asp:Image ID="Image1" runat="server" Height="30px" Width="30px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                    <td><%# Eval("usernumber")%></td>
                    <td><asp:LinkButton ID="lbtFriend" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                    <td><%# Eval("sex")%></td>
                    <td><asp:CheckBox ID="cbFriend" runat="server" /></td>
                    <br />
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <asp:Button ID="btnSelect" runat="server" Text="选择已有标签" OnClick="btnSelect_Click" style="height: 27px" />
        <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" Visible ="false" />
        <br />
        <asp:Repeater ID="rptLable" runat="server" Visible ="false" OnItemDataBound="rptLable_ItemDataBound" >
            <ItemTemplate>
                    <td><asp:RadioButton ID="rbLable" runat="server" AutoPostBack="true" oncheckedchanged="rbLable_CheckedChanged"/></td>
                    <td><%# Eval("lablename")%></td>
                <br />
                <asp:Repeater ID="rptLableFriend" runat="server" Visible="false"  >
                     <ItemTemplate>
                            <asp:Image ID="imglable" runat="server" Height="25px" Width="25px" ImageUrl='<%# Eval("headimagepath") %>' />
                            <asp:Label ID="lblableusernumber" runat="server" Text='<%# Eval("usernumber") %>' ></asp:Label>
                            <asp:Label ID="lbnickname" runat="server" Text='<%# Eval("nickname") %>' ></asp:Label>     
                         <br />                      
                     </ItemTemplate>
                 </asp:Repeater>    
                <br />
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <asp:Button ID="btnCheck" runat="server" Text="确定" OnClick="btnCheck_Click" />
        <asp:Button ID="btnSave" runat="server" Text="保存至标签" OnClick="btnSave_Click" />
        <asp:Label ID="lbLablename" runat="server" Text="标签名：" Visible ="false" ></asp:Label>
        <asp:TextBox ID="txtLabel" runat="server" Visible ="false"  ></asp:TextBox>
        <asp:Button ID="btnSaveLable" runat="server" Text="保存" OnClick="btnSaveLable_Click" Visible ="false" />
        <asp:Button ID="btnReturn" runat="server" Text="取消" OnClick="btnReturn_Click" Visible ="false" />
    </div>

    

    <div>
    <asp:Repeater ID="RptSaying" runat="server" OnItemCommand="RptSaying_ItemCommand" OnItemDataBound="RptSaying_ItemDataBound" >
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
                <asp:LinkButton ID="lbtPraise" runat="server" CommandName="praise" CommandArgument='<%#Eval("sayingid") %>' ></asp:LinkButton>
                (<asp:Label ID="lbPraiseNumber" runat="server" Text='<%#Eval("praise") %>' ></asp:Label>)<br />
            </ItemTemplate>
       </asp:Repeater>
    </div>
</asp:Content>
