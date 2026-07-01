using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace CXWeb.sys
{
    public partial class Report : System.Web.UI.Page
    {
        public string f = "";
        protected string mc_code;
        protected static string table_width = "800";
        protected static string code_colPLC = "";
        protected static string code_colPN = "";
        protected static string code_colProcess = "";
        protected static string code_colStatus = "";
        
        protected Button btnXem;
        protected DateTime pDate;

        protected Button btnExcel;

        connectDB myconn = new connectDB();
        protected void Page_Load(object sender, EventArgs e)
        {

            //code_colPLC.Value = " hidden ='true' ";
            
            myconn.myopen();

            //string strsql = "  select distinct clocate from [PLC_PLCM_CX] where P_Kind='A'";
            string strsql = "SELECT DepId FROM PLC_MachineList GROUP BY DepId";

            this.Repeater1.DataSource = myconn.mysearch(strsql);
            this.Repeater1.DataBind();

            myconn.myclose();

            pDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            if (DateTime.Now.Hour >= 18)
            {
                pDate = pDate.AddHours(18.0);
            }
            else if (DateTime.Now.Hour < 6)
            {
                pDate = pDate.AddHours(-6.0);
            }
            else
            {
                pDate = pDate.AddHours(6.0);
            }
            mc_code = base.Request.QueryString["mc"];
            if (mc_code != null)
            {
                this.f = "getmachinedata('" + mc_code + "');";
            }
            scan_col_status();
            if (!base.IsPostBack && mc_code != null)
            {
               

            
                this.date1.Text = pDate.ToString("dd/MM/yyyy HH:mm");
                this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                this.mamay.Text = mc_code;
                hmamay.Value = mc_code;
                this.Repeater2.DataSource = this.GetData(mc_code, pDate.ToString("yyyy-MM-dd HH:mm"), DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                this.Repeater2.DataBind();
            }
           
        }

        private DataTable GetData(string pMaMay, string pDate1, string pDate2)
        {
            string strsql;
            string strsqlPN;

            DataTable dataByTime = new DataTable();
          
            DataTable dataPN = new DataTable();
        

            myconn.myopen();
            //strsql = "select * from v_dtl_machine where cMachine = '" + pMaMay + "' and dTime > '" + pDate1 + "' and dTime< '" + pDate2 + "' order by dtime desc";
            //strsql = " select *,a.[Count] - 66 as iCnt from PLC_VL a \n" +
            //         " left join MACHINE_STATUS b  on b.dStTimeStart = (select top 1 dStTimeStart from MACHINE_STATUS where dStTimeStart <= a.TTime and dMachine = a.MACHINE_CODE order by dStTimeStart desc) \n" +
            //         " and b.dMachine = a.MACHINE_CODE \n"+
            //         " left join  MACHINE_STATUS_CODE c on b.dMachine = a.MACHINE_CODE and b.dStatus = c.cCode and c.cCat = 'MachineStatus' \n" +
            //         " where a.MACHINE_CODE = '" + pMaMay + "' \n" +
            //         " and a.TTime >= '" + pDate1 + "' and a.TTime <= '" + pDate2 + "' \n" +
            //         " order by a.TTime desc";
            strsql = "SELECT *, pv.[Count] - 66 AS iCnt \n"
                + "FROM PLC_VL pv \n"
                + "LEFT JOIN MACHINE_STATUS ms ON ms.dMachine = pv.MACHINE_CODE \n"
                + "LEFT JOIN MACHINE_STATUS_CODE msc ON msc.cCat = 'MachineStatus' AND msc.cCode = ms.dStatus \n"
                + "WHERE ms.dStTimeStart = (SELECT TOP 1 dStTimeStart FROM MACHINE_STATUS WHERE dMachine = pv.MACHINE_CODE AND dStTimeStart <= pv.TTime ORDER BY dStTimeStart DESC) \n"
                + "AND pv.MACHINE_CODE = '" + pMaMay + "' \n"
                + "AND pv.TTime BETWEEN '" + pDate1 + "' AND '" + pDate2 + "' \n"
                + "ORDER BY pv.TTime ";//DESC
            dataByTime = myconn.mysearch(strsql);

            strsqlPN = "  select a.* "+
                    " FROM OPENQUERY(TIPTOP, "+
                    " 'select * "+
                    " from V_1444_CHECKIO t "+
                    " where t.ckind = ''I'' "+
                    " and t.shaud03 = ''" + pMaMay.Trim() + "'' " +
                    " and t.sha04 >= (select max(sha04) from V_1444_CHECKIO "+
                    " where ckind = ''I''  and shaud03 = ''" + pMaMay.Trim() + "''   and sha04 < ''" + pDate1.Substring(0,10) + "'' ) " +
                    " order by t.sha04 desc, t.sha041 desc'   ) a";
            dataPN = myconn.mysearch(strsqlPN);

            myconn.myclose();

            dataByTime.Columns.Add("color");
            dataByTime.Columns.Add("color_report");
            dataByTime.Columns.Add("color_PN");
            dataByTime.Columns.Add("color_Process");
            dataByTime.Columns.Add("color_time");
            dataByTime.Columns.Add("code");
            dataByTime.Columns.Add("PN");
            dataByTime.Columns.Add("process");
            dataByTime.Columns.Add("code2");
            string[] array = new string[]
            {
                "none",
                "green",
                "yellow",
                "red"
            };
            //foreach (DataRow dataRow in dataByTime.Rows)
            //{
            //    dataRow["color"] = array[Convert.ToInt32(dataRow["State"])];               
            //}

            int index = 0;
            int index2 = 0;
            int merge_count = 1;
            int indexPN = 0;
            int merge2_count = 1;
            int count_report_group = 0;
            int count_PN_group = 0;
            int count_Process_group = 0;
            string dateDt1 = "";
            string dateDt2 = "";
           
            for (int i = 0; i < dataByTime.Rows.Count; i++)
            {
                dataByTime.Rows[i]["color"] = array[Convert.ToInt32(dataByTime.Rows[i]["State"])];

                if (i % 2 == 0)
                {
                    dataByTime.Rows[i]["color_time"] = "blue";
                }
                else
                {
                    dataByTime.Rows[i]["color_time"] = "blue";
                }

                //------------------------------------------处理机台回报部分------------------------------------------
               
                if (dataByTime.Rows[i]["dStatus"].ToString().Trim() == "")
                {
                    dataByTime.Rows[i]["color_report"] = "green";
                    dataByTime.Rows[i]["cDesc"] = "無機台回報資訊(Không có thông tin báo cáo)";
                }
                else if (dataByTime.Rows[i]["dStatus"].ToString().Substring(0, 1) == "A")
                {
                    dataByTime.Rows[i]["color_report"] = "green";
                }
                else if (dataByTime.Rows[i]["dStatus"].ToString().Substring(0, 1) == "B")
                {
                    dataByTime.Rows[i]["color_report"] = "yellow";
                }
                else
                {
                    dataByTime.Rows[i]["color_report"] = "red";
                }
                //if (i > 0)
                //{
                //    if (dataByTime.Rows[i]["dStatus"].ToString() != dataByTime.Rows[i - 1]["dStatus"].ToString())
                //    {
                //        count_report_group++;
                //    }




                //}
                //if (count_report_group % 2 == 0)
                //{
                //    dataByTime.Rows[i]["color_report"] = "blue";
                //}
                //else if (count_report_group % 2 == 1)
                //{
                //    dataByTime.Rows[i]["color_report"] = "light_blue";
                //}
                //-----------------------------------------------------------------------------------------------
                //------------------------------处理PN和Process部分----------------------------------------------
                if (dataPN.Rows.Count > 0)
                {
                    //if (Convert.ToInt32(dataByTime.Rows[i]["TTime"].ToString().Substring(0, 10).Replace("/", "")+ dataByTime.Rows[i]["TTime"].ToString().Substring(11, 5).Replace(":", "")) 
                    //    < Convert.ToInt32(dataPN.Rows[indexPN]["SHA04"].ToString().Substring(0, 10).Replace("/", "")+ dataPN.Rows[indexPN]["SHA041"].ToString().Replace(":", "")))

                    //if (string.Compare((dataByTime.Rows[i]["TTime"].ToString().Substring(0, 10).Replace("/", "") + (dataByTime.Rows[i]["TTime"].ToString().Substring(15, 1) != ":" ? dataByTime.Rows[i]["TTime"].ToString().Substring(11, 5).Replace(":", ""): "0" + dataByTime.Rows[i]["TTime"].ToString().Substring(11, 5).Replace(":", "")))
                    //  , (dataPN.Rows[indexPN]["SHA04"].ToString().Substring(0, 10).Replace("/", "") + dataPN.Rows[indexPN]["SHA041"].ToString().Replace(":", "")))<0)
                    dateDt1 = Convert.ToDateTime(dataByTime.Rows[i]["TTime"]).ToString("yyyy/MM/dd HH:mm").Substring(0, 10).Replace("/", "") + Convert.ToDateTime(dataByTime.Rows[i]["TTime"]).ToString("yyyy/MM/dd HH:mm").Substring(11, 5).Replace(":", "");
                    dateDt2 = Convert.ToDateTime(dataPN.Rows[indexPN]["SHA04"]).ToString("yyyy/MM/dd").Replace("/", "") + dataPN.Rows[indexPN]["SHA041"].ToString().Replace(":", "");
                    if (string.Compare(dateDt1, dateDt2) < 0)
                    {
                        for (int j = indexPN; j < dataPN.Rows.Count; j++)
                        {
                            //if (Convert.ToInt32(dataByTime.Rows[i]["TTime"].ToString().Substring(0, 10).Replace("/", "")+ dataByTime.Rows[i]["TTime"].ToString().Substring(11, 5).Replace(":", ""))
                            //    >= Convert.ToInt32(dataPN.Rows[j]["SHA04"].ToString().Substring(0, 10).Replace("/", "")+ dataPN.Rows[j]["SHA041"].ToString().Replace(":", "")))
                            //if (string.Compare((dataByTime.Rows[i]["TTime"].ToString().Substring(0, 10).Replace("/", "") + (dataByTime.Rows[i]["TTime"].ToString().Substring(15, 1) != ":" ? dataByTime.Rows[i]["TTime"].ToString().Substring(11, 5).Replace(":", "") : "0" + dataByTime.Rows[i]["TTime"].ToString().Substring(11, 5).Replace(":", "")))
                            //  ,(dataPN.Rows[j]["SHA04"].ToString().Substring(0, 10).Replace("/", "") + dataPN.Rows[j]["SHA041"].ToString().Replace(":", "")))>=0)
                            dateDt1 = Convert.ToDateTime(dataByTime.Rows[i]["TTime"]).ToString("yyyy/MM/dd HH:mm").Substring(0, 10).Replace("/", "") + Convert.ToDateTime(dataByTime.Rows[i]["TTime"]).ToString("yyyy/MM/dd HH:mm").Substring(11, 5).Replace(":", "");
                            dateDt2 = Convert.ToDateTime(dataPN.Rows[j]["SHA04"]).ToString("yyyy/MM/dd").Replace("/", "") + dataPN.Rows[j]["SHA041"].ToString().Replace(":", "");
                            if (string.Compare(dateDt1, dateDt2) >= 0)
                            {
                                indexPN = j;
                                dataByTime.Rows[i]["PN"] = dataPN.Rows[indexPN]["SGM03_PAR"].ToString();
                                dataByTime.Rows[i]["process"] = dataPN.Rows[indexPN]["SGM04"].ToString();

                                index2 = i;
                                merge2_count = 1;
                               
                                break;
                            }
                        }
                    }
                    else
                    {
                        dataByTime.Rows[i]["PN"] = dataPN.Rows[indexPN]["SGM03_PAR"].ToString();
                        dataByTime.Rows[i]["process"] = dataPN.Rows[indexPN]["SGM04"].ToString();
                        if (i > 0)
                        {
                            merge2_count++;
                            dataByTime.Rows[i]["code2"] = " hidden = 'true' ";
                            dataByTime.Rows[index2]["code2"] = " rowspan = '" + merge2_count.ToString() + "' class = '" + dataByTime.Rows[index2]["color"].ToString() + "'";
                        }


                    }
                }

                if(i>0)
                {
                    if (dataByTime.Rows[i]["PN"].ToString() != dataByTime.Rows[i - 1]["PN"].ToString())
                    {
                        count_PN_group++;
                    }
                    if (dataByTime.Rows[i]["process"].ToString() != dataByTime.Rows[i - 1]["process"].ToString())
                    {
                        count_Process_group++;
                    }
                }
                if (count_PN_group % 2 == 0)
                {
                    dataByTime.Rows[i]["color_PN"] = "purple";
                }
                else if (count_PN_group % 2 == 1)
                {
                    dataByTime.Rows[i]["color_PN"] = "light_purple";
                }
                if (count_Process_group % 2 == 0)
                {
                    dataByTime.Rows[i]["color_Process"] = "purple";
                }
                else if (count_Process_group % 2 == 1)
                {
                    dataByTime.Rows[i]["color_Process"] = "light_purple";
                }

                //------------------------------处理筛选栏位逻辑-----------------------------------------------------------------------

                if (i > 0)
                {
                    if (colPLC.Checked == false)
                    {
                        if (colPN.Checked == false && colStatus.Checked == true && colProcess.Checked == false)
                        {
                            if (dataByTime.Rows[i]["dStatus"].ToString() == dataByTime.Rows[i - 1]["dStatus"].ToString())
                            {
                                dataByTime.Rows.RemoveAt(i - 1);
                                i--;
                            }
                        }
                        else if (colPN.Checked == false && colStatus.Checked == true && colProcess.Checked == true)
                        {
                            if (dataByTime.Rows[i]["dStatus"].ToString() == dataByTime.Rows[i - 1]["dStatus"].ToString()
                                && dataByTime.Rows[i]["process"].ToString() == dataByTime.Rows[i - 1]["process"].ToString())
                            {
                                dataByTime.Rows.RemoveAt(i - 1);
                                i--;
                            }

                        }
                        else if (colPN.Checked == true && colStatus.Checked == false && colProcess.Checked == false)
                        {
                            if (dataByTime.Rows[i]["PN"].ToString() == dataByTime.Rows[i - 1]["PN"].ToString())
                            {
                                dataByTime.Rows.RemoveAt(i - 1);
                                i--;
                            }
                        }
                        else if (colPN.Checked == true && colStatus.Checked == false && colProcess.Checked == true)
                        {
                            if (dataByTime.Rows[i]["PN"].ToString() == dataByTime.Rows[i - 1]["PN"].ToString()
                                && dataByTime.Rows[i]["process"].ToString() == dataByTime.Rows[i - 1]["process"].ToString())
                            {
                                dataByTime.Rows.RemoveAt(i - 1);
                                i--;
                            }
                        }
                        else if (colPN.Checked == true && colStatus.Checked == true && colProcess.Checked == false)
                        {
                            if (dataByTime.Rows[i]["dStatus"].ToString() == dataByTime.Rows[i - 1]["dStatus"].ToString()
                                && dataByTime.Rows[i]["PN"].ToString() == dataByTime.Rows[i - 1]["PN"].ToString())
                            {
                                dataByTime.Rows.RemoveAt(i - 1);
                                i--;
                            }
                        }
                        else if (colPN.Checked == true && colStatus.Checked == true && colProcess.Checked == true)
                        {
                            if (dataByTime.Rows[i]["dStatus"].ToString() == dataByTime.Rows[i - 1]["dStatus"].ToString()
                                && dataByTime.Rows[i]["process"].ToString() == dataByTime.Rows[i - 1]["process"].ToString()
                                && dataByTime.Rows[i]["PN"].ToString() == dataByTime.Rows[i - 1]["PN"].ToString())
                            {
                                dataByTime.Rows.RemoveAt(i - 1);
                                i--;
                            }
                        }
                        else if (colPN.Checked == false && colStatus.Checked == false && colProcess.Checked == false)
                        {
                            if (colProcess.Checked == false)
                            {
                                dataByTime.Rows.RemoveAt(i - 1);
                                i--;

                            }
                        }
                        else if (colPN.Checked == false && colStatus.Checked == false && colProcess.Checked == true)
                        {
                            if (dataByTime.Rows[i]["process"].ToString() == dataByTime.Rows[i - 1]["process"].ToString())
                            {
                                dataByTime.Rows.RemoveAt(i - 1);
                                i--;
                            }
                        }

                        
                    }
                }
            }
            //------------------------------处理筛选栏位逻辑-----------------------------------------------------------------------

            //for (int i = 1; i < dataByTime.Rows.Count-1; i++)
            //{

            //    if (colPLC.Checked == false)
            //    {
            //        if (colPN.Checked == false && colStatus.Checked == true)
            //        {
            //            if (colProcess.Checked == false)
            //            {
            //                if (dataByTime.Rows[i]["dStatus"].ToString() == dataByTime.Rows[i - 1]["dStatus"].ToString()
            //                    && dataByTime.Rows[i]["dStatus"].ToString() == dataByTime.Rows[i + 1]["dStatus"].ToString())
            //                {
            //                    dataByTime.Rows.RemoveAt(i);
            //                    i--;
            //                }
            //            }
            //            else
            //            {
            //                if (dataByTime.Rows[i]["dStatus"].ToString() == dataByTime.Rows[i - 1]["dStatus"].ToString()
            //                  && dataByTime.Rows[i]["process"].ToString() == dataByTime.Rows[i - 1]["process"].ToString()
            //                  && dataByTime.Rows[i]["dStatus"].ToString() == dataByTime.Rows[i + 1]["dStatus"].ToString()
            //                  && dataByTime.Rows[i]["process"].ToString() == dataByTime.Rows[i + 1]["process"].ToString())
            //                {
            //                    dataByTime.Rows.RemoveAt(i);
            //                    i--;
            //                }
            //            }
            //        }
            //        else if (colPN.Checked == true && colStatus.Checked == false)
            //        {
            //            if (dataByTime.Rows[i]["PN"].ToString() == dataByTime.Rows[i - 1]["PN"].ToString()
            //                && dataByTime.Rows[i]["PN"].ToString() == dataByTime.Rows[i + 1]["PN"].ToString())
            //            {
            //                dataByTime.Rows.RemoveAt(i);
            //                i--;
            //            }
            //        }
            //        else if (colPN.Checked == true && colStatus.Checked == true)
            //        {
            //            if (dataByTime.Rows[i]["dStatus"].ToString() == dataByTime.Rows[i - 1]["dStatus"].ToString()
            //                && dataByTime.Rows[i]["PN"].ToString() == dataByTime.Rows[i - 1]["PN"].ToString()
            //                && dataByTime.Rows[i]["dStatus"].ToString() == dataByTime.Rows[i + 1]["dStatus"].ToString()
            //                && dataByTime.Rows[i]["PN"].ToString() == dataByTime.Rows[i + 1]["PN"].ToString())
            //            {
            //                dataByTime.Rows.RemoveAt(i);
            //                i--;
            //            }
            //        }
            //        else
            //        {
            //            if (colProcess.Checked == false)
            //            {
            //                dataByTime.Rows.RemoveAt(i);
            //                i--;
            //            }
            //            else
            //            {
            //                if (dataByTime.Rows[i]["process"].ToString() == dataByTime.Rows[i - 1]["process"].ToString()
            //                 && dataByTime.Rows[i]["process"].ToString() == dataByTime.Rows[i + 1]["process"].ToString())
            //                {
            //                    dataByTime.Rows.RemoveAt(i);
            //                    i--;
            //                }
            //            }

            //        }
            //    }
            //}
           
         
           
            return dataByTime;
        }

        protected void btnXem_Click(object sender, EventArgs e)
        {

            query();

        }
        void query()
        {
            DateTime text1;
            DateTime text2;
            try
            {
                text1 = new DateTime(Convert.ToInt32(date1.Text.Substring(6, 4)), Convert.ToInt32(date1.Text.Substring(3, 2)), Convert.ToInt32(date1.Text.Substring(0, 2)), Convert.ToInt32(date1.Text.Substring(11, 2)), Convert.ToInt32(date1.Text.Substring(14, 2)), 0);
                text2 = new DateTime(Convert.ToInt32(date2.Text.Substring(6, 4)), Convert.ToInt32(date2.Text.Substring(3, 2)), Convert.ToInt32(date2.Text.Substring(0, 2)), Convert.ToInt32(date2.Text.Substring(11, 2)), Convert.ToInt32(date2.Text.Substring(14, 2)), 0);
            }
            catch
            {
                showMessage("日期格式不对/Kiểu thời gian không đúng");
                this.date1.Text = pDate.ToString("dd/MM/yyyy HH:mm");
                this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                text1 = new DateTime(Convert.ToInt32(date1.Text.Substring(6, 4)), Convert.ToInt32(date1.Text.Substring(3, 2)), Convert.ToInt32(date1.Text.Substring(0, 2)), Convert.ToInt32(date1.Text.Substring(11, 2)), Convert.ToInt32(date1.Text.Substring(14, 2)), 0);
                text2 = new DateTime(Convert.ToInt32(date2.Text.Substring(6, 4)), Convert.ToInt32(date2.Text.Substring(3, 2)), Convert.ToInt32(date2.Text.Substring(0, 2)), Convert.ToInt32(date2.Text.Substring(11, 2)), Convert.ToInt32(date2.Text.Substring(14, 2)), 0);
            }
            this.mamay.Text = hmamay.Value;

            this.Repeater2.DataSource = this.GetData(this.hmamay.Value, text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            this.Repeater2.DataBind();
        }



        public void showMessage(string mess)
        {
            string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            Response.Write(strBuilder);
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            DateTime text1;
            DateTime text2;
            try
            {
                text1 = new DateTime(Convert.ToInt32(date1.Text.Substring(6, 4)), Convert.ToInt32(date1.Text.Substring(3, 2)), Convert.ToInt32(date1.Text.Substring(0, 2)), Convert.ToInt32(date1.Text.Substring(11, 2)), Convert.ToInt32(date1.Text.Substring(14, 2)), 0);
                text2 = new DateTime(Convert.ToInt32(date2.Text.Substring(6, 4)), Convert.ToInt32(date2.Text.Substring(3, 2)), Convert.ToInt32(date2.Text.Substring(0, 2)), Convert.ToInt32(date2.Text.Substring(11, 2)), Convert.ToInt32(date2.Text.Substring(14, 2)), 0);
            }
            catch
            {
                showMessage("日期格式不对/Kiểu thời gian không đúng");
                this.date1.Text = pDate.ToString("dd/MM/yyyy HH:mm");
                this.date2.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                text1 = new DateTime(Convert.ToInt32(date1.Text.Substring(6, 4)), Convert.ToInt32(date1.Text.Substring(3, 2)), Convert.ToInt32(date1.Text.Substring(0, 2)), Convert.ToInt32(date1.Text.Substring(11, 2)), Convert.ToInt32(date1.Text.Substring(14, 2)), 0);
                text2 = new DateTime(Convert.ToInt32(date2.Text.Substring(6, 4)), Convert.ToInt32(date2.Text.Substring(3, 2)), Convert.ToInt32(date2.Text.Substring(0, 2)), Convert.ToInt32(date2.Text.Substring(11, 2)), Convert.ToInt32(date2.Text.Substring(14, 2)), 0);
            }
            this.mamay.Text = hmamay.Value;

            DataTable data = this.GetData(this.hmamay.Value, text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
          

            //string text = this.date1.Text;
            //string text2 = this.date2.Text;
            string value = this.hmamay.Value;
            //DataTable data = this.GetData(value, Convert.ToDateTime(text), Convert.ToDateTime(text2));
            //DataTable name = DATA3.INSTANCE.GetName(value);
            //if (name.Rows.Count > 0)
            //{
            //    this.mamay.Text = name.Rows[0]["MA_MAY1"].ToString();
            //}
            string arg = "ReportMachine_" + this.mamay.Text + DateTime.Now.ToString("_dd_MM_yyyy") + ".xls";
            base.Response.Clear();
            //base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            base.Response.ContentType = "application/ms-excel";
            base.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", arg));
            base.Response.BinaryWrite(Excel.ReportMachine(data, this.mamay.Text, "FROM: " + text1.ToString("yyyy-MM-dd HH:mm") + " TO: " + text2.ToString("yyyy-MM-dd HH:mm")));
            base.Response.Flush();
            base.Response.End();
        }

        protected void colPLC_CheckedChanged(object sender, EventArgs e)
        {
            //if (colPLC.Checked == false && code_colPLC=="")
            //{
            //    code_colPLC = " hidden ='true' ";
            //    table_width = Convert.ToString(Convert.ToInt32(table_width)- 95);
            //}
            //else if (colPLC.Checked == true && code_colPLC != "")
            //{
            //    code_colPLC = "";
            //    table_width = Convert.ToString(Convert.ToInt32(table_width) + 95);
            //}
            //query();
            scan_col_status();
            query();
        }
        protected void colPN_CheckedChanged(object sender, EventArgs e)
        {
            //if (colPN.Checked == false && code_colPN=="")
            //{
            //    code_colPN = " hidden ='true' ";
            //    table_width = Convert.ToString(Convert.ToInt32(table_width) - 150);
            //}
            //else if (colPN.Checked == true && code_colPN != "")
            //{
            //    code_colPN = "";
            //    table_width = Convert.ToString(Convert.ToInt32(table_width) + 150);
            //}
            //query();
            scan_col_status();
            query();
        }
        protected void colProcess_CheckedChanged(object sender, EventArgs e)
        {
            //if (colProcess.Checked == false && code_colProcess=="")
            //{
            //    code_colProcess = " hidden ='true' ";
            //    table_width = Convert.ToString(Convert.ToInt32(table_width) - 95);
            //}
            //else if (colProcess.Checked == true && code_colProcess != "")
            //{
            //    code_colProcess = "";
            //    table_width = Convert.ToString(Convert.ToInt32(table_width) + 95);
            //}
            //query();
            scan_col_status();
            query();
        }
        protected void colStatus_CheckedChanged(object sender, EventArgs e)
        {

            //if (colStatus.Checked == false && code_colStatus=="")
            //{
            //    code_colStatus = " hidden ='true' ";
            //    table_width = Convert.ToString(Convert.ToInt32(table_width) - 320);
            //}
            //else if(colStatus.Checked == true && code_colStatus != "")
            //{
            //    code_colStatus = "";
            //    table_width = Convert.ToString(Convert.ToInt32(table_width) + 320);
            //}
            //query();
            scan_col_status();
            query();


        }

        void scan_col_status()
        {
            if (colStatus.Checked == false && code_colStatus == "")
            {
                code_colStatus = " hidden ='true' ";
                table_width = Convert.ToString(Convert.ToInt32(table_width) - 320);
            }
            else if (colStatus.Checked == true && code_colStatus != "")
            {
                code_colStatus = "";
                table_width = Convert.ToString(Convert.ToInt32(table_width) + 320);
            }
            if (colProcess.Checked == false && code_colProcess == "")
            {
                code_colProcess = " hidden ='true' ";
                table_width = Convert.ToString(Convert.ToInt32(table_width) - 95);
            }
            else if (colProcess.Checked == true && code_colProcess != "")
            {
                code_colProcess = "";
                table_width = Convert.ToString(Convert.ToInt32(table_width) + 95);
            }
            if (colPN.Checked == false && code_colPN == "")
            {
                code_colPN = " hidden ='true' ";
                table_width = Convert.ToString(Convert.ToInt32(table_width) - 150);
            }
            else if (colPN.Checked == true && code_colPN != "")
            {
                code_colPN = "";
                table_width = Convert.ToString(Convert.ToInt32(table_width) + 150);
            }
            if (colPLC.Checked == false && code_colPLC == "")
            {
                code_colPLC = " hidden ='true' ";
                table_width = Convert.ToString(Convert.ToInt32(table_width) - 95);
            }
            else if (colPLC.Checked == true && code_colPLC != "")
            {
                code_colPLC = "";
                table_width = Convert.ToString(Convert.ToInt32(table_width) + 95);
            }
            
        }
    }
}