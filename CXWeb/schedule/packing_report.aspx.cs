using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CXWeb.schedule
{
    public partial class packing_report : System.Web.UI.Page
    {
        private static connectEIP conn;
        private static DataTable fields;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtDTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                conn = new connectEIP();

                fields = GetFields("packing", "packing_log");
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
            DataTable data = LoadData(txtDFrom.Text, txtDTo.Text);
            grvData.DataSource = data;
            grvData.DataBind();
            lblMessage.Text = "";
        }

        protected void grvData_DataBinding(object sender, EventArgs e)
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
            }
        }

        protected void grvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grvData.Columns[0].ItemStyle.Width = 80;
                grvData.Columns[1].ItemStyle.Width = 80;
                grvData.Columns[2].ItemStyle.Width = 100;
                grvData.Columns[3].ItemStyle.Width = 70;
                grvData.Columns[4].ItemStyle.Width = 80;

                for (int i = 5; i < fields.Rows.Count; i++)
                    grvData.Columns[i].ItemStyle.Width = 30;
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

                grvData.Controls[0].Controls.AddAt(0, cn_header);
                grvData.Controls[0].Controls.AddAt(1, vn_header);
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

        protected DataTable LoadData(string fromDate, string toDate)
        {
            string strQuery = "SELECT * FROM packing_log \n"
                + "WHERE pkl1 >= '" + fromDate + "' AND pkl1 <= '" + toDate + "' \n"
                + "ORDER BY pkl1 DESC";
            conn.myopen();
            DataTable result = conn.mysearch(strQuery);
            conn.myclose();
            return result;
        }
    }
}