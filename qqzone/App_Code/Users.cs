using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Users 的摘要说明
/// </summary>
public class Users
{
    public int Userid;
    public string Usernumber;
    public string Nickname;
    public string Zonename;
    public string Password;
    public string Sex;
    public string Birthday;
    public string Phonenumber;
    public string Address;
    public string urlHeadPhoto;
    public Users(string usernumber)
    {
        this.Usernumber = usernumber;
        string sql = "select * from Users where usernumber='" + usernumber + "'";
        DataTable dt = new DataTable();
        dt = DataClass.DataT(sql);
        this.Userid = Convert.ToInt32(dt.Rows[0][0].ToString());
        this.Nickname = dt.Rows[0][2].ToString();
        this.Zonename = dt.Rows[0][3].ToString();
        this.Password = dt.Rows[0][4].ToString();
        this.Sex = dt.Rows[0][5].ToString();
        this.Birthday = dt.Rows[0][6].ToString();
        this.Phonenumber = dt.Rows[0][7].ToString();
        this.Address = dt.Rows[0][8].ToString();
        this.urlHeadPhoto = dt.Rows[0][10].ToString();
    }

    /*public void addFriend()
    {
        string sql = "select * from Friend where userid='" + Userid + "'";
        DataTable dt = new DataTable();
        dt = DataClass.DataT(sql);
        for (int i=0;i<dt.Rows.Count;i++)
        {
            friend.Add(new Friends(Convert.ToInt32(dt.Rows[0][1].ToString())));
        }
    }

    public void addSaying()
    {
        string sql = "select * from Saying where userid='" + Userid + "'";
        DataTable dt = new DataTable();
        dt = DataClass.DataT(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            saying.Add(new Saying(Convert.ToInt32(dt.Rows[0][1].ToString()))); 
        }
    }*/

}