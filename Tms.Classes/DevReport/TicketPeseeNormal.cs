using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Tms.Classes;

/// <summary>
/// Summary description for TicketPeseeNormal
/// </summary>
public class TicketPeseeNormal : DevExpress.XtraReports.UI.XtraReport
{
    private DetailBand Detail;
    private TopMarginBand TopMargin;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel2;
    private BottomMarginBand BottomMargin;
    private XRLabel xrLabel4;
    private XRLabel xrLabel3;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell6;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell60;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell62;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell64;
    private XRTableCell xrTableCell54;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell67;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell68;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell70;
    private XRTable xrTable4;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell33;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell34;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTable ZoneVgm;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell46;
    private XRTableRow xrTableRow17;
    private XRTableCell xrTableCell71;
    private XRTableCell poidsvgm;
    private DetailReportBand DetailReport;
    private DetailBand Detail1;
    private GroupHeaderBand GroupHeader2;
    private XRLabel xrLabel18;
    private DetailReportBand DetailReport1;
    private DetailBand Detail2;
    private GroupHeaderBand GroupHeader3;
    private XRLabel xrLabel5;
    private DetailReportBand DetailReport2;
    private DetailBand Detail3;
    private GroupHeaderBand GroupHeader4;
    private XRLabel xrLabel17;
    private GroupFooterBand GroupFooter1;
    private PageFooterBand PageFooter;
    private XRLabel xrLabel6;
    private XRLabel xrLabel7;
    private XRLabel xrLabel1;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource2;
    private DevExpress.XtraReports.Parameters.Parameter peseeID;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell5;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell73;
    private XRTableCell xrTableCell74;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell76;
    private CalculatedField NbrSacsRefoulés;
    private CalculatedField PoidsBrut;
    private DevExpress.XtraReports.Parameters.Parameter IsCopy;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell79;
    private XRTableCell xrTableCell80;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell77;
    private XRTableCell xrTableCell78;
    private DevExpress.XtraReports.Parameters.Parameter IsVgm;
    private XRTableRow xrTableRow21;
    private XRTableCell LivraisonEstEmpotage;
    private XRPageInfo xrPageInfo1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public TicketPeseeNormal()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TicketPeseeNormal));
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ZoneVgm = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
            this.poidsvgm = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
            this.LivraisonEstEmpotage = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.sqlDataSource2 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.peseeID = new DevExpress.XtraReports.Parameters.Parameter();
            this.NbrSacsRefoulés = new DevExpress.XtraReports.UI.CalculatedField();
            this.PoidsBrut = new DevExpress.XtraReports.UI.CalculatedField();
            this.IsCopy = new DevExpress.XtraReports.Parameters.Parameter();
            this.IsVgm = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoneVgm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel3});
            this.Detail.HeightF = 68.52407F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.PeseeNumero")});
            this.xrLabel4.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(249.2268F, 10.83333F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(400.7732F, 37.69073F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "1000";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.83334F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(249.2268F, 37.69073F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "TICKET DE PESEE N° ";
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrLabel1,
            this.xrPictureBox1,
            this.xrLabel2});
            this.TopMargin.HeightF = 125F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.Format = "Date du document : {0}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(454.3439F, 102F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(275.6561F, 22.5F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.AdresseSociete")});
            this.xrLabel1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(121.6667F, 79.00003F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(168.3333F, 22.99999F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "xrLabel2";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 35F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(121.6667F, 90F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.TelSociete")});
            this.xrLabel2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(121.6667F, 102F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(168.3333F, 22.99999F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "xrLabel2";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13,
            this.xrTableRow14,
            this.xrTableRow15});
            this.xrTable4.SizeF = new System.Drawing.SizeF(730F, 75F);
            this.xrTable4.StylePriority.UseBorders = false;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell37,
            this.xrTableCell35,
            this.xrTableCell27,
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell33});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 1D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell37.StylePriority.UseFont = false;
            this.xrTableCell37.StylePriority.UsePadding = false;
            this.xrTableCell37.StylePriority.UseTextAlignment = false;
            this.xrTableCell37.Text = "Sacs déchargés :";
            this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell37.Weight = 1D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.PeseeSacsDecharges")});
            this.xrTableCell35.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseFont = false;
            this.xrTableCell35.Text = "xrTableCell35";
            this.xrTableCell35.Weight = 1D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.StylePriority.UsePadding = false;
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.Text = "Sacs bons :";
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell27.Weight = 0.89230771578275236D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.SacsBons")});
            this.xrTableCell28.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseFont = false;
            this.xrTableCell28.Text = "xrTableCell28";
            this.xrTableCell28.Weight = 1.1076922842172476D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell29.StylePriority.UseFont = false;
            this.xrTableCell29.StylePriority.UsePadding = false;
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.Text = "Sacs OPA :";
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell29.Weight = 0.81548891507662258D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.SacsOPA")});
            this.xrTableCell33.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseFont = false;
            this.xrTableCell33.Text = "xrTableCell33";
            this.xrTableCell33.Weight = 1.1845110849233773D;
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell38,
            this.xrTableCell36,
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell34});
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 1D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.StylePriority.UsePadding = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.Text = "Sacs acceptés :";
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell38.Weight = 1D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.PeseeSacsAcceptes")});
            this.xrTableCell36.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseFont = false;
            this.xrTableCell36.Text = "xrTableCell36";
            this.xrTableCell36.Weight = 1D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell30.StylePriority.UseFont = false;
            this.xrTableCell30.StylePriority.UsePadding = false;
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.Text = "Sacs mauvais :";
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell30.Weight = 0.89230771578275236D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.SacsMauvais")});
            this.xrTableCell31.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.Text = "xrTableCell31";
            this.xrTableCell31.Weight = 1.1076922842172476D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell32.StylePriority.UseFont = false;
            this.xrTableCell32.StylePriority.UsePadding = false;
            this.xrTableCell32.StylePriority.UseTextAlignment = false;
            this.xrTableCell32.Text = "Nbre Palettes :";
            this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell32.Weight = 0.81548891507662258D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.NbrePalettes")});
            this.xrTableCell34.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseFont = false;
            this.xrTableCell34.Text = "xrTableCell34";
            this.xrTableCell34.Weight = 1.1845110849233773D;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell42,
            this.xrTableCell43,
            this.xrTableCell44});
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 1D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell39.StylePriority.UseFont = false;
            this.xrTableCell39.StylePriority.UsePadding = false;
            this.xrTableCell39.StylePriority.UseTextAlignment = false;
            this.xrTableCell39.Text = "Sacs Refoulés :";
            this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell39.Weight = 1D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.NbrSacsRefoulés")});
            this.xrTableCell40.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseFont = false;
            this.xrTableCell40.Text = "xrTableCell40";
            this.xrTableCell40.Weight = 1D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell41.StylePriority.UseFont = false;
            this.xrTableCell41.StylePriority.UsePadding = false;
            this.xrTableCell41.StylePriority.UseTextAlignment = false;
            this.xrTableCell41.Text = "Sacs Nylon :";
            this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell41.Weight = 0.89230771578275236D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.SacsNylon")});
            this.xrTableCell42.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseFont = false;
            this.xrTableCell42.Text = "xrTableCell42";
            this.xrTableCell42.Weight = 1.1076922842172476D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell43.StylePriority.UseFont = false;
            this.xrTableCell43.StylePriority.UsePadding = false;
            this.xrTableCell43.StylePriority.UseTextAlignment = false;
            this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell43.Weight = 0.81548891507662258D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseFont = false;
            this.xrTableCell44.Weight = 1.1845110849233773D;
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow8,
            this.xrTableRow23,
            this.xrTableRow9,
            this.xrTableRow11,
            this.xrTableRow12,
            this.xrTableRow10,
            this.xrTableRow7,
            this.xrTableRow6,
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(730F, 275F);
            this.xrTable3.StylePriority.UseBorders = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell49,
            this.xrTableCell60});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UsePadding = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Date :";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell7.Weight = 0.39198108444470059D;
            this.xrTableCell7.WordWrap = false;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.LivraisonDate", "{0:d}")});
            this.xrTableCell8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.Weight = 1.1037008804972079D;
            this.xrTableCell8.WordWrap = false;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell49.StylePriority.UseFont = false;
            this.xrTableCell49.StylePriority.UsePadding = false;
            this.xrTableCell49.StylePriority.UseTextAlignment = false;
            this.xrTableCell49.Text = "Campagne :";
            this.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell49.Weight = 0.30492804947869767D;
            this.xrTableCell49.WordWrap = false;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.Campagne")});
            this.xrTableCell60.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.StylePriority.UseFont = false;
            this.xrTableCell60.Weight = 0.71903894680221225D;
            this.xrTableCell60.WordWrap = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell50,
            this.xrTableCell61});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UsePadding = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Fournisseur :";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell9.Weight = 0.39198108444470059D;
            this.xrTableCell9.WordWrap = false;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.SupplierName")});
            this.xrTableCell10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Weight = 1.1037008804972079D;
            this.xrTableCell10.WordWrap = false;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell50.StylePriority.UseFont = false;
            this.xrTableCell50.StylePriority.UsePadding = false;
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell50.Weight = 0.30492804947869767D;
            this.xrTableCell50.WordWrap = false;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.StylePriority.UseFont = false;
            this.xrTableCell61.Weight = 0.71903894680221225D;
            this.xrTableCell61.WordWrap = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell51,
            this.xrTableCell62});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.StylePriority.UsePadding = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.Text = "Type de Livraison :";
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell17.Weight = 0.39198108444470059D;
            this.xrTableCell17.WordWrap = false;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.LivraisonTypeName")});
            this.xrTableCell18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.Text = "xrTableCell18";
            this.xrTableCell18.Weight = 1.1037008804972079D;
            this.xrTableCell18.WordWrap = false;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell51.StylePriority.UseFont = false;
            this.xrTableCell51.StylePriority.UsePadding = false;
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell51.Weight = 0.30492804947869767D;
            this.xrTableCell51.WordWrap = false;
            // 
            // xrTableCell62
            // 
            this.xrTableCell62.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell62.Name = "xrTableCell62";
            this.xrTableCell62.StylePriority.UseFont = false;
            this.xrTableCell62.Weight = 0.71903894680221225D;
            this.xrTableCell62.WordWrap = false;
            // 
            // xrTableRow23
            // 
            this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell52,
            this.xrTableCell63});
            this.xrTableRow23.Name = "xrTableRow23";
            this.xrTableRow23.Weight = 1D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell47.StylePriority.UseFont = false;
            this.xrTableCell47.StylePriority.UsePadding = false;
            this.xrTableCell47.StylePriority.UseTextAlignment = false;
            this.xrTableCell47.Text = "N° Livraison :";
            this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell47.Weight = 0.39198108444470059D;
            this.xrTableCell47.WordWrap = false;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.LivraisonNumber")});
            this.xrTableCell48.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseFont = false;
            this.xrTableCell48.Text = "xrTableCell48";
            this.xrTableCell48.Weight = 1.1037008804972079D;
            this.xrTableCell48.WordWrap = false;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell52.StylePriority.UseFont = false;
            this.xrTableCell52.StylePriority.UsePadding = false;
            this.xrTableCell52.StylePriority.UseTextAlignment = false;
            this.xrTableCell52.Text = "Transitaire :";
            this.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell52.Weight = 0.30492804947869767D;
            this.xrTableCell52.WordWrap = false;
            // 
            // xrTableCell63
            // 
            this.xrTableCell63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.TransitaireNom")});
            this.xrTableCell63.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell63.Name = "xrTableCell63";
            this.xrTableCell63.StylePriority.UseFont = false;
            this.xrTableCell63.Weight = 0.71903894680221225D;
            this.xrTableCell63.WordWrap = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell53,
            this.xrTableCell64});
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.StylePriority.UsePadding = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.Text = "N° Vehicule :";
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell19.Weight = 0.39198108444470059D;
            this.xrTableCell19.WordWrap = false;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.Immatriculation")});
            this.xrTableCell20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.Weight = 1.1037008804972079D;
            this.xrTableCell20.WordWrap = false;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell53.StylePriority.UseFont = false;
            this.xrTableCell53.StylePriority.UsePadding = false;
            this.xrTableCell53.StylePriority.UseTextAlignment = false;
            this.xrTableCell53.Text = "Certification :";
            this.xrTableCell53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell53.Weight = 0.30492804947869767D;
            this.xrTableCell53.WordWrap = false;
            // 
            // xrTableCell64
            // 
            this.xrTableCell64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.CertificationNom")});
            this.xrTableCell64.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell64.Name = "xrTableCell64";
            this.xrTableCell64.StylePriority.UseFont = false;
            this.xrTableCell64.Weight = 0.71903894680221225D;
            this.xrTableCell64.WordWrap = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell54,
            this.xrTableCell65});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.StylePriority.UsePadding = false;
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.Text = "Provenance :";
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell23.Weight = 0.39198108444470059D;
            this.xrTableCell23.WordWrap = false;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.Provenance")});
            this.xrTableCell24.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.Weight = 1.1037008804972079D;
            this.xrTableCell24.WordWrap = false;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell54.StylePriority.UseFont = false;
            this.xrTableCell54.StylePriority.UsePadding = false;
            this.xrTableCell54.StylePriority.UseTextAlignment = false;
            this.xrTableCell54.Text = "N° Lot :";
            this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell54.Weight = 0.30492804947869767D;
            this.xrTableCell54.WordWrap = false;
            // 
            // xrTableCell65
            // 
            this.xrTableCell65.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.NumeroLot")});
            this.xrTableCell65.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell65.Name = "xrTableCell65";
            this.xrTableCell65.StylePriority.UseFont = false;
            this.xrTableCell65.Weight = 0.71903894680221225D;
            this.xrTableCell65.WordWrap = false;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell55,
            this.xrTableCell66});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 1D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.StylePriority.UsePadding = false;
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.Text = "Destination :";
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell25.Weight = 0.39198108444470059D;
            this.xrTableCell25.WordWrap = false;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.Destination")});
            this.xrTableCell26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseFont = false;
            this.xrTableCell26.Weight = 1.1037008804972079D;
            this.xrTableCell26.WordWrap = false;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell55.StylePriority.UseFont = false;
            this.xrTableCell55.StylePriority.UsePadding = false;
            this.xrTableCell55.StylePriority.UseTextAlignment = false;
            this.xrTableCell55.Text = "N° Plomb : ";
            this.xrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell55.Weight = 0.30492804947869767D;
            this.xrTableCell55.WordWrap = false;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.NumeroPlomb")});
            this.xrTableCell66.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.StylePriority.UseFont = false;
            this.xrTableCell66.Weight = 0.71903894680221225D;
            this.xrTableCell66.WordWrap = false;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell56,
            this.xrTableCell67});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.StylePriority.UsePadding = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = "Transporteur :";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell21.Weight = 0.39198108444470059D;
            this.xrTableCell21.WordWrap = false;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.Transporteur")});
            this.xrTableCell22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.Weight = 1.1037008804972079D;
            this.xrTableCell22.WordWrap = false;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell56.StylePriority.UseFont = false;
            this.xrTableCell56.StylePriority.UsePadding = false;
            this.xrTableCell56.StylePriority.UseTextAlignment = false;
            this.xrTableCell56.Text = "N° Conteneur :";
            this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell56.Weight = 0.30492804947869767D;
            this.xrTableCell56.WordWrap = false;
            // 
            // xrTableCell67
            // 
            this.xrTableCell67.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.NumeroConteneur")});
            this.xrTableCell67.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell67.Name = "xrTableCell67";
            this.xrTableCell67.StylePriority.UseFont = false;
            this.xrTableCell67.Weight = 0.71903894680221225D;
            this.xrTableCell67.WordWrap = false;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell57,
            this.xrTableCell68});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UsePadding = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "Chauffeur :";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell15.Weight = 0.39198108444470059D;
            this.xrTableCell15.WordWrap = false;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.Chauffeur")});
            this.xrTableCell16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.Weight = 1.1037008804972079D;
            this.xrTableCell16.WordWrap = false;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell57.StylePriority.UseFont = false;
            this.xrTableCell57.StylePriority.UsePadding = false;
            this.xrTableCell57.StylePriority.UseTextAlignment = false;
            this.xrTableCell57.Text = "N° OT :";
            this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell57.Weight = 0.30492804947869767D;
            this.xrTableCell57.WordWrap = false;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.NumeroOT")});
            this.xrTableCell68.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.StylePriority.UseFont = false;
            this.xrTableCell68.Weight = 0.71903894680221225D;
            this.xrTableCell68.WordWrap = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell58,
            this.xrTableCell69});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UsePadding = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "Sacs declarés :";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell13.Weight = 0.39198108444470059D;
            this.xrTableCell13.WordWrap = false;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.LivraisonEstimatedBags")});
            this.xrTableCell14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.Text = "xrTableCell14";
            this.xrTableCell14.Weight = 1.1037008804972079D;
            this.xrTableCell14.WordWrap = false;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell58.StylePriority.UseFont = false;
            this.xrTableCell58.StylePriority.UsePadding = false;
            this.xrTableCell58.StylePriority.UseTextAlignment = false;
            this.xrTableCell58.Text = "N° Externe :";
            this.xrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell58.Weight = 0.30492804947869767D;
            this.xrTableCell58.WordWrap = false;
            // 
            // xrTableCell69
            // 
            this.xrTableCell69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.NumeroExterne")});
            this.xrTableCell69.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell69.Name = "xrTableCell69";
            this.xrTableCell69.StylePriority.UseFont = false;
            this.xrTableCell69.Weight = 0.71903894680221225D;
            this.xrTableCell69.WordWrap = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell59,
            this.xrTableCell70});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UsePadding = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Poids déclaré (kg) :";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell11.Weight = 0.39198108444470059D;
            this.xrTableCell11.WordWrap = false;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.LivraisonEstimatedTonnage", "{0:#,#}")});
            this.xrTableCell12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "xrTableCell12";
            this.xrTableCell12.Weight = 1.1037008804972079D;
            this.xrTableCell12.WordWrap = false;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell59.StylePriority.UseFont = false;
            this.xrTableCell59.StylePriority.UsePadding = false;
            this.xrTableCell59.StylePriority.UseTextAlignment = false;
            this.xrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell59.Weight = 0.30492804947869767D;
            this.xrTableCell59.WordWrap = false;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell70.Name = "xrTableCell70";
            this.xrTableCell70.StylePriority.UseFont = false;
            this.xrTableCell70.Weight = 0.71903894680221225D;
            this.xrTableCell70.WordWrap = false;
            // 
            // ZoneVgm
            // 
            this.ZoneVgm.LocationFloat = new DevExpress.Utils.PointFloat(494.3439F, 40F);
            this.ZoneVgm.Name = "ZoneVgm";
            this.ZoneVgm.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16,
            this.xrTableRow20,
            this.xrTableRow19,
            this.xrTableRow17,
            this.xrTableRow21});
            this.ZoneVgm.SizeF = new System.Drawing.SizeF(235.6561F, 125F);
            this.ZoneVgm.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SetVgmZone);
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell45,
            this.xrTableCell46});
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.Weight = 1D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell45.StylePriority.UseFont = false;
            this.xrTableCell45.StylePriority.UsePadding = false;
            this.xrTableCell45.StylePriority.UseTextAlignment = false;
            this.xrTableCell45.Text = "Tare Sacs :";
            this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell45.Weight = 1.0733410644531252D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.PeseeTareOfBags", "{0:#,#}")});
            this.xrTableCell46.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.NullValueText = "0";
            this.xrTableCell46.StylePriority.UseFont = false;
            this.xrTableCell46.Weight = 1.28322021484375D;
            // 
            // xrTableRow20
            // 
            this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell79,
            this.xrTableCell80});
            this.xrTableRow20.Name = "xrTableRow20";
            this.xrTableRow20.Weight = 1D;
            // 
            // xrTableCell79
            // 
            this.xrTableCell79.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell79.Name = "xrTableCell79";
            this.xrTableCell79.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell79.StylePriority.UseFont = false;
            this.xrTableCell79.StylePriority.UsePadding = false;
            this.xrTableCell79.StylePriority.UseTextAlignment = false;
            this.xrTableCell79.Text = "Tare Palettes :";
            this.xrTableCell79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell79.Weight = 1.0733410644531252D;
            // 
            // xrTableCell80
            // 
            this.xrTableCell80.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.PeseeTareOfPalets", "{0:#,#}")});
            this.xrTableCell80.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell80.Name = "xrTableCell80";
            this.xrTableCell80.NullValueText = "0";
            this.xrTableCell80.StylePriority.UseFont = false;
            this.xrTableCell80.Text = "xrTableCell80";
            this.xrTableCell80.Weight = 1.28322021484375D;
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell77,
            this.xrTableCell78});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 1D;
            // 
            // xrTableCell77
            // 
            this.xrTableCell77.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell77.Name = "xrTableCell77";
            this.xrTableCell77.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell77.StylePriority.UseFont = false;
            this.xrTableCell77.StylePriority.UsePadding = false;
            this.xrTableCell77.StylePriority.UseTextAlignment = false;
            this.xrTableCell77.Text = "Tare Conteneur :";
            this.xrTableCell77.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell77.Weight = 1.0733410644531252D;
            // 
            // xrTableCell78
            // 
            this.xrTableCell78.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.TareConteneur", "{0:#,#}")});
            this.xrTableCell78.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell78.Name = "xrTableCell78";
            this.xrTableCell78.NullValueText = "0";
            this.xrTableCell78.StylePriority.UseFont = false;
            this.xrTableCell78.Text = "xrTableCell78";
            this.xrTableCell78.Weight = 1.28322021484375D;
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell71,
            this.poidsvgm});
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.Weight = 1D;
            // 
            // xrTableCell71
            // 
            this.xrTableCell71.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell71.Name = "xrTableCell71";
            this.xrTableCell71.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell71.StylePriority.UseFont = false;
            this.xrTableCell71.StylePriority.UsePadding = false;
            this.xrTableCell71.StylePriority.UseTextAlignment = false;
            this.xrTableCell71.Text = "Poids VGM :";
            this.xrTableCell71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell71.Weight = 1.0733410644531252D;
            // 
            // poidsvgm
            // 
            this.poidsvgm.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.PoidsVGM", "{0:#,#}")});
            this.poidsvgm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.poidsvgm.Name = "poidsvgm";
            this.poidsvgm.NullValueText = "0";
            this.poidsvgm.StylePriority.UseFont = false;
            this.poidsvgm.Weight = 1.28322021484375D;
            // 
            // xrTableRow21
            // 
            this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.LivraisonEstEmpotage});
            this.xrTableRow21.Name = "xrTableRow21";
            this.xrTableRow21.Weight = 1D;
            // 
            // LivraisonEstEmpotage
            // 
            this.LivraisonEstEmpotage.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.LivraisonEstEmpotage")});
            this.LivraisonEstEmpotage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LivraisonEstEmpotage.Name = "LivraisonEstEmpotage";
            this.LivraisonEstEmpotage.StylePriority.UseFont = false;
            this.LivraisonEstEmpotage.Text = "LivraisonEstEmpotage";
            this.LivraisonEstEmpotage.Visible = false;
            this.LivraisonEstEmpotage.Weight = 2.3565612792968751D;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(494.3439F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(235.6561F, 25F);
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
            this.xrTableCell3.Text = "Poids Brut (kg) :";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell3.Weight = 0.692874128495744D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.PoidsBrut", "{0:#,#}")});
            this.xrTableCell6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.Weight = 0.82835746946940159D;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader2});
            this.DetailReport.DataSource = this.sqlDataSource2;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.Detail1.HeightF = 298.3333F;
            this.Detail1.Name = "Detail1";
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18});
            this.GroupHeader2.HeightF = 29.13404F;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // xrLabel18
            // 
            this.xrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrLabel18.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(730F, 29.13404F);
            this.xrLabel18.StylePriority.UseBorders = false;
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "Livraison";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // sqlDataSource2
            // 
            this.sqlDataSource2.ConnectionName = "TMS2017_ReportServer";
            this.sqlDataSource2.Name = "sqlDataSource2";
            storedProcQuery1.Name = "Pesee_Ticket_Normal";
            queryParameter1.Name = "@ID";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.peseeID]", typeof(System.Guid));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.StoredProcName = "Pesee_Ticket_Normal";
            this.sqlDataSource2.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource2.ResultSchemaSerializable = resources.GetString("sqlDataSource2.ResultSchemaSerializable");
            // 
            // DetailReport1
            // 
            this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupHeader3});
            this.DetailReport1.DataSource = this.sqlDataSource2;
            this.DetailReport1.Level = 1;
            this.DetailReport1.Name = "DetailReport1";
            // 
            // Detail2
            // 
            this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.Detail2.HeightF = 100F;
            this.Detail2.Name = "Detail2";
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5});
            this.GroupHeader3.HeightF = 26.66667F;
            this.GroupHeader3.Name = "GroupHeader3";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrLabel5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(730F, 26.66667F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Sacherie";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // DetailReport2
            // 
            this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupHeader4,
            this.GroupFooter1});
            this.DetailReport2.DataSource = this.sqlDataSource2;
            this.DetailReport2.Level = 2;
            this.DetailReport2.Name = "DetailReport2";
            // 
            // Detail3
            // 
            this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail3.HeightF = 48.33334F;
            this.Detail3.Name = "Detail3";
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow18});
            this.xrTable2.SizeF = new System.Drawing.SizeF(730F, 48.33334F);
            this.xrTable2.StylePriority.UseBorders = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell5});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseBorders = false;
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "Date/Heure P{0}";
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.IgnoreNullValues = true;
            this.xrTableCell4.Summary = xrSummary1;
            this.xrTableCell4.Text = "Date/Heure P1 :";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell4.Weight = 0.3980768867322555D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.PeseeFirstWeightDate", "{0:g}")});
            this.xrTableCell1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 1.124452755339612D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            this.xrTableCell2.Summary = xrSummary2;
            this.xrTableCell2.Text = "Poids P1 (Kg) :";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell2.Weight = 0.39440515834190504D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.PeseeFirstWeight", "{0:#,#}")});
            this.xrTableCell5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 0.47152642173347714D;
            // 
            // xrTableRow18
            // 
            this.xrTableRow18.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell73,
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell76});
            this.xrTableRow18.Name = "xrTableRow18";
            this.xrTableRow18.StylePriority.UseBorders = false;
            this.xrTableRow18.Weight = 1D;
            // 
            // xrTableCell73
            // 
            this.xrTableCell73.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell73.Name = "xrTableCell73";
            this.xrTableCell73.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell73.StylePriority.UseFont = false;
            this.xrTableCell73.StylePriority.UsePadding = false;
            this.xrTableCell73.StylePriority.UseTextAlignment = false;
            this.xrTableCell73.Text = "Date/Heure P2 :";
            this.xrTableCell73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell73.Weight = 0.3980768867322555D;
            // 
            // xrTableCell74
            // 
            this.xrTableCell74.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.PeseeSecondWeightDate", "{0:g}")});
            this.xrTableCell74.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell74.Name = "xrTableCell74";
            this.xrTableCell74.StylePriority.UseFont = false;
            this.xrTableCell74.Text = "xrTableCell74";
            this.xrTableCell74.Weight = 1.124452755339612D;
            // 
            // xrTableCell75
            // 
            this.xrTableCell75.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell75.Name = "xrTableCell75";
            this.xrTableCell75.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell75.StylePriority.UseFont = false;
            this.xrTableCell75.StylePriority.UsePadding = false;
            this.xrTableCell75.StylePriority.UseTextAlignment = false;
            this.xrTableCell75.Text = "Poids P2 (Kg) :";
            this.xrTableCell75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell75.Weight = 0.39440515834190504D;
            // 
            // xrTableCell76
            // 
            this.xrTableCell76.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Pesee_Ticket_Normal.PeseeSecondWeight", "{0:#,#}")});
            this.xrTableCell76.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell76.Name = "xrTableCell76";
            this.xrTableCell76.StylePriority.UseFont = false;
            this.xrTableCell76.Text = "xrTableCell76";
            this.xrTableCell76.Weight = 0.47152642173347714D;
            // 
            // GroupHeader4
            // 
            this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel17});
            this.GroupHeader4.HeightF = 27.5F;
            this.GroupHeader4.Name = "GroupHeader4";
            // 
            // xrLabel17
            // 
            this.xrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrLabel17.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(730F, 27.5F);
            this.xrLabel17.StylePriority.UseBorders = false;
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "Pesée";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.ZoneVgm,
            this.xrTable1});
            this.GroupFooter1.HeightF = 168.3333F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.xrLabel7});
            this.PageFooter.HeightF = 100F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(21.6667F, 0F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "Visa Fournisseur";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(545.8829F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "Visa Entité";
            // 
            // peseeID
            // 
            this.peseeID.Name = "peseeID";
            this.peseeID.Type = typeof(System.Guid);
            this.peseeID.ValueInfo = "00000000-0000-0000-0000-000000000000";
            this.peseeID.Visible = false;
            // 
            // NbrSacsRefoulés
            // 
            this.NbrSacsRefoulés.DataMember = "Pesee_Ticket_Normal";
            this.NbrSacsRefoulés.Expression = "[PeseeSacsDecharges] - [PeseeSacsAcceptes]";
            this.NbrSacsRefoulés.Name = "NbrSacsRefoulés";
            // 
            // PoidsBrut
            // 
            this.PoidsBrut.DataMember = "Pesee_Ticket_Normal";
            this.PoidsBrut.Expression = "Abs([PeseeFirstWeight] - [PeseeSecondWeight])";
            this.PoidsBrut.Name = "PoidsBrut";
            // 
            // IsCopy
            // 
            this.IsCopy.Description = "Is Copy";
            this.IsCopy.Name = "IsCopy";
            this.IsCopy.Type = typeof(bool);
            this.IsCopy.ValueInfo = "False";
            this.IsCopy.Visible = false;
            // 
            // IsVgm
            // 
            this.IsVgm.Description = "Is Vgm";
            this.IsVgm.Name = "IsVgm";
            this.IsVgm.Type = typeof(bool);
            this.IsVgm.ValueInfo = "False";
            this.IsVgm.Visible = false;
            // 
            // TicketPeseeNormal
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport,
            this.DetailReport1,
            this.DetailReport2,
            this.PageFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.NbrSacsRefoulés,
            this.PoidsBrut});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource2});
            this.DataMember = "Pesee_Ticket_Normal";
            this.DataSource = this.sqlDataSource2;
            this.Margins = new System.Drawing.Printing.Margins(60, 60, 125, 0);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.peseeID,
            this.IsCopy,
            this.IsVgm});
            this.Version = "17.1";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SetWaterMark);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoneVgm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void SetWaterMark(object sender, System.Drawing.Printing.PrintEventArgs e)
    {        

        if ((bool)this.IsCopy.Value)
            ReportWaterMark.SetTextWatermark((XtraReport)sender);
    }
    

    private void SetVgmZone(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (bool.Parse(this.LivraisonEstEmpotage.Text))
        {
            ZoneVgm.Visible = true;
        }
    }
}
