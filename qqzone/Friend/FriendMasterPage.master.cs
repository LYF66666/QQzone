using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FriendMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null && Session["friend"] != null)
        {
            Friends friend = (Friends)Session["friend"];
            Users user = (Users)Session["user"];
            string path = friend.urlHeadPhoto;
            this.Image.ImageUrl = path;
            lbnickname.Text = friend.Nickname;
            lbzonename.Text = friend.Zonename;
            string checksql = "select * from Users where userid='" + friend.Friendid + "'";
            string zonevisible = DataClass.DataT(checksql).Rows[0][11].ToString();
            string sql = "select * from Friend where (userid='" + user.Userid + "' and friendid='" + friend.Friendid + "') or (userid='" + friend.Friendid + "' and friendid='" + user.Userid + "') ";  //查询当前用户是否为该用户的好友
            if (DataClass.Select(sql) == 1)  //若为好友
            {
                lbAdd.Visible = true;
                btnAdd.Visible = false;
                if (zonevisible == "self")  //若空间访问权限设置为仅自己可见
                    Response.Write("<script>alert('您没有访问权限！');location='Mycenter.aspx'</script>");
            }
            else if (zonevisible != "all")  //若不为好友，判断空间访问权限是否为所有人可见
                Response.Write("<script>alert('您没有访问权限！');location='Mycenter.aspx'</script>");
        }
        else
            Response.Redirect("Login.aspx");
    }
    
    protected void btnLog_Click(object sender, EventArgs e)
    {
        Response.Redirect("FriendLog.aspx");
    }

    protected void btnPhotos_Click(object sender, EventArgs e)
    {
        Response.Redirect("FriendAlbum.aspx");
    }

    protected void btnMessage_Click(object sender, EventArgs e)
    {
        Response.Redirect("FriendMessage.aspx");
    }

    protected void btnShuoShuo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Friendcenter.aspx");
    }

    protected void btnInformation_Click(object sender, EventArgs e)
    {
        Response.Redirect("FriendInformation.aspx");
    }

    protected void btnMycenter_Click(object sender, EventArgs e)
    {
        Response.Redirect("MyCenter.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        Friends friend = (Friends)Session["friend"];
        int friendid = friend.Friendid;
        string selectsql = "select * from Friend where (userid='" + user.Userid + "' and friendid='" + friendid + "') or (userid='" + friendid + "' and friendid='" + user.Userid + "')";
        int result = DataClass.Select(selectsql);
        if (result == 1)
            Response.Write("<script>alert('您已添加过该好友，不可重复添加！')</script>");
        else
        {
            DateTime time = DateTime.Now;
            string sql = "insert into Request values('" + user.Userid + "','" + friendid + "','" + time + "')";
            DataClass.Save(sql);
            Response.Write("<script>alert('请求发送成功！');location='Friend.aspx'</script>");
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Response.Redirect("Login.aspx");
    }

    protected void btnVisitor_Click(object sender, EventArgs e)
    {
        Response.Redirect("FriendVisitor.aspx");
    }
}