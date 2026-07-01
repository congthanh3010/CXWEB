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
    public partial class QA_Log_Daily : System.Web.UI.Page
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
                    data.Columns.Add(new DataColumn("1 (Code_Id)", typeof(string)));              
                    data.Columns.Add(new DataColumn("2 (Nong_Do)", typeof(float)));
                   

                    data.Columns.Add(new DataColumn("3 (UploadUser)", typeof(string)));
                    data.Columns.Add(new DataColumn("4 (Ngay_Kiem_tra)", typeof(string)));


                    ISheet sheet = workbook.GetSheetAt(Convert.ToInt32(ddlSheet.SelectedValue));
                    string user = ddlCode.Text + txtUser.Text.Trim();

                    int index = 1;
                    int rowCount = sheet.PhysicalNumberOfRows;
                    int colCount = sheet.GetRow(0).PhysicalNumberOfCells;

                    DataFormatter format = new DataFormatter();
                    int rowNum = 4;
                    string val = sheet.GetRow(rowNum-1).Cells[0].StringCellValue.ToString().Trim();
                    while (val != "")
                    {
                       
                        IRow row = sheet.GetRow(rowNum);
                        var newRow = data.NewRow();
                        
                        for (int i = 0; i <= 2; i++ )
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
                        
                        newRow[3] = user;
                        newRow[4] = report_date.Text;

                        string strDAte = newRow[0].ToString();
                        // Load data
                        conn = new connectEIP();
                        string strQueryKT = "DELETE FROM QA_Daily WHERE Ngay = '" + strDAte + "' and Code_Id = '" + newRow[1].ToString() + "'";
                        conn.myopen();
                        conn.mySqlExecute(strQueryKT);
                        conn.myclose();
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
                    //string strQuery = "DELETE FROM QA_Daily WHERE Ngay_Kiem_tra = '" + report_date.Text + "'";
                    //conn.mySqlExecute(strQuery);

                    // Bulk Copy to SQL Server
                    string result = conn.BulkCopy(data, "QA_Daily");
                    if (result != "")
                    {
                        lblMessage.Text = result;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        conn.myclose();
                        return;
                    }

                    // Load data
                    string strQuery = "SELECT * FROM QA_Daily WHERE Ngay_Kiem_tra = '" + report_date.Text + "' order by CreateDate,Code_Id";
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
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Ngày";
                newRow1.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Mã Hóa chất";
                newRow1.Controls.Add(cell);
                // Kết quả
                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Text = "Nồng Độ 1";
                newRow1.Controls.Add(cell);
                

                newRow1.CssClass = "grvHeader";            

                grvWLog.Controls[0].Controls.AddAt(0, newRow1);
             

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