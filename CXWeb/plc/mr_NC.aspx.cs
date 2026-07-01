using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;

namespace CXWeb.plc
{
    public partial class mr_NC : System.Web.UI.Page
    {
        connectDB myconn = new connectDB();
        protected static int max_num = 30;        
        protected static string[] time = new string[24];
        protected static string[] machine_code = new string[max_num];
        protected static string[] PN = new string[max_num];
        protected static string[] Process = new string[max_num];
        protected static string[] code_row_machine = new string[max_num];
        protected static string area_code= "";
        protected static string head_title = "";
        protected static string select_machine_sql = "";
        protected static string[] sw = new string[2];

        private static DataTable dataByTime_all = new DataTable();
        private static DataTable dataPN_all = new DataTable();
        private static DataTable dataMa_St_all = new DataTable();
        private static DataTable dataWorkTime = new DataTable();
        private static DataTable dataMess = new DataTable();
        private static DataTable dataLog = new DataTable();

        protected static DateTime dateNow;
        protected void Page_Load(object sender, EventArgs e)
        {
            //dateNow = new DateTime(2020, 9, 27, 21, 0, 0);
            dateNow = DateTime.Now;
            this.date.Text = dateNow.ToString("dd/MM/yyyy");
            if (dateNow.Hour >= 6 && dateNow.Hour < 18)
            {
                shift.Text = "CA1";
            }
            else
                shift.Text = "CA2";
            

            if (area.SelectedValue.Trim() != "")
            {
                area_code = area.SelectedValue;
            }
            else
            {
                //if (area_code == "CT1") area_code = "CT2";
                //else if (area_code == "CT2") area_code = "CT2_LH";
                //else if (area_code == "CT2_LH") area_code = "CT1";

                //if (harea_code.Value == null || harea_code.Value.Trim() == "" || harea_code.Value.Trim() == "CT2_LH")
                //{
                //    area_code = "CT1";
                //    harea_code.Value = "CT1";
                //}
                //else if(harea_code.Value.Trim() == "CT1")
                //{
                //    area_code = "CT2";
                //    harea_code.Value = "CT2";
                //}
                //else if(harea_code.Value.Trim() == "CT2")
                //{
                //    area_code = "CT2_LH";
                //    harea_code.Value = "CT2_LH";
                //}

                if (dateNow.Minute % 11 == 10) area_code = "NC";
                else if (dateNow.Minute % 11 == 11) area_code = "NC1";
            }

            if (area_code == "NC") head_title = "PLC數據統計分析_NC加工區之一";
            else if (area_code == "NC1") head_title = "PLC數據統計分析_NC加工區之二";

            int no = 0;
            myconn.myopen();
            string sql = "SELECT * FROM PLC_MachineList WHERE DepId='" + area_code +"' AND MStatus='Show' ORDER BY Num";
            DataTable dt = myconn.mysearch(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                machine_code[i] = dt.Rows[i]["MachineId"].ToString();
                no++;
            }
            myconn.myclose();

            //int no = 0;
            //if (area_code == "CT1")
            //{
            //    machine_code[no] = "DN-H001"; no += 1; //20200213新增機台
            //    machine_code[no] = "DN-H004"; no += 1;
            //    //machine_code[no] = "DN-H005"; no += 1;
            //    machine_code[no] = "DN-H008"; no += 1;
            //    machine_code[no] = "DN-H011"; no += 1;
            //    machine_code[no] = "DN-H012"; no += 1;
            //    machine_code[no] = "DN-H014"; no += 1;
            //    machine_code[no] = "DN-H015"; no += 1;
            //    machine_code[no] = "DN-H016"; no += 1;
            //    machine_code[no] = "DN-H017"; no += 1;
            //    machine_code[no] = "DN-H200"; no += 1;
            //    machine_code[no] = "DN-H330"; no += 1;
            //    machine_code[no] = "DN-H340"; no += 1;
            //    machine_code[no] = "DN-H350"; no += 1;
            //    machine_code[no] = "NF-0001"; no += 1;
            //    machine_code[no] = "NF-0002"; no += 1;
            //    machine_code[no] = "DC-H200"; no += 1;
            //    machine_code[no] = "DN-H201"; no += 1;

            //}
            //else if (area_code == "CT2")
            //{
            //    //machine_code[no] = "KT-0414"; no += 1;
            //    machine_code[no] = "KT-0416"; no += 1;
            //    machine_code[no] = "KT-0650010"; no += 1;
            //    //machine_code[no] = "TD-0103"; no += 1;
            //    machine_code[no] = "TD-0253"; no += 1;
            //    machine_code[no] = "TD-0254"; no += 1;
            //    machine_code[no] = "TD-0255"; no += 1;
            //    //machine_code[no] = "TD-0305"; no += 1;
            //    //machine_code[no] = "TD-0306"; no += 1;
            //    //machine_code[no] = "TD-0307"; no += 1;
            //    machine_code[no] = "TD-0401"; no += 1;
            //    machine_code[no] = "TD-0402"; no += 1;
            //    //machine_code[no] = "TD-0403"; no += 1;
            //    machine_code[no] = "TD-0415"; no += 1;
            //    //machine_code[no] = "TD-0650"; no += 1;
            //    machine_code[no] = "TD-0650012"; no += 1;
            //    machine_code[no] = "TD-0651"; no += 1;

            //}
            //else if (area_code == "CT21")
            //{
            //    machine_code[no] = "TD-0652"; no += 1;
            //    machine_code[no] = "TD-0653"; no += 1;
            //    machine_code[no] = "TD-0654"; no += 1;
            //    machine_code[no] = "TD-0655"; no += 1;
            //    machine_code[no] = "TD-0659"; no += 1;
            //    machine_code[no] = "TD-1001"; no += 1;
            //    machine_code[no] = "TD-1002"; no += 1;
            //    machine_code[no] = "TD-1003"; no += 1;
            //    machine_code[no] = "TD-1004"; no += 1;
            //    machine_code[no] = "TD-2000"; no += 1;
            //    machine_code[no] = "KT-0650013"; no += 1;
            //    //machine_code[no] = "TN-0201"; no += 1;
            //    machine_code[no] = "TD-0250"; no += 1;
            //    machine_code[no] = "TD-0251"; no += 1;
            //    machine_code[no] = "TD-0252"; no += 1;
            //    //machine_code[no] = "TN-0302"; no += 1;
            //    //machine_code[no] = "TN-0600"; no += 1;

            //}
            //else if (area_code == "CT2_LH")
            //{
            //    machine_code[no] = "TD-0405"; no += 1;
            //    machine_code[no] = "TD-0406"; no += 1;
            //    machine_code[no] = "TD-0409"; no += 1;
            //    machine_code[no] = "TD-0410"; no += 1;
            //    machine_code[no] = "KT-0413"; no += 1;
            //    machine_code[no] = "KT-0650011"; no += 1;
            //    machine_code[no] = "KT-0657"; no += 1;
            //    machine_code[no] = "KT-0800"; no += 1;
            //    machine_code[no] = "TD-0407"; no += 1;
            //    machine_code[no] = "TD-0408"; no += 1;
            //    machine_code[no] = "TD-0411"; no += 1;
            //    machine_code[no] = "TD-0412"; no += 1;
            //    machine_code[no] = "TD-0630"; no += 1;
            //    machine_code[no] = "TD-0656"; no += 1;
            //    machine_code[no] = "KT-0658"; no += 1;

            //}
            //else if (area_code == "CT3")
            //{
            //    machine_code[no] = "DD-0601"; no += 1;
            //    //machine_code[no] = "DD-0801"; no += 1;
            //    machine_code[no] = "DD-0802"; no += 1;
            //    machine_code[no] = "DD-1103"; no += 1;
            //    machine_code[no] = "DL-0352"; no += 1;
            //    //machine_code[no] = "DL-0451"; no += 1;
            //    machine_code[no] = "DL-0453"; no += 1;
            //    //machine_code[no] = "DL-0454"; no += 1;
            //    //machine_code[no] = "DL-0455"; no += 1;
            //    machine_code[no] = "DL-0601"; no += 1;
            //    machine_code[no] = "DL-0602"; no += 1;
            //    machine_code[no] = "DL-0603"; no += 1;
            //    machine_code[no] = "DL-0604"; no += 1;
            //    machine_code[no] = "DL-0606"; no += 1;
            //    machine_code[no] = "DL-0801"; no += 1;

            //}
            //else if (area_code == "CT31")
            //{
            //    machine_code[no] = "DL-0802"; no += 1;
            //    //machine_code[no] = "DL-1101"; no += 1;
            //    machine_code[no] = "DL-1102"; no += 1;
            //    machine_code[no] = "DL-1103"; no += 1;
            //    machine_code[no] = "DL-1104"; no += 1;
            //    machine_code[no] = "XP-0453"; no += 1;
            //    machine_code[no] = "XP-0454"; no += 1;
            //    machine_code[no] = "XP-0455"; no += 1;
            //    machine_code[no] = "XP-0601"; no += 1;
            //    machine_code[no] = "XP-0602"; no += 1;
            //    machine_code[no] = "XP-0603"; no += 1;
            //    //machine_code[no] = "XP-1102"; no += 1;
            //    machine_code[no] = "XP-1601"; no += 1;
            //    //machine_code[no] = "XP-2601"; no += 1;

            //}
            //else if (area_code == "CNC1")
            //{
            //    machine_code[no] = "CN-C017"; no += 1;
            //    machine_code[no] = "CN-C018"; no += 1;
            //    machine_code[no] = "CN-C019"; no += 1;
            //    machine_code[no] = "CN-C020"; no += 1;
            //    //machine_code[no] = "CN-C021"; no += 1;
            //    machine_code[no] = "CN-C022"; no += 1;    // Đang sửa chữa
            //    machine_code[no] = "CN-C035"; no += 1;
            //    machine_code[no] = "CN-C036"; no += 1;
            //    machine_code[no] = "CN-C037"; no += 1;
            //    machine_code[no] = "CN-C038"; no += 1;
            //    machine_code[no] = "CN-C039"; no += 1;    // Đang sửa chữa
            //    machine_code[no] = "CN-C040"; no += 1;    // Đang sữa chữa
            //    //machine_code[no] = "CN-C041"; no += 1;
            //    machine_code[no] = "CN-C042"; no += 1;
            //    machine_code[no] = "CN-C043"; no += 1;
            //    machine_code[no] = "CN-C044"; no += 1;
            //    //machine_code[no] = "CNC-045"; no += 1;
            //    machine_code[no] = "CN-C046"; no += 1;
            //    machine_code[no] = "CN-C047"; no += 1;
            //    machine_code[no] = "CN-C048"; no += 1;
            //    machine_code[no] = "CN-C049"; no += 1;
            //    machine_code[no] = "CN-C050"; no += 1;
            //    machine_code[no] = "CN-C051"; no += 1;
            //    machine_code[no] = "CN-C052"; no += 1;
            //    machine_code[no] = "CN-C053"; no += 1;

            //}
            //else if (area_code == "CNC11")
            //{
            //    machine_code[no] = "CN-C054"; no += 1;
            //    //machine_code[no] = "CNC-055"; no += 1;
            //    machine_code[no] = "CN-C061"; no += 1;
            //    machine_code[no] = "CN-C062"; no += 1;
            //    machine_code[no] = "CN-C067"; no += 1;
            //    machine_code[no] = "CN-C068"; no += 1;
            //    machine_code[no] = "CN-C069"; no += 1;
            //    machine_code[no] = "CN-C070"; no += 1;
            //    machine_code[no] = "CN-C071"; no += 1;
            //    machine_code[no] = "CN-C072"; no += 1;
            //    machine_code[no] = "CN-C073"; no += 1;
            //    machine_code[no] = "CN-C074"; no += 1;
            //    machine_code[no] = "CN-C075"; no += 1;
            //    machine_code[no] = "CN-C076"; no += 1;
            //    machine_code[no] = "CN-C077"; no += 1;
            //    machine_code[no] = "CN-C078"; no += 1;
            //    machine_code[no] = "CN-C079"; no += 1;
            //    machine_code[no] = "CN-C080"; no += 1;
            //    machine_code[no] = "CN-C081"; no += 1;
            //    machine_code[no] = "CN-C082"; no += 1;
            //    machine_code[no] = "CN-C083"; no += 1;
            //    machine_code[no] = "CN-C084"; no += 1;
            //    machine_code[no] = "CN-C085"; no += 1;
            //    machine_code[no] = "CN-C086"; no += 1;
            //    machine_code[no] = "CN-C087"; no += 1;
            //}

            //else if (area_code == "CNC2")
            //{

            //    machine_code[no] = "CN-C009"; no += 1;
            //    //machine_code[no] = "CN-C011"; no += 1;
            //    machine_code[no] = "CN-C023"; no += 1;
            //    machine_code[no] = "CN-C024"; no += 1;
            //    machine_code[no] = "CN-C025"; no += 1;
            //    machine_code[no] = "CN-C026"; no += 1;
            //    machine_code[no] = "CN-C027"; no += 1;
            //    machine_code[no] = "CN-C056"; no += 1;
            //    machine_code[no] = "CN-C057"; no += 1;
            //    machine_code[no] = "CN-C058"; no += 1;
            //    machine_code[no] = "CN-C059"; no += 1;
            //    machine_code[no] = "CN-C060"; no += 1;
            //    machine_code[no] = "CN-C063"; no += 1;
            //    machine_code[no] = "CN-C064"; no += 1;
            //    machine_code[no] = "CN-C065"; no += 1;
            //    //machine_code[no] = "CN-C066"; no += 1;
            //    machine_code[no] = "CN-C088"; no += 1;

            //}
            //else if (area_code == "CNC21")
            //{
            //    machine_code[no] = "CN-C089"; no += 1;
            //    machine_code[no] = "CN-C090"; no += 1;
            //    machine_code[no] = "CN-C091"; no += 1;
            //    machine_code[no] = "CN-C092"; no += 1;
            //    machine_code[no] = "CN-C093"; no += 1;
            //    machine_code[no] = "CN-C094"; no += 1;
            //    machine_code[no] = "CN-C095"; no += 1;
            //    machine_code[no] = "CN-C096"; no += 1;
            //    machine_code[no] = "CN-C097"; no += 1;
            //    machine_code[no] = "CN-C098"; no += 1;
            //    machine_code[no] = "CN-C099"; no += 1;
            //    machine_code[no] = "CN-C100"; no += 1;
            //    machine_code[no] = "CN-C101"; no += 1;
            //    machine_code[no] = "CN-C102"; no += 1;
            //    machine_code[no] = "CN-C103"; no += 1;
            //    //machine_code[no] = "CN-C104"; no += 1;
            //    machine_code[no] = "CN-C105"; no += 1;
            //    machine_code[no] = "CN-C106"; no += 1;
            //    machine_code[no] = "CN-C107"; no += 1;
            //}

            //else if (area_code == "NC")
            //{
            //    //machine_code[no] = "GC-NC0001"; no += 1;
            //    //machine_code[no] = "GC-NC0002"; no += 1;
            //    //machine_code[no] = "GC-NC0003"; no += 1;
            //    machine_code[no] = "GC-NC0004"; no += 1;
            //    machine_code[no] = "GC-NC0005"; no += 1;
            //    machine_code[no] = "GC-NC0006"; no += 1;
            //    machine_code[no] = "GC-NC0007"; no += 1;
            //    machine_code[no] = "GC-NC0008"; no += 1;
            //    machine_code[no] = "GC-NC0009"; no += 1;
            //    machine_code[no] = "GC-NC0010"; no += 1;
            //    machine_code[no] = "GC-NC0011"; no += 1;
            //    machine_code[no] = "GC-NC0012"; no += 1;
            //    machine_code[no] = "GC-NC0013"; no += 1;
            //    machine_code[no] = "GC-NC0014"; no += 1;
            //    machine_code[no] = "GC-NC0015"; no += 1;
            //    machine_code[no] = "GC-NC0016"; no += 1;
            //    machine_code[no] = "GC-NC0017"; no += 1;
            //    machine_code[no] = "GC-NC0018"; no += 1;
            //    machine_code[no] = "GC-NC0020"; no += 1;
            //    machine_code[no] = "GC-NC0021"; no += 1;
            //    machine_code[no] = "GC-NC0022"; no += 1;
            //    machine_code[no] = "GC-NC0023"; no += 1;
            //    machine_code[no] = "GC-NC0024"; no += 1;

            //}
            //else if (area_code == "NC1")
            //{

            //    machine_code[no] = "GC-NC0025"; no += 1;
            //    machine_code[no] = "GC-NC0026"; no += 1;
            //    machine_code[no] = "GC-NC0027"; no += 1;
            //    machine_code[no] = "GC-NC0028"; no += 1;
            //    machine_code[no] = "GC-NC0029"; no += 1;
            //    machine_code[no] = "GC-NC0030"; no += 1;
            //    machine_code[no] = "GC-NC0031"; no += 1;
            //    machine_code[no] = "GC-NC0032"; no += 1;
            //    //machine_code[no] = "GC-NC0033"; no += 1;
            //    machine_code[no] = "GC-NC0042"; no += 1;
            //    //machine_code[no] = "GC-NC0043"; no += 1;
            //    machine_code[no] = "GC-NC0044"; no += 1;
            //    machine_code[no] = "GC-NC0045"; no += 1;
            //    //machine_code[no] = "GC-NC0046"; no += 1;
            //    machine_code[no] = "GC-NC0047"; no += 1;
            //    machine_code[no] = "GC-NC0034"; no += 1;
            //    machine_code[no] = "GC-NC0035"; no += 1;
            //    machine_code[no] = "GC-NC0036"; no += 1;
            //    machine_code[no] = "GC-NC0037"; no += 1;
            //    machine_code[no] = "GC-NC0038"; no += 1;
            //    machine_code[no] = "GC-NC0039"; no += 1;
            //    machine_code[no] = "GC-NC0040"; no += 1;
            //    machine_code[no] = "GC-NC0041"; no += 1;

            //}
            //else if (area_code == "DE")
            //{
            //    machine_code[no] = "DL-0452"; no += 1;
            //    machine_code[no] = "TH-0251"; no += 1;
            //    machine_code[no] = "TH-0351"; no += 1;
            //    machine_code[no] = "TH-0451"; no += 1;
            //    machine_code[no] = "TH-0601"; no += 1;
            //    machine_code[no] = "TH-0602"; no += 1;
            //}
            //else if (area_code == "JA")
            //{
            //    machine_code[no] = "JA-0065001"; no += 1;
            //    machine_code[no] = "JA-0065002"; no += 1;
            //    machine_code[no] = "JA-0080001"; no += 1;
            //    machine_code[no] = "JA-0080002"; no += 1;
            //    machine_code[no] = "JA-0080003"; no += 1;
            //    machine_code[no] = "JA-0080004"; no += 1;
            //    machine_code[no] = "JA-0150001"; no += 1;
            //    machine_code[no] = "JA-0160001"; no += 1;
            //    machine_code[no] = "JA-0160002"; no += 1;
            //    machine_code[no] = "JA-0160003"; no += 1;
            //    machine_code[no] = "JA-0160004"; no += 1;
            //    machine_code[no] = "JA-0250001"; no += 1;
            //    machine_code[no] = "JA-0250002"; no += 1;
            //    machine_code[no] = "JA-0250003"; no += 1;
            //    machine_code[no] = "JA-0250004"; no += 1;
            //}

            for (int i = 0; i < max_num; i++)
                code_row_machine[i] = " hidden = 'true' ";
            for (int i = no; i < max_num; i++)
                machine_code[i] = "";

            select_machine_sql = "'" + machine_code[0] + "'";
            for (int i = 0; i < no; i++)
            {
                code_row_machine[i] = "";
                select_machine_sql = select_machine_sql + "," + "'" + machine_code[i] + "'";
            }

            if (!base.IsPostBack)
            {    
                query();
            }
        }
        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    Response.Redirect("/plc/mr.aspx");
        //}
        protected void btnXem_Click(object sender, EventArgs e)
        {

            query();

        }
        private void query()
        {
            DateTime text1;
            DateTime text2;
            try
            {
                text1 = new DateTime(Convert.ToInt32(date.Text.Substring(6, 4)), Convert.ToInt32(date.Text.Substring(3, 2)), Convert.ToInt32(date.Text.Substring(0, 2)), 0, 0, 0);
                text2 = new DateTime(Convert.ToInt32(date.Text.Substring(6, 4)), Convert.ToInt32(date.Text.Substring(3, 2)), Convert.ToInt32(date.Text.Substring(0, 2)), 0, 0, 0);
            }
            catch
            {
                this.date.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                text1 = new DateTime(Convert.ToInt32(date.Text.Substring(6, 4)), Convert.ToInt32(date.Text.Substring(3, 2)), Convert.ToInt32(date.Text.Substring(0, 2)), 0, 0, 0);
                text2 = new DateTime(Convert.ToInt32(date.Text.Substring(6, 4)), Convert.ToInt32(date.Text.Substring(3, 2)), Convert.ToInt32(date.Text.Substring(0, 2)), 0, 0, 0);
            }


           
            if (shift.Text == "CA2")
            {
                //text1 = text1.AddHours(18);
                text1 = text1.AddHours(6);//換成抓24個小時
                text2 = text2.AddHours(30);
                //text2 = text2.AddMinutes(-1);//為了減少資料，只抓偶數分鐘
                text2 = text2.AddMinutes(-2);

                time[0] = "06"; time[1] = "07"; time[2] = "08"; time[3] = "09"; time[4] = "10"; time[5] = "11";
                time[6] = "12"; time[7] = "13"; time[8] = "14"; time[9] = "15"; time[10] = "16"; time[11] = "17";
                time[12] = "18"; time[13] = "19"; time[14] = "20"; time[15] = "21"; time[16] = "22"; time[17] = "23";
                time[18] = "00"; time[19] = "01"; time[20] = "02"; time[21] = "03"; time[22] = "04"; time[23] = "05";            }
            else
            {
                shift.Text = "CA1";

                //text1 = text1.AddHours(6);
                text1 = text1.AddHours(-6);//換成抓24個小時
                text2 = text2.AddHours(18);
                //text2 = text2.AddMinutes(-1);//為了減少資料，只抓偶數分鐘
                text2 = text2.AddMinutes(-2);

                time[0] = "18"; time[1] = "19"; time[2] = "20"; time[3] = "21"; time[4] = "22"; time[5] = "23";
                time[6] = "00"; time[7] = "01"; time[8] = "02"; time[9] = "03"; time[10] = "04"; time[11] = "05";
                time[12] = "06"; time[13] = "07"; time[14] = "08"; time[15] = "09"; time[16] = "10"; time[17] = "11";
                time[18] = "12"; time[19] = "13"; time[20] = "14"; time[21] = "15"; time[22] = "16"; time[23] = "17";
            }

            string sBegin = text1.ToString("yyyy-MM-dd HH:mm");
            string sMiddle = text1.AddHours(12).ToString("yyyy-MM-dd HH:mm");
            string sEnd = text2.ToString("yyyy-MM-dd HH:mm");

            GetData_all(text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));

