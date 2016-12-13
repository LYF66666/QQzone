using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FriendVisitor : System.Web.UI.Page
{
    public static Friends friend;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null || Session["friend"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            Users user = (Users)Session["user"];
            Friends friend = (Friends)Session["friend"];
            if (!IsPostBack)
            {
                int userid = user.Userid;
                int friendid = friend.Friendid;
                int result = DataClass.CheckVisitorVisible(userid, friendid);  //判断当前用户是否有权限查看该好友的访客
                if (result == 0)
                    Response.Write("<script>alert('您没有权限查看该用户的访客！');location='Friendcenter.aspx'</script>");
                else
                {
                    string distinctsql = "select distinct userid from Visitor where hostid='" + friend.Friendid + "'";  //查询出访客中不重复的所有用户id
                    DataTable iddt = DataClass.DataT(distinctsql);
                    DataTable dt = new DataTable();  //新建数据表
                    //添加相应的列
                    dt.Columns.Add("userid");  
                    dt.Columns.Add("usernumber");
                    dt.Columns.Add("nickname");
                    dt.Columns.Add("headimagepath");
                    dt.Columns.Add("hostid");
                    dt.Columns.Add("time");
                    for (int i = 0; i < iddt.Rows.Count; i++)
                    {
                        int id = Convert.ToInt32(iddt.Rows[i][0].ToString());  //获取第i+1位用户的id
                        string sql = "select top 1 * from UsersVisitor where hostid='" + friend.Friendid + "' and userid='" + id + "' order by time desc";  //获取该用户的所有访问记录中时间最新的一条
                        DataRow dr = DataClass.DataT(sql).Rows[0];  //获取当前行
                        dt.Rows.Add(dr.ItemArray);  //将当前行添加入数据表
                    }
                    dt.DefaultView.Sort = "time desc";  //将数据表按时间倒序排列
                    dt = dt.DefaultView.ToTable();
                    rptVisitor.DataSource = dt;  //绑定repeater
                    rptVisitor.DataBind();

                    foreach (RepeaterItem item in this.rptVisitor.Items)  //遍历repeater
                    {
                        LinkButton lbtDelete = item.FindControl("lbtDelete") as LinkButton;  //获取当前行对应LinkButton
                        int i = item.ItemIndex;
                        int ID = Convert.ToInt32(dt.Rows[i][0].ToString());  //获取当前行对应的用户id
                        if (ID == user.Userid)  //若该用户为当前用户
                            lbtDelete.Visible = true;  //删除按键可见
                    }
                }

                string allsql = "select * from Visitor where hostid='" + friend.Friendid + "'";  //查询出该好友的所有访客记录
                DataTable alldt = DataClass.DataT(allsql);
                int all = alldt.Rows.Count;  //总访客量为访客记录总行数
                lbAll.Text = all.ToString();
                int today = 0;
                for (int j = 0; j < alldt.Rows.Count; j++)
                {
                    string t1 = DateTime.Now.ToShortDateString().ToString(); //获取当前日期
                    string t2 = DateTime.Parse(alldt.Rows[j][3].ToString()).ToShortDateString().ToString();  //获取该条访问记录的日期
                    if (t1 == t2)  //若访问日期为今日
                        today++;  //今日访客数量+1
                }
                lbToday.Text = today.ToString();
            }
        }        
    }

    protected void rptVisitor_ItemCommand(object source, RepeaterCommandEventArgs e)
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
            Friends friend = (Friends)Session["friend"];
            int userid = Convert.ToInt32(e.CommandArgument.ToString());
            string sql = "delete from Visitor where hostid='" + friend.Friendid + "' and userid='" + userid + "'";
            DataClass.Save(sql);
            Response.Write("<script>alert('删除成功！');location='FriendVisitor.aspx'</script>");
        }
    }
}