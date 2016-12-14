using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Saying : System.Web.UI.Page
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
                Users user = (Users)Session["user"];
                int userid = user.Userid;
                string sql = "select * from UsersSaying where userid='" + userid + "' order by publishtime desc";
                DataTable dt = DataClass.DataT(sql);
                RptSaying.DataSource = dt;
                RptSaying.DataBind();
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
            string sql = "insert into Saying values('" + userid + "',N'" + content + "','" + time + "')";
            DataClass.Save(sql);
            Response.Write("<script>alert('发表成功！');location='Saying.aspx'</script>");
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
                Response.Write("<script>alert('回复成功！');location='Saying.aspx'</script>");
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
            DateTime time = DateTime.Now;
            string selectsql = "select * from Saying where sayingid='" + sayingid + "'";
            string content = DataClass.DataT(selectsql).Rows[0][2].ToString();
            string sql = "insert into Saying values('" + user.Userid + "',N'" + content + "','" + time + "','0','all')";
            DataClass.DataT(sql);
            Response.Write("<script>alert('转发成功！');location='Saying.aspx'</script>");
        }
        if (e.CommandName == "delete")
        {
            int sayingid = Convert.ToInt32(e.CommandArgument.ToString());
            string sql = "delete from Saying where sayingid='" + sayingid + "'";
            DataClass.DataT(sql);
            string allowsql = "delete from SayingAllow where sayingid='" + sayingid + "'";
            DataClass.DataT(allowsql);
            Response.Write("<script>alert('删除成功！');location='Saying.aspx'</script>");
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
            if (reply.Length == 0)
                Response.Write("<script>alert('输入不能为空！')</script>");
            else
            {
                Users user = (Users)Session["user"];
                DateTime time = DateTime.Now;
                string idsql = "select userid from Sayingcomment where commentid='" + commentid + "'";
                int authorid = Convert.ToInt32(DataClass.DataT(idsql).Rows[0][0].ToString());
                string sql = "insert into Commentreply values('" + commentid + "','" + authorid + "','" + user.Userid + "',N'" + reply + "','" + time + "')";
                DataClass.Save(sql);
                Response.Write("<script>alert('回复成功！');location='Saying.aspx'</script>");
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