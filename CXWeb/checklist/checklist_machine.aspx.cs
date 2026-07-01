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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace CXWeb.checklist
{
    public partial class checklist_machine : System.Web.UI.Page
    {
        connectEIP myconn = new connectEIP();
        protected string maxpoint = "([0-7])*";

        protected static string prob_table_code="";
        protected void Page_Load(object sender, EventArgs e)
        {
            string mc_code = base.Request.QueryString["mc"];
            string sub_unit_code = base.Request.QueryString["su"];
            string mc_group = base.Request.QueryString["mg"];
            //Page.MaintainScrollPositionOnPostBack = true;
            prob_table_code = "";
            if (mc_code == null)
            {
              
                GridView1.Columns[1].HeaderText = "群組 Nhóm máy";
                prob_table_code = "hidden = 'hidden'";
            }

            if (IsPostBack == false && (mc_code!=null || mc_group!=null))
            {
                

                hmamay.Value = mc_code;
                hsub_unit.Value = sub_unit_code;
                hmgroup.Value = mc_group;

                query();
                query_machine_problem();
            }
            
           
        }       
        
        private void query()
        {
            try
            {
              
                string date;
                string shift;
                string strsql;
                DataTable mydata = new DataTable();
                DataTable setting_data = new DataTable();
                DataTable final_data = new DataTable();
                if (DateTime.Now.Hour >= 18)
                {
                    shift = "CA2";
                    date = DateTime.Now.ToString("yyyy/MM/dd");
                }
                else if (DateTime.Now.Hour < 6)
                {
                    shift = "CA2";
                    date = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
                }
                else
                {
                    shift = "CA1";
                    date = DateTime.Now.ToString("yyyy/MM/dd");
                }
                date = base.Request.QueryString["da"];
                //setting grid

                strsql = "select m.col_name,m.max_point,n.description from checklist_unit_item_setting m,cod_code n" +
                    " where m.table_code = 'checklist' and m.unit='" + hsub_unit.Value + "' and n.class='checklist_table' and m.col_name=n.code" +
                    " order by m.col_no";
                myconn.myopen();
                setting_data = myconn.mysearch(strsql);

                string select_str = "ten_may,thao_tac_vien";

                for (int i = 0; i < setting_data.Rows.Count; i++)
                {
                    GridView1.Columns[i + 3].HeaderText = setting_data.Rows[i]["description"].ToString();
                    GridView1.Columns[i + 3].Visible = true;
                    select_str = select_str + "," + setting_data.Rows[i]["col_name"].ToString();
                }
                select_str = select_str + ",sum_point";

                // tim du lieu trong checklist


                // tim kiem theo may, neu tìm không thấy thì insert
                if (hmamay.Value != "")
                    strsql = "select " + select_str + " from checklist_table where don_vi='" + hsub_unit.Value + "' and thoi_gian ='" + date  + "'" +
                    " and ten_may =N'" + hmamay.Value + "'";
                else strsql = "select " + select_str + " from checklist_table where don_vi='" + hsub_unit.Value + "' and thoi_gian ='" + date + "'" +
                     " and ten_may =N'" + hmgroup.Value + "'";
                mydata = myconn.mysearch(strsql);



                if (mydata.Rows.Count == 0)
                {

                    string strsql2;

                    // ko xóa hết chấm lại, chỉ insert những máy ko có 20190503
                    //if (hmgroup.Value != "")// neu cham diem theo group thi xoa het cham lại
                    //{
                    //    myconn.mySqlExecute("delete from checklist_table where don_vi='" + hsub_unit.Value + "' and thoi_gian='" + date  + "' and nhom_may=N'" + hmgroup.Value + "'");
                    //}


                    if (hmamay.Value!="")
                        strsql2 = "select * from checklist_unit_setting where sub_unit ='" + hsub_unit.Value + "' and machine_no=N'" + hmamay.Value + "' ";
                    else
                    {
                        //strsql2 = "select * from checklist_unit_setting where sub_unit ='" + hsub_unit.Value + "' and machine_group=N'" + hmgroup.Value + "' ";
                        //khi them may 1 loạt theo nhóm, chỉ thêm những máy chưa điểm kiểm 20190503
                        strsql2 = "select * from checklist_unit_setting where sub_unit ='" + hsub_unit.Value + "' and machine_group=N'" + hmgroup.Value + "'"+
                                  " and machine_no not in (select ten_may from checklist_table where don_vi='" + hsub_unit.Value + "' and thoi_gian = '" + date+ "'and nhom_may=N'" + hmgroup.Value + "' ) ";
                    }
                        

                    string inssql = "";
                    DataTable unit_setting_data = new DataTable();
                    unit_setting_data = myconn.mysearch(strsql2);

                    if (unit_setting_data.Rows.Count != 0)// insert may de cham diem
                    {
                        for (int i = 0; i < unit_setting_data.Rows.Count; i++)
                        {
                            if (shift == "CA1")
                                inssql = "insert into checklist_table (don_vi,thoi_gian,ten_may,thao_tac_vien,nhom_may) values" +
                                " ('" + hsub_unit.Value + "','" + date + "',N'" + unit_setting_data.Rows[i]["machine_no"].ToString() + "','" + unit_setting_data.Rows[i]["ca1_msnv"].ToString() + "',N'" + unit_setting_data.Rows[i]["machine_group"].ToString() + "');";
                            else
                                inssql = "insert into checklist_table (don_vi,thoi_gian,ten_may,thao_tac_vien,nhom_may) values" +
                                " ('" + hsub_unit.Value + "','" + date + "',N'" + unit_setting_data.Rows[i]["machine_no"].ToString() + "','" + unit_setting_data.Rows[i]["ca2_msnv"].ToString() + "',N'" + unit_setting_data.Rows[i]["machine_group"].ToString() + "');";
                            myconn.mySqlExecute(inssql);
                        }
                    }
                   
                    if(hmgroup.Value!="")
                    {
                       // them nhom may de diem kiểm
                        inssql = "insert into checklist_table (don_vi,thoi_gian,ten_may,nhom_may) values" +
                              " ('" + hsub_unit.Value + "','" + date + "',N'" + hmgroup.Value + "',N'" + hmgroup.Value + "');";
                        myconn.mySqlExecute(inssql);


                       
                      
                    }

                    mydata = myconn.mysearch(strsql);
                }


             
                myconn.myclose();
              
                for (int i = 1; i <= 20; i++)
                {
                    final_data.Columns.Add(i.ToString());
                }
                final_data.Columns.Add("colSum");
                for (int i = 0; i < mydata.Rows.Count; i++)
                {
                    final_data.Rows.Add();
                    for (int j = 0; j < mydata.Columns.Count-1; j++)
                    {

                        final_data.Rows[i][j] = mydata.Rows[i][j].ToString();
                    }
                    final_data.Rows[i]["colSum"] = mydata.Rows[i][mydata.Columns.Count - 1].ToString();


                }

                GridView1.DataSource = final_data;
                GridView1.DataBind();
              

                //--------------------------------------them hàng head max point-----------------------------------------------------------
                int left_col_num = 3;
                int quality_col_num;
                int s_col_num;
                int security_col_num;


                GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

                //spanned cell that will span the columns I don't want to give the additional header 
                TableCell left = new TableHeaderCell();
                left.ColumnSpan = left_col_num;
                row.Cells.Add(left);

                //spanned cell that will span the columns i want to give the additional header
                TableCell[] maxPoint = new TableCell[20];
                //maxPoint.ColumnSpan = GridView1.Columns.Count - 3;               


                for (int i = 0; i < setting_data.Rows.Count; i++)
                {
                    maxPoint[i] = new TableHeaderCell();
                    maxPoint[i].Text = setting_data.Rows[i]["max_point"].ToString();
                    row.Cells.Add(maxPoint[i]);

                }

                TableCell final_cell = new TableHeaderCell();
                final_cell.ColumnSpan = GridView1.Columns.Count - setting_data.Rows.Count - left_col_num;
                row.Cells.Add(final_cell);

                
                //Add the new row to the gridview as the master header row
                //A table is the only Control (index[0]) in a GridView
                ((Table)GridView1.Controls[0]).Rows.AddAt(0, row);

                //--------------------------------------them hàng head phan loai hang muc kiem nghiem-----------------------------------------------------------


                if (hsub_unit.Value == "GC" || hsub_unit.Value == "NC"
                   || hsub_unit.Value == "XL" || hsub_unit.Value == "XT" || hsub_unit.Value == "XD" || hsub_unit.Value == "DDCX")
                {
                    quality_col_num = 4;
                    s_col_num = 8;
                    security_col_num = 3;
                }
                else if (hsub_unit.Value == "CT1" || hsub_unit.Value == "CT2_DD"
                    || hsub_unit.Value == "CT2_LH")
                {
                    quality_col_num = 4;
                    s_col_num = 3;
                    security_col_num = 3;
                }
                else if (hsub_unit.Value == "CT2_URPC")
                {
                    quality_col_num = 3;
                    s_col_num = 3;
                    security_col_num = 2;
                }
                else if (hsub_unit.Value == "CNC")
                {
                    quality_col_num = 4;
                    s_col_num = 9;
                    security_col_num = 4;
                }
                else if (hsub_unit.Value == "CT3_ML")
                {
                    quality_col_num = 4;
                    s_col_num = 4;
                    security_col_num = 3;
                }
                else if (hsub_unit.Value == "CT3_DG")
                {
                    quality_col_num = 8;
                    s_col_num = 3;
                    security_col_num = 2;
                }
                else if (hsub_unit.Value == "CT3")
                {
                    quality_col_num = 4;
                    s_col_num = 4;
                    security_col_num = 4;
                }
                else
                {
                    quality_col_num = 1;
                    s_col_num = 1;
                    security_col_num = 1;
                }
                GridViewRow row2 = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
                TableCell left2 = new TableHeaderCell();
                left2.ColumnSpan = left_col_num;
                row2.Cells.Add(left2);

                TableCell quality_cell = new TableHeaderCell();
                quality_cell.ColumnSpan = quality_col_num;
                quality_cell.Text = "品質 Chất lượng";
                row2.Cells.Add(quality_cell);

                TableCell s_cell = new TableHeaderCell();
                s_cell.ColumnSpan = s_col_num;
                s_cell.Text = "5S";
                row2.Cells.Add(s_cell);

                TableCell security_cell = new TableHeaderCell();
                security_cell.ColumnSpan = security_col_num;
                security_cell.Text = "安全 An toàn";
                row2.Cells.Add(security_cell);

                TableCell final_cell2 = new TableHeaderCell();
                final_cell2.ColumnSpan = GridView1.Columns.Count - quality_col_num - s_col_num - security_col_num - left_col_num;
                row2.Cells.Add(final_cell2);

                ((Table)GridView1.Controls[0]).Rows.AddAt(0, row2);


               
            }
            catch (Exception ms)
            {
                showMessage(ms.Message);
                Response.Write(ms.Message);
            }
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

            // xu ly bao sai khi nhap diem vượt qua max point
            DataTable setting_data = new DataTable();
            string strsql = "select m.col_name,m.max_point,n.description from checklist_unit_item_setting m,cod_code n" +
                   " where m.table_code = 'checklist' and m.unit='" + hsub_unit.Value + "' and n.class='checklist_table' and m.col_name=n.code" +
                   " order by m.col_no";
            myconn.myopen();
            setting_data = myconn.mysearch(strsql);
            myconn.myclose();

            if (setting_data.Rows.Count >= 1)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol03")).MaximumValue = setting_data.Rows[0]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol03")).ErrorMessage = "Điểm <="+ setting_data.Rows[0]["max_point"].ToString();

            }

            if (setting_data.Rows.Count >= 2)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol04")).MaximumValue = setting_data.Rows[1]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol04")).ErrorMessage = "Điểm <=" + setting_data.Rows[1]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 3)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol05")).MaximumValue = setting_data.Rows[2]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol05")).ErrorMessage = "Điểm <=" + setting_data.Rows[2]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 4)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol06")).MaximumValue = setting_data.Rows[3]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol06")).ErrorMessage = "Điểm <=" + setting_data.Rows[3]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 5)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol07")).MaximumValue = setting_data.Rows[4]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol07")).ErrorMessage = "Điểm <=" + setting_data.Rows[4]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 6)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol08")).MaximumValue = setting_data.Rows[5]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol08")).ErrorMessage = "Điểm <=" + setting_data.Rows[5]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 7)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol09")).MaximumValue = setting_data.Rows[6]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol09")).ErrorMessage = "Điểm <=" + setting_data.Rows[6]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 8)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol10")).MaximumValue = setting_data.Rows[7]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol10")).ErrorMessage = "Điểm <=" + setting_data.Rows[7]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 9)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol11")).MaximumValue = setting_data.Rows[8]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol11")).ErrorMessage = "Điểm <=" + setting_data.Rows[8]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 10)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol12")).MaximumValue = setting_data.Rows[9]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol12")).ErrorMessage = "Điểm <=" + setting_data.Rows[9]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 11)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol13")).MaximumValue = setting_data.Rows[10]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol13")).ErrorMessage = "Điểm <=" + setting_data.Rows[10]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 12)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol14")).MaximumValue = setting_data.Rows[11]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol14")).ErrorMessage = "Điểm <=" + setting_data.Rows[11]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 13)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol15")).MaximumValue = setting_data.Rows[12]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol15")).ErrorMessage = "Điểm <=" + setting_data.Rows[12]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 14)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol16")).MaximumValue = setting_data.Rows[13]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol16")).ErrorMessage = "Điểm <=" + setting_data.Rows[13]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 15)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol17")).MaximumValue = setting_data.Rows[14]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol17")).ErrorMessage = "Điểm <=" + setting_data.Rows[14]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 16)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol18")).MaximumValue = setting_data.Rows[15]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol18")).ErrorMessage = "Điểm <=" + setting_data.Rows[15]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 17)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol19")).MaximumValue = setting_data.Rows[16]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol19")).ErrorMessage = "Điểm <=" + setting_data.Rows[16]["max_point"].ToString();
            }
            if (setting_data.Rows.Count >= 18)
            {
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol20")).MaximumValue = setting_data.Rows[17]["max_point"].ToString();
                ((RangeValidator)GridView1.Rows[0].FindControl("RVCol20")).ErrorMessage = "Điểm <=" + setting_data.Rows[17]["max_point"].ToString();
            }


        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            row_update();


        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
        }
        public void showMessage(string mess)
        {
            //string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
            //Response.Write(strBuilder);
            string myStringVariable = string.Empty;
            myStringVariable = mess;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
        }
        protected void btn1_Click(object sender, EventArgs e)
        {
            row_update();
            Response.Redirect("/checklist/checklist_machine_selection.aspx?su="+ hsub_unit.Value);
        }

        void row_update()
        {

            try
            {
                if (Session["LoginUserInfo"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                string date;
                string shift;

                if (DateTime.Now.Hour >= 18)
                {
                    shift = "CA2";
                    date = DateTime.Now.ToString("yyyy/MM/dd");
                }
                else if (DateTime.Now.Hour < 6)
                {
                    shift = "CA2";
                    date = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
                }
                else
                {
                    shift = "CA1";
                    date = DateTime.Now.ToString("yyyy/MM/dd");
                }
                date = base.Request.QueryString["da"];

                DataTable select_data = new DataTable();
                string strsql;
                myconn.myopen();

                strsql = "select col_name from checklist_unit_item_setting where table_code='checklist' and unit='" + hsub_unit.Value + "' order by col_no";
                select_data = myconn.mysearch(strsql);

                //string ten_may = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblcol01")).Text;
                string col03 = ((TextBox)GridView1.Rows[0].FindControl("txtcol03")).Text;
                string col04 = ((TextBox)GridView1.Rows[0].FindControl("txtcol04")).Text;
                string col05 = ((TextBox)GridView1.Rows[0].FindControl("txtcol05")).Text;
                string col06 = ((TextBox)GridView1.Rows[0].FindControl("txtcol06")).Text;
                string col07 = ((TextBox)GridView1.Rows[0].FindControl("txtcol07")).Text;
                string col08 = ((TextBox)GridView1.Rows[0].FindControl("txtcol08")).Text;
                string col09 = ((TextBox)GridView1.Rows[0].FindControl("txtcol09")).Text;
                string col10 = ((TextBox)GridView1.Rows[0].FindControl("txtcol10")).Text;
                string col11 = ((TextBox)GridView1.Rows[0].FindControl("txtcol11")).Text;
                string col12 = ((TextBox)GridView1.Rows[0].FindControl("txtcol12")).Text;
                string col13 = ((TextBox)GridView1.Rows[0].FindControl("txtcol13")).Text;
                string col14 = ((TextBox)GridView1.Rows[0].FindControl("txtcol14")).Text;
                string col15 = ((TextBox)GridView1.Rows[0].FindControl("txtcol15")).Text;
                string col16 = ((TextBox)GridView1.Rows[0].FindControl("txtcol16")).Text;
                string col17 = ((TextBox)GridView1.Rows[0].FindControl("txtcol17")).Text;
                string col18 = ((TextBox)GridView1.Rows[0].FindControl("txtcol18")).Text;
                string col19 = ((TextBox)GridView1.Rows[0].FindControl("txtcol19")).Text;
                string col20 = ((TextBox)GridView1.Rows[0].FindControl("txtcol20")).Text;



                if (col03.Trim() == "") col03 = "0";
                if (col04.Trim() == "") col04 = "0";
                if (col05.Trim() == "") col05 = "0";
                if (col06.Trim() == "") col06 = "0";
                if (col07.Trim() == "") col07 = "0";
                if (col08.Trim() == "") col08 = "0";
                if (col09.Trim() == "") col09 = "0";
                if (col10.Trim() == "") col10 = "0";
                if (col11.Trim() == "") col11 = "0";
                if (col12.Trim() == "") col12 = "0";
                if (col13.Trim() == "") col13 = "0";
                if (col14.Trim() == "") col14 = "0";
                if (col15.Trim() == "") col15 = "0";
                if (col16.Trim() == "") col16 = "0";
                if (col17.Trim() == "") col17 = "0";
                if (col18.Trim() == "") col18 = "0";
                if (col19.Trim() == "") col19 = "0";
                if (col20.Trim() == "") col20 = "0";
                Decimal sum_point = 0;
                try
                {
                    sum_point = Convert.ToDecimal(col03) + Convert.ToDecimal(col04) + Convert.ToDecimal(col05)
                        + Convert.ToDecimal(col06) + Convert.ToDecimal(col07) + Convert.ToDecimal(col08)
                        + Convert.ToDecimal(col09) + Convert.ToDecimal(col10) + Convert.ToDecimal(col11)
                        + Convert.ToDecimal(col12) + Convert.ToDecimal(col13) + Convert.ToDecimal(col14)
                        + Convert.ToDecimal(col15) + Convert.ToDecimal(col16) + Convert.ToDecimal(col17)
                        + Convert.ToDecimal(col18) + Convert.ToDecimal(col19) + Convert.ToDecimal(col20);
                }
                catch
                {
                    showMessage("評分錯誤！Nhập điểm sai !");
                    return;
                }


                strsql = "update checklist_table set ";
                if (select_data.Rows.Count >= 1) strsql = strsql + select_data.Rows[0]["col_name"].ToString() + " ='" + col03 + "'";
                if (select_data.Rows.Count >= 2) strsql = strsql + "," + select_data.Rows[1]["col_name"].ToString() + " ='" + col04 + "'";
                if (select_data.Rows.Count >= 3) strsql = strsql + "," + select_data.Rows[2]["col_name"].ToString() + " ='" + col05 + "'";
                if (select_data.Rows.Count >= 4) strsql = strsql + "," + select_data.Rows[3]["col_name"].ToString() + " ='" + col06 + "'";
                if (select_data.Rows.Count >= 5) strsql = strsql + "," + select_data.Rows[4]["col_name"].ToString() + " ='" + col07 + "'";
                if (select_data.Rows.Count >= 6) strsql = strsql + "," + select_data.Rows[5]["col_name"].ToString() + "='" + col08 + "'";
                if (select_data.Rows.Count >= 7) strsql = strsql + "," + select_data.Rows[6]["col_name"].ToString() + "='" + col09 + "'";
                if (select_data.Rows.Count >= 8) strsql = strsql + "," + select_data.Rows[7]["col_name"].ToString() + "='" + col10 + "'";
                if (select_data.Rows.Count >= 9) strsql = strsql + "," + select_data.Rows[8]["col_name"].ToString() + "='" + col11 + "'";
                if (select_data.Rows.Count >= 10) strsql = strsql + "," + select_data.Rows[9]["col_name"].ToString() + "='" + col12 + "'";
                if (select_data.Rows.Count >= 11) strsql = strsql + "," + select_data.Rows[10]["col_name"].ToString() + "='" + col13 + "'";
                if (select_data.Rows.Count >= 12) strsql = strsql + "," + select_data.Rows[11]["col_name"].ToString() + "='" + col14 + "'";
                if (select_data.Rows.Count >= 13) strsql = strsql + "," + select_data.Rows[12]["col_name"].ToString() + "='" + col15 + "'";
                if (select_data.Rows.Count >= 14) strsql = strsql + "," + select_data.Rows[13]["col_name"].ToString() + "='" + col16 + "'";
                if (select_data.Rows.Count >= 15) strsql = strsql + "," + select_data.Rows[14]["col_name"].ToString() + "='" + col17 + "'";
                if (select_data.Rows.Count >= 16) strsql = strsql + "," + select_data.Rows[15]["col_name"].ToString() + "='" + col18 + "'";
                if (select_data.Rows.Count >= 17) strsql = strsql + "," + select_data.Rows[16]["col_name"].ToString() + "='" + col19 + "'";
                if (select_data.Rows.Count >= 18) strsql = strsql + "," + select_data.Rows[17]["col_name"].ToString() + "='" + col20 + "'";


                myconn.myopen();

                if (hmamay.Value != "")// cham diem theo may
                {
                    strsql = strsql + ",sum_point='" + sum_point + "',nguoi_diem_kiem='" + Session["LoginUserInfo"].ToString() + "'"
                   + " where don_vi='" + hsub_unit.Value + "' and thoi_gian='" + date + "' and ten_may=N'" + hmamay.Value + "'   ";

                }
                else// cham diem theo group
                {
                    strsql = strsql + ",sum_point='" + sum_point + "',nguoi_diem_kiem='" + Session["LoginUserInfo"].ToString() + "'"
                 + " where don_vi='" + hsub_unit.Value + "' and thoi_gian='" + date + "' and nhom_may=N'" + hmgroup.Value + "'   ";
                }
                myconn.mySqlExecute(strsql);

                myconn.myclose();

                GridView1.EditIndex = -1;
                query();
            }
            catch (Exception ms)
            {
                showMessage(ms.Message);
                Response.Write(ms.Message);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Session["LoginUserInfo"] == null)
            {
                Response.Redirect("login.aspx");
            }

            if(txtProb_desc.Text.Trim()=="")
            {
                showMessage("機台異常不能為空！Vấn đề máy không được để trống!");
                return;
            }
            string fileName = "", filePath = "";
            if (Page.IsValid && FileUpload1.HasFile && CheckFileType(FileUpload1.FileName))
            {
                //if (!System.IO.Directory.Exists("images/checklist"))
                //    System.IO.Directory.CreateDirectory("images/checklist");

                fileName = "/images/checklist/" + hmamay.Value + hmgroup.Value + "_" + DateTime.Now.ToString("yyMMdd_HHmmss") + FileUpload1.FileName.Substring(FileUpload1.FileName.IndexOf("."), FileUpload1.FileName.Length - FileUpload1.FileName.IndexOf("."));
                filePath = MapPath(fileName);
                //FileUpload1.SaveAs(filePath);

                //Gets the Full Path using Filecontrol1 which points to actual location in the hardisk :) 

                //using (System.Drawing.Image Img = System.Drawing.Image.FromFile(System.IO.Path.GetFullPath(FileUpload1.PostedFile.FileName)))
                
                System.Drawing.Image Img = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);
                {
                    Size ThumbNailSize = NewImageSize(Img.Height, Img.Width, 800);

                    using (System.Drawing.Image ImgThnail = new Bitmap(Img, ThumbNailSize.Width, ThumbNailSize.Height))
                    {
                        ImgThnail.Save(filePath, Img.RawFormat);
                        ImgThnail.Dispose();
                    }
                    Img.Dispose();
                }
            }
            string strsql = "insert into  checklist_machine_problem (machine_no,sub_unit,machine_group,problem_start_date,problem_creator,problem_desc, problem_picture_addr,status) values (N'" +
                     hmamay.Value + "',N'" + hsub_unit.Value + "',N'" + hmgroup.Value + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','" + Session["LoginUserInfo"].ToString() + "',N'" + txtProb_desc.Text.Trim() + "','" + fileName + "','problem')"
                        ;
            myconn.myopen();
            myconn.mySqlExecute(strsql);
            myconn.myclose();


            rb1.Checked = true;
            rb2.Checked = false;
            rb3.Checked = false;
            rb4.Checked = false;

            query_machine_problem();
            showMessage("上傳成功！Báo cáo thành công.");
        }
      
        bool CheckFileType(string fileName)
        {

            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }
        void query_machine_problem()
        {
            query();
            DataTable prob_data = new DataTable();
            string strsql;
            //if (hmamay.Value != "")
                strsql = "select * from checklist_machine_problem where machine_no=N'"+hmamay.Value+"' ";
            //else
            //    strsql = "select * from checklist_machine_problem where machine_group='" + hmgroup.Value + "'";
            if (rb1.Checked == true)
                strsql = strsql + " and status='problem' order by problem_start_date asc";
            else if (rb2.Checked == true)
                strsql = strsql + " and status='accept' order by problem_start_date asc";
            else if (rb3.Checked == true)
                strsql = strsql + " and status='solution' order by problem_start_date asc";
            else if (rb4.Checked == true)
                strsql = strsql + " order by problem_start_date asc";
            myconn.myopen();
            prob_data = myconn.mysearch(strsql);

            prob_data.Columns.Add("code");
            for (int i = 0; i < prob_data.Rows.Count; i++)
            {
                prob_data.Rows[i]["code"] = "style = 'background-image: url("+ prob_data.Rows [i]["problem_picture_addr"].ToString()+ ");'";
                
            }
            GridView2.DataSource = prob_data;
            GridView2.DataBind();
            //this.Repeater1.DataSource = prob_data;
            //this.Repeater1.DataBind();
        }
        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            query_machine_problem();
        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            query_machine_problem();

        }
        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            try
            {
                if (Session["LoginUserInfo"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                
                string filePath="", fileName="";
                if (Page.IsValid && ((FileUpload)GridView2.Rows[e.RowIndex].FindControl("FileUploadSol")).HasFile && CheckFileType(((FileUpload)GridView2.Rows[e.RowIndex].FindControl("FileUploadSol")).FileName))
                {
                    //if (!System.IO.Directory.Exists("images/checklist"))
                    //    System.IO.Directory.CreateDirectory("images/checklist");

                    fileName = "/images/checklist/" + hmamay.Value + hmgroup.Value + "_" + DateTime.Now.ToString("yyMMdd_HHmmss") + ((FileUpload)GridView2.Rows[e.RowIndex].FindControl("FileUploadSol")).FileName.Substring(((FileUpload)GridView2.Rows[e.RowIndex].FindControl("FileUploadSol")).FileName.IndexOf("."), ((FileUpload)GridView2.Rows[e.RowIndex].FindControl("FileUploadSol")).FileName.Length- ((FileUpload)GridView2.Rows[e.RowIndex].FindControl("FileUploadSol")).FileName.IndexOf(".")) ;
                    filePath = MapPath(fileName);
                    //((FileUpload)GridView2.Rows[e.RowIndex].FindControl("FileUploadSol")).SaveAs(filePath);
                    //Image1.ImageUrl = fileName;
                    System.Drawing.Image Img = new System.Drawing.Bitmap(((FileUpload)GridView2.Rows[e.RowIndex].FindControl("FileUploadSol")).PostedFile.InputStream);
                    {
                        Size ThumbNailSize = NewImageSize(Img.Height, Img.Width, 800);

                        using (System.Drawing.Image ImgThnail = new Bitmap(Img, ThumbNailSize.Width, ThumbNailSize.Height))
                        {
                            ImgThnail.Save(filePath, Img.RawFormat);
                            ImgThnail.Dispose();
                        }
                        Img.Dispose();
                    }
                }


                string machine_no = ((Label)GridView2.Rows[e.RowIndex].FindControl("lblMa_no")).Text;
                string problem_start_date = ((Label)GridView2.Rows[e.RowIndex].FindControl("lblprob_date")).Text;
                string solution_desc = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("txtSol")).Text;
                string accept_desc = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("txtAcc")).Text;
                string predict_finish_date = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("txtPreF")).Text;
                string status = "";
                if (solution_desc.Trim() == "" && accept_desc.Trim() == "")
                {
                    showMessage("接收或解決內容不能為空！Nội dung tiếp nhận hoặc giải quyết không được để trống!");
                    return;
                }
                else if (solution_desc.Trim() != "")
                    status = "solution";
                else
                    status = "accept";


                DataTable select_data = new DataTable();
                string strsql = "update checklist_machine_problem set status ='" + status+"'" ;
                if (solution_desc.Trim() != "")
                    strsql = strsql + " ,solution_start_date = '"
                    + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "',solution_picture_addr='"
                    + fileName + "',solution_desc=N'" + solution_desc.Trim()
                    + "',solution_creator='" + Session["LoginUserInfo"].ToString() + "'";
                if (accept_desc.Trim() != "")
                    strsql = strsql + " ,accept_start_date = '"
                   + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") 
                   + "',accept_desc=N'" + accept_desc.Trim()
                   + "',predict_finish_date=N'" + predict_finish_date.Trim()
                   + "',accept_creator='" + Session["LoginUserInfo"].ToString() + "'";
                strsql = strsql+" where machine_no =N'"
                    + machine_no + "' and problem_start_date='"+ problem_start_date+"'";
                myconn.myopen();
                myconn.mySqlExecute(strsql);
                myconn.myclose();


                GridView2.EditIndex = -1;

                rb1.Checked = false;
                rb2.Checked = false;
                rb3.Checked = false;
                rb4.Checked = false;
                if (solution_desc.Trim() != "")
                    rb3.Checked = true;
                else
                    rb2.Checked = true;

                query_machine_problem();
                showMessage("解決成功！Báo giải quyết thành công.");
            }
            catch (Exception ms)
            {
                showMessage(ms.Message);
                Response.Write(ms.Message);
            }


        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        public Size NewImageSize(int OriginalHeight, int OriginalWidth, double FormatSize)
        {
            Size NewSize;
            double tempval;

            if (OriginalHeight > FormatSize && OriginalWidth > FormatSize)
            {
                if (OriginalHeight > OriginalWidth)
                    tempval = FormatSize / Convert.ToDouble(OriginalHeight);
                else
                    tempval = FormatSize / Convert.ToDouble(OriginalWidth);

                NewSize = new Size(Convert.ToInt32(tempval * OriginalWidth), Convert.ToInt32(tempval * OriginalHeight));
            }
            else
                NewSize = new Size(OriginalWidth, OriginalHeight); return NewSize;
        }
        protected void RadioButton_CheckedChanged(object sender, EventArgs e)

        {

            //RadioButton selectedRadioButton = (RadioButton)sender;

            //lblResult.Text = selectedRadioButton.Text;
            query_machine_problem();
        }

    }
}