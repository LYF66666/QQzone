using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Visitor : System.Web.UI.Page
{
    public static Friends friend;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            if (!IsPostBack)
            {
                Users user = (Users)Session["user"];
                string distinctsql = "select distinct userid from Visitor where hostid='" + user.Userid + "'";
                DataTable iddt = DataClass.DataT(distinctsql);
                DataTable dt = new DataTable();
                dt.Columns.Add("userid");
                dt.Columns.Add("usernumber");
                dt.Columns.Add("nickname");
                dt.Columns.Add("headimagepath");
                dt.Columns.Add("hostid");
                dt.Columns.Add("time");
                for (int i = 0; i < iddt.Rows.Count; i++)
                {
                    int userid = Convert.ToInt32(iddt.Rows[i][0].ToString());
                    string sql = "select top 1 * from UsersVisitor where hostid='" + user.Userid + "' and userid='" + userid + "' order by time desc";
                    DataRow dr = DataClass.DataT(sql).Rows[0];
                    dt.Rows.Add(dr.ItemArray);
                }
                dt.DefaultView.Sort = "time desc";
                dt = dt.DefaultView.ToTable();
                rptVisitor.DataSource = dt;
                rptVisitor.DataBind();

                string allsql = "select * from Visitor where hostid='" + user.Userid + "'";
                DataTable alldt = DataClass.DataT(allsql);
                int all = alldt.Rows.Count;
                lbAll.Text = all.ToString();
                int today = 0;
                for (int j = 0; j < alldt.Rows.Count; j++)
                {
                    int t1 = DateTime.Now.Day;
                    int t2 = DateTime.Parse(alldt.Rows[j][3].ToString()).Day;
                    if (t1 == t2)
                        today++;
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
    }
}