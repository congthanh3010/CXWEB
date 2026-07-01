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

namespace CXWeb.kanban
{
    public partial class main : System.Web.UI.Page
    {
        connectEIP myconn = new connectEIP();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
            

            }
        }        
        
        protected void btnCT1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/kanban/kanban_zhizao.aspx?u=2FN201");
        }
        protected void btnCT2_Click(object sender, EventArgs e)
        {
            Response.Redirect("/kanban/kanban_zhizao.aspx?u=2FN202");
        }
        protected void btnCT3_Click(object sender, EventArgs e)
        {
            Response.Redirect("/kanban/kanban_zhizao.aspx?u=2FN501");
        }
        protected void btnCT4_Click(object sender, EventArgs e)
        {
            Response.Redirect("/kanban/kanban_zhizao2.aspx");
        }
        protected void btnCT5_Click(object sender, EventArgs e)
        {
            Response.Redirect("/kanban/setting_zhizao.aspx");
        }
        protected void btnQA_Click(object sender, EventArgs e)
        {
            Response.Redirect("/kanban/kanban_zhizao3.aspx");
        }
    }
}