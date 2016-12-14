using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FriendInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null || Session["friend"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            Friends friend = (Friends)Session["friend"];
            lbSex.Text = friend.Sex;
            int year = Convert.ToInt32(DateTime.Now.Year.ToString());  //获取当前年份
            int birthyear = Convert.ToInt32(friend.Birthday.Substring(0, 4));  //获取出生年份
            string age = (year - birthyear).ToString();  //相减得到年龄
            lbAge.Text = age;
            lbBirthday.Text = friend.Birthday.Substring(5);
            lbPhone.Text = friend.Phonenumber;
            lbAddress.Text = friend.Address;
        }        
    }
}