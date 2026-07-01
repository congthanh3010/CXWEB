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
    public partial class tracking_qa_detail : System.Web.UI.Page
    {
        connectTT myconn = new connectTT();
        protected void Page_Load(object sender, EventArgs e)
        {
            string dh_string = base.Request.QueryString["dh"];
            if (!IsPostBack && dh_string != null)
            {
               
                query(dh_string);
            }
        }
        void query(string dh_string)
        {
            string strsql = "";
            dh_string = "('"+dh_string.Replace(",", "', '")+"')";

            //-------------------------------------------------------------

            DataTable mydata = new DataTable();
            myconn.myopen();
            
            strsql = " select tc_qcm01,to_char(tc_qcm09,'yyyy/MM/dd') as tc_qcm09,tc_qcm02,tc_qcm04,tc_qcm05,tc_qcm06,tc_qcm10||'-'||gen02 as tc_qcm10,NVL(tc_imq02,'無資料') as tc_imq02,NVL(tc_imq03,'無資料') as tc_imq03" +
                     " from tc_qcm_file" +
                     " left outer join gen_file on tc_qcm10 = gen01" +
                     " left outer join tc_imq_file on tc_imq01 = tc_qcm05 where tc_qcm01 in " + dh_string+ "order by tc_qcm09";
            mydata = myconn.mysearch(strsql);


           
            GridView1.DataSource = mydata;

            GridView1.DataBind();


            myconn.myclose();
        }
    }
}