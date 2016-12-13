using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pwdeditor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        Users user = (Users)Session["user"];
        string password = txtPassword.Text;
        string newpwd = txtNewPwd.Text;
        string phonenumber = txtPhonenumber.Text;
        string pwd = user.Password;  //获取当前用户的密码
        string salt = pwd.Substring(0, 4);  //取出前四位为盐值
        string changepass = String.Concat(salt, password);   //连接盐值与用户输入的密码
        string md5pass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(changepass, "MD5");  //进行MD5加密
        password = String.Concat(salt, md5pass);  //连接盐值与MD5加密过的密码
        string sql = "select * from Users where usernumber='" + user.Usernumber + "' and password='" + password + "'";
        if (password.Length == 0 || newpwd.Length == 0 || phonenumber.Length == 0)
            Response.Write("<script>alert('输入不能为空！');</script>");
        else if (DataClass.Select(sql) == 0)
            Response.Write("<script>alert('原密码错误！');</script>");
        else if(phonenumber != user.Phonenumber)
            Response.Write("<script>alert('手机号错误！');</script>");
        else
        {
            changepass= String.Concat(salt, newpwd);   //连接盐值与新密码
            md5pass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(changepass, "MD5");  //进行MD5加密
            password = String.Concat(salt, md5pass);  //连接盐值与MD5加密过的密码
            string savesql = "update Users set password='" + password + "' where userid='" + user.Userid + "'";
            DataClass.DataT(savesql);
            Response.Write("<script>alert('重置成功！');location='Information.aspx'</script>");
        }
    }
}