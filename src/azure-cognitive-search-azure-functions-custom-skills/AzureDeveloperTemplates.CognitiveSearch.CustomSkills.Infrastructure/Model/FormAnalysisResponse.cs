using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Model
{
    public class FormAnalysisResponse
    {
        public string status { get; set; }
        public DateTime createdDateTime { get; set; }
        public DateTime lastUpdatedDateTime { get; set; }
        public AnalyzeResult analyzeResult { get; set; }
    }

    public class AnalyzeResult
    {
        public string version { get; set; }
        public List<ReadResult> readResults { get; set; }
        public List<PageResult> pageResults { get; set; }
        public List<DocumentResult> documentResults { get; set; }
        public List<object> errors { get; set; }
    }

    public class ReadResult
    {
        public int page { get; set; }
        public string language { get; set; }
        public double angle { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public string unit { get; set; }
    }

    public class PageResult
    {
        public int page { get; set; }
        public List<Table> tables { get; set; }
    }

    public class Table
    {
        public int rows { get; set; }
        public int columns { get; set; }
        public List<Cell> cells { get; set; }
    }

    public class Cell
    {
        public int rowIndex { get; set; }
        public int columnIndex { get; set; }
        public string text { get; set; }
        public List<double> boundingBox { get; set; }
        public int? columnSpan { get; set; }
        public int? rowSpan { get; set; }
    }



    public class DocumentResult
    {
        public string docType { get; set; }
        public List<int> pageRange { get; set; }
        public Fields fields { get; set; }
    }

    public class Fields
    {
        public Charges Charges { get; set; }
        public ForCompany ForCompany { get; set; }
        public FromCompany FromCompany { get; set; }
        public InvoiceDate InvoiceDate { get; set; }
        public InvoiceDueDate InvoiceDueDate { get; set; }
        public InvoiceNumber InvoiceNumber { get; set; }
        public VatID VatID { get; set; }
    }

    public class Charges :BaseField
    {
        public string fieldName => "Charges";
    }

    public class ForCompany : BaseField
    {
        public string fieldName => "ForCompany";
    }

    public class FromCompany : BaseField
    {
        public string fieldName => "FromCompany";
    }

    public class InvoiceDate : BaseField
    {
        public string fieldName => "InvoiceDate";
    }

    public class InvoiceDueDate : BaseField
    {
        public string fieldName => "InvoiceDueDate";
    }

    public class InvoiceNumber : BaseField
    {
        public string fieldName => "InvoiceNumber";
    }

    public class VatID : BaseField
    {
        public string fieldName => "VatID";
    }

    public abstract class BaseField
    {
        public string type { get; set; }
        public string valueString { get; set; }
        public string text { get; set; }
        public int page { get; set; }
        public List<double> boundingBox { get; set; }
        public double confidence { get; set; }
    }
}
