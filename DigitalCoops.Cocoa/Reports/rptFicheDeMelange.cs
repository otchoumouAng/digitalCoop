using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptFicheDeMelange
/// </summary>
public class rptFicheDeMelange : DevExpress.XtraReports.UI.XtraReport
{
    private DetailBand Detail;
    private TopMarginBand TopMargin;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private BottomMarginBand BottomMargin;
    private ReportHeaderBand ReportHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell15;
    private XRControlStyle xrControlStyle1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell44;
    private XRPageInfo xrPageInfo2;
    private XRPageInfo xrPageInfo1;
    private DevExpress.XtraReports.Parameters.Parameter paramID;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell17;
    private XRTable xrTable5;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell60;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell48;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell43;
    private GroupFooterBand GroupFooter1;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell49;
    private CalculatedField MoyPondereMoisture;
    private CalculatedField MoyPondereMouldy;
    private CalculatedField MoyPondereWeevil;
    private CalculatedField moyPondereeSlaty;
    private CalculatedField moyPondereeFfa;
    private CalculatedField myPondereeMatierEtrangere;
    private CalculatedField moyPondereeSieving;
    private CalculatedField moyPondereeGrainage;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private XRPageInfo xrPageInfo3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell50;
    private XRLabel xrLabel3;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell62;
    private XRLabel xrLabel8;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private ReportFooterBand ReportFooter;
    private XRTableCell xrTableCell64;
    private XRLabel xrLabel9;
    private XRTableCell xrTableCell63;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell66;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptFicheDeMelange()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptFicheDeMelange));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.paramID = new DevExpress.XtraReports.Parameters.Parameter();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.MoyPondereMoisture = new DevExpress.XtraReports.UI.CalculatedField();
            this.MoyPondereMouldy = new DevExpress.XtraReports.UI.CalculatedField();
            this.MoyPondereWeevil = new DevExpress.XtraReports.UI.CalculatedField();
            this.moyPondereeSlaty = new DevExpress.XtraReports.UI.CalculatedField();
            this.moyPondereeFfa = new DevExpress.XtraReports.UI.CalculatedField();
            this.myPondereeMatierEtrangere = new DevExpress.XtraReports.UI.CalculatedField();
            this.moyPondereeSieving = new DevExpress.XtraReports.UI.CalculatedField();
            this.moyPondereeGrainage = new DevExpress.XtraReports.UI.CalculatedField();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.Detail.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseFont = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable5.BorderWidth = 0.5F;
            this.xrTable5.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTable5.SizeF = new System.Drawing.SizeF(747F, 25F);
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseBorderWidth = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell52,
            this.xrTableCell51,
            this.xrTableCell64,
            this.xrTableCell46,
            this.xrTableCell60,
            this.xrTableCell59,
            this.xrTableCell54,
            this.xrTableCell47,
            this.xrTableCell58,
            this.xrTableCell57,
            this.xrTableCell55,
            this.xrTableCell56,
            this.xrTableCell4,
            this.xrTableCell6,
            this.xrTableCell48});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.DateLivraison", "{0:dd/MM/yyyy}")});
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.Text = "xrTableCell52";
            this.xrTableCell52.Weight = 0.23203141745889933D;
            this.xrTableCell52.WordWrap = false;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.LIvraisonID")});
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.Text = "xrTableCell51";
            this.xrTableCell51.Weight = 0.25450898098730157D;
            this.xrTableCell51.WordWrap = false;
            // 
            // xrTableCell64
            // 
            this.xrTableCell64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.Immatriculation")});
            this.xrTableCell64.Name = "xrTableCell64";
            this.xrTableCell64.Text = "xrTableCell64";
            this.xrTableCell64.Weight = 0.2519303297253026D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.PrefixeProvenanceLivraison")});
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.Text = "xrTableCell46";
            this.xrTableCell46.Weight = 0.0848956307698163D;
            this.xrTableCell46.WordWrap = false;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.NbreSacs", "{0:n0}")});
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.Text = "xrTableCell60";
            this.xrTableCell60.Weight = 0.16800123109224591D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.Humidite", "{0:n2}")});
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.Text = "xrTableCell59";
            this.xrTableCell59.Weight = 0.15974759809711531D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.Moisie", "{0:n2}")});
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.Text = "xrTableCell54";
            this.xrTableCell54.Weight = 0.15430875716796338D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.Grainage", "{0:n2}")});
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.Text = "xrTableCell47";
            this.xrTableCell47.Weight = 0.18216456130901465D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.Mitee", "{0:n2}")});
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.Text = "xrTableCell58";
            this.xrTableCell58.Weight = 0.19514793256094287D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.Ardoisee", "{0:n2}")});
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.Text = "xrTableCell57";
            this.xrTableCell57.Weight = 0.17354110574227633D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.Ffa", "{0:n2}")});
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.Text = "xrTableCell55";
            this.xrTableCell55.Weight = 0.19615800655738103D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.MatiereEtrangere", "{0:0.00}")});
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.Text = "xrTableCell56";
            this.xrTableCell56.Weight = 0.19724671052483711D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.Dechets", "{0:0.00}")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.Weight = 0.17331991833063914D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.PoidsBrut", "{0:n0}")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.Weight = 0.18832859825104337D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.PoidsNet", "{0:n0}")});
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.Text = "xrTableCell48";
            this.xrTableCell48.Weight = 0.20719445932012348D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo3,
            this.xrPictureBox1,
            this.xrLabel1,
            this.xrLabel2});
            this.TopMargin.HeightF = 156.5F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo3
            // 
            this.xrPageInfo3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo3.Format = "Date Of Document : {0:dd/MM/yyyy HH:mm}";
            this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(428.6103F, 111.6203F);
            this.xrPageInfo3.Name = "xrPageInfo3";
            this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo3.SizeF = new System.Drawing.SizeF(316.723F, 18.33334F);
            this.xrPageInfo3.StylePriority.UseFont = false;
            this.xrPageInfo3.StylePriority.UseTextAlignment = false;
            this.xrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(28.94885F, 53.99999F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(162.8638F, 77.5F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBox1_BeforePrint);
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.AdresseSociete")});
            this.xrLabel1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(191.8127F, 83.9537F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(154.1667F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            this.xrLabel1.WordWrap = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.TelSociete")});
            this.xrLabel2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(191.8127F, 106.9536F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(154.1667F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrLabel2.WordWrap = false;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Italic);
            this.xrPageInfo2.Format = "Printed on {0:d}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(915.0832F, 4.435323F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(147.9167F, 11.5647F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
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
            // xrPageInfo1
            // 
            this.xrPageInfo1.Format = "Page {0} of {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(345.9794F, 66.99997F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrLabel5,
            this.xrLabel4,
            this.xrTable4,
            this.xrTable1});
            this.ReportHeader.HeightF = 245.7341F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel9
            // 
            this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.ProvenanceEtType")});
            this.xrLabel9.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(466.1103F, 0F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(280.8896F, 50.19073F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel5
            // 
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.NumeroProduction")});
            this.xrLabel5.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(286.0385F, 0F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(159.9409F, 50.19073F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "1000";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(286.0385F, 50.19073F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "FICHE DE MELANGE N° : ";
            // 
            // xrTable4
            // 
            this.xrTable4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 64.3574F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9,
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow2,
            this.xrTableRow8});
            this.xrTable4.SizeF = new System.Drawing.SizeF(747F, 125F);
            this.xrTable4.StylePriority.UseFont = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell65,
            this.xrTableCell66});
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCell65
            // 
            this.xrTableCell65.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell65.Name = "xrTableCell65";
            this.xrTableCell65.StylePriority.UseFont = false;
            this.xrTableCell65.StylePriority.UseTextAlignment = false;
            this.xrTableCell65.Text = "Melange N° :";
            this.xrTableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell65.Weight = 0.76512939983277117D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.NumeroMelange", " {0}")});
            this.xrTableCell66.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.StylePriority.UseFont = false;
            this.xrTableCell66.StylePriority.UseTextAlignment = false;
            this.xrTableCell66.Text = "xrTableCell66";
            this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell66.Weight = 5.8924796251083222D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell44,
            this.xrTableCell61,
            this.xrTableCell62,
            this.xrTableCell21});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.Text = "Exportateur : ";
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell38.Weight = 0.76512939983277117D;
            this.xrTableCell38.WordWrap = false;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.ExportateurNom", " {0}")});
            this.xrTableCell39.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseFont = false;
            this.xrTableCell39.StylePriority.UseTextAlignment = false;
            this.xrTableCell39.Text = "xrTableCell20";
            this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell39.Weight = 1.6486701078568711D;
            this.xrTableCell39.WordWrap = false;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseFont = false;
            this.xrTableCell44.StylePriority.UseTextAlignment = false;
            this.xrTableCell44.Text = "Campagne :";
            this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell44.Weight = 0.89651671679341072D;
            this.xrTableCell44.WordWrap = false;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.CampagneID", " {0}")});
            this.xrTableCell61.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.StylePriority.UseFont = false;
            this.xrTableCell61.StylePriority.UseTextAlignment = false;
            this.xrTableCell61.Text = "xrTableCell61";
            this.xrTableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell61.Weight = 1.1837434550279102D;
            // 
            // xrTableCell62
            // 
            this.xrTableCell62.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell62.Name = "xrTableCell62";
            this.xrTableCell62.StylePriority.UseFont = false;
            this.xrTableCell62.StylePriority.UseTextAlignment = false;
            this.xrTableCell62.Text = "Agent";
            this.xrTableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell62.Weight = 0.69363230683373289D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.AgentMelangeur", " {0}")});
            this.xrTableCell21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = "xrTableCell21";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell21.Weight = 1.4699170385963978D;
            this.xrTableCell21.WordWrap = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18,
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell19});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.Text = "Certification :";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell18.Weight = 0.76512939983277106D;
            this.xrTableCell18.WordWrap = false;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.CertificationDesignation", " {0}")});
            this.xrTableCell35.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseFont = false;
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            this.xrTableCell35.Text = "xrTableCell35";
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell35.Weight = 1.6486698549123031D;
            this.xrTableCell35.WordWrap = false;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseFont = false;
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            this.xrTableCell36.Text = "Est reusiné";
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell36.Weight = 0.89651670407239159D;
            this.xrTableCell36.WordWrap = false;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.EstReusine", " {0}")});
            this.xrTableCell23.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.Text = "xrTableCell23";
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell23.Weight = 1.1837437167892388D;
            this.xrTableCell23.WordWrap = false;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            this.xrTableCell24.Text = "Quart :";
            this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell24.Weight = 0.69363177686511179D;
            this.xrTableCell24.WordWrap = false;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.QuartNom", " {0}")});
            this.xrTableCell19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell19.Weight = 1.4699175724692779D;
            this.xrTableCell19.WordWrap = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.xrTableCell37,
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell42,
            this.xrTableCell17});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "Date : ";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell16.Weight = 0.76512939983277117D;
            this.xrTableCell16.WordWrap = false;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.DateProduction", " {0:dd/MM/yyyy}")});
            this.xrTableCell37.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseFont = false;
            this.xrTableCell37.StylePriority.UseTextAlignment = false;
            this.xrTableCell37.Text = "xrTableCell37";
            this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell37.Weight = 1.6486701387289251D;
            this.xrTableCell37.WordWrap = false;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseFont = false;
            this.xrTableCell40.StylePriority.UseTextAlignment = false;
            this.xrTableCell40.Text = "Date Melange";
            this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell40.Weight = 0.89651669528965572D;
            this.xrTableCell40.WordWrap = false;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.DateComposition", " {0:dd/MM/yyyy}")});
            this.xrTableCell41.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseFont = false;
            this.xrTableCell41.StylePriority.UseTextAlignment = false;
            this.xrTableCell41.Text = "xrTableCell41";
            this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell41.Weight = 1.1837426487894145D;
            this.xrTableCell41.WordWrap = false;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseFont = false;
            this.xrTableCell42.StylePriority.UseTextAlignment = false;
            this.xrTableCell42.Text = "Chef Quart :";
            this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell42.Weight = 0.69363315449324481D;
            this.xrTableCell42.WordWrap = false;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.ChefQuartNom", " {0}")});
            this.xrTableCell17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.Text = "xrTableCell17";
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell17.Weight = 1.4699169878070835D;
            this.xrTableCell17.WordWrap = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25,
            this.xrTableCell49});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.Text = "Commentaire :";
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell25.Weight = 0.76512939983277117D;
            this.xrTableCell25.WordWrap = false;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.Commentaire", " {0}")});
            this.xrTableCell49.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell49.Multiline = true;
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.StylePriority.UseFont = false;
            this.xrTableCell49.StylePriority.UseTextAlignment = false;
            this.xrTableCell49.Text = "xrTableCell49";
            this.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell49.Weight = 5.8924796251083231D;
            // 
            // xrTable1
            // 
            this.xrTable1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTable1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTable1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable1.ForeColor = System.Drawing.Color.White;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 200.5258F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(747F, 45.20833F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseForeColor = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell63,
            this.xrTableCell5,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell3,
            this.xrTableCell22,
            this.xrTableCell45,
            this.xrTableCell15});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Date";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.75066300097058236D;
            this.xrTableCell1.WordWrap = false;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Del. ID";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.82338173034031126D;
            this.xrTableCell2.WordWrap = false;
            // 
            // xrTableCell63
            // 
            this.xrTableCell63.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell63.Name = "xrTableCell63";
            this.xrTableCell63.StylePriority.UseFont = false;
            this.xrTableCell63.StylePriority.UseTextAlignment = false;
            this.xrTableCell63.Text = "Truck ID";
            this.xrTableCell63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell63.Weight = 0.81504007594745409D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "PR";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 0.27465221194559741D;
            this.xrTableCell5.WordWrap = false;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Bags";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 0.54351378624256608D;
            this.xrTableCell7.WordWrap = false;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell8.Multiline = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Moist.\r\n (%)";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 0.516812311695017D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Mdly. \r\n(%)";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell9.Weight = 0.49921682523791033D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell10.Multiline = true;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "Beans\r\n Ct.";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 0.58933485699342769D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell11.Multiline = true;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Weevil \r\n(%)";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell11.Weight = 0.63133795512668556D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell12.Multiline = true;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Slaty \r\n(%)";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 0.561436436529503D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell13.Multiline = true;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "FFA \r\n(%)";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell13.Weight = 0.63460618347920883D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.NullValueText = "0";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Frgn.\r\n Matt.";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 0.63812970082134535D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell22.Multiline = true;
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.Text = "Siev. \r\n(%)";
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell22.Weight = 0.56071939449463115D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.StylePriority.UseFont = false;
            this.xrTableCell45.StylePriority.UseTextAlignment = false;
            this.xrTableCell45.Text = "Gr Wgt(Kg)";
            this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell45.Weight = 0.60927661263674149D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTableCell15.Multiline = true;
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "Poids Net\r\n(Kg)";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell15.Weight = 0.67031047734255333D;
            // 
            // xrTable3
            // 
            this.xrTable3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTable3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xrTable3.ForeColor = System.Drawing.Color.White;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(119.4755F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(627.5245F, 25F);
            this.xrTable3.StylePriority.UseBackColor = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UseForeColor = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell26,
            this.xrTableCell27,
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell53,
            this.xrTableCell50,
            this.xrTableCell34});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseTextAlignment = false;
            this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.CanGrow = false;
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.Text = "Total :";
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell20.Weight = 1.0332809062546335D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.NbreSacs")});
            this.xrTableCell26.Name = "xrTableCell26";
            xrSummary1.FormatString = "{0:n0}";
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell26.Summary = xrSummary1;
            this.xrTableCell26.Weight = 0.46593190074354185D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.MoyPondereMoisture", "{0:n2}")});
            this.xrTableCell27.Name = "xrTableCell27";
            xrSummary2.FormatString = "{0:#.00}";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.IgnoreNullValues = true;
            this.xrTableCell27.Summary = xrSummary2;
            this.xrTableCell27.Weight = 0.44304230400889744D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.MoyPondereMouldy", "{0:n2}")});
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.Weight = 0.42795734924830531D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.moyPondereeGrainage", "{0:n2}")});
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.Weight = 0.50521324715337423D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.MoyPondereWeevil", "{0:n2}")});
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.Weight = 0.54122017201357242D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.moyPondereeSlaty", "{0:n2}")});
            this.xrTableCell31.Name = "xrTableCell31";
            xrSummary3.FormatString = "{0:#,#}";
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary3.IgnoreNullValues = true;
            this.xrTableCell31.Summary = xrSummary3;
            this.xrTableCell31.Weight = 0.48129615848132634D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.moyPondereeFfa", "{0:n2}")});
            this.xrTableCell32.Name = "xrTableCell32";
            xrSummary4.FormatString = "{0:#,#}";
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Avg;
            this.xrTableCell32.Summary = xrSummary4;
            this.xrTableCell32.Weight = 0.54402174474628406D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.MatiereEtrangere")});
            this.xrTableCell33.Name = "xrTableCell33";
            xrSummary5.FormatString = "{0:0.00}";
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell33.Summary = xrSummary5;
            this.xrTableCell33.Weight = 0.54704249894355306D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.Dechets")});
            this.xrTableCell53.Name = "xrTableCell53";
            xrSummary6.FormatString = "{0:0.00}";
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell53.Summary = xrSummary6;
            this.xrTableCell53.Weight = 0.48068105630630575D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.PoidsBrut")});
            this.xrTableCell50.Name = "xrTableCell50";
            xrSummary7.FormatString = "{0:n0}";
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell50.Summary = xrSummary7;
            this.xrTableCell50.Weight = 0.52230782378363694D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.PoidsNet")});
            this.xrTableCell34.Name = "xrTableCell34";
            xrSummary8.FormatString = "{0:n0}";
            xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell34.Summary = xrSummary8;
            this.xrTableCell34.Weight = 0.57463044165373089D;
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
            // 
            // paramID
            // 
            this.paramID.Description = "Ordre Production ID";
            this.paramID.Name = "paramID";
            this.paramID.Type = typeof(System.Guid);
            this.paramID.ValueInfo = "00000000-0000-0000-0000-000000000000";
            this.paramID.Visible = false;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("MelangeDesignation", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable2
            // 
            this.xrTable2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable2.SizeF = new System.Drawing.SizeF(270.8342F, 25F);
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.xrTableCell43});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.Text = "Melange :";
            this.xrTableCell14.Weight = 0.616666603088379D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.MelangeDesignation")});
            this.xrTableCell43.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseFont = false;
            this.xrTableCell43.StylePriority.UseTextAlignment = false;
            this.xrTableCell43.Text = "xrTableCell43";
            this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            this.xrTableCell43.Weight = 1.3833333969116211D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.GroupFooter1.HeightF = 25F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // MoyPondereMoisture
            // 
            this.MoyPondereMoisture.DataMember = "V2_MelangeProduction_PrintFiche";
            this.MoyPondereMoisture.Expression = "[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([Humidite] * [PoidsNet]) /" +
    "[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([PoidsNet])";
            this.MoyPondereMoisture.Name = "MoyPondereMoisture";
            // 
            // MoyPondereMouldy
            // 
            this.MoyPondereMouldy.DataMember = "V2_MelangeProduction_PrintFiche";
            this.MoyPondereMouldy.Expression = "[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([Moisie] * [PoidsNet]) /[]" +
    "[[MelangeDesignation] == [^.MelangeDesignation]].Sum([PoidsNet])";
            this.MoyPondereMouldy.Name = "MoyPondereMouldy";
            // 
            // MoyPondereWeevil
            // 
            this.MoyPondereWeevil.DataMember = "V2_MelangeProduction_PrintFiche";
            this.MoyPondereWeevil.Expression = "[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([Mitee] * [PoidsNet]) /[][" +
    "[MelangeDesignation] == [^.MelangeDesignation]].Sum([PoidsNet])";
            this.MoyPondereWeevil.Name = "MoyPondereWeevil";
            // 
            // moyPondereeSlaty
            // 
            this.moyPondereeSlaty.DataMember = "V2_MelangeProduction_PrintFiche";
            this.moyPondereeSlaty.Expression = "[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([Ardoisee] * [PoidsNet]) /" +
    "[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([PoidsNet])";
            this.moyPondereeSlaty.Name = "moyPondereeSlaty";
            // 
            // moyPondereeFfa
            // 
            this.moyPondereeFfa.DataMember = "V2_MelangeProduction_PrintFiche";
            this.moyPondereeFfa.Expression = "[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([Ffa] * [PoidsNet]) /[][[M" +
    "elangeDesignation] == [^.MelangeDesignation]].Sum([PoidsNet])";
            this.moyPondereeFfa.Name = "moyPondereeFfa";
            // 
            // myPondereeMatierEtrangere
            // 
            this.myPondereeMatierEtrangere.DataMember = "V2_MelangeProduction_PrintFiche";
            this.myPondereeMatierEtrangere.Expression = "[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([MatiereEtrangere] * [Poid" +
    "sNet]) /[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([PoidsNet])";
            this.myPondereeMatierEtrangere.Name = "myPondereeMatierEtrangere";
            // 
            // moyPondereeSieving
            // 
            this.moyPondereeSieving.DataMember = "V2_MelangeProduction_PrintFiche";
            this.moyPondereeSieving.Expression = "[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([Dechets] * [PoidsNet]) /[" +
    "][[MelangeDesignation] == [^.MelangeDesignation]].Sum([PoidsNet])";
            this.moyPondereeSieving.Name = "moyPondereeSieving";
            // 
            // moyPondereeGrainage
            // 
            this.moyPondereeGrainage.DataMember = "V2_MelangeProduction_PrintFiche";
            this.moyPondereeGrainage.Expression = "[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([Grainage] * [PoidsNet]) /" +
    "[][[MelangeDesignation] == [^.MelangeDesignation]].Sum([PoidsNet])";
            this.moyPondereeGrainage.Name = "moyPondereeGrainage";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel3.BorderWidth = 0.5F;
            this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_MelangeProduction_PrintFiche.InstructionsMelange")});
            this.xrLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(487.147F, 10F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(255.0266F, 78.83331F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseBorderWidth = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Inst";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(28.94885F, 113.6667F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "LABO TV";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(323.5F, 113.6667F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Supervisor";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(615.9794F, 113.6667F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Director";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel3,
            this.xrLabel6});
            this.ReportFooter.HeightF = 146.6667F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.PrintAtBottom = true;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "TMS2017_ReportServer";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "V2_MelangeProduction_PrintFiche";
            queryParameter1.Name = "@ID";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.paramID]", typeof(System.Guid));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.StoredProcName = "V2_MelangeProduction_PrintFiche";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // rptFicheDeMelange
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1,
            this.GroupFooter1,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.MoyPondereMoisture,
            this.MoyPondereMouldy,
            this.MoyPondereWeevil,
            this.moyPondereeSlaty,
            this.moyPondereeFfa,
            this.myPondereeMatierEtrangere,
            this.moyPondereeSieving,
            this.moyPondereeGrainage});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "V2_MelangeProduction_PrintFiche";
            this.DataSource = this.sqlDataSource1;
            this.Margins = new System.Drawing.Printing.Margins(40, 40, 156, 100);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.paramID});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
            this.Version = "17.1";
            this.Watermark.Font = new System.Drawing.Font("Verdana", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.Watermark.ForeColor = System.Drawing.Color.DarkGray;
            this.Watermark.TextTransparency = 193;
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void SetFieldsToZero(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel lbl = sender as XRLabel;
        if (string.IsNullOrEmpty(lbl.Text))
            lbl.Text = "0";
    }

    private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrPictureBox1.ImageUrl = "~\\Images\\Logo\\logo.jpg";
    }
}
