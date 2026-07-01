using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CXWeb.kanban
{
    public partial class kanban_zhizao3 : System.Web.UI.Page
    {
        private static DataTable mydata = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                query();
            }
        }

        void query()
        {
            connectTT myconn = new connectTT();
            myconn.myopen();

            string strsql = "";
            strsql = "select to_char(tc_ohc02,'yyyy/MM/dd') 客訴日期, tc_ohc08 料號, tc_ohc32 客訴內容, "
                + "to_char((select tc_ohd08 from tc_ohd_file where tc_ohc01 = tc_ohd01 and tc_ohd02 = '1'),'yyyy/MM/dd') 出貨日期, "
                + "'' 責任單位 "
                + "from tc_ohc_file "
                + "where tc_ohcconf = 'Y' and tc_ohc03 <> '3' "
                + "order by tc_ohc02 desc";
            mydata = myconn.mysearch(strsql);
            GridView1.DataSource = mydata;
            GridView1.DataBind();
            Timer1.Enabled = true;
            try { myconn.myclose(); } catch { return; }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (GridView1.PageIndex == GridView1.PageCount - 1)
                GridView1.PageIndex = 0;
            else
                GridView1.PageIndex = GridView1.PageIndex + 1;
            GridView1.DataSource = mydata;
            GridView1.DataBind();
        }
    }
}