            this.Repeater1.DataSource = this.GetData(0, machine_code[0], sBegin, sEnd, sMiddle);
            this.Repeater1.DataBind();
            this.Repeater2.DataSource = this.GetData(1, machine_code[1], sBegin, sEnd, sMiddle);
            this.Repeater2.DataBind();
            this.Repeater3.DataSource = this.GetData(2, machine_code[2], sBegin, sEnd, sMiddle);
            this.Repeater3.DataBind();
            this.Repeater4.DataSource = this.GetData(3, machine_code[3], sBegin, sEnd, sMiddle);
            this.Repeater4.DataBind();
            this.Repeater5.DataSource = this.GetData(4, machine_code[4], sBegin, sEnd, sMiddle);
            this.Repeater5.DataBind();
            this.Repeater6.DataSource = this.GetData(5, machine_code[5], sBegin, sEnd, sMiddle);
            this.Repeater6.DataBind();
            this.Repeater7.DataSource = this.GetData(6, machine_code[6], sBegin, sEnd, sMiddle);
            this.Repeater7.DataBind();
            this.Repeater8.DataSource = this.GetData(7, machine_code[7], sBegin, sEnd, sMiddle);
            this.Repeater8.DataBind();
            this.Repeater9.DataSource = this.GetData(8, machine_code[8], sBegin, sEnd, sMiddle);
            this.Repeater9.DataBind();
            this.Repeater10.DataSource = this.GetData(9, machine_code[9], sBegin, sEnd, sMiddle);
            this.Repeater10.DataBind();
            this.Repeater11.DataSource = this.GetData(10, machine_code[10], sBegin, sEnd, sMiddle);
            this.Repeater11.DataBind();
            this.Repeater12.DataSource = this.GetData(11, machine_code[11], sBegin, sEnd, sMiddle);
            this.Repeater12.DataBind();
            this.Repeater13.DataSource = this.GetData(12, machine_code[12], sBegin, sEnd, sMiddle);
            this.Repeater13.DataBind();
            this.Repeater14.DataSource = this.GetData(13, machine_code[13], sBegin, sEnd, sMiddle);
            this.Repeater14.DataBind();
            this.Repeater15.DataSource = this.GetData(14, machine_code[14], sBegin, sEnd, sMiddle);
            this.Repeater15.DataBind();
            this.Repeater16.DataSource = this.GetData(15, machine_code[15], sBegin, sEnd, sMiddle);
            this.Repeater16.DataBind();
            this.Repeater17.DataSource = this.GetData(16, machine_code[16], sBegin, sEnd, sMiddle);
            this.Repeater17.DataBind();
            this.Repeater18.DataSource = this.GetData(17, machine_code[17], sBegin, sEnd, sMiddle);
            this.Repeater18.DataBind();
            this.Repeater19.DataSource = this.GetData(18, machine_code[18], sBegin, sEnd, sMiddle);
            this.Repeater19.DataBind();
            this.Repeater20.DataSource = this.GetData(19, machine_code[19], sBegin, sEnd, sMiddle);
            this.Repeater20.DataBind();
            this.Repeater21.DataSource = this.GetData(20, machine_code[20], sBegin, sEnd, sMiddle);
            this.Repeater21.DataBind();
            this.Repeater22.DataSource = this.GetData(21, machine_code[21], sBegin, sEnd, sMiddle);
            this.Repeater22.DataBind();
            this.Repeater23.DataSource = this.GetData(22, machine_code[22], sBegin, sEnd, sMiddle);
            this.Repeater23.DataBind();
            this.Repeater24.DataSource = this.GetData(23, machine_code[23], sBegin, sEnd, sMiddle);
            this.Repeater24.DataBind();
            this.Repeater25.DataSource = this.GetData(24, machine_code[24], sBegin, sEnd, sMiddle);
            this.Repeater25.DataBind();
            //this.Repeater26.DataSource = this.GetData(25, machine_code[25], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater26.DataBind();
            //this.Repeater27.DataSource = this.GetData(26, machine_code[26], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater27.DataBind();
            //this.Repeater28.DataSource = this.GetData(27, machine_code[27], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater28.DataBind();
            //this.Repeater29.DataSource = this.GetData(28, machine_code[28], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater29.DataBind();
            //this.Repeater30.DataSource = this.GetData(29, machine_code[29], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater30.DataBind();
            //this.Repeater31.DataSource = this.GetData(30, machine_code[30], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater31.DataBind();
            //this.Repeater32.DataSource = this.GetData(31, machine_code[31], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater32.DataBind();
            //this.Repeater33.DataSource = this.GetData(32, machine_code[32], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater33.DataBind();
            //this.Repeater34.DataSource = this.GetData(33, machine_code[33], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater34.DataBind();
            //this.Repeater35.DataSource = this.GetData(34, machine_code[34], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater35.DataBind();
            //this.Repeater36.DataSource = this.GetData(35, machine_code[35], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater36.DataBind();
            //this.Repeater37.DataSource = this.GetData(36, machine_code[36], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater37.DataBind();
            //this.Repeater38.DataSource = this.GetData(37, machine_code[37], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater38.DataBind();
            //this.Repeater39.DataSource = this.GetData(38, machine_code[38], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater39.DataBind();
            //this.Repeater40.DataSource = this.GetData(39, machine_code[39], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater40.DataBind();
            //this.Repeater41.DataSource = this.GetData(40, machine_code[40], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater41.DataBind();
            //this.Repeater42.DataSource = this.GetData(41, machine_code[41], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater42.DataBind();
            //this.Repeater43.DataSource = this.GetData(42, machine_code[42], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater43.DataBind();
            //this.Repeater44.DataSource = this.GetData(43, machine_code[43], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater44.DataBind();
            //this.Repeater45.DataSource = this.GetData(44, machine_code[44], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater45.DataBind();
            //this.Repeater46.DataSource = this.GetData(45, machine_code[45], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater46.DataBind();
            //this.Repeater47.DataSource = this.GetData(46, machine_code[46], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater47.DataBind();
            //this.Repeater48.DataSource = this.GetData(47, machine_code[47], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater48.DataBind();
            //this.Repeater49.DataSource = this.GetData(48, machine_code[48], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater49.DataBind();
            //this.Repeater50.DataSource = this.GetData(49, machine_code[49], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater50.DataBind();
            //this.Repeater51.DataSource = this.GetData(50, machine_code[50], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater51.DataBind();
            //this.Repeater52.DataSource = this.GetData(51, machine_code[51], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater52.DataBind();
            //this.Repeater53.DataSource = this.GetData(52, machine_code[52], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater53.DataBind();
            //this.Repeater54.DataSource = this.GetData(53, machine_code[53], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater54.DataBind();
            //this.Repeater55.DataSource = this.GetData(54, machine_code[54], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater55.DataBind();
            //this.Repeater56.DataSource = this.GetData(55, machine_code[55], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater56.DataBind();
            //this.Repeater57.DataSource = this.GetData(56, machine_code[56], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater57.DataBind();
            //this.Repeater58.DataSource = this.GetData(57, machine_code[57], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater58.DataBind();
            //this.Repeater59.DataSource = this.GetData(58, machine_code[58], text1.ToString("yyyy-MM-dd HH:mm"), text2.ToString("yyyy-MM-dd HH:mm"));
            //this.Repeater59.DataBind();

