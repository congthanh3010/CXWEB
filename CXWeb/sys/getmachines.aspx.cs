using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace CXWeb.sys
{
    public partial class getmachines : System.Web.UI.Page
    {
        connectDB myconn = new connectDB();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string text = base.Request.QueryString["m"];
            if (text != null)
            {
                myconn.myopen();

                //string strsql = "  select cmachine from [PLC_PLCM_CX] where clocate='"+ text+"'";
                string strsql = string.Format("SELECT MachineId FROM PLC_MachineList WHERE DepId = '{0}'", text);

                this.repeater1.DataSource = myconn.mysearch(strsql);
                this.repeater1.DataBind();

                myconn.myclose();
                this.repeater1.DataBind();
            }
          
           

        }
    }
}