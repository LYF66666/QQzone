using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            Users user = (Users)Session["user"];
            string path = user.urlHeadPhoto;
            this.ImageButton.ImageUrl = path;
            lbnickname.Text = user.Nickname;
            lbzonename.Text = user.Zonename;
        }
        else
            Response.Redirect("Login.aspx");
    }
    protected void btnLog_Click(object sender, EventArgs e)
    {
        Response.Redirect("Log.aspx");
    }

    protected void btnPhotos_Click(object sender, EventArgs e)
    {
        Response.Redirect("Album.aspx");
    }

    protected void btnMessage_Click(object sender, EventArgs e)
    {
        Response.Redirect("Message.aspx");
    }

    protected void btnShuoShuo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Saying.aspx");
    }

    protected void btnInformation_Click(object sender, EventArgs e)
    {
        Response.Redirect("Information.aspx");
    }

    protected void ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        myFileUpload.Visible = true;
        btnUpload.Visible = true;
        btnreturn.Visible = true;
    }

    protected void btnMycenter_Click(object sender, EventArgs e)
    {
        Response.Redirect("MyCenter.aspx");
    }

    protected void btnSetup_Click(object sender, EventArgs e)
    {
        Response.Redirect("Setup.aspx");
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("Login.aspx");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        int userid = user.Userid;
        string FullName = myFileUpload.PostedFile.FileName;//获取图片物理地址
        if (FullName == "")
        {
            this.lbImage.Visible = true;
            this.lbImage.Text = "请选择图片";
        }
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
                this.ImageButton.ImageUrl = "~\\image" + "\\" + name;//界面显示图片
                string sql = "update Users set headimagename='" + name + "',headimagepath='image/" + name + "' where userid='" + userid + "'";
                DataClass.Save(sql);
                myFileUpload.Visible = false;
                lbImage.Visible = false;
                btnUpload.Visible = false;
                btnreturn.Visible = false;
                Response.AddHeader("Refresh", "0");
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

    protected void btnGoback_Click(object sender, EventArgs e)
    {
        Response.Write("<script language=javascript>history.go(-1);</script>");
    }

    protected void btnFriend_Click(object sender, EventArgs e)
    {
        Response.Redirect("Friend.aspx");
    }

    protected void btnVisitor_Click(object sender, EventArgs e)
    {
        Response.Redirect("Visitor.aspx");
    }
}
