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
    public partial class report9 : System.Web.UI.Page
    {
        connectTT_efgp myconn = new connectTT_efgp();
        //connectPLM myconn = new connectPLM();
        protected static int col_num=13;
        protected static int colspan = 0;
        protected static string strsql;
        protected static string[] col_name = new string[col_num];
        protected static int table_width = 0;
        protected static string code_hide_table = "hidden ='true'";
        protected static DataTable tbTempt3;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                code_hide_table = "hidden ='true'";
                this.date1.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
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
                    return;
                }



                DataTable tbTempt;
                tbTempt = new DataTable();
               
                tbTempt3 = new DataTable();
               
                for (int i = 0; i < col_num; i++)
                {
                    tbTempt3.Columns.Add(i.ToString());
                }

                //this.label_test.Text = text1.ToString("yyyy-MM-dd") + text2.ToString("yyyy-MM-dd");


               tbTempt = this.GetData("1", text1.ToString("yyyy-MM-dd"),text2.ToString("yyyy-MM-dd"));

                //for (int i = 0; i < col_num; i++)
                //{
                //    col_name[i] = tbTempt.Columns[i].ColumnName;
                //}
                col_name[0] = "單據號碼";
                col_name[1] = "申請日期";
                col_name[2] = "申請部門";
                col_name[3] = "工號";
                col_name[4] = "姓名";
                col_name[5] = "出差地點";
                col_name[6] = "洽辦內容";
                col_name[7] = "外出日期";
                col_name[8] = "外出時間";
                col_name[9] = "返回日期";
                col_name[10] = "返回時間";
                col_name[11] = "備註";
                col_name[12] = "外出人員";
                for (int i = 0; i < tbTempt.Rows.Count; i++)
                {
                    tbTempt3.Rows.Add();
                    for (int j=0;j<col_num;j++)
                    {
                      
                        tbTempt3.Rows[i][j] = tbTempt.Rows[i][j].ToString();
                    }
                       
                   
                }

                temptData.DataSource = tbTempt3;
                temptData.DataBind();
                this.Repeater1.DataSource = tbTempt3;
                this.Repeater1.DataBind();


            }
            catch (Exception ms)
            {
                Response.Write(strsql);
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
         
                
            DataTable dataByTime = new DataTable();
            myconn.myopen();
            //strsql = " select  pmm09,sum(pmm40t) as pmm40t from  PMM_FILE  where pmm04>=TO_DATE('" + pDate1 + "','YYYY-MM-DD') and pmm04<=TO_DATE('" + pDate2 + "','YYYY-MM-DD') group by pmm09";
            if (code == "1")
                strsql = "select a.serialnumber,a.app_date,a.dep_no,a.emp_no,a.emp_name,a.BT_LOC,a.BT_CONTENT," +
                        " a.o_date1,a.o_time1,a.o_date2,a.o_time2,a.bt_remark,bt_emp_name_txt,a.f_date" +
                        " from(" +
                        " select  emp_no, emp_name, dep_no, serialnumber, APP_DATE, o_date1, o_time1, o_date2, o_time2, bt_remark," +
                        " (" +
                        "          Select ChangeProcessStateAudit.createdTime" +
                        "         from ChangeProcessStateAudit" +
                        " inner join ProcessInstance on ChangeProcessStateAudit.sourceOID = ProcessInstance.OID" +
                        " and ChangeProcessStateAudit.newState = ProcessInstance.currentState" +
                        " and ProcessInstance.currentState = 3" +
                        " where serialNumber = CX_AD_0008_VN_TW1104.processserialnumber" +
                        " )f_date," +
                        " BT_LOC,BT_CONTENT" +
                        " from CX_AD_0008_VN_TW1104" +
                        " where hidorgids = 'CX_VN'" +
                        " )a" +
                        " left outer join CX_AD_0008_VN_TW1104_BT_GRID01 on formserialnumber = a.serialnumber" +
                        " where not a.f_date is null" +
                        " and a.f_date >=TO_DATE('" + pDate1 + "', 'YYYY/MM/DD')" +
                        " and a.f_date <=TO_DATE('" + pDate2 + "', 'YYYY/MM/DD')";

            else if (code == "2")
                strsql = "select * from V_5836_TOOL_MAINTAIN_R " +
                            " where CREATED_ON >= '" + pDate1 + "' and CREATED_ON <= '" + pDate2 + "'";
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
                return;
            }
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
            base.Response.BinaryWrite(Excel.Report9(col_name,tbTempt3, text2.ToString("yyyy-MM-dd"), text2.ToString("yyyy-MM-dd"),col_num));
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