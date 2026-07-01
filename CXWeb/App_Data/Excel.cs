using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.Streaming;
//using Telerik.Web.UI.GridExcelBuilder;

namespace CXWeb
{
    public class Excel
    {
        public static byte[] RerportMachine(DataTable pData, string pName, string pDate)
        {
            XSSFWorkbook xSSFWorkbook;
            using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report1.xlsx"), FileMode.Open, FileAccess.Read))
            {
                xSSFWorkbook = new XSSFWorkbook(fileStream);
            }
            ISheet sheet = xSSFWorkbook.GetSheet("Sheet1");
            ICellStyle cellStyle = xSSFWorkbook.CreateCellStyle();
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = HorizontalAlignment.Center;
            sheet.GetRow(6).GetCell(0).SetCellValue(pDate);
            string[] array = new string[]
            {
                "TIME",
                "PRODUCT/MINUTE"
            };
            IRow row = sheet.CreateRow(8);
            for (int i = 0; i < array.Length; i++)
            {
                ICell expr_C3 = row.CreateCell(i + 2);
                expr_C3.SetCellValue(array[i]);
                expr_C3.CellStyle = cellStyle;
            }
            sheet.GetRow(5).GetCell(0).SetCellValue("REPORT MACHINE: " + pName);
            sheet.GetRow(6).GetCell(0).SetCellValue(pDate);
            ICellStyle cellStyle2 = xSSFWorkbook.CreateCellStyle();
            cellStyle2.BorderTop = BorderStyle.Thin;
            cellStyle2.BorderLeft = BorderStyle.Thin;
            cellStyle2.BorderBottom = BorderStyle.Thin;
            cellStyle2.BorderRight = BorderStyle.Thin;
            cellStyle2.FillForegroundColor = IndexedColors.Green.Index;
            cellStyle2.FillPattern = FillPattern.SolidForeground;
            ICellStyle cellStyle3 = xSSFWorkbook.CreateCellStyle();
            cellStyle3.BorderTop = BorderStyle.Thin;
            cellStyle3.BorderLeft = BorderStyle.Thin;
            cellStyle3.BorderBottom = BorderStyle.Thin;
            cellStyle3.BorderRight = BorderStyle.Thin;
            cellStyle3.FillForegroundColor = IndexedColors.Yellow.Index;
            cellStyle3.FillPattern = FillPattern.SolidForeground;
            ICellStyle cellStyle4 = xSSFWorkbook.CreateCellStyle();
            cellStyle4.BorderTop = BorderStyle.Thin;
            cellStyle4.BorderLeft = BorderStyle.Thin;
            cellStyle4.BorderBottom = BorderStyle.Thin;
            cellStyle4.BorderRight = BorderStyle.Thin;
            cellStyle4.FillForegroundColor = IndexedColors.Red.Index;
            cellStyle4.FillPattern = FillPattern.SolidForeground;
            cellStyle2.Alignment = (cellStyle3.Alignment = (cellStyle4.Alignment = HorizontalAlignment.Center));
            ICellStyle cellStyle5 = null;
            for (int j = 0; j < pData.Rows.Count; j++)
            {
                IRow expr_209 = sheet.CreateRow(9 + j);
                ICell cell = expr_209.CreateCell(2);
                ICell cell2 = expr_209.CreateCell(3);
                switch (Convert.ToInt32(pData.Rows[j]["State"]))
                {
                    case 1:
                        cellStyle5 = cellStyle2;
                        break;
                    case 2:
                        cellStyle5 = cellStyle3;
                        break;
                    case 3:
                        cellStyle5 = cellStyle4;
                        break;
                }
                cell.CellStyle = cellStyle5;
                cell2.CellStyle = cellStyle5;
                cell.SetCellValue(Convert.ToDateTime(pData.Rows[j]["TTime"]).ToString("dd/MM/yyyy HH:mm"));
                cell2.SetCellValue((double)Convert.ToInt32(pData.Rows[j]["iCnt"]));
            }
            return Excel.WriteToStream(xSSFWorkbook).GetBuffer();
        }
         public static byte[] ReportMachine(DataTable pData, string pName, string pDate)
        {
            //XSSFWorkbook xSSFWorkbook;
            //using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report1.xlsx"), FileMode.Open, FileAccess.Read))
            //{
            //    xSSFWorkbook = new XSSFWorkbook(fileStream);
            //}
            //ISheet sheet = xSSFWorkbook.GetSheet("Sheet1");
            //ICellStyle cellStyle = xSSFWorkbook.CreateCellStyle();

            HSSFWorkbook hssfWorkbook;
            using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report1.xls"), FileMode.Open, FileAccess.Read))
            {
                hssfWorkbook = new HSSFWorkbook(fs);
            }
            ISheet sheet = hssfWorkbook.GetSheet("Sheet1");
            ICellStyle cellStyle = hssfWorkbook.CreateCellStyle();

            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = HorizontalAlignment.Center;
            sheet.GetRow(6).GetCell(0).SetCellValue(pDate);
            string[] array = new string[]
            {
                "Time",
                "Times/Min",
                "State",
                "P/N",
                "Process",
                "Status"
              
            };
            IRow row = sheet.CreateRow(8);
            for (int i = 0; i < array.Length; i++)
            {
                ICell expr_C3 = row.CreateCell(i + 2);
                expr_C3.SetCellValue(array[i]);
                expr_C3.CellStyle = cellStyle;
            }
            sheet.GetRow(5).GetCell(0).SetCellValue("REPORT MACHINE: " + pName);
            sheet.GetRow(6).GetCell(0).SetCellValue(pDate);
            ICellStyle cellStyle2 = hssfWorkbook.CreateCellStyle();
            cellStyle2.BorderTop = BorderStyle.Thin;
            cellStyle2.BorderLeft = BorderStyle.Thin;
            cellStyle2.BorderBottom = BorderStyle.Thin;
            cellStyle2.BorderRight = BorderStyle.Thin;
            cellStyle2.FillForegroundColor = IndexedColors.Green.Index;
            cellStyle2.FillPattern = FillPattern.SolidForeground;
            ICellStyle cellStyle3 = hssfWorkbook.CreateCellStyle();
            cellStyle3.BorderTop = BorderStyle.Thin;
            cellStyle3.BorderLeft = BorderStyle.Thin;
            cellStyle3.BorderBottom = BorderStyle.Thin;
            cellStyle3.BorderRight = BorderStyle.Thin;
            cellStyle3.FillForegroundColor = IndexedColors.Yellow.Index;
            cellStyle3.FillPattern = FillPattern.SolidForeground;
            ICellStyle cellStyle4 = hssfWorkbook.CreateCellStyle();
            cellStyle4.BorderTop = BorderStyle.Thin;
            cellStyle4.BorderLeft = BorderStyle.Thin;
            cellStyle4.BorderBottom = BorderStyle.Thin;
            cellStyle4.BorderRight = BorderStyle.Thin;
            cellStyle4.FillForegroundColor = IndexedColors.Red.Index;
            cellStyle4.FillPattern = FillPattern.SolidForeground;
            cellStyle2.Alignment = (cellStyle3.Alignment = (cellStyle4.Alignment = HorizontalAlignment.Center));
            ICellStyle cellStyle5 = null;
            for (int j = 0; j < pData.Rows.Count; j++)
            {
                IRow expr_209 = sheet.CreateRow(9 + j);
                ICell cell = expr_209.CreateCell(2);
                ICell cell2 = expr_209.CreateCell(3);
                ICell cell3 = expr_209.CreateCell(4);
                ICell cell4 = expr_209.CreateCell(5);
                ICell cell5 = expr_209.CreateCell(6);
                ICell cell6 = expr_209.CreateCell(7);
                switch (Convert.ToInt32(pData.Rows[j]["State"]))
                {
                    case 1:
                        cellStyle5 = cellStyle2;
                        break;
                    case 2:
                        cellStyle5 = cellStyle3;
                        break;
                    case 3:
                        cellStyle5 = cellStyle4;
                        break;
                }
                cellStyle5.FillForegroundColor = IndexedColors.White.Index;
                cell.CellStyle = cellStyle5;
                cell2.CellStyle = cellStyle5;
                cell3.CellStyle = cellStyle5;
                cell4.CellStyle = cellStyle5;
                cell5.CellStyle = cellStyle5;
                cell6.CellStyle = cellStyle5;
                cell.SetCellValue(Convert.ToDateTime(pData.Rows[j]["TTime"]).ToString("dd/MM/yyyy HH:mm"));
                cell2.SetCellValue((double)Convert.ToInt32(pData.Rows[j]["iCnt"]));
                cell3.SetCellValue((double)Convert.ToInt32(pData.Rows[j]["State"]));
                cell4.SetCellValue(pData.Rows[j]["PN"].ToString());
                cell5.SetCellValue(pData.Rows[j]["process"].ToString());
                cell6.SetCellValue(pData.Rows[j]["cDesc"].ToString());
            }
            //return Excel.WriteToStream(xSSFWorkbook).GetBuffer();
            MemoryStream ms = new MemoryStream();
            hssfWorkbook.Write(ms);
            return ms.GetBuffer();
        }
        public static byte[] Report5(DataTable pData, string year1, string year2,int max_count_col)
        {
            //XSSFWorkbook xSSFWorkbook;
            //using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report5.xlsx"), FileMode.Open, FileAccess.Read))
            //{
            //    xSSFWorkbook = new XSSFWorkbook(fileStream);
            //}
            //ISheet sheet = xSSFWorkbook.GetSheet("Sheet1");
            //ICellStyle cellStyle = xSSFWorkbook.CreateCellStyle();

            HSSFWorkbook hssfWorkbook;
            using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report5.xls"), FileMode.Open, FileAccess.Read))
            {
                hssfWorkbook = new HSSFWorkbook(fs);
            }
            ISheet sheet = hssfWorkbook.GetSheet("Sheet1");
            ICellStyle cellStyle = hssfWorkbook.CreateCellStyle();

            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = HorizontalAlignment.Center;
            string[] array0 = new string[max_count_col * 2 + 10];
            for (int i = 0; i < array0.Length; i++)
            {
                array0[i] = "";
            }
            array0[3] = year1;
            array0[5] = year2;
          
            string[] array = new string[max_count_col * 2 + 10];
            int count = -2;
            for (int i = 0; i < array.Length; i++)
            {
                if(i%2==1)
                {
                    array[i] = "采购量" + count.ToString();
                }                   
                else
                {
                    array[i] = "单价" + count.ToString() + "(USD)";
                    count++;
                }
                   
            }
            array[0] = "材料编号";
            array[1] = "品名规格";
            array[2] = "采购单位";
            array[3] = year1 + "采购量";
            array[4] = year1 + "平均单价(USD)";

            array[array.Length - 5] = year2 + "采购量";
            array[array.Length - 4] = year2 + "平均单价(USD)";
            array[array.Length - 3] = "价差";
            array[array.Length - 2] = "价差差异(%)";
            array[array.Length - 1] = "差异金额";
          
            IRow row0= sheet.CreateRow(7);
            for (int i = 0; i < array0.Length; i++)
            {
                ICell expr_C3 = row0.CreateCell(i);
                expr_C3.SetCellValue(array0[i]);
                expr_C3.CellStyle = cellStyle;
            }
            IRow row = sheet.CreateRow(8);
            for (int i = 0; i < array.Length; i++)
            {
                ICell expr_C3 = row.CreateCell(i);
                expr_C3.SetCellValue(array[i]);
                expr_C3.CellStyle = cellStyle;
            }
            
            sheet.GetRow(4).GetCell(1).SetCellValue("報表轉出日期: " + DateTime.Now.ToString("yyyy/MM/dd"));
            sheet.GetRow(5).GetCell(0).SetCellValue("REPORT: " + year1 + " & " + year2);
            ICellStyle cellStyle2 = hssfWorkbook.CreateCellStyle();
            cellStyle2.BorderTop = BorderStyle.Thin;
            cellStyle2.BorderLeft = BorderStyle.Thin;
            cellStyle2.BorderBottom = BorderStyle.Thin;
            cellStyle2.BorderRight = BorderStyle.Thin;
            cellStyle2.FillForegroundColor = IndexedColors.White.Index;
            cellStyle2.FillPattern = FillPattern.SolidForeground;
            
            cellStyle2.Alignment =   HorizontalAlignment.Center;
           
            for (int j = 0; j < pData.Rows.Count; j++)
            {
                IRow expr_209 = sheet.CreateRow(9 + j);
                //ICell cell = expr_209.CreateCell(2);
                //ICell cell2 = expr_209.CreateCell(3);
                //ICell cell3 = expr_209.CreateCell(4);
                //ICell cell4 = expr_209.CreateCell(5);
                //ICell cell5 = expr_209.CreateCell(6);
                //ICell cell6 = expr_209.CreateCell(7);
                //ICell cell7 = expr_209.CreateCell(8);
                //ICell cell8 = expr_209.CreateCell(9);
                //ICell cell9 = expr_209.CreateCell(10);
                //ICell cell10 = expr_209.CreateCell(11);
                

                ICell[] cell_arr = new ICell[max_count_col*2+10];
                for (int i=0;i< cell_arr.Length; i++)
                {
                    cell_arr[i]= expr_209.CreateCell(i);
                    cell_arr[i].CellStyle = cellStyle2; 
                }

              

                cell_arr[0].SetCellValue(pData.Rows[j]["pmn04"].ToString());
                cell_arr[1].SetCellValue(pData.Rows[j]["ima02"].ToString());
                cell_arr[2].SetCellValue(pData.Rows[j]["pmn07"].ToString());

                if (pData.Rows[j]["pmn20_1"].ToString().Trim() != "")
                    cell_arr[3].SetCellValue(Convert.ToDouble(pData.Rows[j]["pmn20_1"]));
                if (pData.Rows[j]["pmn31t_1"].ToString().Trim() != "")
                    try
                    {
                        cell_arr[4].SetCellValue(Convert.ToDouble(pData.Rows[j]["pmn31t_1"]));
                    }
                    catch
                    {
                        cell_arr[4].SetCellValue(pData.Rows[j]["pmn31t_1"].ToString());
                    }

                for (int i = 0; i < max_count_col * 2; i++)
                {
                    if (pData.Rows[j][i + 10].ToString().Trim() != "")
                        cell_arr[i + 5].SetCellValue(Convert.ToDouble(pData.Rows[j][i + 10]));
                }
                if (pData.Rows[j]["pmn20_2"].ToString().Trim() != "")
                    cell_arr[cell_arr.Length - 5].SetCellValue(Convert.ToDouble(pData.Rows[j]["pmn20_2"]));
                if (pData.Rows[j]["pmn31t_2"].ToString().Trim() != "")
                    try
                    {
                        cell_arr[cell_arr.Length - 4].SetCellValue(Convert.ToDouble(pData.Rows[j]["pmn31t_2"]));
                    }
                    catch
                    {
                        cell_arr[cell_arr.Length - 4].SetCellValue(pData.Rows[j]["pmn31t_2"].ToString());
                    }
                    
                if (pData.Rows[j]["price_diff"].ToString().Trim() != "")
                    cell_arr[cell_arr.Length - 3].SetCellValue(Convert.ToDouble(pData.Rows[j]["price_diff"]));
                if (pData.Rows[j]["price_diff_percent"].ToString().Trim() != "")
                    try
                    {
                        cell_arr[cell_arr.Length - 2].SetCellValue(Convert.ToDouble(pData.Rows[j]["price_diff_percent"]));
                    }
                    catch
                    {
                        cell_arr[cell_arr.Length - 2].SetCellValue(pData.Rows[j]["price_diff_percent"].ToString());
                    }
                  
                if (pData.Rows[j]["price_diff2"].ToString().Trim() != "")
                    try
                    {
                        cell_arr[cell_arr.Length - 1].SetCellValue(Convert.ToDouble(pData.Rows[j]["price_diff2"]));
                    }
                    catch
                    {
                        cell_arr[cell_arr.Length - 1].SetCellValue(pData.Rows[j]["price_diff2"].ToString());
                    }
             
            }
            //return Excel.WriteToStream(xSSFWorkbook).GetBuffer();
            MemoryStream ms = new MemoryStream();
            hssfWorkbook.Write(ms);
            return ms.GetBuffer();
        }
        public static byte[] Report6(string[] col_name,DataTable pData, string date1, string date2, int col_num)
        {
            //XSSFWorkbook xSSFWorkbook;
            //using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report6.xlsx"), FileMode.Open, FileAccess.Read))
            //{
            //    xSSFWorkbook = new XSSFWorkbook(fileStream);
            //}
            //ISheet sheet = xSSFWorkbook.GetSheet("Sheet1");
            //ICellStyle cellStyle = xSSFWorkbook.CreateCellStyle();

            HSSFWorkbook hssfWorkbook;
            using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report6.xls"), FileMode.Open, FileAccess.Read))
            {
                hssfWorkbook = new HSSFWorkbook(fs);
            }
            ISheet sheet = hssfWorkbook.GetSheet("Sheet1");
            ICellStyle cellStyle = hssfWorkbook.CreateCellStyle();

            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = HorizontalAlignment.Center;
          

            string[] array = new string[col_num];
         
            for (int i = 0; i < col_num; i++)
            {
                array[i] = col_name[i].ToString();

            }        

         
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < array.Length; i++)
            {
                ICell expr_C3 = row.CreateCell(i);
                expr_C3.SetCellValue(array[i]);
                expr_C3.CellStyle = cellStyle;
            }

