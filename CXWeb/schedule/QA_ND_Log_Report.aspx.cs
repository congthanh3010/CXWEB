using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CXWeb.schedule
{
    public partial class QA_ND_Log_Report : System.Web.UI.Page
    {
        private static DataTable data;
        private static connectEIP conn;
        private static object[] tmpValue;
        private static int[] tmpIndex;
        private static List<int[]> numCellMerge;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                report_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                report_enddate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                data = new DataTable();
                conn = new connectEIP();
            }
        }

        protected void grvWLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (row.RowType == DataControlRowType.Header)
            {
                GridViewRow newRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                GridViewRow newRow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);
                GridViewRow newRow3 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Normal);

                // Row header 1
                TableHeaderCell cell = new TableHeaderCell();
                cell = new TableHeaderCell();
                cell.ColumnSpan = 7;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Thông Tin Hạng Mục";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Thông Số Mức 1";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Thông Số Mức 2";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Thông Số Mức 3";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Thông Số Mức 4";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Thông Số Mức 5";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Thông Số Mức 6";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                // Row header 2
                // Thông tin 
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Ngày";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Ngày Import";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Mã Máy";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Mã Hạng Mục";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Tên Hạng Mục";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Đơn Vị";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Tiêu Chuẩn";
                newRow2.Controls.Add(cell);


                // Thông số 
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế 1";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế TD 1";
                newRow2.Controls.Add(cell);
                
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế 2";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế TD 2";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế 3";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế TD 3";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế 4";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế TD 4";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế 5";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế TD 5";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế 6";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nhiệt kế TD 6";
                newRow2.Controls.Add(cell);

                newRow2.Controls.Add(cell);
                newRow1.CssClass = "grvHeader";
                newRow2.CssClass = "grvHeader";

                grvWLog.Controls[0].Controls.AddAt(0, newRow1);
                grvWLog.Controls[0].Controls.AddAt(1, newRow2);

            }
        }
        private void getWorkLog()
        {
            // Load data
            conn = new connectEIP();
            conn.myopen();

            string strQuery = "SELECT * FROM QA_Nhiet_Do_Log WHERE Ngay >= '" + report_date.Text + "' and Ngay <= '" + report_enddate.Text + "' and Code_ID Like '%" + txtCode.Text + "%'";
            string strMa_Tram = txtCode.Text;

            if (strMa_Tram == "")
                strQuery = "SELECT * FROM QA_Nhiet_Do_Log WHERE Ngay >= '" + report_date.Text + "' and Ngay <= '" + report_enddate.Text + "'";

            strQuery = strQuery + " order by Ngay_Kiem_tra, Ngay, Machine_ID, Code_Id, Code_Name";
            DataTable log = conn.mysearch(strQuery);
            grvWLog.DataSource = null;
            grvWLog.DataSource = log;
            grvWLog.DataBind();
            conn.myclose();
        }

        private void getWorkExcel()
        {
            var report = createReport();
            // Export Excel
            string file_name = string.Format("Bảng Báo Cảo Dữ Liệu Hóa Nghiệm {0}.xls", DateTime.Now.ToString("yyyy-MM-dd"));
            using (var export_data = new MemoryStream())
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", file_name));
                Response.BinaryWrite(report);
                Response.Flush();
                Response.End();
            }
        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            getWorkLog();
        }
        private void mergeCell(GridViewRow grvRow, GridViewRow grvPrevRow, int index)
        {
            if (grvRow.Cells[index].Text == grvPrevRow.Cells[index].Text)
            {
                if (grvPrevRow.Cells[index].RowSpan < 2)
                    grvRow.Cells[index].RowSpan = 2;
                else
                    grvRow.Cells[index].RowSpan = grvPrevRow.Cells[index].RowSpan + 1;
                grvPrevRow.Cells[index].Visible = false;

                if (index < 2)
                    mergeCell(grvRow, grvPrevRow, index + 1);
            }
        }

        private byte[] createReport()
        {
            HSSFWorkbook workbook;  // extension *.xls
            //XSSFWorkbook workbook;  // extension *.xlsx
            using (FileStream fstream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\QA_ND_Log.xls"), FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(fstream);
            }

            ISheet sheet = workbook.GetSheet("Sheet1");

            ICellStyle cellStyle0 = workbook.CreateCellStyle();
            cellStyle0.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle0.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle0.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle0.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle0.Alignment = HorizontalAlignment.Center;
            cellStyle0.DataFormat = workbook.CreateDataFormat().GetFormat("text");
                    
            ICellStyle cellStyle1 = workbook.CreateCellStyle();
            cellStyle1.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle1.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle1.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle1.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle1.Alignment = HorizontalAlignment.Center;
            cellStyle1.VerticalAlignment = VerticalAlignment.Top;
            cellStyle1.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyle2 = workbook.CreateCellStyle();
            cellStyle2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle2.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle2.Alignment = HorizontalAlignment.Center;
            cellStyle2.VerticalAlignment = VerticalAlignment.Top;
            cellStyle2.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyle3 = workbook.CreateCellStyle();
            cellStyle3.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle3.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle3.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle3.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle3.Alignment = HorizontalAlignment.Center;
            cellStyle3.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyle4 = workbook.CreateCellStyle();
            cellStyle4.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle4.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle4.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle4.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle4.Alignment = HorizontalAlignment.Center;
            cellStyle4.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyle5 = workbook.CreateCellStyle();
            cellStyle5.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.Alignment = HorizontalAlignment.Center;
            cellStyle5.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyle6 = workbook.CreateCellStyle();
            cellStyle6.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.Alignment = HorizontalAlignment.Right;
            cellStyle6.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle7 = workbook.CreateCellStyle();
            cellStyle7.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle7.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle7.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle7.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle7.Alignment = HorizontalAlignment.Right;
            cellStyle7.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle8 = workbook.CreateCellStyle();
            cellStyle8.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle8.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle8.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle8.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle8.Alignment = HorizontalAlignment.Center;
            cellStyle8.DataFormat = workbook.CreateDataFormat().GetFormat("text");


            ICellStyle cellStyle9 = workbook.CreateCellStyle();
            cellStyle6.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.Alignment = HorizontalAlignment.Right;
            cellStyle6.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle10 = workbook.CreateCellStyle();
            cellStyle10.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle10.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle10.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle10.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle10.Alignment = HorizontalAlignment.Right;
            cellStyle10.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle11 = workbook.CreateCellStyle();
            cellStyle11.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle11.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle11.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle11.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle11.Alignment = HorizontalAlignment.Center;
            cellStyle11.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyle12 = workbook.CreateCellStyle();
            cellStyle12.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle12.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle12.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle12.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle12.Alignment = HorizontalAlignment.Right;
            cellStyle12.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle13 = workbook.CreateCellStyle();
            cellStyle13.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle13.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle13.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle13.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle13.Alignment = HorizontalAlignment.Right;
            cellStyle13.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle14 = workbook.CreateCellStyle();
            cellStyle14.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle14.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle14.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle14.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle14.Alignment = HorizontalAlignment.Center;
            cellStyle14.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyle15 = workbook.CreateCellStyle();
            cellStyle15.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle15.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle15.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle15.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle15.Alignment = HorizontalAlignment.Right;
            cellStyle15.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");


            ICellStyle cellStyle16 = workbook.CreateCellStyle();
            cellStyle16.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle16.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle16.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle16.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle16.Alignment = HorizontalAlignment.Right;
            cellStyle16.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");


            ICellStyle cellStyle17 = workbook.CreateCellStyle();
            cellStyle17.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle17.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle17.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle17.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle17.Alignment = HorizontalAlignment.Center;
            cellStyle17.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyle18 = workbook.CreateCellStyle();
            cellStyle18.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle18.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle18.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle18.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle18.Alignment = HorizontalAlignment.Right;
            cellStyle18.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle19 = workbook.CreateCellStyle();
            cellStyle19.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle19.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle19.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle19.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle19.Alignment = HorizontalAlignment.Right;
            cellStyle19.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle20 = workbook.CreateCellStyle();
            cellStyle20.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle20.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle20.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle20.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle20.Alignment = HorizontalAlignment.Center;
            cellStyle20.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyle21 = workbook.CreateCellStyle();
            cellStyle21.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle21.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle21.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle21.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle21.Alignment = HorizontalAlignment.Right;
            cellStyle21.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle22 = workbook.CreateCellStyle();
            cellStyle22.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle22.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle22.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle22.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle22.Alignment = HorizontalAlignment.Right;
            cellStyle22.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle23 = workbook.CreateCellStyle();
            cellStyle23.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle23.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle23.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle23.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle23.Alignment = HorizontalAlignment.Center;
            cellStyle23.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyle24 = workbook.CreateCellStyle();
            cellStyle24.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle24.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle24.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle24.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle24.Alignment = HorizontalAlignment.Center;
            cellStyle24.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            //Get data

            conn = new connectEIP();
            conn.myopen();

            string strQuery = "SELECT CONVERT(VARCHAR,Ngay, 120) as Ngay,  Code_Id,  Code_Name, Machine_Id, Don_Vi, Tieu_Chuan,   \n "
                + " round(Nhiet_Ke_01,2) as Nhiet_Ke_01, round(Nhiet_Ke_TD_01,2) as Nhiet_Ke_TD_01, round(Nhiet_Ke_02,2) as Nhiet_Ke_02,round(Nhiet_Ke_TD_02,2) as Nhiet_Ke_TD_02, \n "
                + "  round(Nhiet_Ke_03,2) as Nhiet_Ke_03,round(Nhiet_Ke_TD_03,2) as Nhiet_Ke_TD_03,\n"
                + " round(Nhiet_Ke_04,2) as Nhiet_Ke_04,round(Nhiet_Ke_TD_04,2) as Nhiet_Ke_TD_04,  round(Nhiet_Ke_05,2) as Nhiet_Ke_05,round(Nhiet_Ke_TD_05,2) as Nhiet_Ke_TD_05,\n "
                + "  round(Nhiet_Ke_06,2) as Nhiet_Ke_06, Nhiet_Ke_TD_06,  Ngay_Kiem_tra \n"
                + " FROM QA_Nhiet_Do_Log WHERE Ngay >= '" + report_date.Text + "' and Ngay <= '" + report_enddate.Text + "' and Code_Id Like '%" + txtCode.Text + "%'";

            string strMa_Tram = txtCode.Text;
            if (strMa_Tram == "")
                strQuery = "SELECT CONVERT(VARCHAR,Ngay, 120) as Ngay,  Code_Id,  Code_Name, Machine_Id, Don_Vi, Tieu_Chuan,   \n "
                + " round(Nhiet_Ke_01,2) as Nhiet_Ke_01, Nhiet_Ke_TD_01, round(Nhiet_Ke_02,2) as Nhiet_Ke_02, Nhiet_Ke_TD_02, round(Nhiet_Ke_03,2) as Nhiet_Ke_03, Nhiet_Ke_TD_03, \n"
                + " round(Nhiet_Ke_04,2) as Nhiet_Ke_04, Nhiet_Ke_TD_04, round(Nhiet_Ke_05,2) as Nhiet_Ke_05, Nhiet_Ke_TD_05, round(Nhiet_Ke_06,2) as Nhiet_Ke_06, Nhiet_Ke_TD_06, Ngay_Kiem_tra \n"
                + " FROM QA_Nhiet_Do_Log WHERE Ngay >= '" + report_date.Text + "' and Ngay <= '" + report_enddate.Text + "'";

            strQuery = strQuery + " order by Ngay_Kiem_tra, Ngay, Machine_Id, Code_Id";



            data = conn.mysearch(strQuery);
            


            conn.myclose();

            // Set value to cell
            numCellMerge = new List<int[]>();
            tmpIndex = new int[data.Columns.Count];
            tmpValue = new object[data.Columns.Count];
            int rowCount = data.Rows.Count;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 2);
                setColumnValue(i, 0, 2);
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    if (j < 1)
                    {
                        if (j == 0)
                            cell.CellStyle = cellStyle1;
                        else
                            cell.CellStyle = cellStyle2;

                        if (tmpValue[j] != null)
                        {
                            if (tmpValue[j].GetType().Name == "DateTime")
                                cell.SetCellValue(Convert.ToDateTime(tmpValue[j]));
                            else
                                cell.SetCellValue(tmpValue[j].ToString());
                        }
                    }
                    else
                    {
                        object value = data.Rows[i][j];
                        switch (value.GetType().Name)
                        {
                            case "DateTime":
                                cell.CellStyle = cellStyle6;
                                if (!string.IsNullOrWhiteSpace(value.ToString()))
                                    cell.SetCellValue(Convert.ToDateTime(value));
                                break;
                            case "Decimal":
                                if (j == 8 || j == 9)
                                    cell.CellStyle = cellStyle5;
                                else
                                    cell.CellStyle = cellStyle4;
                                if (!string.IsNullOrWhiteSpace(value.ToString()))
                                    cell.SetCellValue(Convert.ToDouble(value));
                                break;
                            case "Int32":
                                cell.CellStyle = cellStyle4;
                                if (!string.IsNullOrWhiteSpace(value.ToString()))
                                    cell.SetCellValue(Convert.ToInt32(value));
                                break;
                            default:
                                cell.CellStyle = cellStyle3;
                                cell.SetCellValue(value.ToString());
                                break;
                        }
                    }
                }
            }

            //// Merge cell
            //for (int i = 0; i < 3; i++)
            //{
            //    if (tmpIndex[i] < (data.Rows.Count + 1))
            //        numCellMerge.Add(new int[] { tmpIndex[i] + 2, data.Rows.Count + 1, i, i });
            //}

            //foreach (var merge in numCellMerge)
            //{
            //    NPOI.SS.Util.CellRangeAddress range = new NPOI.SS.Util.CellRangeAddress(merge[0], merge[1], merge[2], merge[3]);
            //    sheet.AddMergedRegion(range);
            //}

            MemoryStream mstream = new MemoryStream();
            workbook.Write(mstream);
            return mstream.GetBuffer();
        }

        private void setColumnValue(int rowIndex, int colBegin, int colEnd)
        {
            if (colBegin <= colEnd)
            {
                if (rowIndex > tmpIndex[colBegin] && data.Rows[rowIndex][colBegin].ToString() == data.Rows[tmpIndex[colBegin]][colBegin].ToString())
                {
                    tmpValue[colBegin] = null;
                    setColumnValue(rowIndex, colBegin + 1, colEnd);
                }
                else
                {
                    for (int i = colBegin; i <= colEnd; i++)
                    {
                        if (rowIndex > (tmpIndex[i] + 1))
                            numCellMerge.Add(new int[] { tmpIndex[i] + 2, rowIndex + 1, i, i });
                        tmpValue[i] = data.Rows[rowIndex][i];
                        tmpIndex[i] = rowIndex;
                    }
                }
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            getWorkExcel();
        }
    }
}