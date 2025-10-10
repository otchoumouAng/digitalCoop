using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptDeliveriesReport
/// </summary>
public class rptDeliveriesReport : DevExpress.XtraReports.UI.XtraReport
{
    private DetailBand Detail;
    private TopMarginBand TopMargin;
    private XRLabel ReportTitle;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private ReportFooterBand ReportFooter;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    private DevExpress.DataAccess.Excel.ExcelDataSource excelDataSource1;
    private DevExpress.XtraReports.Parameters.Parameter paramCampagne;
    private DevExpress.XtraReports.Parameters.Parameter paramTypeLivraison;
    private DevExpress.XtraReports.Parameters.Parameter paramFournisseur;
    private DevExpress.XtraReports.Parameters.Parameter paramDateDebut;
    private DevExpress.XtraReports.Parameters.Parameter paramDateFin;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource2;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell14;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private DevExpress.XtraReports.Parameters.Parameter paramFournisseurText;
    private DevExpress.XtraReports.Parameters.Parameter paramTypeLivraisonText;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell45;
    private XRPageInfo xrPageInfo2;
    private XRPageInfo xrPageInfo1;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell27;
    private CalculatedField HiddenForeign;
    private CalculatedField HiddenBeansCount;
    private CalculatedField HiddenMoisture;
    private CalculatedField MoyPondBeans;
    private CalculatedField MoyPondForeign;
    private CalculatedField MoyPondMoisture;
    private CalculatedField NetAccepted;
    private DevExpress.XtraReports.Parameters.Parameter paramStatus;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell28;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel3;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell60;
    private DevExpress.XtraReports.Parameters.Parameter paramSiteText;
    private DevExpress.XtraReports.Parameters.Parameter paramSite;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptDeliveriesReport()
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
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptDeliveriesReport));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary9 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary10 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary11 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary12 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter6 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter7 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter8 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter9 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter10 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter11 = new DevExpress.DataAccess.Sql.QueryParameter();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.ReportTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramFournisseurText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramDateFin = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramSiteText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramCampagne = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramTypeLivraisonText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramDateDebut = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.excelDataSource1 = new DevExpress.DataAccess.Excel.ExcelDataSource();
            this.paramTypeLivraison = new DevExpress.XtraReports.Parameters.Parameter();
            this.paramFournisseur = new DevExpress.XtraReports.Parameters.Parameter();
            this.sqlDataSource2 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.HiddenForeign = new DevExpress.XtraReports.UI.CalculatedField();
            this.HiddenBeansCount = new DevExpress.XtraReports.UI.CalculatedField();
            this.HiddenMoisture = new DevExpress.XtraReports.UI.CalculatedField();
            this.MoyPondBeans = new DevExpress.XtraReports.UI.CalculatedField();
            this.MoyPondForeign = new DevExpress.XtraReports.UI.CalculatedField();
            this.MoyPondMoisture = new DevExpress.XtraReports.UI.CalculatedField();
            this.NetAccepted = new DevExpress.XtraReports.UI.CalculatedField();
            this.paramStatus = new DevExpress.XtraReports.Parameters.Parameter();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.paramSite = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.Detail.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.Detail.HeightF = 22.5F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseFont = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable3
            // 
            this.xrTable3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1073F, 22.5F);
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35,
            this.xrTableCell51,
            this.xrTableCell31,
            this.xrTableCell34,
            this.xrTableCell53,
            this.xrTableCell7,
            this.xrTableCell33,
            this.xrTableCell54,
            this.xrTableCell29,
            this.xrTableCell32,
            this.xrTableCell55,
            this.xrTableCell26,
            this.xrTableCell49,
            this.xrTableCell30,
            this.xrTableCell50,
            this.xrTableCell28});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.dateLivraison", "{0:dd/MM/yyyy}")});
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.Text = "xrTableCell35";
            this.xrTableCell35.Weight = 0.19159706663218817D;
            this.xrTableCell35.WordWrap = false;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.typeLivraisonNom")});
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.Text = "xrTableCell51";
            this.xrTableCell51.Weight = 0.23936486221981845D;
            this.xrTableCell51.WordWrap = false;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.numeroLivraison")});
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.Text = "xrTableCell31";
            this.xrTableCell31.Weight = 0.20155070323535246D;
            this.xrTableCell31.WordWrap = false;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.fournisseurNom")});
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.Text = "xrTableCell34";
            this.xrTableCell34.Weight = 0.42304632537073872D;
            this.xrTableCell34.WordWrap = false;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.immatriculationLivraison")});
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.Text = "xrTableCell53";
            this.xrTableCell53.Weight = 0.21810343547725053D;
            this.xrTableCell53.WordWrap = false;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.NbrSacsDeclares", "{0:n0}")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.Weight = 0.15781224829397364D;
            this.xrTableCell7.WordWrap = false;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.PoidsDeclares", "{0:n0}")});
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.Text = "xrTableCell33";
            this.xrTableCell33.Weight = 0.174910002296924D;
            this.xrTableCell33.WordWrap = false;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.MoistureLivraison", "{0:n}")});
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.Text = "xrTableCell54";
            this.xrTableCell54.Weight = 0.12863133937981458D;
            this.xrTableCell54.WordWrap = false;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.beansCount", "{0:n0}")});
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.Text = "xrTableCell29";
            this.xrTableCell29.Weight = 0.15496236013677328D;
            this.xrTableCell29.WordWrap = false;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.foreignMatter", "{0:n}")});
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.Text = "xrTableCell32";
            this.xrTableCell32.Weight = 0.15647888183593756D;
            this.xrTableCell32.WordWrap = false;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.sacsAcceptes", "{0:n0}")});
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.Text = "xrTableCell55";
            this.xrTableCell55.Weight = 0.17481693150501654D;
            this.xrTableCell55.WordWrap = false;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.poidsBrutLivre", "{0:n0}")});
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.Text = "xrTableCell26";
            this.xrTableCell26.Weight = 0.17230004188843351D;
            this.xrTableCell26.WordWrap = false;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.tareLivraison", "{0:n0}")});
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.Text = "xrTableCell49";
            this.xrTableCell49.Weight = 0.12038150726915631D;
            this.xrTableCell49.WordWrap = false;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.poidsAccepte", "{0:n0}")});
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.Text = "xrTableCell30";
            this.xrTableCell30.Weight = 0.15160672773519412D;
            this.xrTableCell30.WordWrap = false;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.qualityRetention", "{0:n0}")});
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.Text = "xrTableCell50";
            this.xrTableCell50.Weight = 0.18283124427759853D;
            this.xrTableCell50.WordWrap = false;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.NetAccepted", "{0:n0}")});
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.Text = "xrTableCell28";
            this.xrTableCell28.Weight = 0.15160632244582944D;
            this.xrTableCell28.WordWrap = false;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.ReportTitle,
            this.xrLabel1,
            this.xrLabel2});
            this.TopMargin.HeightF = 114.5834F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportTitle
            // 
            this.ReportTitle.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.ReportTitle.BorderWidth = 2F;
            this.ReportTitle.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.ReportTitle.LocationFloat = new DevExpress.Utils.PointFloat(252.0832F, 36.5647F);
            this.ReportTitle.Name = "ReportTitle";
            this.ReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ReportTitle.SizeF = new System.Drawing.SizeF(547.9167F, 61.88892F);
            this.ReportTitle.StylePriority.UseBorders = false;
            this.ReportTitle.StylePriority.UseBorderWidth = false;
            this.ReportTitle.StylePriority.UseFont = false;
            this.ReportTitle.StylePriority.UseTextAlignment = false;
            this.ReportTitle.Text = "LIVRAISONS ACCEPTEES";
            this.ReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.ReportTitle.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SetTitle);
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.sqlDataSource1, "Parametres_Select.AdresseSociete")});
            this.xrLabel1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(37.89534F, 52.45361F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(188.3334F, 23.00001F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrLabel1.WordWrap = false;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "TMS2017_ReportServer";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "Parametres_Select";
            storedProcQuery1.StoredProcName = "Parametres_Select";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", this.sqlDataSource1, "Parametres_Select.TelSociete")});
            this.xrLabel2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(37.89534F, 75.45362F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(188.3334F, 22.99999F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.WordWrap = false;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Italic);
            this.xrPageInfo2.Format = "Printed on {0:d}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(193.7287F, 22.99998F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1});
            this.BottomMargin.HeightF = 26F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.xrPageInfo1.Format = "Page {0} / {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(511.9908F, 0F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrTable4,
            this.xrTable2});
            this.ReportHeader.HeightF = 84.16666F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel3
            // 
            this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.dateLivraison")});
            this.xrLabel3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(856.1018F, 35F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(216.8982F, 23F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0} Livraison(s)";
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Count;
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel3.Summary = xrSummary1;
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrTable4
            // 
            this.xrTable4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 58F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable4.SizeF = new System.Drawing.SizeF(856.1016F, 25F);
            this.xrTable4.StylePriority.UseFont = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell44,
            this.xrTableCell45});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.Text = "Fournisseur :";
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell38.Weight = 0.63558181238660449D;
            this.xrTableCell38.WordWrap = false;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramFournisseurText, "Text", " {0}")});
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Text = "xrTableCell20";
            this.xrTableCell39.Weight = 4.4354448497628542D;
            // 
            // paramFournisseurText
            // 
            this.paramFournisseurText.Description = "paramFournisseurText";
            this.paramFournisseurText.Name = "paramFournisseurText";
            this.paramFournisseurText.Visible = false;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseFont = false;
            this.xrTableCell44.StylePriority.UseTextAlignment = false;
            this.xrTableCell44.Text = "Date Fin :";
            this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell44.Weight = 0.62109359702489519D;
            this.xrTableCell44.WordWrap = false;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramDateFin, "Text", " {0:dd/MM/yyyy}")});
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.Text = "xrTableCell18";
            this.xrTableCell45.Weight = 0.965488765766741D;
            this.xrTableCell45.WordWrap = false;
            // 
            // paramDateFin
            // 
            this.paramDateFin.Name = "paramDateFin";
            this.paramDateFin.Type = typeof(System.DateTime);
            this.paramDateFin.Visible = false;
            // 
            // xrTable2
            // 
            this.xrTable2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 8F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6,
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(856.1016F, 50F);
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell52,
            this.xrTableCell56,
            this.xrTableCell57,
            this.xrTableCell58,
            this.xrTableCell59,
            this.xrTableCell60});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseFont = false;
            this.xrTableCell52.StylePriority.UseTextAlignment = false;
            this.xrTableCell52.Text = "Site :";
            this.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell52.Weight = 0.59751295723594278D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramSiteText, "Text", " {0}")});
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.Weight = 1.4024870427640572D;
            // 
            // paramSiteText
            // 
            this.paramSiteText.Name = "paramSiteText";
            this.paramSiteText.Visible = false;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.StylePriority.UseFont = false;
            this.xrTableCell57.StylePriority.UseTextAlignment = false;
            this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell57.Weight = 0.760132380598574D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.Weight = 2.0071610225146417D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.StylePriority.UseFont = false;
            this.xrTableCell59.StylePriority.UseTextAlignment = false;
            this.xrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell59.Weight = 0.58389220042665269D;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.Weight = 0.9076599164796626D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "Campagne :";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell16.Weight = 0.59751295723594278D;
            this.xrTableCell16.WordWrap = false;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramCampagne, "Text", " {0}")});
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Text = "xrTableCell17";
            this.xrTableCell17.Weight = 1.4024870427640572D;
            // 
            // paramCampagne
            // 
            this.paramCampagne.Name = "paramCampagne";
            this.paramCampagne.ValueInfo = "{Tous}";
            this.paramCampagne.Visible = false;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = "Type De Livraison :";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell21.Weight = 0.760132380598574D;
            this.xrTableCell21.WordWrap = false;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramTypeLivraisonText, "Text", " {0}")});
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Text = "xrTableCell22";
            this.xrTableCell22.Weight = 2.0071610225146417D;
            // 
            // paramTypeLivraisonText
            // 
            this.paramTypeLivraisonText.Description = "paramDeliveryTypeText";
            this.paramTypeLivraisonText.Name = "paramTypeLivraisonText";
            this.paramTypeLivraisonText.Visible = false;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.Text = "Date Debut :";
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell23.Weight = 0.58389220042665269D;
            this.xrTableCell23.WordWrap = false;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramDateDebut, "Text", " {0:dd/MM/yyyy}")});
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.Text = "xrTableCell24";
            this.xrTableCell24.Weight = 0.9076599164796626D;
            this.xrTableCell24.WordWrap = false;
            // 
            // paramDateDebut
            // 
            this.paramDateDebut.Name = "paramDateDebut";
            this.paramDateDebut.Type = typeof(System.DateTime);
            this.paramDateDebut.Visible = false;
            // 
            // xrTable1
            // 
            this.xrTable1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTable1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTable1.ForeColor = System.Drawing.Color.White;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1073F, 42.70833F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseForeColor = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell4,
            this.xrTableCell2,
            this.xrTableCell6,
            this.xrTableCell5,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell3,
            this.xrTableCell15,
            this.xrTableCell14,
            this.xrTableCell18,
            this.xrTableCell19});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Date";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.73720772727251938D;
            this.xrTableCell1.WordWrap = false;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Type De Livraison";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 0.921003416085853D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Livraison N°";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.77550592095302662D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "Fournisseur";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 1.627754453425446D;
            this.xrTableCell6.WordWrap = false;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Immat";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.8391948925598971D;
            this.xrTableCell5.WordWrap = false;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Sacs est..";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 0.60721431763696454D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Est. Tonnage";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 0.67300064385216907D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "Hum";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 0.49493499723035284D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Grainage";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell11.Weight = 0.5962467885313667D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Mat. Etrag";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 0.60409590772269617D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "Sacs Acc.";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell13.Weight = 0.67062997566140925D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.NullValueText = "0";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Poids Brut";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.66295881416393554D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "Tare";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell15.Weight = 0.46319139954871391D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "Net Livré";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell14.Weight = 0.58333675171081167D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.Text = "Total Retention";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell18.Weight = 0.70347829636397452D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.Text = "Net Accepté";
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell19.Weight = 0.58333548254116674D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.ReportFooter.HeightF = 37.77776F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable5
            // 
            this.xrTable5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTable5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTable5.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable5.ForeColor = System.Drawing.Color.White;
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(369.5002F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable5.SizeF = new System.Drawing.SizeF(703.4998F, 25F);
            this.xrTable5.StylePriority.UseBackColor = false;
            this.xrTable5.StylePriority.UseBorderColor = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseForeColor = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell48,
            this.xrTableCell36,
            this.xrTableCell47,
            this.xrTableCell37,
            this.xrTableCell42,
            this.xrTableCell20,
            this.xrTableCell41,
            this.xrTableCell40,
            this.xrTableCell25,
            this.xrTableCell46,
            this.xrTableCell43,
            this.xrTableCell27});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseTextAlignment = false;
            this.xrTableCell48.Text = "Total :";
            this.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell48.Weight = 0.32876585571868633D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.NbrSacsDeclares")});
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.NullValueText = "0";
            xrSummary2.FormatString = "{0:n0}";
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell36.Summary = xrSummary2;
            this.xrTableCell36.Weight = 0.21566220645448481D;
            this.xrTableCell36.WordWrap = false;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.PoidsDeclares")});
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.NullValueText = "0";
            xrSummary3.FormatString = "{0:n0}";
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell47.Summary = xrSummary3;
            this.xrTableCell47.Text = "0";
            this.xrTableCell47.Weight = 0.23902728216989133D;
            this.xrTableCell47.WordWrap = false;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.MoyPondMoisture", "{0:n}")});
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.NullValueText = "0";
            xrSummary4.FormatString = "{0:#,#}";
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary4.IgnoreNullValues = true;
            this.xrTableCell37.Summary = xrSummary4;
            this.xrTableCell37.Weight = 0.17578433355261364D;
            this.xrTableCell37.WordWrap = false;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.MoyPondBeans", "{0:n}")});
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.NullValueText = "0";
            xrSummary5.FormatString = "{0:#,#}";
            xrSummary5.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            this.xrTableCell42.Summary = xrSummary5;
            this.xrTableCell42.Weight = 0.2117668957159006D;
            this.xrTableCell42.WordWrap = false;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.MoyPondForeign", "{0:n}")});
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.NullValueText = "0";
            xrSummary6.FormatString = "{0:#,#}";
            xrSummary6.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary6.IgnoreNullValues = true;
            this.xrTableCell20.Summary = xrSummary6;
            this.xrTableCell20.Weight = 0.21383982482947944D;
            this.xrTableCell20.WordWrap = false;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.sacsAcceptes")});
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.NullValueText = "0";
            xrSummary7.FormatString = "{0:n0}";
            xrSummary7.IgnoreNullValues = true;
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell41.Summary = xrSummary7;
            this.xrTableCell41.Weight = 0.23890006675284892D;
            this.xrTableCell41.WordWrap = false;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.poidsBrutLivre")});
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.NullValueText = "0";
            xrSummary8.FormatString = "{0:n0}";
            xrSummary8.IgnoreNullValues = true;
            xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell40.Summary = xrSummary8;
            this.xrTableCell40.Weight = 0.2354605671692511D;
            this.xrTableCell40.WordWrap = false;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.tareLivraison")});
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.NullValueText = "0";
            xrSummary9.FormatString = "{0:n0}";
            xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell25.Summary = xrSummary9;
            this.xrTableCell25.Weight = 0.16451036003357092D;
            this.xrTableCell25.WordWrap = false;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.poidsAccepte")});
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.NullValueText = "0";
            xrSummary10.FormatString = "{0:n0}";
            xrSummary10.IgnoreNullValues = true;
            xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell46.Summary = xrSummary10;
            this.xrTableCell46.Weight = 0.21009044104429933D;
            this.xrTableCell46.WordWrap = false;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.qualityRetention")});
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.NullValueText = "0";
            xrSummary11.FormatString = "{0:n0}";
            xrSummary11.IgnoreNullValues = true;
            xrSummary11.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell43.Summary = xrSummary11;
            this.xrTableCell43.Weight = 0.24236276953939082D;
            this.xrTableCell43.WordWrap = false;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "LivraisonSuivi_Select.NetAccepted")});
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.NullValueText = "0";
            xrSummary12.FormatString = "{0:n0}";
            xrSummary12.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell27.Summary = xrSummary12;
            this.xrTableCell27.Weight = 0.21176158987655741D;
            this.xrTableCell27.WordWrap = false;
            // 
            // excelDataSource1
            // 
            this.excelDataSource1.Name = "excelDataSource1";
            // 
            // paramTypeLivraison
            // 
            this.paramTypeLivraison.Name = "paramTypeLivraison";
            this.paramTypeLivraison.Type = typeof(int);
            this.paramTypeLivraison.ValueInfo = "-1";
            this.paramTypeLivraison.Visible = false;
            // 
            // paramFournisseur
            // 
            this.paramFournisseur.Name = "paramFournisseur";
            this.paramFournisseur.ValueInfo = "-1";
            this.paramFournisseur.Visible = false;
            // 
            // sqlDataSource2
            // 
            this.sqlDataSource2.ConnectionName = "TMS2017_ReportServer";
            this.sqlDataSource2.Name = "sqlDataSource2";
            storedProcQuery2.Name = "LivraisonSuivi_Select";
            queryParameter1.Name = "@CropYear";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.paramCampagne]", typeof(string));
            queryParameter2.Name = "@LivraisonTypeID";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.paramTypeLivraison]", typeof(int));
            queryParameter3.Name = "@Fournisseur";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.paramFournisseur]", typeof(string));
            queryParameter4.Name = "@ExportateurID";
            queryParameter4.Type = typeof(short);
            queryParameter4.ValueInfo = "-1";
            queryParameter5.Name = "@AtGate";
            queryParameter5.Type = typeof(short);
            queryParameter5.ValueInfo = "0";
            queryParameter6.Name = "@InProcess";
            queryParameter6.Type = typeof(short);
            queryParameter6.ValueInfo = "0";
            queryParameter7.Name = "@OnlyByStatus";
            queryParameter7.Type = typeof(short);
            queryParameter7.ValueInfo = "1";
            queryParameter8.Name = "@Status";
            queryParameter8.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter8.Value = new DevExpress.DataAccess.Expression("[Parameters.paramStatus]", typeof(string));
            queryParameter9.Name = "@datedebut";
            queryParameter9.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter9.Value = new DevExpress.DataAccess.Expression("[Parameters.paramDateDebut]", typeof(System.DateTime));
            queryParameter10.Name = "@datefin";
            queryParameter10.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter10.Value = new DevExpress.DataAccess.Expression("[Parameters.paramDateFin]", typeof(System.DateTime));
            queryParameter11.Name = "@SiteID";
            queryParameter11.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter11.Value = new DevExpress.DataAccess.Expression("[Parameters.paramSite]", typeof(int));
            storedProcQuery2.Parameters.Add(queryParameter1);
            storedProcQuery2.Parameters.Add(queryParameter2);
            storedProcQuery2.Parameters.Add(queryParameter3);
            storedProcQuery2.Parameters.Add(queryParameter4);
            storedProcQuery2.Parameters.Add(queryParameter5);
            storedProcQuery2.Parameters.Add(queryParameter6);
            storedProcQuery2.Parameters.Add(queryParameter7);
            storedProcQuery2.Parameters.Add(queryParameter8);
            storedProcQuery2.Parameters.Add(queryParameter9);
            storedProcQuery2.Parameters.Add(queryParameter10);
            storedProcQuery2.Parameters.Add(queryParameter11);
            storedProcQuery2.StoredProcName = "LivraisonSuivi_Select";
            this.sqlDataSource2.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery2});
            this.sqlDataSource2.ResultSchemaSerializable = resources.GetString("sqlDataSource2.ResultSchemaSerializable");
            // 
            // HiddenForeign
            // 
            this.HiddenForeign.DataMember = "LivraisonSuivi_Select";
            this.HiddenForeign.Expression = "[foreignMatter] * [poidsAccepte]";
            this.HiddenForeign.Name = "HiddenForeign";
            // 
            // HiddenBeansCount
            // 
            this.HiddenBeansCount.DataMember = "LivraisonSuivi_Select";
            this.HiddenBeansCount.Expression = "[beansCount] * [poidsAccepte]";
            this.HiddenBeansCount.Name = "HiddenBeansCount";
            // 
            // HiddenMoisture
            // 
            this.HiddenMoisture.DataMember = "LivraisonSuivi_Select";
            this.HiddenMoisture.Expression = "[MoistureLivraison] * [poidsAccepte]";
            this.HiddenMoisture.Name = "HiddenMoisture";
            // 
            // MoyPondBeans
            // 
            this.MoyPondBeans.DataMember = "LivraisonSuivi_Select";
            this.MoyPondBeans.Expression = "SUM([HiddenBeansCount])/ Sum([poidsAccepte])";
            this.MoyPondBeans.Name = "MoyPondBeans";
            // 
            // MoyPondForeign
            // 
            this.MoyPondForeign.DataMember = "LivraisonSuivi_Select";
            this.MoyPondForeign.Expression = "Sum([HiddenForeign]) / Sum([poidsAccepte])";
            this.MoyPondForeign.Name = "MoyPondForeign";
            // 
            // MoyPondMoisture
            // 
            this.MoyPondMoisture.DataMember = "LivraisonSuivi_Select";
            this.MoyPondMoisture.Expression = "Sum([HiddenMoisture]) / Sum([poidsAccepte])";
            this.MoyPondMoisture.Name = "MoyPondMoisture";
            // 
            // NetAccepted
            // 
            this.NetAccepted.DataMember = "LivraisonSuivi_Select";
            this.NetAccepted.Expression = "[poidsAccepte] - [qualityRetention]";
            this.NetAccepted.Name = "NetAccepted";
            // 
            // paramStatus
            // 
            this.paramStatus.Name = "paramStatus";
            this.paramStatus.Visible = false;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupHeader1.HeightF = 42.70833F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // paramSite
            // 
            this.paramSite.Description = "Parameter1";
            this.paramSite.Name = "paramSite";
            this.paramSite.Type = typeof(int);
            this.paramSite.ValueInfo = "0";
            this.paramSite.Visible = false;
            // 
            // rptDeliveriesReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter,
            this.GroupHeader1});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.HiddenForeign,
            this.HiddenBeansCount,
            this.HiddenMoisture,
            this.MoyPondBeans,
            this.MoyPondForeign,
            this.MoyPondMoisture,
            this.NetAccepted});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1,
            this.excelDataSource1,
            this.sqlDataSource2});
            this.DataMember = "LivraisonSuivi_Select";
            this.DataSource = this.sqlDataSource2;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(12, 15, 115, 26);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.paramCampagne,
            this.paramTypeLivraison,
            this.paramFournisseur,
            this.paramDateDebut,
            this.paramDateFin,
            this.paramFournisseurText,
            this.paramTypeLivraisonText,
            this.paramStatus,
            this.paramSite,
            this.paramSiteText});
            this.Version = "17.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void SetFieldsToZero(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel lbl = sender as XRLabel;
        if (string.IsNullOrEmpty(lbl.Text))
            lbl.Text = "0";
    }

    private void SetTitle(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel lbl = sender as XRLabel;
        if (paramStatus.Value.ToString() == "AC")
            lbl.Text = "ACCEPTED DELIVERIES REPORT";
        else
            lbl.Text = "REJECTED DELIVERIES REPORT";
    }
}
