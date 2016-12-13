using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Getnumber : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usernumber"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            string number = Session["usernumber"].ToString();
            lbNumber.Text = number;
            Response.Write(@"<script   language='javascript'>setTimeout('',10000);</script>");
            Response.Write("<meta   http-equiv='refresh'   content='10;URL=Login.aspx'>");

        }
    }

    protected void btnturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}