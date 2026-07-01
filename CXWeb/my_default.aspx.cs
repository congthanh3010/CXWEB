using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CXWeb
{
    public partial class my_default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("http://eip.cxtechnology.vn:8080/CXHOME/login");
            Response.Redirect("/sys/main.aspx");
        }
    }
}