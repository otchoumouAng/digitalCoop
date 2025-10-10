using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptDeliveriesList
/// </summary>
public class rptDeliveriesList : DevExpress.XtraReports.UI.XtraReport
{
    private DetailBand Detail;
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private XRPageInfo xrPageInfo1;
    private GroupHeaderBand GroupHeader1;
    private GroupFooterBand GroupFooter1;
    private ReportHeaderBand ReportHeader;
    private ReportFooterBand ReportFooter;
    private XRControlStyle TmsGroupHeaderStyle;
    private XRControlStyle TmsGroupFooterStyle;
    private CalculatedField Exposure;
    private CalculatedField Balance;
    private CalculatedField EstimatedExposure;
    private DevExpress.XtraReports.Parameters.Parameter campagneID;
    private XRTable xrTable2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell17;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell3;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell5;
    private XRLabel xrLabel1;
    private XRTable xrTable3;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell24;
    private XRTable xrTable5;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell30;
    private DevExpress.XtraReports.Parameters.Parameter paramTypeLivraison;
    private DevExpress.XtraReports.Parameters.Parameter paramFournisseur;
    private DevExpress.XtraReports.Parameters.Parameter paramDateDebut;
    private DevExpress.XtraReports.Parameters.Parameter paramDateFin;
    private DevExpress.XtraReports.Parameters.Parameter paramExportateur;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource2;
    private DevExpress.XtraReports.Parameters.Parameter paramTypeLivraisonText;
    private DevExpress.XtraReports.Parameters.Parameter paramFournisseurText;
    private DevExpress.XtraReports.Parameters.Parameter paramExportateurNom;
    private DevExpress.XtraReports.Parameters.Parameter paramCampagneText;
    private XRPageInfo xrPageInfo2;
    private XRTableCell xrTableCell6;
    private DevExpress.XtraReports.Parameters.Parameter paramSite;
    private DevExpress.XtraReports.Parameters.Parameter paramSiteText;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell31;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptDeliveriesList()
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
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter6 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter7 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptDeliveriesList));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramSiteText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramExportateurNom = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramCampagneText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramDateDebut = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramDateFin = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramFournisseurText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramTypeLivraisonText = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.TmsGroupHeaderStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TmsGroupFooterStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Exposure = new DevExpress.XtraReports.UI.CalculatedField();
            this.Balance = new DevExpress.XtraReports.UI.CalculatedField();
            this.EstimatedExposure = new DevExpress.XtraReports.UI.CalculatedField();
            this.campagneID = new DevExpress.XtraReports.Parameters.Parameter();
            this.paramTypeLivraison = new DevExpress.XtraReports.Parameters.Parameter();
            this.paramFournisseur = new DevExpress.XtraReports.Parameters.Parameter();
            this.paramExportateur = new DevExpress.XtraReports.Parameters.Parameter();
            this.sqlDataSource2 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.paramSite = new DevExpress.XtraReports.Parameters.Parameter();
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
            this.Detail.StylePriority.UseTextAlignment = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable3.BorderWidth = 0.5F;
            this.xrTable3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1069F, 25F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseBorderWidth = false;
            this.xrTable3.StylePriority.UseFont = false;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25,
            this.xrTableCell22,
            this.xrTableCell26,
            this.xrTableCell32,
            this.xrTableCell23,
            this.xrTableCell27,
            this.xrTableCell28,
            this.xrTableCell24});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Livraison_PrintList.LivraisonDate", "{0:dd/MM/yyyy HH:mm}")});
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Text = "xrTableCell25";
            this.xrTableCell25.Weight = 0.33384326235812234D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Livraison_PrintList.LivraisonTypeNom")});
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Text = "xrTableCell22";
            this.xrTableCell22.Weight = 0.557644287379395D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Livraison_PrintList.LivraisonNumero")});
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.Text = "xrTableCell26";
            this.xrTableCell26.Weight = 0.29678715085718493D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Livraison_PrintList.SiteNom")});
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.Weight = 0.39268705833931783D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Livraison_PrintList.FournisseurNom")});
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Text = "xrTableCell23";
            this.xrTableCell23.Weight = 0.63352742422823638D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Livraison_PrintList.LivraisonImmatriculation")});
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Text = "xrTableCell27";
            this.xrTableCell27.Weight = 0.28551081683774354D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Livraison_PrintList.LivraisonSacsDeclares", "{0:#,#}")});
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.NullValueText = "0";
            this.xrTableCell28.Text = "xrTableCell28";
            this.xrTableCell28.Weight = 0.21663489764479937D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Livraison_PrintList.LivraisonPoidsDeclare", "{0:#,#}")});
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.Text = "0";
            this.xrTableCell24.Weight = 0.28336510235520063D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 37.16667F;
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
            this.xrPageInfo2.Format = "Printed On : {0}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(10F, 60.83333F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(327.6667F, 23F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.Format = "page {0} / {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(361.6666F, 60.83333F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(327.6667F, 23F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.GroupHeader1.HeightF = 33.83331F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable2
            // 
            this.xrTable2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1069F, 33.83331F);
            this.xrTable2.StyleName = "TmsGroupHeaderStyle";
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.xrTableCell15,
            this.xrTableCell19,
            this.xrTableCell31,
            this.xrTableCell16,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell17});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.Text = "Date Livraison";
            this.xrTableCell18.Weight = 0.33384326235812234D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Text = "Type De Livraison";
            this.xrTableCell15.Weight = 0.5576442873793952D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Text = "Livraison N°";
            this.xrTableCell19.Weight = 0.29678715085718493D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.Text = "Site";
            this.xrTableCell31.Weight = 0.39268706160503147D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.Text = "Fournisseur";
            this.xrTableCell16.Weight = 0.63352777006732464D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Text = "Immat";
            this.xrTableCell20.Weight = 0.28551046773294136D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Text = "Sacs est..";
            this.xrTableCell21.Weight = 0.21663500674005004D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Text = "Ton. est. (Kg)";
            this.xrTableCell17.Weight = 0.28336499325994996D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.GroupFooter1.HeightF = 26.66667F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrTable5
            // 
            this.xrTable5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0.8333333F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1069F, 25F);
            this.xrTable5.StyleName = "TmsGroupFooterStyle";
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell30,
            this.xrTableCell36,
            this.xrTableCell37});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.Text = "Total :  ";
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell30.Weight = 2.4999999854539663D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Livraison_PrintList.LivraisonSacsDeclares")});
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.NullValueText = "0";
            xrSummary1.FormatString = "{0:#,#}";
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell36.Summary = xrSummary1;
            this.xrTableCell36.Weight = 0.21663490491781612D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Livraison_PrintList.LivraisonPoidsDeclare")});
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.NullValueText = "0";
            xrSummary2.FormatString = "{0:#,#}";
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell37.Summary = xrSummary2;
            this.xrTableCell37.Weight = 0.28336510962821737D;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.xrLabel1});
            this.ReportHeader.HeightF = 130.8333F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 55.83328F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow1,
            this.xrTableRow2});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1069F, 75F);
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell29});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Site :";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell8.Weight = 0.2345259564080604D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramSiteText, "Text", " {0}")});
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell29.Weight = 2.7654740435919396D;
            // 
            // paramSiteText
            // 
            this.paramSiteText.Name = "paramSiteText";
            this.paramSiteText.Visible = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell1,
            this.xrTableCell9,
            this.xrTableCell2,
            this.xrTableCell13,
            this.xrTableCell7,
            this.xrTableCell14,
            this.xrTableCell3});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Exportateur : ";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell11.Weight = 0.2345259564080604D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramExportateurNom, "Text", " {0}")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = " ";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 0.95374870714800486D;
            // 
            // paramExportateurNom
            // 
            this.paramExportateurNom.Description = "Parameter1";
            this.paramExportateurNom.Name = "paramExportateurNom";
            this.paramExportateurNom.Visible = false;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Campagne : ";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell9.Weight = 0.31172533644393474D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramCampagneText, "Text", " {0}")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 0.32602462550364186D;
            // 
            // paramCampagneText
            // 
            this.paramCampagneText.Description = "Parameter1";
            this.paramCampagneText.Name = "paramCampagneText";
            this.paramCampagneText.Visible = false;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "Du : ";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell13.Weight = 0.137518022070049D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramDateDebut, "Text", " {0:dd/MM/yyyy}")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell7.Weight = 0.28645743791317069D;
            // 
            // paramDateDebut
            // 
            this.paramDateDebut.Name = "paramDateDebut";
            this.paramDateDebut.Type = typeof(System.DateTime);
            this.paramDateDebut.Visible = false;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "Au : ";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell14.Weight = 0.089356671233435769D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramDateFin, "Text", " {0:dd/MM/yyyy}")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell3.Weight = 0.6606432432797027D;
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
            this.xrTableCell12,
            this.xrTableCell4,
            this.xrTableCell10,
            this.xrTableCell6,
            this.xrTableCell5});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Fournisseur : ";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell12.Weight = 0.2345259564080604D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramFournisseurText, "Text", " {0}")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell4.Weight = 0.95374870714800486D;
            // 
            // paramFournisseurText
            // 
            this.paramFournisseurText.Description = "Parameter1";
            this.paramFournisseurText.Name = "paramFournisseurText";
            this.paramFournisseurText.Visible = false;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "Type De Livraison : ";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell10.Weight = 0.31172533644393474D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramTypeLivraisonText, "Text", " {0}")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell6.Weight = 0.75D;
            // 
            // paramTypeLivraisonText
            // 
            this.paramTypeLivraisonText.Description = "Parameter1";
            this.paramTypeLivraisonText.Name = "paramTypeLivraisonText";
            this.paramTypeLivraisonText.Visible = false;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Livraison_PrintList.ID")});
            this.xrTableCell5.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0} Deliverie(s)";
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Count;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell5.Summary = xrSummary3;
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell5.Weight = 0.75D;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel1.BorderWidth = 0.5F;
            this.xrLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(41.66667F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(985.6667F, 41.66667F);
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseBorderWidth = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Liste des livraisons";
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
            // campagneID
            // 
            this.campagneID.Name = "campagneID";
            this.campagneID.Visible = false;
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
            this.paramFournisseur.Type = typeof(int);
            this.paramFournisseur.ValueInfo = "0";
            this.paramFournisseur.Visible = false;
            // 
            // paramExportateur
            // 
            this.paramExportateur.Name = "paramExportateur";
            this.paramExportateur.Type = typeof(int);
            this.paramExportateur.ValueInfo = "0";
            this.paramExportateur.Visible = false;
            // 
            // sqlDataSource2
            // 
            this.sqlDataSource2.ConnectionName = "TMS2017_ReportServer";
            this.sqlDataSource2.Name = "sqlDataSource2";
            storedProcQuery1.Name = "Livraison_PrintList";
            queryParameter1.Name = "@CropYear";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.campagneID]", typeof(string));
            queryParameter2.Name = "@LivraisonTypeID";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.paramTypeLivraison]", typeof(int));
            queryParameter3.Name = "@Fournisseur";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.paramFournisseur]", typeof(int));
            queryParameter4.Name = "@StartDate";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("[Parameters.paramDateDebut]", typeof(System.DateTime));
            queryParameter5.Name = "@EndDate";
            queryParameter5.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter5.Value = new DevExpress.DataAccess.Expression("[Parameters.paramDateFin]", typeof(System.DateTime));
            queryParameter6.Name = "@ExportateurID";
            queryParameter6.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter6.Value = new DevExpress.DataAccess.Expression("[Parameters.paramExportateur]", typeof(int));
            queryParameter7.Name = "@siteid";
            queryParameter7.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter7.Value = new DevExpress.DataAccess.Expression("[Parameters.paramSite]", typeof(int));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.Parameters.Add(queryParameter3);
            storedProcQuery1.Parameters.Add(queryParameter4);
            storedProcQuery1.Parameters.Add(queryParameter5);
            storedProcQuery1.Parameters.Add(queryParameter6);
            storedProcQuery1.Parameters.Add(queryParameter7);
            storedProcQuery1.StoredProcName = "Livraison_PrintList";
            this.sqlDataSource2.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource2.ResultSchemaSerializable = resources.GetString("sqlDataSource2.ResultSchemaSerializable");
            // 
            // paramSite
            // 
            this.paramSite.Name = "paramSite";
            this.paramSite.Type = typeof(int);
            this.paramSite.ValueInfo = "0";
            this.paramSite.Visible = false;
            // 
            // rptDeliveriesList
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
            this.sqlDataSource2});
            this.DataMember = "Livraison_PrintList";
            this.DataSource = this.sqlDataSource2;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 37, 100);
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
            this.paramTypeLivraisonText,
            this.paramFournisseurText,
            this.paramExportateurNom,
            this.paramCampagneText,
            this.paramSite,
            this.paramSiteText});
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
