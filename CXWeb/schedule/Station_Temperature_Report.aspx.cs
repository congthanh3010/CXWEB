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
    public partial class Station_Temperature_Report : System.Web.UI.Page
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
                cell.RowSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "TT";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.RowSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Trạm 變電站 ";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.RowSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Ngày";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 6;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "TRẠM ĐIỆN 變電站";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 4;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "TỦ PHÂN PHỐI 分配電箱";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 6;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "THIẾT BỊ SỬ DỤNG 設備使用";
                cell.CssClass = "grvHeader";
                newRow1.Controls.Add(cell);

                // Row header 2
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "BIẾN THẾ<br/>變壓機(KVA)";
                newRow2.Controls.Add(cell);

                // Row header 2
                // Trạm Điện
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "DAO CẮT <br/>總開關 （A)";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "CB <br/>開關 ";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "VỊ TRÍ DD<br/>量測電線區域 ";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Cường độ dòng điện  <br/>電流(A)";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Qui Cách dây dẫn <br/> 電纜規格";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "NHIỆT ĐỘ DÂY DẪN  <br/>電線溫度";
                newRow2.Controls.Add(cell);

                // Tủ Phân phối
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "TỦ PHÂN PHỐI <br/>分配電箱";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Năm lắp đặt dây điện <br/> 電線年限";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Cường độ dòng điện CB <br/>電流(A)";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Cường độ dòng điện Thục tế <br/>實際使用電流(A)";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "NHIỆT ĐỘ TỦ PHÂN PHỐI <br/>分配電箱溫度";
                newRow2.Controls.Add(cell);

                //Thiết bị sử dụng
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "設編號 <br/>MSM";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Năm lắp đặt dây điện  電線年限";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "NHIỆT ĐỘ <br/>温度 ";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "CB <br/>開關 ";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "AP MAX";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Cường độ dòng điện Thục tế  <br/>實際使用電流";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "GHI CHÚ <br/>備註";
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

            string strQuery = "SELECT * FROM Station_Temperature WHERE Ngay_Kiem_tra_2 >= '" + report_date.Text + "' and Ngay_Kiem_tra_2 <= '" + report_enddate.Text + "' and Station_No = '" + ddlQua_Trinh.Text + "'";
            string strMa_Tram = ddlQua_Trinh.Text;
            if (strMa_Tram == "T0")
                strQuery = "SELECT * FROM Station_Temperature WHERE Ngay_Kiem_tra_2 >= '" + report_date.Text + "' and Ngay_Kiem_tra_2 <= '" + report_enddate.Text + "'";

            strQuery = strQuery + " order by Ngay_Kiem_tra_2,Station_No, Stt_Id";
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
            string file_name = string.Format("Bảng dữ liệu Trạm Điện {0}.xls", DateTime.Now.ToString("yyyy-MM-dd"));
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
            using (FileStream fstream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\Station_temperature.xls"), FileMode.Open, FileAccess.Read))
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
            cellStyle1.DataFormat = workbook.CreateDataFormat().GetFormat("yyyy-MM-dd");

            ICellStyle cellStyle2 = workbook.CreateCellStyle();
            cellStyle2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle2.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle2.Alignment = HorizontalAlignment.Center;
            cellStyle2.VerticalAlignment = VerticalAlignment.Top;
            cellStyle2.DataFormat = workbook.CreateDataFormat().GetFormat("#,###");

            ICellStyle cellStyle3 = workbook.CreateCellStyle();
            cellStyle3.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle3.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle3.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle3.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle3.Alignment = HorizontalAlignment.Center;
            cellStyle3.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle4 = workbook.CreateCellStyle();
            cellStyle4.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle4.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle4.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle4.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle4.Alignment = HorizontalAlignment.Center;
            cellStyle4.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle5 = workbook.CreateCellStyle();
            cellStyle5.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.Alignment = HorizontalAlignment.Center;
            cellStyle5.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle6 = workbook.CreateCellStyle();
            cellStyle6.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.Alignment = HorizontalAlignment.Center;
            cellStyle6.DataFormat = workbook.CreateDataFormat().GetFormat("text");

            ICellStyle cellStyle7 = workbook.CreateCellStyle();
            cellStyle7.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle7.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle7.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle7.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle7.Alignment = HorizontalAlignment.Center;
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
            cellStyle6.Alignment = HorizontalAlignment.Center;
            cellStyle6.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle10 = workbook.CreateCellStyle();
            cellStyle10.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle10.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle10.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle10.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle10.Alignment = HorizontalAlignment.Center;
            cellStyle10.DataFormat = workbook.CreateDataFormat().GetFormat("text");

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
            cellStyle12.Alignment = HorizontalAlignment.Center;
            cellStyle12.DataFormat = workbook.CreateDataFormat().GetFormat("#,###");

            ICellStyle cellStyle13 = workbook.CreateCellStyle();
            cellStyle13.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle13.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle13.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle13.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle13.Alignment = HorizontalAlignment.Center;
            cellStyle13.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle14 = workbook.CreateCellStyle();
            cellStyle14.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle14.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle14.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle14.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle14.Alignment = HorizontalAlignment.Center;
            cellStyle14.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle15 = workbook.CreateCellStyle();
            cellStyle15.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle15.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle15.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle15.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle15.Alignment = HorizontalAlignment.Center;
            cellStyle15.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");


            ICellStyle cellStyle16 = workbook.CreateCellStyle();
            cellStyle16.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle16.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle16.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle16.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle16.Alignment = HorizontalAlignment.Center;
            cellStyle16.DataFormat = workbook.CreateDataFormat().GetFormat("text");


            ICellStyle cellStyle17 = workbook.CreateCellStyle();
            cellStyle17.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle17.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle17.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle17.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle17.Alignment = HorizontalAlignment.Center;
            cellStyle17.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle18 = workbook.CreateCellStyle();
            cellStyle18.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle18.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle18.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle18.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle18.Alignment = HorizontalAlignment.Center;
            cellStyle18.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle19 = workbook.CreateCellStyle();
            cellStyle19.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle19.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle19.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle19.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle19.Alignment = HorizontalAlignment.Center;
            cellStyle19.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle20 = workbook.CreateCellStyle();
            cellStyle20.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle20.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle20.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle20.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle20.Alignment = HorizontalAlignment.Center;
            cellStyle20.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle cellStyle21 = workbook.CreateCellStyle();
            cellStyle21.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle21.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle21.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle21.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle21.Alignment = HorizontalAlignment.Center;
            cellStyle21.DataFormat = workbook.CreateDataFormat().GetFormat("text");
            //Get data

            conn = new connectEIP();
            conn.myopen();

            string strQuery = "SELECT left(stt_id,1),Ngay_Kiem_tra_2, Station_No,TD_Bien_The,TD_Dao_Cat, TD_CB,TD_VT_DD,TD_Cuong_Do,TD_QC,TD_Nhiet_Do,  \n "
                + " TP_Tu_PP,Tp_Lap_Dat,TP_Cuong_Do,TP_Cuong_Do_TT,TP_Nhiet_Do,  \n"
                + "TB_MSM,TB_Lap_Dat,TB_Nhiet_Do,TB_CB,TB_AP_MAX,TB_Cuong_Do_TT,TB_Ghi_Chu \n"
                + " FROM Station_Temperature WHERE Ngay_Kiem_tra_2 >= '" + report_date.Text + "' and Ngay_Kiem_tra_2 <= '" + report_enddate.Text + "' and Station_No = '" + ddlQua_Trinh.Text + "'";

            string strMa_Tram = ddlQua_Trinh.Text;
            if (strMa_Tram == "T0")
                strQuery = "SELECT left(stt_id,1),Ngay_Kiem_tra_2, Station_No,TD_Bien_The,TD_Dao_Cat, TD_CB,TD_VT_DD,TD_Cuong_Do,TD_QC, TD_Nhiet_Do,  \n "
                + " TP_Tu_PP,Tp_Lap_Dat,TP_Cuong_Do,TP_Cuong_Do_TT,TP_Nhiet_Do,  \n"
                + "TB_MSM,TB_Lap_Dat,TB_Nhiet_Do,TB_CB,TB_AP_MAX,TB_Cuong_Do_TT,TB_Ghi_Chu \n"
                + " FROM Station_Temperature WHERE Ngay_Kiem_tra_2 >= '" + report_date.Text + "' and Ngay_Kiem_tra_2 <= '" + report_enddate.Text + "'";

            strQuery = strQuery + " order by Ngay_Kiem_tra_2,Station_No, stt_id";



            data = conn.mysearch(strQuery);
            


            conn.myclose();

            // Set value to cell
            numCellMerge = new List<int[]>();
            tmpIndex = new int[data.Columns.Count];
            tmpValue = new object[data.Columns.Count];
            int rowCount = data.Rows.Count;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 3);
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