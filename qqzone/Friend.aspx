<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Friend.aspx.cs" Inherits="Friend" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">  
    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
    <br />
    <asp:RadioButton ID="rbtNumber" runat="server" OnCheckedChanged="rbtNumber_CheckedChanged" Text="账号" GroupName="search" />
    <asp:RadioButton ID="rbtNickname" runat="server" OnCheckedChanged="rbtNickname_CheckedChanged" Text="昵称" GroupName="search" />
    <br />
    <asp:Button ID="btnList" runat="server" Text="好友列表" OnClick="btnList_Click" />
    <asp:Button ID="btnRequest" runat="server" Text="好友请求" OnClick="btnRequest_Click" />
    <asp:Button ID="btnRecommend" runat="server" Text="好友推荐" OnClick="btnRecommend_Click" />
    <br />

    <div id="divSearch" runat="server" visible="false">
    <asp:Repeater ID="rptFriend" runat="server"  OnItemCommand="rptFriend_ItemCommand" >
            <ItemTemplate>
                    <td><asp:Image ID="imgFriendHead" runat="server" Height="30px" Width="30px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                    <td><%# Eval("usernumber")%></td>
                    <td><asp:LinkButton ID="lbtFriend" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                    <td><%# Eval("sex")%></td>
                    <td><asp:LinkButton ID="lbtAdd" runat="server" Text="加为好友" CommandName="add" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                    <br />
                </tr>
            </ItemTemplate>
        </asp:Repeater>
     </div> 

    <div id="divList" runat="server" visible="false">

        <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" >
            <ItemTemplate>
                    <td><asp:Image ID="imgFriendHead" runat="server" Height="30px" Width="30px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                    <td><%# Eval("usernumber")%></td>
                    <td><asp:LinkButton ID="lbtFriend" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                    <td><%# Eval("sex")%></td>
                    <td><asp:LinkButton ID="lbtDelete" runat="server" Text="删除好友" CommandName="delete" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                    <br />
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div id="divRequest" runat="server" visible="false">
    <asp:Repeater ID="rptRequest" runat="server"  OnItemCommand="rptRequest_ItemCommand" >
            <ItemTemplate>
                    <td><asp:Image ID="imgFriendHead" runat="server" Height="30px" Width="30px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                    <td><%# Eval("requesternumber")%></td>
                    <td><asp:LinkButton ID="lbtFriend" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("requesterid") %>' ></asp:LinkButton></td>
                    <td><%# Eval("sex")%></td>
                    <td><asp:Label ID="lbTime" runat="server" Text='<%#Eval("time") %>'></asp:Label></td>
                    <td><asp:LinkButton ID="lbtAgree" runat="server" Text="同意" CommandName="agree" CommandArgument='<%#Eval("requesterid") %>' ></asp:LinkButton></td>
                    <br />
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div id="divRecommend" runat="server" visible="false">
    <asp:Repeater ID="rptRecommend" runat="server"  OnItemCommand="rptRecommend_ItemCommand" >
            <ItemTemplate>
                    <td><asp:Image ID="imgFriendHead" runat="server" Height="30px" Width="30px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                    <td><%# Eval("usernumber")%></td>
                    <td><asp:LinkButton ID="lbtFriend" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                    <td><%# Eval("sex")%></td>
                    你们有<asp:Label ID="lbNumber" runat="server" Text='<%#Eval("number") %>'></asp:Label>个共同好友
                    <td><asp:LinkButton ID="lbtAdd" runat="server" Text="加为好友" CommandName="add" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                    <br />
                </tr>
            </ItemTemplate>
        </asp:Repeater>
     </div> 

    </asp:Content>
