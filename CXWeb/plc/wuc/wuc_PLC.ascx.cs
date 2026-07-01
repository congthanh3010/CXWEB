using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CXWeb.plc.wuc
{
    public partial class wuc_PLC : System.Web.UI.UserControl
    {
        private static List<string> lstTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            loadLayout();
        }

        private void loadLayout()
        {
            lstTitle = new List<string>();
            DateTime now = DateTime.Now;
            var lstCA1 = new List<string>() { "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "%" };
            var lstCA2 = new List<string>() { "18", "19", "20", "21", "22", "23", "00", "01", "02", "03", "04", "05", "%" };
            if (now.Hour >= 6 && now.Hour < 18)
                lstTitle = lstCA2.Union(lstCA1).ToList();
            else
                lstTitle = lstCA1.Union(lstCA2).ToList();
            Repeater1.DataSource = lstTitle;
            Repeater1.DataBind();
        }
    }
}