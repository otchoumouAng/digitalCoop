using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptCommercialInvoice
/// </summary>
public class rptCommercialInvoice : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRPageInfo xrPageInfo1;
    private DevExpress.XtraReports.Parameters.Parameter paramID;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private PageHeaderBand PageHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell3;
    private XRTable xrTable2;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell14;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell22;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell30;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell38;
    private XRTable xrTable3;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell39;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTableRow xrTableRow17;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell60;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell52;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell62;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell64;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell78;
    private XRTableCell xrTableCell79;
    private XRTableCell xrTableCell80;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell73;
    private XRTableCell xrTableCell76;
    private XRTableRow xrTableRow21;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell72;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell70;
    private XRTableCell xrTableCell71;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell74;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell77;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell67;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell83;
    private XRTableCell xrTableCell84;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell68;
    private XRTableCell xrTableCell81;
    private XRTableCell xrTableCell82;
    private XRTableCell xrTableCell85;
    private XRTableCell xrTableCell86;
    private XRTableCell xrTableCell87;
    private XRTableCell xrTableCell88;
    private XRTableCell xrTableCell89;
    private XRTableCell xrTableCell90;
    private XRTableCell xrTableCell91;
    private XRTableCell xrTableCell92;
    private CalculatedField MontantBrutCfa;
    private CalculatedField MontantATCfa;
    private CalculatedField DeductionApcCfa;
    private CalculatedField DeductionBlankCfa;
    private CalculatedField AutreDeductionCfa;
    private CalculatedField MontantNetCfa;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptCommercialInvoice()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptCommercialInvoice));
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell85 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell86 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell87 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell92 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.paramID = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell84 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.MontantBrutCfa = new DevExpress.XtraReports.UI.CalculatedField();
            this.MontantATCfa = new DevExpress.XtraReports.UI.CalculatedField();
            this.DeductionApcCfa = new DevExpress.XtraReports.UI.CalculatedField();
            this.DeductionBlankCfa = new DevExpress.XtraReports.UI.CalculatedField();
            this.AutreDeductionCfa = new DevExpress.XtraReports.UI.CalculatedField();
            this.MontantNetCfa = new DevExpress.XtraReports.UI.CalculatedField();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.Detail.HeightF = 570.8333F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable3
            // 
            this.xrTable3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13,
            this.xrTableRow14,
            this.xrTableRow17,
            this.xrTableRow18,
            this.xrTableRow16,
            this.xrTableRow15,
            this.xrTableRow19,
            this.xrTableRow23,
            this.xrTableRow22,
            this.xrTableRow21,
            this.xrTableRow24,
            this.xrTableRow25,
            this.xrTableRow20});
            this.xrTable3.SizeF = new System.Drawing.SizeF(650F, 446.875F);
            this.xrTable3.StylePriority.UseFont = false;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell40,
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell39,
            this.xrTableCell16});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 1D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell40.StylePriority.UseBorders = false;
            this.xrTableCell40.StylePriority.UsePadding = false;
            this.xrTableCell40.Text = "N°OF BAGS";
            this.xrTableCell40.Weight = 0.71153847327599162D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.NbreSacs")});
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell36.StylePriority.UseBorders = false;
            this.xrTableCell36.StylePriority.UsePadding = false;
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell36.Weight = 0.55250630305363579D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseBorders = false;
            this.xrTableCell37.Weight = 0.57179525522085339D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseBorders = false;
            this.xrTableCell39.Weight = 0.54992450420673067D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorders = false;
            this.xrTableCell16.Weight = 0.6142356561573511D;
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell41,
            this.xrTableCell42,
            this.xrTableCell43,
            this.xrTableCell44,
            this.xrTableCell17});
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 1D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell41.StylePriority.UseBorders = false;
            this.xrTableCell41.StylePriority.UsePadding = false;
            this.xrTableCell41.Text = "QUANTITY (Tons)";
            this.xrTableCell41.Weight = 0.71153847327599162D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.Quantite", "{0:n2}")});
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell42.StylePriority.UseBorders = false;
            this.xrTableCell42.StylePriority.UsePadding = false;
            this.xrTableCell42.StylePriority.UseTextAlignment = false;
            this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell42.Weight = 0.55250630305363579D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseBorders = false;
            this.xrTableCell43.Weight = 0.57179525522085339D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseBorders = false;
            this.xrTableCell44.Weight = 0.54992450420673067D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseBorders = false;
            this.xrTableCell17.Weight = 0.6142356561573511D;
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell53,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell56,
            this.xrTableCell68});
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.Weight = 1D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell53.StylePriority.UseBorders = false;
            this.xrTableCell53.StylePriority.UsePadding = false;
            this.xrTableCell53.Text = "GROSS WT. kgs";
            this.xrTableCell53.Weight = 0.71153847327599162D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.PoidsBrutTheorique", "{0:#,#}")});
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell54.StylePriority.UseBorders = false;
            this.xrTableCell54.StylePriority.UsePadding = false;
            this.xrTableCell54.StylePriority.UseTextAlignment = false;
            this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell54.Weight = 0.55250630305363579D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.StylePriority.UseBorders = false;
            this.xrTableCell55.Weight = 0.57179525522085339D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.StylePriority.UseBorders = false;
            this.xrTableCell56.Weight = 0.54992450420673067D;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.StylePriority.UseBorders = false;
            this.xrTableCell68.Weight = 0.6142356561573511D;
            // 
            // xrTableRow18
            // 
            this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell57,
            this.xrTableCell58,
            this.xrTableCell59,
            this.xrTableCell60,
            this.xrTableCell81});
            this.xrTableRow18.Name = "xrTableRow18";
            this.xrTableRow18.Weight = 1D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell57.StylePriority.UseBorders = false;
            this.xrTableCell57.StylePriority.UsePadding = false;
            this.xrTableCell57.Text = "NET WT. kgs";
            this.xrTableCell57.Weight = 0.71153847327599162D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.PoidsNetTheorique", "{0:#,#}")});
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell58.StylePriority.UseBorders = false;
            this.xrTableCell58.StylePriority.UsePadding = false;
            this.xrTableCell58.StylePriority.UseTextAlignment = false;
            this.xrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell58.Weight = 0.55250630305363579D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.StylePriority.UseBorders = false;
            this.xrTableCell59.Weight = 0.57179525522085339D;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.StylePriority.UseBorders = false;
            this.xrTableCell60.Weight = 0.54992450420673067D;
            // 
            // xrTableCell81
            // 
            this.xrTableCell81.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell81.Name = "xrTableCell81";
            this.xrTableCell81.StylePriority.UseBorders = false;
            this.xrTableCell81.Weight = 0.6142356561573511D;
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell49,
            this.xrTableCell50,
            this.xrTableCell51,
            this.xrTableCell52,
            this.xrTableCell82});
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.Weight = 1D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell49.StylePriority.UseBorders = false;
            this.xrTableCell49.StylePriority.UsePadding = false;
            this.xrTableCell49.Text = "CIF PRICE / 1000kgs";
            this.xrTableCell49.Weight = 0.71153847327599162D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseBorders = false;
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell50.Weight = 0.55250630305363579D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.PrixCAF", "{0:#,#.00#}")});
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell51.StylePriority.UseBorders = false;
            this.xrTableCell51.StylePriority.UsePadding = false;
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell51.Weight = 0.57179525522085339D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseBorders = false;
            this.xrTableCell52.Weight = 0.54992450420673067D;
            // 
            // xrTableCell82
            // 
            this.xrTableCell82.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell82.Name = "xrTableCell82";
            this.xrTableCell82.StylePriority.UseBorders = false;
            this.xrTableCell82.Weight = 0.6142356561573511D;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell45,
            this.xrTableCell46,
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell85});
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 1D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell45.StylePriority.UseBorders = false;
            this.xrTableCell45.StylePriority.UsePadding = false;
            this.xrTableCell45.Text = "FOB PRICE / 1000kgs";
            this.xrTableCell45.Weight = 0.71153847327599162D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseBorders = false;
            this.xrTableCell46.StylePriority.UseTextAlignment = false;
            this.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell46.Weight = 0.55250630305363579D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.PrixFob", "{0:#,#.00#}")});
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell47.StylePriority.UseBorders = false;
            this.xrTableCell47.StylePriority.UsePadding = false;
            this.xrTableCell47.StylePriority.UseTextAlignment = false;
            this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell47.Weight = 0.57179525522085339D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.MontantBrut", "{0:#,#.000}")});
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell48.StylePriority.UseBorders = false;
            this.xrTableCell48.StylePriority.UsePadding = false;
            this.xrTableCell48.StylePriority.UseTextAlignment = false;
            this.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell48.Weight = 0.54992450420673067D;
            // 
            // xrTableCell85
            // 
            this.xrTableCell85.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell85.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MontantBrutCfa", "{0:#,#.000}")});
            this.xrTableCell85.Name = "xrTableCell85";
            this.xrTableCell85.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell85.StylePriority.UseBorders = false;
            this.xrTableCell85.StylePriority.UsePadding = false;
            this.xrTableCell85.StylePriority.UseTextAlignment = false;
            this.xrTableCell85.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell85.Weight = 0.6142356561573511D;
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell61,
            this.xrTableCell62,
            this.xrTableCell63,
            this.xrTableCell64,
            this.xrTableCell86});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 1D;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.StylePriority.UseBorders = false;
            this.xrTableCell61.Weight = 0.71153847327599162D;
            // 
            // xrTableCell62
            // 
            this.xrTableCell62.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell62.Name = "xrTableCell62";
            this.xrTableCell62.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell62.StylePriority.UseBorders = false;
            this.xrTableCell62.StylePriority.UsePadding = false;
            this.xrTableCell62.StylePriority.UseTextAlignment = false;
            this.xrTableCell62.Text = "AT";
            this.xrTableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell62.Weight = 0.55250630305363579D;
            // 
            // xrTableCell63
            // 
            this.xrTableCell63.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell63.Name = "xrTableCell63";
            this.xrTableCell63.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell63.StylePriority.UseBorders = false;
            this.xrTableCell63.StylePriority.UsePadding = false;
            this.xrTableCell63.StylePriority.UseTextAlignment = false;
            this.xrTableCell63.Text = "99%";
            this.xrTableCell63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell63.Weight = 0.57179525522085339D;
            // 
            // xrTableCell64
            // 
            this.xrTableCell64.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.MontantAT", "{0:#,#.000}")});
            this.xrTableCell64.Name = "xrTableCell64";
            this.xrTableCell64.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell64.StylePriority.UseBorders = false;
            this.xrTableCell64.StylePriority.UsePadding = false;
            this.xrTableCell64.StylePriority.UseTextAlignment = false;
            this.xrTableCell64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell64.Weight = 0.54992450420673067D;
            // 
            // xrTableCell86
            // 
            this.xrTableCell86.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell86.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MontantATCfa", "{0:#,#.000}")});
            this.xrTableCell86.Name = "xrTableCell86";
            this.xrTableCell86.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell86.StylePriority.UseBorders = false;
            this.xrTableCell86.StylePriority.UsePadding = false;
            this.xrTableCell86.StylePriority.UseTextAlignment = false;
            this.xrTableCell86.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell86.Weight = 0.6142356561573511D;
            // 
            // xrTableRow23
            // 
            this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell77,
            this.xrTableCell78,
            this.xrTableCell79,
            this.xrTableCell80,
            this.xrTableCell87});
            this.xrTableRow23.Name = "xrTableRow23";
            this.xrTableRow23.Weight = 1D;
            // 
            // xrTableCell77
            // 
            this.xrTableCell77.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell77.Name = "xrTableCell77";
            this.xrTableCell77.StylePriority.UseBorders = false;
            this.xrTableCell77.Weight = 0.71153847327599162D;
            // 
            // xrTableCell78
            // 
            this.xrTableCell78.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell78.Name = "xrTableCell78";
            this.xrTableCell78.StylePriority.UseBorders = false;
            this.xrTableCell78.StylePriority.UseTextAlignment = false;
            this.xrTableCell78.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell78.Weight = 0.55250630305363579D;
            // 
            // xrTableCell79
            // 
            this.xrTableCell79.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell79.Name = "xrTableCell79";
            this.xrTableCell79.StylePriority.UseBorders = false;
            this.xrTableCell79.StylePriority.UseTextAlignment = false;
            this.xrTableCell79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell79.Weight = 0.57179525522085339D;
            // 
            // xrTableCell80
            // 
            this.xrTableCell80.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell80.Name = "xrTableCell80";
            this.xrTableCell80.StylePriority.UseBorders = false;
            this.xrTableCell80.StylePriority.UseTextAlignment = false;
            this.xrTableCell80.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell80.Weight = 0.54992450420673067D;
            // 
            // xrTableCell87
            // 
            this.xrTableCell87.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell87.Name = "xrTableCell87";
            this.xrTableCell87.StylePriority.UseBorders = false;
            this.xrTableCell87.StylePriority.UseTextAlignment = false;
            this.xrTableCell87.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell87.Weight = 0.6142356561573511D;
            // 
            // xrTableRow22
            // 
            this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell73,
            this.xrTableCell76,
            this.xrTableCell88});
            this.xrTableRow22.Name = "xrTableRow22";
            this.xrTableRow22.Weight = 1D;
            // 
            // xrTableCell73
            // 
            this.xrTableCell73.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell73.Name = "xrTableCell73";
            this.xrTableCell73.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell73.StylePriority.UseBorders = false;
            this.xrTableCell73.StylePriority.UsePadding = false;
            this.xrTableCell73.Text = "BLANK PREFINANCE REIMBURSEMENT";
            this.xrTableCell73.Weight = 1.835840066763071D;
            // 
            // xrTableCell76
            // 
            this.xrTableCell76.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell76.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.DeductionFinancementBlank", "-  {0:#,#.000}")});
            this.xrTableCell76.Name = "xrTableCell76";
            this.xrTableCell76.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell76.StylePriority.UseBorders = false;
            this.xrTableCell76.StylePriority.UsePadding = false;
            this.xrTableCell76.StylePriority.UseTextAlignment = false;
            this.xrTableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell76.Weight = 0.54992446899414027D;
            // 
            // xrTableCell88
            // 
            this.xrTableCell88.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell88.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DeductionBlankCfa", "- {0:#,#}")});
            this.xrTableCell88.Name = "xrTableCell88";
            this.xrTableCell88.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell88.StylePriority.UseBorders = false;
            this.xrTableCell88.StylePriority.UsePadding = false;
            this.xrTableCell88.StylePriority.UseTextAlignment = false;
            this.xrTableCell88.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell88.Weight = 0.6142356201733703D;
            // 
            // xrTableRow21
            // 
            this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell69,
            this.xrTableCell72,
            this.xrTableCell89});
            this.xrTableRow21.Name = "xrTableRow21";
            this.xrTableRow21.Weight = 1D;
            // 
            // xrTableCell69
            // 
            this.xrTableCell69.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell69.Name = "xrTableCell69";
            this.xrTableCell69.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell69.StylePriority.UseBorders = false;
            this.xrTableCell69.StylePriority.UsePadding = false;
            this.xrTableCell69.Text = "APC PREFINANCE REIMBURSEMENT";
            this.xrTableCell69.Weight = 1.8358401371882513D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell72.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.DeductionFinancementAPC", "- {0:#,#.000}")});
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell72.StylePriority.UseBorders = false;
            this.xrTableCell72.StylePriority.UsePadding = false;
            this.xrTableCell72.StylePriority.UseTextAlignment = false;
            this.xrTableCell72.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell72.Weight = 0.54992439856896014D;
            // 
            // xrTableCell89
            // 
            this.xrTableCell89.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell89.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DeductionApcCfa", "- {0:#,#}")});
            this.xrTableCell89.Name = "xrTableCell89";
            this.xrTableCell89.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell89.StylePriority.UseBorders = false;
            this.xrTableCell89.StylePriority.UsePadding = false;
            this.xrTableCell89.StylePriority.UseTextAlignment = false;
            this.xrTableCell89.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell89.Weight = 0.61423554820540949D;
            // 
            // xrTableRow24
            // 
            this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell70,
            this.xrTableCell71,
            this.xrTableCell90});
            this.xrTableRow24.Name = "xrTableRow24";
            this.xrTableRow24.Weight = 1D;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell70.Name = "xrTableCell70";
            this.xrTableCell70.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell70.StylePriority.UseBorders = false;
            this.xrTableCell70.StylePriority.UsePadding = false;
            this.xrTableCell70.Text = "OTHER DEDUCTION";
            this.xrTableCell70.Weight = 1.8358401371882513D;
            // 
            // xrTableCell71
            // 
            this.xrTableCell71.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell71.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.AutresDeduction", "- {0:#,#.000}")});
            this.xrTableCell71.Name = "xrTableCell71";
            this.xrTableCell71.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell71.StylePriority.UseBorders = false;
            this.xrTableCell71.StylePriority.UsePadding = false;
            this.xrTableCell71.StylePriority.UseTextAlignment = false;
            this.xrTableCell71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell71.Weight = 0.54992439856896014D;
            // 
            // xrTableCell90
            // 
            this.xrTableCell90.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell90.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AutreDeductionCfa", "- {0:#,#}")});
            this.xrTableCell90.Name = "xrTableCell90";
            this.xrTableCell90.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell90.StylePriority.UseBorders = false;
            this.xrTableCell90.StylePriority.UsePadding = false;
            this.xrTableCell90.StylePriority.UseTextAlignment = false;
            this.xrTableCell90.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell90.Weight = 0.61423554820540949D;
            // 
            // xrTableRow25
            // 
            this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell91});
            this.xrTableRow25.Name = "xrTableRow25";
            this.xrTableRow25.Weight = 1D;
            // 
            // xrTableCell74
            // 
            this.xrTableCell74.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell74.Name = "xrTableCell74";
            this.xrTableCell74.StylePriority.UseBorders = false;
            this.xrTableCell74.StylePriority.UseFont = false;
            this.xrTableCell74.StylePriority.UseTextAlignment = false;
            this.xrTableCell74.Weight = 1.8358401371882513D;
            // 
            // xrTableCell75
            // 
            this.xrTableCell75.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell75.Name = "xrTableCell75";
            this.xrTableCell75.StylePriority.UseBorders = false;
            this.xrTableCell75.StylePriority.UseFont = false;
            this.xrTableCell75.StylePriority.UseTextAlignment = false;
            this.xrTableCell75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell75.Weight = 0.54992439856896014D;
            // 
            // xrTableCell91
            // 
            this.xrTableCell91.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell91.Name = "xrTableCell91";
            this.xrTableCell91.StylePriority.UseBorders = false;
            this.xrTableCell91.StylePriority.UseTextAlignment = false;
            this.xrTableCell91.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell91.Weight = 0.61423554820540949D;
            // 
            // xrTableRow20
            // 
            this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell67,
            this.xrTableCell65,
            this.xrTableCell66,
            this.xrTableCell92});
            this.xrTableRow20.Name = "xrTableRow20";
            this.xrTableRow20.Weight = 1D;
            // 
            // xrTableCell67
            // 
            this.xrTableCell67.BackColor = System.Drawing.Color.Transparent;
            this.xrTableCell67.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell67.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell67.Name = "xrTableCell67";
            this.xrTableCell67.StylePriority.UseBackColor = false;
            this.xrTableCell67.StylePriority.UseBorders = false;
            this.xrTableCell67.StylePriority.UseFont = false;
            this.xrTableCell67.StylePriority.UseTextAlignment = false;
            this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell67.Weight = 0.63461541395920973D;
            // 
            // xrTableCell65
            // 
            this.xrTableCell65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell65.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell65.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell65.Name = "xrTableCell65";
            this.xrTableCell65.StylePriority.UseBackColor = false;
            this.xrTableCell65.StylePriority.UseBorders = false;
            this.xrTableCell65.StylePriority.UseFont = false;
            this.xrTableCell65.StylePriority.UseTextAlignment = false;
            this.xrTableCell65.Text = "BALANCE DUE COCOA LTD";
            this.xrTableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell65.Weight = 1.2012247232290416D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell66.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.MontantNet", "{0:#,#.000}")});
            this.xrTableCell66.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell66.StylePriority.UseBackColor = false;
            this.xrTableCell66.StylePriority.UseBorders = false;
            this.xrTableCell66.StylePriority.UseFont = false;
            this.xrTableCell66.StylePriority.UsePadding = false;
            this.xrTableCell66.StylePriority.UseTextAlignment = false;
            this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell66.Weight = 0.54992439856896014D;
            // 
            // xrTableCell92
            // 
            this.xrTableCell92.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell92.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell92.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "MontantNetCfa", "{0:#,#.000}")});
            this.xrTableCell92.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell92.Name = "xrTableCell92";
            this.xrTableCell92.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell92.StylePriority.UseBackColor = false;
            this.xrTableCell92.StylePriority.UseBorders = false;
            this.xrTableCell92.StylePriority.UseFont = false;
            this.xrTableCell92.StylePriority.UsePadding = false;
            this.xrTableCell92.StylePriority.UseTextAlignment = false;
            this.xrTableCell92.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell92.Weight = 0.61423554820540949D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrLabel1,
            this.xrLabel2,
            this.xrPageInfo1});
            this.TopMargin.HeightF = 102.625F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 34.62495F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(154.1667F, 65.83333F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBox1_BeforePrint);
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.ExportateurAdresse")});
            this.xrLabel1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(154.1667F, 52.12497F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(175.4768F, 22.99999F);
            this.xrLabel1.StylePriority.UseFont = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.ExportateurTelFixe")});
            this.xrLabel2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(154.1667F, 75.12497F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(246.1077F, 22.99999F);
            this.xrLabel2.StylePriority.UseFont = false;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.Format = "Date du document : {0}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(451.2603F, 82.12495F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(198.7397F, 18.33334F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 1.916663F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "TMS2017_ReportServer";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "V2_FactureCommerciale_PrintInvoice";
            queryParameter1.Name = "@ID";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.paramID]", typeof(System.Guid));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.StoredProcName = "V2_FactureCommerciale_PrintInvoice";
            storedProcQuery2.Name = "Parametres_Select";
            storedProcQuery2.StoredProcName = "Parametres_Select";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1,
            storedProcQuery2});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // paramID
            // 
            this.paramID.Description = "ID";
            this.paramID.Name = "paramID";
            this.paramID.Type = typeof(System.Guid);
            this.paramID.ValueInfo = "00000000-0000-0000-0000-000000000000";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrLabel4});
            this.ReportHeader.HeightF = 71.875F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.StylePriority.UseBorders = false;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(314.9185F, 50.19073F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "COMMERCIAL INVOICE N° : ";
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.Numero")});
            this.xrLabel4.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(314.9185F, 10.00001F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(335.0815F, 50.19073F);
            this.xrLabel4.StylePriority.UseFont = false;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.xrTable2});
            this.PageHeader.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PageHeader.HeightF = 336.4583F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.StylePriority.UseFont = false;
            this.PageHeader.StylePriority.UseTextAlignment = false;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(367.7744F, 10.00001F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3});
            this.xrTable1.SizeF = new System.Drawing.SizeF(282.2256F, 77.49999F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.ClientNom", "To:  {0}")});
            this.xrTableCell1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Weight = 3D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell2});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.Weight = 0.31879764049185089D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.ClientAdresse")});
            this.xrTableCell2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Weight = 2.6812023595081489D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell3});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.Weight = 0.31879765971165575D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.ClientTelFixe")});
            this.xrTableCell3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Weight = 2.681202340288344D;
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 110.4167F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6,
            this.xrTableRow9,
            this.xrTableRow8,
            this.xrTableRow11,
            this.xrTableRow10,
            this.xrTableRow27});
            this.xrTable2.SizeF = new System.Drawing.SizeF(650F, 200F);
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell6.StylePriority.UsePadding = false;
            this.xrTableCell6.Text = "Contrat N° :";
            this.xrTableCell6.Weight = 0.53174623761858264D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.ContratRefClient")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Weight = 1.4682537623814174D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Weight = 1D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell9.StylePriority.UsePadding = false;
            this.xrTableCell9.Text = "Contract Price :";
            this.xrTableCell9.Weight = 0.53174623761858264D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.PrixCAF", "{0:#,#.00# €}")});
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Weight = 1.4682537623814174D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Weight = 1D;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell18,
            this.xrTableCell13,
            this.xrTableCell19,
            this.xrTableCell14});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell12.StylePriority.UsePadding = false;
            this.xrTableCell12.Text = "Mark & Origin :";
            this.xrTableCell12.Weight = 0.53174623761858264D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.ExportateurNom")});
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.Weight = 0.8356037321544828D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.CertificationDesignation")});
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Weight = 0.33007027762276775D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.ModeTraitementDesignation")});
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Weight = 0.8025797526041667D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.OrigineNom")});
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Weight = 0.5D;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25});
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell23.StylePriority.UsePadding = false;
            this.xrTableCell23.Text = "B/L :";
            this.xrTableCell23.Weight = 0.53174623761858264D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.NumeroBL")});
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.Weight = 1.4682537623814174D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.ConditionnementDesignation")});
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Weight = 1D;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell22});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell20.StylePriority.UsePadding = false;
            this.xrTableCell20.Text = "Vessel :";
            this.xrTableCell20.Weight = 0.53174623761858264D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.NavireNom")});
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Weight = 0.8356037321544828D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.ETA", "{0:dd/MM/yyyy}")});
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseTextAlignment = false;
            this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell32.Weight = 0.58503069196428559D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            this.xrTableCell33.Text = "Destionation:";
            this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell33.Weight = 0.43338361467633929D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.DestinationExportNom")});
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell22.Weight = 0.61423572358630962D;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29,
            this.xrTableCell31,
            this.xrTableCell30});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 1D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell29.StylePriority.UsePadding = false;
            this.xrTableCell29.Text = "Conditions :";
            this.xrTableCell29.Weight = 0.53174623761858264D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell31.Weight = 0.52579356602260041D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.ConditionLivraisonDesignation")});
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell30.Weight = 1.9424601963588168D;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell26,
            this.xrTableCell27,
            this.xrTableCell28});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell26.StylePriority.UsePadding = false;
            this.xrTableCell26.Text = "Domiciliated at :";
            this.xrTableCell26.Weight = 0.53174623761858264D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.DomiciliationNom")});
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Weight = 1.4682537623814174D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.CompteBancaireNumero", "Acct N° {0}")});
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.Weight = 1D;
            // 
            // xrTableRow27
            // 
            this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell83,
            this.xrTableCell84});
            this.xrTableRow27.Name = "xrTableRow27";
            this.xrTableRow27.Weight = 1D;
            // 
            // xrTableCell83
            // 
            this.xrTableCell83.Name = "xrTableCell83";
            this.xrTableCell83.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell83.StylePriority.UsePadding = false;
            this.xrTableCell83.Text = "Lot N°s:";
            this.xrTableCell83.Weight = 0.53174623761858264D;
            // 
            // xrTableCell84
            // 
            this.xrTableCell84.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_FactureCommerciale_PrintInvoice.Lots")});
            this.xrTableCell84.Name = "xrTableCell84";
            this.xrTableCell84.Weight = 2.4682537623814174D;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.GroupHeader1.HeightF = 35F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable4
            // 
            this.xrTable4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTable4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTable4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
            this.xrTable4.SizeF = new System.Drawing.SizeF(650F, 35F);
            this.xrTable4.StylePriority.UseBackColor = false;
            this.xrTable4.StylePriority.UseBorderColor = false;
            this.xrTable4.StylePriority.UseFont = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell38,
            this.xrTableCell15});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 1D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell34.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseBackColor = false;
            this.xrTableCell34.StylePriority.UseBorderColor = false;
            this.xrTableCell34.Text = "Designation";
            this.xrTableCell34.Weight = 2.375D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.Text = "Unit Price (Euro)";
            this.xrTableCell35.Weight = 1.0743395364224515D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.Text = "Amount (Euro)";
            this.xrTableCell38.Weight = 1.0332469634496948D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Text = "Montant (CFA)";
            this.xrTableCell15.Weight = 1.1540804710743187D;
            // 
            // MontantBrutCfa
            // 
            this.MontantBrutCfa.Expression = "Round([V2_FactureCommerciale_PrintInvoice.MontantBrut]*[V2_FactureCommerciale_Pri" +
    "ntInvoice.TauxConv])";
            this.MontantBrutCfa.FieldType = DevExpress.XtraReports.UI.FieldType.Double;
            this.MontantBrutCfa.Name = "MontantBrutCfa";
            // 
            // MontantATCfa
            // 
            this.MontantATCfa.Expression = "Round([V2_FactureCommerciale_PrintInvoice.MontantAT]*[V2_FactureCommerciale_Print" +
    "Facture.TauxConv])";
            this.MontantATCfa.FieldType = DevExpress.XtraReports.UI.FieldType.Double;
            this.MontantATCfa.Name = "MontantATCfa";
            // 
            // DeductionApcCfa
            // 
            this.DeductionApcCfa.Expression = "Round([V2_FactureCommerciale_PrintInvoice.DeductionFinancementAPC]*[V2_FactureCom" +
    "merciale_PrintInvoice.TauxConv])";
            this.DeductionApcCfa.FieldType = DevExpress.XtraReports.UI.FieldType.Double;
            this.DeductionApcCfa.Name = "DeductionApcCfa";
            // 
            // DeductionBlankCfa
            // 
            this.DeductionBlankCfa.Expression = "Round([V2_FactureCommerciale_PrintInvoice.DeductionFinancementBlank]*[V2_FactureC" +
    "ommerciale_PrintInvoice.TauxConv])";
            this.DeductionBlankCfa.FieldType = DevExpress.XtraReports.UI.FieldType.Double;
            this.DeductionBlankCfa.Name = "DeductionBlankCfa";
            // 
            // AutreDeductionCfa
            // 
            this.AutreDeductionCfa.Expression = "Round([V2_FactureCommerciale_PrintInvoice.AutresDeduction]*[V2_FactureCommerciale" +
    "_PrintInvoice.TauxConv])";
            this.AutreDeductionCfa.FieldType = DevExpress.XtraReports.UI.FieldType.Double;
            this.AutreDeductionCfa.Name = "AutreDeductionCfa";
            // 
            // MontantNetCfa
            // 
            this.MontantNetCfa.Expression = "Round([V2_FactureCommerciale_PrintInvoice.MontantNet]*[V2_FactureCommerciale_Prin" +
    "tInvoice.TauxConv])";
            this.MontantNetCfa.FieldType = DevExpress.XtraReports.UI.FieldType.Double;
            this.MontantNetCfa.Name = "MontantNetCfa";
            // 
            // rptCommercialInvoice
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.GroupHeader1});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.MontantBrutCfa,
            this.MontantATCfa,
            this.DeductionApcCfa,
            this.DeductionBlankCfa,
            this.AutreDeductionCfa,
            this.MontantNetCfa});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataSource = this.sqlDataSource1;
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 103, 2);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.paramID});
            this.Version = "17.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrPictureBox1.ImageUrl = "~\\Images\\Logo\\logo.jpg";
    }
}
