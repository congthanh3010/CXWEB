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
using System.Text.RegularExpressions;

namespace CXWeb.schedule
{
    public partial class machine_stoptime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                textDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                stop_time.Text = "0";
                //textShift.SelectedValue = "CA1";
                query();
            }
        }
        void query()
        {
            connectEIP myconn = new connectEIP();

            string strsql = "";
           
            //-------------------------------------------------------------

            DataTable mydata = new DataTable();
            DataTable mydata2 = new DataTable();
            myconn.myopen();


            //strsql = "select convert(varchar, date, 111) date,machine_no,stop_time from schedule_machine_stoptime where machine_no ='" + machine_no.Text + "' order by date";

            //mydata = myconn.mysearch(strsql);

            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["MACHINE_NO"] = machine_no.Text;

            mydata = myconn.ExecuteReturnDt("sp_Query_Machine_stop", htPara, CommandType.StoredProcedure);


            GridView1.DataSource = mydata;
            GridView1.DataBind();

          


            myconn.myclose();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            query();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            query();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {




        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            query();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable mydata = new DataTable();
            DataTable machine_data = new DataTable();
            string sql = "";
            connectEIP myconn = new connectEIP();

            myconn.myopen();

            sql = "  select a.* " +
                 " FROM OPENQUERY(TIPTOP, " +
                 " 'select * " +
                 " from eci_file t " +
                 " where t.eciacti=''Y'' " +
                 " and t.eci01 = ''" + machine_no.Text + "'''   ) a";
            machine_data = myconn.mysearch(sql);
            if (machine_data.Rows.Count == 0)
            {
                hreport_msg.Value = "機台編號錯誤/The Machine_No is not available！";
                //showMessage(report_msg);
                return;
            }
            if (IsNumber(stop_time.Text) == false)
            {
                hreport_msg.Value = "Stop_Time is not numberic!!!";
            }
            else
            {
                myconn.mySqlExecute("delete from schedule_machine_stoptime where machine_no ='" + machine_no.Text + "' and date ='" + textDate.Text + "'");

                myconn.mySqlExecute("insert into schedule_machine_stoptime (date,machine_no,stop_time) values ('" + textDate.Text + "','" + machine_no.Text + "','" + stop_time.Text + "') ");

                myconn.myclose();

                hreport_msg.Value = " ";

                query();
            }
            
        }

        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/main.aspx");
        }
    }
}