using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mycenter : System.Web.UI.Page
{
    public static Friends friend;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            txtSaying.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            if (!IsPostBack)
            {
                //绑定repeater RptSaying
                Users user = (Users)Session["user"];
                int userid = user.Userid;
                string sql = "select * from UsersSaying where userid in ( select friendid from Friend where userid='" + userid + "' union select userid from Friend where friendid='" + userid + "' union select userid from Users where userid='" + userid + "') order by publishtime desc";  //查询出当前用户所有好友的所有说说
                DataTable alldt = DataClass.DataT(sql);
                //查询出对当前用户不可见的所有说说信息
                for (int i = 0; i < alldt.Rows.Count; i++)
                {
                    string allow = alldt.Rows[i][7].ToString();  //获取当前说说的访问权限对应的字符串
                    int sayingid = Convert.ToInt32(alldt.Rows[i][3].ToString());  //获取当前说说的id
                    int authorid = Convert.ToInt32(alldt.Rows[i][0].ToString());  //获取发表当前说说的用户id
                    if (userid != authorid)
                    {
                        DataRow dr = alldt.Rows[i];  //获取当前行
                        if (allow == "self")  //若权限设置为仅自己可见
                        {
                            alldt.Rows.Remove(dr);  //从数据表中删除该行 
                        }
                        else if (allow == "defined")  //若权限设置为自定义
                        {
                            string checksql = "select * from SayingAllow where sayingid='" + sayingid + "' and friendid='" + user.Userid + "'";  //查询说说权限表当前说说对当前用户是否可见
                            int result = DataClass.Select(checksql);
                            if (result == 0)  //若当前说说对当前用户不可见
                            {
                                alldt.Rows.Remove(dr);  //从数据表中删除该行
                            }
                        }
                    }
                }
                for (int i = 0; i < alldt.Rows.Count; i++)
                {
                    int authorid = Convert.ToInt32(alldt.Rows[i][0].ToString());  //获取发表当前说说的用户id
                    DataRow dr = alldt.Rows[i];  //获取当前行
                    if(authorid != user.Userid)
                    {
                        string unablesql = "select * from Friend where ((userid='" + user.Userid + "' and friendid='" + authorid + "') or (userid='" + authorid + "' and friendid='" + userid + "')) and unable='yes'";
                        int result = DataClass.Select(unablesql);
                        if (result == 0)
                        {
                            alldt.Rows.Remove(dr);  //从数据表中删除该行
                        }
                    }                   
                }
                RptSaying.DataSource = alldt;  //绑定repeater
                RptSaying.DataBind();

                //绑定repeater rptList
                string listsql = "select * from Users where userid in (select friendid from Friend where userid='" + user.Userid + "' union select userid from Friend where friendid='" + user.Userid + "')";
                DataTable frienddt = DataClass.DataT(listsql);
                this.rptList.DataSource = frienddt;
                this.rptList.DataBind();

                foreach (RepeaterItem item in this.RptSaying.Items)  //遍历repeater
                {
                    LinkButton lbtPraise = item.FindControl("lbtPraise") as LinkButton;  //获取当前行的LinkButton
                    int i = item.ItemIndex;  //获取当前行的id
                    int sayingid = Convert.ToInt32(alldt.Rows[i][3].ToString());  //获取该行绑定数据对应的说说id
                    string checksql = "select * from Praise where userid='" + user.Userid + "' and sayingid='" + sayingid + "'";  //查询当前用户是否对该说说点过赞
                    int result = DataClass.Select(checksql);
                    if (result == 1)
                        lbtPraise.Text = "已赞";
                    else
                        lbtPraise.Text = "赞";
                }
            }
        }       
    }

    protected void btnPublish_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        int userid = user.Userid;
        string content = txtSaying.Text;
        if(content.Length == 0)
            Response.Write("<script>alert('输入不能为空！')</script>");
        else
        {
            DateTime time = DateTime.Now;
            string allow="all";
            if (Session["allow"] != null)
                allow = Session["allow"].ToString();
            string sql = "insert into Saying values('" + userid + "',N'" + content + "','" + time + "','0','" + allow + "')";
            DataClass.Save(sql);
            if (allow == "defined")  //当好友权限为自定义时
            {
                string selectsql = "select top 1 * from Saying where userid='" + userid + "' order by publishtime desc";  //查询出当前用户所有说说中最新的一条
                int sayingid = Convert.ToInt32(DataClass.DataT(selectsql).Rows[0][0].ToString());  //获取该条说说的id
                List<int> idlist = (List<int>)Session["idlist"];  //获取传递的动态数组idlist
                for(int i=0;i<idlist.Count;i++)
                {
                    int friendid = idlist[i];  //获取数组中第i+1个好友的id
                    string insertsql = "insert into SayingAllow values('" + sayingid + "','" + friendid + "')";  //将说说id和好友id存入数据库
                    DataClass.Save(insertsql);
                }
            }
            Response.Write("<script>alert('发表成功！');location='Mycenter.aspx'</script>");
        }
    }

    protected void RptSaying_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drvw = (DataRowView)e.Item.DataItem;
            int sayingid = Convert.ToInt32(drvw["sayingid"]);
            string sql = "select * from UsersComment where sayingid='" + sayingid + "' order by publishtime";
            DataTable dt = new DataTable();
            dt = DataClass.DataT(sql);
            Repeater rept = (Repeater)e.Item.FindControl("RptComment");
            // rept = (Repeater)e.Item.FindControl("RptComment");
            rept.DataSource = dt;
            rept.DataBind();
        }
    }

    protected void RptComment_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drvw = (DataRowView)e.Item.DataItem;
            int commentid = Convert.ToInt32(drvw["commentid"]);
            string sql = "select * from UsersReply where commentid='" + commentid + "' order by publishtime";
            DataTable dt = new DataTable();
            dt = DataClass.DataT(sql);
            Repeater rept = (Repeater)e.Item.FindControl("RptReply");
            // rept = (Repeater)e.Item.FindControl("RptComment");
            rept.DataSource = dt;
            rept.DataBind();
        }
    }

    protected void RptSaying_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Users user = (Users)Session["user"];
        if (e.CommandName == "reply")
        {
            TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
            string sayingid = e.CommandArgument.ToString();
            string comment = txtComment.Text;
            if(comment.Length == 0)
                Response.Write("<script>alert('输入不能为空！')</script>");
            else
            {
                int userid = user.Userid;
                DateTime time = DateTime.Now;
                string sql = "insert into Sayingcomment values('" + sayingid + "','" + userid + "',N'" + comment + "','" + time + "')";
                DataClass.Save(sql);
                Response.Write("<script>alert('回复成功！');location='Mycenter.aspx'</script>");
            }           
        }
        if (e.CommandName == "visit")
        {
            int friendid= Convert.ToInt32(e.CommandArgument.ToString());
            friend = new Friends(friendid);
            Session["friend"] = friend;
            Response.Redirect("Friendcenter.aspx");
        }
        if (e.CommandName == "spread")
        {
            int sayingid = Convert.ToInt32(e.CommandArgument.ToString());
            DateTime time = DateTime.Now;
            string selectsql = "select * from Saying where sayingid='" + sayingid + "'";
            string content = DataClass.DataT(selectsql).Rows[0][2].ToString();
            string sql = "insert into Saying values('" + user.Userid + "',N'" + content + "','" + time + "','0','all')";
            DataClass.DataT(sql);
            Response.Write("<script>alert('转发成功！');location='Mycenter.aspx'</script>");
        }
        if (e.CommandName == "praise")
        {
            int sayingid = Convert.ToInt32(e.CommandArgument.ToString());
            string checksql = "select * from Praise where userid='" + user.Userid + "' and sayingid='" + sayingid + "'";
            int result = DataClass.Select(checksql);
            if(result==0)
            {
                string updatesql = "update Saying set Praise=Praise+1 where sayingid='" + sayingid + "'";
                DataClass.Save(updatesql);
                string insertsql = "insert into Praise values('" + sayingid + "','" + user.Userid + "')";
                DataClass.Save(insertsql);
                LinkButton lbtPraise = (LinkButton)e.Item.FindControl("lbtPraise");
                lbtPraise.Text = "已赞";
                Response.Redirect("Mycenter.aspx");
            }
            else
            {
                string updatesql = "update Saying set Praise=Praise-1 where sayingid='" + sayingid + "'";
                DataClass.Save(updatesql);
                string deletesql = "delete from Praise where userid='" + user.Userid + "' and sayingid='" + sayingid + "'";
                DataClass.Save(deletesql);
                LinkButton lbtPraise = (LinkButton)e.Item.FindControl("lbtPraise");
                lbtPraise.Text = "赞";
                Response.Redirect("Mycenter.aspx");
            }
        }
    }

    protected void RptComment_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "reply")
        {
            e.Item.FindControl("txtReplycomment").Visible = true;
            e.Item.FindControl("lbtPublish").Visible = true;
            e.Item.FindControl("lbtReturn").Visible = true;
        }

        if (e.CommandName == "publish")
        {
            TextBox txtReplycomment = (TextBox)e.Item.FindControl("txtReplycomment");
            int commentid = Convert.ToInt32(e.CommandArgument.ToString());
            string reply = txtReplycomment.Text;
            if(reply.Length == 0)
                Response.Write("<script>alert('输入不能为空！')</script>");
            else
            {
                Users user = (Users)Session["user"];
                DateTime time = DateTime.Now;
                string idsql = "select userid from Sayingcomment where commentid='" + commentid + "'";
                int authorid = Convert.ToInt32(DataClass.DataT(idsql).Rows[0][0].ToString());
                string sql = "insert into Commentreply values('" + commentid + "','" + authorid + "','" + user.Userid + "',N'" + reply + "','" + time + "')";
                DataClass.Save(sql);
                Response.Write("<script>alert('回复成功！');location='Mycenter.aspx'</script>");
            }           
        }

        if (e.CommandName == "return")
        {
            e.Item.FindControl("txtReplycomment").Visible = false;
            e.Item.FindControl("lbtPublish").Visible = false;
            e.Item.FindControl("lbtReturn").Visible = false;
        }

        if (e.CommandName == "visit")
        {
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            friend = new Friends(friendid);
            Session["friend"] = friend;
            Response.Redirect("Friendcenter.aspx");
        }
    }

    protected void RptReply_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "replyuser")
        {
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            friend = new Friends(friendid);
            Session["friend"] = friend;
            Response.Redirect("Friendcenter.aspx");
        }
    }

    protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var sIndex = DropDownList.SelectedIndex; 
        string allow = "all";
        if (sIndex == 0)
        {
            allow = "all";
            divList.Visible = false;
        }
        else if (sIndex == 1)
        {
            allow = "friends";
            divList.Visible = false;
        }
        else if (sIndex == 2)
        {
            allow = "self";
            divList.Visible = false;
        }
        else if (sIndex == 3)
        {
            divList.Visible = true;
            allow = "defined";
        }
        Session["allow"] = allow;
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        List<int> idlist = new List<int>();  //新建动态数组idlist
        foreach (RepeaterItem item in this.rptList.Items)  //遍历repeater好友列表
        {
            Users user = (Users)Session["user"];
            CheckBox cbFriend = item.FindControl("cbFriend") as CheckBox;  //获取当前行的CheckBox            
            if (cbFriend.Checked == true)  //当对应复选框被选中时
            {
                int i = item.ItemIndex;  //获取当前行的id
                string selectsql = "select * from Users where userid in (select friendid from Friend where userid='" + user.Userid + "' union select userid from Friend where friendid='" + user.Userid + "')";  //查询出当前用户的所有好友
                DataTable dt = DataClass.DataT(selectsql);  //获得数据表
                int friendid = Convert.ToInt32(dt.Rows[i][0].ToString());  //获取被选中的好友id
                idlist.Add(friendid);  //将该好友id存入动态数组中
            }           
        }
        if(idlist.Count == 0)
            Response.Write("<script>alert('请选择好友！')</script>");
        else
        {
            Session["idlist"] = idlist;  //通过Session传递数组
            divList.Visible = false;
        }        
    }
}