            ICellStyle cellStyle2 = hssfWorkbook.CreateCellStyle();
            cellStyle2.BorderTop = BorderStyle.Thin;
            cellStyle2.BorderLeft = BorderStyle.Thin;
            cellStyle2.BorderBottom = BorderStyle.Thin;
            cellStyle2.BorderRight = BorderStyle.Thin;
            cellStyle2.FillForegroundColor = IndexedColors.White.Index;
            cellStyle2.FillPattern = FillPattern.SolidForeground;

            cellStyle2.Alignment = HorizontalAlignment.Center;

            for (int j = 0; j < pData.Rows.Count; j++)
            {
                IRow expr_209 = sheet.CreateRow(j+1);              


                ICell[] cell_arr = new ICell[col_num];
                for (int i = 0; i < col_num; i++)
                {
                    cell_arr[i] = expr_209.CreateCell(i);
                    cell_arr[i].CellStyle = cellStyle2;
                }
             
                for (int i = 0; i < col_num; i++)
                {
                    if (pData.Rows[j][i].ToString().Trim() != "")
                        cell_arr[i].SetCellValue(pData.Rows[j][i].ToString());
                }
              

            }
            //return Excel.WriteToStream(xSSFWorkbook).GetBuffer();
            MemoryStream ms = new MemoryStream();
            hssfWorkbook.Write(ms);
            return ms.GetBuffer();
        }
        public static byte[] Report7(string[] col_name, DataTable pData, string date1, string date2, int col_num, DataTable pData1 = null)
        {
            //XSSFWorkbook xSSFWorkbook;
            //using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report7.xlsx"), FileMode.Open, FileAccess.Read))
            //{
            //    xSSFWorkbook = new XSSFWorkbook(fileStream);
            //}
            //ISheet sheet = xSSFWorkbook.GetSheet("Sheet1");
            //ICellStyle cellStyle = xSSFWorkbook.CreateCellStyle();

            HSSFWorkbook hssfWorkbook;
            using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report7.xls"), FileMode.Open, FileAccess.Read))
            {
                hssfWorkbook = new HSSFWorkbook(fs);
            }
            ISheet sheet = hssfWorkbook.GetSheet("Sheet1");
            ICellStyle cellStyle = hssfWorkbook.CreateCellStyle();

            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = HorizontalAlignment.Center;


            string[] array = new string[col_num];

            for (int i = 0; i < col_num; i++)
            {
                array[i] = col_name[i].ToString();

            }


            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < array.Length; i++)
            {
                ICell expr_C3 = row.CreateCell(i);
                expr_C3.SetCellValue(array[i]);
                expr_C3.CellStyle = cellStyle;
            }

