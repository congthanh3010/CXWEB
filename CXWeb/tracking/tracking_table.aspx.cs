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
    public partial class tracking_table : System.Web.UI.Page
    {
        protected static string[] tracking_col_name = new string[50];
        protected static int tracking_col_num_each_stage = 6;
        connectTT myconn = new connectTT(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            string product = base.Request.QueryString["pd"];
            string lot_number = base.Request.QueryString["lot"];


            if (!IsPostBack && product != null && lot_number != null)
            {
                hproduct.Value = product;
                hlot_number.Value = lot_number;
                query();
            }

        }
        void query()
        {
            string strsql = "";
         
         
            //-------------------------------------------------------------

            DataTable mydata = new DataTable();
            myconn.myopen();


            //strsql = "select * from checklist_unit_setting where sub_unit ='" + sub_unit + "' order by number";
            strsql = "select distinct  " +
                     " sfv1.sfv04 料號, sfv1.sfv07 批號, sfv1.sfv09 數量, sfv1.sfv08 單位," +
                     " NVL(F_use_for_zhuisu(sfv1.sfv11, sfv1.sfv07, sfv1.sfv04, 'asft730'),'無資料') 生產機台和操作員,NVL(F_use_for_zhuisu(sfv1.sfv11, sfv1.sfv07, sfv1.sfv04, 'cqct511'),'無資料') 檢驗單號," +
                     " sfe1.sfe07 上一階料號1, sfe1.sfe10 上一階料號批號1, sfe1.sfe16 上一階料號數量1, sfe1.sfe17 上一階料號單位1," +
                     " NVL(F_use_for_zhuisu(sfv2.sfv11, sfv2.sfv07, sfv2.sfv04, 'asft730'),'無資料') 上一階機台和操作員1,NVL(F_use_for_zhuisu(sfv2.sfv11, sfv2.sfv07, sfv2.sfv04, 'cqct511'),'無資料') 上一階檢驗單號1," +
                     " sfe2.sfe07 上一階料號2, sfe2.sfe10 上一階料號批號2, sfe2.sfe16 上一階料號數量2, sfe2.sfe17 上一階料號單位2," +
                     " NVL(F_use_for_zhuisu(sfv3.sfv11, sfv3.sfv07, sfv3.sfv04, 'asft730'),'無資料') 上一階機台和操作員2,NVL(F_use_for_zhuisu(sfv3.sfv11, sfv3.sfv07, sfv3.sfv04, 'cqct511'),'無資料') 上一階檢驗單號2," +
                     " sfe3.sfe07 上一階料號3, sfe3.sfe10 上一階料號批號3, sfe3.sfe16 上一階料號數量3, sfe3.sfe17 上一階料號單位3," +
                     " NVL(F_use_for_zhuisu(sfv4.sfv11, sfv4.sfv07, sfv4.sfv04, 'asft730'),'無資料') 上一階機台和操作員3,NVL(F_use_for_zhuisu(sfv4.sfv11, sfv4.sfv07, sfv4.sfv04, 'cqct511'),'無資料') 上一階檢驗單號3," +
                     " sfe4.sfe07 上一階料號4, sfe4.sfe10 上一階料號批號4, sfe4.sfe16 上一階料號數量4, sfe4.sfe17 上一階料號單位4," +
                     " NVL(F_use_for_zhuisu(sfv5.sfv11, sfv5.sfv07, sfv5.sfv04, 'asft730'),'無資料') 上一階機台和操作員4,NVL(F_use_for_zhuisu(sfv5.sfv11, sfv5.sfv07, sfv5.sfv04, 'cqct511'),'無資料') 上一階檢驗單號4," +
                     " sfe5.sfe07 上一階料號5, sfe5.sfe10 上一階料號批號5, sfe5.sfe16 上一階料號數量5, sfe5.sfe17 上一階料號單位5," +
                     " imn15 撥入倉庫,imn16 撥入諸位, imn17 撥入批號,imn04 撥出倉庫, imn05 撥出諸位,imn06 撥出批號," +
                     " NVL(rvv1.rvu05,'參考料號編碼原則') 原料供應商" +
                     " from(select sfv11, sfv04, sfv05, sfv06, sfv07, sum(sfv09) as sfv09, sfv08 from sfv_file group by sfv11, sfv04, sfv05, sfv06, sfv07, sfv08) sfv1" +
                     " left outer join (select sfe01, sfe07, sfe08, sfe09, sfe10, sum(sfe16) as sfe16, sfe17 from sfe_file group by sfe01, sfe07, sfe08, sfe09, sfe10, sfe17) sfe1" +
                     " on sfe1.sfe01 = sfv1.sfv11 and sfe1.sfe07 not like 'WP%' and sfe1.sfe07 not like 'SS%'" +
                     " left outer join(select sfv11, sfv04, sfv05, sfv06, sfv07, sum(sfv09) as sfv09, sfv08 from sfv_file group by sfv11, sfv04, sfv05, sfv06, sfv07, sfv08) sfv2" +
                     " on sfv2.sfv04 = sfe1.sfe07 and sfv2.sfv05 = sfe1.sfe08 and sfv2.sfv06 = sfe1.sfe09 and sfv2.sfv07 = sfe1.sfe10" +
                     " left outer join(select sfe01, sfe07, sfe08, sfe09, sfe10, sum(sfe16) as sfe16, sfe17 from sfe_file group by sfe01, sfe07, sfe08, sfe09, sfe10, sfe17) sfe2" +
                     " on sfe2.sfe01 = sfv2.sfv11 and sfe2.sfe07 not like 'WP%' and sfe2.sfe07 not like 'SS%'" +
                     " left outer join(select sfv11, sfv04, sfv05, sfv06, sfv07, sum(sfv09) as sfv09, sfv08 from sfv_file group by sfv11, sfv04, sfv05, sfv06, sfv07, sfv08) sfv3" +
                     " on sfv3.sfv04 = sfe2.sfe07 and sfv3.sfv05 = sfe2.sfe08 and sfv3.sfv06 = sfe2.sfe09 and sfv3.sfv07 = sfe2.sfe10" +
                     " left outer join(select sfe01, sfe07, sfe08, sfe09, sfe10, sum(sfe16) as sfe16, sfe17 from sfe_file group by sfe01, sfe07, sfe08, sfe09, sfe10, sfe17) sfe3" +
                     " on sfe3.sfe01 = sfv3.sfv11 and sfe3.sfe07 not like 'WP%' and sfe3.sfe07 not like 'SS%'" +
                     " left outer join(select sfv11, sfv04, sfv05, sfv06, sfv07, sum(sfv09) as sfv09, sfv08 from sfv_file group by sfv11, sfv04, sfv05, sfv06, sfv07, sfv08) sfv4" +
                     " on sfv4.sfv04 = sfe3.sfe07 and sfv4.sfv05 = sfe3.sfe08 and sfv4.sfv06 = sfe3.sfe09 and sfv4.sfv07 = sfe3.sfe10" +
                     " left outer join(select sfe01, sfe07, sfe08, sfe09, sfe10, sum(sfe16) as sfe16, sfe17 from sfe_file group by sfe01, sfe07, sfe08, sfe09, sfe10, sfe17) sfe4" +
                     " on sfe4.sfe01 = sfv4.sfv11 and sfe4.sfe07 not like 'WP%' and sfe4.sfe07 not like 'SS%'" +
                     " left outer join (select sfv11, sfv04, sfv05, sfv06, sfv07, sum(sfv09) as sfv09, sfv08 from sfv_file group by sfv11, sfv04, sfv05, sfv06, sfv07, sfv08 ) sfv5" +
                     " on sfv5.sfv04 = sfe4.sfe07 and sfv5.sfv05 = sfe4.sfe08 and sfv5.sfv06 = sfe4.sfe09 and sfv5.sfv07 = sfe4.sfe10" +
                     " left outer join(select sfe01, sfe07, sfe08, sfe09, sfe10, sum(sfe16) as sfe16, sfe17 from sfe_file group by sfe01, sfe07, sfe08, sfe09, sfe10, sfe17) sfe5" +
                     " on sfe5.sfe01 = sfv5.sfv11 and sfe5.sfe07 not like 'WP%' and sfe5.sfe07 not like 'SS%'" +
                     " left outer join(select distinct imn03, imn15, imn16, imn17, imn04, imn05, imn06  from imn_file,imm_file where imm01=imn01 and immconf='Y')" +
                     " on imn03=NVL(NVL(NVL(NVL(sfe5.sfe07,sfe4.sfe07),sfe3.sfe07),sfe2.sfe07),sfe1.sfe07)" +
                     " and imn15 = NVL(NVL(NVL(NVL(sfe5.sfe08, sfe4.sfe08), sfe3.sfe08), sfe2.sfe08), sfe1.sfe08)" +
                     " and imn16 = NVL(NVL(NVL(NVL(sfe5.sfe09, sfe4.sfe09), sfe3.sfe09), sfe2.sfe09), sfe1.sfe09)" +
                     " and imn17 = NVL(NVL(NVL(NVL(sfe5.sfe10, sfe4.sfe10), sfe3.sfe10), sfe2.sfe10), sfe1.sfe10)" +
                     " left outer join (select distinct rvu05,rvv31,rvv32,rvv33,rvv34 from rvv_file,rvu_file where rvu01=rvv01 and rvuconf='Y') rvv1" +
                     " on rvv1.rvv31 = NVL(NVL(NVL(NVL(NVL(imn03, sfe5.sfe07), sfe4.sfe07), sfe3.sfe07), sfe2.sfe07), sfe1.sfe07)" +
                     " and rvv1.rvv32 = NVL(NVL(NVL(NVL(NVL(imn04, sfe5.sfe08), sfe4.sfe08), sfe3.sfe08), sfe2.sfe08), sfe1.sfe08)" +
                     " and rvv1.rvv33 = NVL(NVL(NVL(NVL(NVL(imn05, sfe5.sfe09), sfe4.sfe09), sfe3.sfe09), sfe2.sfe09), sfe1.sfe09)" +
                     " and rvv1.rvv34 = NVL(NVL(NVL(NVL(NVL(imn06, sfe5.sfe10), sfe4.sfe10), sfe3.sfe10), sfe2.sfe10), sfe1.sfe10)" +
                     " where sfv1.sfv04 = '"+hproduct.Value+"' and sfv1.sfv07 = '"+hlot_number.Value+"'" +
                     " order by  sfe1.sfe10,sfe2.sfe10,sfe3.sfe10,sfe4.sfe10,sfe5.sfe10";
             mydata = myconn.mysearch(strsql);

            //mydata.Columns.Add("link");
            //mydata.Columns.Add("link2");
            //for (int i = 0; i < mydata.Rows.Count; i++)
            //{
            //    mydata.Rows[i]["link"] = "checklist_machine.aspx?su=" + sub_unit + "&mc=" + mydata.Rows[i]["machine_no"];
            //    mydata.Rows[i]["link2"] = "checklist_machine.aspx?su=" + sub_unit + "&mg=" + mydata.Rows[i]["machine_group"];
            //}
            int table_width = 0;
            string switch_flag = "1";
            for(int i=0;i<mydata.Columns.Count;i++)
            {
                tracking_col_name[i] = mydata.Columns[i].ColumnName;
                GridView1.Columns[i].HeaderText = mydata.Columns[i].ColumnName;

                if(i% tracking_col_num_each_stage==0 && i<(5* tracking_col_num_each_stage))//在5階段，每階段有tracking_col_num_each_stage欄位處理
                {
                    switch_flag = switch_flag == "0" ? "1" : "0";

                    if (mydata.Rows.Count > 0)
                    {
                        if (mydata.Rows[0][i].ToString().Trim() != "")
                        {
                            
                            table_width += 1200;
                            if(mydata.Rows[0][i].ToString().Substring(0,1)=="A")//如果是原來，不用顯示最後兩欄位，（報工和檢驗欄位）
                            {
                                table_width -= 400;
                                GridView1.Columns[i + tracking_col_num_each_stage - 2].Visible = false;
                                GridView1.Columns[i + tracking_col_num_each_stage - 1].Visible = false;
                            }
                        }
                        else
                        {
                            for(int j=0;j< tracking_col_num_each_stage;j++)
                            {
                                GridView1.Columns[i+j].Visible = false;
                            }
                          
                        }
                    }
                }
                if (i >= (5 * tracking_col_num_each_stage))
                {
                    if (mydata.Rows.Count > 0)
                    {
                        if (mydata.Rows[0][i].ToString().Trim() != "")
                        {
                            table_width += 200;
                        }
                        else
                        {
                            GridView1.Columns[i].Visible = false;
                        }
                    }
                }
                //if(i== 5 * tracking_col_num_each_stage|| i == 5 * tracking_col_num_each_stage+4)
                //    switch_flag = switch_flag == "0" ? "1" : "0";

                GridView1.Columns[i].ItemStyle.BackColor= switch_flag == "0" ? System.Drawing.ColorTranslator.FromHtml("#FBFFC9") : System.Drawing.ColorTranslator.FromHtml("#D9F1FF");
                if(i >= 5 * tracking_col_num_each_stage + 4)//調撥和廠商區域顏色
                {
                    GridView1.Columns[i].ItemStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#B7FFBD");
                }
                else if (i >= 5 * tracking_col_num_each_stage)//第5階顏色
                {
                    GridView1.Columns[i].ItemStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC99");
                }
                






            }
            // 處理欄位之後，再加link欄位
            mydata.Columns.Add("link1");
            mydata.Columns.Add("link2");
            mydata.Columns.Add("link3");
            mydata.Columns.Add("link4");
            mydata.Columns.Add("link5");

            mydata.Columns.Add("linkQA1");
            mydata.Columns.Add("linkQA2");
            mydata.Columns.Add("linkQA3");
            mydata.Columns.Add("linkQA4");
            mydata.Columns.Add("linkQA5");
            for (int i = 0; i < mydata.Rows.Count; i++)
            {
                if(mydata.Rows[i][4].ToString()!="無資料")
                    mydata.Rows[i]["link1"] = "tracking_rw_detail.aspx?yzd=" + mydata.Rows[i][4];
                else
                {
                    mydata.Rows[i]["link1"] = "";                   
                }
                if (mydata.Rows[i][4 + tracking_col_num_each_stage * 1].ToString() != "無資料")
                    mydata.Rows[i]["link2"] = "tracking_rw_detail.aspx?yzd=" + mydata.Rows[i][4 + tracking_col_num_each_stage * 1];
                else
                {
                    mydata.Rows[i]["link2"] = "";
                    
                }
                if (mydata.Rows[i][4 + tracking_col_num_each_stage * 2].ToString() != "無資料")
                    mydata.Rows[i]["link3"] = "tracking_rw_detail.aspx?yzd=" + mydata.Rows[i][4 + tracking_col_num_each_stage * 2];
                else
                {
                    mydata.Rows[i]["link3"] = "";
                    
                }
                if (mydata.Rows[i][4 + tracking_col_num_each_stage * 3].ToString() != "無資料")
                    mydata.Rows[i]["link4"] = "tracking_rw_detail.aspx?yzd=" + mydata.Rows[i][4 + tracking_col_num_each_stage * 3];
                else
                {
                    mydata.Rows[i]["link4"] = "";
                   
                }
                if (mydata.Rows[i][4 + tracking_col_num_each_stage * 4].ToString() != "無資料")
                    mydata.Rows[i]["link5"] = "tracking_rw_detail.aspx?yzd=" + mydata.Rows[i][4 + tracking_col_num_each_stage * 4];
                else
                {
                    mydata.Rows[i]["link5"] = "";
                  
                }

                if (mydata.Rows[i][5].ToString() != "無資料")
                    mydata.Rows[i]["linkQA1"] = "tracking_qa_detail.aspx?dh=" + mydata.Rows[i][4];
                else
                {
                    mydata.Rows[i]["linkQA1"] = "";
                }
                if (mydata.Rows[i][5 + tracking_col_num_each_stage * 1].ToString() != "無資料")
                    mydata.Rows[i]["linkQA2"] = "tracking_qa_detail.aspx?dh=" + mydata.Rows[i][5 + tracking_col_num_each_stage * 1];
                else
                {
                    mydata.Rows[i]["linkQA2"] = "";

                }
                if (mydata.Rows[i][5 + tracking_col_num_each_stage * 2].ToString() != "無資料")
                    mydata.Rows[i]["linkQA3"] = "tracking_qa_detail.aspx?dh=" + mydata.Rows[i][5 + tracking_col_num_each_stage * 2];
                else
                {
                    mydata.Rows[i]["linkQA3"] = "";

                }
                if (mydata.Rows[i][5 + tracking_col_num_each_stage * 3].ToString() != "無資料")
                    mydata.Rows[i]["linkQA4"] = "tracking_qa_detail.aspx?dh=" + mydata.Rows[i][5 + tracking_col_num_each_stage * 3];
                else
                {
                    mydata.Rows[i]["linkQA4"] = "";

                }
                if (mydata.Rows[i][5 + tracking_col_num_each_stage * 4].ToString() != "無資料")
                    mydata.Rows[i]["linkQA5"] = "tracking_qa_detail.aspx?dh=" + mydata.Rows[i][5 + tracking_col_num_each_stage * 4];
                else
                {
                    mydata.Rows[i]["linkQA5"] = "";

                }

            }
            GridView1.Width = table_width;
             GridView1.DataSource = mydata;

            GridView1.DataBind();
            for (int i = 0; i < mydata.Rows.Count; i++)
            {
                if (((HyperLink)GridView1.Rows[i].FindControl("HyperLink1")).NavigateUrl=="")
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLink1")).Text = "無資料";
                if (((HyperLink)GridView1.Rows[i].FindControl("HyperLink2")).NavigateUrl == "")
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLink2")).Text = "無資料";
                if (((HyperLink)GridView1.Rows[i].FindControl("HyperLink3")).NavigateUrl == "")
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLink3")).Text = "無資料";
                if (((HyperLink)GridView1.Rows[i].FindControl("HyperLink4")).NavigateUrl == "")
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLink4")).Text = "無資料";
                if (((HyperLink)GridView1.Rows[i].FindControl("HyperLink5")).NavigateUrl == "")
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLink5")).Text = "無資料";

                if (((HyperLink)GridView1.Rows[i].FindControl("HyperLinkQA1")).NavigateUrl == "")
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLinkQA1")).Text = "無資料";
                if (((HyperLink)GridView1.Rows[i].FindControl("HyperLinkQA2")).NavigateUrl == "")
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLinkQA2")).Text = "無資料";
                if (((HyperLink)GridView1.Rows[i].FindControl("HyperLinkQA3")).NavigateUrl == "")
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLinkQA3")).Text = "無資料";
                if (((HyperLink)GridView1.Rows[i].FindControl("HyperLinkQA4")).NavigateUrl == "")
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLinkQA4")).Text = "無資料";
                if (((HyperLink)GridView1.Rows[i].FindControl("HyperLinkQA5")).NavigateUrl == "")
                    ((HyperLink)GridView1.Rows[i].FindControl("HyperLinkQA5")).Text = "無資料";
            }
               
            //int colspan = 1;
           
            //for (int i = 0; i < mydata.Rows.Count; i++)
            //{
            //    // xu ly mau
            //    if (mydata.Rows[i]["sum_point"].ToString().Trim() != "")
            //    {
            //        GridView1.Rows[i].BackColor = System.Drawing.Color.Yellow;
            //        GridView1.Rows[i].ForeColor = System.Drawing.Color.Red;
            //        ((HyperLink)GridView1.Rows[i].FindControl("HyperLink1")).ForeColor = System.Drawing.Color.Red;
            //        ((HyperLink)GridView1.Rows[i].FindControl("HyperLink2")).ForeColor = System.Drawing.Color.Red;
            //    }


              

            //}
            //for(int i= mydata.Rows.Count-1;i>=0;i--)
            //{
            //    // xu ly phan group rowspan
            //    if (i != 0)
            //    {
            //        if (mydata.Rows[i]["machine_group"].ToString() == mydata.Rows[i - 1]["machine_group"].ToString())
            //        {
            //            colspan++;
            //            GridView1.Rows[i].Cells[0].Visible = false;
            //        }
            //        else
            //        {
            //            GridView1.Rows[i].Cells[0].Attributes.Add("rowspan", colspan.ToString());
            //            colspan = 1;
            //        }
            //    }
            //    else
            //    {
            //        GridView1.Rows[i].Cells[0].Attributes.Add("rowspan", colspan.ToString());
            //        colspan = 1;
            //    }
            //}
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
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/tracking/tracking_product_selection.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        string lblsum_point = ((Label)GridView1.Rows[e.Row.RowIndex].FindControl("lblsum_point")).Text;
            //        if (lblsum_point.Trim() != "") //Select the row 
            //        {
            //            e.Row.BackColor = System.Drawing.Color.Green;
            //            e.Row.ForeColor = System.Drawing.Color.Red;

            //            //or you can select the color 
            //            //e.Row.BackColor = System.Drawing.Color.FromArgb(255, 0, 0); 
            //        }
            //    }
            //}
            //catch (Exception ms)
            //{

            //}
        }
    }
}