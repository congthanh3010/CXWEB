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

namespace CXWeb.plc
{
    public partial class machine_status_upload : System.Web.UI.Page
    {
        connectDB myconn = new connectDB();
        protected static string report_msg = " ";
        protected static string s_machine_status = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            s_machine_status= machine_status.SelectedValue;
            
            DataTable mydata = new DataTable();
            myconn.myopen();
            mydata = myconn.mysearch("select cCode, (cCode+':'+cDesc) as cDesc   from MACHINE_STATUS_CODE ");
            

            machine_status.DataTextField = "cDesc";
            machine_status.DataValueField = "cCode";
            machine_status.DataSource = mydata;
            machine_status.DataBind();

            myconn.myclose();
            if (!IsPostBack)
            {
                report_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void BindData()
        {
            DataTable mydata = new DataTable();
            myconn.myopen();
            mydata = myconn.mysearch("select SUBSTRING(CONVERT(nvarchar,dStTimeStart,120),0,17) as dStTimeStart,dShift,dMachine,cDesc,dStTime from MACHINE_STATUS  left outer join MACHINE_STATUS_CODE on dStatus = cCode where dMachine ='" + machine_no.Text + "' and dStTimeStart > '"+ report_date.Text + "' order by dStTimeStart asc ");
            gvData.DataSource = mydata;
            gvData.DataBind();
            myconn.myclose();
        }

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //string str = gvData.DataKeys[e.RowIndex].Value.ToString();
            //myconn.myopen();
            //myconn.mySqlExecute("delete from COD_CODE where class ='report5' and code = '" + str + "' ");
            //myconn.myclose();
            //BindData();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            machine_status.SelectedValue = s_machine_status;
            string sql = "";
            string test_hour = "";
            string test_min = "";
            DateTime beforeSt_Time_from;
            DateTime beforeSt_Time_to;
            TimeSpan s_stTime;
            DataTable mydata = new DataTable();
            DataTable machine_data = new DataTable();
            //校验日期格式

            try
            {
                test_hour = status_time_start.Text.Substring(0, 2);
                test_min = status_time_start.Text.Substring(3, 2);
                if (Convert.ToInt32(test_hour) < 0 || Convert.ToInt32(test_hour) > 23
                    || Convert.ToInt32(test_min) < 0 || Convert.ToInt32(test_min) > 59)
                {                 
                    report_msg = "Thời gian nhập sai";
                    //showMessage(report_msg);
                    return;
                }
            }
            catch
            {               
                report_msg = "Thời gian nhập sai";
                //showMessage(report_msg);
                return;
            }
            string s_st_time_start = report_date.Text + " " + status_time_start.Text;
            string s_shift = "";
            if (Convert.ToInt32(test_hour) >= 6 && Convert.ToInt32(test_hour) < 18)
            {
                s_shift = "CA1";
            }
            else
                s_shift = "CA2";
            //校验机台编号
            sql = "  select a.* " +
                   " FROM OPENQUERY(TIPTOP, " +
                   " 'select * " +
                   " from eci_file t " +
                   " where t.eciacti=''Y'' " +
                   " and t.eci01 = ''" + machine_no.Text + "'''   ) a";
            machine_data = myconn.mysearch(sql);
            if (machine_data.Rows.Count == 0)
            {
                report_msg = "Tên máy nhập sai";
                //showMessage(report_msg);
                return;
            }
            //计算状态时间 

            myconn.myopen();
            mydata = myconn.mysearch("SELECT TOP (1)* FROM MACHINE_STATUS where dMachine ='"+ machine_no.Text + "' order by dStTimeStart desc ");      
            myconn.myclose();
           
            beforeSt_Time_to = Convert.ToDateTime(s_st_time_start);

            if (mydata.Rows.Count != 0)
            {
                beforeSt_Time_from = Convert.ToDateTime(mydata.Rows[0]["dStTimeStart"].ToString());
                if (beforeSt_Time_from > beforeSt_Time_to)
                {
                    report_msg = "Không thể báo thời điểm trước " + beforeSt_Time_from.ToString("yyyy-MM-dd HH:mm");;
                    //showMessage(report_msg);
                    BindData();
                    return;
                }
            }
            else
            {
                beforeSt_Time_from = beforeSt_Time_to;
            }

           

           
            if (beforeSt_Time_to>DateTime.Now)
            {
                report_msg = "Không thể báo thời điểm sau " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                //showMessage(report_msg);
                BindData();
                return;
            }

            s_stTime = beforeSt_Time_to.Subtract(beforeSt_Time_from);
            
            //执行新增
            myconn.myopen();
            //myconn.mySqlExecute("insert into COD_CODE (class,code,description,description2) values ('report5',N'" + mat_no.Text + "',N'" + remark.Text + "',N'" + remark2.Text + "') ");
            myconn.mySqlExecute("delete from MACHINE_STATUS where dMachine ='"+ machine_no.Text + "' and dStTimeStart = '" + s_st_time_start + ":00.000' ");
            myconn.mySqlExecute("insert into MACHINE_STATUS (dMachine,dStTimeStart,dShift,dStatus,dCreateTime) values ('" + machine_no.Text + "','" + s_st_time_start + "','" + s_shift + "','" + s_machine_status + "','"+DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "') ");
            myconn.mySqlExecute("update MACHINE_STATUS set dStTime='"+ s_stTime.TotalMinutes + "' where dMachine ='" + machine_no.Text + "' and dStTimeStart = '" + beforeSt_Time_from.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            myconn.myclose();
            BindData();
            
            report_msg = " ";
        }
        public void showMessage(string mess)
        {
            string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            Response.Write(strBuilder);
        }
    }
}