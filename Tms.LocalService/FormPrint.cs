using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tms.LocalService
{
    public partial class FormPrint : Form
    {
        public FormPrint()
        {
            InitializeComponent();
        }

        public void PrintReport(XtraReport rep)
        {
            try
            {
                rep.ShowPrintMarginsWarning = false;                
                ReportPrintTool pt = new ReportPrintTool(rep);
                pt.PrintingSystem.StartPrint +=
                    new PrintDocumentEventHandler(printingSystem_StartPrint);                
                pt.Print();
            }
            catch (Exception Ex)
            {
                
            }     
            
        }

        private void printingSystem_StartPrint(object sender, PrintDocumentEventArgs e) {
            // Set the printer name.
            //e.PrintDocument.PrinterSettings.PrinterName =
            //    PrinterSettings.InstalledPrinters[0];
            foreach (string name in PrinterSettings.InstalledPrinters)
            {
                PrinterSettings ps = new PrinterSettings();
                ps.PrinterName = name;
                if (ps.IsDefaultPrinter)
                    e.PrintDocument.PrinterSettings.PrinterName = name;
            }            
        }
    }
}
