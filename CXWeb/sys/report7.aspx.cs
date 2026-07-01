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
    public partial class report7 : System.Web.UI.Page
    {
        connectPLM myconn = new connectPLM();
        protected static int max_count_col = 1,col_num=16;
        protected static int colspan = 0;
        protected string date_from, date_to;
        protected string year1, year2;
        protected static string[] code_hiden = new string[100];
        protected static string[] col_name = new string[col_num];
        protected static int table_width = 0;
        protected static string code_hide_table = "hidden ='true'";
        protected DateTime pDate;
        protected string tg_code="";
        protected static DataTable tbTempt;
        protected static DataTable tbTempt3;
        protected static DataTable dtFilter1;
        protected static DataTable dtFilter2;

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
                dtFilter1 = new DataTable();
                dtFilter2 = new DataTable();
                tbTempt.Clear();
                tbTempt3 = new DataTable();
                tbTempt3.Clear();
                for (int i = 0; i < col_num; i++)
                {
                    tbTempt3.Columns.Add(i.ToString());
                }

                
                //数据库有HH：mm：ss 所以要加一天才能查到
               tbTempt = this.GetData(code, text1.ToString("yyyy-MM-dd"),text2.AddDays(1).ToString("yyyy-MM-dd"));
                dtFilter1 = tbTempt.AsEnumerable().Where(x => x["實際修復時間"] != DBNull.Value).CopyToDataTable();
                dtFilter2 = SummaryData(tbTempt);

                for (int i = 0; i < col_num; i++)
                {
                    col_name[i] = dtFilter1.Columns[i].ColumnName;
                }
                for (int i = 0; i < dtFilter1.Rows.Count; i++)
                {
                    tbTempt3.Rows.Add();
                    for (int j=0;j<col_num;j++)
                    {
                      
                        tbTempt3.Rows[i][j] = dtFilter1.Rows[i][j].ToString();
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
            //strsql = "select * from V_5836_TOOL_REPAIR_R " +
            //         " where 實際修復時間 >= '" + pDate1 + "' and 實際修復時間 < '" + pDate2 + "'";
            {
                strsql = " SELECT   I1.CN_ORG AS 報修單位, I1.NAME AS 報修者, R.ITEM_NUMBER AS 報修單號," +
                        " E.ECI03 AS 工作站, " +
                        " T.ITEM_NUMBER AS 設備編號,M.CX_1104_002 as 機台類型, T.NAME AS 設備名稱, 				" +
                        " STUFF((SELECT ', ',  LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(DP.CX_D_005, CHAR(10), ''), CHAR(13), ''), CHAR(9), ''))) +'-'+" +
                        " LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(DP.CX_D_004, CHAR(10), ''), CHAR(13), ''), CHAR(9), ''))) AS 'data()'" +
                        " FROM innovator.r_cx_detail_repair_part TP, innovator.cx_detail_repair_part DP" +
                        " WHERE DP.ID = TP.RELATED_ID and R.id = TP.SOURCE_ID" +
                        " FOR XML PATH('')), 1, 2, '')  維修部位, " +
                        " STUFF((SELECT ', ', BR.CN_002 AS 'data()'" +
                        " FROM innovator.TOOLREPAIRRECOED_BREAKDOWN_R TB, innovator.CX_BREAKDOWN_RESON BR" +
                        " WHERE BR.ID = TB.RELATED_ID and R.id = TB.SOURCE_ID" +
                        " FOR XML PATH('')), 1, 2, '')  故障原因, " +
                        " convert(varchar,innovator.ConvertToLocal(R.BROKEN_DATE, NULL),120) AS 故障時間, " +
                        " convert(varchar,innovator.ConvertToLocal(R.ENDED_DATE, NULL),120) AS 實際修復時間," +
                        " STUFF((SELECT ', ', I2.NAME AS 'data()'" +
                        " FROM innovator.TOOLREPAIRRECORD_MEMBER RM, innovator.[IDENTITY] I2" +
                        " WHERE I2.ID = RM.RELATED_ID and R.id = RM.SOURCE_ID" +
                        " FOR XML PATH('')), 1, 2, '') 維修人員," +
                        " STUFF((SELECT ', ', MT.CN_001 AS 'data()'" +
                        " FROM innovator.TOOLREPAIRRECORD_MA_TYPE TT, innovator.CX_MA_TYPE MT" +
                        " WHERE MT.ID = TT.RELATED_ID and R.id = TT.SOURCE_ID" +
                        " FOR XML PATH('')), 1, 2, '')   維修方式及對策," +
                        " CASE WHEN R.CN_004 = '0' THEN 'N' ELSE 'Y' END AS 是否停機, " +
                        " STUFF((SELECT ', ', P2.item_number + ' ' + P2.name + '(' + P2.DESCRIPTION + ')' + ' X (' + CONVERT(nvarchar, TP2.cn_qty) + ' ' + P2.unit + ')' AS 'data()'" +
                        " FROM innovator.TOOLREPAIRRECORD_PART TP2, innovator.PART  P2" +
                        " WHERE P2.ID = TP2.RELATED_ID and R.id = TP2.SOURCE_ID" +
                        " FOR XML PATH('')), 1, 2, '')   更換零件," +
                        " CASE WHEN (R.CN_004 = '1' OR" +
                        " R.CN_004 IS NULL) THEN CONVERT(numeric(17, 2), CAST(datediff(mi, R.BROKEN_DATE, R.ENDED_DATE)" +
                        " AS numeric) / 60) ELSE 0 END AS 停機時數 " +
                        " FROM innovator.TOOLREPAIRRECORD AS R" +
                        " INNER JOIN innovator.[IDENTITY]" +
                        " AS I1 ON R.OWNED_BY_ID = I1.ID" +
                        " INNER JOIN innovator.TOOLITEM AS T ON R.CN_001 = T.id" +
                        " LEFT OUTER JOIN (SELECT ECI01, ECI03" +
                        " FROM               OPENQUERY(CXVNTP, 'SELECT eci01,eci03 FROM eci_file') AS derivedtbl_1) AS E" +
                        " ON T.ITEM_NUMBER = E.ECI01 COLLATE Chinese_Taiwan_Stroke_CI_AS" +
                        " LEFT OUTER JOIN innovator.CX_TOOLITEM_MACHINE_KIND2 as M on T.MACHINE_KIND= M.id" +
                        " WHERE          (R.STATE IN ('Finish', 'In Confirm', 'In Process'))" +
                        " and (innovator.ConvertToLocal(R.BROKEN_DATE, NULL) >= '" + pDate1 + "' " +
                        " and innovator.ConvertToLocal(R.BROKEN_DATE, NULL) < '" + pDate2 + "') ";
                if (txtMachine.Text != "")
                    strsql = strsql + " and T.ITEM_NUMBER LIKE '%" + txtMachine.Text + "%'  ";
                if (txtStation.Text != "")
                    strsql = strsql + " and E.ECI03 LIKE '%" + txtStation.Text + "%' ";
            }
            else if (code == "2")
                strsql = "select * from TC_6182_TOOL_REPAIR_R " +
                         " where 實際修復時間 >= '" + pDate1 + "' and 實際修復時間 < '" + pDate2 + "'";
            else
                strsql = " ";
            dataByTime = myconn.mysearch(strsql);
            myconn.myclose();
            return dataByTime;
        }

        private DataTable SummaryData(DataTable data)
        {
            DataTable sum = new DataTable();
            sum.Columns.Add(new DataColumn("機台類型", typeof(string)));
            sum.Columns.Add(new DataColumn("設備編號", typeof(string)));
            sum.Columns.Add(new DataColumn("%", typeof(double)));
            data.AsEnumerable().GroupBy(x => x["設備編號"]).ToList().ForEach(machine => {
                DataRow newRow = sum.NewRow();
                newRow["設備編號"] = machine.Key.ToString();
                newRow["機台類型"] = machine.ElementAt(0)["機台類型"].ToString();

                DateTime? curDate = null;
                int countDay = 0;
                double brokenTimes = 0;
                bool cont = true;
                machine.OrderBy(x => Convert.ToDateTime(x["故障時間"])).ToList().ForEach(item =>
                {
                    DateTime brokenDate = Convert.ToDateTime(item["故障時間"]);
                    DateTime startDate = brokenDate.Hour >= 6 ? Convert.ToDateTime(brokenDate.ToString("yyyy-MM-dd 06:00:00"))
                                                              : Convert.ToDateTime(brokenDate.AddDays(-1).ToString("yyyy-MM-dd 06:00:00"));
                    if (startDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        if (curDate == null || !curDate.Equals(startDate))
                        {
                            curDate = startDate;
                            countDay++;
                        }

                        if (cont)
                        {
                            if (item["停機時數"] != DBNull.Value)
                                brokenTimes += Convert.ToDouble(item["停機時數"]);
                            else
                            {
                                brokenTimes += (startDate.AddDays(1) - brokenDate).TotalHours;
                                cont = false;
                            }
                        }
                    }
                });
                int totalHour = 24 * countDay;
                newRow["%"] = Math.Round((totalHour - brokenTimes) / totalHour * 100, 2);
                sum.Rows.Add(newRow);
            });
            sum = sum.AsEnumerable().OrderBy(x => x["機台類型"]).CopyToDataTable();
            return sum;
        }

        protected void btnXem_Click(object sender, EventArgs e)
        {

            
            LoadChartType();
            LoadChart("1");
            code_hide_table = "";
        }
        //protected void btnXem2_Click(object sender, EventArgs e)
        //{


        //    LoadChartType();
        //    LoadChart("2");
        //    code_hide_table = "";
           
        //}
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
            string arg = "report07 " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            base.Response.Clear();
            //base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            base.Response.ContentType = "application/ms-excel";
            base.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", arg));
            base.Response.BinaryWrite(Excel.Report7(col_name, dtFilter1, text2.ToString("yyyy-MM-dd"), text2.ToString("yyyy-MM-dd"), col_num, dtFilter2));
            base.Response.Flush();
            base.Response.End();
        }
        public void showMessage(string mess)
        {
            string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            Response.Write(strBuilder);
        }

        private void ExportExcel()
        {

        }
    }
}