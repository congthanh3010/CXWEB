using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace CXWeb.plc
{
    public partial class worker_qty : System.Web.UI.Page
    {
        private static connectEIP conn;
        private static string filePath;
        private static bool isHasFile = false;
        private static IWorkbook workbook;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim() == "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa nhập người tải lên";
            }
            else if (!isHasFile)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa chọn file tải lên";
            }
            else
            {
                try
                {
                    // Create table data
                    DataTable data = new DataTable();
                    data.Columns.Add(new DataColumn("0 (WSID)", typeof(string)));
                    data.Columns.Add(new DataColumn("1 (CNName)", typeof(string)));
                    data.Columns.Add(new DataColumn("2 (VNName)", typeof(string)));
                    //data.Columns.Add(new DataColumn("3 (OffEstimateQty)", typeof(int)));
                    //data.Columns.Add(new DataColumn("4 (OffRealQty)", typeof(int)));
                    //data.Columns.Add(new DataColumn("5 (PartEstimateQty)", typeof(int)));
                    //data.Columns.Add(new DataColumn("6 (PartRealQty)", typeof(int)));
                    data.Columns.Add(new DataColumn("3 (WorkQty)", typeof(int)));
                    data.Columns.Add(new DataColumn("4 (WorkTime)", typeof(double)));
                    data.Columns.Add(new DataColumn("5 (UploadUser)", typeof(string)));

                    ISheet sheet = workbook.GetSheetAt(Convert.ToInt32(ddlSheet.SelectedValue));
                    string user = ddlCode.Text + txtUser.Text.Trim();

                    int index = 1;
                    int rowCount = sheet.PhysicalNumberOfRows;
                    int colCount = sheet.GetRow(0).PhysicalNumberOfCells;

                    DataFormatter format = new DataFormatter();
                    int rowNum = 2;
                    string val = sheet.GetRow(rowNum).Cells[8].StringCellValue.Trim();
                    while (val != "")
                    {
                        IRow row = sheet.GetRow(rowNum);
                        var newRow = data.NewRow();
                        string stt = "00" + index;
                        newRow[0] = stt.Substring(stt.Length - 2, 2);
                        int[] colIndex = new int[] {0, 8, 9, 10 };
                        for (int i = 0; i < colIndex.Length; i++)
                        {
                            ICell cell = row.GetCell(colIndex[i]);
                            switch (cell.CellType)
                            {
                                case CellType.String:
                                    newRow[i + 1] = cell.StringCellValue.Trim();
                                    break;
                                case CellType.Numeric:
                                    newRow[i + 1] = newRow[i + 1].GetType().Name == "System.Double" ? cell.NumericCellValue : (int)cell.NumericCellValue;
                                    break;
                            }
                        }
                        newRow[5] = user;
                        data.Rows.Add(newRow);
                        index++;
                        rowNum++;
                        val = sheet.GetRow(rowNum).Cells[8].StringCellValue.Trim();
                    }

                    conn = new connectEIP();
                    conn.myopen();

                    // Delete data already exists
                    string strQuery = "DELETE FROM DepWorkerQty WHERE CreateDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                    conn.mySqlExecute(strQuery);

                    // Bulk Copy to SQL Server
                    string result = conn.BulkCopy(data, "DepWorkerQty");
                    if (result != "")
                    {
                        lblMessage.Text = result;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        conn.myclose();
                        return;
                    }

                    // Load data
                    strQuery = "SELECT ROW_NUMBER() OVER (ORDER BY WSID) AS NumOrd,* FROM DepWorkerQty WHERE CreateDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                    DataTable log = conn.mysearch(strQuery);
                    grvWLog.DataSource = log;
                    grvWLog.DataBind();

                    lblMessage.ForeColor = System.Drawing.Color.Blue;
                    lblMessage.Text = "Tải file lên thành công lúc " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                    conn.myclose();
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = ex.Message;
                }
            }
        }

        private bool isRowHasValue(IRow row)
        {
            DataFormatter formatter = new DataFormatter();
            for (int i = 3; i <= 6; i++)
            {
                if (formatter.FormatCellValue(row.Cells[i]).Trim() != "")
                    return true;
            }
            return false;
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa chọn file";
            }
            else
            {
                try
                {
                    // Upload excel file to server
                    string strFolderPath = Server.MapPath("~/App_Data");
                    string ext = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    filePath = strFolderPath + "/Worker_Qty" + ext;
                    FileUpload1.SaveAs(filePath);

                    if (File.Exists(filePath))
                    {
                        isHasFile = true;
                        using (FileStream fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            string extension = Path.GetExtension(filePath);
                            if (extension.ToLower() == ".xls")
                                workbook = new HSSFWorkbook(fstream);
                            else
                                workbook = new XSSFWorkbook(fstream);
                        }

                        ddlSheet.Items.Clear();
                        for (int i = 0; i < workbook.NumberOfSheets; i++)
                            ddlSheet.Items.Add(new ListItem(workbook.GetSheetName(i), i.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = ex.Message;
                }
            }
        }

        protected void grvWLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (row.RowType == DataControlRowType.Header)
            {
                GridViewRow newRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

                // Row header 1
                TableHeaderCell cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "TT";
                newRow.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Tên đơn vị";
                cell.CssClass = "grvHeader";
                newRow.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Số lượng nhân viên<br/>人數";
                cell.CssClass = "grvHeader";
                newRow.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Tổng số giờ<br/>總時數";
                cell.CssClass = "grvHeader";
                newRow.Controls.Add(cell);

                // Add header to gridview
                newRow.CssClass = "grvHeader";
                grvWLog.Controls[0].Controls.AddAt(0, newRow);
            }
        }
    }
}