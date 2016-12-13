using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Album : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            this.ibtAddAlbum.ImageUrl = "image/add.png";
            if (!IsPostBack)
            {
                DataBindToRepeater(1);
            }
        }        
    }

    void DataBindToRepeater(int currentPage)
    {
        Users user = (Users)Session["user"];
        string sql = "select * from Album where userid='" + user.Userid + "' order by creattime desc";
        DataTable dt = new DataTable();
        dt = DataClass.DataT(sql);
        rptAlbum.DataSource = dt;
        rptAlbum.DataBind();
    }

    protected void rptAlbum_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "photo")
        {
            int albumid = Convert.ToInt32(e.CommandArgument.ToString());
            Session["albumid"] = albumid;
            Response.Redirect("Photos.aspx");           
        }
        if (e.CommandName == "delete")
        {
            int albumid = Convert.ToInt32(e.CommandArgument.ToString());
            string sql = "delete from Album where albumid='" + albumid + "'";
            DataClass.Save(sql);
            Response.Write("<script>alert('删除成功！');location='Album.aspx'</script>");
        }
    }

    protected void ibtAddAlbum_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AddAlbum.aspx");
    }
}