using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace CXWeb.schedule
{
    public partial class packing : System.Web.UI.Page
    {
        private static connectEIP conn;
        private static DataTable fields;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn = new connectEIP();
                fields = new DataTable();
                fields = GetFields(Page.GetType().BaseType.Name, "packing_log");
            }

            DataTable log = LoadData();
            grvWLog.DataSource = log;
            grvWLog.DataBind();
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                string filePath = string.Concat(Server.MapPath("~/App_Data/Packing_log" + Path.GetExtension(fileUpload.PostedFile.FileName)));
                fileUpload.SaveAs(filePath);
                DataTable data = CopyToDataTable(filePath);
                File.Delete(filePath);

                DateTime impTime = DateTime.Now;
                DataTable process = ProcessData(data, impTime);
                foreach (DataColumn col in process.Columns)
                    col.ColumnName = "(" + col.ColumnName + ")";
                string message = CopyToSQL(process, "packing_log");
                if (!string.IsNullOrWhiteSpace(message))
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = message;
                    return;
                }

                DataTable recent = LoadData();
                grvWLog.DataSource = recent;
                grvWLog.DataBind();

                lblMessage.ForeColor = System.Drawing.Color.Blue;
                lblMessage.Text = $"Đã tải lên {process.Rows.Count} dòng";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Chưa chọn file tải lên";
            }
        }

        protected DataTable GetFields(string function, string TableName)
        {
            DataTable result = new DataTable();
            string strQuery = "SELECT FieldName, VNTitle, CNTitle, ExcelColumn, DataType, ValueFormat, Unit \n"
                + "FROM ProgramField \n"
                + "WHERE ProgramCode='" + function + "' AND TableName='" + TableName + "' \n"
                + "AND FieldName NOT IN ('Id', 'ImportTime') \n"
                + "ORDER BY ExcelColumn";
            conn.myopen();
            result = conn.mysearch(strQuery);
            conn.myclose();
            return result;
        }

        protected DataTable CopyToDataTable(string filePath)
        {
            // Connection string to excel Workbook
            DataTable result = new DataTable();
            string connString = string.Empty;
            string HDR = "Yes";
            string ext = Path.GetExtension(filePath);
            if (ext == ".xlsx")
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=1\"";
            else
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=1\"";

            using (OleDbConnection excelConn = new OleDbConnection(connString))
            {
                excelConn.Open();
                try
                {
                    DataTable sheet = excelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet.Rows[0]["TABLE_NAME"].ToString() + "]", excelConn);
                    cmd.CommandType = CommandType.Text;

                    // Fill data to table
                    result = new DataTable(sheet.Rows[0]["TABLE_NAME"].ToString());
                    new OleDbDataAdapter(cmd).Fill(result);
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Lỗi tải file: " + ex;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                excelConn.Close();
            }
            return result;
        }

        protected DataTable ProcessData(DataTable data, DateTime time)
        {
            var enumFields = fields.AsEnumerable();

            DataTable result = new DataTable("ProcessData");
            result.Columns.Add(new DataColumn("Id", typeof(string)));
            foreach (DataRow field in fields.Rows)
                result.Columns.Add(new DataColumn(field["FieldName"].ToString(), Type.GetType(field["DataType"].ToString())));
            result.Columns.Add(new DataColumn("ImportTime", typeof(DateTime)));

            int maxId = GetMaxIndex();
            for (int rowIndex = 0; rowIndex < data.Rows.Count; rowIndex++)
            {
                DataRow newRow = result.NewRow();
                string numOrd = "00000" + (maxId + rowIndex + 1).ToString();
                string id = DateTime.Now.ToString("yyyyMMdd") + numOrd.Substring(numOrd.Length - 5, 5);
                newRow["Id"] = id;

                DataRow row = data.Rows[rowIndex];
                for (int colIndex = 0; colIndex < data.Columns.Count; colIndex++)
                {
                    if (enumFields.Any(x => Convert.ToInt32(x["ExcelColumn"]) == colIndex))
                    {
                        var field = enumFields.FirstOrDefault(x => Convert.ToInt32(x["ExcelColumn"]) == colIndex);
                        if (!string.IsNullOrWhiteSpace(row[colIndex].ToString()))
                        {
                            string value = row[colIndex].ToString().Trim(field["Unit"].ToString().ToCharArray());
                            try { newRow[field["FieldName"].ToString()] = Convert.ChangeType(value, Type.GetType(field["DataType"].ToString())); } catch { }
                        }
                    }
                }
                newRow["ImportTime"] = time;
                result.Rows.Add(newRow);
            }

            return result;
        }

        protected DataTable LoadData()
        {
            DataTable result = new DataTable();
            string strQuery = "SELECT * FROM packing_log WHERE LEFT(Id,8) = CONVERT(VARCHAR,GETDATE(),112) ORDER BY Id, ImportTime DESC";
            conn.myopen();
            result = conn.mysearch(strQuery);
            conn.myclose();
            return result;
        }

        protected string CopyToSQL(DataTable data, string desTableName)
        {
            string result = string.Empty;

            conn.myopen();
            result = conn.BulkCopy(data, desTableName);
            conn.myclose();

            return result;
        }

        protected int GetMaxIndex()
        {
            conn.myopen();
            string strQuery = "SELECT ISNULL(RIGHT(MAX(Id),5),'00000') AS MaxId FROM packing_log WHERE LEFT(Id,8) = CONVERT(VARCHAR,GETDATE(),112)";
            DataTable data = conn.mysearch(strQuery);
            conn.myclose();
            string id = data.Rows[0]["MaxId"].ToString();
            return int.Parse(id);
        }

        protected void grvWLog_DataBinding(object sender, EventArgs e)
        {
            var grid = sender as GridView;
            if (grid.Columns.Count == 0)
            {
                BoundField bound = new BoundField();
                foreach (DataRow field in fields.Rows)
                {
                    bound = new BoundField();
                    bound.DataField = field["FieldName"].ToString();
                    string format = "{0:" + field["ValueFormat"].ToString() + "}" + field["Unit"].ToString();
                    bound.DataFormatString = format;
                    grid.Columns.Add(bound);
                }

                bound = new BoundField();
                bound.DataField = "ImportTime";
                bound.DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}";
                grid.Columns.Add(bound);
            }
        }

        protected void grvWLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grvWLog.Columns[0].ItemStyle.Width = 80;
                grvWLog.Columns[1].ItemStyle.Width = 80;
                grvWLog.Columns[2].ItemStyle.Width = 100;
                grvWLog.Columns[3].ItemStyle.Width = 70;
                grvWLog.Columns[4].ItemStyle.Width = 80;

                for (int i = 5; i < fields.Rows.Count; i++)
                    grvWLog.Columns[i].ItemStyle.Width = 30;

                grvWLog.Columns[fields.Rows.Count].ItemStyle.Width = 150;
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow cn_header = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                GridViewRow vn_header = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);

                TableHeaderCell cell = new TableHeaderCell();
                for (int i = 0; i < fields.Rows.Count; i++)
                {
                    cell = new TableHeaderCell();
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Wrap = true;
                    cell.CssClass = "grvHeader";
                    cell.Text = fields.Rows[i]["CNTitle"].ToString();
                    cn_header.Controls.Add(cell);

                    cell = new TableHeaderCell();
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Wrap = true;
                    cell.CssClass = "grvHeader";
                    cell.Text = fields.Rows[i]["VNTitle"].ToString();
                    vn_header.Controls.Add(cell);
                }

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Wrap = true;
                cell.CssClass = "grvHeader";
                cell.Text = "Import Time";
                cn_header.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Wrap = true;
                cell.CssClass = "grvHeader";
                cell.Text = "Thời gian tải lên";
                vn_header.Controls.Add(cell);

                grvWLog.Controls[0].Controls.AddAt(0, cn_header);
                grvWLog.Controls[0].Controls.AddAt(1, vn_header);
            }
        }
    }
}