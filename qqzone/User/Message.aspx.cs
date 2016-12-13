using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Message : System.Web.UI.Page
{
    public static Friends friend;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            if (!IsPostBack)
            {
                Users user = (Users)Session["user"];
                int userid = user.Userid;
                string sql = "select * from UsersMessage where hostid='" + userid + "' order by publishtime desc";
                DataTable dt = DataClass.DataT(sql);
                RptMessage.DataSource = dt;
                RptMessage.DataBind();
            }
        }        
    }

    protected void RptMessage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drvw = (DataRowView)e.Item.DataItem;
            int messageid = Convert.ToInt32(drvw["messageid"]);
            string sql = "select * from UsersMessageComment where messageid='" + messageid + "' order by publishtime";
            DataTable dt = new DataTable();
            dt = DataClass.DataT(sql);
            Repeater rept = (Repeater)e.Item.FindControl("RptComment");
            // rept = (Repeater)e.Item.FindControl("RptComment");
            rept.DataSource = dt;
            rept.DataBind();
        }
    }

    protected void RptMessage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Users user = (Users)Session["user"];
        if (e.CommandName == "reply")
        {
            TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
            int messageid = Convert.ToInt32(e.CommandArgument.ToString());
            string comment = txtComment.Text;
            if (comment.Length == 0)
                Response.Write("<script>alert('输入不能为空！')</script>");
            else
            {
                int userid = user.Userid;
                DateTime time = DateTime.Now;
                string sql = "insert into Messagecomment values('" + messageid + "','" + userid + "',N'" + comment + "','" + time + "')";
                DataClass.Save(sql);
                Response.Write("<script>alert('回复成功！');location='Message.aspx'</script>");
            }
        }
        if (e.CommandName == "visit")
        {
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            friend = new Friends(friendid);
            Session["friend"] = friend;
            Response.Redirect("Friendcenter.aspx");
        }
        if (e.CommandName == "delete")
        {
            int messageid = Convert.ToInt32(e.CommandArgument.ToString());
            string sql = "delete from Message where messageid='" + messageid + "'";
            DataClass.Save(sql);
            Response.Write("<script>alert('删除成功！');location='Message.aspx'</script>");
        }
    }

    protected void RptComment_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "visit")
        {
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            friend = new Friends(friendid);
            Session["friend"] = friend;
            Response.Redirect("Friendcenter.aspx");
        }
    }

}