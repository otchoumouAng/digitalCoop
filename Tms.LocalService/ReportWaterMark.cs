using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Tms.LocalService
{
    public static class ReportWaterMark
    {
        public static void SetTextWatermark(XtraReport report)
        {
            // Set Water Mark To Copy reports
            // Call in Report
            report.Watermark.Text = "COPY";
            report.Watermark.TextDirection = DirectionMode.ForwardDiagonal;
            report.Watermark.Font = new Font(report.Watermark.Font.FontFamily, 70);
            report.Watermark.ForeColor = Color.Black;
            report.Watermark.TextTransparency = 185;
            report.Watermark.ShowBehind = false;            
        }
    }
}