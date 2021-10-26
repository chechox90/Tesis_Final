using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;

namespace DLL.NEGOCIO.Generico.Interfaces
{
    public interface I_N_Excel
    {
        DataTable LecturaExcelTablaNormal(DataTable dt, WorkbookPart workbookPart, Sheet hojaExcel);

        Stylesheet GenerateStylesheet();

        Cell ConstructCell(string value, CellValues dataType, string cellReference, uint styleIndex = 0);
    }
}
