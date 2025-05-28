using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for rptFinancingAgreementcs
/// </summary>
public class rptFinancingAgreement : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand GrpHeader;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel3;
    private DevExpress.XtraReports.Parameters.Parameter FinancementID;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource2;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel6;
    private XRLabel lblInfosSociete;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell lblNomSociete;
    private XRTableCell lblAdresseSociete;
    private XRTableCell lblTel1Societe;
    private XRTableCell lblTel2Societe;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private XRLabel xrLabel8;
    private XRLabel xrLabel9;
    private XRLabel lblSupplierLocation;
    private XRTableCell lblLocation;
    private XRLabel lblSupplierTonnage;
    private XRTableCell lblTonnage;
    private XRLabel xrLabel10;
    private XRLabel xrLabel7;
    private XRLabel xrLabel11;
    private XRLabel lbl3rdCondition;
    private XRTableCell lblVilleSociete;
    private XRLabel lblSupplierCondition1;
    private XRLabel lblSupplierCondition2;
    private XRTableCell lblDateAvance;
    private XRTableCell lblMontantAvance;
    private XRLabel lblMontantEnLettre;
    private DevExpress.XtraReports.Parameters.Parameter MontantEnLettre;
    private XRLabel lblQualityCondition;
    private XRLabel xrLabel13;
    private XRLabel xrLabel14;
    private XRLabel xrLabel15;
    private XRLabel xrLabel16;
    private XRLabel lblConditionRepayment;
    private XRLabel lblRepayment;
    private XRLabel xrLabel21;
    private XRLabel xrLabel23;
    private XRLabel xrLabel22;
    private XRLabel xrLabel24;
    private XRLabel xrLabel25;
    private XRLabel xrLabel26;
    private XRLabel lblCompanyConsent;
    private XRLabel lblCompanyConsent2;
    private XRLabel lblCompanyConsent3;
    private XRLabel xrLabel27;
    private XRLabel lblCompanyConsent31;
    private XRTableCell lblVille2Societe;
    private XRLabel xrLabel32;
    private XRLabel xrLabel31;
    private XRLabel xrLabel30;
    private XRLabel xrLabel29;
    private XRLabel lblReference;
    private XRTableCell lblCampagne;
    private XRTableCell lblNumero;
    private XRLabel lblSupplierInfo;
    private XRLabel lblCompanyConsent1;
    private XRLabel lblSupplier;
    private XRPageInfo xrPageInfo1;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public rptFinancingAgreement()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptFinancingAgreement));
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            this.GrpHeader = new DevExpress.XtraReports.UI.DetailBand();
            this.lblSupplier = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCompanyConsent1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSupplierInfo = new DevExpress.XtraReports.UI.XRLabel();
            this.lblReference = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCompanyConsent31 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCompanyConsent3 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCompanyConsent2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCompanyConsent = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblRepayment = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblConditionRepayment = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblQualityCondition = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblMontantEnLettre = new DevExpress.XtraReports.UI.XRLabel();
            this.MontantEnLettre = new DevExpress.XtraReports.Parameters.Parameter();
            this.lblSupplierCondition2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSupplierCondition1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl3rdCondition = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSupplierTonnage = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSupplierLocation = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInfosSociete = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblNomSociete = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblAdresseSociete = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblTel1Societe = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblTel2Societe = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblLocation = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblTonnage = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblVilleSociete = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblDateAvance = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblMontantAvance = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblVille2Societe = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblCampagne = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblNumero = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.FinancementID = new DevExpress.XtraReports.Parameters.Parameter();
            this.sqlDataSource2 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // GrpHeader
            // 
            this.GrpHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblSupplier,
            this.lblCompanyConsent1,
            this.lblSupplierInfo,
            this.lblReference,
            this.xrLabel32,
            this.xrLabel31,
            this.xrLabel30,
            this.xrLabel29,
            this.lblCompanyConsent31,
            this.xrLabel27,
            this.lblCompanyConsent3,
            this.lblCompanyConsent2,
            this.lblCompanyConsent,
            this.xrLabel26,
            this.xrLabel25,
            this.xrLabel24,
            this.xrLabel23,
            this.xrLabel22,
            this.xrLabel21,
            this.lblRepayment,
            this.xrLabel16,
            this.lblConditionRepayment,
            this.xrLabel15,
            this.xrLabel14,
            this.lblQualityCondition,
            this.xrLabel13,
            this.lblMontantEnLettre,
            this.lblSupplierCondition2,
            this.lblSupplierCondition1,
            this.lbl3rdCondition,
            this.xrLabel11,
            this.xrLabel10,
            this.xrLabel7,
            this.lblSupplierTonnage,
            this.lblSupplierLocation,
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel6,
            this.lblInfosSociete,
            this.xrTable1});
            this.GrpHeader.Dpi = 254F;
            this.GrpHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.GrpHeader.HeightF = 3352.272F;
            this.GrpHeader.Name = "GrpHeader";
            this.GrpHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.GrpHeader.StylePriority.UseFont = false;
            this.GrpHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.GrpHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SetReportContent);
            // 
            // lblSupplier
            // 
            this.lblSupplier.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.FournisseurNom", "{0:#,#}")});
            this.lblSupplier.Dpi = 254F;
            this.lblSupplier.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSupplier.LocationFloat = new DevExpress.Utils.PointFloat(433.6018F, 164.0417F);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblSupplier.SizeF = new System.Drawing.SizeF(222.375F, 45.19087F);
            this.lblSupplier.StylePriority.UseFont = false;
            this.lblSupplier.Visible = false;
            // 
            // lblCompanyConsent1
            // 
            this.lblCompanyConsent1.Dpi = 254F;
            this.lblCompanyConsent1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCompanyConsent1.LocationFloat = new DevExpress.Utils.PointFloat(204.1222F, 2235.942F);
            this.lblCompanyConsent1.Name = "lblCompanyConsent1";
            this.lblCompanyConsent1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCompanyConsent1.SizeF = new System.Drawing.SizeF(1903.001F, 58.41992F);
            this.lblCompanyConsent1.StylePriority.UseFont = false;
            this.lblCompanyConsent1.StylePriority.UseTextAlignment = false;
            this.lblCompanyConsent1.Text = "CompanyConsent1";
            this.lblCompanyConsent1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblSupplierInfo
            // 
            this.lblSupplierInfo.Dpi = 254F;
            this.lblSupplierInfo.Font = new System.Drawing.Font("Segoe UI Black", 13F);
            this.lblSupplierInfo.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 505.2486F);
            this.lblSupplierInfo.Name = "lblSupplierInfo";
            this.lblSupplierInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblSupplierInfo.SizeF = new System.Drawing.SizeF(2093F, 74.29498F);
            this.lblSupplierInfo.StylePriority.UseFont = false;
            this.lblSupplierInfo.StylePriority.UseTextAlignment = false;
            this.lblSupplierInfo.Text = "Supplier";
            this.lblSupplierInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblReference
            // 
            this.lblReference.Dpi = 254F;
            this.lblReference.Font = new System.Drawing.Font("Segoe UI Black", 14F);
            this.lblReference.LocationFloat = new DevExpress.Utils.PointFloat(158.75F, 0F);
            this.lblReference.Name = "lblReference";
            this.lblReference.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblReference.SizeF = new System.Drawing.SizeF(1864.916F, 74.29492F);
            this.lblReference.StylePriority.UseFont = false;
            this.lblReference.StylePriority.UseTextAlignment = false;
            this.lblReference.Text = "Reference";
            this.lblReference.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel32
            // 
            this.xrLabel32.Dpi = 254F;
            this.xrLabel32.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(1459.958F, 3058.477F);
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(642.9366F, 58.41992F);
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.StylePriority.UseTextAlignment = false;
            this.xrLabel32.Text = "THE SUPPLIER";
            this.xrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel31
            // 
            this.xrLabel31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.VilleSociete")});
            this.xrLabel31.Dpi = 254F;
            this.xrLabel31.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel31.LocationFloat = new DevExpress.Utils.PointFloat(1452.835F, 2767.541F);
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel31.SizeF = new System.Drawing.SizeF(654.8541F, 58.41992F);
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.Text = "xrLabel31";
            // 
            // xrLabel30
            // 
            this.xrLabel30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.NomSociete")});
            this.xrLabel30.Dpi = 254F;
            this.xrLabel30.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel30.LocationFloat = new DevExpress.Utils.PointFloat(209.4179F, 3058.477F);
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel30.SizeF = new System.Drawing.SizeF(836.0834F, 58.41992F);
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.Text = "xrLabel30";
            // 
            // xrLabel29
            // 
            this.xrLabel29.Dpi = 254F;
            this.xrLabel29.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel29.LocationFloat = new DevExpress.Utils.PointFloat(206.7701F, 2767.541F);
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel29.SizeF = new System.Drawing.SizeF(899.5825F, 58.41992F);
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.StylePriority.UseTextAlignment = false;
            this.xrLabel29.Text = "Done in two (2) originals";
            this.xrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblCompanyConsent31
            // 
            this.lblCompanyConsent31.Dpi = 254F;
            this.lblCompanyConsent31.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCompanyConsent31.LocationFloat = new DevExpress.Utils.PointFloat(206.7701F, 2566.458F);
            this.lblCompanyConsent31.Name = "lblCompanyConsent31";
            this.lblCompanyConsent31.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCompanyConsent31.SizeF = new System.Drawing.SizeF(1926.041F, 58.41992F);
            this.lblCompanyConsent31.StylePriority.UseFont = false;
            this.lblCompanyConsent31.StylePriority.UseTextAlignment = false;
            this.lblCompanyConsent31.Text = "CompanyConsent31";
            this.lblCompanyConsent31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel27
            // 
            this.xrLabel27.Dpi = 254F;
            this.xrLabel27.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(206.7701F, 2481.792F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(1898.271F, 58.42017F);
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.StylePriority.UseTextAlignment = false;
            this.xrLabel27.Text = "Any disagreement arising from this contract will be an object of preliminary peac" +
    "eful settlement otherwise,";
            this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblCompanyConsent3
            // 
            this.lblCompanyConsent3.Dpi = 254F;
            this.lblCompanyConsent3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCompanyConsent3.LocationFloat = new DevExpress.Utils.PointFloat(206.7701F, 2405.063F);
            this.lblCompanyConsent3.Name = "lblCompanyConsent3";
            this.lblCompanyConsent3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCompanyConsent3.SizeF = new System.Drawing.SizeF(1918.353F, 58.41992F);
            this.lblCompanyConsent3.StylePriority.UseFont = false;
            this.lblCompanyConsent3.StylePriority.UseTextAlignment = false;
            this.lblCompanyConsent3.Text = "CompanyConsent3";
            this.lblCompanyConsent3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblCompanyConsent2
            // 
            this.lblCompanyConsent2.Dpi = 254F;
            this.lblCompanyConsent2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCompanyConsent2.LocationFloat = new DevExpress.Utils.PointFloat(204.688F, 2328.545F);
            this.lblCompanyConsent2.Name = "lblCompanyConsent2";
            this.lblCompanyConsent2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCompanyConsent2.SizeF = new System.Drawing.SizeF(1911.374F, 58.41992F);
            this.lblCompanyConsent2.StylePriority.UseFont = false;
            this.lblCompanyConsent2.StylePriority.UseTextAlignment = false;
            this.lblCompanyConsent2.Text = "CompanyConsent2";
            this.lblCompanyConsent2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblCompanyConsent
            // 
            this.lblCompanyConsent.Dpi = 254F;
            this.lblCompanyConsent.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCompanyConsent.LocationFloat = new DevExpress.Utils.PointFloat(1128.083F, 2166.938F);
            this.lblCompanyConsent.Name = "lblCompanyConsent";
            this.lblCompanyConsent.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCompanyConsent.SizeF = new System.Drawing.SizeF(974.8752F, 58.41992F);
            this.lblCompanyConsent.StylePriority.UseFont = false;
            this.lblCompanyConsent.StylePriority.UseTextAlignment = false;
            this.lblCompanyConsent.Text = "CompanyConsent";
            this.lblCompanyConsent.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Dpi = 254F;
            this.xrLabel26.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(204.688F, 2166.938F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(923.3955F, 58.41992F);
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.Text = "The Supplier cannot engage responsability of ";
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Dpi = 254F;
            this.xrLabel25.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(204.688F, 2084.917F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(1898.271F, 58.41992F);
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "The Supplier is solely responsible for his all actions and engagements in his nam" +
    "e.";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel24
            // 
            this.xrLabel24.Dpi = 254F;
            this.xrLabel24.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(204.688F, 2005.542F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(1918.353F, 58.4198F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = "The Supplier takes absolute responsability of usage over the pre-finance granted " +
    "to him/her.";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel23
            // 
            this.xrLabel23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.DateEcheance", "{0:dddd, MMMM d, yyyy}")});
            this.xrLabel23.Dpi = 254F;
            this.xrLabel23.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(1142.729F, 1915.584F);
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel23.SizeF = new System.Drawing.SizeF(962.312F, 58.42004F);
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.Text = "xrLabel23";
            // 
            // xrLabel22
            // 
            this.xrLabel22.Dpi = 254F;
            this.xrLabel22.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(204.688F, 1915.584F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(938.041F, 58.42004F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "The Supplier will pay the balance latest on the";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel21
            // 
            this.xrLabel21.Dpi = 254F;
            this.xrLabel21.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(204.6871F, 1834.899F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(1888.436F, 58.42004F);
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = "each delivery as instalmental repayment of the pre-finance.";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblRepayment
            // 
            this.lblRepayment.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.RemboursementTaux", "{0:#,#}")});
            this.lblRepayment.Dpi = 254F;
            this.lblRepayment.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblRepayment.LocationFloat = new DevExpress.Utils.PointFloat(269.4351F, 164.0417F);
            this.lblRepayment.Name = "lblRepayment";
            this.lblRepayment.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblRepayment.SizeF = new System.Drawing.SizeF(164.1667F, 58.42004F);
            this.lblRepayment.StylePriority.UseFont = false;
            this.lblRepayment.Text = "lblRepayment";
            this.lblRepayment.Visible = false;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Dpi = 254F;
            this.xrLabel16.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(204.688F, 1690.687F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(1935.54F, 58.42004F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "The Buyer acknowledges that what has been given as collateral has not been pledge" +
    "d somewhere else.";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblConditionRepayment
            // 
            this.lblConditionRepayment.Dpi = 254F;
            this.lblConditionRepayment.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblConditionRepayment.LocationFloat = new DevExpress.Utils.PointFloat(204.688F, 1771.399F);
            this.lblConditionRepayment.Name = "lblConditionRepayment";
            this.lblConditionRepayment.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblConditionRepayment.SizeF = new System.Drawing.SizeF(1918.353F, 58.42004F);
            this.lblConditionRepayment.StylePriority.UseFont = false;
            this.lblConditionRepayment.StylePriority.UseTextAlignment = false;
            this.lblConditionRepayment.Text = "Repayment condition";
            this.lblConditionRepayment.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Dpi = 254F;
            this.xrLabel15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(204.6871F, 1593.003F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(1921.001F, 58.42004F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "mouldy and/or slaty bean exceeding 10%.";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Dpi = 254F;
            this.xrLabel14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(204.688F, 1534.583F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(1898.5F, 58.42017F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "The Buyer and the Supplier agree on a discount of 20FCFA/Kg in case of high perce" +
    "ntage of";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblQualityCondition
            // 
            this.lblQualityCondition.Dpi = 254F;
            this.lblQualityCondition.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblQualityCondition.LocationFloat = new DevExpress.Utils.PointFloat(204.688F, 1452.774F);
            this.lblQualityCondition.Name = "lblQualityCondition";
            this.lblQualityCondition.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblQualityCondition.SizeF = new System.Drawing.SizeF(1921F, 58.42004F);
            this.lblQualityCondition.StylePriority.UseFont = false;
            this.lblQualityCondition.StylePriority.UseTextAlignment = false;
            this.lblQualityCondition.Text = "QualityCondition";
            this.lblQualityCondition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Dpi = 254F;
            this.xrLabel13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(204.688F, 1373.188F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(1921F, 58.42041F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Quality will be determined only by the quality analysis report from ";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblMontantEnLettre
            // 
            this.lblMontantEnLettre.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.MontantEnLettre, "Text", "")});
            this.lblMontantEnLettre.Dpi = 254F;
            this.lblMontantEnLettre.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblMontantEnLettre.LocationFloat = new DevExpress.Utils.PointFloat(50F, 164.0417F);
            this.lblMontantEnLettre.Name = "lblMontantEnLettre";
            this.lblMontantEnLettre.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblMontantEnLettre.SizeF = new System.Drawing.SizeF(219.4351F, 58.42017F);
            this.lblMontantEnLettre.StylePriority.UseFont = false;
            this.lblMontantEnLettre.Text = "lblMontantEnLettre";
            this.lblMontantEnLettre.Visible = false;
            // 
            // MontantEnLettre
            // 
            this.MontantEnLettre.Description = "Parameter1";
            this.MontantEnLettre.Name = "MontantEnLettre";
            this.MontantEnLettre.Visible = false;
            // 
            // lblSupplierCondition2
            // 
            this.lblSupplierCondition2.Dpi = 254F;
            this.lblSupplierCondition2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSupplierCondition2.LocationFloat = new DevExpress.Utils.PointFloat(206.77F, 1275.291F);
            this.lblSupplierCondition2.Name = "lblSupplierCondition2";
            this.lblSupplierCondition2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblSupplierCondition2.SizeF = new System.Drawing.SizeF(1805.375F, 58.41956F);
            this.lblSupplierCondition2.StylePriority.UseFont = false;
            this.lblSupplierCondition2.StylePriority.UseTextAlignment = false;
            this.lblSupplierCondition2.Text = "SupplierCondition2";
            this.lblSupplierCondition2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblSupplierCondition1
            // 
            this.lblSupplierCondition1.Dpi = 254F;
            this.lblSupplierCondition1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSupplierCondition1.LocationFloat = new DevExpress.Utils.PointFloat(204.1222F, 1203.642F);
            this.lblSupplierCondition1.Name = "lblSupplierCondition1";
            this.lblSupplierCondition1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblSupplierCondition1.SizeF = new System.Drawing.SizeF(1841.5F, 58.41968F);
            this.lblSupplierCondition1.StylePriority.UseFont = false;
            this.lblSupplierCondition1.StylePriority.UseTextAlignment = false;
            this.lblSupplierCondition1.Text = "SupplierCondition1";
            this.lblSupplierCondition1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lbl3rdCondition
            // 
            this.lbl3rdCondition.Dpi = 254F;
            this.lbl3rdCondition.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lbl3rdCondition.LocationFloat = new DevExpress.Utils.PointFloat(197F, 1098.656F);
            this.lbl3rdCondition.Name = "lbl3rdCondition";
            this.lbl3rdCondition.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lbl3rdCondition.SizeF = new System.Drawing.SizeF(1867.417F, 58.42004F);
            this.lbl3rdCondition.StylePriority.UseFont = false;
            this.lbl3rdCondition.StylePriority.UseTextAlignment = false;
            this.lbl3rdCondition.Text = "3rdCondition";
            this.lbl3rdCondition.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Dpi = 254F;
            this.xrLabel11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(197F, 1040.236F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(1867.417F, 58.42004F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = ". The acceptable weight will be the weight after refraction mentioned on the";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Dpi = 254F;
            this.xrLabel10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(197F, 981.816F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(605.896F, 58.42004F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = ". Slaty bean maximum 10%";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Dpi = 254F;
            this.xrLabel7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(197F, 923.3958F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(605.896F, 58.41998F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = ". Moisie bean maximum 10%";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblSupplierTonnage
            // 
            this.lblSupplierTonnage.Dpi = 254F;
            this.lblSupplierTonnage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSupplierTonnage.LocationFloat = new DevExpress.Utils.PointFloat(197F, 833.4375F);
            this.lblSupplierTonnage.Name = "lblSupplierTonnage";
            this.lblSupplierTonnage.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblSupplierTonnage.SizeF = new System.Drawing.SizeF(1898.5F, 58.41986F);
            this.lblSupplierTonnage.StylePriority.UseFont = false;
            this.lblSupplierTonnage.StylePriority.UseTextAlignment = false;
            this.lblSupplierTonnage.Text = "SupplierTonnage";
            this.lblSupplierTonnage.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblSupplierLocation
            // 
            this.lblSupplierLocation.Dpi = 254F;
            this.lblSupplierLocation.Font = new System.Drawing.Font("Segoe UI Black", 14F);
            this.lblSupplierLocation.LocationFloat = new DevExpress.Utils.PointFloat(197F, 738.399F);
            this.lblSupplierLocation.Name = "lblSupplierLocation";
            this.lblSupplierLocation.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblSupplierLocation.SizeF = new System.Drawing.SizeF(1898.5F, 58.42004F);
            this.lblSupplierLocation.StylePriority.UseFont = false;
            this.lblSupplierLocation.StylePriority.UseTextAlignment = false;
            this.lblSupplierLocation.Text = "SupplierAndLocation";
            this.lblSupplierLocation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Dpi = 254F;
            this.xrLabel9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(197F, 679.9792F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(907.5211F, 58.41998F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "It has been agreed as follows :";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Dpi = 254F;
            this.xrLabel8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(664.104F, 579.5435F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(907.521F, 58.41998F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "The supplier on the other hand";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("Segoe UI Black", 14F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(870.479F, 425.9792F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(425.9791F, 58.41995F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "AND";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(661.4582F, 354.5417F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(907.5209F, 58.41998F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "The buyer on one hand";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Dpi = 254F;
            this.xrLabel6.Font = new System.Drawing.Font("Segoe UI Black", 14F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(899.5833F, 164.0417F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(425.9792F, 58.42F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "BETWEEN";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lblInfosSociete
            // 
            this.lblInfosSociete.Dpi = 254F;
            this.lblInfosSociete.Font = new System.Drawing.Font("Segoe UI Black", 13F);
            this.lblInfosSociete.LocationFloat = new DevExpress.Utils.PointFloat(25.00001F, 245.9567F);
            this.lblInfosSociete.Name = "lblInfosSociete";
            this.lblInfosSociete.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInfosSociete.SizeF = new System.Drawing.SizeF(2093F, 74.29495F);
            this.lblInfosSociete.StylePriority.UseFont = false;
            this.lblInfosSociete.StylePriority.UseTextAlignment = false;
            this.lblInfosSociete.Text = "CompanyInfo";
            this.lblInfosSociete.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.lblInfosSociete.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SetReportContent);
            // 
            // xrTable1
            // 
            this.xrTable1.Dpi = 254F;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 100.5417F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(2043.124F, 63.49999F);
            this.xrTable1.Visible = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblNomSociete,
            this.lblAdresseSociete,
            this.lblTel1Societe,
            this.lblTel2Societe,
            this.lblLocation,
            this.lblTonnage,
            this.lblVilleSociete,
            this.lblDateAvance,
            this.lblMontantAvance,
            this.lblVille2Societe,
            this.lblCampagne,
            this.lblNumero});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // lblNomSociete
            // 
            this.lblNomSociete.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.NomSociete")});
            this.lblNomSociete.Dpi = 254F;
            this.lblNomSociete.Name = "lblNomSociete";
            this.lblNomSociete.Weight = 0.42443928116046609D;
            // 
            // lblAdresseSociete
            // 
            this.lblAdresseSociete.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.AdresseSociete")});
            this.lblAdresseSociete.Dpi = 254F;
            this.lblAdresseSociete.Name = "lblAdresseSociete";
            this.lblAdresseSociete.Weight = 0.567380237056108D;
            // 
            // lblTel1Societe
            // 
            this.lblTel1Societe.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.TelSociete")});
            this.lblTel1Societe.Dpi = 254F;
            this.lblTel1Societe.Name = "lblTel1Societe";
            this.lblTel1Societe.Weight = 0.34493894595472663D;
            // 
            // lblTel2Societe
            // 
            this.lblTel2Societe.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.Tel2Societe")});
            this.lblTel2Societe.Dpi = 254F;
            this.lblTel2Societe.Name = "lblTel2Societe";
            this.lblTel2Societe.Weight = 0.38841899820176495D;
            // 
            // lblLocation
            // 
            this.lblLocation.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.Provenance")});
            this.lblLocation.Dpi = 254F;
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Visible = false;
            this.lblLocation.Weight = 0.38841899820176495D;
            // 
            // lblTonnage
            // 
            this.lblTonnage.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.Tonnage", "{0:#,#}")});
            this.lblTonnage.Dpi = 254F;
            this.lblTonnage.Name = "lblTonnage";
            this.lblTonnage.Weight = 0.29734817003143132D;
            // 
            // lblVilleSociete
            // 
            this.lblVilleSociete.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.VilleSociete")});
            this.lblVilleSociete.Dpi = 254F;
            this.lblVilleSociete.Name = "lblVilleSociete";
            this.lblVilleSociete.Weight = 0.29734817003143132D;
            // 
            // lblDateAvance
            // 
            this.lblDateAvance.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.DateFinancement", "{0:dddd, MMMM d, yyyy}")});
            this.lblDateAvance.Dpi = 254F;
            this.lblDateAvance.Name = "lblDateAvance";
            this.lblDateAvance.Weight = 0.29734817003143132D;
            // 
            // lblMontantAvance
            // 
            this.lblMontantAvance.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.Montant", "{0:#,#}")});
            this.lblMontantAvance.Dpi = 254F;
            this.lblMontantAvance.Name = "lblMontantAvance";
            this.lblMontantAvance.Weight = 0.29734817003143132D;
            // 
            // lblVille2Societe
            // 
            this.lblVille2Societe.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.Ville2Societe")});
            this.lblVille2Societe.Dpi = 254F;
            this.lblVille2Societe.Name = "lblVille2Societe";
            this.lblVille2Societe.Weight = 0.29734817003143132D;
            // 
            // lblCampagne
            // 
            this.lblCampagne.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.Campagne")});
            this.lblCampagne.Dpi = 254F;
            this.lblCampagne.Name = "lblCampagne";
            this.lblCampagne.Weight = 0.15106547967766593D;
            // 
            // lblNumero
            // 
            this.lblNumero.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Financement_PrintAgreement.Numero")});
            this.lblNumero.Dpi = 254F;
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Weight = 0.20048138094815388D;
            // 
            // xrLabel3
            // 
            this.xrLabel3.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel3.BorderWidth = 3F;
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Segoe UI Black", 22F);
            this.xrLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(410.7291F, 241.3F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(1439.75F, 147.6375F);
            this.xrLabel3.StylePriority.UseBorderDashStyle = false;
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseBorderWidth = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseForeColor = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "PROTOCOL ACCORD";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 6.708328F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Dpi = 254F;
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(10.3125F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(309.0334F, 228.6F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 254F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Dpi = 254F;
            this.xrPageInfo1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.Format = "Printed on : {0}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(93.13333F, 156.6333F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(2002.367F, 58.42F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // FinancementID
            // 
            this.FinancementID.Name = "FinancementID";
            this.FinancementID.Type = typeof(System.Guid);
            this.FinancementID.ValueInfo = "efb146fa-e126-4491-ad50-a405658c7c62";
            this.FinancementID.Visible = false;
            // 
            // sqlDataSource2
            // 
            this.sqlDataSource2.ConnectionName = "Tms2017_ReportServer";
            this.sqlDataSource2.Name = "sqlDataSource2";
            storedProcQuery1.Name = "Financement_PrintAgreement";
            queryParameter1.Name = "@ID";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.FinancementID]", typeof(System.Guid));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.StoredProcName = "Financement_PrintAgreement";
            this.sqlDataSource2.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource2.ResultSchemaSerializable = resources.GetString("sqlDataSource2.ResultSchemaSerializable");
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrLabel3});
            this.ReportHeader.Dpi = 254F;
            this.ReportHeader.HeightF = 441.8542F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // rptFinancingAgreement
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.GrpHeader,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource2});
            this.DataMember = "Financement_PrintAgreement";
            this.DataSource = this.sqlDataSource2;
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(0, 16, 7, 254);
            this.PageHeight = 2794;
            this.PageWidth = 2159;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.FinancementID,
            this.MontantEnLettre});
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "17.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    
    private void SetReportContent(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        XRLabel lbl = sender as XRLabel;
        lblReference.Text = "OUR REF : "+ lblNumero.Text + "/" + lblCampagne.Text; 
        lblInfosSociete.Text = lblNomSociete.Text.ToUpper() + " , " + lblAdresseSociete.Text.ToUpper() + " , TEL. " + lblTel1Societe.Text + " / " + lblTel2Societe.Text ;
        lblSupplierInfo.Text = lblSupplier.Text.ToUpper();
        lblSupplierLocation.Text = lblSupplier.Text.ToUpper() + " in the " + lblLocation.Text.ToUpper() + " locality ";
        decimal tonnage = string.IsNullOrEmpty(lblTonnage.Text) ? 0 : decimal.Parse(lblTonnage.Text) / 1000;
        lblSupplierTonnage.Text = "The supplier agrees to supply " + tonnage.ToString() + " tons of cocoa under the following conditions :";
        lbl3rdCondition.Text = lblNomSociete.Text.ToUpper() + " " + lblVilleSociete.Text + ", entry report (bon d'entrée).";
        lblSupplierCondition1.Text = "The supplier " + lblSupplier.Text.ToUpper() + " has been given this day " + lblDateAvance.Text + " the sum of";
        lblSupplierCondition2.Text = lblMontantAvance.Text + " FCFA (" + lblMontantEnLettre.Text + ")" ;
        lblQualityCondition.Text = lblNomSociete.Text.ToUpper() + " processing plant in " + lblVilleSociete.Text +".";
        lblConditionRepayment.Text = "The parties have agreed that minimum " + lblRepayment.Text + " FCFA/KG will be retained on";
        lblCompanyConsent.Text = lblNomSociete.Text + " without ";
        lblCompanyConsent1.Text = lblNomSociete.Text + "'s consent.";
        lblCompanyConsent2.Text = "Where applicable , this engagement should be on " + lblNomSociete.Text + "'s Letter Head, Stamped and Signed by appropriate authority.";
        lblCompanyConsent3.Text = lblNomSociete.Text.ToUpper() + " reserve the right to take legal action against the Supplier in case of non-respect of the above mentioned points.";
        lblCompanyConsent31.Text = "The " + lblVilleSociete.Text + " Tribunal or " + lblVille2Societe.Text + " Tribunal will be competent authority for legal action.";

    }

    
}
