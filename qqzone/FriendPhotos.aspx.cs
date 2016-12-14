using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FriendPhotos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null || Session["friend"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            if (!IsPostBack)
            {
                DataBindToRepeater(1);
                int albumid = Convert.ToInt32(Session["albumid"].ToString());
                string sql = "select * from UsersAlbum where albumid='" + albumid + "'";
                DataTable dt = DataClass.DataT(sql);
                RptComment.DataSource = dt;
                RptComment.DataBind();
            }
        }        
    }

    void DataBindToRepeater(int currentPage)
    {
        int albumid = Convert.ToInt32(Session["albumid"].ToString());
        string sql = "select * from Photos where albumid='" + albumid + "' order by publishtime desc";
        DataTable dt = new DataTable();
        dt = DataClass.DataT(sql);
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.PageSize = 5;
        pds.DataSource = dt.DefaultView;
        lbTotal.Text = pds.PageCount.ToString();
        pds.CurrentPageIndex = currentPage - 1;
        rptPhoto.DataSource = pds;
        rptPhoto.DataBind();
    }

    protected void btnUp_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbNow.Text) - 1 < 1)
            lbNow.Text = "1";
        else
            lbNow.Text = Convert.ToString(Convert.ToInt32(lbNow.Text) - 1);
        DataBindToRepeater(Convert.ToInt32(lbNow.Text));
    }

    protected void btnDrow_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbNow.Text) + 1 > Convert.ToInt32(lbTotal.Text))
            lbNow.Text = Convert.ToString(Convert.ToInt32(lbTotal.Text));
        else
            lbNow.Text = Convert.ToString(Convert.ToInt32(lbNow.Text) + 1);
        DataBindToRepeater(Convert.ToInt32(lbNow.Text));
    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {
        lbNow.Text = "1";
        DataBindToRepeater(Convert.ToInt32(lbNow.Text));
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        lbNow.Text = Convert.ToString(Convert.ToInt32(lbTotal.Text));
        DataBindToRepeater(Convert.ToInt32(lbNow.Text));
    }

    protected void btnJump_Click(object sender, EventArgs e)
    {
        int i = 0;
        if (int.TryParse(txtJump.Text, out i))
        {
            if (Convert.ToInt32(txtJump.Text) < 1)
                lbNow.Text = "1";
            else if (Convert.ToInt32(txtJump.Text) > Convert.ToInt32(lbTotal.Text))
                lbNow.Text = Convert.ToString(Convert.ToInt32(lbTotal.Text));
            else
                lbNow.Text = Convert.ToString(Convert.ToInt32(txtJump.Text));
        }
        else
            lbNow.Text = Convert.ToString(Convert.ToInt32(lbTotal.Text));
        DataBindToRepeater(Convert.ToInt32(lbNow.Text));
    }

    protected void btnReply_Click(object sender, EventArgs e)
    {
        string content = txtComment.Text;
        if (content.Length == 0)
            Response.Write("<script>alert('输入不能为空！')</script>");
        else
        {
            Users user = (Users)Session["user"];
            Friends friend = (Friends)Session["friend"];
            int userid = user.Userid;
            int friendid = friend.Friendid;
            int result = DataClass.CheckAbleToComment(userid, friendid);
            if (result == 0)
                Response.Write("<script>alert('您没有权限评论该用户！')</script>");
            else
            {
                int albumid = Convert.ToInt32(Session["albumid"].ToString());
                DateTime time = DateTime.Now;
                string sql = "insert into Albumcomment values('" + albumid + "','" + user.Userid + "',N'" + content + "','" + time + "')";
                DataClass.Save(sql);
                Response.Write("<script>alert('回复成功！');location='FriendPhotos.aspx'</script>");
            }            
        }
    }
}