using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Friendcenter : System.Web.UI.Page
{
    public static Friends friend;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null || Session["friend"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            if (!IsPostBack)
            {
                Friends friend = (Friends)Session["friend"];
                Users user = (Users)Session["user"];
                if (user.Userid == friend.Friendid)
                {
                    Response.Redirect("Mycenter.aspx");
                }
                else
                {
                    string sql = "select * from UsersSaying where userid='" + friend.Friendid + "' order by publishtime desc";  //查询出该好友的所有说说
                    DataTable dt = DataClass.DataT(sql);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string allow = dt.Rows[i][7].ToString();  //获取当前说说的访问权限对应的字符串
                        int sayingid = Convert.ToInt32(dt.Rows[i][3].ToString());  //获取当前说说的id
                        DataRow dr = dt.Rows[i];  //获取当前行
                        if (allow == "self")  //若权限设置为仅自己可见
                        {
                            dt.Rows.Remove(dr);  //从数据表中删除该行

                        }
                        else if (allow == "friends")  //若权限设置为好友可见
                        {
                            string checksql = "select * from Friend where (friendid='" + friend.Friendid + "' and userid='" + user.Userid + "') or (friendid='" + user.Userid + "' and userid='" + friend.Friendid + "')";  //查询当前用户是否为被访问用户的好友
                            int result = DataClass.Select(checksql);
                            if (result == 0)  //若当前用户不是被访问用户的好友
                            {
                                dt.Rows.Remove(dr);  //从数据表中删除该行
                            }
                        }
                        else if (allow == "defined")  //若权限设置为自定义
                        {
                            string checksql = "select * from SayingAllow where sayingid='" + sayingid + "' and friendid='" + user.Userid + "'";  //查询说说权限表当前说说对当前用户是否可见
                            int result = DataClass.Select(checksql);
                            if (result == 0)  //若当前说说对当前用户不可见
                            {
                                dt.Rows.Remove(dr);  //从数据表中删除该行
                            }
                        }
                    }
                    RptSaying.DataSource = dt;  //repeater绑定数据源
                    RptSaying.DataBind();

                    foreach (RepeaterItem item in this.RptSaying.Items)  //遍历repeater
                    {
                        LinkButton lbtPraise = item.FindControl("lbtPraise") as LinkButton;
                        int i = item.ItemIndex;
                        int sayingid = Convert.ToInt32(dt.Rows[i][3].ToString());
                        string checksql = "select * from Praise where userid='" + user.Userid + "' and sayingid='" + sayingid + "'";  //查询当前用户是否已给当前说说点过赞
                        int result = DataClass.Select(checksql);
                        if (result == 1)
                            lbtPraise.Text = "已赞";
                        else
                            lbtPraise.Text = "赞";
                    }
                }

                DateTime time = DateTime.Now;
                string selectsql = "select top 1 * from Visitor where hostid='" + friend.Friendid + "' and userid='" + user.Userid + "' order by time desc";  //查询出当前用户在该好友访客表中时间最近的一条数据
                int exist = DataClass.Select(selectsql);  //查询当前用户是否访问过该好友
                if (exist == 0)  //若未访问过
                {
                    string insertsql = "insert into Visitor values('" + friend.Friendid + "','" + user.Userid + "','" + time + "')";  //将访问记录存入访客表
                    DataClass.Save(insertsql);
                }
                else  //若已访问过
                {
                    string t1 = time.ToShortDateString().ToString(); //获取当前日期
                    string t2 = DateTime.Parse(DataClass.DataT(selectsql).Rows[0][3].ToString()).ToShortDateString().ToString();  //获取最新一条访问记录的日期
                    if (t2 != t1)  //若当日未访问过
                    {
                        string insertsql = "insert into Visitor values('" + friend.Friendid + "','" + user.Userid + "','" + time + "')";  //将访问记录存入访客表
                        DataClass.Save(insertsql);
                    }
                }
            }
        }      
    }

    protected void RptSaying_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drvw = (DataRowView)e.Item.DataItem;
            int sayingid = Convert.ToInt32(drvw["sayingid"]);
            string sql = "select * from UsersComment where sayingid='" + sayingid + "' order by publishtime desc";  //查询出当前说说下的所有评论
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
            string sql = "select * from UsersReply where commentid='" + commentid + "' order by publishtime";  //查询出当前评论下的所有回复
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
        Friends friend = (Friends)Session["friend"];
        if (e.CommandName == "reply")
        {
            TextBox txtComment = e.Item.FindControl("txtComment") as TextBox;  //获取当前行的TextBox
            string sayingid = e.CommandArgument.ToString();
            string comment = txtComment.Text;
            if(comment.Length == 0)
                Response.Write("<script>alert('输入不能为空！')</script>");
            else
            {
                int userid = user.Userid;
                int friendid = friend.Friendid;
                int result = DataClass.CheckAbleToComment(userid,friendid);
                if(result == 0)
                    Response.Write("<script>alert('您没有权限评论该用户！')</script>");
                else
                {
                    string nickname = user.Nickname;
                    string path = user.urlHeadPhoto;
                    DateTime time = DateTime.Now;
                    string sql = "insert into Sayingcomment values('" + sayingid + "','" + userid + "',N'" + nickname + "','" + path + "',N'" + comment + "','" + time + "')";
                    DataClass.Save(sql);
                    Response.Write("<script>alert('回复成功！');location='Friendcenter.aspx'</script>");
                }                
            }           
        }
        if (e.CommandName == "visit")
        {
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            friend = new Friends(friendid);
            Session["friend"] = friend;
            Response.Redirect("Friendcenter.aspx");
        }
        if (e.CommandName == "spread")
        {           
            int sayingid = Convert.ToInt32(e.CommandArgument.ToString());
            DateTime time = DateTime.Now;  //获取当前时间
            string selectsql = "select * from Saying where sayingid='" + sayingid + "'";
            string content = DataClass.DataT(selectsql).Rows[0][2].ToString();  //根据说说id查询出说说内容
            string sql = "insert into Saying values('" + user.Userid + "',N'" + content + "','" + time + "','0','all')";
            DataClass.DataT(sql);
            Response.Write("<script>alert('转发成功！');location='Friendcenter.aspx'</script>");
        }
        if (e.CommandName == "praise")
        {
            int sayingid = Convert.ToInt32(e.CommandArgument.ToString());
            string checksql = "select * from Praise where userid='" + user.Userid + "' and sayingid='" + sayingid + "'";  //查询当前用户是否已给当前说说点过赞
            int result = DataClass.Select(checksql); 
            if (result == 0)  //若未点过赞
            {
                string updatesql = "update Saying set Praise=Praise+1 where sayingid='" + sayingid + "'";  //更新该条说说的点赞数+1
                DataClass.Save(updatesql);
                string insertsql = "insert into Praise values('" + sayingid + "','" + user.Userid + "')";  //将用户id和说说id存入点赞表
                DataClass.Save(insertsql);
                LinkButton lbtPraise = (LinkButton)e.Item.FindControl("lbtPraise");  //获取对应的LinkButton
                lbtPraise.Text = "已赞";  //内容显示为已赞
                Response.Redirect("Friendcenter.aspx");
            }
            else  //若已点过赞
            {
                string updatesql = "update Saying set Praise=Praise-1 where sayingid='" + sayingid + "'";  //更新该条说说的点赞数-1
                DataClass.Save(updatesql);
                string deletesql = "delete from Praise where userid='" + user.Userid + "' and sayingid='" + sayingid + "'";  //将用户id和说说id从点赞表删除
                DataClass.Save(deletesql);
                LinkButton lbtPraise = (LinkButton)e.Item.FindControl("lbtPraise");  //获取对应的LinkButton
                lbtPraise.Text = "赞";  //内容显示为赞
                Response.Redirect("Friendcenter.aspx");
            }
        }
    }

    protected void RptComment_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "reply")
        {
            e.Item.FindControl("txtReplycomment").Visible = true;  //回复文本框可见
            e.Item.FindControl("lbtPublish").Visible = true;  //发表按键可见
            e.Item.FindControl("lbtReturn").Visible = true;  //取消按键可见
        }

        if (e.CommandName == "publish")
        {
            TextBox txtReplycomment = (TextBox)e.Item.FindControl("txtReplycomment");  //获取当前行文本框
            int commentid = Convert.ToInt32(e.CommandArgument.ToString());
            string reply = txtReplycomment.Text;
            if(reply.Length == 0)
                Response.Write("<script>alert('输入不能为空！')</script>");
            else
            {
                Users user = (Users)Session["user"];
                Friends friend = (Friends)Session["friend"];
                int friendid = friend.Friendid;
                int userid = user.Userid;
                int result = DataClass.CheckAbleToComment(userid,friendid);  //查询是否有评论权限
                if (result == 0)
                    Response.Write("<script>alert('您没有权限评论该用户！')</script>");
                else
                {
                    DateTime time = DateTime.Now;
                    string idsql = "select userid from Sayingcomment where commentid='" + commentid + "'";
                    int authorid = Convert.ToInt32(DataClass.DataT(idsql).Rows[0][0].ToString());
                    string sql = "insert into Commentreply values('" + commentid + "','" + authorid + "','" + user.Userid + "',N'" + reply + "','" + time + "')";
                    DataClass.Save(sql);
                    Response.Write("<script>alert('回复成功！');location='Friendcenter.aspx'</script>");
                }                
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
}