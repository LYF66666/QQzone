using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// DataClass 的摘要说明
/// </summary>
public class DataClass
{
    private static string str = @"server=(localdb)\v11.0;Integrated Security=SSPI;database=qqzone;";
    public DataClass()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static void Save(string sql)
    {
        SqlConnection conn = new SqlConnection(str);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public static int Select(string sql)
    {
        SqlConnection conn = new SqlConnection(str);
        System.Data.DataTable dt = new DataTable();
        conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        da.Fill(dt);
        int count = dt.Rows.Count, result = 0;
        if (count > 0)
            result = 1;
        else
            result = 0;
        conn.Close();
        return result;
    }



    public static DataTable DataT(string sql)
    {
        SqlConnection conn = new SqlConnection(str);
        System.Data.DataTable dt = new DataTable();
        conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        da.Fill(dt);
        conn.Close();
        return dt;
    }


    private static char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    public static string GenerateRandomNumber(int Length)
    {
        System.Text.StringBuilder newRandom = new System.Text.StringBuilder(10);
        Random rd = new Random();
        for (int i = 0; i < Length; i++)
        {
            newRandom.Append(constant[rd.Next(10)]);
        }
        return newRandom.ToString();
    }

    public static int CheckAbleToComment(int userid,int friendid)
    {
        string sql = "select * from Users where userid='" + friendid + "'";
        string able = DataClass.DataT(sql).Rows[0][13].ToString();
        int result = 1;
        if (able == "all")
            result = 1;
        else if (able == "friends")
            result = 0;
        else
            result = -1;
        int temp = 1;
        string selectsql = "select * from Friend where (userid='" + userid + "' and friendid='" + friendid + "') or (userid='" + friendid + "' and friendid='" + userid + "') ";
        if (DataClass.Select(selectsql) == 1)
        {
            if (result != -1)
                temp = 1;
            else
                temp = 0;
        }
        else
        {
            if (result == 1)
                temp = 1;
            else
                temp = 0;
        }
        return temp;
    }

    public static int CheckVisitorVisible(int userid, int friendid)
    {
        string sql = "select * from Users where userid='" + friendid + "'";
        string able = DataClass.DataT(sql).Rows[0][12].ToString();
        int result = 1;
        if (able == "all")
            result = 1;
        else if (able == "friends")
            result = 0;
        else
            result = -1;
        int temp = 1;
        string selectsql = "select * from Friend where (userid='" + userid + "' and friendid='" + friendid + "') or (userid='" + friendid + "' and friendid='" + userid + "') ";
        if (DataClass.Select(selectsql) == 1)
        {
            if (result != -1)
                temp = 1;
            else
                temp = 0;
        }
        else
        {
            if (result == 1)
                temp = 1;
            else
                temp = 0;
        }
        return temp;
    }
}