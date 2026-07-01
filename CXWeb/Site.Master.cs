using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CXWeb
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected static string code_menu = "submenu";
        protected void Page_Load(object sender, EventArgs e)
        {
          if(MainContent.Page.ToString()=="ASP.sys_index_aspx")
            {
                code_menu = "null";
            }
          else
            {
                code_menu = "submenu";
            }
        }
      
    }
}