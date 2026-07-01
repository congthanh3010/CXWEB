using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace CXWeb.schedule
{
    public partial class xi_lan_log : System.Web.UI.Page
    {
        private static connectEIP conn;
        private static DataTable fields;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new connectEIP();

            fields = new DataTable();
            fields = GetFields(Page.GetType().BaseType.Name, "Xi_lan_log");

            DataTable data = LoadData();
            grvWLog.DataSource = data;
            grvWLog.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                string filePath = string.Concat(Server.MapPath("~/App_Data/Xi_lan_log" + Path.GetExtension(fileUpload.PostedFile.FileName)));
                fileUpload.SaveAs(filePath);
                DataTable data = CopyToDataTable(filePath);
                File.Delete(filePath);

                DataTable process = ProcessData(data);
                foreach (DataColumn col in process.Columns)
                    col.ColumnName = "(" + col.ColumnName + ")";
                string message = CopyToSQL(process, "Xi_lan_log");
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
                lblMessage.Text = "Dữ liệu đã được tải lên";
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
                + "AND FieldName NOT IN ('Id', 'MachineId', 'UploadTime') \n"
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

        protected DataTable ProcessData(DataTable data)
        {
            var enumFields = fields.AsEnumerable();

            DataTable result = new DataTable("ProcessData");
            result.Columns.Add(new DataColumn("Id", typeof(string)));
            result.Columns.Add(new DataColumn("MachineId", typeof(string)));
            foreach (DataRow field in fields.Rows)
                result.Columns.Add(new DataColumn(field["FieldName"].ToString(), Type.GetType(field["DataType"].ToString())));
            result.Columns.Add(new DataColumn("UploadTime", typeof(DateTime)));

            int maxId = GetMaxIndex();
            for (int rowIndex = 1; rowIndex < data.Rows.Count; rowIndex++)
            {
                DataRow newRow = result.NewRow();
                string numOrd = "00000" + (maxId + rowIndex).ToString();
                string id = DateTime.Now.ToString("yyyyMMdd") + numOrd.Substring(numOrd.Length - 5, 5);
                newRow["Id"] = id;
                newRow["MachineId"] = ddlMachine.SelectedValue;

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
                newRow["UploadTime"] = DateTime.Now;
                result.Rows.Add(newRow);
            }

            return result;
        }

        protected DataTable LoadData()
        {
            DataTable result = new DataTable();
            string strQuery = "SELECT *, CASE MachineId WHEN 'XL-001E' THEN 'E' ELSE 'F' END AS MachineCode \n"
                + "FROM Xi_lan_log WHERE CONVERT(VARCHAR,UploadTime,111)=CONVERT(VARCHAR,GETDATE(),111) \n"
                + "ORDER BY MachineCode, xml1 DESC";
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
            string strQuery = "SELECT ISNULL(RIGHT(MAX(Id),5),'00000') AS MaxId FROM Xi_lan_log WHERE CONVERT(VARCHAR,UploadTime,111)='" + DateTime.Now.ToString("yyyy/MM/dd") + "'";
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
                bound.DataField = "MachineCode";
                grid.Columns.Add(bound);
                foreach (DataRow field in fields.Rows)
                {
                    bound = new BoundField();
                    bound.DataField = field["FieldName"].ToString();
                    string format = "{0:" + field["ValueFormat"].ToString() + "}" + field["Unit"].ToString();
                    bound.DataFormatString = format;
                    grid.Columns.Add(bound);
                }
            }
        }

        protected void grvWLog_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].Width = new Unit("20px");
            for (int i = 0; i < fields.Rows.Count; i++)
            {
                e.Row.Cells[i + 1].HorizontalAlign = HorizontalAlign.Center;
                if (i == 0 || i == 1)
                    e.Row.Cells[i + 1].Width = new Unit("100px");
                else if (fields.Rows[i]["DataType"].ToString() == typeof(Int32).FullName
                    || fields.Rows[i]["DataType"].ToString() == typeof(Double).FullName)
                {
                    e.Row.Cells[i + 1].Width = new Unit("30px");
                }
                else if (fields.Rows[i]["DataType"].ToString() == typeof(DateTime).FullName)
                    e.Row.Cells[i + 1].Width = new Unit("60px");
                else
                    e.Row.Cells[i + 1].Width = new Unit("80px");
            }
        }

        protected void grvWLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow cn_header = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                GridViewRow vn_header = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);

                TableHeaderCell cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Wrap = true;
                cell.CssClass = "grvHeader";
                cell.Text = "機台";
                cn_header.Controls.Add(cell);

                cell = new TableHeaderCell();
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.Wrap = true;
                cell.CssClass = "grvHeader";
                cell.Text = "Chuyền";
                vn_header.Controls.Add(cell);

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

                grvWLog.Controls[0].Controls.AddAt(0, cn_header);
                grvWLog.Controls[0].Controls.AddAt(1, vn_header);
            }
        }
    }
}