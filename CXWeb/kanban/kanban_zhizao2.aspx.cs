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
    public partial class kanban_zhizao2 : System.Web.UI.Page
    {
        
        private static DataTable mydata = new DataTable();
        //private static int index = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                head_label.Text = "製造後端（一般加工、CNC、電鍍、包裝)</br>Chế tạo bước cuối (Gia công thông thường, CNC, Xi mạ, Đóng gói)";
                
                query();

                search_green();
             
                query2(Convert.ToInt32(h_index.Value));            



            }

        }
        void query()
        {
           

            string strsql = "";


            h_index.Value = "0";
            h_index_old.Value = (GridView1.PageSize - 1).ToString();// bao dam lan dau ko bien xanh




            connectTT myconn = new connectTT();
            myconn.myopen();

            //---------------------------------------------------------------

            strsql = "select * from (select a.ogb04 料號, a.oga03 客戶代碼, a.oga032 客戶名稱, NVL(to_char(a.ogb12,'999,999,999'),'0') 出貨數量, NVL(to_char(b.kuncun,'999,999,999'),0) 庫存數量, a.ogb12 - b.kuncun SL_CL,row_number() over(order by a.ogb12 - b.kuncun desc nulls last) rnm" +
                    "\n , to_char(c.wipqty_baozhuang,'999,999,999') 包裝, to_char(d.wipqty_diandu,'999,999,999') 電鍍, to_char(e.wipqty_jiagong,'999,999,999') 加工, to_char(f.wipqty_jiagongqian,'999,999,999') 加工前" +
                    "\n from" +
                    "\n (" +
                    "\n select ogb04, oga03, oga032, sum(ogb12) ogb12" +
                    "\n from" +
                    "\n (" +
                    "\n select oga.oga09, oga.oga01, oga.oga02, oga.oga021, oga.oga03, oga.oga032, ogb.ogb04, ogb.ogb12 from ogb_file ogb" +
                    "\n inner join oga_file oga on oga01 = ogb01 and ogaconf = 'Y' and ogapost = 'N' and oga09 in ('1') and oga55 = '1'" +
                    "\n inner join oea_file oea on oea.oea01 = ogb.ogb31 and  oeaconf = 'Y' and  oea49 = '1'" +
                    "\n )" +
                    "\n group by ogb04, oga03, oga032) a" +
                    "\n left outer join (select img01, nvl(sum(img21 * img10), 0) kuncun from img_file group by img01) b on b.img01 = a.ogb04" +
                    "\n left outer join" +
                    "\n (" +
                    "\n select sum(sgm301 + sgm302 + sgm303 + sgm304 - sgm311 - sgm312 - sgm313 - sgm314 - sgm316 - sgm317) wipqty_baozhuang" +
                    "\n , shm05" +
                    "\n from shm_file" +
                    "\n left outer" +
                    "\n join sgm_file  on sgm01 = shm01" +
                    "\n where shm28 = 'N'" +
                    "\n and(sgm301 + sgm302 + sgm303 + sgm304 - sgm311 - sgm312 - sgm313 - sgm314 - sgm316 - sgm317) > 0" +
                    "\n and sgm45 like N'包裝%'" +
                    "\n group by shm05" +
                    "\n ) c" +
                    "\n on c.shm05 = a.ogb04" +
                    "\n left outer join" +
                    "\n (" +
                    "\n select sum(sgm301 + sgm302 + sgm303 + sgm304 - sgm311 - sgm312 - sgm313 - sgm314 - sgm316 - sgm317) wipqty_diandu" +
                    "\n , shm05" +
                    "\n from shm_file" +
                    "\n left outer join sgm_file  on sgm01 = shm01" +
                    "\n where shm28 = 'N'" +
                    "\n and(sgm301 + sgm302 + sgm303 + sgm304 - sgm311 - sgm312 - sgm313 - sgm314 - sgm316 - sgm317) > 0" +
                    "\n and(sgm45 like N'滾電%' or sgm45 like N'吊電%' or sgm45 like N'吊电%' or sgm45 like N'電著%')" +
                    "\n group by shm05" +
                    "\n ) d" +
                    "\n on d.shm05 = a.ogb04" +
                    "\n left outer join" +
                    "\n (" +
                    "\n select sum(sgm301 + sgm302 + sgm303 + sgm304 - sgm311 - sgm312 - sgm313 - sgm314 - sgm316 - sgm317) wipqty_jiagong" +
                    "\n , shm05" +
                    "\n from shm_file" +
                    "\n left outer join sgm_file  on sgm01 = shm01" +
                    "\n where shm28 = 'N'" +
                    "\n and(sgm301 + sgm302 + sgm303 + sgm304 - sgm311 - sgm312 - sgm313 - sgm314 - sgm316 - sgm317) > 0" +
                    "\n and(sgm45 like N'CNC%' or sgm45 like N'加工%')" +
                    "\n group by shm05" +
                    "\n ) e" +
                    "\n on e.shm05 = a.ogb04" +
                    "\n left outer join" +
                    "\n (" +
                    "\n select i.modelver, sum(qty) as wipqty_jiagongqian" +
                    "\n from" +
                    "\n (select f_1444_modelver(sgm03_par) modelver, sgm03_par," +
                    "\n (sgm301 + sgm302 + sgm303 + sgm304 - sgm311 - sgm312 - sgm313 - sgm314 - sgm316 - sgm317) as qty, sgm04" +
                    "\n from sgm_file" +
                    "\n left outer" +
                    "\n join shm_file on shm01 = sgm01" +
                    "\n where (sgm301 + sgm302 + sgm303 + sgm304 - sgm311 - sgm312 - sgm313 - sgm314 - sgm316 - sgm317) > 0 and shm28 = 'N') i" +
                    "\n left outer join" +
                    "\n (select modelver, min(ecb01) as ecb01 from" +
                    "\n (select f_1444_modelver(ecb01) as ModelVer, ecb01, ecb06" +
                    "\n from ecb_file" +
                    "\n left outer join ecu_file on ecu01 = ecb01 and ecu02 = ecb02" +
                    "\n where ecb02 = '10' and ecu10 = 'Y' and ecb06 like 'Z%' order by ecb01)" +
                    "\n group by modelver) e" +
                    "\n on e.modelver = i.modelver" +
                    "\n where(sgm03_par < nvl(ecb01, ' ') or ecb01 is null)" +
                    "\n group by i.modelver" +
                    "\n ) f" +
                    "\n on f.modelver = f_1444_modelver(a.ogb04)" +
                    "\n order by a.ogb12 - b.kuncun desc) z where rnm <=20";
            mydata.Clear();
            mydata = myconn.mysearch(strsql);
            GridView1.DataSource = mydata;

            GridView1.DataBind();

            for (int i=0;i< GridView1.PageSize; i++)
            {
                if (Convert.ToInt64(((Label)GridView1.Rows[i].FindControl("lblcol04")).Text.Replace(",", "")) > Convert.ToInt64(((Label)GridView1.Rows[i].FindControl("lblcol05")).Text.Replace(",", "")))
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.Green;
                    GridView1.Rows[i].ForeColor = System.Drawing.Color.White;
                }
            }


           

            try
            {
                myconn.myclose();
            }
            catch
            {
                return;
            }
          

            
        }
        void query2(int index)
        {
            string strsql = "";




            DataTable mydata2 = new DataTable();

            connectTT myconn = new connectTT();
            myconn.myopen();

            //---------------------------------------------------------------

            body_label.Text=  "料號 (Mã liệu) ："+mydata.Rows[index]["料號"].ToString();

            GridView1.Rows[index % GridView1.PageSize].BackColor = System.Drawing.Color.Yellow;
            GridView1.Rows[index % GridView1.PageSize].ForeColor = System.Drawing.Color.Red;

            if (Convert.ToInt32(h_index_old.Value) % GridView1.PageSize < Convert.ToInt32(h_index.Value) % GridView1.PageSize)
            {
                GridView1.Rows[Convert.ToInt32(h_index_old.Value) % GridView1.PageSize].BackColor = System.Drawing.Color.Green;
                GridView1.Rows[Convert.ToInt32(h_index_old.Value) % GridView1.PageSize].ForeColor = System.Drawing.Color.White;
            }
            //-----------------------------------------------

            strsql = "select ogb.ogb04,oga.oga021,ogb.ogb31,to_char(ogb.ogb12,'999,999,999') ogb12,oga.oga03,oga.oga032 from ogb_file ogb" +
                    "\n inner join oga_file oga on oga01 = ogb01 and ogaconf = 'Y' and ogapost = 'N' and oga09 in ('1') and oga55 = '1'" +
                    "\n inner join oea_file oea on oea.oea01 = ogb.ogb31 and oeaconf = 'Y' and oea49 = '1'" +
                    "\n where ogb.ogb04 = '" + mydata.Rows[index]["料號"].ToString() + "' order by oga.oga021";

            mydata2 = myconn.mysearch(strsql);
            GridView2.DataSource = mydata2;

            GridView2.DataBind();


            try
            {
                myconn.myclose();
            }
            catch
            {
                return;
            }


        }
         protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {

       

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {        
            search_green();
            query2(Convert.ToInt32(h_index.Value));

        }
        void search_green()
        {
            if (h_index.Value == "0" && h_index_old.Value!="0" &&
                Convert.ToInt64(((Label)GridView1.Rows[Convert.ToInt32(h_index.Value) % GridView1.PageSize].FindControl("lblcol04")).Text.Replace(",", "")) > Convert.ToInt64(((Label)GridView1.Rows[Convert.ToInt32(h_index.Value) % GridView1.PageSize].FindControl("lblcol05")).Text.Replace(",", "")))
            {
                h_index_old.Value = "0";
            }
            else
            {
                if(h_index.Value != "0") h_index_old.Value = h_index.Value;
                h_index.Value = (Convert.ToInt32(h_index.Value) + 1).ToString(); // bat dau tu chinh no

                if (Convert.ToInt32(h_index.Value) % GridView1.PageSize == 0) //neu +1 la qua trang thi qua trang
                {
                    if (GridView1.PageIndex == GridView1.PageCount - 1)
                        //GridView1.PageIndex = 0;
                        Response.Redirect("/kanban/kanban_zhizao3.aspx?page=kanban_zhizao2");
                    else
                        GridView1.PageIndex = GridView1.PageIndex + 1;

                    GridView1.DataSource = mydata;
                    GridView1.DataBind();

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        if (Convert.ToInt64(((Label)GridView1.Rows[i].FindControl("lblcol04")).Text.Replace(",", "")) > Convert.ToInt64(((Label)GridView1.Rows[i].FindControl("lblcol05")).Text.Replace(",", "")))
                        {
                            GridView1.Rows[i].BackColor = System.Drawing.Color.Green;
                            GridView1.Rows[i].ForeColor = System.Drawing.Color.White;
                        }
                    }
                    int a = 0;
                }

                //if (Convert.ToInt32(h_index.Value) == mydata.Rows.Count)
                //{
                //    GridView1.PageIndex = 0;
                //    query();
                //}

                //index +1 cho toi khi green
                while (Convert.ToInt64(((Label)GridView1.Rows[Convert.ToInt32(h_index.Value) % GridView1.PageSize].FindControl("lblcol04")).Text.Replace(",", "")) <= Convert.ToInt64(((Label)GridView1.Rows[Convert.ToInt32(h_index.Value) % GridView1.PageSize].FindControl("lblcol05")).Text.Replace(",", "")))
                {
                    h_index.Value = (Convert.ToInt32(h_index.Value) + 1).ToString();
                    if (Convert.ToInt32(h_index.Value) % GridView1.PageSize == 0) //neu +1 la qua trang thi qua trang
                    {
                        if (GridView1.PageIndex == GridView1.PageCount - 1)
                            //GridView1.PageIndex = 0;
                            Response.Redirect("/kanban/kanban_zhizao3.aspx?page=kanban_zhizao2");
                        else GridView1.PageIndex = GridView1.PageIndex + 1;

                        GridView1.DataSource = mydata;
                        GridView1.DataBind();

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {

                            if (Convert.ToInt64(((Label)GridView1.Rows[i].FindControl("lblcol04")).Text.Replace(",", "")) > Convert.ToInt64(((Label)GridView1.Rows[i].FindControl("lblcol05")).Text.Replace(",", "")))
                            {
                                GridView1.Rows[i].BackColor = System.Drawing.Color.Green;
                                GridView1.Rows[i].ForeColor = System.Drawing.Color.White;
                            }
                        }
                    }
                    //if (Convert.ToInt32(h_index.Value) == mydata.Rows.Count)
                    //{
                    //    GridView1.PageIndex = 0;
                    //    query();

                    //}
                }
            }
        }
      
      
    }
   
}