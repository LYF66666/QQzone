<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Log.aspx.cs" Inherits="Log" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">

    <asp:Repeater ID="rptLog" runat="server"  OnItemCommand="rptLog_ItemCommand" >
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
                    <td><asp:LinkButton ID="lbtEditor" runat="server" Text="编辑" CommandName="editor" CommandArgument='<%#Eval("logid") %>' ></asp:LinkButton></td>
                    <td><asp:LinkButton ID="lbtDelete" runat="server" Text="删除" CommandName="delete" CommandArgument='<%#Eval("logid") %>' ></asp:LinkButton></td>
                    <br />
                </tr>
            </ItemTemplate>
        </asp:Repeater>

    <asp:Button ID="btnWrite" runat="server" Text="写日志" OnClick="btnWrite_Click" />

    </asp:Content>
