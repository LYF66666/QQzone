using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Setup : System.Web.UI.Page
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
                string listsql = "select * from Users where userid in (select friendid from Friend where userid='" + user.Userid + "' union select userid from Friend where friendid='" + user.Userid + "')";
                DataTable frienddt = DataClass.DataT(listsql);
                this.rptList.DataSource = frienddt;
                this.rptList.DataBind();

                foreach (RepeaterItem item in this.rptList.Items)  //遍历repeater
                {
                    LinkButton lbtUnable = item.FindControl("lbtUnable") as LinkButton;  //获取当前行的LinkButton
                    int i = item.ItemIndex;  //获取当前行的id
                    int friendid = Convert.ToInt32(frienddt.Rows[i][0].ToString());  //获取该行绑定数据对应的说说id
                    string checksql = "select * from Friend where unable='yes' and ((friendid='" + friendid + "' and userid='" + user.Userid + "') or (friendid='" + user.Userid + "' and userid='" + friendid + "'))";
                    int result = DataClass.Select(checksql);  //查询当前好友是否未被屏蔽
                    if (result == 1)  //若未被屏蔽
                        lbtUnable.Text = "屏蔽";
                    else  //若已被屏蔽
                        lbtUnable.Text = "取消屏蔽";
                }
            }
        }         
    }

    protected void btnZoneVisible_Click(object sender, EventArgs e)
    {
        divChoose.Visible = true;
        btnZoneSubmit.Visible = true;
        btnVisitorSubmit.Visible = false;
        btnCommentSubmit.Visible = false;
        divUnable.Visible = false;
    }

    protected void btnVisitorVisible_Click(object sender, EventArgs e)
    {
        divChoose.Visible = true;
        btnZoneSubmit.Visible = false;
        btnVisitorSubmit.Visible = true;
        btnCommentSubmit.Visible = false;
        divUnable.Visible = false;
    }

    protected void btnComment_Click(object sender, EventArgs e)
    {
        divChoose.Visible = true;
        btnZoneSubmit.Visible = false;
        btnVisitorSubmit.Visible = false;
        btnCommentSubmit.Visible = true;
        divUnable.Visible = false;
    }

    protected void btnNoSee_Click(object sender, EventArgs e)
    {
        divChoose.Visible = false;
        btnZoneSubmit.Visible = false;
        btnVisitorSubmit.Visible = false;
        btnCommentSubmit.Visible = false;
        divUnable.Visible = true;
    }

    protected void btnZoneSubmit_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        string visible = "all";
        if (rbAll.Checked == true)
            visible = "all";
        else if (rbFriend.Checked == true)
            visible = "friends";
        else if (rbSelf.Checked == true)
            visible = "self";
        string sql = "update Users set zonevisible='" + visible + "' where userid='" + user.Userid + "'";
        DataClass.Save(sql);
        Response.Write("<script>alert('设置成功！');location='Setup.aspx'</script>");
    }

    protected void rbAll_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void rbFriend_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void rbSelf_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void btnVisitorSubmit_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        string visible = "all";
        if (rbAll.Checked == true)
            visible = "all";
        else if (rbFriend.Checked == true)
            visible = "friends";
        else if (rbSelf.Checked == true)
            visible = "self";
        string sql = "update Users set visitorvisible='" + visible + "' where userid='" + user.Userid + "'";
        DataClass.Save(sql);
        Response.Write("<script>alert('设置成功！');location='Setup.aspx'</script>");
    }

    protected void btnCommentSubmit_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        string able = "all";
        if (rbAll.Checked == true)
            able = "all";
        else if (rbFriend.Checked == true)
            able = "friends";
        else if (rbSelf.Checked == true)
            able = "self";
        string sql = "update Users set commentable='" + able + "' where userid='" + user.Userid + "'";
        DataClass.Save(sql);
        Response.Write("<script>alert('设置成功！');location='Setup.aspx'</script>");
    }

    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "unable")
        {
            Users user = (Users)Session["user"];
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            string checksql = "select * from Friend where unable='yes' and ((friendid='" + friendid + "' and userid='" + user.Userid + "') or (friendid='" + user.Userid + "' and userid='" + friendid + "'))";
            int result = DataClass.Select(checksql);
            if (result == 1)
            {
                string updatesql = "update Friend set unable='no' where ((friendid='" + friendid + "' and userid='" + user.Userid + "') or (friendid='" + user.Userid + "' and userid='" + friendid + "')) ";
                DataClass.Save(updatesql);
                LinkButton lbtUnable = (LinkButton)e.Item.FindControl("lbtUnable");
                lbtUnable.Text = "取消屏蔽";
                Response.Write("<script>alert('设置成功！');location='Setup.aspx'</script>");
            }
            else if(result == 0)
            {
                string updatesql = "update Friend set unable='yes' where ((friendid='" + friendid + "' and userid='" + user.Userid + "') or (friendid='" + user.Userid + "' and userid='" + friendid + "')) ";
                DataClass.Save(updatesql);
                LinkButton lbtUnable = (LinkButton)e.Item.FindControl("lbtUnable");
                lbtUnable.Text = "屏蔽";
                Response.Write("<script>alert('设置成功！');location='Setup.aspx'</script>");
            }
        }

        if (e.CommandName == "visit")
        {
            int friendid = Convert.ToInt32(e.CommandArgument.ToString());
            friend = new Friends(friendid);
            Session["friend"] = friend;
            Response.Redirect("Friendcenter.aspx");
        }
    }
}