using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Log : System.Web.UI.Page
{
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
                string sql = "select * from Log where userid='" + userid + "' order by publishtime desc";
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

        if (e.CommandName == "delete")
        {
            int logid = Convert.ToInt32(e.CommandArgument.ToString());
            string sql = "delete from Log where logid='" + logid + "'";
            DataClass.Save(sql);
            string sqlcomment = "delete from Logcomment where logid='" + logid + "'";
            DataClass.Save(sqlcomment);
            Response.Write("<script>alert('删除成功！');location='Log.aspx'</script>");
        }

        if (e.CommandName == "editor")
        {
            int logid = Convert.ToInt32(e.CommandArgument.ToString());
            Session["logid"] = logid;
            Response.Redirect("LogEditor.aspx");
        }
    }

    protected void btnWrite_Click(object sender, EventArgs e)
    {
        Response.Redirect("WriteLog.aspx");
    }
}