using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DeviceBase.BLL.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace DeviceBase.BLL.Implement.Export
{
    public class MsExcel:IExport
    {
        public byte[] Export(DataTable datatable)
        {
            string[] header = new[]
            {
                "Инв.№", "Инв.№ (бух)", "Серийный номер", "Класс устройства", "Тип устройства", "Подтип устройства", "Модель", "Ввод в эклпл.",
                "Комментарий", "Ответственный", "Должность", "Служба","Отдел","Адрес","Комната","Этаж","Тел.","Площадка"
            };

            if (datatable == null)
            {
                return null;
            }
            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add("Devices");
                int startRowFrom = 1;
                workSheet.Cells[startRowFrom, 1].Value = "DeviceBase";
                workSheet.Cells[startRowFrom, 1, startRowFrom + 1, datatable.Columns.Count].Merge = true;
                workSheet.Cells[startRowFrom, 1, startRowFrom + 1, datatable.Columns.Count].Style.Font.Bold = true;
                workSheet.Cells[startRowFrom, 1, startRowFrom + 1, datatable.Columns.Count].Style.Font.Size = 16;
                workSheet.Cells[startRowFrom, 1, startRowFrom + 1, datatable.Columns.Count].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#B2B2B2"));
                workSheet.Cells[startRowFrom, 1, startRowFrom + 1, datatable.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                
                workSheet.Cells["A3"].LoadFromDataTable(datatable, true);


                int columnIndex = 1;
                foreach (DataColumn column in datatable.Columns)
                {
                    workSheet.Cells[3, columnIndex].Value = header[columnIndex - 1];
                    columnIndex++;
                }

                columnIndex = 1;
                foreach (DataColumn column in datatable.Columns)
                {
                    int maxcell =  column.Table.Rows[1].ItemArray[columnIndex-1].ToString().Length;
                    if (maxcell < 150)
                    {
                        workSheet.Column(columnIndex).AutoFit();
                    }
                    columnIndex++;
                }

                
                
                using (ExcelRange r = workSheet.Cells[startRowFrom + 2, 1, startRowFrom + 2, datatable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#2A2A29"));
                    
                }

               
                using (ExcelRange r = workSheet.Cells[startRowFrom + 2, 1, (startRowFrom + 2) + datatable.Rows.Count, datatable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#B2B2B2"));
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#B2B2B2"));
                }


                result = package.GetAsByteArray();
            }

            return result;
        }
    }
}