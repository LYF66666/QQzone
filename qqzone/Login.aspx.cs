using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    public static Users user;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void imgbtnValid_Click(object sender, ImageClickEventArgs e)
    {
        this.imgbtnValid.ImageUrl = "~/ValidCode.aspx";
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string usernumber = txtNumber.Text;
        string password = txtPassword.Text;
        usernumber = usernumber.Replace("'", "\"");
        password = password.Replace("'", "\"");
        string namesql = "select * from Users where usernumber='" + usernumber + "'";
        if (DataClass.Select(namesql) == 0)
            Response.Write("<script>alert('账号或密码错误！');</script>");
        else
        {
            string pwd = DataClass.DataT(namesql).Rows[0][4].ToString();   //查出该账号对应的原密码
            string salt = pwd.Substring(0, 4);  //取出前四位为盐值
            string changepass = String.Concat(salt, password);   //连接盐值与用户输入的密码
            string md5pass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(changepass, "MD5");  //进行MD5加密
            password = String.Concat(salt, md5pass);  //连接盐值与MD5加密过的密码
            string validcode = txtValidCode.Text;
            string sql = "select * from Users where usernumber='" + usernumber + "' and password='" + password + "'";
            if (usernumber.Length == 0 || password.Length == 0)
                Response.Write("<script>alert('输入不能为空！');</script>");
            else if (DataClass.Select(sql) == 0)
                Response.Write("<script>alert('账号或密码错误！');</script>");
            else if (validcode != Session["ValidCode"].ToString())
                Response.Write("<script>alert('验证码错误！');</script>");
            else
            {
                Response.Write("<script>alert('登录成功！');location='Mycenter.aspx'</script>");
                user = new Users(usernumber);
                Session["user"] = user;
            }
        }       
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }
}