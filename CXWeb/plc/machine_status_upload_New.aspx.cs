using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CXWeb.plc
{
    public partial class machine_status_upload_New : System.Web.UI.Page
    {
        connectDB myconn = new connectDB();
        protected static string report_msg = " ";
        protected static string s_machine_status = "";

        protected void Page_Load(object sender, EventArgs e)
        {
          
            
            if (!IsPostBack)
            {
                report_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                report_dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                GetTramList();
                GetMachineList();
                AddTime();
                AddMaNv();
                //txtHour.Text = strHours;
                //txtMin.Text = strMin;

                SetTimeAll();
            }
        }
        #region Event 
        protected void btnSave_Click(object sender, EventArgs e)
        {

            string sql = "";
            string test_hour = "";
            string test_min = "";
            DateTime beforeSt_Time_from;
            DateTime beforeSt_Time_to;
            TimeSpan s_stTime;
            DataTable mydata = new DataTable();
            DataTable machine_data = new DataTable();
            //Xác định ngày
            if (TxtMa_Nv.Text == "")
            {
                report_msg = "Phải nhập Mã Nv !!!";
                //showMessage(report_msg);
                return;
            }
            if(s_machine_status == "")
            {
                report_msg = "Phải cập nhật trạng thái !!!";
                return;
            }
            if(txtMachine_no.Text =="")
            {
                report_msg = "Mã máy rỗng. Yêu cầu chọn đúng Nhân viên và Mã máy !!!";
                return;
            }
            try
            {
                test_hour = status_time_start.Text.Substring(0, 2);
                test_min = status_time_start.Text.Substring(3, 2);
                if (Convert.ToInt32(test_hour) < 0 || Convert.ToInt32(test_hour) > 23
                    || Convert.ToInt32(test_min) < 0 || Convert.ToInt32(test_min) > 59)
                {
                    report_msg = "Thời gian bắt đầu nhập sai !!!";
                    //showMessage(report_msg);
                    return;
                }
            }
            catch
            {
                report_msg = "Thời gian bắt đầu nhập sai !!!";
                //showMessage(report_msg);
                return;
            }

            try
            {
                test_hour = status_time_end.Text.Substring(0, 2);
                test_min = status_time_end.Text.Substring(3, 2);
                if (Convert.ToInt32(test_hour) < 0 || Convert.ToInt32(test_hour) > 23
                    || Convert.ToInt32(test_min) < 0 || Convert.ToInt32(test_min) > 59)
                {
                    report_msg = "Thời gian kết thúc nhập sai !!!";
                    //showMessage(report_msg);
                    return;
                }
            }
            catch
            {
                report_msg = "Thời gian kết thúc  nhập sai !!!";
                //showMessage(report_msg);
                return;
            }

            string s_st_time_start = report_date.Text + " " + status_time_start.Text;
            string s_st_time_end = report_dateEnd.Text + " " + status_time_end.Text;

            //Compare Begin and End time
            DateTime Time_Begin = Convert.ToDateTime(s_st_time_start);
            DateTime Time_End = Convert.ToDateTime(s_st_time_end);

            if (Time_Begin > Time_End)
            {
                report_msg = "Thời gian kết thúc không thể nhỏ hơn thời gian bắt đầu !!! Vui lòng Kiểm tra lại.";
                //showMessage(report_msg);
                return;
            }
            string s_shift = "";
            if (Convert.ToInt32(test_hour) >= 6 && Convert.ToInt32(test_hour) < 18)
            {
                s_shift = "CA1";
            }
            else
                s_shift = "CA2";
            ////校验机台编号
            //myconn.myopen();
            //sql = "  select a.* " +
            //       " FROM OPENQUERY(TIPTOP, " +
            //       " 'select * " +
            //       " from eci_file t " +
            //       " where t.eciacti=''Y'' " +
            //       " and t.eci01 = ''" + txtMachine_no.Text + "'''   ) a";
            //machine_data = myconn.mysearch(sql);
            //myconn.myclose();

            //if (machine_data.Rows.Count == 0)
            //{
            //    report_msg = "Tên máy nhập sai";
            //    //showMessage(report_msg);
            //    return;
            //}
            //计算状态时间 

            myconn.myopen();
            mydata = myconn.mysearch("SELECT TOP (1)* FROM MACHINE_STATUS where dMachine ='" + txtMachine_no.Text + "' order by dStTimeStart desc ");
            myconn.myclose();

            beforeSt_Time_to = Convert.ToDateTime(s_st_time_start);

            if (mydata.Rows.Count != 0)
            {
                beforeSt_Time_from = Convert.ToDateTime(mydata.Rows[0]["dStTimeStart"].ToString());
                if (beforeSt_Time_from > beforeSt_Time_to)
                {
                    report_msg = "Không thể báo thời điểm trước " + beforeSt_Time_from.ToString("yyyy-MM-dd HH:mm"); ;
                    //showMessage(report_msg);

                    return;
                }
            }
            else
            {
                beforeSt_Time_from = beforeSt_Time_to;
            }




            if (beforeSt_Time_to > DateTime.Now)
            {
                report_msg = "Không thể báo thời điểm sau " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                //showMessage(report_msg);

                return;
            }

            s_stTime = beforeSt_Time_to.Subtract(beforeSt_Time_from);

            //执行新增
            myconn.myopen();
            //myconn.mySqlExecute("insert into COD_CODE (class,code,description,description2) values ('report5',N'" + mat_no.Text + "',N'" + remark.Text + "',N'" + remark2.Text + "') ");
            myconn.mySqlExecute("delete from MACHINE_STATUS where dMachine ='" + txtMachine_no.Text + "' and dStTimeStart = '" + s_st_time_start + ":00.000' ");

            string strSql = "insert into MACHINE_STATUS (dMachine,dStTimeStart,dShift,dStatus,dCreateTime,dCreateUser,dStTimeEnd,cong_doan,Chung_Loai,User_Mod) values ('" +
                txtMachine_no.Text + "','" + s_st_time_start + "','" + s_shift + "','" + s_machine_status + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "', '" + TxtMa_Nv.Text + "','"+ s_st_time_end + 
                "','"+ ddlQua_Trinh.Text +"','" + txtChung_Loai.Text + "','" + txtUser_Mod.Text + "'  ) ";

            myconn.mySqlExecute("insert into MACHINE_STATUS (dMachine,dStTimeStart,dShift,dStatus,dCreateTime,dCreateUser,dStTimeEnd,cong_doan,Chung_Loai,User_Mod) values ('" +
                txtMachine_no.Text + "','" + s_st_time_start + "','" + s_shift + "','" + s_machine_status + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "', '" + TxtMa_Nv.Text + "','" + s_st_time_end +
                "','" + ddlQua_Trinh.Text + "','" + txtChung_Loai.Text + "','" + txtUser_Mod.Text + "'  ) ");

            myconn.mySqlExecute("update MACHINE_STATUS set dStTime='" + s_stTime.TotalMinutes + "' where dMachine ='" + txtMachine_no.Text + "' and dStTimeStart = '" + beforeSt_Time_from.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            myconn.myclose();


            
            ResetButton();
            SetTimeAll();
            report_msg = "LƯU THÀNH CÔNG--" + status_time_start.Text;
            s_machine_status = "";
            ddlQua_Trinh.SelectedIndex = 0;
            txtUser_Mod.Text = "";
            txtChung_Loai.Text = "";
            TxtMa_Nv.Text = "";
            txtMa_So.Text = ""; 

        }

        protected void btn_Button17(object sender, EventArgs e)
        {
            ResetButton();
            string StrColor = Button17.BackColor.ToString();
            Button17.BackColor = System.Drawing.Color.Green;
            
            s_machine_status = Button17.Text.Substring(0, 3);
           
        }

        protected void btn_Button6(object sender, EventArgs e)
        {
            ResetButton();
            Button6.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button6.Text.Substring(0, 3);
        }

        protected void btn_Button7(object sender, EventArgs e)
        {
            ResetButton();
            Button7.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button7.Text.Substring(0, 3);
        }

        protected void btn_Button8(object sender, EventArgs e)
        {
            ResetButton();
            Button8.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button8.Text.Substring(0, 3);
        }
        protected void btn_Button1(object sender, EventArgs e)
        {
            ResetButton();
            Button1.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button1.Text.Substring(0, 3);
        }
        protected void btn_Button2(object sender, EventArgs e)
        {
            ResetButton();
            Button2.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button2.Text.Substring(0, 3);
        }
        protected void btn_Button3(object sender, EventArgs e)
        {
            ResetButton();
            Button3.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button3.Text.Substring(0, 3);
        }
        protected void btn_Button4(object sender, EventArgs e)
        {
            ResetButton();
            Button4.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button4.Text.Substring(0, 3);
        }
        protected void btn_Button5(object sender, EventArgs e)
        {
            ResetButton();
            Button5.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button5.Text.Substring(0, 3);
        }
        protected void btn_Button9(object sender, EventArgs e)
        {
            ResetButton();
            Button9.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button9.Text.Substring(0, 3);
        }
        protected void btn_Button10(object sender, EventArgs e)
        {
            ResetButton();
            Button10.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button10.Text.Substring(0, 3);
        }
        protected void btn_Button11(object sender, EventArgs e)
        {
            ResetButton();
            Button11.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button11.Text.Substring(0, 3);
        }
        protected void btn_Button13(object sender, EventArgs e)
        {
            ResetButton();
            Button13.BackColor = System.Drawing.Color.Green;

            s_machine_status = Button13.Text.Substring(0, 3);


        }
        protected void Button12_Click(object sender, EventArgs e)
        {
            ResetButton();
            Button12.BackColor = System.Drawing.Color.Green;

            s_machine_status = Button12.Text.Substring(0, 3);

        }
        protected void btn_Button14(object sender, EventArgs e)
        {
            ResetButton();
            Button14.BackColor = System.Drawing.Color.Green;
            s_machine_status = Button14.Text.Substring(0, 3);
        }
        protected void btn_Button15(object sender, EventArgs e)
        {
            //ResetButton();
            //Button15.BackColor = System.Drawing.Color.Green;
            //s_machine_status = Button15.Text.Substring(0, 3);
            //btn15.
        }

        protected void txtTram_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMachineList();
            report_msg = "";
        }
        protected void txtMa_Trama_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMachineList();
            report_msg = "";
        }
        protected void txtMa_Trama_TextChanged(object sender, EventArgs e)
        {
            GetMachineList();
            report_msg = "";
        }//txtHour_SelectedIndexChanged

        protected void txtHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetTime();
        }
        protected void txtMin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetTime();
            report_msg = "";
        }

        protected void txtMa_Nv1_SelectedIndexChanged(object sesnder, EventArgs e)
        {
            SetTMaNV();
            report_msg = "";
        }
        protected void txtMa_Nv2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTMaNV();
            report_msg = "";
        }

        protected void txtMa_Nv3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTMaNV();
            report_msg = "";
        }

        protected void txtMa_Nv4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTMaNV();
            report_msg = "";
        }
        #endregion
        #region Proc
        private void ResetButton()
        {
            Button1.BackColor = System.Drawing.Color.Empty;
            Button2.BackColor = System.Drawing.Color.Empty;
            Button3.BackColor = System.Drawing.Color.Empty;
            Button4.BackColor = System.Drawing.Color.Empty;
            Button5.BackColor = System.Drawing.Color.Empty;
            Button6.BackColor = System.Drawing.Color.Empty;
            Button7.BackColor = System.Drawing.Color.Empty;
            Button8.BackColor = System.Drawing.Color.Empty;
            Button9.BackColor = System.Drawing.Color.Empty;
            Button10.BackColor = System.Drawing.Color.Empty;
            Button11.BackColor = System.Drawing.Color.Empty;
            Button13.BackColor = System.Drawing.Color.Empty;
            Button14.BackColor = System.Drawing.Color.Empty;
            Button15.BackColor = System.Drawing.Color.Empty;
            Button12.BackColor = System.Drawing.Color.Empty;
            Button17.BackColor = System.Drawing.Color.Empty;
            s_machine_status = "";
            report_msg = "";
            //SetTime();

        }
        private void GetMachineList()
        {
            DataTable mydata = new DataTable();
            myconn.myopen();
            mydata = myconn.mysearch("select * from PLC_MachineList where DepID = '" + txtTram.Text + "'");


            txtMachine_no.DataTextField = "MachineId";
            txtMachine_no.DataValueField = "MachineId";
            txtMachine_no.DataSource = mydata;
            txtMachine_no.DataBind();
        }
        private void GetTramList()
        {
            DataTable mydata = new DataTable();
            myconn.myopen();
            mydata = myconn.mysearch("select distinct DepId from PLC_MachineList");


            txtTram.DataTextField = "DepId";
            txtTram.DataValueField = "DepId";
            txtTram.DataSource = mydata;
            txtTram.DataBind();
        }
        private void AddMaNv()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    txtMa_Nv1.Items.Add(i.ToString());
            //    txtMa_Nv6.Items.Add(i.ToString());
            //    txtMa_Nv3.Items.Add(i.ToString());
            //    txtMa_Nv4.Items.Add(i.ToString());
            //}
            txtMa_Nv0.Items.Add("VN");
            txtMa_Nv0.Items.Add("TV");
        }
        private void AddTime()
        {
            //add hours
            //for (int i = 0; i < 24; i++)
            //{
            //    if(i<10)
            //        txtHour.Items.Add("0" + i.ToString());
            //    else
            //        txtHour.Items.Add(i.ToString());
            //}

            ////add min
            //for (int i = 0; i < 60; i++)
            //{
            //    if (i < 10)
            //        txtMin.Items.Add("0" + i.ToString());
            //    else
            //        txtMin.Items.Add(i.ToString());
            //}
        }
        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }
        private void SetTime()
        {
            string strHours = DateTime.Now.Hour.ToString();
            string strMin = DateTime.Now.Minute.ToString();
            //string strHour_Min = "";

            //strHour_Min = strHour + ":" + strMin;
            //status_time_start.Text = strHour_Min;
            if (strHours.Length == 1)
                strHours = "0" + strHours;
            if (strMin.Length == 1)
                strMin = "0" + strMin;
            status_time_start.Text = strHours + ":" + strMin;
            //status_time_end.Text = strHours + ":" + strMin;
            report_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
           // report_dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");

        }
        private void SetTimeAll()
        {
            string strHours = DateTime.Now.Hour.ToString();
            string strMin = DateTime.Now.Minute.ToString();
            //string strHour_Min = "";

            //strHour_Min = strHour + ":" + strMin;
            //status_time_start.Text = strHour_Min;
            if (strHours.Length == 1)
                strHours = "0" + strHours;
            if (strMin.Length == 1)
                strMin = "0" + strMin;
            status_time_start.Text = strHours + ":" + strMin;
            status_time_end.Text = strHours + ":" + strMin;
            report_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            report_dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");

        }
        private void SetTMaNV()
        {
            string strMa_Vn = "";
            string strMa_So = txtMa_So.Text;
            if (txtMa_So.Text.Length < 4)
            {
                report_msg = "Mã số nhân viên nhập sai quy cách, Vui lòng nhập lại !!!";
                TxtMa_Nv.Text = "";
                return;
            }
            else
                report_msg = "";
            
            if(IsNumber(strMa_So) == false)
            {
                report_msg = "Mã số nhân viên nhập sai quy cách, Vui lòng nhập lại !!! ";
                TxtMa_Nv.Text = "";
                return;
            }
            else
                report_msg = "";

            strMa_Vn =txtMa_Nv0.Text + txtMa_So.Text;
            TxtMa_Nv.Text = strMa_Vn;

            DataTable mydata = new DataTable();
            myconn.myopen();
            //mydata = myconn.mysearch("select * from PLC_MachineList where DepID = '" + txtMa_Trama.Text + "'");
            mydata = myconn.mysearch("select * from PLC_Machine_Worker T1 INNER JOIN PLC_MachineList T2 ON T1.Machine_Code = T2.MachineId where Worker_Code = '" + TxtMa_Nv.Text + "'");
            myconn.myclose();
            if (mydata.Rows.Count > 0)
            {
                txtMachine_no.DataTextField = "Machine_Code";
                txtMachine_no.DataValueField = "Machine_Code";
                txtTram.DataTextField = "";
                txtTram.Enabled = false;
                txtMachine_no.DataSource = mydata;
                txtMachine_no.DataBind();               
            }
            else
            {
                txtTram.Enabled = true;
                GetTramList();
                GetMachineList();
            
            }
                            
            //SetTime();
        }
        protected void txtMa_So_TextChanged(object sender, EventArgs e)
        {
            SetTMaNV();
        }
        #endregion


    }
}