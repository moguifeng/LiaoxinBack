using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RedpacketTestApp
{
    public static class EPPlusExcelHelper
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <returns></returns>
        public static MemoryStream ExportExcelByDataTable(DataTable dataTable)
        {
            MemoryStream reslut = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(dataTable.TableName);
                int startRowFrom = 1;
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);
                int columnIndex = 1;
                foreach (DataColumn item in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                    int maxLength = columnCells.Max(cell => cell.Value == null ? 1 : cell.Value.ToString().Count());
                    if (maxLength < 150)
                    {
                        workSheet.Column(columnIndex).AutoFit();
                    }
                    columnIndex++;
                }
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                    r.Style.WrapText = true;
                }
                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }
                package.SaveAs(reslut);
            }
            return reslut;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <returns></returns>
        public static MemoryStream ExportExcelByDataSet(DataSet ds)
        {
            MemoryStream reslut = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage())
            {
                foreach (DataTable dataTable in ds.Tables)
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(dataTable.TableName);
                    int startRowFrom = 1;
                    workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);
                    int columnIndex = 1;
                    foreach (DataColumn item in dataTable.Columns)
                    {
                        ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                        int maxLength = columnCells.Max(cell => cell.Value == null ? 1 : cell.Value.ToString().Count());
                        if (maxLength < 150)
                        {
                            workSheet.Column(columnIndex).AutoFit();
                        }
                        columnIndex++;
                    }
                    using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                    {
                        r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        r.Style.Font.Bold = true;
                        r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                        r.Style.WrapText = true;
                    }
                    using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                    {
                        r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                    }
                }
                package.SaveAs(reslut);
            }
            return reslut;
        }


        /// <summary>
        ///将指定的Excel的文件转换成DataTable （Excel的第一个sheet）
        /// </summary>
        /// <param name="fullFielPath">文件的绝对路径</param>
        /// <returns></returns>
        public static DataTable WorksheetToTable(string fullFielPath)
        {
            try
            {
                FileInfo existingFile = new FileInfo(fullFielPath);

                ExcelPackage package = new ExcelPackage(existingFile);
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];//选定 指定页

                return WorksheetToTable(worksheet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 将worksheet转成datatable
        /// </summary>
        /// <param name="worksheet">待处理的worksheet</param>
        /// <returns>返回处理后的datatable</returns>
        public static DataTable WorksheetToTable(ExcelWorksheet worksheet, bool hasHeader = true)
        {

            DataTable dt = new DataTable();
            int columnCount = 0;
            foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                dt.Columns.Add(hasHeader ? firstRowCell.Text.Replace(" ", "") : string.Format("Column {0}", firstRowCell.Start.Column));
                columnCount++;
            }
            var startRow = hasHeader ? 2 : 1;
            for (int rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
            {
                try
                {
                    ExcelRange wsRow = worksheet.Cells[rowNum, 1, rowNum, columnCount];
                    DataRow row = dt.NewRow();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                    dt.Rows.Add(row);
                }
                catch(Exception ex)
                {

                }
            }
            return dt;
        }




        private static string GetString(object obj)
        {
            try
            {
                return obj.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static T GetEntity<T>(DataTable table) where T : new()
        {
            T entity = new T();
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (row.Table.Columns.Contains(item.Name))
                    {
                        if (DBNull.Value != row[item.Name])
                        {
                            item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                        }

                    }
                }
            }

            return entity;
        }

        public static IList<T> GetEntities<T>(DataTable table) where T : new()
        {
            IList<T> entities = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (var item in entity.GetType().GetProperties())
                {
                    item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                }
                entities.Add(entity);
            }
            return entities;
        }


        public static IList<T> GetEntities<T>(string fullFielPath) where T : new()
        {
            DataTable table = WorksheetToTable(fullFielPath);
            IList<T> entities = new List<T>();
            T entityTemplete = new T();
            IList<PropertyInfo> propertyList = new List<PropertyInfo>();

            foreach (var item in entityTemplete.GetType().GetProperties())
            {
                if (table.Columns.Contains(item.Name))
                {
                    propertyList.Add(item);
                }
            }

            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (var item in propertyList)
                {
                    item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                }
                entities.Add(entity);
            }

            return entities;
        }
    }
}
