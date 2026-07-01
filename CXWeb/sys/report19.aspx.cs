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

namespace CXWeb.sys
{
    public partial class report19 : System.Web.UI.Page
    {
        connectPLM myconn = new connectPLM();
        protected static int col_num = 3;
        protected static string[] col_name = new string[col_num];
        protected static string code_hide_table = "hidden ='true'";
        protected DataTable tbTempt;
        protected static DataTable tbTempt3;
        protected static string report_msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Column");
           
            if (!IsPostBack)
            {
               
                this.date1.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
                this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");

                code_hide_table = "hidden ='true'";
                report_msg = "";
            }


        }

        private void LoadChart()
        {
            try
            {

                DateTime text1;
                DateTime text2;
                try
                {
                    text1 = new DateTime(Convert.ToInt32(date1.Text.Substring(6, 4)), Convert.ToInt32(date1.Text.Substring(3, 2)), Convert.ToInt32(date1.Text.Substring(0, 2)));
                    text2 = new DateTime(Convert.ToInt32(date2.Text.Substring(6, 4)), Convert.ToInt32(date2.Text.Substring(3, 2)), Convert.ToInt32(date2.Text.Substring(0, 2)));
                }
                catch
                {
                    //this.Repeater1.DataSource = null;
                    //this.Repeater1.DataBind();
                    code_hide_table = "hidden ='true'";
                    report_msg = "日期格式不对";
                    return;
                }


                tbTempt = new DataTable();
                tbTempt3 = new DataTable();

                for (int i = 0; i < col_num; i++)
                {
                    tbTempt3.Columns.Add(i.ToString());
                }


                //数据库有HH：mm：ss 所以要加一天才能查到
                tbTempt = this.GetData("1", text1.ToString("yyyy-MM-dd"), text2.AddDays(1).ToString("yyyy-MM-dd"));

                tbTempt.Columns[0].ColumnName = "維修技術員工號";
                tbTempt.Columns[1].ColumnName = "維修技術員姓名";               
                tbTempt.Columns[2].ColumnName = "維修耗時";
                tbTempt.Columns[3].ColumnName = "維修次數";
                //-----------------------------------------------------

                //DataTable tb = new DataTable();
                //tb.Columns.Add("col1");
                //tb.Columns.Add("col2");

        
                foreach (DataRow row in tbTempt.Rows)
                {
                    if (Chart1.Series["Category"].Points.Count < 20)
                    {
                        Chart1.Series["Category"].Points.Add();
                        Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Label = row["維修耗時"].ToString();
                        Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].SetValueXY(row["維修技術員工號"].ToString(), Convert.ToDouble(row["維修耗時"]));
                    }
                  
                }
              
               

               

                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;





                //-----------------------------------------------------

                for (int i = 0; i < col_num; i++)
                {
                    col_name[i] = tbTempt.Columns[i].ColumnName;
                }
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    tbTempt3.Rows.Add(tbTempt.Rows[i]["維修技術員工號"].ToString(), tbTempt.Rows[i]["維修技術員姓名"].ToString(), tbTempt.Rows[i]["維修耗時"].ToString());
                }

                this.Repeater1.DataSource = tbTempt3;
                this.Repeater1.DataBind();

            }
            catch (Exception ms)
            {
                showMessage(ms.Message);
                Response.Write(ms.Message);
            }
        }




        private DataTable GetData(string code, string pDate1, string pDate2)
        {
                   

            string strsql;
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            if (code == "1")
                strsql = "SELECT SUBSTRING(NAME,1,CHARINDEX(' ',NAME)-1) EMPLOYEE_NO"+
                         " , SUBSTRING(NAME, CHARINDEX(' ', NAME) + 1, LEN(NAME) - CHARINDEX(' ', NAME)) NAME" +
                         " , CAST(ROUND(SUM(REPAIR_HR) / count(*), 2) AS numeric(5, 2)) AVG_HR" +
                         " , count(*) CNT" +
                         " FROM[CX_official_1].[dbo].[V_5836_TOOL_REPAIRMEMBER]" +
                         " WHERE STATE in ('Finish','In Confirm')" +
                         " AND CREATED_ON BETWEEN '"+ pDate1+ "' and '" + pDate2 + "'" +
                         " GROUP BY NAME "+
                         "  order by AVG_HR desc ";
            else
                strsql = " ";
            dataByTime = myconn.mysearch(strsql);
            myconn.myclose();
            return dataByTime;
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            report_msg = "";
            LoadChart();
            code_hide_table = "";
        }

        //protected void btnExcel_Click(object sender, EventArgs e)
        //{
        //    if (textmonth.Text.Length == 1)
        //        textmonth.Text = "0" + textmonth.Text;

        //    string query_time = textyear.Text.Substring(2, 2) + "/" + textmonth.Text;

        //    if (code_hide_table.Trim() != "")
        //    {
        //        showMessage("请先查询！");
        //        return;
        //    }
        //    string arg = "機台之換模耗時 " + DateTime.Now.ToString("yyyy/MM/dd") + ".xlsx";
        //    base.Response.Clear();
        //    base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    base.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", arg));
        //    base.Response.BinaryWrite(Excel.Report(col_name,tbTempt3, query_time, col_num));
        //    base.Response.Flush();
        //    base.Response.End();
        //}

        public void showMessage(string mess)
        {
            string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            Response.Write(strBuilder);
        }
    }
}