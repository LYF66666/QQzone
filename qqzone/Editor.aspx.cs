using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Editor : System.Web.UI.Page
{
    public static Users user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            if (!IsPostBack)
            {
                Users user = (Users)Session["user"];
                txtzonename.Text = user.Zonename;
                txtnickname.Text = user.Nickname;
                txtbirthday.Text = user.Birthday;
                txtphonenumber.Text = user.Phonenumber;
                txtaddress.Text = user.Address;
            }
        }        
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        string zonename = txtzonename.Text;
        string nickname = txtnickname.Text;
        string birthday = txtbirthday.Text;
        string phonenumber = txtphonenumber.Text;
        string address = txtaddress.Text;
        if (zonename.Length == 0 || nickname.Length == 0 || birthday.Length == 0 || phonenumber.Length == 0 || address.Length == 0 )
            Response.Write("<script>alert('输入不能为空！')</script>");
        else if (rbMan.Checked == false && rbWoman.Checked == false)
            Response.Write("<script>alert('请选择性别！')</script>");
        else
        {
            string sex = "0";
            if (rbMan.Checked)
                sex = "男";
            if (rbWoman.Checked)
                sex = "女";
            string sql = "update Users set nickname=N'" + nickname + "',zonename=N'" + zonename + "',sex=N'" + sex + "',birthday=N'" + birthday + "',phonenumber='" + phonenumber + "',address=N'" + address + "' where userid='" + user.Userid + "'";
            DataClass.Save(sql);
            user = new Users(user.Usernumber);
            Session["user"] = user;
            Response.Write("<script>alert('修改成功！');location='Information.aspx'</script>");
        }
    }
}