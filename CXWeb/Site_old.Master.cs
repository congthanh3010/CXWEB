using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CXWeb
{
    public partial class Site_old : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            //this.Literal1.Text = (base.Request.Url.GetLeftPart(UriPartial.Path).Contains("/sys2") ? "<li><a href=\"http://cxtechnology.vn/sys2/default.aspx\">Home</a></li>" : "<li><a href = \"http://cxtechnology.vn/sys/default.aspx\" > Home </a></li>");
            //this.Literal2.Text = (base.Request.Url.GetLeftPart(UriPartial.Path).Contains("/sys2") ? "<li><a href=\"http://cxtechnology.vn/sys\">Forging</a></li>" : "<li><a href = \"http://cxtechnology.vn/sys2\" > Machining </a></li>");
            //this.Literal3.Text = (base.Request.Url.GetLeftPart(UriPartial.Path).Contains("/sys2") ? "<li><a href=\"http://cxtechnology.vn/sys2/report.aspx\">Machine</a></li>" : "<li><a href = \"http://cxtechnology.vn/sys/report.aspx\" > Machine </a></li>");
            //this.Literal4.Text = (base.Request.Url.GetLeftPart(UriPartial.Path).Contains("/sys2") ? "<li><a href=\"http://cxtechnology.vn/sys2/reports.aspx\">Report System</a></li>" : "<li><a href = \"http://cxtechnology.vn/sys/reports.aspx\" > Report System </a></li>");
            //this.Literal5.Text = (base.Request.Url.GetLeftPart(UriPartial.Path).Contains("/sys2") ? "<li><a href=\"http://cxtechnology.vn/sys2/user.aspx\">User Management</a></li>" : "<li><a href = \"http://cxtechnology.vn/sys/user.aspx\" > User Management </a></li>");
        }
        protected void lbtThoat_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            base.Response.Redirect("http://cxtechnology.vn");
        }
    }
}