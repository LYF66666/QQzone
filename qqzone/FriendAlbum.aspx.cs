using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FriendAlbum : System.Web.UI.Page
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
                string sql = "select * from Album where userid='" + friend.Friendid + "' order by creattime desc";
                DataTable dt = new DataTable();
                dt = DataClass.DataT(sql);
                rptAlbum.DataSource = dt;
                rptAlbum.DataBind();
            }
        }
    }

    protected void rptAlbum_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "photo")
        {
            int albumid = Convert.ToInt32(e.CommandArgument.ToString());
            Session["albumid"] = albumid;
            Response.Redirect("FriendPhotos.aspx");
        }

        if (e.CommandName == "spread")
        {
            Users user = (Users)Session["user"];
            int albumid = Convert.ToInt32(e.CommandArgument.ToString());
            DateTime time = DateTime.Now;
            string selectsql = "select * from Album where albumid='" + albumid + "'";
            DataTable dt = DataClass.DataT(selectsql);
            string albumname = dt.Rows[0][2].ToString();
            string albumcover = dt.Rows[0][3].ToString();
            string sql = "insert into Album values('" + user.Userid + "',N'" + albumname + "','"+albumcover+"','" + time + "')";  //将该相册信息与用户信息存入数据库
            DataClass.DataT(sql);
            string idsql = "select top 1 * from Album where userid='" + user.Userid + "' order by creattime desc";  //查询出当前用户所有相册中创建时间最新的一个
            int newid = Convert.ToInt32(DataClass.DataT(idsql).Rows[0][0].ToString());  //获取该最新相册的id
            string photosql = "select * from Photos where albumid='" + albumid + "'";  //查询出被转发的相册中的所有相片
            DataTable photodt = DataClass.DataT(photosql);
            for(int i=0;i<photodt.Rows.Count;i++)
            {
                string name = photodt.Rows[i][1].ToString();  //获取第i+1张相片的名字
                string path = photodt.Rows[i][2].ToString();  //获取第i+1张相片的存储路径
                string savesql = "insert into Photos values('" + user.Userid + "','" + name + "','" + path + "','" + newid + "','" + time + "')";  //将相片信息与用户信息存入新相册
                DataClass.Save(savesql);
            }
            Response.Write("<script>alert('转发成功！');location='FriendAlbum.aspx'</script>");
        }
    }
}