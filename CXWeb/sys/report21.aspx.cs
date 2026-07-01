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
    public partial class report21 : System.Web.UI.Page
    {
        connectPLM myconn = new connectPLM();
        protected static int col_num = 6;
        protected static string[] col_name = new string[col_num];
        protected static string code_hide_table = "hidden ='true'";
        protected DataTable tbTempt;
        protected static DataTable tbTempt3;
        protected static string report_msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
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
                tbTempt.Columns[0].ColumnName = "機台編號";
                tbTempt.Columns[1].ColumnName = "故障次數";
                tbTempt.Columns[2].ColumnName = "生產數量";
                tbTempt.Columns[3].ColumnName = "故障平均生產數量";
                tbTempt.Columns[4].ColumnName = "生產時間(時)";
                tbTempt.Columns[5].ColumnName = "故障平均生產時間(時)";
                for (int i = 0; i < col_num; i++)
                {
                    col_name[i] = tbTempt.Columns[i].ColumnName;
                }
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    tbTempt3.Rows.Add();
                    for (int j = 0; j < col_num; j++)
                    {

                        tbTempt3.Rows[i][j] = tbTempt.Rows[i][j].ToString();
                    }


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




        private DataTable GetData(string code, string pDate1, string pDate2, string pDate3)
        {
            string query_in = "",query_date1="",query_date2="";
            query_date1 = pDate1.Replace("-", "");
            query_date2 = pDate3.Replace("-", "");//pDate3 là ko có + thêm 1 ngày, dùng để search bên TT
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
            if (CNC.Checked == true)
            {
                query_in += "'CNC',";
            }
            if (GC.Checked == true)
            {
                query_in += "'GC',";
            }
            if (LH.Checked == true)
            {
                query_in += "'LH',";
            }
            if (ML.Checked == true)
            {
                query_in += "'ML',";
            }
            if (DG.Checked == true)
            {
                query_in += "'DG',";
            }
            if (XM.Checked == true)
            {
                query_in += "'XM',";
            }
            if (XT.Checked == true)
            {
                query_in += "'XT',";
            }

            query_in += "''";

            string strsql;
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            if (code == "1")               
                strsql = "SELECT b.ITEM_NUMBER " +
                         " , ISNULL(a.TIMES, 0) " +
                         " , ISNULL(c.CNT, 0)" +
                         " , ISNULL(ROUND(c.cnt / a.TIMES, 0), 0)" +
                         " , ISNULL(c.PRODUCE_TIME, 0) " +
                         " , ISNULL(ROUND(c.PRODUCE_TIME / a.TIMES, 0), 0)" +
                         " FROM[CX_official_1].[dbo].[V_5836_TOOLITEM_ATTRIBUTE] b left outer join" +
                         " (" +
                         " SELECT ITEM_NUMBER, count(*) TIMES" +
                         " FROM dbo.V_5836_TOOL_BRKDWN_BY_TON" +
                         " WHERE CREATED_ON BETWEEN '" + pDate1 + "' and '" + pDate2 + "'	" +
                         " GROUP BY ITEM_NUMBER) a on a.ITEM_NUMBER=b.ITEM_NUMBER left outer join" +
                         " (SELECT P.ITEM_NUMBER, SUM(P.CNT) CNT , SUM(P.PRODUCE_TIME) PRODUCE_TIME" +
                         " FROM(SELECT A.ITEM_NUMBER, A.DEPT, C.CNT , C.PRODUCE_TIME" +
                         " FROM [CX_official_1].[dbo].[V_5836_TOOLITEM_ATTRIBUTE] A," +
                         " ( SELECT* FROM OPENQUERY(CXVNTP, '" +
                         " SELECT SHB09, SUM(CNT) CNT, round(SUM(PRODUCE_TIME)/60,2) PRODUCE_TIME" +
                         " FROM(select to_char(((case when (ecg05<ecg06 or (ecg05> ecg06 and shb031 > ''12:00'' )) then shb03 else shb03 - 1 end)),''yyyymmdd'') shb03" +
                         " , shb09" +
                         " , (shb111+shb112+shb115) as cnt" +
                         " , shb033 as PRODUCE_TIME" +
                         " from shb_file, ecg_file" +
                         " where shbconf=''Y'' and shb08 = ecg01(+))" +
                         " WHERE SHB03 BETWEEN ''" + query_date1 + "'' AND ''" + query_date2 + "''" +
                         " GROUP BY SHB09')) C" +
                         " WHERE A.ITEM_NUMBER=C.SHB09 COLLATE Chinese_Taiwan_Stroke_CI_AS) P" +
                         " GROUP BY P.ITEM_NUMBER) c on b.ITEM_NUMBER=c.ITEM_NUMBER" +
                         " WHERE b.DEPT IN(" + query_in + ")" +
                         " AND b.STATE<> 'Superseded'" +
                         " AND b.CLASSIFICATION like '有算家動率設備%' " +
                         " ORDER BY b.ITEM_NUMBER asc";
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            
            if (code_hide_table.Trim() != "")
            {
                showMessage("请先查询！");
                return;
            }
            string arg = "report " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            base.Response.Clear();
            //base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            base.Response.ContentType= "application/ms-excel";
            base.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", arg));
            base.Response.BinaryWrite(Excel.Report(col_name, tbTempt3, "", col_num));
            base.Response.Flush();
            base.Response.End();
        }

        public void showMessage(string mess)
        {
            string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            Response.Write(strBuilder);
        }
    }
}