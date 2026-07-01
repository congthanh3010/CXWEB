using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace CXWeb.schedule
{
    public partial class Ghi_Chep_Khuon_report : System.Web.UI.Page
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
                txtDFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtDTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                data = new DataTable();
                conn = new connectEIP();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (txtDFrom.Text.Trim() == "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa nhập ngày bắt đầu";
                return;
            }
            if (txtDTo.Text.Trim() == "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa nhập ngày kết thúc";
                return;
            }
            data = getWorkLog();

            data.Columns.Add("code");
            data.Columns.Add("Ten_file");
            data.Columns.Add("Ten_hinh");
            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i]["code"] = "style = 'background-image: url(" + data.Rows[i]["Hinh_Anh"].ToString() + ");'";
                if (data.Rows[i]["Hinh_Anh"].ToString().Length >= 18)
                {
                    data.Rows[i]["Ten_File"] = data.Rows[i]["Thuc_Te"].ToString().Substring(18, data.Rows[i]["Thuc_Te"].ToString().Length - 18);
                    data.Rows[i]["Ten_hinh"] = data.Rows[i]["Hinh_Anh"].ToString().Substring(18, data.Rows[i]["Hinh_Anh"].ToString().Length - 18);
                }
            }

            grvData.DataSource = null;
            grvData.DataSource = data;
            grvData.DataBind();
            lblMessage.Text = "";

            if (data.Rows.Count > 0)
                btnExcel.Enabled = true;
            else
                btnExcel.Enabled = false;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            var report = createReport();
            // Export Excel
            string file_name = string.Format("模具領用(修改)檢測紀錄 Ghi Chép do kiểm lãnh dùng(sua)khuôn {0}.xls", DateTime.Now.ToString("yyyy-MM-dd"));
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

        private DataTable getWorkLog()
        {
            string strQuery = "SELECT Ngay_Kiem,So_Kiem_Tra,Thanh_Tra,Qua_trinh,So_Luong_Kiem_Tra,Kich_Thuoc_NG,Chung_Loai,Kich_Thuoc_Ok,Loai_Khuon,Hinh_Anh,Thuc_Te \n"
                + "FROM Mod_Log \n"
                + "WHERE  Ngay_Kiem >= '" + txtDFrom.Text.Trim() + "' AND Ngay_Kiem <= '" + txtDTo.Text.Trim() + "' \n";

            string strChung_Loai = txtChung_Loai.Text.Trim();
            strChung_Loai = strChung_Loai.Replace("*", "%");

            if (txtChung_Loai.Text.Trim() != "")
                strQuery += "AND Chung_Loai Like '%"+ txtChung_Loai.Text + "%' \n";
          
            strQuery += "ORDER BY Ngay_Kiem DESC, So_Kiem_Tra, Chung_Loai";
            conn.myopen();
            DataTable result = conn.mysearch(strQuery);
            conn.myclose();
            return result;
        }

        protected void grvData_DataBound(object sender, EventArgs e)
        {
            for (int i = grvData.Rows.Count - 2; i >= 0; i--)
            {
                GridViewRow row = grvData.Rows[i];
                GridViewRow prev_row = grvData.Rows[i + 1];
                mergeCell(row, prev_row, 0);
            }
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
            using (FileStream fstream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\Mod_Log.xls"), FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(fstream);
            }

            ISheet sheet = workbook.GetSheet("Sheet1");
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
            cellStyle4.DataFormat = workbook.CreateDataFormat().GetFormat("#,###");

            ICellStyle cellStyle5 = workbook.CreateCellStyle();
            cellStyle5.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle5.Alignment = HorizontalAlignment.Center;
            cellStyle5.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00");

            ICellStyle cellStyle6 = workbook.CreateCellStyle();
            cellStyle6.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle6.Alignment = HorizontalAlignment.Center;
            cellStyle6.DataFormat = workbook.CreateDataFormat().GetFormat("HH:mm");

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
                    if (j < 3)
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
    }
}