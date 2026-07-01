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
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CXWeb.plc
{
    public partial class Machine_Status_Report : System.Web.UI.Page
    {
        connectDB myconn = new connectDB();
        protected static string report_msg = " ";
        protected static string s_machine_status = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                report_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                report_dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                GetTramList();
                GetMachineList();
                //AddTime();
                //AddMaNv();
                ////txtHour.Text = strHours;
                ////txtMin.Text = strMin;

                //SetTimeAll();
            }
        }


        private void GetMachineList()
        {
            DataTable mydata = new DataTable();
            myconn.myopen();
            mydata = myconn.mysearch("select * from PLC_MachineList where DepID = '" + txtTram.Text + "'");


            txtMachine_no.DataTextField = "MachineId";
            txtMachine_no.DataValueField = "MachineId";
            txtMachine_no.DataSource = mydata;
            txtMachine_no.DataBind();
        }
        private void GetTramList()
        {
            DataTable mydata = new DataTable();
            myconn.myopen();
            mydata = myconn.mysearch("select distinct DepId from PLC_MachineList");


            txtTram.DataTextField = "DepId";
            txtTram.DataValueField = "DepId";
            txtTram.DataSource = mydata;
            txtTram.DataBind();
        }

        protected void txtTram_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMachineList();
            report_msg = "";
        }
    }
}