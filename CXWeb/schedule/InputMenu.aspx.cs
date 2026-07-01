using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CXWeb.schedule
{
    public partial class InputMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/U_work_log.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/wash_material_log.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/xi_lan_log.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/xi_treo_log.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/xi_ma_report.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("/BaoCongA/FrmCheckIn.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/packing.aspx");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/QC_Machine_status.aspx");
        }
        protected void Button9_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/QA_HoaChat_Chart.aspx");
        }
    }
}