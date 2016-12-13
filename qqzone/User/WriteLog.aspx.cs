using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WriteLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
    }

    protected void btnPublish_Click(object sender, EventArgs e)
    {
        string headline = txtHeadline.Text;
        string content = txtContent.Text;
        if (headline.Length == 0 || content.Length == 0)
            Response.Write("<script>alert('输入不能为空！')</script>");
        else
        {
            Users user = (Users)Session["user"];
            DateTime time = DateTime.Now;
            string sql = "insert into Log values('" + user.Userid + "',N'" + headline + "',N'" + content + "','" + time + "')";
            DataClass.Save(sql);
            Response.Write("<script>alert('发表成功！');location='Log.aspx'</script>");
        }
    }
}