using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Photos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            this.ImageButton.ImageUrl = "image/add.png";
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

    protected void ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        myFileUpload.Visible = true;
        lbImage.Visible = true;
        btnUpload.Visible = true;
        btnreturn.Visible = true;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        int albumid = Convert.ToInt32(Session["albumid"].ToString());
        string FullName = myFileUpload.PostedFile.FileName;//获取图片物理地址
        if (FullName == "")
            this.lbImage.Text = "请选择图片";
        else
        {
            FileInfo fi = new FileInfo(FullName);
            string name = fi.Name;//获取图片名称
            string type = fi.Extension;//获取图片类型
            if (type == ".jpg" || type == ".gif" || type == ".bmp" || type == ".png")
            {
                string SavePath = Server.MapPath("~\\image");//图片保存到文件夹下
                this.myFileUpload.PostedFile.SaveAs(SavePath + "\\" + name);//保存路径
                this.ImageButton.Visible = true;
                DateTime time = DateTime.Now;
                string sql = "insert into Photos values('" + user.Userid + "','" + name + "','image/" + name + "','"+albumid+"','"+time+"')";
                DataClass.Save(sql);
                myFileUpload.Visible = false;
                lbImage.Visible = false;
                btnUpload.Visible = false;
                btnreturn.Visible = false;
                Response.Write("<script>alert('上传成功！');location='Photos.aspx'</script>");
            }
            else
            {
                this.lbImage.Text = "请选择正确的格式图片";
            }
        }
    }

    protected void btnreturn_Click(object sender, EventArgs e)
    {
        myFileUpload.Visible = false;
        lbImage.Visible = false;
        btnUpload.Visible = false;
        btnreturn.Visible = false;
    }

    protected void btnReply_Click(object sender, EventArgs e)
    {
        string content = txtComment.Text;
        if(content.Length == 0)
            Response.Write("<script>alert('输入不能为空！')</script>");
        else
        {
            Users user = (Users)Session["user"];
            int albumid = Convert.ToInt32(Session["albumid"].ToString());
            DateTime time = DateTime.Now;
            string sql = "insert into Albumcomment values('" + albumid + "','" + user.Userid + "',N'" + content + "','" + time + "')";
            DataClass.Save(sql);
            Response.Write("<script>alert('回复成功！');location='Photos.aspx'</script>");
        }        
    }
}