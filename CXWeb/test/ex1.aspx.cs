using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace CXWeb.test
{
    
    public partial class ex1 : System.Web.UI.Page
    {
        public string CheckGioiTinh(object obj)
        {
            bool b = bool.Parse(obj.ToString());

            if (b)
                return "Nam.";
            else
                return "Nữ.";
        }

        private void load_data()
        {
            SqlConnection con = new SqlConnection("Data Source=INTRAWEB;Initial Catalog=EIP;Persist Security Info=True;User ID=sa;Password=sa@WEB");
            SqlDataAdapter da = new SqlDataAdapter("select * from checklist_table", con);
            DataTable tb = new DataTable();
            da.Fill(tb);

            gvSinhVien.DataSource = tb;
            gvSinhVien.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                load_data();
            }
        }
        //protected void btInsert_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection("server=.;database=bcdonlinesv;Integrated Security=true;");
        //    int phai = 0;
        //    if (RadioNam.Checked == true)
        //        phai = 1;
        //    SqlCommand cmd = new SqlCommand("insert into sinhvien(tensv,ngaysinh,phai,diachi,dienthoai,email) values('" + txtTen.Text + "'," + NgaySinh.SelectedDate.ToShortDateString() + ",'" + phai + "','" + txtDiaChi.Text + "','" + txtDienThoai.Text + "','" + txtEmail.Text + "')", con);
        //    con.Open(); // mo ket noi
        //    cmd.ExecuteNonQuery(); // thuc thi
        //    con.Close();
        //}
        //protected void btUpdate_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection("server=.;database=bcdonlinesv;Integrated security=true;");
        //    int phai = 0;
        //    if (RadioNam.Checked == true)
        //        phai = 1;
        //    SqlCommand cmd = new SqlCommand("update sinhvien set tensv='" + txtTen.Text + "',ngaysinh=" + NgaySinh.SelectedDate.ToShortDateString() + ",phai='" + phai + "',diachi='" + txtDiaChi.Text + "',dienthoai='" + txtDienThoai.Text + "',email='" + txtEmail.Text + "'  where masv='" + txtMa.Text + "'", con);
        //    con.Open(); // mo ket noi
        //    cmd.ExecuteNonQuery(); // thuc thi
        //    con.Close();
        //}
        //protected void btDelete_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection("server=.;database=bcdonlinesv;Integrated security=true;");
        //    SqlCommand cmd = new SqlCommand("delete from sinhvien  where masv='" + txtMa.Text + "'", con);
        //    con.Open(); // mo ket noi
        //    cmd.ExecuteNonQuery(); // thuc thi
        //    con.Close();
        //}
        protected void gvSinhVien_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = int.Parse(gvSinhVien.DataKeys[e.RowIndex].Value.ToString());
            SqlConnection con = new SqlConnection("server=.;database=bcdonlinesv;Integrated security=true;");
            SqlCommand cmd = new SqlCommand("delete from sinhvien where masv='" + id + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            load_data();
        }
        protected void gvSinhVien_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSinhVien.EditIndex = -1;
            load_data();
        }
        protected void gvSinhVien_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = int.Parse(gvSinhVien.DataKeys[e.RowIndex].Value.ToString());
            string ten = (gvSinhVien.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
            //string ngaysinh = (gvSinhVien.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text;
            string gioitinh = ((TextBox)gvSinhVien.Rows[e.RowIndex].FindControl("txtten_may")).Text;
            string diachi = (gvSinhVien.Rows[e.RowIndex].Cells[4].Controls[0] as TextBox).Text;
            string dt = (gvSinhVien.Rows[e.RowIndex].Cells[5].Controls[0] as TextBox).Text;
            string email = (gvSinhVien.Rows[e.RowIndex].Cells[6].Controls[0] as TextBox).Text;
            //SqlConnection con = new SqlConnection("server=.;database=bcdonlinesv;Integrated security=true;");
            //SqlCommand cmd = new SqlCommand("update sinhvien set tensv='" + ten + "',ngaysinh='" + ngaysinh + "',phai='" + gioitinh + "', diachi='" + diachi + "',dienthoai='" + dt + "',email='" + email + "' where masv='" + id + "'", con);
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            gvSinhVien.EditIndex = -1;
            load_data();
        }
        protected void gvSinhVien_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSinhVien.EditIndex = e.NewEditIndex;
            load_data();
        }
    }

}