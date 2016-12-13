using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string nickname = txtNickName.Text;
        string password = txtPassword.Text;
        string checkpass = txtCheck.Text;
        string year = txtYear.Text;
        string month = txtMonth.Text;
        string day = txtDay.Text;
        string phonenumber = txtPhoneNumber.Text;
        if (nickname.Length == 0 || password.Length == 0 || checkpass.Length == 0 || year.Length == 0 || month.Length == 0 || day.Length == 0 || phonenumber.Length == 0)
            Response.Write("<script>alert('输入不能为空！')</script>");
        else if (password.Length < 6)
            Response.Write("<script>alert('密码不能小于6位！')</script>");
        else if (!Regex.IsMatch(txtPassword.Text, "^[A-Za-z0-9]*$"))
            Response.Write("<script>alert('密码只能包含数字和字母！')</script>");
        else if (txtPassword.Text != txtCheck.Text)
            Response.Write("<script>alert('两次输入密码不一致！')</script>");
        else if (phonenumber.Length != 11)
            Response.Write("<script>alert('请输入正确的11位手机号！')</script>");
        else if (year.Length != 4 || Convert.ToInt32(month) > 12 || Convert.ToInt32(day) > 31)
            Response.Write("<script>alert('请输入正确的生日日期！')</script>");
        else if (rbMan.Checked == false && rbWoman.Checked == false)
            Response.Write("<script>alert('请选择性别！')</script>");
        else
        {
            string sex = "0";
            if (rbMan.Checked)
                sex = "男";
            if (rbWoman.Checked)
                sex = "女";
            string salt = DataClass.GenerateRandomNumber(4);
            string changepass = String.Concat(salt, password);
            string md5pass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(changepass, "MD5");
            string pwd = String.Concat(salt, md5pass);
            string zonename = nickname + "的QQ空间";
            string birthday = "" + year + "年" + month + "月" + day + "日";
            string address = "未填写";
            string headimagename = "blank.jpg";
            string usernumber = DataClass.GenerateRandomNumber(9);
            string selectsql = "select * from Users where usernumber='" + usernumber + "'";
            while (DataClass.Select(selectsql) == 1)
            {
                usernumber = DataClass.GenerateRandomNumber(9);
            }
            string sql = "insert into Users values('" + usernumber + "',N'" + nickname + "',N'" + zonename + "','" + pwd + "',N'" + sex + "',N'" + birthday + "','" + phonenumber + "',N'" + address + "','" + headimagename + "','image/" + headimagename + "','all','all','all')";
            DataClass.Save(sql);
            Session["usernumber"] = usernumber;
            Response.Write("<script>alert('注册成功！');location='Getnumber.aspx'</script>");
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void rbMan_CheckedChanged(object sender, EventArgs e)
    {
        rbWoman.Checked = false;
    }

    protected void rbWoman_CheckedChanged(object sender, EventArgs e)
    {
        rbMan.Checked = false;
    }
}