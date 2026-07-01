using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Threading;

namespace CXWeb.kanban
{
    public partial class setting_zhizao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                query();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int[] day_num =new int[5];
            try
            {
                day_num[0] = Convert.ToInt32(TextBox1.Text);
                day_num[1] = Convert.ToInt32(TextBox2.Text);
                day_num[2] = Convert.ToInt32(TextBox3.Text);
                day_num[3] = Convert.ToInt32(TextBox4.Text);
                day_num[4] = Convert.ToInt32(TextBox5.Text);
            }
            catch
            {
                showMessage("天數格式不对！ Giá trị số ngày không đúng!");
                return;
            }

            if(day_num[0]>= day_num[1] || day_num[1] >= day_num[2] || day_num[2] >= day_num[3] || day_num[3] >= day_num[4] )
            {
                showMessage("天數要從小到大！ Số ngày phải từ nhỏ đến lớn!");
                return;
            }
            string ins_sql;

            connectEIP myconn = new connectEIP();

            myconn.myopen();

            ins_sql = "update kanban_setting_zhizao set day_number='" + TextBox1.Text + "' where code ='1'";
            myconn.mySqlExecute(ins_sql);
            ins_sql = "update kanban_setting_zhizao set day_number='" + TextBox2.Text + "' where code ='2'";
            myconn.mySqlExecute(ins_sql);
            ins_sql = "update kanban_setting_zhizao set day_number='" + TextBox3.Text + "' where code ='3'";
            myconn.mySqlExecute(ins_sql);
            ins_sql = "update kanban_setting_zhizao set day_number='" + TextBox4.Text + "' where code ='4'";
            myconn.mySqlExecute(ins_sql);
            ins_sql = "update kanban_setting_zhizao set day_number='" + TextBox5.Text + "' where code ='5'";
            myconn.mySqlExecute(ins_sql);

            myconn.myclose();

            query();
        }
        void query()
        {


            string strsql = "";

            DataTable mydata = new DataTable();
            connectEIP myconn = new connectEIP();

            myconn.myopen();
            strsql = "select * from kanban_setting_zhizao order by code";
            mydata = myconn.mysearch(strsql);
            TextBox1.Text = mydata.Rows[0]["day_number"].ToString();
            TextBox2.Text = mydata.Rows[1]["day_number"].ToString();
            TextBox3.Text = mydata.Rows[2]["day_number"].ToString();
            TextBox4.Text = mydata.Rows[3]["day_number"].ToString();
            TextBox5.Text = mydata.Rows[4]["day_number"].ToString();

            myconn.myclose();
        }
        public void showMessage(string mess)
        {
            string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            Response.Write(strBuilder);
        }
    }
}