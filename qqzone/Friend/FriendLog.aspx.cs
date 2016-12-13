using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FriendLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null || Session["friend"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            if (!IsPostBack)
            {
                Friends friend = (Friends)Session["friend"];
                string sql = "select * from Log where userid='" + friend.Friendid + "' order by publishtime desc";  //查询该好友的所有日志按时间顺序排列
                DataTable dt = DataClass.DataT(sql);
                rptLog.DataSource = dt;
                rptLog.DataBind();
            }
        }        
    }

    protected void rptLog_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "headline")
        {
            int logid = Convert.ToInt32(e.CommandArgument.ToString());
            Session["logid"] = logid;
            Response.Redirect("Logcontent.aspx");
        }
    }
}