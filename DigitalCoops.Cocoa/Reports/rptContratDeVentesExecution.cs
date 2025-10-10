using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptContratDeVentesExecution
/// </summary>
public class rptContratDeVentesExecution : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    private ReportHeaderBand reportHeaderBand1;
    private GroupHeaderBand groupHeaderBand1;
    private GroupHeaderBand groupHeaderBand2;
    private GroupFooterBand groupFooterBand1;
    private XRLabel xrLabel2;
    private ReportFooterBand reportFooterBand1;
    private XRControlStyle Title;
    private XRControlStyle GroupCaption3;
    private XRControlStyle GroupData3;
    private XRControlStyle DetailCaption3;
    private XRControlStyle DetailData3;
    private XRControlStyle DetailData3_Odd;
    private XRControlStyle DetailCaptionBackground3;
    private XRControlStyle TotalCaption3;
    private XRControlStyle TotalData3;
    private XRControlStyle TotalBackground3;
    private XRControlStyle GrandTotalCaption3;
    private XRControlStyle GrandTotalData3;
    private XRControlStyle GrandTotalBackground3;
    private XRControlStyle PageInfo;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell29;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell32;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell35;
    private XRLabel xrLabel1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell2;
    private GroupFooterBand GroupFooter1;
    private XRTable xrTable8;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell57;
    private XRPageInfo xrPageInfo4;
    private XRPageInfo xrPageInfo3;
    private DevExpress.XtraReports.Parameters.Parameter paramCampagne;
    private DevExpress.XtraReports.Parameters.Parameter paramExportateurID;
    private DevExpress.XtraReports.Parameters.Parameter paramExportateur;
    private DevExpress.XtraReports.Parameters.Parameter paramCertification;
    private DevExpress.XtraReports.Parameters.Parameter paramCertificationID;
    private DevExpress.XtraReports.Parameters.Parameter paramDateDebut;
    private DevExpress.XtraReports.Parameters.Parameter paramDateFin;
    private XRTable xrTable5;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptContratDeVentesExecution()
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
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptContratDeVentesExecution));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo4 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.reportHeaderBand1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramCampagne = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramDateDebut = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramDateFin = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramExportateur = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramCertification = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.groupHeaderBand1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.groupHeaderBand2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.groupFooterBand1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.reportFooterBand1 = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GroupCaption3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GroupData3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailCaption3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailData3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailData3_Odd = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DetailCaptionBackground3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TotalCaption3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TotalData3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TotalBackground3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrandTotalCaption3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrandTotalData3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrandTotalBackground3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.paramExportateurID = new DevExpress.XtraReports.Parameters.Parameter();
            this.paramCertificationID = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
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
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(871.5623F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.OddStyleName = "DetailData3_Odd";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(128.4376F, 25F);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell24});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 11.5D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.ebNumero")});
            this.xrTableCell23.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StyleName = "DetailData3";
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.Text = "xrTableCell23";
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell23.Weight = 0.38402060497729618D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.QuantiteEmb", "{0:#,#.##0}")});
            this.xrTableCell24.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StyleName = "DetailData3";
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell24.Weight = 0.41507181961609863D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.TopMargin.HeightF = 100F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable4
            // 
            this.xrTable4.Font = new System.Drawing.Font("Calibri Light", 10F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 45F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6});
            this.xrTable4.SizeF = new System.Drawing.SizeF(300F, 55F);
            this.xrTable4.StylePriority.UseFont = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Parametres_Select.NomSociete")});
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.Weight = 3D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell32});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Parametres_Select.AdresseSociete")});
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.Weight = 3D;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Parametres_Select.TelSociete")});
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.Weight = 3D;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo4,
            this.xrPageInfo3});
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo4
            // 
            this.xrPageInfo4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo4.Format = "Printed On : {0}";
            this.xrPageInfo4.LocationFloat = new DevExpress.Utils.PointFloat(6.640427F, 67F);
            this.xrPageInfo4.Name = "xrPageInfo4";
            this.xrPageInfo4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo4.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo4.SizeF = new System.Drawing.SizeF(231.6667F, 23F);
            this.xrPageInfo4.StylePriority.UseFont = false;
            this.xrPageInfo4.StylePriority.UseTextAlignment = false;
            this.xrPageInfo4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrPageInfo3
            // 
            this.xrPageInfo3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo3.Format = "Page {0} / {1}";
            this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(442.4736F, 67F);
            this.xrPageInfo3.Name = "xrPageInfo3";
            this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo3.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrPageInfo3.StylePriority.UseFont = false;
            this.xrPageInfo3.StylePriority.UseTextAlignment = false;
            this.xrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "TMS2017_ReportServer";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "Parametres_Select";
            storedProcQuery1.StoredProcName = "Parametres_Select";
            storedProcQuery2.Name = "V2_ContratDeVentes_PrintExecution";
            queryParameter1.Name = "@ExportateurID";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.paramExportateurID]", typeof(int));
            queryParameter2.Name = "@Campagne";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.paramCampagne]", typeof(string));
            queryParameter3.Name = "@CertificationID";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.paramCertificationID]", typeof(int));
            queryParameter4.Name = "@DateDebut";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("[Parameters.paramDateDebut]", typeof(System.DateTime));
            queryParameter5.Name = "@DateFin";
            queryParameter5.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter5.Value = new DevExpress.DataAccess.Expression("[Parameters.paramDateFin]", typeof(System.DateTime));
            storedProcQuery2.Parameters.Add(queryParameter1);
            storedProcQuery2.Parameters.Add(queryParameter2);
            storedProcQuery2.Parameters.Add(queryParameter3);
            storedProcQuery2.Parameters.Add(queryParameter4);
            storedProcQuery2.Parameters.Add(queryParameter5);
            storedProcQuery2.StoredProcName = "V2_ContratDeVentes_PrintExecution";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1,
            storedProcQuery2});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // reportHeaderBand1
            // 
            this.reportHeaderBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5,
            this.xrLabel1});
            this.reportHeaderBand1.HeightF = 171.3005F;
            this.reportHeaderBand1.Name = "reportHeaderBand1";
            // 
            // xrTable5
            // 
            this.xrTable5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0.2524444F, 111.1111F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7,
            this.xrTableRow8});
            this.xrTable5.SizeF = new System.Drawing.SizeF(999.7476F, 49.99997F);
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Campagne :";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell3.Weight = 0.2334881252127626D;
            this.xrTableCell3.WordWrap = false;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramCampagne, "Text", "")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.Weight = 0.95180125029326257D;
            // 
            // paramCampagne
            // 
            this.paramCampagne.Description = "Campagne";
            this.paramCampagne.Name = "paramCampagne";
            this.paramCampagne.Visible = false;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Du :";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell5.Weight = 0.37770936811857625D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramDateDebut, "Text", "{0:dd/MM/yyyy}")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell6.StylePriority.UsePadding = false;
            this.xrTableCell6.Weight = 0.44938554174980228D;
            // 
            // paramDateDebut
            // 
            this.paramDateDebut.Description = "Date debut";
            this.paramDateDebut.Name = "paramDateDebut";
            this.paramDateDebut.Type = typeof(System.DateTime);
            this.paramDateDebut.Visible = false;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "Au :";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell7.Weight = 0.12983161513070107D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramDateFin, "Text", "{0:dd/MM/yyyy}")});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell8.StylePriority.UsePadding = false;
            this.xrTableCell8.Weight = 0.75038270835753385D;
            // 
            // paramDateFin
            // 
            this.paramDateFin.Description = "Date fin";
            this.paramDateFin.Name = "paramDateFin";
            this.paramDateFin.Type = typeof(System.DateTime);
            this.paramDateFin.Visible = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell36,
            this.xrTableCell37});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.Text = "Exportateur :";
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell9.Weight = 0.2334881252127626D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramExportateur, "Text", "")});
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell10.StylePriority.UsePadding = false;
            this.xrTableCell10.Weight = 0.95180134477494038D;
            // 
            // paramExportateur
            // 
            this.paramExportateur.Description = "Exportateur";
            this.paramExportateur.Name = "paramExportateur";
            this.paramExportateur.Visible = false;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseFont = false;
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            this.xrTableCell36.Text = "Certification :";
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell36.Weight = 0.37770930414547965D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.paramCertification, "Text", "")});
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell37.StylePriority.UsePadding = false;
            this.xrTableCell37.Weight = 1.329599834729456D;
            // 
            // paramCertification
            // 
            this.paramCertification.Description = "Certification";
            this.paramCertification.Name = "paramCertification";
            this.paramCertification.Visible = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(6.000004F, 32.5F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(558.3334F, 50.83333F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "Sales Contract Execution";
            // 
            // groupHeaderBand1
            // 
            this.groupHeaderBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.groupHeaderBand1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("DateCV", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("Numero", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("ReferenceClient", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("DebutExpedition", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("FinExpedition", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("Prix", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("RemiseFob", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("PrixFob", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("Quantite", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("Balance", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.groupHeaderBand1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
            this.groupHeaderBand1.HeightF = 25F;
            this.groupHeaderBand1.Level = 1;
            this.groupHeaderBand1.Name = "groupHeaderBand1";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.OddStyleName = "DetailData3_Odd";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(999.9999F, 25F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell38,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell1,
            this.xrTableCell20,
            this.xrTableCell2});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 11.5D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.DateCV", "{0:dd/MM/yyyy}")});
            this.xrTableCell11.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StyleName = "DetailData3";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell11.Weight = 0.067181726696176641D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.Numero")});
            this.xrTableCell12.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StyleName = "DetailData3";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 0.065102571541138785D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.ReferenceClient")});
            this.xrTableCell13.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StyleName = "DetailData3";
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell13.Weight = 0.084755433844126621D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.DebutExpedition", "{0:dd/MM/yyyy}")});
            this.xrTableCell14.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StyleName = "DetailData3";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell14.Weight = 0.082361532258699438D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.Text = "-";
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell38.Weight = 0.015905454587651982D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.FinExpedition", "{0:dd/MM/yyyy}")});
            this.xrTableCell15.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StyleName = "DetailData3";
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell15.Weight = 0.068612790428362713D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.Prix", "{0:n2}")});
            this.xrTableCell16.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StyleName = "DetailData3";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell16.Weight = 0.093016960828062878D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.RemiseFob", "{0:n2}")});
            this.xrTableCell17.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StyleName = "DetailData3";
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell17.Weight = 0.099785226315729156D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.PrixFob", "{0:n2}")});
            this.xrTableCell18.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StyleName = "DetailData3";
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell18.Weight = 0.11766770456726199D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.Quantite", "{0:#,#.##0}")});
            this.xrTableCell19.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StyleName = "DetailData3";
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell19.Weight = 0.0979611082649929D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.Balance", "{0:#,#.##0}")});
            this.xrTableCell1.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 0.07747239301633381D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StyleName = "DetailData3";
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.Text = "OT";
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell20.Weight = 0.06160026274275214D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Quantity (T)";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.066580933635160378D;
            // 
            // groupHeaderBand2
            // 
            this.groupHeaderBand2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.groupHeaderBand2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
            this.groupHeaderBand2.HeightF = 34.39519F;
            this.groupHeaderBand2.Level = 2;
            this.groupHeaderBand2.Name = "groupHeaderBand2";
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1000F, 34.22328F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27,
            this.xrTableCell28,
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell33,
            this.xrTableCell34});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell21.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell21.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseBackColor = false;
            this.xrTableCell21.StylePriority.UseBorderColor = false;
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = "Date";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell21.Weight = 0.1886837641581966D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell22.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell22.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseBackColor = false;
            this.xrTableCell22.StylePriority.UseBorderColor = false;
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.Text = "Number";
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell22.Weight = 0.18284438994784816D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell25.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell25.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseBackColor = false;
            this.xrTableCell25.StylePriority.UseBorderColor = false;
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.Text = "Custom Ref.";
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell25.Weight = 0.23804045277002775D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell26.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell26.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseBackColor = false;
            this.xrTableCell26.StylePriority.UseBorderColor = false;
            this.xrTableCell26.StylePriority.UseFont = false;
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            this.xrTableCell26.Text = "Period";
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell26.Weight = 0.46869145642609211D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell27.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell27.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseBackColor = false;
            this.xrTableCell27.StylePriority.UseBorderColor = false;
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.Text = "Price (Euro)";
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell27.Weight = 0.26124350673131108D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell28.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell28.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseBackColor = false;
            this.xrTableCell28.StylePriority.UseBorderColor = false;
            this.xrTableCell28.StylePriority.UseFont = false;
            this.xrTableCell28.StylePriority.UseTextAlignment = false;
            this.xrTableCell28.Text = "Fob Discount";
            this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell28.Weight = 0.28025256841684076D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell30.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell30.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseBackColor = false;
            this.xrTableCell30.StylePriority.UseBorderColor = false;
            this.xrTableCell30.StylePriority.UseFont = false;
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.Text = "Fob Price (Euro)";
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell30.Weight = 0.33047655881006133D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell31.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell31.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseBackColor = false;
            this.xrTableCell31.StylePriority.UseBorderColor = false;
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            this.xrTableCell31.Text = "Quantity (T)";
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell31.Weight = 0.27512936635202079D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell33.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell33.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseBackColor = false;
            this.xrTableCell33.StylePriority.UseBorderColor = false;
            this.xrTableCell33.StylePriority.UseFont = false;
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            this.xrTableCell33.Text = "Balance (T)";
            this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell33.Weight = 0.21758568626029157D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell34.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell34.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseBackColor = false;
            this.xrTableCell34.StylePriority.UseBorderColor = false;
            this.xrTableCell34.StylePriority.UseFont = false;
            this.xrTableCell34.StylePriority.UseTextAlignment = false;
            this.xrTableCell34.Text = "Execution";
            this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell34.Weight = 0.36000472560954033D;
            // 
            // groupFooterBand1
            // 
            this.groupFooterBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
            this.groupFooterBand1.GroupUnion = DevExpress.XtraReports.UI.GroupFooterUnion.WithLastDetail;
            this.groupFooterBand1.HeightF = 2.525253F;
            this.groupFooterBand1.Name = "groupFooterBand1";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(1000F, 2.525253F);
            this.xrLabel2.StyleName = "GroupCaption3";
            this.xrLabel2.StylePriority.UseBorders = false;
            // 
            // reportFooterBand1
            // 
            this.reportFooterBand1.HeightF = 114.8939F;
            this.reportFooterBand1.Name = "reportFooterBand1";
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.BorderColor = System.Drawing.Color.Black;
            this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Title.BorderWidth = 1F;
            this.Title.Font = new System.Drawing.Font("Tahoma", 14F);
            this.Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.Title.Name = "Title";
            // 
            // GroupCaption3
            // 
            this.GroupCaption3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
            this.GroupCaption3.BorderColor = System.Drawing.Color.White;
            this.GroupCaption3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.GroupCaption3.BorderWidth = 2F;
            this.GroupCaption3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.GroupCaption3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.GroupCaption3.Name = "GroupCaption3";
            this.GroupCaption3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
            this.GroupCaption3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GroupData3
            // 
            this.GroupData3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
            this.GroupData3.BorderColor = System.Drawing.Color.White;
            this.GroupData3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.GroupData3.BorderWidth = 2F;
            this.GroupData3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.GroupData3.ForeColor = System.Drawing.Color.White;
            this.GroupData3.Name = "GroupData3";
            this.GroupData3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
            this.GroupData3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailCaption3
            // 
            this.DetailCaption3.BackColor = System.Drawing.Color.Transparent;
            this.DetailCaption3.BorderColor = System.Drawing.Color.Transparent;
            this.DetailCaption3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.DetailCaption3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.DetailCaption3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.DetailCaption3.Name = "DetailCaption3";
            this.DetailCaption3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
            this.DetailCaption3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailData3
            // 
            this.DetailData3.Font = new System.Drawing.Font("Tahoma", 8F);
            this.DetailData3.ForeColor = System.Drawing.Color.Black;
            this.DetailData3.Name = "DetailData3";
            this.DetailData3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
            this.DetailData3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailData3_Odd
            // 
            this.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.DetailData3_Odd.BorderColor = System.Drawing.Color.Transparent;
            this.DetailData3_Odd.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.DetailData3_Odd.BorderWidth = 1F;
            this.DetailData3_Odd.Font = new System.Drawing.Font("Tahoma", 8F);
            this.DetailData3_Odd.ForeColor = System.Drawing.Color.Black;
            this.DetailData3_Odd.Name = "DetailData3_Odd";
            this.DetailData3_Odd.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
            this.DetailData3_Odd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailCaptionBackground3
            // 
            this.DetailCaptionBackground3.BackColor = System.Drawing.Color.Transparent;
            this.DetailCaptionBackground3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.DetailCaptionBackground3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.DetailCaptionBackground3.BorderWidth = 2F;
            this.DetailCaptionBackground3.Name = "DetailCaptionBackground3";
            // 
            // TotalCaption3
            // 
            this.TotalCaption3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.TotalCaption3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(147)))), ((int)(((byte)(147)))));
            this.TotalCaption3.Name = "TotalCaption3";
            this.TotalCaption3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
            this.TotalCaption3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // TotalData3
            // 
            this.TotalData3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.TotalData3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.TotalData3.Name = "TotalData3";
            this.TotalData3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 6, 0, 0, 100F);
            this.TotalData3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // TotalBackground3
            // 
            this.TotalBackground3.Name = "TotalBackground3";
            // 
            // GrandTotalCaption3
            // 
            this.GrandTotalCaption3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.GrandTotalCaption3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.GrandTotalCaption3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.GrandTotalCaption3.Name = "GrandTotalCaption3";
            this.GrandTotalCaption3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
            this.GrandTotalCaption3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GrandTotalData3
            // 
            this.GrandTotalData3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.GrandTotalData3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.GrandTotalData3.ForeColor = System.Drawing.Color.White;
            this.GrandTotalData3.Name = "GrandTotalData3";
            this.GrandTotalData3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 6, 0, 0, 100F);
            this.GrandTotalData3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GrandTotalBackground3
            // 
            this.GrandTotalBackground3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.GrandTotalBackground3.BorderColor = System.Drawing.Color.White;
            this.GrandTotalBackground3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.GrandTotalBackground3.BorderWidth = 2F;
            this.GrandTotalBackground3.Name = "GrandTotalBackground3";
            // 
            // PageInfo
            // 
            this.PageInfo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.PageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.PageInfo.Name = "PageInfo";
            this.PageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
            this.GroupFooter1.HeightF = 38.54163F;
            this.GroupFooter1.Level = 1;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrTable8
            // 
            this.xrTable8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(871.5623F, 0F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.xrTable8.SizeF = new System.Drawing.SizeF(128.4377F, 24.99998F);
            this.xrTable8.StylePriority.UseFont = false;
            this.xrTable8.StylePriority.UseTextAlignment = false;
            this.xrTable8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell56,
            this.xrTableCell57});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 1D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell56.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.StylePriority.UseBackColor = false;
            this.xrTableCell56.StylePriority.UseBorderColor = false;
            this.xrTableCell56.Text = "Total (T) :";
            this.xrTableCell56.Weight = 0.23424892085991542D;
            this.xrTableCell56.WordWrap = false;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell57.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(185)))), ((int)(((byte)(202)))));
            this.xrTableCell57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "V2_ContratDeVentes_PrintExecution.QuantiteEmb")});
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
            this.xrTableCell57.StylePriority.UseBackColor = false;
            this.xrTableCell57.StylePriority.UseBorderColor = false;
            this.xrTableCell57.StylePriority.UsePadding = false;
            this.xrTableCell57.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:#,#.##0}";
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell57.Summary = xrSummary1;
            this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell57.Weight = 0.25319033226123466D;
            this.xrTableCell57.WordWrap = false;
            // 
            // paramExportateurID
            // 
            this.paramExportateurID.Description = "ExportateurID";
            this.paramExportateurID.Name = "paramExportateurID";
            this.paramExportateurID.Type = typeof(int);
            this.paramExportateurID.ValueInfo = "0";
            this.paramExportateurID.Visible = false;
            // 
            // paramCertificationID
            // 
            this.paramCertificationID.Description = "CertificationID";
            this.paramCertificationID.Name = "paramCertificationID";
            this.paramCertificationID.Type = typeof(int);
            this.paramCertificationID.ValueInfo = "0";
            this.paramCertificationID.Visible = false;
            // 
            // rptContratDeVentesExecution
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.reportHeaderBand1,
            this.groupHeaderBand1,
            this.groupHeaderBand2,
            this.groupFooterBand1,
            this.reportFooterBand1,
            this.GroupFooter1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "V2_ContratDeVentes_PrintExecution";
            this.DataSource = this.sqlDataSource1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(51, 49, 100, 100);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.paramCampagne,
            this.paramExportateurID,
            this.paramExportateur,
            this.paramCertification,
            this.paramCertificationID,
            this.paramDateDebut,
            this.paramDateFin});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.GroupCaption3,
            this.GroupData3,
            this.DetailCaption3,
            this.DetailData3,
            this.DetailData3_Odd,
            this.DetailCaptionBackground3,
            this.TotalCaption3,
            this.TotalData3,
            this.TotalBackground3,
            this.GrandTotalCaption3,
            this.GrandTotalData3,
            this.GrandTotalBackground3,
            this.PageInfo});
            this.Version = "17.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
