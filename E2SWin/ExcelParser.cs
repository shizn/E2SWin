using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text.RegularExpressions;

namespace E2SWin
{
    public class ExcelParser
    {
        public static SharedStringItem GetSharedStringItemById(WorkbookPart workbookPart, int id)
        {
            return workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
        }

        public static List<SheetDataInfo> parseXlsx(string path, int startLine)
        {
            List<SheetDataInfo> result = new List<SheetDataInfo>();

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(path, false))
            {
                // 对于每张sheet 读取sheet的名称并存入结果
                //DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.Sheets;
                //foreach (DocumentFormat.OpenXml.OpenXmlElement sheet in sheets)
                //{
                //    result.Add(new SheetDataInfo());
                //    result.Last().sheetName = GetSheetName(sheet);
                //}

                // 对于每张sheet 读取表头并存入结果
                var sheetslist = spreadsheetDocument.WorkbookPart.Workbook.Sheets.Cast<Sheet>().ToList();
                foreach (WorksheetPart w in spreadsheetDocument.WorkbookPart.WorksheetParts)
                {
                    string partRelationshipId = spreadsheetDocument.WorkbookPart.GetIdOfPart(w);
                    var correspondingSheet = sheetslist.FirstOrDefault(s => s.Id.HasValue && s.Id.Value == partRelationshipId);
                    // 读取sheet的名称并存入结果
                    result.Add(new SheetDataInfo());
                    result.Last().sheetName = correspondingSheet.Name;
                    // 获得对应sheet名的工作表
                    SheetData sheetData = w.Worksheet.Elements<SheetData>().First();
                    // 读取表头并存入结果
                    foreach (Row r in sheetData.Elements<Row>().Take(1))
                    {
                        foreach (Cell cell in r.Elements<Cell>())
                        {
                            string cellValue = string.Empty;
                            if (cell.DataType != null)
                            {
                                if (cell.DataType == CellValues.SharedString)
                                {
                                    SharedStringItem item = GetSharedStringItemById(spreadsheetDocument.WorkbookPart, Int32.Parse(cell.InnerText));
                                    cellValue = item.Text.Text ?? item.InnerText ?? item.InnerXml;
                                }
                                else
                                {
                                    throw new NotImplementedException();
                                }
                            }
                            else
                            {
                                cellValue = cell.CellValue.Text;
                            }
                            result.Last().contentRow.Add(cellValue);
                        }
                    }
                    // 读取汉字描述并存入结果
                    foreach (Row r in sheetData.Elements<Row>().Skip(1).Take(1))
                    {
                        foreach (Cell cell in r.Elements<Cell>())
                        {
                            string cellValue = string.Empty;
                            if (cell.DataType != null)
                            {
                                if (cell.DataType == CellValues.SharedString)
                                {
                                    SharedStringItem item = GetSharedStringItemById(spreadsheetDocument.WorkbookPart, Int32.Parse(cell.InnerText));
                                    cellValue = item.Text.Text ?? item.InnerText ?? item.InnerXml;
                                }
                                else
                                {
                                    throw new NotImplementedException();
                                }
                            }
                            else
                            {
                                cellValue = cell.CellValue.Text;
                            }
                            result.Last().chsDescription.Add(cellValue);
                        }
                    }
                    // 读取实际内容部分
                    foreach (Row r in sheetData.Elements<Row>().Skip(startLine))
                    {
                        result.Last().tableData.Add(new List<string>());
                        foreach (Cell cell in r.Elements<Cell>())
                        {
                            string cellValue = string.Empty;
                            if (cell.DataType != null)
                            {
                                if (cell.DataType == CellValues.SharedString)
                                {
                                    SharedStringItem item = GetSharedStringItemById(spreadsheetDocument.WorkbookPart, Int32.Parse(cell.InnerText));
                                    cellValue = item.Text.Text ?? item.InnerText ?? item.InnerXml;
                                }
                                else if (cell.DataType == CellValues.String)
                                {
                                    cellValue = cell.CellValue.Text;
                                }
                                else
                                {
                                    // 未被支持的单元格内容格式
                                    throw new NotImplementedException();
                                }
                            }
                            else
                            {
                                cellValue = cell.CellValue.Text;
                            }
                            result.Last().tableData.Last().Add(cellValue);
                        }
                    }
                }
            }
            return result;
        }


        public static string GetSheetName(DocumentFormat.OpenXml.OpenXmlElement sheet)
        {
            DocumentFormat.OpenXml.OpenXmlAttribute attr = sheet.GetAttribute("name", "");
            return attr.Value;
        }

        public static void GetSheetInfo(string fileName)
        {
            // Open file as read-only.
            using (SpreadsheetDocument mySpreadsheet = SpreadsheetDocument.Open(fileName, false))
            {
                DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = mySpreadsheet.WorkbookPart.Workbook.Sheets;

                // For each sheet, display the sheet information.
                foreach (DocumentFormat.OpenXml.OpenXmlElement sheet in sheets)
                {
                    foreach (DocumentFormat.OpenXml.OpenXmlAttribute attr in sheet.GetAttributes())
                    {
                        Console.WriteLine("{0}: {1}", attr.LocalName, attr.Value);
                    }
                }
            }
        }



        // Given a document name, a worksheet name, and a cell name, gets the column of the cell and returns
        // the content of the first cell in that column.
        public static string GetColumnHeading(string docName, string worksheetName, string cellName)
        {
            // Open the document as read-only.
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(docName, false))
            {
                IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == worksheetName);
                if (sheets.Count() == 0)
                {
                    // The specified worksheet does not exist.
                    return null;
                }

                WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);

                // Get the column name for the specified cell.
                string columnName = GetColumnName(cellName);

                // Get the cells in the specified column and order them by row.
                IEnumerable<Cell> cells = worksheetPart.Worksheet.Descendants<Cell>().Where(c => string.Compare(GetColumnName(c.CellReference.Value), columnName, true) == 0)
                    .OrderBy(r => GetRowIndex(r.CellReference));

                if (cells.Count() == 0)
                {
                    // The specified column does not exist.
                    return null;
                }

                // Get the first cell in the column.
                Cell headCell = cells.First();

                // If the content of the first cell is stored as a shared string, get the text of the first cell
                // from the SharedStringTablePart and return it. Otherwise, return the string value of the cell.
                if (headCell.DataType != null && headCell.DataType.Value == CellValues.SharedString)
                {
                    SharedStringTablePart shareStringPart = document.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    SharedStringItem[] items = shareStringPart.SharedStringTable.Elements<SharedStringItem>().ToArray();
                    return items[int.Parse(headCell.CellValue.Text)].InnerText;
                }
                else
                {
                    return headCell.CellValue.Text;
                }
            }
        }
        // Given a cell name, parses the specified cell to get the column name.
        private static string GetColumnName(string cellName)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellName);

            return match.Value;
        }

        // Given a cell name, parses the specified cell to get the row index.
        private static uint GetRowIndex(string cellName)
        {
            // Create a regular expression to match the row index portion the cell name.
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(cellName);

            return uint.Parse(match.Value);
        }
    }

}
