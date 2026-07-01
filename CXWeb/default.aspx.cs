using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace CXWeb
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("/plc/mr.aspx");
            //Response.Redirect("/kanban/main.aspx");
            //Response.Redirect("/tracking/tracking_product_selection.aspx");
            //Response.Redirect("/test/cs.aspx");
            //Response.Redirect("/sys/report28.aspx");
            Response.Redirect("/sys/main.aspx");
            //Response.Redirect("/checklist/login.aspx");
        }
      
    }
}