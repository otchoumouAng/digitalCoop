using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptAnalysePhysiqueLot
/// </summary>
public class rptAnalysePhysiqueLot : DevExpress.XtraReports.UI.XtraReport
{
    private DetailBand Detail;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private XRPageInfo xrPageInfo2;
    private XRPageInfo xrPageInfo1;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell2;
    private GroupFooterBand GroupFooter1;
    private ReportHeaderBand ReportHeader;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell14;
    private DevExpress.XtraReports.Parameters.Parameter paramDateDebut;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell15;
    private DevExpress.XtraReports.Parameters.Parameter paramDateFin;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell13;
    private XRLabel xrLabel1;
    private ReportFooterBand ReportFooter;
    private XRControlStyle TmsGroupHeaderStyle;
    private XRControlStyle TmsGroupFooterStyle;
    private CalculatedField Exposure;
    private CalculatedField Balance;
    private CalculatedField EstimatedExposure;
    private DevExpress.XtraReports.Parameters.Parameter paramStatut;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell33;
    private XRTableCell lblAnalyseStatut;
    private XRTable xrTable5;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell60;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell62;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell64;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell67;
    private DevExpress.XtraReports.Parameters.Parameter paramStatutText;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell35;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell68;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell70;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptAnalysePhysiqueLot()
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
            DevExpress.XtraReports.UI.XRSummary xrSummary13 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary14 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary15 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary16 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary17 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary18 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary19 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary20 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptAnalysePhysiqueLot));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblAnalyseStatut = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramDateDebut = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramDateFin = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramStatutText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.TmsGroupHeaderStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TmsGroupFooterStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Exposure = new DevExpress.XtraReports.UI.CalculatedField();
            this.Balance = new DevExpress.XtraReports.UI.CalculatedField();
            this.EstimatedExposure = new DevExpress.XtraReports.UI.CalculatedField();
            this.paramStatut = new DevExpress.XtraReports.Parameters.Parameter();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 19.16667F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable2.BorderWidth = 0.5F;
            this.xrTable2.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1109F, 19.16667F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseBorderWidth = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblAnalyseStatut,
            this.xrTableCell6,
            this.xrTableCell34,
            this.xrTableCell3,
            this.xrTableCell36,
            this.xrTableCell7,
            this.xrTableCell39,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell8,
            this.xrTableCell42,
            this.xrTableCell43,
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell9,
            this.xrTableCell50,
            this.xrTableCell45,
            this.xrTableCell44,
            this.xrTableCell49,
            this.xrTableCell47,
            this.xrTableCell46,
            this.xrTableCell10});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // lblAnalyseStatut
            // 
            this.lblAnalyseStatut.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.DateAnalyseExport", "{0:dd/MM/yyyy}")});
            this.lblAnalyseStatut.Name = "lblAnalyseStatut";
            this.lblAnalyseStatut.Text = "lblAnalyseStatut";
            this.lblAnalyseStatut.Weight = 0.19950405426988527D;
            this.lblAnalyseStatut.WordWrap = false;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.NumeroLot", "{0:dd/MM/yyyy HH:mm}")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "Date";
            this.xrTableCell6.Weight = 0.22091973720099928D;
            this.xrTableCell6.WordWrap = false;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.NumeroFiche")});
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.Text = "xrTableCell34";
            this.xrTableCell34.Weight = 0.16568985535713657D;
            this.xrTableCell34.WordWrap = false;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.BeanCount", "{0:n0}")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.Weight = 0.14013826320362688D;
            this.xrTableCell3.WordWrap = false;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.MouldyPc", "{0:0.00}")});
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.Text = "xrTableCell36";
            this.xrTableCell36.Weight = 0.13488442409780238D;
            this.xrTableCell36.WordWrap = false;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.FlatPc", "{0:0.00}")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "Number";
            this.xrTableCell7.Weight = 0.11984937958506667D;
            this.xrTableCell7.WordWrap = false;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.WeevilPc", "{0:0.00}")});
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Text = "xrTableCell39";
            this.xrTableCell39.Weight = 0.13308457558401632D;
            this.xrTableCell39.WordWrap = false;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.GerminatedPc", "{0:0.00}")});
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.Text = "xrTableCell37";
            this.xrTableCell37.Weight = 0.13886733502085494D;
            this.xrTableCell37.WordWrap = false;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.DefectivesPc", "{0:0.00}")});
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.Text = "xrTableCell38";
            this.xrTableCell38.Weight = 0.14039566303842435D;
            this.xrTableCell38.WordWrap = false;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.SlatyPc", "{0:0.00}")});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "Date Debut";
            this.xrTableCell8.Weight = 0.10351056059182259D;
            this.xrTableCell8.WordWrap = false;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.NonFermentedPc", "{0:0.00}")});
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.Text = "xrTableCell42";
            this.xrTableCell42.Weight = 0.12572955248548859D;
            this.xrTableCell42.WordWrap = false;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.VioletPc", "{0:0.00}")});
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.Text = "xrTableCell43";
            this.xrTableCell43.Weight = 0.13410353564733168D;
            this.xrTableCell43.WordWrap = false;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.SievingPc", "{0:0.00}")});
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.Text = "xrTableCell40";
            this.xrTableCell40.Weight = 0.13259873560929067D;
            this.xrTableCell40.WordWrap = false;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.PoidsFeves", "{0:n0}")});
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.Weight = 0.1334586279112927D;
            this.xrTableCell41.WordWrap = false;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.FragmentPc", "{0:0.00}")});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Text = "Date Fin";
            this.xrTableCell9.Weight = 0.12849414167650902D;
            this.xrTableCell9.WordWrap = false;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.BrokenBeansPc", "{0:0.00}")});
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.Text = "xrTableCell50";
            this.xrTableCell50.Weight = 0.1335794828866981D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.LightCropPc", "{0:0.00}")});
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.Text = "xrTableCell45";
            this.xrTableCell45.Weight = 0.13415571199935589D;
            this.xrTableCell45.WordWrap = false;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.BeansCluster", "{0:0.00}")});
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.Text = "xrTableCell44";
            this.xrTableCell44.Weight = 0.13888351328769685D;
            this.xrTableCell44.WordWrap = false;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.CRM", "{0:0.00}")});
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.Text = "xrTableCell49";
            this.xrTableCell49.Weight = 0.10360863533001496D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.MoisturePc", "{0:0.00}")});
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.Text = "xrTableCell47";
            this.xrTableCell47.Weight = 0.10811718440712376D;
            this.xrTableCell47.WordWrap = false;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.ForeignMatter", "{0:0.00}")});
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.Text = "xrTableCell46";
            this.xrTableCell46.Weight = 0.12588674167840233D;
            this.xrTableCell46.WordWrap = false;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.FFA", "{0:0.00}")});
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "Price ( CFA)";
            this.xrTableCell10.Weight = 0.10454028913115981D;
            this.xrTableCell10.WordWrap = false;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 35.5F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1});
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo2.Format = "Printed On : {0:dd/MM/yyyy HH:mm}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 60.83333F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(231.6667F, 23F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.Format = "Page {0}/{1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(500.9022F, 60.83333F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(174.1667F, 23F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupHeader1.HeightF = 29.16667F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable1
            // 
            this.xrTable1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1109F, 29.16667F);
            this.xrTable1.StyleName = "TmsGroupHeaderStyle";
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell17,
            this.xrTableCell27,
            this.xrTableCell1,
            this.xrTableCell21,
            this.xrTableCell18,
            this.xrTableCell26,
            this.xrTableCell5,
            this.xrTableCell28,
            this.xrTableCell22,
            this.xrTableCell30,
            this.xrTableCell19,
            this.xrTableCell31,
            this.xrTableCell24,
            this.xrTableCell2,
            this.xrTableCell23,
            this.xrTableCell48,
            this.xrTableCell32,
            this.xrTableCell35,
            this.xrTableCell33,
            this.xrTableCell20,
            this.xrTableCell25});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "Date Analyse";
            this.xrTableCell4.Weight = 0.19950407146870075D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Text = "Lot N°";
            this.xrTableCell17.Weight = 0.2209197681588671D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Text = "Fiche N°";
            this.xrTableCell27.Weight = 0.16568982439926872D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "Beans Count";
            this.xrTableCell1.Weight = 0.14013818064931277D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Text = "Moisie (%)";
            this.xrTableCell21.Weight = 0.13488434734610405D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.Text = "Flat    (%)";
            this.xrTableCell18.Weight = 0.11984946278235184D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.Text = "Weevil (%)";
            this.xrTableCell26.Weight = 0.13308457042437166D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "Germ.    (%)";
            this.xrTableCell5.Weight = 0.13886733502085502D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.Text = "Defect. (%)";
            this.xrTableCell28.Weight = 0.14039567068131326D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Text = "Ardoisee (%)";
            this.xrTableCell22.Weight = 0.10351072302405059D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.Text = "Unf.   (%)";
            this.xrTableCell30.Weight = 0.1257292260215428D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Text = "Violette (%)";
            this.xrTableCell19.Weight = 0.13410370075595998D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.Text = "Tamis (%)";
            this.xrTableCell31.Weight = 0.13259873560929064D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.Text = "BC   (Wgt)";
            this.xrTableCell24.Weight = 0.13345862134531414D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Frag   (%)";
            this.xrTableCell2.Weight = 0.12849414167650913D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Text = "Brk Bn (%)";
            this.xrTableCell23.Weight = 0.13357948631852323D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.Text = "Light Crop";
            this.xrTableCell48.Weight = 0.13415571394017606D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.Text = "Bean Cluster";
            this.xrTableCell32.Weight = 0.13888364078780768D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.Text = "CRM (%)";
            this.xrTableCell35.Weight = 0.10360880151555368D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.Text = "Moist. (%)";
            this.xrTableCell33.Weight = 0.10811725771905908D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Text = "F. M.   (%)";
            this.xrTableCell20.Weight = 0.12588674425822463D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Text = "FFA (%)";
            this.xrTableCell25.Weight = 0.10453997609684292D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.GroupFooter1.HeightF = 29.16667F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable5.BorderWidth = 0.5F;
            this.xrTable5.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1109F, 29.16667F);
            this.xrTable5.StyleName = "TmsGroupFooterStyle";
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseBorderWidth = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell51,
            this.xrTableCell68,
            this.xrTableCell69,
            this.xrTableCell52,
            this.xrTableCell53,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell56,
            this.xrTableCell57,
            this.xrTableCell58,
            this.xrTableCell59,
            this.xrTableCell60,
            this.xrTableCell61,
            this.xrTableCell62,
            this.xrTableCell63,
            this.xrTableCell64,
            this.xrTableCell70,
            this.xrTableCell65,
            this.xrTableCell66,
            this.xrTableCell67});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            this.xrTableCell51.Text = "Total :";
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell51.Weight = 0.58611369154494142D;
            this.xrTableCell51.WordWrap = false;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.BeanCount")});
            this.xrTableCell68.Name = "xrTableCell68";
            xrSummary1.FormatString = "{0:n0}";
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell68.Summary = xrSummary1;
            this.xrTableCell68.Weight = 0.14013826148374539D;
            // 
            // xrTableCell69
            // 
            this.xrTableCell69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.MouldyPc")});
            this.xrTableCell69.Name = "xrTableCell69";
            xrSummary2.FormatString = "{0:0.00}";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell69.Summary = xrSummary2;
            this.xrTableCell69.Weight = 0.1306551020891415D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.FlatPc")});
            this.xrTableCell52.Name = "xrTableCell52";
            xrSummary3.FormatString = "{0:0.00}";
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell52.Summary = xrSummary3;
            this.xrTableCell52.Weight = 0.12407872223230608D;
            this.xrTableCell52.WordWrap = false;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.WeevilPc")});
            this.xrTableCell53.Name = "xrTableCell53";
            xrSummary4.FormatString = "{0:0.00}";
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary4.IgnoreNullValues = true;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell53.Summary = xrSummary4;
            this.xrTableCell53.Weight = 0.13308448722708643D;
            this.xrTableCell53.WordWrap = false;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.GerminatedPc")});
            this.xrTableCell54.Name = "xrTableCell54";
            xrSummary5.FormatString = "{0:0.00}";
            xrSummary5.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell54.Summary = xrSummary5;
            this.xrTableCell54.Weight = 0.13886734018049962D;
            this.xrTableCell54.WordWrap = false;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.DefectivesPc")});
            this.xrTableCell55.Name = "xrTableCell55";
            xrSummary6.FormatString = "{0:0.00}";
            xrSummary6.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary6.IgnoreNullValues = true;
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell55.Summary = xrSummary6;
            this.xrTableCell55.Weight = 0.1403956630384243D;
            this.xrTableCell55.WordWrap = false;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.SlatyPc")});
            this.xrTableCell56.Name = "xrTableCell56";
            xrSummary7.FormatString = "{0:0.00}";
            xrSummary7.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary7.IgnoreNullValues = true;
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell56.Summary = xrSummary7;
            this.xrTableCell56.Weight = 0.10351072570045092D;
            this.xrTableCell56.WordWrap = false;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.NonFermentedPc")});
            this.xrTableCell57.Name = "xrTableCell57";
            xrSummary8.FormatString = "{0:0.00}";
            xrSummary8.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary8.IgnoreNullValues = true;
            xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell57.Summary = xrSummary8;
            this.xrTableCell57.Weight = 0.12572939380657128D;
            this.xrTableCell57.WordWrap = false;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.VioletPc")});
            this.xrTableCell58.Name = "xrTableCell58";
            xrSummary9.FormatString = "{0:0.00}";
            xrSummary9.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary9.IgnoreNullValues = true;
            xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell58.Summary = xrSummary9;
            this.xrTableCell58.Weight = 0.13410353189402102D;
            this.xrTableCell58.WordWrap = false;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.SievingPc")});
            this.xrTableCell59.Name = "xrTableCell59";
            xrSummary10.FormatString = "{0:0.00}";
            xrSummary10.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary10.IgnoreNullValues = true;
            xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell59.Summary = xrSummary10;
            this.xrTableCell59.Weight = 0.13259873560929075D;
            this.xrTableCell59.WordWrap = false;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.PoidsFeves")});
            this.xrTableCell60.Name = "xrTableCell60";
            xrSummary11.FormatString = "{0:0.00}";
            xrSummary11.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary11.IgnoreNullValues = true;
            xrSummary11.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell60.Summary = xrSummary11;
            this.xrTableCell60.Weight = 0.13345862134531414D;
            this.xrTableCell60.WordWrap = false;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.FragmentPc")});
            this.xrTableCell61.Name = "xrTableCell61";
            xrSummary12.FormatString = "{0:0.00}";
            xrSummary12.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary12.IgnoreNullValues = true;
            xrSummary12.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell61.Summary = xrSummary12;
            this.xrTableCell61.Weight = 0.12849414167650911D;
            this.xrTableCell61.WordWrap = false;
            // 
            // xrTableCell62
            // 
            this.xrTableCell62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.BrokenBeansPc")});
            this.xrTableCell62.Name = "xrTableCell62";
            xrSummary13.FormatString = "{0:0.00}";
            xrSummary13.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary13.IgnoreNullValues = true;
            xrSummary13.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell62.Summary = xrSummary13;
            this.xrTableCell62.Weight = 0.1335794874271854D;
            this.xrTableCell62.WordWrap = false;
            // 
            // xrTableCell63
            // 
            this.xrTableCell63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.LightCropPc")});
            this.xrTableCell63.Name = "xrTableCell63";
            xrSummary14.FormatString = "{0:0.00}";
            xrSummary14.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary14.IgnoreNullValues = true;
            xrSummary14.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell63.Summary = xrSummary14;
            this.xrTableCell63.Weight = 0.13415571027153644D;
            this.xrTableCell63.WordWrap = false;
            // 
            // xrTableCell64
            // 
            this.xrTableCell64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.BeansCluster")});
            this.xrTableCell64.Name = "xrTableCell64";
            xrSummary15.FormatString = "{0:0.00}";
            xrSummary15.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary15.IgnoreNullValues = true;
            xrSummary15.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell64.Summary = xrSummary15;
            this.xrTableCell64.Weight = 0.13888359584201102D;
            this.xrTableCell64.WordWrap = false;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.CRM")});
            this.xrTableCell70.Name = "xrTableCell70";
            xrSummary16.FormatString = "{0:0.00}";
            xrSummary16.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary16.IgnoreNullValues = true;
            xrSummary16.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell70.Summary = xrSummary16;
            this.xrTableCell70.Weight = 0.10360864048965961D;
            // 
            // xrTableCell65
            // 
            this.xrTableCell65.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.MoisturePc")});
            this.xrTableCell65.Name = "xrTableCell65";
            xrSummary17.FormatString = "{0:0.00}";
            xrSummary17.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary17.IgnoreNullValues = true;
            xrSummary17.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell65.Summary = xrSummary17;
            this.xrTableCell65.Weight = 0.10811711733174353D;
            this.xrTableCell65.WordWrap = false;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.ForeignMatter")});
            this.xrTableCell66.Name = "xrTableCell66";
            xrSummary18.FormatString = "{0:0.00}";
            xrSummary18.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary18.IgnoreNullValues = true;
            xrSummary18.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell66.Summary = xrSummary18;
            this.xrTableCell66.Weight = 0.12588674167840236D;
            this.xrTableCell66.WordWrap = false;
            // 
            // xrTableCell67
            // 
            this.xrTableCell67.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.FFA")});
            this.xrTableCell67.Name = "xrTableCell67";
            xrSummary19.FormatString = "{0:0.00}";
            xrSummary19.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            xrSummary19.IgnoreNullValues = true;
            xrSummary19.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell67.Summary = xrSummary19;
            this.xrTableCell67.Weight = 0.1045402891311598D;
            this.xrTableCell67.WordWrap = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrLabel1});
            this.ReportHeader.HeightF = 74.16666F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 49.16664F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1109F, 25F);
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell14,
            this.xrTableCell12,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell29,
            this.xrTableCell13});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Du :";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell11.Weight = 0.20068872479644978D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramDateDebut, "Text", " {0:dd/MM/yyyy}")});
            this.xrTableCell14.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "xrTableCell14";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell14.Weight = 0.53097606505687234D;
            // 
            // paramDateDebut
            // 
            this.paramDateDebut.Name = "paramDateDebut";
            this.paramDateDebut.Type = typeof(System.DateTime);
            this.paramDateDebut.Visible = false;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Au :";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell12.Weight = 0.17636787788509867D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramDateFin, "Text", " {0:dd/MM/yyyy}")});
            this.xrTableCell15.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "xrTableCell15";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell15.Weight = 0.55936631018747573D;
            // 
            // paramDateFin
            // 
            this.paramDateFin.Name = "paramDateFin";
            this.paramDateFin.Type = typeof(System.DateTime);
            this.paramDateFin.Visible = false;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "Statut :";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell16.Weight = 0.41084239478353557D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramStatutText, "Text", " {0}")});
            this.xrTableCell29.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseFont = false;
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.Text = "xrTableCell29";
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell29.Weight = 0.96998207934369D;
            // 
            // paramStatutText
            // 
            this.paramStatutText.Description = "Parameter1";
            this.paramStatutText.Name = "paramStatutText";
            this.paramStatutText.Visible = false;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_AnalysePhysiqueExport_Select.ID")});
            this.xrTableCell13.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            xrSummary20.FormatString = "{0} Analysis";
            xrSummary20.Func = DevExpress.XtraReports.UI.SummaryFunc.Count;
            xrSummary20.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell13.Summary = xrSummary20;
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
            this.xrTableCell13.Weight = 1.7995065985522003D;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel1.BorderWidth = 0.5F;
            this.xrLabel1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(47.88655F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(1009.968F, 32.16667F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseBorderWidth = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Liste des analyses physiques - Lot";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ReportFooter
            // 
            this.ReportFooter.HeightF = 43.33333F;
            this.ReportFooter.Name = "ReportFooter";
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
            // Exposure
            // 
            this.Exposure.DataMember = "Fournisseur_PrintFinancialStatus";
            this.Exposure.Expression = "Sum([Balance]) - [Saving]";
            this.Exposure.Name = "Exposure";
            // 
            // Balance
            // 
            this.Balance.DataMember = "Fournisseur_PrintFinancialStatus";
            this.Balance.Expression = "[Allocated] - [Recovered]";
            this.Balance.Name = "Balance";
            // 
            // EstimatedExposure
            // 
            this.EstimatedExposure.DataMember = "Fournisseur_PrintFinancialStatus";
            this.EstimatedExposure.Expression = "[Exposure] - IsNull([DeliveryGate],0)";
            this.EstimatedExposure.Name = "EstimatedExposure";
            // 
            // paramStatut
            // 
            this.paramStatut.Name = "paramStatut";
            this.paramStatut.Type = typeof(short);
            this.paramStatut.ValueInfo = "-1";
            this.paramStatut.Visible = false;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "TMS2017_ReportServer";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "V2_AnalysePhysiqueExport_Select";
            queryParameter1.Name = "@begindate";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.paramDateDebut]", typeof(System.DateTime));
            queryParameter2.Name = "@enddate";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.paramDateFin]", typeof(System.DateTime));
            queryParameter3.Name = "@status";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.paramStatut]", typeof(short));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.Parameters.Add(queryParameter3);
            storedProcQuery1.StoredProcName = "V2_AnalysePhysiqueExport_Select";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // rptAnalysePhysiqueLot
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupFooter1,
            this.ReportHeader,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.Exposure,
            this.Balance,
            this.EstimatedExposure});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "V2_AnalysePhysiqueExport_Select";
            this.DataSource = this.sqlDataSource1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(30, 30, 36, 100);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.paramDateDebut,
            this.paramDateFin,
            this.paramStatut,
            this.paramStatutText});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.TmsGroupHeaderStyle,
            this.TmsGroupFooterStyle});
            this.Version = "17.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
