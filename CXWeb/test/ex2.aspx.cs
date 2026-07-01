using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Services;
namespace CXWeb.test
{
    public partial class ex2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbTime.Text = DateTime.Now.ToString();
            }
        }

        [WebMethod]
        public static string GetTime()
        {
            return DateTime.Now.ToString();
        }
    }
}