            //ICellStyle cellStyle2 = xSSFWorkbook.CreateCellStyle();
            ICellStyle cellStyle2 = hssfWorkbook.CreateCellStyle();
            cellStyle2.BorderTop = BorderStyle.Thin;
            cellStyle2.BorderLeft = BorderStyle.Thin;
            cellStyle2.BorderBottom = BorderStyle.Thin;
            cellStyle2.BorderRight = BorderStyle.Thin;
            cellStyle2.FillForegroundColor = IndexedColors.White.Index;
            cellStyle2.FillPattern = FillPattern.SolidForeground;

            cellStyle2.Alignment = HorizontalAlignment.Center;

            for (int j = 0; j < pData.Rows.Count; j++)
            {
                IRow expr_209 = sheet.CreateRow(j + 1);


                ICell[] cell_arr = new ICell[col_num];
                for (int i = 0; i < col_num; i++)
                {
                    cell_arr[i] = expr_209.CreateCell(i);
                    cell_arr[i].CellStyle = cellStyle2;

                    if (pData.Rows[j][i].ToString().Trim() != "")
                    {
                        switch (pData.Columns[i].DataType.Name)
                        {
                            case "String":
                                cell_arr[i].SetCellValue(pData.Rows[j][i].ToString());
                                break;
                            case "Decimal":
                                cell_arr[i].SetCellValue(Convert.ToDouble(pData.Rows[j][i]));
                                break;
                            case "DateTime":
                                cell_arr[i].SetCellValue(Convert.ToDateTime(pData.Rows[j][i]));
                                break;
                        }
                    }
                }

                //for (int i = 0; i < col_num; i++)
                //{
                //    if (pData.Rows[j][i].ToString().Trim() != "")
                //        cell_arr[i].SetCellValue(pData.Rows[j][i].ToString());
                //}


            }

