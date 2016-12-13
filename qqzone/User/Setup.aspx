<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Setup.aspx.cs" Inherits="Setup" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server"> 

    <asp:Button ID="btnZoneVisible" runat="server" Text="谁能看我的空间" OnClick="btnZoneVisible_Click" />
    <asp:Button ID="btnVisitorVisible" runat="server" Text="谁能看我的访客" OnClick="btnVisitorVisible_Click" />
    <asp:Button ID="btnComment" runat="server" Text="谁能评论我的空间" OnClick="btnComment_Click" />
    <asp:Button ID="btnNoSee" runat="server" Text="不看他的动态" OnClick="btnNoSee_Click" />

    <div id="divChoose" runat="server" visible="false">

        <asp:RadioButton ID="rbAll" runat="server" GroupName="choose" OnCheckedChanged="rbAll_CheckedChanged" Text="所有人" Checked ="true"  />
        <asp:RadioButton ID="rbFriend" runat="server" GroupName="choose" OnCheckedChanged="rbFriend_CheckedChanged" Text="QQ好友" />
        <asp:RadioButton ID="rbSelf" runat="server" GroupName="choose" OnCheckedChanged="rbSelf_CheckedChanged" Text="仅自己" />
        <br />
        </div> 

        <asp:Button ID="btnZoneSubmit" runat="server" Text="确定" OnClick="btnZoneSubmit_Click" Visible ="false" />
        <asp:Button ID="btnVisitorSubmit" runat="server" Text="确定" OnClick="btnVisitorSubmit_Click" Visible ="false" />
        <asp:Button ID="btnCommentSubmit" runat="server" Text="确定" OnClick="btnCommentSubmit_Click" Visible ="false" />

    <div id="divUnable" runat="server" visible="false" >

        <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" >
            <ItemTemplate>
                    <td><asp:Image ID="Image1" runat="server" Height="30px" Width="30px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                    <td><%# Eval("usernumber")%></td>
                    <td><asp:LinkButton ID="lbtFriend" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                    <td><%# Eval("sex")%></td>
                    <td><asp:LinkButton ID="lbtUnable" runat="server" Text="屏蔽" CommandName="unable" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                    <br />
                </tr>
            </ItemTemplate>
        </asp:Repeater>

        </div> 
      
 
    
    </asp:Content>
