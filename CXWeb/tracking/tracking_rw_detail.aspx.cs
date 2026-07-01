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

namespace CXWeb.tracking
{
    public partial class tracking_rw_detail : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        protected void Page_Load(object sender, EventArgs e)
        {
            string yzd_string = base.Request.QueryString["yzd"];
            if (!IsPostBack && yzd_string != null)
            {
               
                query(yzd_string);
            }
        }
        void query(string yzd_string)
        {
            string strsql = "";
            yzd_string = "('"+yzd_string.Replace(",", "', '")+"')";

            //-------------------------------------------------------------

            DataTable mydata = new DataTable();
            myconn.myopen();
            
            strsql = " select to_char(shb02,'yyyy/MM/dd') as shb02,shb021,shb16,shb05,shb082,shb09,shb04||'-'||gen02 as shb04 from shb_file left outer join gen_file on shb04=gen01   where shb01 in " + yzd_string+"order by shb02,shb021";
            mydata = myconn.mysearch(strsql);


           
            GridView1.DataSource = mydata;

            GridView1.DataBind();


            myconn.myclose();
        }
    }
}