using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptWeighingList
/// </summary>
public class rptWeighingList : DevExpress.XtraReports.UI.XtraReport
{
    private DetailBand Detail;
    private XRTable xrTable3;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell40;
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private XRPageInfo xrPageInfo2;
    private XRPageInfo xrPageInfo1;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell21;
    private GroupFooterBand GroupFooter1;
    private XRTable xrTable5;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell51;
    private ReportHeaderBand ReportHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell4;
    private DevExpress.XtraReports.Parameters.Parameter campagneID;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell10;
    private DevExpress.XtraReports.Parameters.Parameter paramExportateurText;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell9;
    private DevExpress.XtraReports.Parameters.Parameter paramDateDebut;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell2;
    private DevExpress.XtraReports.Parameters.Parameter paramDateFin;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private DevExpress.XtraReports.Parameters.Parameter paramFournisseurText;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private DevExpress.XtraReports.Parameters.Parameter paramTypeLivraisonText;
    private XRLabel lblWeighingTitle;
    private ReportFooterBand ReportFooter;
    private XRControlStyle TmsGroupHeaderStyle;
    private XRControlStyle TmsGroupFooterStyle;
    private CalculatedField Exposure;
    private CalculatedField Balance;
    private CalculatedField EstimatedExposure;
    private CalculatedField NetAccepted;
    private CalculatedField MoyPondereeForeignM;
    private CalculatedField MoyPondereeBrisure;
    private CalculatedField MoyPondereeMoisture;
    private CalculatedField MoyPondereeBeansCount;
    private DevExpress.XtraReports.Parameters.Parameter paramTypeLivraison;
    private DevExpress.XtraReports.Parameters.Parameter paramFournisseur;
    private DevExpress.XtraReports.Parameters.Parameter paramExportateur;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell15;
    private DevExpress.XtraReports.Parameters.Parameter paramPontBascule;
    private DevExpress.XtraReports.Parameters.Parameter paramStatus;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource2;
    private DevExpress.XtraReports.Parameters.Parameter paramPontBasculeText;
    private DevExpress.XtraReports.Parameters.Parameter paramStatusText;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell23;
    private CalculatedField Tare;
    private XRTableCell xrTableCell26;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptWeighingList()
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
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter6 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter7 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptWeighingList));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.campagneID = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramPontBasculeText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramStatusText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramDateDebut = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramDateFin = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramFournisseurText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramTypeLivraisonText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblWeighingTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.TmsGroupHeaderStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TmsGroupFooterStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Exposure = new DevExpress.XtraReports.UI.CalculatedField();
            this.Balance = new DevExpress.XtraReports.UI.CalculatedField();
            this.EstimatedExposure = new DevExpress.XtraReports.UI.CalculatedField();
            this.NetAccepted = new DevExpress.XtraReports.UI.CalculatedField();
            this.MoyPondereeForeignM = new DevExpress.XtraReports.UI.CalculatedField();
            this.MoyPondereeBrisure = new DevExpress.XtraReports.UI.CalculatedField();
            this.MoyPondereeMoisture = new DevExpress.XtraReports.UI.CalculatedField();
            this.MoyPondereeBeansCount = new DevExpress.XtraReports.UI.CalculatedField();
            this.paramTypeLivraison = new DevExpress.XtraReports.Parameters.Parameter();
            this.paramFournisseur = new DevExpress.XtraReports.Parameters.Parameter();
            this.paramExportateur = new DevExpress.XtraReports.Parameters.Parameter();
            this.paramExportateurText = new DevExpress.XtraReports.Parameters.Parameter();
            this.paramPontBascule = new DevExpress.XtraReports.Parameters.Parameter();
            this.paramStatus = new DevExpress.XtraReports.Parameters.Parameter();
            this.sqlDataSource2 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.Tare = new DevExpress.XtraReports.UI.CalculatedField();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable3.BorderWidth = 0.5F;
            this.xrTable3.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1109F, 25F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseBorderWidth = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33,
            this.xrTableCell31,
            this.xrTableCell13,
            this.xrTableCell36,
            this.xrTableCell41,
            this.xrTableCell42,
            this.xrTableCell39,
            this.xrTableCell43,
            this.xrTableCell38,
            this.xrTableCell40});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.LivraisonDate", "{0:dd/MM/yyyy HH:mm}")});
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.Weight = 0.31537420412137551D;
            this.xrTableCell33.WordWrap = false;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.LivraisonTypeName")});
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.Text = "xrTableCell31";
            this.xrTableCell31.Weight = 0.52321913695958844D;
            this.xrTableCell31.WordWrap = false;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.LivraisonNumber")});
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "xrTableCell13";
            this.xrTableCell13.Weight = 0.21436341460273536D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.LivraisonSupplierName")});
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.Text = "xrTableCell36";
            this.xrTableCell36.Weight = 0.53450955390070232D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.LivraisonImmatriculation")});
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.Text = "xrTableCell41";
            this.xrTableCell41.Weight = 0.2707127639041278D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.PeseeFirstWeight", "{0:#,#}")});
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.NullValueText = "0";
            xrSummary1.FormatString = "{0:#,#}";
            xrSummary1.IgnoreNullValues = true;
            this.xrTableCell42.Summary = xrSummary1;
            this.xrTableCell42.Weight = 0.2493500292462417D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.PeseeSecondWeight", "{0:#,#}")});
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.NullValueText = "0";
            this.xrTableCell39.Text = "xrTableCell39";
            this.xrTableCell39.Weight = 0.23681526768841757D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.PeseeGrossWeight", "{0:#,#}")});
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.NullValueText = "0";
            this.xrTableCell43.Text = "xrTableCell43";
            this.xrTableCell43.Weight = 0.26904664929650512D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.Tare", "{0:#,#}")});
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.NullValueText = "0";
            this.xrTableCell38.Text = "xrTableCell38";
            this.xrTableCell38.Weight = 0.15791237214538195D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.PeseeNetWeightDelivered", "{0:#,#}")});
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.NullValueText = "0";
            this.xrTableCell40.Text = "xrTableCell40";
            this.xrTableCell40.Weight = 0.2286966081349241D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 39.16667F;
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
            this.xrPageInfo2.Format = "Printed On :  {0}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(15.41658F, 60.83333F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(294.5834F, 23F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.Format = "Page {0}/{1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(473.3333F, 60.83333F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(151.0834F, 23F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.GroupHeader1.HeightF = 35F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable2
            // 
            this.xrTable2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1109F, 35F);
            this.xrTable2.StyleName = "TmsGroupHeaderStyle";
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell22,
            this.xrTableCell16,
            this.xrTableCell19,
            this.xrTableCell24,
            this.xrTableCell17,
            this.xrTableCell20,
            this.xrTableCell25,
            this.xrTableCell14,
            this.xrTableCell21,
            this.xrTableCell18});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Text = "Date Livraison";
            this.xrTableCell22.Weight = 0.31537420412137551D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Text = "Type De Livraison";
            this.xrTableCell16.Weight = 0.52321913695958844D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Text = "Livraison N°";
            this.xrTableCell19.Weight = 0.21436342836178773D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.Text = "Supplier";
            this.xrTableCell24.Weight = 0.53450954014165D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Text = "Truck ID";
            this.xrTableCell17.Weight = 0.27071277422341711D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Text = "1st Weight";
            this.xrTableCell20.Weight = 0.24935002924624178D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Text = "2nd Weight";
            this.xrTableCell25.Weight = 0.23681530896557457D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Text = "Poids Brut";
            this.xrTableCell14.Weight = 0.26904663897721592D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Text = "Tare";
            this.xrTableCell21.Weight = 0.15791237558514509D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.Text = "Delivered";
            this.xrTableCell18.Weight = 0.22869656341800393D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.GroupFooter1.HeightF = 25F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrTable5
            // 
            this.xrTable5.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1109F, 25F);
            this.xrTable5.StyleName = "TmsGroupFooterStyle";
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell15,
            this.xrTableCell44,
            this.xrTableCell47,
            this.xrTableCell52,
            this.xrTableCell51});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.Text = "Total :";
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell23.Weight = 1.8581792145188163D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.PeseeFirstWeight")});
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.NullValueText = "0";
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:#,#}";
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell15.Summary = xrSummary2;
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell15.Weight = 0.2493500086076631D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.PeseeSecondWeight")});
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.NullValueText = "0";
            this.xrTableCell44.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:#,#}";
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell44.Summary = xrSummary3;
            this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell44.Weight = 0.23681523673054969D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.PeseeGrossWeight")});
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.NullValueText = "0";
            xrSummary4.FormatString = "{0:#,#}";
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell47.Summary = xrSummary4;
            this.xrTableCell47.Weight = 0.2690466492965049D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.Tare")});
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.NullValueText = "0";
            xrSummary5.FormatString = "{0:#,#}";
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell52.Summary = xrSummary5;
            this.xrTableCell52.Weight = 0.15791237558514512D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.PeseeNetWeightDelivered")});
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.NullValueText = "0";
            xrSummary6.FormatString = "{0:#,#}";
            xrSummary6.IgnoreNullValues = true;
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell51.Summary = xrSummary6;
            this.xrTableCell51.Weight = 0.22869651526132084D;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.lblWeighingTitle});
            this.ReportHeader.HeightF = 115F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 53.33333F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1109F, 50F);
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell4,
            this.xrTableCell3,
            this.xrTableCell10,
            this.xrTableCell56,
            this.xrTableCell55,
            this.xrTableCell12,
            this.xrTableCell9,
            this.xrTableCell11,
            this.xrTableCell2});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Campagne :";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell1.Weight = 0.27502335779459475D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.campagneID, "Text", " {0}")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell4.Weight = 0.33000101141287957D;
            // 
            // campagneID
            // 
            this.campagneID.Name = "campagneID";
            this.campagneID.Visible = false;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Bridge N° :";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell3.Weight = 0.23356893596191236D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramPontBasculeText, "Text", " {0}")});
            this.xrTableCell10.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "xrTableCell10";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell10.Weight = 0.61767351258868464D;
            // 
            // paramPontBasculeText
            // 
            this.paramPontBasculeText.Name = "paramPontBasculeText";
            this.paramPontBasculeText.Visible = false;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.StylePriority.UseFont = false;
            this.xrTableCell56.StylePriority.UseTextAlignment = false;
            this.xrTableCell56.Text = "Statut :";
            this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell56.Weight = 0.23286777188479899D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramStatusText, "Text", " {0}")});
            this.xrTableCell55.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.StylePriority.UseFont = false;
            this.xrTableCell55.StylePriority.UseTextAlignment = false;
            this.xrTableCell55.Text = "xrTableCell55";
            this.xrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell55.Weight = 0.34846886690723611D;
            // 
            // paramStatusText
            // 
            this.paramStatusText.Name = "paramStatusText";
            this.paramStatusText.Visible = false;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Du : ";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell12.Weight = 0.13766971457425378D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramDateDebut, "Text", " {0:dd/MM/yyyy}")});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell9.Weight = 0.2938434777635186D;
            // 
            // paramDateDebut
            // 
            this.paramDateDebut.Name = "paramDateDebut";
            this.paramDateDebut.Type = typeof(System.DateTime);
            this.paramDateDebut.Visible = false;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Au : ";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell11.Weight = 0.094652975065827816D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramDateFin, "Text", " {0:dd/MM/yyyy}")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 0.43623037604629311D;
            // 
            // paramDateFin
            // 
            this.paramDateFin.Name = "paramDateFin";
            this.paramDateFin.Type = typeof(System.DateTime);
            this.paramDateFin.Visible = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell26,
            this.xrTableCell8});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Fournisseur :";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell5.Weight = 0.27502335779459475D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramFournisseurText, "Text", " {0}")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell6.Weight = 1.1180133172412303D;
            // 
            // paramFournisseurText
            // 
            this.paramFournisseurText.Description = "Parameter1";
            this.paramFournisseurText.Name = "paramFournisseurText";
            this.paramFournisseurText.Visible = false;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Type De Livraison  : ";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell7.Weight = 0.29609789049270507D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramTypeLivraisonText, "Text", " {0}")});
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            this.xrTableCell26.Text = "xrTableCell26";
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell26.Weight = 0.77998208075298D;
            // 
            // paramTypeLivraisonText
            // 
            this.paramTypeLivraisonText.Description = "Parameter1";
            this.paramTypeLivraisonText.Name = "paramTypeLivraisonText";
            this.paramTypeLivraisonText.Visible = false;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Select.ID")});
            this.xrTableCell8.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            xrSummary7.FormatString = "{0} Pesée(s)";
            xrSummary7.Func = DevExpress.XtraReports.UI.SummaryFunc.Count;
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell8.Summary = xrSummary7;
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell8.Weight = 0.5308833537184896D;
            // 
            // lblWeighingTitle
            // 
            this.lblWeighingTitle.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.lblWeighingTitle.BorderWidth = 0.5F;
            this.lblWeighingTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeighingTitle.LocationFloat = new DevExpress.Utils.PointFloat(50.83333F, 0F);
            this.lblWeighingTitle.Name = "lblWeighingTitle";
            this.lblWeighingTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblWeighingTitle.SizeF = new System.Drawing.SizeF(1010.667F, 33.83334F);
            this.lblWeighingTitle.StylePriority.UseBorders = false;
            this.lblWeighingTitle.StylePriority.UseBorderWidth = false;
            this.lblWeighingTitle.StylePriority.UseFont = false;
            this.lblWeighingTitle.StylePriority.UseTextAlignment = false;
            this.lblWeighingTitle.Text = "List Of Weighings";
            this.lblWeighingTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            // NetAccepted
            // 
            this.NetAccepted.DataMember = "AnalysePhysique_SelectAcceptedDeliveries";
            this.NetAccepted.Expression = "[TonnageAccepted] - [QualityRetentionLivraison]";
            this.NetAccepted.Name = "NetAccepted";
            // 
            // MoyPondereeForeignM
            // 
            this.MoyPondereeForeignM.DataMember = "AnalysePhysique_SelectAcceptedDeliveries";
            this.MoyPondereeForeignM.Expression = "Sum([MatiereEtrangere] * [TonnageAccepted])/Sum([TonnageAccepted])";
            this.MoyPondereeForeignM.Name = "MoyPondereeForeignM";
            // 
            // MoyPondereeBrisure
            // 
            this.MoyPondereeBrisure.DataMember = "AnalysePhysique_SelectAcceptedDeliveries";
            this.MoyPondereeBrisure.Expression = "Sum([Brisures]* [TonnageAccepted]) / Sum([TonnageAccepted])";
            this.MoyPondereeBrisure.Name = "MoyPondereeBrisure";
            // 
            // MoyPondereeMoisture
            // 
            this.MoyPondereeMoisture.DataMember = "AnalysePhysique_SelectAcceptedDeliveries";
            this.MoyPondereeMoisture.Expression = "Sum([Humidite] * [TonnageAccepted])/ Sum([TonnageAccepted])";
            this.MoyPondereeMoisture.Name = "MoyPondereeMoisture";
            // 
            // MoyPondereeBeansCount
            // 
            this.MoyPondereeBeansCount.DataMember = "AnalysePhysique_SelectAcceptedDeliveries";
            this.MoyPondereeBeansCount.Expression = "Sum( [BeansCount] * [TonnageAccepted])/ Sum([TonnageAccepted])";
            this.MoyPondereeBeansCount.Name = "MoyPondereeBeansCount";
            // 
            // paramTypeLivraison
            // 
            this.paramTypeLivraison.Name = "paramTypeLivraison";
            this.paramTypeLivraison.Type = typeof(int);
            this.paramTypeLivraison.ValueInfo = "0";
            this.paramTypeLivraison.Visible = false;
            // 
            // paramFournisseur
            // 
            this.paramFournisseur.Name = "paramFournisseur";
            this.paramFournisseur.Visible = false;
            // 
            // paramExportateur
            // 
            this.paramExportateur.Name = "paramExportateur";
            this.paramExportateur.Type = typeof(int);
            this.paramExportateur.ValueInfo = "0";
            this.paramExportateur.Visible = false;
            // 
            // paramExportateurText
            // 
            this.paramExportateurText.Description = "Parameter1";
            this.paramExportateurText.Name = "paramExportateurText";
            this.paramExportateurText.Visible = false;
            // 
            // paramPontBascule
            // 
            this.paramPontBascule.Name = "paramPontBascule";
            this.paramPontBascule.Type = typeof(int);
            this.paramPontBascule.ValueInfo = "-1";
            this.paramPontBascule.Visible = false;
            // 
            // paramStatus
            // 
            this.paramStatus.Name = "paramStatus";
            this.paramStatus.Type = typeof(short);
            this.paramStatus.ValueInfo = "0";
            this.paramStatus.Visible = false;
            // 
            // sqlDataSource2
            // 
            this.sqlDataSource2.ConnectionName = "Tms2017_ReportServer";
            this.sqlDataSource2.Name = "sqlDataSource2";
            storedProcQuery1.Name = "Pesee_Select";
            queryParameter1.Name = "@PontBasculeID";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.paramPontBascule]", typeof(int));
            queryParameter2.Name = "@CropYearID";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.campagneID]", typeof(string));
            queryParameter3.Name = "@DeliveryTypeID";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.paramTypeLivraison]", typeof(int));
            queryParameter4.Name = "@Supplier";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("[Parameters.paramFournisseur]", typeof(string));
            queryParameter5.Name = "@StartDate";
            queryParameter5.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter5.Value = new DevExpress.DataAccess.Expression("[Parameters.paramDateDebut]", typeof(System.DateTime));
            queryParameter6.Name = "@EndDate";
            queryParameter6.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter6.Value = new DevExpress.DataAccess.Expression("[Parameters.paramDateFin]", typeof(System.DateTime));
            queryParameter7.Name = "@Status";
            queryParameter7.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter7.Value = new DevExpress.DataAccess.Expression("[Parameters.paramStatus]", typeof(short));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.Parameters.Add(queryParameter3);
            storedProcQuery1.Parameters.Add(queryParameter4);
            storedProcQuery1.Parameters.Add(queryParameter5);
            storedProcQuery1.Parameters.Add(queryParameter6);
            storedProcQuery1.Parameters.Add(queryParameter7);
            storedProcQuery1.StoredProcName = "Pesee_Select";
            this.sqlDataSource2.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource2.ResultSchemaSerializable = resources.GetString("sqlDataSource2.ResultSchemaSerializable");
            // 
            // Tare
            // 
            this.Tare.DataMember = "Pesee_Select";
            this.Tare.Expression = "[PeseeTareOfPalets] + [PeseeTareOfBags]";
            this.Tare.Name = "Tare";
            // 
            // rptWeighingList
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
            this.EstimatedExposure,
            this.NetAccepted,
            this.MoyPondereeForeignM,
            this.MoyPondereeBrisure,
            this.MoyPondereeMoisture,
            this.MoyPondereeBeansCount,
            this.Tare});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource2});
            this.DataMember = "Pesee_Select";
            this.DataSource = this.sqlDataSource2;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(30, 30, 39, 100);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.campagneID,
            this.paramTypeLivraison,
            this.paramFournisseur,
            this.paramDateDebut,
            this.paramDateFin,
            this.paramExportateur,
            this.paramFournisseurText,
            this.paramTypeLivraisonText,
            this.paramExportateurText,
            this.paramPontBascule,
            this.paramStatus,
            this.paramPontBasculeText,
            this.paramStatusText});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.TmsGroupHeaderStyle,
            this.TmsGroupFooterStyle});
            this.Version = "17.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
