using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Information : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            Users user = (Users)Session["user"];
            lbSex.Text = user.Sex;
            int year = Convert.ToInt32(DateTime.Now.Year.ToString());
            int birthyear = Convert.ToInt32(user.Birthday.Substring(0, 4));
            string age = (year - birthyear).ToString();
            lbAge.Text = age;
            lbBirthday.Text = user.Birthday.Substring(5);
            lbPhone.Text = user.Phonenumber;
            lbAddress.Text = user.Address;
        }        
    }

    protected void btnEditor_Click(object sender, EventArgs e)
    {
        Response.Redirect("Editor.aspx");
    }

    protected void btnPwd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pwdeditor.aspx");
    }
}