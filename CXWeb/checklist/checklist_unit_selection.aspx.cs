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

namespace CXWeb.checklist
{
    public partial class checklist_unit_selection : System.Web.UI.Page
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
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=CT1");
        }
        protected void btnCT2_DD_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=CT2_DD");
        }
        protected void btnCT2_LH_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=CT2_LH");
        }
        protected void btnCT2_URPC_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=CT2_URPC");
        }
        protected void btnCT3_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=CT3");
        }
        protected void btnCT3_ML_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=CT3_ML");
        }
        protected void btnCT3_DG_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=CT3_DG");
        }
        protected void btnDDCX_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=DDCX");
        }
        protected void btnNC_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=NC");
           
        }
        protected void btnCNC_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=CNC");

        }
        protected void btnGC_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=GC");
        }
        protected void btnXL_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=XL");
        }
        protected void btnXT_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=XT");
        }
        protected void btnXD_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su=XD");
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_unit_report.aspx");
        }
    }
}