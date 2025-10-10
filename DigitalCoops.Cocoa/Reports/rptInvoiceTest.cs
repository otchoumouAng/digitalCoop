using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Tms2017.MVC;

/// <summary>
/// Summary description for rptInvoice
/// </summary>
public class rptInvoiceTest : DevExpress.XtraReports.UI.XtraReport
{
    private DetailBand Detail;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell GroupDesignation;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private TopMarginBand TopMargin;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel2;
    private BottomMarginBand BottomMargin;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private GroupHeaderBand GroupHeader1;
    private ReportFooterBand ReportFooter;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell6;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell9;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRLabel xrLabel5;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell5;
    private XRTable xrTable4;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private PageFooterBand PageFooter;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private PageHeaderBand PageHeader;
    private ReportHeaderBand ReportHeader;
    private XRControlStyle TmsGroupHeaderStyle;
    private XRControlStyle TmsGroupFooterStyle;
    private XRLabel xrLabel1;
    private XRLabel xrLabel8;
    private XRLabel GroupTotal;
    private GroupFooterBand GroupFooter2;
    private DevExpress.XtraReports.Parameters.Parameter ID;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource2;
    private CalculatedField SousTotal;
    private CalculatedField Total;
    private GroupHeaderBand GroupHeader3;
    private GroupFooterBand GroupFooter3;
    private DevExpress.XtraReports.Parameters.Parameter IsCopy;
    private XRTableCell xrTableCell25;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptInvoiceTest()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptInvoiceTest));
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.GroupDesignation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.TmsGroupHeaderStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TmsGroupFooterStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.ID = new DevExpress.XtraReports.Parameters.Parameter();
            this.sqlDataSource2 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.SousTotal = new DevExpress.XtraReports.UI.CalculatedField();
            this.Total = new DevExpress.XtraReports.UI.CalculatedField();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooter3 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.IsCopy = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 24.16667F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("DtOrder", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable2.BorderWidth = 0.5F;
            this.xrTable2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(666.9996F, 24.16667F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseBorderWidth = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.GroupDesignation,
            this.xrTableCell24,
            this.xrTableCell1,
            this.xrTableCell5,
            this.xrTableCell25,
            this.xrTableCell2});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // GroupDesignation
            // 
            this.GroupDesignation.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.Designation")});
            this.GroupDesignation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupDesignation.Name = "GroupDesignation";
            this.GroupDesignation.StylePriority.UseFont = false;
            xrSummary1.FormatString = "Date/Heure P{0}";
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.IgnoreNullValues = true;
            this.GroupDesignation.Summary = xrSummary1;
            this.GroupDesignation.Weight = 0.87270695118662045D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.Reference")});
            this.xrTableCell24.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.Text = "xrTableCell24";
            this.xrTableCell24.Weight = 0.37729311015923828D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.Quantity", "{0:#,#}")});
            this.xrTableCell1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 0.40048773281279382D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.PrixTaux", "{0:0.00}")});
            this.xrTableCell5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 0.3674555996093784D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.SignDesignation")});
            this.xrTableCell25.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.Weight = 0.059601023847865642D;
            this.xrTableCell25.WordWrap = false;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.Montant", "{0:#,#}")});
            this.xrTableCell2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            xrSummary2.FormatString = "{0:#,#}";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            this.xrTableCell2.Summary = xrSummary2;
            this.xrTableCell2.Weight = 0.37338301396465223D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrPictureBox1,
            this.xrLabel2});
            this.TopMargin.HeightF = 125F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.AdresseSociete")});
            this.xrLabel1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(171.7268F, 79.00004F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(127.5601F, 22.99999F);
            this.xrLabel1.StylePriority.UseFont = false;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50.83333F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(171.7268F, 74.16667F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBox1_BeforePrint);
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.TelSociete")});
            this.xrLabel2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(171.7268F, 102F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(127.5601F, 22.99999F);
            this.xrLabel2.StylePriority.UseFont = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel3});
            this.ReportHeader.HeightF = 77.09659F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.Numero")});
            this.xrLabel4.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(237.5F, 16.90587F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(273.5106F, 50.19073F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "1000";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 16.90587F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(237.5F, 50.19073F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "FACTURE N° : ";
            // 
            // xrTable3
            // 
            this.xrTable3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 94.83358F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow5,
            this.xrTableRow4});
            this.xrTable3.SizeF = new System.Drawing.SizeF(666.9999F, 75F);
            this.xrTable3.StylePriority.UseFont = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell10,
            this.xrTableCell9});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell7.StylePriority.UsePadding = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Delivery date :";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell7.Weight = 0.63767350153306723D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.DateLivraison", "{0:g}")});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.Weight = 2.0559805597663532D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell10.StylePriority.UsePadding = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "Vehicule No :";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell10.Weight = 0.69697093870057925D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.Immatriculation")});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.Weight = 0.609375D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell15.StylePriority.UsePadding = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "Type De Livraison :";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell15.Weight = 0.63767350153306723D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.TypeLivraisonNom")});
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Text = "xrTableCell16";
            this.xrTableCell16.Weight = 2.0559805597663532D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell17.StylePriority.UsePadding = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.Text = "Poids Net (kg) :";
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell17.Weight = 0.69697093870057925D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.PoidsNet", "{0:#,#}")});
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.Text = "xrTableCell18";
            this.xrTableCell18.Weight = 0.609375D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell11.StylePriority.UsePadding = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Delivery No :";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell11.Weight = 0.63767350153306723D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.LIvraisonNumero")});
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "xrTableCell12";
            this.xrTableCell12.Weight = 2.0559805597663532D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell13.StylePriority.UsePadding = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell13.Weight = 0.69697093870057925D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Weight = 0.609375D;
            // 
            // xrLabel5
            // 
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.FournisseurNom")});
            this.xrLabel5.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 27.97618F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(667F, 37.69073F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "Fournisseur";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable4.SizeF = new System.Drawing.SizeF(667F, 25F);
            this.xrTable4.StyleName = "TmsGroupHeaderStyle";
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell22,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell21});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Text = "Designation";
            this.xrTableCell23.Weight = 2.375D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Text = "Reference";
            this.xrTableCell22.Weight = 1.0267721557617187D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Text = "Quantité (kg)";
            this.xrTableCell19.Weight = 1.0898950195312502D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Text = "Prix/Taux";
            this.xrTableCell20.Weight = 1.033332824707031D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Text = "Montant (CFA)";
            this.xrTableCell21.Weight = 1.1450000000000005D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.HeightF = 26.29627F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(340.177F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(326.8228F, 25F);
            this.xrTable1.StyleName = "TmsGroupFooterStyle";
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell6});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UsePadding = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Montant à payer au fournisseur :";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell3.Weight = 1.453795731239877D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.Total")});
            this.xrTableCell6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            xrSummary3.FormatString = "{0:#,#}";
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Page;
            this.xrTableCell6.Summary = xrSummary3;
            this.xrTableCell6.Weight = 0.655943908397544D;
            this.xrTableCell6.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.TotalInvoice_SummaryGetResult);
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrLabel6});
            this.PageFooter.HeightF = 100F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(536.8333F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "Visa Entité";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(149.2268F, 0F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "Visa Fournisseur";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrLabel5});
            this.PageHeader.HeightF = 198.1669F;
            this.PageHeader.Name = "PageHeader";
            // 
            // TmsGroupHeaderStyle
            // 
            this.TmsGroupHeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.TmsGroupHeaderStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.TmsGroupHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TmsGroupHeaderStyle.Name = "TmsGroupHeaderStyle";
            this.TmsGroupHeaderStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TmsGroupHeaderStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TmsGroupFooterStyle
            // 
            this.TmsGroupFooterStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(220)))), ((int)(((byte)(228)))));
            this.TmsGroupFooterStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(220)))), ((int)(((byte)(228)))));
            this.TmsGroupFooterStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TmsGroupFooterStyle.Name = "TmsGroupFooterStyle";
            this.TmsGroupFooterStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.GroupeDesignation")});
            this.xrLabel8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0.0002034505F, 0F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(666.9998F, 23F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Type Facture";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GroupTotal
            // 
            this.GroupTotal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Facture_Print.Montant")});
            this.GroupTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupTotal.LocationFloat = new DevExpress.Utils.PointFloat(565.3865F, 0F);
            this.GroupTotal.Name = "GroupTotal";
            this.GroupTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.GroupTotal.SizeF = new System.Drawing.SizeF(101.6132F, 23F);
            this.GroupTotal.StylePriority.UseBorders = false;
            this.GroupTotal.StylePriority.UseFont = false;
            this.GroupTotal.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:#,#}";
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.GroupTotal.Summary = xrSummary4;
            this.GroupTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.GroupTotal.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.Group_SummaryGetResult);
            this.GroupTotal.SummaryReset += new System.EventHandler(this.Group_SummaryReset);
            this.GroupTotal.SummaryRowChanged += new System.EventHandler(this.Group_SummaryRowChanged);
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupFooter2.HeightF = 25F;
            this.GroupFooter2.Level = 1;
            this.GroupFooter2.Name = "GroupFooter2";
            // 
            // ID
            // 
            this.ID.Name = "ID";
            this.ID.Type = typeof(System.Guid);
            this.ID.ValueInfo = "00000000-0000-0000-0000-000000000000";
            this.ID.Visible = false;
            // 
            // sqlDataSource2
            // 
            this.sqlDataSource2.ConnectionName = "TMS2017_ReportServer";
            this.sqlDataSource2.Name = "sqlDataSource2";
            storedProcQuery1.Name = "Facture_Print";
            queryParameter1.Name = "@ID";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.ID]", typeof(System.Guid));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.StoredProcName = "Facture_Print";
            this.sqlDataSource2.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource2.ResultSchemaSerializable = resources.GetString("sqlDataSource2.ResultSchemaSerializable");
            // 
            // SousTotal
            // 
            this.SousTotal.DataMember = "Facture_Print";
            this.SousTotal.Expression = "Sum([Montant])";
            this.SousTotal.Name = "SousTotal";
            // 
            // Total
            // 
            this.Total.DataMember = "Facture_Print";
            this.Total.Name = "Total";
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8});
            this.GroupHeader3.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("GroupeDesignation", DevExpress.XtraReports.UI.XRColumnSortOrder.Descending)});
            this.GroupHeader3.HeightF = 23F;
            this.GroupHeader3.Name = "GroupHeader3";
            // 
            // GroupFooter3
            // 
            this.GroupFooter3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.GroupFooter3.BorderWidth = 0.5F;
            this.GroupFooter3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.GroupTotal});
            this.GroupFooter3.HeightF = 23.33333F;
            this.GroupFooter3.Name = "GroupFooter3";
            this.GroupFooter3.StylePriority.UseBorders = false;
            this.GroupFooter3.StylePriority.UseBorderWidth = false;
            // 
            // IsCopy
            // 
            this.IsCopy.Description = "Report Is Copy";
            this.IsCopy.Name = "IsCopy";
            this.IsCopy.Type = typeof(bool);
            this.IsCopy.ValueInfo = "False";
            this.IsCopy.Visible = false;
            // 
            // rptInvoiceTest
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1,
            this.ReportFooter,
            this.PageFooter,
            this.PageHeader,
            this.GroupFooter2,
            this.GroupHeader3,
            this.GroupFooter3});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.SousTotal,
            this.Total});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource2});
            this.DataMember = "Facture_Print";
            this.DataSource = this.sqlDataSource2;
            this.Margins = new System.Drawing.Printing.Margins(80, 80, 125, 0);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.ID,
            this.IsCopy});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.TmsGroupHeaderStyle,
            this.TmsGroupFooterStyle});
            this.Version = "17.1";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SetWaterMark);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    double total = 0;
    double TotalVal = 0;
    double TotalDed = 0;

    private void SetWaterMark(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if ((bool)this.IsCopy.Value)        
            ReportWaterMark.SetTextWatermark((XtraReport)sender);
    }   

    //private void Group_SummaryCalculated(object sender, TextFormatEventArgs e)
    //{
    //    if (GetCurrentColumnValue("GroupDesignation").ToString() == "Valorizations")        
    //        TotalVal = Convert.ToDouble(e.Value);
    //    else
    //        TotalDed = Convert.ToDouble(e.Value);
    //}

    private void TotalInvoice_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        //Dans notre cas Faire la difference entre le total Valorisation et le total Dedauction
        // Affecter Le Total Facture
        e.Result = TotalVal - TotalDed;
        e.Handled = true;
    }

    private void Group_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {
        //Affecter le resultat trouvé apres avoir calculé la somme d'un groupe
        e.Result = total;
        e.Handled = true;
    }

    private void Group_SummaryReset(object sender, EventArgs e)
    {
        //Reinitialiser le Total d'un groupe avant de faire la somme de l'autre groupe
        total = 0;
    }

    private void Group_SummaryRowChanged(object sender, EventArgs e)
    {
        //Faire La somme des valeurs d'un Groupe
        if (GetCurrentColumnValue("GroupeDesignation").ToString() == "Valorizations" || GetCurrentColumnValue("GroupeDesignation").ToString() == "Valorisations")
        {
            TotalVal += Convert.ToDouble(GetCurrentColumnValue("Montant"));
            total = TotalVal;
        }
        else {
            TotalDed += Convert.ToDouble(GetCurrentColumnValue("Montant"));
            total = TotalDed;
        }
    }

    private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrPictureBox1.ImageUrl = "~\\Images\\Logo\\logo.jpg";
    }
}