            DataView view = dataMess.DefaultView;
            view.Sort = "NTime";
            DataTable dt = view.ToTable();
            this.Repeater26.DataSource = dt;
            this.Repeater26.DataBind();
        }

        private void GetData_all(string pDate1, string pDate2)
        {
            string strsql;

            dataByTime_all.TableName = "dataByTime";            
            dataPN_all.TableName = "dataPN";            
            dataMa_St_all.TableName = "dataMa_St";

            dataMess = new DataTable();
            dataMess.TableName = "dataMess";
            dataMess.Columns.Add("NTime");
            dataMess.Columns[0].DataType = typeof(DateTime);
            dataMess.Columns.Add("Message");

            myconn.myopen();

            strsql = "select a.*,a.[Count] ^ 66 as iCnt from PLC_VL a where a.MACHINE_CODE in ("+ select_machine_sql+") and a.TTime >= '" + pDate1 + "' and a.TTime<= '" + pDate2 + "' and DATEPART(mi,[TTime]) %2=0 order by a.MACHINE_CODE,a.TTime asc";//為了減少資料，只抓偶數分鐘
            //strsql = "select a.*,a.[Count] ^ 66 as iCnt from PLC_VL a where a.MACHINE_CODE = '" + pMaMay + "' and a.TTime >= '" + pDate1 + "' and a.TTime<= '2018-11-23 10:59' order by a.TTime asc";
            dataByTime_all = myconn.mysearch(strsql);

            strsql = "select a.Item,a.Process,a.MACHINECODE from tc_5836_chkin_status a where a.MACHINECODE in ("+ select_machine_sql + ") order by a.MACHINECODE";
            dataPN_all = myconn.mysearch(strsql);

            strsql = "SELECT SUBSTRING(CONVERT(nvarchar,dStTimeStart,120),12,5) as dStTimeStart,dStatus,cDesc,dMachine FROM MACHINE_STATUS t left outer join MACHINE_STATUS_CODE on dStatus = cCode where  dMachine  in ("+ select_machine_sql + ") and dStTimeStart >= '" + pDate1 + "' and  dStTimeStart = (select max(dStTimeStart) dStTimeStart from MACHINE_STATUS where dmachine = t.dmachine) order by dMachine,dStTimeStart desc ";
            dataMa_St_all = myconn.mysearch(strsql);

            strsql = "SELECT pv.MACHINE_CODE\n"
                + "       ,MAX(CASE WHEN (pv.[State] IN (1,2) AND pv.MACHINE_CODE LIKE 'CN-C%') OR (pv.[State]=1 AND pv.MACHINE_CODE NOT LIKE 'CN-C%') THEN TTime END) LastWork\n"
                + "       ,MAX(CASE WHEN (pv.[State]=3 AND pv.MACHINE_CODE LIKE 'CN-C%') OR (pv.[State] IN (2,3) AND pv.MACHINE_CODE NOT LIKE 'CN-C%') THEN TTime END) LastNoWork\n"
                + "       ,MIN(CASE WHEN (pv.[State]=3 AND pv.MACHINE_CODE LIKE 'CN-C%') OR (pv.[State] IN (2,3) AND pv.MACHINE_CODE NOT LIKE 'CN-C%') THEN TTime END) FirstNoWork\n"
                + "FROM PLC_VL pv\n"
                + "WHERE pv.TTime >= '" + pDate1 + "' AND pv.TTime <= '" + pDate2 + "'\n"
                + "AND pv.MACHINE_CODE IN (" + select_machine_sql + ")\n"
                + "GROUP BY pv.MACHINE_CODE";
            dataWorkTime = myconn.mysearch(strsql);

            sw[0] = "";
            sw[1] = "";
            strsql = @"SELECT DepId,MAX(CASE WHEN NLevel=1 THEN GroupName END) group1,MAX(CASE WHEN NLevel=2 THEN GroupName END) group2
                        FROM PLC_ApiGroup 
                        WHERE DepId='" + area_code + @"'
                        GROUP BY DepId";
            DataTable dt = myconn.mysearch(strsql);
            if (dt.Rows.Count > 0)
            {
                sw[0] = dt.Rows[0]["group1"].ToString();
                sw[1] = dt.Rows[0]["group2"].ToString();
            }

            strsql = "SELECT MachineId, ApiType, NLevel, MAX(SendDate) SendDate FROM PLC_SendLog GROUP BY MachineId, ApiType, NLevel";
            dataLog = myconn.mysearch(strsql);
            myconn.myclose();
        }
        private DataTable GetData(int flag, string pMaMay, string pDate1, string pDate2, string pMiddle)
        {
            string strsql;
            string lastColour = "none";

            DataTable dataByTime = new DataTable();
            dataByTime.TableName = "dataByTime";
            DataTable dataPN = new DataTable();
            dataPN.TableName = "dataPN";
            DataTable dataMa_St = new DataTable();
            dataMa_St.TableName = "dataMa_St";

            DataTable dataReport = new DataTable();
            dataReport.TableName = "dataReport";
            dataReport.Columns.Add("TTime");
            dataReport.Columns.Add("color");
            dataReport.Columns.Add("MACHINE_CODE");
            dataReport.Columns.Add("code");

            //myconn.myopen();

            //strsql = "select a.*,a.[Count] ^ 66 as iCnt from PLC_VL a where a.MACHINE_CODE = '" + pMaMay + "' and a.TTime >= '" + pDate1 + "' and a.TTime<= '" + pDate2 + "' order by a.TTime asc";
            ////strsql = "select a.*,a.[Count] ^ 66 as iCnt from PLC_VL a where a.MACHINE_CODE = '" + pMaMay + "' and a.TTime >= '" + pDate1 + "' and a.TTime<= '2018-11-23 10:59' order by a.TTime asc";
            //dataByTime = myconn.mysearch(strsql);

            //strsql = "select a.Item,a.Process from tc_5836_chkin_status a where a.MACHINECODE = '" + pMaMay + "'";
            //dataPN = myconn.mysearch(strsql);

            //strsql = "SELECT TOP (1)  SUBSTRING(CONVERT(nvarchar,dStTimeStart,120),12,5) as dStTimeStart,dStatus,cDesc FROM MACHINE_STATUS  left outer join MACHINE_STATUS_CODE on dStatus = cCode where dMachine = '" + pMaMay + "'and dStTimeStart >= '" + pDate1 + "'  order by dStTimeStart desc ";
            //dataMa_St = myconn.mysearch(strsql);

            dataByTime = dataByTime_all.Clone();
            foreach (DataRow dr_dataByTime_all in dataByTime_all.Rows)
            {
                if (dr_dataByTime_all["MACHINE_CODE"].ToString() == pMaMay)
                {
                    dataByTime.ImportRow(dr_dataByTime_all);
                }
            }

            dataPN = dataPN_all.Clone();
            foreach (DataRow dr_dataPN_all in dataPN_all.Rows)
            {
                if (dr_dataPN_all["MACHINECODE"].ToString() == pMaMay)
                {
                    dataPN.ImportRow(dr_dataPN_all);
                }
            }

            dataMa_St = dataMa_St_all.Clone();
            foreach (DataRow dr_dataMa_St_all in dataMa_St_all.Rows)
            {
                if (dr_dataMa_St_all["dMachine"].ToString() == pMaMay)
                {
                    dataMa_St.ImportRow(dr_dataMa_St_all);
                }
            }


            //myconn.myclose();    
            dataByTime.Columns.Add("color");

            PN[flag] = dataPN.Rows.Count != 0 ? dataPN.Rows[0]["Item"].ToString() : "";
            Process[flag] = dataPN.Rows.Count != 0 ? dataPN.Rows[0]["Process"].ToString() : "";
            //if (dataPN.Rows.Count != 0)
            //{
            //    PN[flag] = dataPN.Rows[0]["Item"].ToString() ;
            //    Process[flag]= dataPN.Rows[0]["Process"].ToString();
            //}

            string[] array = new string[]
            {
                "none",
                "green",
                "yellow",
                "red"
            };
            foreach (DataRow dataRow in dataByTime.Rows)
            {
                //if (area_code.Contains("CNC"))
                //    dataRow["color"] = Convert.ToInt32(dataRow["State"]) == 2 ? array[1] : array[Convert.ToInt32(dataRow["State"])];
                //else
                //    dataRow["color"] = array[Convert.ToInt32(dataRow["State"])];
                dataRow["color"] = array[Convert.ToInt32(dataRow["State"])];

            }
            if (dataByTime.Rows.Count != 0)
                lastColour = dataByTime.Rows[dataByTime.Rows.Count - 1]["color"].ToString();
            DataRow rowBegin = dataByTime.NewRow();
            rowBegin["TTime"] = pDate1;

            if (dataByTime.Rows.Count == 0 || (dataByTime.Rows[0]["TTime"].ToString().Substring(11, 5) != "06:00"
                && dataByTime.Rows[0]["TTime"].ToString().Substring(11, 5) != "18:00"))
            {
                dataByTime.Rows.InsertAt(rowBegin, 0);
            }

            //if (dataByTime.Rows[dataByTime.Rows.Count - 1]["TTime"].ToString().Substring(11, 5) != "05:59"
            //    && dataByTime.Rows[dataByTime.Rows.Count - 1]["TTime"].ToString().Substring(11, 5) != "17:59")
            if (dataByTime.Rows[dataByTime.Rows.Count - 1]["TTime"].ToString().Substring(11, 5) != "05:58"
                && dataByTime.Rows[dataByTime.Rows.Count - 1]["TTime"].ToString().Substring(11, 5) != "17:58")

            {
                dataByTime.Rows.Add();
                dataByTime.Rows[dataByTime.Rows.Count - 1]["TTime"] = pDate2;
            }

            dataReport.ImportRow(dataByTime.Rows[0]);
            dataReport.Rows[dataReport.Rows.Count - 1]["MACHINE_CODE"] = "";
            dataReport.Rows[dataReport.Rows.Count - 1]["code"] = " class = '" + dataReport.Rows[dataReport.Rows.Count - 1]["color"].ToString() + "' ";
            for (int i = 1; i < dataByTime.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dataByTime.Rows[i]["TTime"].ToString()).ToString("HH:mm") != Convert.ToDateTime(dataByTime.Rows[i - 1]["TTime"].ToString()).ToString("HH:mm"))// xử lý tình huống bị trùng phút
                {
                    // while (Convert.ToDateTime(dataByTime.Rows[i]["TTime"].ToString()).Minute != Convert.ToDateTime(dataReport.Rows[dataReport.Rows.Count - 1]["TTime"].ToString()).AddMinutes(1).Minute
                    //     || Convert.ToDateTime(dataByTime.Rows[i]["TTime"].ToString()).Hour != (Convert.ToDateTime(dataReport.Rows[dataReport.Rows.Count - 1]["TTime"].ToString()).AddMinutes(1).Minute != 0 ? Convert.ToDateTime(dataReport.Rows[dataReport.Rows.Count - 1]["TTime"].ToString()).Hour : Convert.ToDateTime(dataReport.Rows[dataReport.Rows.Count - 1]["TTime"].ToString()).AddHours(1).Hour))
                    while (Convert.ToDateTime(dataByTime.Rows[i]["TTime"].ToString()).ToString("HH:mm") != Convert.ToDateTime(dataReport.Rows[dataReport.Rows.Count - 1]["TTime"].ToString()).AddMinutes(2).ToString("HH:mm"))
                    {
                        dataReport.Rows.Add();
                        dataReport.Rows[dataReport.Rows.Count - 1]["TTime"] = Convert.ToDateTime(dataReport.Rows[dataReport.Rows.Count - 2]["TTime"].ToString()).AddMinutes(2);
                        dataReport.Rows[dataReport.Rows.Count - 1]["code"] = " class = 'none'";
                    }
                    dataReport.ImportRow(dataByTime.Rows[i]);
                    dataReport.Rows[dataReport.Rows.Count - 1]["MACHINE_CODE"] = "";
                    dataReport.Rows[dataReport.Rows.Count - 1]["code"] = " class = '" + dataReport.Rows[dataReport.Rows.Count - 1]["color"].ToString() + "' ";
                }
            }

            for (int i = 0; i < dataReport.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dataReport.Rows[i]["TTime"].ToString()).Minute == 0)
                {
                    dataReport.Rows[i]["code"] = " class = 'black' ttime='" + Convert.ToDateTime(dataReport.Rows[i]["TTime"].ToString()).ToString("yyyy-MM-dd HH:mm") + "'";
                }
            }

            DataRow rowMiddle = dataReport.AsEnumerable().FirstOrDefault(r => Convert.ToDateTime(r["TTime"].ToString()).ToString("yyyy-MM-dd HH:mm") == pMiddle);
            int index = dataReport.Rows.IndexOf(rowMiddle);
            DataRow cellMiddle = dataReport.NewRow();
            cellMiddle["MACHINE_CODE"] = perWork(dataByTime, pDate1, pMiddle).ToString("00.00");
            cellMiddle["code"] = " style='width: 50px; text-align: center'";
            dataReport.Rows.InsertAt(cellMiddle, index);
            DataRow Lborder = dataReport.NewRow();
            Lborder["code"] = " class='black'";
            dataReport.Rows.InsertAt(Lborder, index);

            dataReport.Rows[dataReport.Rows.Count - 1]["code"] = " class = 'black'";
            dataReport.Rows.Add("", "", perWork(dataByTime, pMiddle, pDate2).ToString("00.00"), " style='width: 50px; text-align: center;'");

            dataReport.Rows.Add("", "", "", " class = '" + lastColour + "'");
            dataReport.Rows.Add("", "", "", " class = '" + lastColour + "'");
            dataReport.Rows.Add("", "", "", " class = '" + lastColour + "'");
            dataReport.Rows.Add("", "", "", " class = '" + lastColour + "'");
            dataReport.Rows.Add("", "", "", " class = '" + lastColour + "'");
            dataReport.Rows.Add("", "", "", " class = '" + lastColour + "'");
            dataReport.Rows.Add("", "", "", " class = 'black'");

            dataReport.Rows.Add("", "", PN[flag], "style = 'width:70px; text-align: left; padding-left: 5px;'");
            dataReport.Rows.Add("", "", "", " class = 'black'");
            dataReport.Rows.Add("", "", Process[flag], "style = 'width:30px; text-align: left; padding-left: 3px;'");

            if (dataMa_St.Rows.Count != 0)
            {
                dataReport.Rows.Add("", "", "", " class = 'black'");
                dataReport.Rows.Add("", "", dataMa_St.Rows[0]["dStTimeStart"].ToString(), "style = 'width:50px; text-align: left; padding-left: 5px;'");
                dataReport.Rows.Add("", "", "", " class = 'black'");
                if (dataMa_St.Rows[0]["dStatus"].ToString() == "A01")
                {
                    dataReport.Rows.Add("", "", "", " class = 'green'");
                    dataReport.Rows.Add("", "", "", " class = 'green'");
                    dataReport.Rows.Add("", "", "", " class = 'green'");
                    dataReport.Rows.Add("", "", "", " class = 'green'");
                    dataReport.Rows.Add("", "", "", " class = 'green'");
                    dataReport.Rows.Add("", "", "", " class = 'green'");
                }
                else
                {
                    dataReport.Rows.Add("", "", "", " class = 'red'");
                    dataReport.Rows.Add("", "", "", " class = 'red'");
                    dataReport.Rows.Add("", "", "", " class = 'red'");
                    dataReport.Rows.Add("", "", "", " class = 'red'");
                    dataReport.Rows.Add("", "", "", " class = 'red'");
                    dataReport.Rows.Add("", "", "", " class = 'red'");
                }

                dataReport.Rows.Add("", "", dataMa_St.Rows[0]["cDesc"].ToString(), "style = 'width:240px; text-align: left; padding-left: 5px'");
            }
            else
            {
                dataReport.Rows.Add("", "", "", " class = 'black'");
                dataReport.Rows.Add("", "", " ", "style = 'width:50px; text-align: left; padding-left: 3px'");
                dataReport.Rows.Add("", "", "", " class = 'black'");
                dataReport.Rows.Add("", "", "", " class = 'none'");
                dataReport.Rows.Add("", "", "", " class = 'none'");
                dataReport.Rows.Add("", "", "", " class = 'none'");
                dataReport.Rows.Add("", "", "", " class = 'none'");
                dataReport.Rows.Add("", "", "", " class = 'none'");
                dataReport.Rows.Add("", "", "", " class = 'none'");
                dataReport.Rows.Add("", "", " ", "style = 'width:240px; text-align: left; padding-left: 5px'");
            }
            string NColor1 = "style='width: 100px; font-weight: normal;'";
            string NColor2 = "style='width: 100px; font-weight: normal;'";
            if (pMaMay != null && pMaMay != "")
            {
                var MColor = dataWorkTime.Select("MACHINE_CODE='" + pMaMay + "'");
                if (MColor.Length == 0)
                {
                    var valDate = dataLog.Select().FirstOrDefault(x => x["MachineId"].ToString() == pMaMay && x["ApiType"].ToString() == "Line"
                                                                        && Convert.ToInt32(x["NLevel"]) == 2);
                    DateTime sDate = valDate == null ? DateTime.Now : Convert.ToDateTime(valDate["SendDate"]);
                    NColor1 = "class='red' style='width: 100px; font-weight: normal;'";
                    NColor2 = "class='red' style='width: 100px; font-weight: normal;'";
                    string mess = sDate.ToString("HH:mm") + " 機台" + pMaMay + "機台已經停機60分鐘";
                    mess += " --- " + sDate.ToString("HH:mm") + " máy " + pMaMay + " đã dừng hoạt động quá 60 phút";
                    dataMess.Rows.Add(Convert.ToDateTime(pMiddle), mess);
                }
                else
                {
                    if (MColor[0].IsNull("LastWork") || ((dateNow - Convert.ToDateTime(MColor[0]["LastWork"].ToString())).TotalMinutes >= 60))
                    {
                        NColor1 = "class='red' style='width: 100px; font-weight: normal;'";
                        NColor2 = "class='red' style='width: 100px; font-weight: normal;'";
                        var valDate = dataLog.Select().FirstOrDefault(x => x["MachineId"].ToString() == pMaMay && x["ApiType"].ToString() == "Line"
                                                                        && Convert.ToInt32(x["NLevel"]) == 2);
                        DateTime sDate = valDate == null ? DateTime.Now : Convert.ToDateTime(valDate["SendDate"]);
                        if (MColor[0].IsNull("LastWork"))
                        {
                            string mess = sDate.ToString("HH:mm") + " 機台" + pMaMay + "機台已經停機60分鐘";
                            mess += " --- " + sDate.ToString("HH:mm") + " máy " + pMaMay + " đã dừng hoạt động quá 60 phút";
                            dataMess.Rows.Add(Convert.ToDateTime(pMiddle), mess);
                        }
                        else
                        {
                            string mess = sDate.ToString("HH:mm") + " 機台" + pMaMay + "機台已經停機60分鐘";
                            mess += " --- " + sDate.ToString("HH:mm") + " máy " + pMaMay + " đã dừng hoạt động quá 60 phút";
                            dataMess.Rows.Add(Convert.ToDateTime(MColor[0]["LastWork"].ToString()), mess);
                        }
                    }
                    else if ((dateNow - Convert.ToDateTime(MColor[0]["LastWork"].ToString())).TotalMinutes >= 30)
                    {
                        var valDate = dataLog.Select().FirstOrDefault(x => x["MachineId"].ToString() == pMaMay && x["ApiType"].ToString() == "Line"
                                                                        && Convert.ToInt32(x["NLevel"]) == 1);
                        DateTime sDate = valDate == null ? DateTime.Now : Convert.ToDateTime(valDate["SendDate"]);
                        NColor1 = "class='yellow' style='width: 100px; font-weight: normal;'";
                        string mess = sDate.ToString("HH:mm") + " 機台" + pMaMay + "機台已經停機30分鐘";
                        mess += " --- " + sDate.ToString("HH:mm") + " máy " + pMaMay + " đã dừng hoạt động quá 30 phút";
                        dataMess.Rows.Add(Convert.ToDateTime(MColor[0]["LastWork"].ToString()), mess);
                    }
                }
            }
            
            dataReport.Rows.Add("", "", "", " class = 'black'");
            dataReport.Rows.Add("", "", sw[0], NColor1);
            dataReport.Rows.Add("", "", "", " class = 'black'");
            dataReport.Rows.Add("", "", sw[1], NColor2);
            dataReport.Rows.Add("", "", "", " class = 'black'");

            return dataReport;
        }

        protected void area_SelectedIndexChanged(object sender, EventArgs e)
        {
            query();
        }

        protected double perWork(DataTable data, string sBegin, string sEnd)
        {
            var dataList = data.AsEnumerable().Where(x => Convert.ToDateTime(x["TTime"]) >= Convert.ToDateTime(sBegin)
                                                        && Convert.ToDateTime(x["TTime"]) < Convert.ToDateTime(sEnd));
            int iCountGreen = 0;
            if (data.Rows[1]["MACHINE_CODE"].ToString().Contains("CN-C"))
                iCountGreen = dataList.Where(x => x["State"].ToString() == "1" || x["State"].ToString() == "2").Count();
            else
                iCountGreen = dataList.Where(x => x["State"].ToString() == "1").Count();
            return iCountGreen != 0 ? ((iCountGreen * 100.00) / 359) : 0.00;
        }
    }
}