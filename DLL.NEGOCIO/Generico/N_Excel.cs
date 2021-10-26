using DLL.NEGOCIO.Generico.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace DLL.NEGOCIO.Generico
{
    public class N_Excel : I_N_Excel
    {
        public DataTable LecturaExcelTablaNormal(DataTable dt, WorkbookPart workbookPart, Sheet hojaExcel)
        {
            int contNumCells = 0;
            int maxNumCells = dt.Columns.Count;
            DataRow dr = dt.NewRow();

            WorksheetPart worksheetPart = workbookPart.GetPartById(hojaExcel.Id) as WorksheetPart;
            OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);

            var cellFormats = workbookPart.WorkbookStylesPart.Stylesheet.CellFormats;
            var numberingFormats = workbookPart.WorkbookStylesPart.Stylesheet.NumberingFormats;

            var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

            bool comienzaLectura = false;

            try
            {

                while (reader.Read())
                {
                    if (reader.ElementType == typeof(Cell))
                    {
                        Cell celda = (Cell)reader.LoadCurrentElement();
                        if (celda.CellReference == "A1")
                        {
                            comienzaLectura = true;
                        }

                        if (comienzaLectura)
                        {
                            // En caso de que el while no pase por una celda porque esta no tiene valor
                            // se agrega valor vacío en la celda del datatable
                            int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(celda.CellReference));

                            if (contNumCells > cellColumnIndex && contNumCells < maxNumCells)
                            {
                                do
                                {
                                    dr[contNumCells] = "";
                                    contNumCells++;
                                }
                                while (contNumCells < maxNumCells);

                                dt.Rows.Add(dr);
                                contNumCells = 0;
                                dr = dt.NewRow();
                            }

                            if (cellColumnIndex < maxNumCells)
                            {
                                if (contNumCells < cellColumnIndex)
                                {
                                    do
                                    {
                                        dr[contNumCells] = "";
                                        contNumCells++;
                                    }
                                    while (contNumCells < cellColumnIndex);
                                }

                                string value = null;

                                if (celda != null)
                                {
                                    value = celda.InnerText;
                                    if (celda.DataType != null)
                                    {
                                        switch (celda.DataType.Value)
                                        {
                                            case CellValues.SharedString:
                                                if (stringTable != null)
                                                {
                                                    value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                                                }
                                                break;
                                            case CellValues.Boolean:
                                                switch (value)
                                                {
                                                    case "0":
                                                        value = "FALSE";
                                                        break;
                                                    default:
                                                        value = "TRUE";
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        /*
                                        0 = 'General';
                                        1 = '0';
                                        2 = '0.00';
                                        3 = '#,##0';
                                        4 = '#,##0.00';
                                        5 = '$#,##0;\-$#,##0';
                                        6 = '$#,##0;[Red]\-$#,##0';
                                        7 = '$#,##0.00;\-$#,##0.00';
                                        8 = '$#,##0.00;[Red]\-$#,##0.00';
                                        9 = '0%';
                                        10 = '0.00%';
                                        11 = '0.00E+00';
                                        12 = '# ?/?';
                                        13 = '# ??/??';
                                        14 = 'mm-dd-yy';
                                        15 = 'd-mmm-yy';
                                        16 = 'd-mmm';
                                        17 = 'mmm-yy';
                                        18 = 'h:mm AM/PM';
                                        19 = 'h:mm:ss AM/PM';
                                        20 = 'h:mm';
                                        21 = 'h:mm:ss';
                                        22 = 'm/d/yy h:mm';

                                        37 = '#,##0 ;(#,##0)';
                                        38 = '#,##0 ;[Red](#,##0)';
                                        39 = '#,##0.00;(#,##0.00)';
                                        40 = '#,##0.00;[Red](#,##0.00)';

                                        44 = '_("$"* #,##0.00_);_("$"* \(#,##0.00\);_("$"* "-"??_);_(@_)';
                                        45 = 'mm:ss';
                                        46 = '[h]:mm:ss';
                                        47 = 'mmss.0';
                                        48 = '##0.0E+0';
                                        49 = '@';

                                        27 = '[$-404]e/m/d';
                                        30 = 'm/d/yy';
                                        36 = '[$-404]e/m/d';
                                        50 = '[$-404]e/m/d';
                                        57 = '[$-404]e/m/d';

                                        59 = 't0';
                                        60 = 't0.00';
                                        61 = 't#,##0';
                                        62 = 't#,##0.00';
                                        67 = 't0%';
                                        68 = 't0.00%';
                                        69 = 't# ?/?';
                                        70 = 't# ??/??';
                                         */

                                        if (celda.StyleIndex == null)
                                        {
                                            value = celda.InnerText;
                                        }
                                        else
                                        {
                                            var styleIndex = (int)celda.StyleIndex.Value;
                                            var cellFormat = (CellFormat)cellFormats.ElementAt(styleIndex);
                                            if (cellFormat.NumberFormatId != null)
                                            {
                                                var numberFormatId = cellFormat.NumberFormatId.Value;

                                                switch (numberFormatId)
                                                {
                                                    case 0:
                                                        value = celda.InnerText;
                                                        break;
                                                    case 14:
                                                    case 15:
                                                    case 16:
                                                    case 17:
                                                    case 22:
                                                        if (!string.IsNullOrEmpty(value))
                                                        {
                                                            value = DateTime.FromOADate(double.Parse(value)).ToShortDateString();
                                                        }
                                                        break;
                                                    case 18:
                                                    case 19:
                                                    case 20:
                                                    case 21:
                                                    case 45:
                                                    case 46:
                                                    case 47:
                                                    case 164:
                                                    case 165:
                                                        value = celda.InnerText;
                                                        if (value != "")
                                                        {
                                                            int indexE = value.LastIndexOf('E');
                                                            string cleanString;
                                                            if (indexE > 0)
                                                                cleanString = value.Substring(0, indexE);
                                                            else
                                                                cleanString = value;

                                                            var clone = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                                                            clone.NumberFormat.NumberDecimalSeparator = ".";
                                                            clone.NumberFormat.NumberGroupSeparator = ",";

                                                            double dec = double.Parse(cleanString, clone);
                                                            double fromDays;

                                                            if (dec >= 1)
                                                                fromDays = (dec / 100);
                                                            else
                                                                fromDays = dec;

                                                            TimeSpan hora = TimeSpan.FromDays(fromDays);
                                                            value = hora.ToString(@"hh\:mm\:ss");
                                                        }
                                                        break;
                                                    default:
                                                        value = celda.InnerText;
                                                        //var numberingFormat = numberingFormats.Cast<NumberingFormat>()
                                                        //    .Single(f => f.NumberFormatId.Value == numberFormatId);

                                                        //// Here's yer string! Example: $#,##0.00_);[Red]($#,##0.00)
                                                        //string formatString = numberingFormat.FormatCode.Value;
                                                        break;
                                                }

                                            }
                                        }
                                    }
                                }


                                dr[contNumCells] = value;
                                contNumCells += 1;

                                if (contNumCells == maxNumCells)
                                {
                                    dt.Rows.Add(dr);
                                    contNumCells = 0;
                                    dr = dt.NewRow();
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }


            return dt;
        }

        public Stylesheet GenerateStylesheet()
        {
            Stylesheet styleSheet = null;

            Fonts fonts = new Fonts(
                new Font( // Index 0 - default
                    new FontSize() { Val = 10 }
                ),
                new Font( // Index 1 - header
                    new FontSize() { Val = 10 },
                    new Bold(),
                    new Color() { Rgb = "000000" }

                ));

            Fills fills = new Fills(
                    new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0 - default
                    new Fill(new PatternFill() { PatternType = PatternValues.Gray125 }), // Index 1 - default
                    new Fill(new PatternFill(new ForegroundColor { Rgb = "CCCCCC" })
                    { PatternType = PatternValues.Solid }) // Index 2 - header
                );

            Borders borders = new Borders(
                    new Border(), // index 0 default
                    new Border( // index 1 black border
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder()),
                    new Border( // index 2 black border
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder()),
                    new Border( // index 3 black border
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },                        
                        new DiagonalBorder()),
                    new Border( // index 4 black border
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder()),
                    new Border( // index 5 black border
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                );

            CellFormats cellFormats = new CellFormats(
                    /*0*/new CellFormat(), // default
                    /*1*/new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }, // body
                    /*2*/new CellFormat { FontId = 1, FillId = 2, BorderId = 1, ApplyFill = true, Alignment = new Alignment() { WrapText = true, Vertical = VerticalAlignmentValues.Center, Horizontal = HorizontalAlignmentValues.Center } }, // header
                    /*3*/new CellFormat { FontId = 1, FillId = 2, BorderId = 2 },
                    /*4*/new CellFormat { FontId = 1, FillId = 0, BorderId = 1 },
                    /*5*/new CellFormat { FontId = 1, FillId = 0, BorderId = 0 },
                    /*6*/new CellFormat { FontId = 0, FillId = 0, BorderId = 3, ApplyFill = true, Alignment = new Alignment() { WrapText = true, Vertical = VerticalAlignmentValues.Center, Horizontal = HorizontalAlignmentValues.Center } },
                    /*7*/new CellFormat { FontId = 0, FillId = 0, BorderId = 4, ApplyFill = true, Alignment = new Alignment() { WrapText = true, Vertical = VerticalAlignmentValues.Center, Horizontal = HorizontalAlignmentValues.Center } },
                    /*8*/new CellFormat { FontId = 0, FillId = 0, BorderId = 5, ApplyFill = true, Alignment = new Alignment() { WrapText = true, Vertical = VerticalAlignmentValues.Center, Horizontal = HorizontalAlignmentValues.Center } },
                    /*9*/new CellFormat { FontId = 0, FillId = 0, BorderId = 5, ApplyFill = true }
                );

            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);

            return styleSheet;
        }

        public Cell ConstructCell(string value, CellValues dataType, string cellReference, uint styleIndex = 0)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
                StyleIndex = styleIndex,
                CellReference = cellReference
            };
        }

        private static List<char> Letters = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ' };
        /// <summary>
        /// Given a cell name, parses the specified cell to get the column name.
        /// </summary>
        /// <param name="cellReference">Address of the cell (ie. B2)</param>
        /// <returns>Column Name (ie. B)</returns>
        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);

            return match.Value;
        }

        /// <summary>
        /// Given just the column name (no row index), it will return the zero based column index.
        /// Note: This method will only handle columns with a length of up to two (ie. A to Z and AA to ZZ). 
        /// A length of three can be implemented when needed.
        /// </summary>
        /// <param name="columnName">Column Name (ie. A or AB)</param>
        /// <returns>Zero based index if the conversion was successful; otherwise null</returns>
        public static int? GetColumnIndexFromName(string columnName)
        {
            int? columnIndex = null;

            string[] colLetters = Regex.Split(columnName, "([A-Z]+)");
            colLetters = colLetters.Where(s => !string.IsNullOrEmpty(s)).ToArray();

            if (colLetters.Count() <= 2)
            {
                int index = 0;
                foreach (string col in colLetters)
                {
                    List<char> col1 = colLetters.ElementAt(index).ToCharArray().ToList();
                    int? indexValue = Letters.IndexOf(col1.ElementAt(index));

                    if (indexValue != -1)
                    {
                        // The first letter of a two digit column needs some extra calculations
                        if (index == 0 && colLetters.Count() == 2)
                        {
                            columnIndex = columnIndex == null ? (indexValue + 1) * 26 : columnIndex + ((indexValue + 1) * 26);
                        }
                        else
                        {
                            columnIndex = columnIndex == null ? indexValue : columnIndex + indexValue;
                        }
                    }

                    index++;
                }
            }

            return columnIndex;
        }
    
    }
}
