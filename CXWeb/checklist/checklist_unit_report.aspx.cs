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

namespace CXWeb.checklist
{
    public partial class checklist_unit_report : System.Web.UI.Page
    {
        connectEIP myconn = new connectEIP();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                textDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                //textShift.SelectedValue = "CA1";
                query();

            }

        }
        void query()
        {
            string strsql = "";
            string strsql2 = "";
            //string date;
            //string shift;

            //if (DateTime.Now.Hour >= 18)
            //{
            //    shift = "CA2";
            //    date = DateTime.Now.ToString("yyyy/MM/dd");
            //}
            //else if (DateTime.Now.Hour < 6)
            //{
            //    shift = "CA2";
            //    date = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            //}
            //else
            //{
            //    shift = "CA1";
            //    date = DateTime.Now.ToString("yyyy/MM/dd");
            //}
            //-------------------------------------------------------------

            DataTable mydata = new DataTable();
            DataTable mydata2 = new DataTable();
            myconn.myopen();


            //strsql = "select * from checklist_unit_setting where sub_unit ='" + sub_unit + "' order by number";
            strsql = "select a.sub_unit,case a.sub_unit" +
                     " when 'CT1' then N'製一 Chế tạo 1'" +
                     " when 'CT2_DD' then N'製二單打 Dập đơn'" +
                     " when 'CT2_LH' then N'製二連續鍛打Liên hợp'" +
                     " when 'CT2_URPC' then N'製二退噴洗 Ủ rửa phun cát'" +
                     " when 'CT3' then N'製三 Chế tạo 3'" +
                     " when 'CT3_ML' then N'鋁製 Màng loa'" +
                     " when 'CT3_DG' then N'包裝 Đóng gói'" +
                     " when 'DDCX' then N'精密沖壓 Đột dập chính xác'" +
                     " when 'NC' then 'NC'" +
                     " when 'CNC' then 'CNC'" +
                     " when 'GC' then N'加工 GC'" +
                     " when 'XL' then N'滾電 Xi lăn'" +
                     " when 'XT' then N'吊電 Xi treo'" +
                     " when 'XD' then N'電著 Xi đen'" +
                     " end as unit_name" +
                     " , CONVERT(nvarchar, isnull(b.checked_ma_num, 0)) + '/' + CONVERT(nvarchar, count(*)) as check_status," +
                     " isnull(c.count_prob,0) count_prob" +
                     " from checklist_unit_setting a" +
                     " left outer join" +
                     " (SELECT don_vi, count(*) as checked_ma_num" +
                     " FROM checklist_table" +
                     " where thoi_gian = '" + textDate.Text + "'" +
                     " and nhom_may <> ten_may and sum_point is not null" +
                     " group by don_vi) b on a.sub_unit = b.don_vi" +
                     " left outer join"+
                     " (select sub_unit, count(*) as count_prob from checklist_machine_problem where status = 'problem'"+
                     " group by sub_unit) c" +
                     " on a.sub_unit = c.sub_unit" +
                     " group by a.sub_unit,b.checked_ma_num, c.count_prob" +
                     " order by a.sub_unit";
            mydata = myconn.mysearch(strsql);

            mydata.Columns.Add("link");
            for (int i = 0; i < mydata.Rows.Count; i++)
            {
                mydata.Rows[i]["link"] = "checklist_machine_selection.aspx?su=" + mydata.Rows[i]["sub_unit"] ;
            }


            GridView1.DataSource = mydata;
            GridView1.DataBind();

            //員工點檢

            strsql2 = "select nguoi_diem_kiem as check_creator,isnull(a.gen02,N'員工沒資料') check_creator_name,count(*) check_times" +
                      " from checklist_table" +
                      " left outer join" +
                      " (SELECT * FROM OPENQUERY(TIPTOP, '" +
                                      " SELECT gen01, gen02" +
                                      " FROM gen_file where genacti = ''Y''')) a" +
                      " on a.gen01 = nguoi_diem_kiem" +
                      " where thoi_gian = '" + textDate.Text +"' and nguoi_diem_kiem <> 'NULL'and nhom_may <> ten_may and sum_point is not null" +
                      " group by thoi_gian ,nguoi_diem_kiem,a.gen02" +
                      " order by thoi_gian";
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
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/checklist/checklist_unit_selection.aspx");
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            query();
        }
    }
}