            // Sheet 2
            if (pData1 != null)
            {
                sheet = hssfWorkbook.GetSheet("Sheet2");

                row = sheet.CreateRow(0);
                for (int i = 0; i < pData1.Columns.Count; i++)
                {
                    ICell expr_C3 = row.CreateCell(i);
                    expr_C3.SetCellValue(pData1.Columns[i].ColumnName);
                    expr_C3.CellStyle = cellStyle;
                }

                string curString = string.Empty;
                int index = 0;
                for (int i = 0; i < pData1.Rows.Count; i++)
                {
                    IRow expr_209 = sheet.CreateRow(i + 1);

                    ICell[] cell_arr = new ICell[pData1.Columns.Count];
                    for (int j = 0; j < pData1.Columns.Count; j++)
                    {
                        cell_arr[j] = expr_209.CreateCell(j);
                        cell_arr[j].CellStyle = cellStyle2;

                        if (pData1.Rows[i][j].ToString().Trim() != "")
                        {
                            if (j == 0)
                            {
                                if (!curString.Equals(pData1.Rows[i][j].ToString()))
                                {
                                    if (i > 0 && index < i && !string.IsNullOrEmpty(curString))
                                    {
                                        var arr = new NPOI.SS.Util.CellRangeAddress(index, i, j, j);
                                        sheet.AddMergedRegion(arr);
                                    }
                                    curString = pData1.Rows[i][j].ToString();
                                    index = i + 1;

                                    switch (pData1.Columns[j].DataType.Name)
                                    {
                                        case "String":
                                            cell_arr[j].SetCellValue(pData1.Rows[i][j].ToString());
                                            break;
                                        case "Double":
                                            cell_arr[j].SetCellValue(Convert.ToDouble(pData1.Rows[i][j]));
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                switch (pData1.Columns[j].DataType.Name)
                                {
                                    case "String":
                                        cell_arr[j].SetCellValue(pData1.Rows[i][j].ToString());
                                        break;
                                    case "Double":
                                        cell_arr[j].SetCellValue(Convert.ToDouble(pData1.Rows[i][j]));
                                        break;
                                }
                            }
                        }
                    }
                    sheet.AutoSizeColumn(i, true);
                }
            }

            //return Excel.WriteToStream(xSSFWorkbook).GetBuffer();
            MemoryStream mstream = new MemoryStream();
            hssfWorkbook.Write(mstream);
            return mstream.GetBuffer();
        }
        public static byte[] Report8(string[] col_name, DataTable pData, string query_time, int col_num)
        {
            //XSSFWorkbook xSSFWorkbook;
            //using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report8.xlsx"), FileMode.Open, FileAccess.Read))
            //{
            //    xSSFWorkbook = new XSSFWorkbook(fileStream);
            //}
            //ISheet sheet = xSSFWorkbook.GetSheet("Sheet1");
            //ICellStyle cellStyle = xSSFWorkbook.CreateCellStyle();

            HSSFWorkbook hssfWorkbook;
            using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report8.xls"), FileMode.Open, FileAccess.Read))
            {
                hssfWorkbook = new HSSFWorkbook(fileStream);
            }
            ISheet sheet = hssfWorkbook.GetSheet("Sheet1");
            ICellStyle cellStyle = hssfWorkbook.CreateCellStyle();

            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = HorizontalAlignment.Center;


            string[] array0 = new string[col_num];

            for (int i = 0; i < array0.Length; i++)
            {
                array0[i] = "天(%)";

            }
            array0[0] = "";
            array0[array0.Length-1] = "";

            IRow row0 = sheet.CreateRow(2);
            for (int i = 0; i < array0.Length; i++)
            {
                ICell expr_C3 = row0.CreateCell(i);
                expr_C3.SetCellValue(array0[i]);
                expr_C3.CellStyle = cellStyle;
            }

            string[] array = new string[col_num];

            for (int i = 0; i < col_num; i++)
            {
                array[i] = col_name[i].ToString();

            }


            IRow row = sheet.CreateRow(3);
            for (int i = 0; i < array.Length; i++)
            {
                ICell expr_C3 = row.CreateCell(i);
                expr_C3.SetCellValue(array[i]);
                expr_C3.CellStyle = cellStyle;
            }
            sheet.GetRow(0).GetCell(0).SetCellValue("CÔNG SUẤT MÁY THÁNG "+ query_time .Substring(2,2)+ " NĂM 20"+ query_time.Substring(0,2));
            sheet.GetRow(1).GetCell(0).SetCellValue("20" + query_time.Substring(0, 2)+ "年"+ query_time .Substring(2,2)+"月的設備稼動率");
            ICellStyle cellStyle2 = hssfWorkbook.CreateCellStyle();
            cellStyle2.BorderTop = BorderStyle.Thin;
            cellStyle2.BorderLeft = BorderStyle.Thin;
            cellStyle2.BorderBottom = BorderStyle.Thin;
            cellStyle2.BorderRight = BorderStyle.Thin;
            cellStyle2.FillForegroundColor = IndexedColors.White.Index;
            cellStyle2.FillPattern = FillPattern.SolidForeground;

            cellStyle2.Alignment = HorizontalAlignment.Center;

            for (int j = 0; j < pData.Rows.Count; j++)
            {
                IRow expr_209 = sheet.CreateRow(j + 4);


                ICell[] cell_arr = new ICell[col_num];
                for (int i = 0; i < col_num; i++)
                {
                    cell_arr[i] = expr_209.CreateCell(i);
                    cell_arr[i].CellStyle = cellStyle2;
                }

                for (int i = 0; i < col_num; i++)
                {
                    if (pData.Rows[j][i].ToString().Trim() != "")
                    {
                        if(i<=4)
                            cell_arr[i].SetCellValue(pData.Rows[j][i].ToString());
                        else
                            cell_arr[i].SetCellValue(Convert.ToDouble(pData.Rows[j][i]));
                    }
                       
                }


            }
            //return Excel.WriteToStream(hssfWorkbook).GetBuffer();
            MemoryStream ms = new MemoryStream();
            hssfWorkbook.Write(ms);
            return ms.GetBuffer();
        }
        public static byte[] Report9(string[] col_name, DataTable pData, string date1, string date2, int col_num)
        {
            //XSSFWorkbook xSSFWorkbook;
            //using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report9.xlsx"), FileMode.Open, FileAccess.Read))
            //{
            //    xSSFWorkbook = new XSSFWorkbook(fileStream);
            //}
            //ISheet sheet = xSSFWorkbook.GetSheet("Sheet1");
            //ICellStyle cellStyle = xSSFWorkbook.CreateCellStyle();

            HSSFWorkbook hssfWorkbook;
            using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report9.xls"), FileMode.Open, FileAccess.Read))
            {
                hssfWorkbook = new HSSFWorkbook(fileStream);
            }
            ISheet sheet = hssfWorkbook.GetSheet("Sheet1");
            ICellStyle cellStyle = hssfWorkbook.CreateCellStyle();

            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = HorizontalAlignment.Center;


            string[] array = new string[col_num];

            for (int i = 0; i < col_num; i++)
            {
                array[i] = col_name[i].ToString();

            }


            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < array.Length; i++)
            {
                ICell expr_C3 = row.CreateCell(i);
                expr_C3.SetCellValue(array[i]);
                expr_C3.CellStyle = cellStyle;
            }

            ICellStyle cellStyle2 = hssfWorkbook.CreateCellStyle();
            cellStyle2.BorderTop = BorderStyle.Thin;
            cellStyle2.BorderLeft = BorderStyle.Thin;
            cellStyle2.BorderBottom = BorderStyle.Thin;
            cellStyle2.BorderRight = BorderStyle.Thin;
            cellStyle2.FillForegroundColor = IndexedColors.White.Index;
            cellStyle2.FillPattern = FillPattern.SolidForeground;

            cellStyle2.Alignment = HorizontalAlignment.Center;

            for (int j = 0; j < pData.Rows.Count; j++)
            {
                IRow expr_209 = sheet.CreateRow(j + 1);


                ICell[] cell_arr = new ICell[col_num];
                for (int i = 0; i < col_num; i++)
                {
                    cell_arr[i] = expr_209.CreateCell(i);
                    cell_arr[i].CellStyle = cellStyle2;
                }

                for (int i = 0; i < col_num; i++)
                {
                    if (pData.Rows[j][i].ToString().Trim() != "")
                        cell_arr[i].SetCellValue(pData.Rows[j][i].ToString());
                }


            }
            //return Excel.WriteToStream(hssfWorkbook).GetBuffer();
            MemoryStream ms = new MemoryStream();
            hssfWorkbook.Write(ms);
            return ms.GetBuffer();
        }

