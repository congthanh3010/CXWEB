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
        private string page;
        private string unit;

        protected void Page_Load(object sender, EventArgs e)
        {
            page = base.Request.QueryString["page"];
            unit = base.Request.QueryString["u"];
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
            strsql = "select to_char(tc_ohc02,'yyyy/MM/dd') 客訴日期, tc_ohc08 料號, \n"
                //+ "tc_ohc32 客訴內容, "
                + "tc_ohc31||'-'||(select tc_oaf03 from tc_oaf_file where tc_oaf01 = tc_ohc30 and tc_oaf02 = tc_ohc31) 客訴內容, \n"
                //+ "to_char((select tc_ohd08 from tc_ohd_file where tc_ohc01 = tc_ohd01 and tc_ohd02 = '1'),'yyyy/MM/dd') 出貨日期, \n"
                //+ "'' 責任單位 \n"
                + "tc_ohcud02 責任單位, tc_ohc214 機台編號, \n"
                + "tc_ohc215||'-'||gen02 作業員編號 \n"
                + "from tc_ohc_file \n"
                + "inner join gen_file ON gen01=tc_ohc215\n"
                + "where tc_ohcconf = 'Y' and tc_ohc03 <> '3'";
            if (unit != null && unit.Trim() != "")
                strsql += "\nand gen03='" + unit + "'";
            strsql += "\norder by tc_ohc02 desc";
            mydata = myconn.mysearch(strsql);
            GridView1.DataSource = mydata;
            GridView1.DataBind();
            Timer1.Enabled = true;
            try { myconn.myclose(); } catch { return; }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (GridView1.PageIndex == GridView1.PageCount - 1)
            {
                if (page == null && page != "")
                    GridView1.PageIndex = 0;
                else
                {
                    //if (page == "kanban_zhizao2")
                    //    Response.Redirect($"/kanban/{page}.aspx");
                    //else if (page == "kanban_zhizao")
                    //    Response.Redirect($"/kanban/kanban_zhizao4.aspx?page={page}&u={unit}");
                    //else
                    //    GridView1.PageIndex = 0;
                    string url = $"/kanban/healthy_notice.aspx?page={page}";
                    if (unit != null && unit != "")
                        url += $"&u={unit}";
                    Response.Redirect(url);
                }
            }
            else
                GridView1.PageIndex = GridView1.PageIndex + 1;
            GridView1.DataSource = mydata;
            GridView1.DataBind();
        }
    }
}