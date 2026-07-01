using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CXWeb
{
    public partial class Site_plc : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //this.Literal1.Text = (base.Request.Url.GetLeftPart(UriPartial.Path).Contains("/sys2") ? "<li><a href=\"/sys2/default.aspx\">Home</a></li>" : "<li><a href = \"/sys/default.aspx\" > Home </a></li>");
            this.Literal2.Text = (base.Request.Url.GetLeftPart(UriPartial.Path).Contains("/sys2") ? "<li><a href=\"/sys\">Forging</a></li>" : "<li><a href = \"/plc/machine_status_upload_New.aspx\" > Machining </a></li>");
            this.Literal3.Text = (base.Request.Url.GetLeftPart(UriPartial.Path).Contains("/sys2") ? "<li><a href=\"/sys2/Report.aspx\">Machine</a></li>" : "<li><a href = \"/plc/layout.aspx\" > Machine </a></li>");
            this.Literal4.Text = (base.Request.Url.GetLeftPart(UriPartial.Path).Contains("/sys2") ? "<li><a href=\"/sys2/reports.aspx\">Report System</a></li>" : "<li><a href = \"/sys/Report.aspx\" > Report System </a></li>");
            this.Literal5.Text = (base.Request.Url.GetLeftPart(UriPartial.Path).Contains("/sys2") ? "<li><a href=\"/sys2/main.aspx\">User Management</a></li>" : "<li><a href = \"/sys/main.aspx\" > User Management </a></li>");
            this.Literal6.Text = (base.Request.Url.GetLeftPart(UriPartial.Path).Contains("/sys2") ? "<li><a href=\"/sys/mcReport.aspx\">Machine Report</a></li>" : "<li><a href = \"/sys/mcReport.aspx\" > Machine Report </a></li>");
        }
        protected void lbtThoat_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            base.Response.Redirect("http://eip.cxtechnology.vn/sys/main.aspx");
        }
    }
}