        public static byte[] Report(string[] col_name, DataTable pData,string query_time, int col_num)
        {
            //XSSFWorkbook xSSFWorkbook;
            //using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report.xlsx"), FileMode.Open, FileAccess.Read))
            //{
            //    xSSFWorkbook = new XSSFWorkbook(fileStream);
            //}
            //ISheet sheet = xSSFWorkbook.GetSheet("Sheet1");
            //ICellStyle cellStyle = xSSFWorkbook.CreateCellStyle();

            HSSFWorkbook hssfWorkbook;
            using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report.xls"), FileMode.Open, FileAccess.Read))
            {
                hssfWorkbook = new HSSFWorkbook(fileStream);
            }
            ISheet sheet = hssfWorkbook.GetSheet("Sheet1");
            ICellStyle cellStyle = hssfWorkbook.CreateCellStyle();

            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = HorizontalAlignment.Center;


            string[] array = new string[col_num];

            for (int i = 0; i < col_num; i++)
            {
                array[i] = col_name[i].ToString();

            }


            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < array.Length; i++)
            {
                ICell expr_C3 = row.CreateCell(i);
                expr_C3.SetCellValue(array[i]);
                expr_C3.CellStyle = cellStyle;
            }

            ICellStyle cellStyle2 = hssfWorkbook.CreateCellStyle();
            cellStyle2.BorderTop = BorderStyle.Thin;
            cellStyle2.BorderLeft = BorderStyle.Thin;
            cellStyle2.BorderBottom = BorderStyle.Thin;
            cellStyle2.BorderRight = BorderStyle.Thin;
            cellStyle2.FillForegroundColor = IndexedColors.White.Index;
            cellStyle2.FillPattern = FillPattern.SolidForeground;

