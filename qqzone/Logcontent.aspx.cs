using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logcontent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            int logid = Convert.ToInt32(Session["logid"].ToString());  //获取传递的日志id
            Logs log = new Logs(logid);  //实例化日志
            txtHeadline.Text = log.Headline;
            txtContent.Text = log.Content;
            lbTime.Text = log.Time;
            if (!IsPostBack)
            {
                string sql = "select * from UsersLog where logid='" + logid + "'";
                DataTable dt = DataClass.DataT(sql);
                RptComment.DataSource = dt;
                RptComment.DataBind();
            }
        }        
    }

    protected void btnReply_Click(object sender, EventArgs e)
    {
        string content = txtComment.Text;
        Users user = (Users)Session["user"];
        int logid = Convert.ToInt32(Session["logid"].ToString());  //获取传递的日志id
        Logs log = new Logs(logid);  //实例化日志
        int authorid = log.Userid;
        if(user.Userid != authorid )
        {
            Friends friend = (Friends)Session["friend"];
            int userid = user.Userid;
            int friendid = friend.Friendid;
            int result = DataClass.CheckAbleToComment(userid, friendid);
            if (result == 0)
                Response.Write("<script>alert('您没有权限评论该用户！')</script>");
            else
            {
                DateTime time = DateTime.Now;
                string sql = "insert into Logcomment values('" + logid + "','" + user.Userid + "',N'" + content + "','" + time + "')";
                DataClass.Save(sql);
                Response.Write("<script>alert('回复成功！');location='Logcontent.aspx'</script>");
            }
        }
        else
        {
            DateTime time = DateTime.Now;
            string sql = "insert into Logcomment values('" + logid + "','" + user.Userid + "',N'" + content + "','" + time + "')";
            DataClass.Save(sql);
            Response.Write("<script>alert('回复成功！');location='Logcontent.aspx'</script>");
        }
        
    }
}