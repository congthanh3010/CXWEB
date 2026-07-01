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


namespace CXWeb.schedule
{
    public partial class product_required_quantity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                product_no.Text = base.Request.QueryString["pr"];
                query();
            }
        }
        void query()
        {
            connectTT myconn = new connectTT();
            connectEIP myconn_SQL = new connectEIP();
            string strsql = "";
            string strsql2 = "";

            //-------------------------------------------------------------

            DataTable mydata = new DataTable();
            DataTable mydata2 = new DataTable();
            //myconn.myopen();


            //strsql = "SELECT oeb04 料號,oeb01 訂單單號,oeb03 項次,oea04 客戶,occ02 簡稱,to_char(oeb15, 'yyyy/MM/dd') 交貨日期,oeb12 收訂量,oeb05 單位," +
            //         " oeb12 - oeb24 + oeb25 - oeb26 未出貨量,to_char(oeb16, 'yyyy/MM/dd')排定交貨日, oeb24 已出貨量,oeb23 待出貨量, oea72 確認日" +
            //         " FROM oeb_file, oea_file, occ_file" +
            //         " WHERE oeb04 = '"+product_no.Text+"'" +
            //         " AND oeb01 = oea01 AND oea04 = occ01 AND oea00<> '0'" +
            //         " AND oeb70 = 'N' AND oeb12-oeb24 + oeb25 - oeb26 > 0  AND oeaconf = 'Y'  and(oeb12 - oeb24 + oeb25 - oeb26) > 0" +
            //         " union" +
            //         " select  opd01,'FORECAST' as oeb01,opd05,opd02,occ02,null as oeb15,opd08,null as oeb05 ,opd08 as oeb12,to_char(opd06, 'yyyy/MM/dd') ,null,null,null" +
            //         " from opd_file, occ_file where opd01 = '" + product_no.Text + "' and opd08 <> 0 AND opd02 = occ01" +
            //         " and opd03 in (select max(opd03) from opd_file where opd08 <> 0 and opd01 = '" + product_no.Text + "')" +
            //         " ORDER BY 排定交貨日 ";

            //mydata = myconn.mysearch(strsql);


            //SqlCommand cmd = new SqlCommand("sp_FilterOrder", myconn_SQL.conn);
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@ITEM_NO", product_no.Text);

            //myconn_SQL.myopen();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);

            //da.Fill(mydata);
            //myconn_SQL.myclose();
            System.Collections.Hashtable htPara = new System.Collections.Hashtable();
            htPara["ITEM_NO"] = product_no.Text;

            mydata = myconn_SQL.ExecuteReturnDt("sp_FilterOrder", htPara, CommandType.StoredProcedure);


            GridView1.DataSource = mydata;
            GridView1.DataBind();

            int sum_order_quantity = 0;
            for (int i=0;i< mydata.Rows.Count;i++)
            {
                sum_order_quantity += Convert.ToInt32(mydata.Rows[i]["Unshipped_Quat"]);
                finish_date.Text = mydata.Rows[0]["Sche_Delivery"].ToString();
            }
            stage.Text = "B0";
            wip_quantity.Text = "0";
            order_quantity.Text = sum_order_quantity.ToString();


            //tim wip

            strsql2 = "select SGM03_PAR 料件,SGM03 序號,SGM04 作業編號,SGM45 作業名稱,WIPQTY 未完成數量 " +
                     " ,Sum(WIPQTY)OVER(ORDER BY  sgm03_par, sgm03, sgm04) as WIP累計" +
                     " ,SGM301 良品轉入, SGM311 良品轉出" +
                     " ,SGM313 當站報廢 from v_sgmimg" +
                     " where sgm03_par like '%" + product_no.Text.Substring(1,7) + "%'" +
                     " order by sgm03_par, sgm03, sgm04  ";

            mydata2 = myconn.mysearch(strsql2);

            GridView2.DataSource = mydata2;
            GridView2.DataBind();  

            myconn.myclose();

          
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            query();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            query();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {




        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            query();
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/schedule/order_list.aspx");
        }
        protected void btnSchedule_Click(object sender, EventArgs e)
        {
            //string url = "window.open('/schedule/DBR.aspx?pr="+ product_no.Text+ "&oqty="+ order_quantity .Text+ "&wqty="+ wip_quantity .Text+ "&d="+ finish_date.Text+ "&st="+ stage .Text+ "','_newtab');";
            //Page.ClientScript.RegisterStartupScript(
            //this.GetType(), "OpenWindow", url, true);

            Response.Redirect("/schedule/DBR.aspx?pr=" + product_no.Text + "&oqty=" + order_quantity.Text + "&wqty=" + wip_quantity.Text + "&d=" + finish_date.Text + "&st=" + stage.Text);



        }
    }
}