            cellStyle2.Alignment = HorizontalAlignment.Center;

            for (int j = 0; j < pData.Rows.Count; j++)
            {
                IRow expr_209 = sheet.CreateRow(j + 1);


                ICell[] cell_arr = new ICell[col_num];
                for (int i = 0; i < col_num; i++)
                {
                    cell_arr[i] = expr_209.CreateCell(i);
                    cell_arr[i].CellStyle = cellStyle2;
                }

                for (int i = 0; i < col_num; i++)
                {
                    if (pData.Rows[j][i].ToString().Trim() != "")
                        cell_arr[i].SetCellValue(pData.Rows[j][i].ToString());
                }


            }
            //return Excel.WriteToStream(hssfWorkbook).GetBuffer();
            MemoryStream ms = new MemoryStream();
            hssfWorkbook.Write(ms);
            return ms.GetBuffer();
        }
        public static byte[] RerportSYSTEM(DataTable pData, string pDate)
        {
            XSSFWorkbook xSSFWorkbook;
            using (FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath("\\image\\report2.xlsx"), FileMode.Open, FileAccess.Read))
            {
                xSSFWorkbook = new XSSFWorkbook(fileStream);
            }
            ISheet sheet = xSSFWorkbook.GetSheet("Sheet1");
            sheet.GetRow(6).GetCell(0).SetCellValue(pDate);
            ICellStyle cellStyle = xSSFWorkbook.CreateCellStyle();
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.Alignment = HorizontalAlignment.Center;
            ICellStyle cellStyle2 = xSSFWorkbook.CreateCellStyle();
            cellStyle2.BorderTop = BorderStyle.Thin;
            cellStyle2.BorderLeft = BorderStyle.Thin;
            cellStyle2.BorderBottom = BorderStyle.Thin;
            cellStyle2.BorderRight = BorderStyle.Thin;
            cellStyle2.Alignment = HorizontalAlignment.Center;
            cellStyle2.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyle2.FillPattern = FillPattern.SolidForeground;
            sheet.GetRow(6).GetCell(0).SetCellValue(pDate);
            string[] array = new string[]
            {
                "AREA",
                "MACHINE CODE",
                "STATUS",
                "TOTAL TIME",
                "TOTAL PRODUCT",
                "PRODUCT/MINUTE"
            };
            IRow row = sheet.CreateRow(8);
            for (int i = 0; i < 6; i++)
            {
                ICell expr_11F = row.CreateCell(i);
                expr_11F.CellStyle = cellStyle2;
                expr_11F.SetCellValue(array[i]);
            }
            for (int j = 0; j < pData.Rows.Count; j++)
            {
                IRow expr_14E = sheet.CreateRow(9 + j);
                ICell cell = expr_14E.CreateCell(0);
                ICell cell2 = expr_14E.CreateCell(1);
                ICell cell3 = expr_14E.CreateCell(2);
                ICell cell4 = expr_14E.CreateCell(3);
                ICell cell5 = expr_14E.CreateCell(4);
                ICell cell6 = expr_14E.CreateCell(5);
                cell.SetCellValue(pData.Rows[j]["AREA"].ToString());
                cell2.SetCellValue(pData.Rows[j]["CODE"].ToString());
                cell3.SetCellValue(pData.Rows[j]["STATUS"].ToString());
                cell4.SetCellValue(pData.Rows[j]["TOTALT"].ToString());
                cell5.SetCellValue(pData.Rows[j]["TOTAL"].ToString());
                cell6.SetCellValue(pData.Rows[j]["AVG"].ToString());
                cell.CellStyle = (cell2.CellStyle = (cell3.CellStyle = (cell4.CellStyle = (cell5.CellStyle = (cell6.CellStyle = cellStyle)))));
            }
            return Excel.WriteToStream(xSSFWorkbook).GetBuffer();
        }

        public static int LoadImage(string path, IWorkbook wb)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] array = new byte[fileStream.Length];
            fileStream.Read(array, 0, (int)fileStream.Length);
            return wb.AddPicture(array, PictureType.JPEG);
        }

        //private static MemoryStream WriteToStream(XSSFWorkbook workbook)
        //{
        //    MemoryStream memoryStream = new MemoryStream();
        //    workbook.Write(memoryStream);
        //    return memoryStream;
        //}

        //private static MemoryStream WriteToStream(HSSFWorkbook workbook)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    workbook.Write(ms);
        //    return ms;
        //}

        private static MemoryStream WriteToStream(IWorkbook workbook)
        {
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            return ms;
        }
    }
}