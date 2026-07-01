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

namespace CXWeb.schedule
{
    public partial class main : System.Web.UI.Page
    {
        connectEIP myconn = new connectEIP();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
            

            }
        }        
        
        protected void btnCT1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/machine_stoptime_V2.aspx");
        }
        protected void btnSampleOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/order_hangmau.aspx");
        }
        protected void btnCT2_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/order_list.aspx");
        }
        protected void btnCT3_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/ma_prod_setting.aspx");
        }
        protected void btnCT7_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/prod_Station.aspx");
        }
        protected void btnCT4_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/ma_schedule_result.aspx");
        }
        protected void btnCT5_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/ma_history.aspx");
        }
        protected void btnCT6_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/prod_attrition_rate.aspx");
        }
        protected void btnTrungCong_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/order_TrungCong.aspx");
        }
    }
}