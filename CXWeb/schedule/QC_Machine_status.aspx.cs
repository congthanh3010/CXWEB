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
	public partial class QC_Machine_status : System.Web.UI.Page
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

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //          btnUpload.Enabled = false;
        //          if (txtUser.Text.Trim() == "")
        //	{
        //		lblMessage.ForeColor = System.Drawing.Color.Red;
        //		lblMessage.Text = "Chưa nhập người tải lên";
        //              btnUpload.Enabled = true;
        //          }
        //	else if (!isHasFile)
        //	{
        //		lblMessage.ForeColor = System.Drawing.Color.Red;
        //		lblMessage.Text = "Chưa chọn file tải lên";
        //	}
        //	else
        //	{
        //		try
        //		{
        //                  btnUpload.Enabled = false;
        //                  // Create table data


        //                  DataTable data = new DataTable();
        //			data.Columns.Add(new DataColumn("0 (ProductCode)", typeof(string)));
        //			data.Columns.Add(new DataColumn("1 (ProductFace)", typeof(string)));
        //			data.Columns.Add(new DataColumn("2 (DateOfImplement)", typeof(DateTime)));
        //			data.Columns.Add(new DataColumn("3 (TimeOfImplement)", typeof(string)));
        //			data.Columns.Add(new DataColumn("4 (LocationCheck)", typeof(string)));
        //			data.Columns.Add(new DataColumn("5 (numberic)", typeof(decimal)));

        //			data.Columns.Add(new DataColumn("6 (Conclusion)", typeof(string)));
        //			data.Columns.Add(new DataColumn("7 (UpperLimit)", typeof(decimal)));
        //			data.Columns.Add(new DataColumn("8 (LowerLimit)", typeof(decimal)));

        //			data.Columns.Add(new DataColumn("9 (DevideNumber)", typeof(int)));
        //			data.Columns.Add(new DataColumn("10 (OperationInfo)", typeof(string)));
        //			data.Columns.Add(new DataColumn("11 (Serial_Number)", typeof(string)));

        //			data.Columns.Add(new DataColumn("12 (CreateDate)", typeof(DateTime)));
        //			data.Columns.Add(new DataColumn("13 (CreateBy)", typeof(string)));
        //			ISheet sheet = workbook.GetSheetAt(Convert.ToInt32(ddlSheet.SelectedValue));
        //			string user = ddlCode.Text + txtUser.Text.Trim();

        //			int index = 1;
        //			int rowCount = sheet.PhysicalNumberOfRows;
        //			int colCount = sheet.GetRow(0).PhysicalNumberOfCells;

        //			DataFormatter format = new DataFormatter();
        //			int rowNum = 3;
        //			string val = sheet.GetRow(rowNum - 1).Cells[0].StringCellValue.ToString().Trim();
        //			while (val != "")
        //			{

        //				IRow row = sheet.GetRow(rowNum);
        //				var newRow = data.NewRow();
        //				//string stt = "00" + index;
        //				//newRow[0] = row.GetCell(0).StringCellValue.Trim().ToString("yyyy-MM-dd");
        //				//newRow[1] = row.GetCell(1).StringCellValue.Trim(); //row.GetCell(2).StringCellValue.Trim();
        //				//newRow[2] = row.GetCell(3).;
        //				for (int i = 0; i <= 11; i++)
        //				{
        //					ICell cell = row.GetCell(i);
        //					switch (cell.CellType)
        //					{
        //						case CellType.String:
        //							newRow[i] = cell.StringCellValue.Trim();
        //							break;
        //						case CellType.Numeric:
        //							newRow[i] = (float)cell.NumericCellValue;
        //							break;
        //					}
        //				}
        //				newRow[12] = DateTime.Now;
        //				newRow[13] = user;



        //				data.Rows.Add(newRow);
        //				index++;
        //				rowNum++;
        //				val = "";
        //				try
        //				{
        //					val = (sheet.GetRow(rowNum) != null
        //						&& sheet.GetRow(rowNum).Cells.Count > 0
        //						&& sheet.GetRow(rowNum).Cells[0] != null)
        //							  ? sheet.GetRow(rowNum).Cells[0].ToString().Trim()
        //							  : "";
        //				}
        //				catch (Exception)
        //				{
        //					try
        //					{
        //						val = sheet.GetRow(rowNum).Cells[0].NumericCellValue.ToString().Trim();
        //					}
        //					catch (Exception)
        //					{

        //						val = "";
        //					}

        //				}

        //			}
        //			conn = new connectEIP();
        //			conn.myopen();

        //                  // Delete data already exists
        //                  string strQuery = "";
        //                  strQuery = "DELETE FROM QC_DEVIDE_CHECK WHERE convert(date,createdate) = '" + report_date.Text + "' and CreateBy = 'VN12345' ";
        //			conn.mySqlExecute(strQuery);

        //			// Bulk Copy to SQL Server
        //			string result = conn.BulkCopy(data, "QC_DEVIDE_CHECK");
        //			if (result != "")
        //			{
        //				lblMessage.Text = result;
        //				lblMessage.ForeColor = System.Drawing.Color.Red;
        //				conn.myclose();
        //				return;
        //			}

        //			// Load data
        //			strQuery = "SELECT * FROM QC_DEVIDE_CHECK WHERE convert(date,createdate) = '" + report_date.Text + "' order by id";
        //			DataTable log = conn.mysearch(strQuery);
        //			grvWLog.DataSource = log;
        //			grvWLog.DataBind();

        //			lblMessage.ForeColor = System.Drawing.Color.Blue;
        //			lblMessage.Text = $"Tải file lên thành công {log.Rows.Count} dòng lúc " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        //			conn.myclose();
        //			File.Delete(filePath);

        //                  btnUpload.Enabled = true;
        //              }
        //		catch (Exception ex)
        //		{

        //			lblMessage.ForeColor = System.Drawing.Color.Red;
        //			lblMessage.Text = ex.Message;
        //                  btnUpload.Enabled = true;
        //              }

        //	}

        //}

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
                    data.Columns.Add(new DataColumn("0 (ProductCode)", typeof(string)));
                    data.Columns.Add(new DataColumn("1 (ProductFace)", typeof(string)));
                    data.Columns.Add(new DataColumn("2 (DateOfImplement)", typeof(DateTime)));
                    data.Columns.Add(new DataColumn("3 (TimeOfImplement)", typeof(string)));
                    data.Columns.Add(new DataColumn("4 (LocationCheck)", typeof(string)));
                    data.Columns.Add(new DataColumn("5 (numberic)", typeof(decimal)));

                    data.Columns.Add(new DataColumn("6 (Conclusion)", typeof(string)));
                    data.Columns.Add(new DataColumn("7 (UpperLimit)", typeof(decimal)));
                    data.Columns.Add(new DataColumn("8 (LowerLimit)", typeof(decimal)));

                    data.Columns.Add(new DataColumn("9 (DevideNumber)", typeof(int)));
                    data.Columns.Add(new DataColumn("10 (OperationInfo)", typeof(string)));
                    data.Columns.Add(new DataColumn("11 (Serial_Number)", typeof(string)));

                    data.Columns.Add(new DataColumn("12 (CreateDate)", typeof(DateTime)));
                    data.Columns.Add(new DataColumn("13 (CreateBy)", typeof(string)));
                    ISheet sheet = workbook.GetSheetAt(Convert.ToInt32(ddlSheet.SelectedValue));
                    string user = ddlCode.Text + txtUser.Text.Trim();

                    int index = 1;
                    int rowCount = sheet.PhysicalNumberOfRows;
                    int colCount = sheet.GetRow(0).PhysicalNumberOfCells;

                    DataFormatter format = new DataFormatter();
                    int rowNum = 3;
                    //string val = sheet.GetRow(rowNum - 1).Cells[0].StringCellValue.ToString().Trim();
                    while (rowNum - 1 <= sheet.PhysicalNumberOfRows)
                    {

                        IRow row = sheet.GetRow(rowNum);
                        var newRow = data.NewRow();
                        //string stt = "00" + index;
                        //newRow[0] = row.GetCell(0).StringCellValue.Trim().ToString("yyyy-MM-dd");
                        //newRow[1] = row.GetCell(1).StringCellValue.Trim(); //row.GetCell(2).StringCellValue.Trim();
                        //newRow[2] = row.GetCell(3).;
                        if (row != null)
                        {
                            for (int i = 0; i <= 11; i++)
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
                            newRow[12] = DateTime.Now;
                            newRow[13] = user.ToString();
                            data.Rows.Add(newRow);
                        }





                        index++;
                        rowNum++;
                        //val = "";
                        try
                        {
                            //val = (sheet.GetRow(rowNum) != null
                            //	&& sheet.GetRow(rowNum).Cells.Count > 0
                            //	&& sheet.GetRow(rowNum).Cells[0] != null)
                            //		  ? sheet.GetRow(rowNum).Cells[0].ToString().Trim()
                            //		  : "";
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                            try
                            {
                                //val = sheet.GetRow(rowNum).Cells[0].NumericCellValue.ToString().Trim();
                            }
                            catch (Exception ex1)
                            {
                                lblMessage.Text = ex1.Message;
                                //val = "";
                            }

                        }

                    }
                    conn = new connectEIP();
                    conn.myopen();

                    // Delete data already exists
                    string strQuery = "";
                    //string strQuery = "DELETE FROM QC_DEVIDE_CHECK WHERE convert(date,createdate) = '" + report_date.Text + "' and CreateBy = 'VN12345' ";
                    //conn.mySqlExecute(strQuery);

                    // Bulk Copy to SQL Server
                    string result = conn.BulkCopy(data, "QC_DEVIDE_CHECK");
                    if (result != "")
                    {
                        lblMessage.Text = result;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        conn.myclose();
                        return;
                    }

                    // Load data
                    strQuery = "SELECT top 100 * FROM QC_DEVIDE_CHECK WHERE convert(date,createdate) = '" + report_date.Text + "' order by id";
                    DataTable log = conn.mysearch(strQuery);
                    grvWLog.DataSource = log;
                    grvWLog.DataBind();

                    lblMessage.ForeColor = System.Drawing.Color.Blue;
                    lblMessage.Text = $"Tải file lên thành công {rowCount.ToString()} dòng lúc " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

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
				//cell.ColumnSpan = 6;
				//cell.HorizontalAlign = HorizontalAlign.Center;
				//cell.VerticalAlign = VerticalAlign.Middle;
				//cell.Text = "Thông tin chung";
				//newRow1.Controls.Add(cell);

				//cell = new TableHeaderCell();
				//cell.ColumnSpan = 18;
				//cell.HorizontalAlign = HorizontalAlign.Center;
				//cell.VerticalAlign = VerticalAlign.Middle;
				//cell.Text = "Kết quả ";
				//newRow1.Controls.Add(cell);

				// Row header 2
				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "主产品料号";
				newRow2.Controls.Add(cell);

				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "产品正反面";
				newRow2.Controls.Add(cell);

				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "日期";
				newRow2.Controls.Add(cell);

				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "时间";
				newRow2.Controls.Add(cell);

				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "检查位置";
				newRow2.Controls.Add(cell);

				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "数值";
				newRow2.Controls.Add(cell);

				// Kết quả
				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "结论";
				newRow2.Controls.Add(cell);

				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "标准上限";
				newRow2.Controls.Add(cell);

				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "标准下限";
				newRow2.Controls.Add(cell);

				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "检查设备编号";
				newRow2.Controls.Add(cell);

				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "操作员信息";
				newRow2.Controls.Add(cell);

				cell = new TableHeaderCell();
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.VerticalAlign = VerticalAlign.Middle;
				cell.Text = "编号";
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


		#region Method
		private void LoadSheet()
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
		#endregion
	}
}