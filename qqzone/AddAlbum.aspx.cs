using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddAlbum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {       
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
                this.Image.Visible = true;
                this.Image.ImageUrl = "~\\image" + "\\" + name;//界面显示图片
                Session["photoname"] = name;
            }
            else
            {
                this.lbImage.Visible = true;
                this.lbImage.Text = "请选择正确的格式图片";
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        string name = Session["photoname"].ToString();
        DateTime time = DateTime.Now;
        if(txtName.Text.Length == 0)
            Response.Write("<script>alert('输入不能为空！')</script>");
        else
        {
            string sql = "insert into Album values('" + user.Userid + "',N'" + txtName.Text + "','image/" + name + "','" + time + "')";
            DataClass.Save(sql);
            Response.Write("<script>alert('创建成功！');location='Album.aspx'</script>");
        }        
    }
}