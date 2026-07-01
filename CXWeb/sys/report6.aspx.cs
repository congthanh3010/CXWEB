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

namespace CXWeb.sys
{
    public partial class report6 : System.Web.UI.Page
    {
        connectPLM myconn = new connectPLM();
        protected static int max_count_col = 1,col_num=15;
        protected static int colspan = 0;
        protected string myDate = "2017-11-02";
        protected string date_from, date_to;
        protected string year1, year2;
        protected static string[] code_hiden = new string[100];
        protected static string[] col_name = new string[col_num];
        protected static int table_width = 0;
        protected DateTime pDate;
        protected string tg_code="";
        protected static string code_hide_table = "hidden ='true'";
        protected DataTable tbTempt;
        protected static DataTable tbTempt3;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Column");
            //Chart2.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Pie");
           

            DateTime pDate = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 6, 0, 0);
            DateTime pDate2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);

            
            //myconn.myopen();

            //string strsql = "  select * from [PLC_PLCM_CX] order by p_kind asc, clocate asc, cmachine asc ";

            //mcTable = myconn.mysearch(strsql);         

            //myconn.myclose();

            //tg_code = base.Request.QueryString["vd"];

            if (!IsPostBack)
            {
                code_hide_table = "hidden ='true'";
                this.date1.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //LoadChartType();

                //LoadChart();

            }
           

        }
        private void LoadChartType()
        {
            //string[] chartType = Enum.GetNames(typeof(SeriesChartType));
            //drTypeChart.DataSource = chartType;
            //drTypeChart.DataBind();
            //drTypeChart.Items.Insert(0, new ListItem("--Option--", "Column"));

            //drTypeChart2.DataSource = chartType;
            //drTypeChart2.DataBind();
            //drTypeChart2.Items.Insert(0, new ListItem("--Option--", "Column"));
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
                    showMessage("日期格式不对/Kiểu thời gian không đúng");
                    this.date1.Text = pDate.ToString("dd/MM/yyyy");
                    this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    text1 = new DateTime(Convert.ToInt32(date1.Text.Substring(6, 4)), Convert.ToInt32(date1.Text.Substring(3, 2)), Convert.ToInt32(date1.Text.Substring(0, 2)), Convert.ToInt32(date1.Text.Substring(11, 2)), Convert.ToInt32(date1.Text.Substring(14, 2)), 0);
                    text2 = new DateTime(Convert.ToInt32(date2.Text.Substring(6, 4)), Convert.ToInt32(date2.Text.Substring(3, 2)), Convert.ToInt32(date2.Text.Substring(0, 2)), Convert.ToInt32(date2.Text.Substring(11, 2)), Convert.ToInt32(date2.Text.Substring(14, 2)), 0);
                }

               

                tbTempt = new DataTable();
                
                tbTempt3 = new DataTable();
               
                for (int i = 0; i < col_num; i++)
                {
                    tbTempt3.Columns.Add(i.ToString());
                }



                //数据库有HH：mm：ss 所以要加一天才能查到
                tbTempt = this.GetData("1", text1.ToString("yyyy-MM-dd"), text2.AddDays(1).ToString("yyyy-MM-dd"));

                for (int i = 0; i < col_num; i++)
                {
                    col_name[i] = tbTempt.Columns[i].ColumnName;
                }
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    tbTempt3.Rows.Add();
                    for (int j=0;j<col_num;j++)
                    {
                      
                        tbTempt3.Rows[i][j] = tbTempt.Rows[i][j].ToString();
                    }
                       
                   
                }


                this.Repeater1.DataSource = tbTempt3;
                this.Repeater1.DataBind();


            }
            catch (Exception ms)
            {
                Response.Write(ms.Message);
            }
        }



        #region action
        //protected void drTypeChart_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), drTypeChart.SelectedValue);
        //    LoadChart();
        //}
        //protected void drTypeChart2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Chart2.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), drTypeChart2.SelectedValue);
        //    LoadChart();
        //}
        #endregion
        private DataTable GetData(string code,string pDate1,string pDate2)
        {
            string strsql;    
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            //strsql = " select  pmm09,sum(pmm40t) as pmm40t from  PMM_FILE  where pmm04>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and pmm04<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') group by pmm09";
            if (code == "1")
            { 
                //strsql = "select * from V_5836_TOOL_MAINTAIN_R " +
                //         " where 實際完成保養時間 >= '" + pDate1 + "' and 實際完成保養時間 < '" + pDate2 + "'";
                //if (txtStation.Text != "")
                //    strsql = strsql + " AND 工作站 LIKE '%" + txtStation.Text + "%' ";
                //if (txtMachine.Text != "")
                //    strsql = strsql + " AND 設備編號 like N'%" +txtMachine.Text +"%' ";
                //if (txtKV.Text != "")
                //    strsql = strsql + " AND 設備名稱 like N'%" + txtKV.Text + "%' ";

                strsql = "SELECT U.KEYED_NAME AS 開單者, M.ITEM_NUMBER AS 保養單號, E.ECI03 AS 工作站, \n"
                    + "       T.ITEM_NUMBER AS 設備編號, T.NAME AS 設備名稱, T.CLASSIFICATION AS 設備分類, \n"
                    + "       CASE WHEN M.IS_MAINTENANCE = 1 THEN \n"
                    + "       	CASE WHEN M.IS_CORRECTION = 1 THEN '保養+校正' ELSE '保養' END \n"
                    + "       ELSE \n"
                    + "       	CASE WHEN M.IS_CORRECTION = 1 THEN '校正' ELSE '' END \n"
                    + "       END AS 保養或校正, \n"
                    + "       innovator.ConvertToLocal(M.START_DATE, NULL) AS 預計開始保養時間, \n"
                    + "       innovator.ConvertToLocal(M.ACTURE_START_DATE, NULL) AS 實際開始保養時間, \n"
                    + "       innovator.ConvertToLocal(M.END_DATE, NULL) AS 預計完成保養時間, \n"
                    + "       innovator.ConvertToLocal(M.ENDED_DATE, NULL) AS 實際完成保養時間, \n"
                    + "       ISNULL(STUFF((SELECT ',' + [NAME] FROM V_5836_TOOL_MAMEMBER WHERE SOURCE_ID = M.id ORDER BY [NAME] FOR XML PATH('')), 1, 1, ''), '') AS 保養校正人員, \n"
                    + "       CONVERT(NUMERIC(17,2), CAST(DATEDIFF(mi, M.ACTURE_START_DATE, M.ENDED_DATE) AS NUMERIC) / 60) AS 停機保養時數, \n"
                    + "       ISNULL(M.CN_001, N'') AS 判定, \n"
                    + "       CASE WHEN (SELECT COUNT(SOURCE_ID) FROM V_5836_TOOL_MA_PART WHERE SOURCE_ID = M.id) > 0 \n"
                    + "       THEN STUFF((SELECT ',' + ITEM_NUMBER + ' ' + NAME + '(' + [DESCRIPTION] + ')' + ' X (' + CONVERT(NVARCHAR,CN_QTY) + ' ' + UNIT + ')' \n"
                    + "                   FROM V_5836_TOOL_MA_PART \n"
                    + "                   WHERE SOURCE_ID = M.id \n"
                    + "                   ORDER BY ITEM_NUMBER FOR XML PATH('')), 1, 1, '') \n"
                    + "        ELSE '' END AS 更換零件, \n"
                    + "        innovator.ConvertToLocal(M.CREATED_ON, NULL) AS CREATED_ON \n"
                    + "FROM innovator.TOOLITEM_MAINTAIN AS M \n"
                    + "LEFT JOIN innovator.[USER] AS U ON U.ID = M.CREATED_BY_ID \n"
                    + "LEFT JOIN innovator.TOOLITEM AS T ON T.id = M.CN_002 \n"
                    + "LEFT MERGE JOIN (SELECT eci01, eci03 FROM OPENQUERY(CXVNTP,'SELECT eci01, eci03 FROM eci_file')) AS E ON E.eci01 = T.ITEM_NUMBER COLLATE Chinese_Taiwan_Stroke_CI_AS \n"
                    + "WHERE M.[STATE] IN ('Finish', 'In Confirm') \n"
                    + "AND M.ENDED_DATE >= '" + pDate1 + "' AND M.ENDED_DATE < '" + pDate2 + "' \n";
                if (!string.IsNullOrWhiteSpace(txtStation.Text))
                    strsql += "AND E.eci03 LIKE '%" + txtStation.Text.Trim() + "%' \n";
                if (!string.IsNullOrWhiteSpace(txtMachine.Text))
                    strsql += "AND T.ITEM_NUMBER LIKE N'%" + txtMachine.Text.Trim() + "%' \n";
                if (!string.IsNullOrWhiteSpace(txtKV.Text))
                    strsql += "AND T.[NAME] LIKE N'%" + txtKV.Text.Trim() + "%'";
            }
            else
                strsql = " ";
            dataByTime = myconn.mysearch(strsql);
            myconn.myclose();
            return dataByTime;
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
         

            LoadChartType();
            LoadChart();
            code_hide_table = "";
        }
        protected void btnExcel_Click(object sender, EventArgs e)
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
                showMessage("日期格式不对/Kiểu thời gian không đúng");
                this.date1.Text = pDate.ToString("dd/MM/yyyy");
                this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                text1 = new DateTime(Convert.ToInt32(date1.Text.Substring(6, 4)), Convert.ToInt32(date1.Text.Substring(3, 2)), Convert.ToInt32(date1.Text.Substring(0, 2)), Convert.ToInt32(date1.Text.Substring(11, 2)), Convert.ToInt32(date1.Text.Substring(14, 2)), 0);
                text2 = new DateTime(Convert.ToInt32(date2.Text.Substring(6, 4)), Convert.ToInt32(date2.Text.Substring(3, 2)), Convert.ToInt32(date2.Text.Substring(0, 2)), Convert.ToInt32(date2.Text.Substring(11, 2)), Convert.ToInt32(date2.Text.Substring(14, 2)), 0);
            }
            if (code_hide_table.Trim() != "")
            {
                showMessage("请先查询！");
                return;
            }
            string arg = "report " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            base.Response.Clear();
            //base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            base.Response.ContentType = "application/ms-excel";
            base.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", arg));
            base.Response.BinaryWrite(Excel.Report6(col_name,tbTempt3, text2.ToString("yyyy-MM-dd"), text2.ToString("yyyy-MM-dd"),col_num));
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