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
    public partial class report17 : System.Web.UI.Page
    {
        connectPLM myconn = new connectPLM();
        protected static int col_num = 2;
        protected static string[] col_name = new string[col_num];
        protected static string code_hide_table = "hidden ='true'";
        protected DataTable tbTempt;
        protected static DataTable tbTempt3;
        protected static string report_msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Column");
            Chart1.Series[1].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line");
            Chart1.Series[2].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line");
            if (!IsPostBack)
            {
                CT1.Enabled = true;
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
                tbTempt = this.GetData("1", text1.ToString("yyyy-MM-dd"), text2.AddDays(1).ToString("yyyy-MM-dd"), text2.ToString("yyyy-MM-dd"));

                //tbTempt.Columns[0].ColumnName = "技術員工號";
                //tbTempt.Columns[1].ColumnName = "技術員姓名";
                //tbTempt.Columns[2].ColumnName = "換模耗時";
                //-----------------------------------------------------

                //DataTable tb = new DataTable();
                //tb.Columns.Add("col1");
                //tb.Columns.Add("col2");

                double max_times = 0;
                double max_avg = 0;
                double max_avg_prod_cnt = 0;
                foreach (DataRow row in tbTempt.Rows)
                {
                    Chart1.Series["Category"].Points.Add();
                    Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].Label = row["TIMES"].ToString();
                    Chart1.Series["Category"].Points[Chart1.Series["Category"].Points.Count - 1].SetValueXY(row["TON"].ToString() + "噸(" + row["MACH_CNT"].ToString() + "台)", Convert.ToDouble(row["TIMES"]));

                    if (max_times < Convert.ToDouble(row["TIMES"])) max_times = Convert.ToDouble(row["TIMES"]);
                }
                foreach (DataRow row in tbTempt.Rows)
                {
                    if (max_avg < Convert.ToDouble(row["AVG_TIMES"]))
                        max_avg = Convert.ToDouble(row["AVG_TIMES"]);
                    if (max_avg_prod_cnt < Convert.ToDouble(row["AVG_PROD_CNT"]))
                        max_avg_prod_cnt = Convert.ToDouble(row["AVG_PROD_CNT"]);
                }
                foreach (DataRow row in tbTempt.Rows)
                {
                   Chart1.Series["Category2"].Points.Add();
                    Chart1.Series["Category2"].Points[Chart1.Series["Category2"].Points.Count - 1].Label = row["AVG_TIMES"].ToString();
                    Chart1.Series["Category2"].Points[Chart1.Series["Category2"].Points.Count - 1].SetValueY( Convert.ToDouble(row["AVG_TIMES"]) * max_times / max_avg);

                }
                foreach (DataRow row in tbTempt.Rows)
                {
                    Chart1.Series["Category3"].Points.Add();
                    Chart1.Series["Category3"].Points[Chart1.Series["Category3"].Points.Count - 1].Label = row["AVG_PROD_CNT"].ToString();
                    Chart1.Series["Category3"].Points[Chart1.Series["Category3"].Points.Count - 1].SetValueY(Convert.ToDouble(row["AVG_PROD_CNT"]) * max_times / max_avg_prod_cnt);

                }

                Chart1.Series["Category2"].Color = System.Drawing.Color.Red;
                Chart1.Series["Category3"].Color = System.Drawing.Color.Blue;

                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;

                Chart1.Series[0].Name = "總故障次數";
                Chart1.Series[1].Name = "平均故障次數";
                Chart1.Series[2].Name = "故障平均生產數量";


                //-----------------------------------------------------

                //for (int i = 0; i < col_num; i++)
                //{
                //    col_name[i] = tbTempt.Columns[i].ColumnName;
                //}
                //for (int i = 0; i < tbTempt.Rows.Count; i++)
                //{
                //    tbTempt3.Rows.Add(tbTempt.Rows[i]["技術員工號"].ToString(), tbTempt.Rows[i]["技術員姓名"].ToString());



                //}

                //this.Repeater1.DataSource = tbTempt3;
                //this.Repeater1.DataBind();

            }
            catch (Exception ms)
            {
                showMessage(ms.Message);
                Response.Write(ms.Message);
            }
        }




        private DataTable GetData(string code, string pDate1, string pDate2, string pDate3)
        {
            string query_in = "",query_date1="",query_date2="";
            query_date1 = pDate1.Replace("-", "");
            query_date2 = pDate3.Replace("-", "");
            if (CT1.Checked == true)
            {
                query_in += "'CT1',";
            }
            if (CT2.Checked == true)
            {
                query_in += "'CT2',";
            }
            if (CT3.Checked == true)
            {
                query_in += "'CT3',";
            }          
            if (LH.Checked == true)
            {
                query_in += "'LH',";
            }
           
            query_in += "''";

            string strsql;
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            if (code == "1")
                strsql = "SELECT convert(int,SUBSTRING(a.TON,1,LEN(a.TON)-1)) TON" +
                         " , a.TIMES" +
                         " , b.MACH_CNT" +
                         " , a.TIMES / b.MACH_CNT AVG_TIMES" +
                         " , c.cnt" +
                         " , ISNULL(ROUND(c.cnt/a.TIMES,0),0) AVG_PROD_CNT" +
                         " FROM(" +
                         " SELECT count(ITEM_NUMBER) MACH_CNT" +
                         " , TON" +
                         " FROM dbo.V_5836_TOOLITEM_ATTRIBUTE" +
                         " WHERE DEPT IN(" + query_in + ")" +
                         " AND TON IS NOT NULL" +
                         " AND STATE <> 'Superseded'" +
                         " AND CLASSIFICATION like '有算家動率設備%'" +
                         " GROUP BY TON) b" +
                         " , (" +
                         " SELECT TON" +
                         " , count(*) TIMES" +
                         " FROM dbo.V_5836_TOOL_BRKDWN_BY_TON" +
                         " WHERE DEPT IN ("+ query_in +")" +
                         " AND CREATED_ON BETWEEN '" + pDate1 + "' and '" + pDate2 + "'" +
                         " GROUP BY TON) a left outer join (" +
                         " SELECT P.TON" +
                         " , SUM(P.CNT) CNT" +
                         " FROM(" +
                         " SELECT A.TON" +
                         " , A.DEPT" +
                         " , C.CNT" +
                         " FROM[CX_official_1].[dbo].[V_5836_TOOLITEM_ATTRIBUTE] A" +
                         " , (" +
                         " SELECT * FROM OPENQUERY(CXVNTP, '" +
                             " SELECT SHB09" +
                                  " , SUM(CNT) CNT" +
                               " FROM V_5836_PROD_CNT" +
                              " WHERE SHB03 BETWEEN ''" + query_date1 + "'' AND ''" + query_date2 + "''" +
                              " GROUP BY SHB09')) C" +
                        " WHERE A.ITEM_NUMBER = C.SHB09 COLLATE Chinese_Taiwan_Stroke_CI_AS" +
                        " AND A.TON IS NOT NULL) P" +
                        " WHERE P.DEPT IN(" + query_in + ")" +
                        " GROUP BY P.TON) c on a.TON = c.TON" +
                        " WHERE a.TON = b.TON" +
                        " order by convert(int,SUBSTRING(a.TON,1,LEN(a.TON)-1)) ";
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