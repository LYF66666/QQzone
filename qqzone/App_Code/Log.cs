using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Log 的摘要说明
/// </summary>
public class Logs
{
    public int Logid;
    public int Userid;
    public string Headline;
    public string Content;
    public string Time;
    public Logs(int logid)
    {
        this.Logid = logid;
        string sql = "select * from Log where logid='" + logid + "'";
        DataTable dt = DataClass.DataT(sql);
        this.Userid = Convert.ToInt32(dt.Rows[0][1].ToString());
        this.Headline = dt.Rows[0][2].ToString();
        this.Content = dt.Rows[0][3].ToString();
        this.Time = dt.Rows[0][4].ToString();
    }
}