using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace CXWeb.schedule
{
    public partial class QA_Water : System.Web.UI.Page
    {
        private static connectEIP conn;
        private static string filePath;
        private static bool isHasFile = false;
        private static IWorkbook workbook;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load();
            }
        }

        private void load()
        {
            report_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
                    data.Columns.Add(new DataColumn("0 (Ngay)", typeof(string)));
                    data.Columns.Add(new DataColumn("1 (XiLan_Time1)", typeof(float)));
                    data.Columns.Add(new DataColumn("2 (XiLan_Time2)", typeof(float)));
                    data.Columns.Add(new DataColumn("3 (XiTreo_Time1)", typeof(float)));
                    data.Columns.Add(new DataColumn("4 (XiTreo_Time2)", typeof(float)));
                    data.Columns.Add(new DataColumn("5 (XiDen_Time1)", typeof(float)));
                    data.Columns.Add(new DataColumn("6 (XiDen_Time2)", typeof(float)));

                    data.Columns.Add(new DataColumn("7 (UploadUser)", typeof(string)));
                    data.Columns.Add(new DataColumn("8 (Ngay_Kiem_tra)", typeof(string)));
                    

                    ISheet sheet = workbook.GetSheetAt(Convert.ToInt32(ddlSheet.SelectedValue));
                    string user = ddlCode.Text + txtUser.Text.Trim();

                    int index = 1;
                    int rowCount = sheet.PhysicalNumberOfRows;
                    int colCount = sheet.GetRow(0).PhysicalNumberOfCells;

                    DataFormatter format = new DataFormatter();
                    int rowNum = 1;
                    string val = sheet.GetRow(rowNum-1).Cells[0].StringCellValue.ToString().Trim();
                    while (val != "")
                    {
                       
                        IRow row = sheet.GetRow(rowNum);
                        var newRow = data.NewRow();
                        //string stt = "00" + index;
                        //newRow[0] = row.GetCell(0).StringCellValue.Trim().ToString();
                        //newRow[1] = row.GetCell(1).StringCellValue.Trim(); //row.GetCell(2).StringCellValue.Trim();
                        //newRow[2] = row.GetCell(3).;
                        for (int i = 0; i <= 6; i++ )
                        {
                            ICell cell = row.GetCell(i);
                            switch (cell.CellType)
                            {
                                case CellType.String:
                                    newRow[i] = cell.StringCellValue.Trim();
                                    break;
                                case CellType.Numeric:
                                    newRow[i] = (float)cell.NumericCellValue;
                                    break;
                            }
                        }
                        newRow[7] = user;
                        newRow[8] = report_date.Text;


                        data.Rows.Add(newRow);
                        index++;
                        rowNum++;
                        val = "";
                        try
                        {
                            val = sheet.GetRow(rowNum).Cells[0].StringCellValue.ToString().Trim();// sheet.GetRow(rowNum).Cells[0].StringCellValue.ToString().Trim();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                val = sheet.GetRow(rowNum).Cells[0].NumericCellValue.ToString().Trim();
                            }
                            catch (Exception)
                            {

                                val = "";
                            }
                            
                        }

                    }
                    conn = new connectEIP();
                    conn.myopen();

                    // Delete data already exists
                    string strQuery = "DELETE FROM QA_Water WHERE Ngay_Kiem_tra = '" + report_date.Text + "'";
                    conn.mySqlExecute(strQuery);

                    // Bulk Copy to SQL Server
                    string result = conn.BulkCopy(data, "QA_Water");
                    if (result != "")
                    {
                        lblMessage.Text = result;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        conn.myclose();
                        return;
                    }

                    // Load data
                    strQuery = "SELECT * FROM QA_Water WHERE Ngay_Kiem_tra = '" + report_date.Text + "' order by Ngay;";
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
                    HttpPostedFile file = FileUpload1.PostedFile;
                    string fileName = file.FileName;
                    filePath = strFolderPath + "/" + fileName;
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
                        grvWLog.DataSource = new DataTable();
                        grvWLog.DataBind();
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
                GridViewRow newRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                GridViewRow newRow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);
                GridViewRow newRow3 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Normal);

                // Row header 1
                TableHeaderCell cell = new TableHeaderCell();
                cell.ColumnSpan = 1;
                cell.RowSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Ngày";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 4;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Miệng nước ra của thiết bị≤10μs/cm  設備出水口≤10μs/cm";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Miệng nước ra ≤20μs/cm  設備出水口≤20μs / cm";
                newRow1.Controls.Add(cell);
                                
                // Row header 2
                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = " xi lăn 滾電";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "xi treo, màng loa 吊電,鋁音錐陽極";
                newRow2.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.ColumnSpan = 2;
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "xi đen 電著";
                newRow2.Controls.Add(cell);                

                newRow1.CssClass = "grvHeader";
                newRow2.CssClass = "grvHeader";

                grvWLog.Controls[0].Controls.AddAt(0, newRow1);
                grvWLog.Controls[0].Controls.AddAt(1, newRow2);

            }
        }
        bool KiemtraCactruongSo(string So)
        {
            try
            {
                Convert.ToInt32(So);
                return true;
            }
            catch { return false; }
        }

    }
}