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
    public partial class report29 : System.Web.UI.Page
    {
        connectPLM myconn = new connectPLM();
        protected static int max_count_col = 1,col_num=25;
        protected static int colspan = 0;
        protected string date_from, date_to;
        protected string year1, year2;
        protected static string[] code_hiden = new string[100];
        protected static string[] col_name = new string[col_num];
        protected static int table_width = 0;
        protected static string code_hide_table = "hidden ='true'";
        protected DateTime pDate;
        protected string tg_code="";
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
        private void LoadChart(string code)
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
                tbTempt.Clear();
                tbTempt3 = new DataTable();
                tbTempt3.Clear();
                for (int i = 0; i < col_num; i++)
                {
                    tbTempt3.Columns.Add(i.ToString());
                }

                
                //数据库有HH：mm：ss 所以要加一天才能查到
               tbTempt = this.GetData(code, text1.ToString("yyyy-MM-dd"),text2.AddDays(1).ToString("yyyy-MM-dd"));

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
                strsql = " select T.KEYED_NAME 設備編號, I1.KEYED_NAME 使用單位, T.MANTAINDURATION 保養週期" +
                        " , convert(varchar, T.RETENTION_PERIOD,23) 最後保養日期,convert(varchar, T.NEXTMANTAINDATE, 23) 下次保養日期" +
                        " ,T.FIXED_TIME_CYCLE 校正週期, convert(varchar, T.LASTED_CORRECTION_DATE,23)  最後校正日期,convert(varchar, T.NEXT_CORRECTION_DATE, 23)  下次維護校正日期" +
                        " ,M.KEYED_NAME 最近保養校正單號, U.KEYED_NAME 開單人員, convert(varchar, M.CREATED_ON,23) 開單日期" +
                        " ,T.CN_015 保養標準工時, T.CN_014 校正標準工時, convert(varchar, innovator.ConvertToLocal(M.START_DATE, null),20) 保養校正開始日" +
                        " ,convert(varchar, innovator.ConvertToLocal(M.END_DATE, null), 20) 保養校正預計結束日期,convert(varchar, innovator.ConvertToLocal(M.ACTURE_START_DATE, null), 20) 保養校正實際開始" +
                        " ,convert(varchar, innovator.ConvertToLocal(M.ENDED_DATE, null), 20) 保養校正實際結束日" +
                        " ,case when M.IS_MAINTENANCE = 1 then 'true' else 'false' end 保養,case when M.IS_CORRECTION = 1 then 'true' else 'false' end 校正" +
                        " , CONVERT(numeric(17, 2), CAST(DATEDIFF(mi, innovator.ConvertToLocal(M.ACTURE_START_DATE, null), innovator.ConvertToLocal(M.ENDED_DATE, null))  AS numeric) / 60) 保養校正時間" +
                        " ,I2.KEYED_NAME 保養校正者" +
                        " , STUFF((SELECT ', ', P.KEYED_NAME + '-' + P.NAME AS 'data()'" +
                        " FROM innovator.TOOLITEM_MA_PART MP, innovator.PART P" +
                        " WHERE P.ID = MP.RELATED_ID and M.id = MP.SOURCE_ID" +
                        " FOR XML PATH('')), 1, 2, '')  備維零件" +
                        " ,M.CN_001 點評, T.CN_011 定期保養單DCC, T.CN_012 校正單DCC" +
                        " from innovator.TOOLITEM_MAINTAIN M" +
                        " inner join innovator.TOOLITEM AS T    ON M.CN_002 = T.id" +
                        " INNER JOIN      innovator.[IDENTITY] AS I1 ON T.CN_001_1 = I1.ID" +
                        " INNER JOIN      innovator.[USER] AS U ON M.CREATED_BY_ID = U.ID" +
                        " INNER JOIN      innovator.[IDENTITY] AS I2 ON M.OWNED_BY_ID = I2.ID" +
                        " where" +
                        " innovator.ConvertToLocal(M.ENDED_DATE, NULL) >= '" + pDate1 + "'" +
                        " and innovator.ConvertToLocal(M.ENDED_DATE, NULL) < '" + pDate2 + "'";


                if (txt01.Text != "")
                    strsql = strsql + " and T.KEYED_NAME like '%" + txt01.Text + "%' ";
                if(txt02.Text != "")
                    strsql = strsql + " and I1.KEYED_NAME like '%" + txt02.Text + "%' ";
                strsql = strsql + " order by M.KEYED_NAME";
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
            LoadChart("1");
            code_hide_table = "";
        }
      
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            export_excel();
        }
        void export_excel()
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
            string arg = "report29 " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            base.Response.Clear();
            //base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            base.Response.ContentType= "application/ms-excel";
            base.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", arg));
            base.Response.BinaryWrite(Excel.Report7(col_name, tbTempt3, text2.ToString("yyyy-MM-dd"), text2.ToString("yyyy-MM-dd"), col_num));
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