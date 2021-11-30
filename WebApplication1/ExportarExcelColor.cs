using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace ConductorEnRed
{
    public class ExportarExcelColor
    {
        public static bool CreateExcelDocument<T>(List<T> list, string xlsxFilePath)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(ListToDataTable(list));

            return CreateExcelDocument(ds, xlsxFilePath);
        }
        #region HELPER_FUNCTIONS
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (!IsNullableType(info.PropertyType))
                        row[info.Name] = info.GetValue(t, null);
                    else
                        row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        private static Type GetNullableType(Type t)
        {
            Type returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType;
        }
        private static bool IsNullableType(Type type)
        {
            return (type == typeof(string) ||
                    type.IsArray ||
                    (type.IsGenericType &&
                     type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
        }

        public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            bool result = CreateExcelDocument(ds, xlsxFilePath);
            ds.Tables.Remove(dt);
            return result;
        }
        #endregion

        //  End of "INCLUDE_WEB_FUNCTIONS" section

        /// <summary>
        /// Create an Excel file, and write it to a file.
        /// </summary>
        /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        /// <param name="excelFilename">Name of file to be written.</param>
        /// <returns>True if successful, false if something went wrong.</returns>
        public static bool CreateExcelDocument(DataSet ds, string excelFilename)
        {
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                {
                    WriteExcelFile(ds, document);
                }
                Trace.WriteLine("Successfully created: " + excelFilename);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        private static double GetWidth(string font, int fontSize, string text)
        {
            System.Drawing.Font stringFont = new System.Drawing.Font(font, fontSize);
            return GetWidth(stringFont, text);
        }

        private static double GetWidth(System.Drawing.Font stringFont, string text)
        {
            // This formula is based on this article plus a nudge ( + 0.2M )
            // http://msdn.microsoft.com/en-us/library/documentformat.openxml.spreadsheet.column.width.aspx
            // Truncate(((256 * Solve_For_This + Truncate(128 / 7)) / 256) * 7) = DeterminePixelsOfString

            System.Drawing.Size textSize = TextRenderer.MeasureText(text, stringFont);
            double width = (double)(((textSize.Width / (double)7) * 256) - (128 / 7)) / 256;
            width = (double)decimal.Round((decimal)width + 0.2M, 2);

            return width;
        }

        private static Column CreateColumnData(double ColumnWidth)
        {
            Column column;
            column = new Column();
            column.Min = 1;
            column.Max = 3;
            column.Width = 600;
            column.CustomWidth = true;
            return column;
        }

        private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet)
        {
            //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
            //  to a file, or writing to a MemoryStream.
            spreadsheet.AddWorkbookPart();
            spreadsheet.WorkbookPart.Workbook = new Workbook();





            //  My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
            spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

            //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
            WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
            Stylesheet stylesheet = new Stylesheet();

            workbookStylesPart.Stylesheet = GenerateStyleSheet();
            workbookStylesPart.Stylesheet.Save();

            //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;
            foreach (DataTable dt in ds.Tables)
            {
                //creamos objeto columnas para personalizar width de las columnas
                Columns columns = new Columns();
                int cantColumns = dt.Columns.Count;

                //recorremos las columnas y filas
                for (int i = 0; i < cantColumns; i++)
                {
                    double width = 0;
                    string columna = dt.Columns[i].ToString();
                    width = GetWidth("Calibri", 11, columna);
                    //recorremos las celdas, si se encuentra un valor mayor se reemplaza
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string fila = dt.Rows[j][i].ToString();
                        double auxWidth = GetWidth("Calibri", 11, fila);

                        if (width == 0)
                        {
                            width = auxWidth;
                        }
                        else
                        {
                            if (auxWidth > width)
                            {
                                width = auxWidth;
                            }
                        }
                    }
                    //agregamos la nueva configuracion para columnas de la hoja de excel
                    columns.Append(new Column() { Min = (uint)(i + 1), Max = (uint)(i + 1), Width = width, CustomWidth = true });
                }

                //  For each worksheet you want to create
                string workSheetID = "rId" + worksheetNumber.ToString();
                string worksheetName = dt.TableName;

                WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new Worksheet();
                //agregamos la configuracion para las columnas de la hoja de excel
                newWorksheetPart.Worksheet.Append(columns);

                // create sheet data
                newWorksheetPart.Worksheet.AppendChild(new SheetData());

                // save worksheet
                if (dt.TableName == "Seguimiento Mensual")
                {
                    WriteDataTableToExcelWorksheet(dt, newWorksheetPart);
                }
                else if (dt.TableName == "Eventos por periodo")
                {
                    WriteDataTableToExcelWorksheetEventosPeriodo(dt, newWorksheetPart);
                }
                else
                {
                    WriteDataTableToExcelWorksheetReportePlanillas(dt, newWorksheetPart);
                }

                newWorksheetPart.Worksheet.Save();

                // create the worksheet to workbook relation
                if (worksheetNumber == 1)
                    spreadsheet.WorkbookPart.Workbook.AppendChild(new Sheets());

                spreadsheet.WorkbookPart.Workbook.GetFirstChild<Sheets>().AppendChild(new Sheet()
                {
                    Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart),
                    SheetId = (uint)worksheetNumber,
                    Name = dt.TableName
                });

                worksheetNumber++;
            }

            spreadsheet.WorkbookPart.Workbook.Save();
        }
        private static void WriteDataTableToExcelWorksheetEventosPeriodo(DataTable dt, WorksheetPart worksheetPart)
        {
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();

            string cellValue = "";

            int numCol = 0;
            int diasGuardados = 0;
            uint indiceFormato = 0;

            var fecha1 = dt.Columns[1].ColumnName.ToString();
            var fecha2 = getObtenerPrimerDiaSemana(fecha1);
            numCol = (DateTime.Parse(fecha1) - fecha2.Date).Days;
            numCol = 7 - numCol;

            int numberOfColumns = dt.Columns.Count;
            bool[] IsNumericColumn = new bool[numberOfColumns];

            string[] excelColumnNames = new string[numberOfColumns];
            for (int n = 0; n < numberOfColumns; n++)
                excelColumnNames[n] = GetExcelColumnName(n);

            uint rowIndex = 1;
            var headerRow = new Row { RowIndex = rowIndex };
            sheetData.Append(headerRow);

            for (int colInx = 0; colInx < numberOfColumns; colInx++)
            {
                DataColumn col = dt.Columns[colInx];

                if (colInx == 0)
                {
                    AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), col.ColumnName, headerRow, 1);
                }
                else if (colInx <= numCol + diasGuardados)
                {
                    AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow, 7 + indiceFormato);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
                }

                if (colInx == (numCol + diasGuardados) & colInx > 0)
                {
                    diasGuardados = diasGuardados + 7;
                    if (diasGuardados >= 7)
                    {
                        indiceFormato = indiceFormato + 1;
                    }
                }

            }

            diasGuardados = 0;
            indiceFormato = 0;

            foreach (DataRow dr in dt.Rows)
            {
                ++rowIndex;
                var newExcelRow = new Row { RowIndex = rowIndex };
                sheetData.Append(newExcelRow);

                diasGuardados = 0;
                indiceFormato = 0;

                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    cellValue = dr.ItemArray[colInx].ToString();

                    if (colInx == 0)
                    {
                        AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 1);
                    }
                    else if (colInx <= numCol + diasGuardados)
                    {
                        AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, (7 + indiceFormato));
                    }

                    if (colInx == (numCol + diasGuardados) & colInx > 0)
                    {
                        diasGuardados = diasGuardados + 7;
                        if (diasGuardados >= 7)
                        {
                            indiceFormato = indiceFormato + 1;
                        }

                    }
                }
            }
        }

        private static DateTime getObtenerPrimerDiaSemana(string diaSemana)
        {
            DateTime primerDiaSemana = DateTime.Parse(diaSemana);
            while (primerDiaSemana.DayOfWeek != DayOfWeek.Monday)
                primerDiaSemana = primerDiaSemana.AddDays(-1);

            return primerDiaSemana;
        }

        private static void WriteDataTableToExcelWorksheetReportePlanillas(DataTable dt, WorksheetPart worksheetPart)
        {
            var worksheet = worksheetPart.Worksheet;

            var sheetData = worksheet.GetFirstChild<SheetData>();

            string cellValue = "";

            //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
            //
            //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
            //  cells of data, we'll know if to write Text values or Numeric cell values.
            int numberOfColumns = dt.Columns.Count;
            bool[] IsNumericColumn = new bool[numberOfColumns];

            string[] excelColumnNames = new string[numberOfColumns];
            for (int n = 0; n < numberOfColumns; n++)
                excelColumnNames[n] = GetExcelColumnName(n);
            //
            //  Create the Header row in our Excel Worksheet
            //
            uint rowIndex = 1;

            var headerRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
            sheetData.Append(headerRow);

            for (int colInx = 0; colInx < numberOfColumns; colInx++)
            {
                DataColumn col = dt.Columns[colInx];

                if (dt.TableName == "PLANILLAS REVISADAS")
                {
                    if (colInx == 3 || colInx == 5 || colInx == 7 || colInx == 9)
                    {
                        AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow, 14);
                    }
                    else
                    {
                        AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow, 1);
                    }
                }
                else
                {
                    AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow, 1);
                }
            }

            //
            //  Now, step through each row of data in our DataTable...
            //
            foreach (DataRow dr in dt.Rows)
            {
                // ...create a new row, and append a set of this row's data to it.
                ++rowIndex;
                var newExcelRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
                sheetData.Append(newExcelRow);

                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    cellValue = dr.ItemArray[colInx].ToString();
                    if (dt.TableName == "PLANILLAS REVISADAS")
                    {
                        if (colInx == 3 || colInx == 5 || colInx == 7 || colInx == 9)
                        {
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 13);
                        }
                        else
                        {
                            int i = 0;
                            if (int.TryParse(cellValue, out i))
                            {
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 0);
                            }
                            else
                            {
                                AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 0);
                            }
                        }
                    }
                    else
                    {
                        int i = 0;

                        if (int.TryParse(cellValue, out i))
                        {
                            AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 0);
                        }
                        else
                        {
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 0);
                        }

                    }
                }
            }
        }
        private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart)
        {
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();

            string cellValue = "";

            //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
            //
            //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
            //  cells of data, we'll know if to write Text values or Numeric cell values.
            int numberOfColumns = dt.Columns.Count;
            bool[] IsNumericColumn = new bool[numberOfColumns];

            string[] excelColumnNames = new string[numberOfColumns];
            for (int n = 0; n < numberOfColumns; n++)
                excelColumnNames[n] = GetExcelColumnName(n);

            //
            //  Create the Header row in our Excel Worksheet
            //
            uint rowIndex = 1;

            var headerRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
            sheetData.Append(headerRow);

            for (int colInx = 0; colInx < numberOfColumns; colInx++)
            {
                DataColumn col = dt.Columns[colInx];

                //columnas
                if (colInx >= 4 && colInx <= 20)
                {
                    AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow, 7);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
                }

                else if (colInx >= 21 && colInx <= 37)
                {
                    AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow, 8);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
                }

                else if (colInx >= 38 && colInx <= 54)
                {
                    AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow, 9);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
                }

                else if (colInx >= 55 && colInx <= 71)
                {
                    AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow, 10);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
                }

                else if (colInx >= 72 && colInx <= 88)
                {
                    AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow, 11);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
                }

                else if (colInx >= 89 && colInx <= 105)
                {
                    AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow, 12);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
                }

                else
                {
                    AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow, 1);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
                }
            }

            //
            //  Now, step through each row of data in our DataTable...
            //
            double cellNumericValue = 0;
            foreach (DataRow dr in dt.Rows)
            {
                // ...create a new row, and append a set of this row's data to it.
                ++rowIndex;
                var newExcelRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
                sheetData.Append(newExcelRow);

                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    cellValue = dr.ItemArray[colInx].ToString();

                    // Create cell with data
                    if (IsNumericColumn[colInx])
                    {
                        //  For numeric cells, make sure our input data IS a number, then write it out to the Excel file.
                        //  If this numeric value is NULL, then don't write anything to the Excel file.
                        cellNumericValue = 0;
                        if (double.TryParse(cellValue, out cellNumericValue))
                        {
                            cellValue = cellNumericValue.ToString();

                            if (colInx >= 4 && colInx <= 20)
                            {
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 15);
                            }

                            else if (colInx >= 21 && colInx <= 37)
                            {
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 16);
                            }

                            else if (colInx >= 38 && colInx <= 54)
                            {
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 17);
                            }

                            else if (colInx >= 55 && colInx <= 71)
                            {
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 18);
                            }

                            else if (colInx >= 72 && colInx <= 88)
                            {
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 19);
                            }

                            else if (colInx >= 89 && colInx <= 105)
                            {
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 20);
                            }

                            else
                            {
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 1);
                            }

                        }
                    }
                    else
                    {
                        if (colInx == 0)
                        {
                            AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 1);
                        }
                        else if (colInx >= 4 && colInx <= 20)
                        {
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 15);
                        }

                        else if (colInx >= 21 && colInx <= 37)
                        {
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 16);
                        }

                        else if (colInx >= 38 && colInx <= 54)
                        {
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 17);
                        }

                        else if (colInx >= 55 && colInx <= 71)
                        {
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 18);
                        }

                        else if (colInx >= 72 && colInx <= 88)
                        {
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 19);
                        }

                        else if (colInx >= 89 && colInx <= 105)
                        {
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 20);
                        }

                        else
                        {
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, 1);
                        }
                    }
                }
            }
        }

        private static void AppendTextCell(string cellReference, string cellStringValue, Row excelRow, uint style)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.String, StyleIndex = style };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static void AppendNumericCell(string cellReference, string cellStringValue, Row excelRow, uint style)
        {
            //  Add a new Excel Cell to our Row 
            //Agrega una celda a la fila
            Cell cell = new Cell() { CellReference = cellReference, StyleIndex = style };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            //  Convert a zero-based column index into an Excel column reference  (A, B, C.. Y, Y, AA, AB, AC... AY, AZ, B1, B2..)
            //
            //  eg  GetExcelColumnName(0) should return "A"
            //      GetExcelColumnName(1) should return "B"
            //      GetExcelColumnName(25) should return "Z"
            //      GetExcelColumnName(26) should return "AA"
            //      GetExcelColumnName(27) should return "AB"
            //      ..etc..
            //
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            char firstChar = (char)('A' + (columnIndex / 26) - 1);
            char secondChar = (char)('A' + (columnIndex % 26));

            return string.Format("{0}{1}", firstChar, secondChar);
        }


        public static Microsoft.Office.Interop.Excel.Workbook DeleteSheets(Microsoft.Office.Interop.Excel.Workbook workbook)
        {
            //Elimina todas las hojas del excel, menos la primera.
            int hoja = workbook.Sheets.Count;
            int numeroHojas = hoja;
            for (var x = 1; x < numeroHojas; x++)
            {
                workbook.Sheets[hoja].Delete();
                hoja--;
            }

            return workbook;
        }


        private static Stylesheet GenerateStyleSheet()
        {
            return new Stylesheet(
            new Fonts(
                        new Font( // Index 0 - The default font.
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" }),

                        new Font( // Index 1 - The bold font.
                        new Bold(),
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" }),

                        new Font( // Index 2 - The Italic font.
                        new Italic(),
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" }),

                        new Font( // Index 3 - The Times Roman font. with 16 size
                        new FontSize() { Val = 16 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),

                        new Font( // Index 4 - The bold font.
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "006100" } },
                        new FontName() { Val = "Calibri" }),

                        new Font( // Index 5 - The bold font.
                        new Bold(),
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "006100" } },
                        new FontName() { Val = "Calibri" })
            ),
            new Fills(
                        new Fill( // Index 0 - The default fill.
                        new PatternFill() { PatternType = PatternValues.None }),

                        new Fill( // Index 1 - The default fill of gray 125 (required)
                        new PatternFill() { PatternType = PatternValues.Gray125 }),

                        new Fill( // Index 2 - The yellow fill.
                        new PatternFill(
                        new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFFFFF00" } }
                        )
                        { PatternType = PatternValues.Solid }),


                        //PARA REPORTE DE SEGUIMIENTO
                        new Fill( // Index 3 - The yellow fill.
                        new PatternFill(
                        new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "B6FFF7" } }//A4FFF5//33FFE9
                        )
                        { PatternType = PatternValues.Solid }),

                        new Fill( // Index 4 - The yellow fill.
                        new PatternFill(
                        new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFE5A4" } }//FFDF90//FFC433
                        )
                        { PatternType = PatternValues.Solid }),

                        new Fill( // Index 5 - The yellow fill.
                        new PatternFill(
                        new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "DDFFA6" } }//D7FF96//B2FF33
                        )
                        { PatternType = PatternValues.Solid }),


                        new Fill( // Index 6 - The yellow fill.
                        new PatternFill(
                        new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "ADFBE1" } }
                        )
                        { PatternType = PatternValues.Solid }),


                        new Fill( // Index 7 - The yellow fill.
                        new PatternFill(
                        new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFF7A3" } }//FFF592//FFEC33
                        )
                        { PatternType = PatternValues.Solid }),

                         new Fill( // Index 8 - The yellow fill.
                        new PatternFill(
                        new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "A9E2F3" } }
                        )
                        { PatternType = PatternValues.Solid }),

                        //PARA REPORTE DE PLANILLAS, TABLA REVISADAS
                        new Fill( // Index 9 - The yellow fill.
                        new PatternFill(
                        new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "C6EFCE" } }
                        )
                        { PatternType = PatternValues.Solid })


                      ),
            new Borders(
                            new Border( // Index 0 - The default border.
                            new LeftBorder(),
                            new RightBorder(),
                            new TopBorder(),
                            new BottomBorder(),
                            new DiagonalBorder()),
                            new Border( // Index 1 - Applies a Left, Right, Top, Bottom border to a cell
                            new LeftBorder(
                            new Color() { Rgb = new HexBinaryValue("ff0000") }
                            )
                            { Style = BorderStyleValues.Thin },
                            new RightBorder(
                            new Color() { Rgb = new HexBinaryValue("ff0000") }
                            )
                            { Style = BorderStyleValues.Thin },
                            new TopBorder(
                            new Color() { Rgb = new HexBinaryValue("ff0000") }
                            )
                            { Style = BorderStyleValues.Thin },
                            new BottomBorder(
                            new Color() { Rgb = new HexBinaryValue("ff0000") }
                            )
                            { Style = BorderStyleValues.Thin },
                            new DiagonalBorder())
                         ),

            new CellFormats(
                    new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 }, // Index 0 - The default cell style. If a cell does not have a style index applied it will use this style combination instead
                    new CellFormat() { FontId = 1, FillId = 0, BorderId = 0, ApplyFont = true }, // Index 1 - Bold
                    new CellFormat() { FontId = 2, FillId = 0, BorderId = 0, ApplyFont = true }, // Index 2 - Italic
                    new CellFormat() { FontId = 3, FillId = 0, BorderId = 0, ApplyFont = true }, // Index 3 - Times Roman
                    new CellFormat() { FontId = 0, FillId = 2, BorderId = 0, ApplyFill = true }, // Index 4 - Yellow Fill
                    new CellFormat( // Index 5 - Alignment
                                    new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                                  )
                    { FontId = 0, FillId = 0, BorderId = 0, ApplyAlignment = true },
                    new CellFormat() { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }, // Index 6 - Border
                                                                                                   //nuevos formatos de celda para informe mensual de seguimiento
                    new CellFormat() { FontId = 1, FillId = 3, BorderId = 0, ApplyFill = true }, // Index 7 - Yellow Fill
                    new CellFormat() { FontId = 1, FillId = 4, BorderId = 0, ApplyFill = true }, // Index 8 - Yellow Fill
                    new CellFormat() { FontId = 1, FillId = 5, BorderId = 0, ApplyFill = true }, // Index 9 - Yellow Fill
                    new CellFormat() { FontId = 1, FillId = 6, BorderId = 0, ApplyFill = true }, // Index 10 - Yellow Fill
                    new CellFormat() { FontId = 1, FillId = 7, BorderId = 0, ApplyFill = true }, // Index 11 - Yellow Fill
                    new CellFormat() { FontId = 1, FillId = 8, BorderId = 0, ApplyFill = true }, // Index 12 - Yellow Fill
                    new CellFormat() { FontId = 4, FillId = 9, BorderId = 0, ApplyFill = true }, // Index 13 - green Fill data

                    new CellFormat() { FontId = 5, FillId = 9, BorderId = 0, ApplyFill = true }, // Index 14 - green Fill title

                    new CellFormat() { FontId = 0, FillId = 3, BorderId = 0, ApplyFill = true }, // Index 15 - Yellow Fill
                    new CellFormat() { FontId = 0, FillId = 4, BorderId = 0, ApplyFill = true }, // Index 16 - Yellow Fill
                    new CellFormat() { FontId = 0, FillId = 5, BorderId = 0, ApplyFill = true }, // Index 17 - Yellow Fill
                    new CellFormat() { FontId = 0, FillId = 6, BorderId = 0, ApplyFill = true }, // Index 18 - Yellow Fill
                    new CellFormat() { FontId = 0, FillId = 7, BorderId = 0, ApplyFill = true }, // Index 19 - Yellow Fill
                    new CellFormat() { FontId = 0, FillId = 8, BorderId = 0, ApplyFill = true }, // Index 20 - Yellow Fill
                    new CellFormat() { FontId = 0, FillId = 9, BorderId = 0, ApplyFill = true } // Index 21 - green Fill data
                )
            ); // return
        }
    }
}