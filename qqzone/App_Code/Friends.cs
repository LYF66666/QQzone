using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Friends 的摘要说明
/// </summary>
public class Friends
{
    public int Friendid;
    public string Friendnumber;
    public string Nickname;
    public string Zonename;
    public string Sex;
    public string Birthday;
    public string Phonenumber;
    public string Address;
    public string urlHeadPhoto;

    public Friends(int frindid)
    {
        this.Friendid = frindid;
        string sql = "select * from Users where userid='" + Friendid + "'";
        DataTable dt = new DataTable();
        dt = DataClass.DataT(sql);
        this.Friendnumber = dt.Rows[0][1].ToString();
        this.Nickname = dt.Rows[0][2].ToString();
        this.Zonename = dt.Rows[0][3].ToString();
        this.Sex = dt.Rows[0][5].ToString();
        this.Birthday = dt.Rows[0][6].ToString();
        this.Phonenumber = dt.Rows[0][7].ToString();
        this.Address = dt.Rows[0][8].ToString();
        this.urlHeadPhoto = dt.Rows[0][10].ToString();

    }
}