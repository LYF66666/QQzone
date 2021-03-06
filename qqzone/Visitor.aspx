﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visitor.aspx.cs" Inherits="Visitor" MasterPageFile="MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">  

    历史总访客：<asp:Label ID="lbAll" runat="server" ></asp:Label>
    <br />
    今日访客：<asp:Label ID="lbToday" runat="server"></asp:Label>
    <br />

    <asp:Repeater ID="rptVisitor" runat="server" OnItemCommand="rptVisitor_ItemCommand" >
            <ItemTemplate>
                    <td><asp:Image ID="imgFriendHead" runat="server" Height="30px" Width="30px" ImageUrl='<%# Eval("headimagepath") %>' /></td>
                    <td><%# Eval("usernumber")%></td>
                    <td><asp:LinkButton ID="lbtFriend" runat="server" Text='<%#Eval("nickname") %>' CommandName="visit" CommandArgument='<%#Eval("userid") %>' ></asp:LinkButton></td>
                    <td><asp:Label ID="lbTime" runat="server" Text='<%#Eval("time") %>'></asp:Label></td>
                    <br />
                </tr>
            </ItemTemplate>
        </asp:Repeater>

    </asp:Content>
