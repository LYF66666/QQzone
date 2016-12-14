using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Friend : System.Web.UI.Page
{
    public static Friends friend;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
    }

    protected void rbtNumber_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void rbtNickname_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        string text = txtSearch.Text;
        string sql = "select * from Users";
        if(text.Length==0)
            Response.Write("<script>alert('输入不能为空！');location='Friend.aspx'</script>");
        else
        {
            if (rbtNumber.Checked == true)
                sql = "select * from Users where usernumber='" + text + "' and userid!='" + user.Userid + "'";
            else if(rbtNickname.Checked == true)
                sql= "select * from Users where nickname like N'%" + text + "%' and userid!='" + user.Userid + "'";
            else
                Response.Write("<script>alert('请选择搜索条件！');location='Friend.aspx'</script>");
        }
        int result = DataClass.Select(sql);
        if (result == 0)
            Response.Write("<script>alert('搜索无结果！')</script>");
        else
        {
            DataTable dt = new DataTable();
            dt = DataClass.DataT(sql);
            this.rptFriend.DataSource = dt;
            this.rptFriend.DataBind();
        }
        divSearch.Visible = true;
        divList.Visible = false;
        divRequest.Visible = false;
        divRecommend.Visible = false;
    }

    protected void btnList_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        string sql = "select * from Users where userid in (select friendid from Friend where userid='" + user.Userid + "' union select userid from Friend where friendid='" + user.Userid + "')";
        DataTable dt = DataClass.DataT(sql);
        this.rptList.DataSource = dt;
        this.rptList.DataBind();
        divList.Visible = true;
        divSearch.Visible = false;
        divRequest.Visible = false;
        divRecommend.Visible = false;
    }

    protected void btnRequest_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        string sql = "select * from FriendRequest where friendid='" + user.Userid + "'";
        DataTable dt = DataClass.DataT(sql);
        this.rptRequest.DataSource = dt;
        this.rptRequest.DataBind();
        divRequest.Visible = true;
        divSearch.Visible = false;
        divList.Visible = false;
        divRecommend.Visible = false;
    }

    protected void btnRecommend_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        DataTable countdt = new DataTable();  //新建数据表countdt
        countdt.Columns.Add("userid");
        countdt.Columns.Add("headimagepath");
        countdt.Columns.Add("usernumber");
        countdt.Columns.Add("nickname");
        countdt.Columns.Add("sex");
        countdt.Columns.Add("number");
        string allsql = "select * from Users where userid <> '" + user.Userid + "'";  //遍历用户表除当前用户外的所有用户
        DataTable alldt = DataClass.DataT(allsql);  //得到除当前用户外所有用户信息组成的数据集
        for(int i=0;i<alldt.Rows.Count;i++)
        {
            int id = Convert.ToInt32(alldt.Rows[i][0].ToString());  //获取数据集中第i+1位用户的用户id
            string friendsql = "select friendid from Friend where userid='" + id + "' union select userid from Friend where friendid='" + id + "'";  //查出该用户的所有好友
            DataTable frienddt = DataClass.DataT(friendsql);  //获得该用户所有好友信息组成的数据集
            int n=0;  //初始化共同好友数量n=0
            for(int j=0;j<frienddt.Rows.Count;j++)
            {
                int friendid= Convert.ToInt32(frienddt.Rows[j][0].ToString());  //获取该用户第j+1位好友的用户id
                string selectsql = "select * from Friend where (userid='" + user.Userid + "' and friendid='" + friendid + "') or (userid='" + friendid + "' and friendid='" + user.Userid + "')";  //查询该好友是否也为当前用户的好友
                int result = DataClass.Select(selectsql);
                if (result == 1)  //若为共同好友则n自增1
                    n++;
            }
            if(n>0)  //若存在共同好友则将该用户信息添加入数据表countdt中
            {
                string usernumber = alldt.Rows[i][1].ToString();  //获取该用户账号
                Users u = new Users(usernumber);  //实例化该用户
                DataRow dr = countdt.NewRow();  //创建一个新的行
                dr = countdt.NewRow();
                dr["userid"] = id;  //将该用户信息填充入该行
                dr["headimagepath"] = u.urlHeadPhoto;
                dr["usernumber"] = u.Usernumber;
                dr["nickname"] = u.Nickname;
                dr["sex"] = u.Sex;
                dr["number"] = n;
                countdt.Rows.Add(dr);  //将该行添加入数据表
            }
        }
        this.rptRecommend.DataSource = countdt;  //将数据库表countdt绑定repeater
        this.rptRecommend.DataBind();
        divRequest.Visible = false;
        divSearch.Visible = false;
        divList.Visible = false;
        divRecommend.Visible = true;
    }

    protected void rptFriend_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "visit")
        {
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            friend = new Friends(friendid);
            Session["friend"] = friend;
            Response.Redirect("Friendcenter.aspx");
        }

        if (e.CommandName == "add")
        {
            Users user = (Users)Session["user"];
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            string selectsql = "select * from Friend where (userid='" + user.Userid + "' and friendid='" + friendid + "') or (userid='" + friendid + "' and friendid='" + user.Userid + "')";  //查询该用户是否已为当前用户的好友
            int result = DataClass.Select(selectsql);
            if(result==1)
                Response.Write("<script>alert('您已添加过该好友，不可重复添加！')</script>");
            else
            {
                DateTime time = DateTime.Now;
                string sql = "insert into Request values('" + user.Userid + "','" + friendid + "','"+time+"')";
                DataClass.Save(sql);
                Response.Write("<script>alert('请求发送成功！');location='Friend.aspx'</script>");
            }
        }
    }

    protected void rptRequest_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "visit")
        {
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            friend = new Friends(friendid);
            Session["friend"] = friend;
            Response.Redirect("Friendcenter.aspx");
        }

        if (e.CommandName == "agree")
        {
            Users user = (Users)Session["user"];
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            DateTime time = DateTime.Now;
            string insertsql = "insert into Friend values('" + user.Userid + "','" + friendid + "','"+time+"','yes')";  //将好友id与用户id存入好友表
            DataClass.Save(insertsql);
            string deletesql = "delete from Request where friendid='" + user.Userid + "' and userid='" + friendid + "'";  //将该条好友请求从好友请求表删除
            DataClass.Save(deletesql);
            Response.Write("<script>alert('添加成功！');location='Friend.aspx'</script>");
        }
    }

    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "visit")
        {
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            friend = new Friends(friendid);
            Session["friend"] = friend;
            Response.Redirect("Friendcenter.aspx");
        }

        if (e.CommandName == "delete")
        {
            Users user = (Users)Session["user"];
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            string sql = "delete from Friend where (userid='" + user.Userid + "' and friendid='" + friendid + "') or (userid='" + friendid + "' and friendid='" + user.Userid + "')";
            DataClass.Save(sql);
            Response.Write("<script>alert('删除成功！');location='Friend.aspx'</script>");
        }
    }

    protected void rptRecommend_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "visit")
        {
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            friend = new Friends(friendid);
            Session["friend"] = friend;
            Response.Redirect("Friendcenter.aspx");
        }

        if (e.CommandName == "add")
        {
            Users user = (Users)Session["user"];
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            string selectsql = "select * from Friend where (userid='" + user.Userid + "' and friendid='" + friendid + "') or (userid='" + friendid + "' and friendid='" + user.Userid + "')";  //查询该用户是否已为当前用户的好友
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
    }
}