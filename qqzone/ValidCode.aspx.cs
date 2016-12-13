using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class ValidCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ValidCode = CreateRandomCode(4);  //生成长度为四位的验证码
        CreateImage(ValidCode);  //对验证码进行绘图
        Session["ValidCode"] = ValidCode;  //传递验证码的值
    }

    //生成随机验证码
    private string CreateRandomCode(int codeCount)
    {
        string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        string[] allCharArray = allChar.Split(',');
        string randomCode = "";
        int temp = -1;
        Random rand = new Random();
        for (int i = 0; i < codeCount; i++)
        {
            if (temp != -1)
            {
                rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
            }
            int t = rand.Next(61);
            if (temp == t)
            {
                return CreateRandomCode(codeCount);
            }
            temp = t;
            randomCode += allCharArray[t];
        }
        return randomCode;
    }

    //绘图
    private void CreateImage(string checkCode)
    {
        int iwidth = (int)(checkCode.Length * 20);
        System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 25);
        Graphics g = Graphics.FromImage(image);
        Font f = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
        Brush b = new System.Drawing.SolidBrush(Color.White);
        //g.FillRectangle(new System.Drawing.SolidBrush(Color.Blue),0,0,image.Width, image.Height);   
        g.Clear(Color.Black);
        g.DrawString(checkCode, f, b, 3, 3);
        Pen blackPen = new Pen(Color.Black, 0);
        Random rand = new Random();
        //for (int i=0;i<5;i++)   
        //{       
        //    int y = rand.Next(image.Height); 
        //    g.DrawLine(blackPen,0,y,image.Width,y);
        //}       
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        Response.ClearContent();
        Response.ContentType = "image/Jpeg";
        Response.BinaryWrite(ms.ToArray());
        g.Dispose();
        image.Dispose();
    }


}