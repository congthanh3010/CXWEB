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
    public partial class QA_Log_Report : System.Web.UI.Page
    {
        private static DataTable data;
        private static connectEIP conn;
        private static object[] tmpValue;
        private static int[] tmpIndex;
        private static List<int[]> numCellMerge;
        private static int index = -1;
        private static string strID;
        private DataTable log;
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
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvWLog, "Select$" + e.Row.RowIndex);
        }
        private void getWorkLog()
        {
            // Load data
            conn = new connectEIP();
            conn.myopen();

            string strQuery = "SELECT Ident00,Ngay, t2.Machine_ID, t1.Code_Id, t2.Code_Name, t2.Don_Vi, round(Nong_Do,2) as Nong_Do, UploadTime \n "
                + " from  QA_Daily t1 inner join QA_List t2 on t1.Code_Id = t2.Code_Id WHERE Ngay >= '" + report_date.Text + "' and Ngay <= '" + report_enddate.Text + "' ";
            string strMa_Tram = txtCode.Text;

            if (strMa_Tram != "")
                strQuery = strQuery + " and Code_ID Like '%" + txtCode.Text + "%''";

            strQuery = strQuery + " order by  Ngay, Machine_ID, Code_Id, Code_Name";
            log = conn.mysearch(strQuery);
            grvWLog.DataSource = null;
            grvWLog.DataSource = log;
            grvWLog.DataBind();
            conn.myclose();
        }

        private void getWorkExcel()
        {
            var report = createReport();
            // Export Excel
            string file_name = string.Format("Bảng Thống kê Dữ Liệu Hóa Nghiệm {0}.xls", DateTime.Now.ToString("yyyy-MM-dd"));
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
            using (FileStream fstream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\QA_Log.xls"), FileMode.Open, FileAccess.Read))
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
            cellStyle4.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

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


           

            //Get data

            conn = new connectEIP();
            conn.myopen();
            //CONVERT(VARCHAR,Ngay, 120) as Ngay,
            string strQuery = "SELECT Ident00,CONVERT(VARCHAR,Ngay, 120) as Ngay, t2.Machine_ID, t1.Code_Id, t2.Code_Name, t2.Don_Vi, round(Nong_Do,2) as Nong_Do, CONVERT(VARCHAR,UploadTime, 120) as UploadTime  \n "
                + " from QA_Daily t1 inner join QA_List t2 on t1.Code_Id = t2.Code_Id  \n"
                + " WHERE Ngay >= '" + report_date.Text + "' and Ngay <= '" + report_enddate.Text + "'";
            strQuery = strQuery + " order by  Ngay, Machine_Id, Code_Id";

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
                                cell.CellStyle = cellStyle1;
                                if (!string.IsNullOrWhiteSpace(value.ToString()))
                                    cell.SetCellValue(Convert.ToDateTime(value));
                                break;
                            case "Decimal":
                                if (j == 8 || j == 9)
                                    cell.CellStyle = cellStyle4;
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
                                cell.CellStyle = cellStyle4;
                                cell.SetCellValue(value.ToString());
                                break;
                        }
                    }
                }
            }

        

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

        protected void grvWLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grvWLog.SelectedIndex >= 0 )
            {
                index = grvWLog.SelectedIndex;
                strID = grvWLog.SelectedDataKey.Value.ToString();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if(strID != "")
            {
                string strQuery = "delete from QA_Daily where Ident00 = " + strID;
                conn.myopen();

                conn.mySqlExecute(strQuery);
                conn.myclose();
                strID = "";
                getWorkLog();
            }
            
        }

     
    }
}