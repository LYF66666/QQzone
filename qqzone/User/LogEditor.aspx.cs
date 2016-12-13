using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogEditor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            if (!IsPostBack)
            {
                int logid = Convert.ToInt32(Session["logid"].ToString());
                Logs log = new Logs(logid);
                txtHeadline.Text = log.Headline;
                txtContent.Text = log.Content;
                lbTime.Text = log.Time;
            }
        }        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int logid = Convert.ToInt32(Session["logid"].ToString());
        string headline = txtHeadline.Text;
        string content = txtContent.Text;
        if (headline.Length == 0 || content.Length == 0)
            Response.Write("<script>alert('输入不能为空！')</script>");
        else
        {
            string sql = "update Log set headline=N'" + headline + "',logcontent=N'" + content + "' where logid='" + logid + "'";
            DataClass.Save(sql);
            Response.Write("<script>alert('修改成功！');location='Log.aspx'</script>");
        }        
    }
}