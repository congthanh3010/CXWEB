using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CXWeb
{
    public partial class Site_checklist : System.Web.UI.MasterPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

         

            string date;
            string shift;
           
            if (DateTime.Now.Hour >= 18)
            {
                shift = "晚班/CA2";
                date = DateTime.Now.ToString("yyyy/MM/dd");
            }
            else if (DateTime.Now.Hour < 6)
            {
                shift = "晚班/CA2";
                date = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            }
            else
            {
                shift = "早班/CA1";
                date = DateTime.Now.ToString("yyyy/MM/dd");
            }


            labLoginName.Text = (string)Session["LoginUserInfo"];
            Labdate.Text = date + " " + shift;


        }
      
    }
}