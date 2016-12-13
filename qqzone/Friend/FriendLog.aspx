<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FriendLog.aspx.cs" Inherits="FriendLog" MasterPageFile="FriendMasterPage.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">

    <asp:Repeater ID="rptLog" runat="server" OnItemCommand="rptLog_ItemCommand"  >
            <HeaderTemplate> 
                <table>
                    <tr>
                        <th>标题</th>
                        <th>发表时间</th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td><asp:LinkButton ID="lbtHeadline" runat="server" Text='<%#Eval("headline") %>' CommandName="headline" CommandArgument='<%#Eval("logid") %>' ></asp:LinkButton></td>
                    <td><%# Eval("publishtime")%></td>
                    <br />
                </tr>
            </ItemTemplate>
        </asp:Repeater>

    </asp:Content>
