using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Tms.Components.Reports
{
    public class ReportInfos
    {
        private ParameterFields _ParamFields;
        public ParameterFields ParamFields
        {
            get { return _ParamFields; }
            set { _ParamFields = value; }
        }

        private ReportDocument _Report;
        public ReportDocument Report
        {
            get { return _Report; }
            set { _Report = value; }
        }

        private string _ReportTitle;
        public string ReportTitle
        {
            get { return _ReportTitle; }
            set { _ReportTitle = value; }
        }

        private bool _ShowPrintButton;
        public bool ShowPrintButton
        {
            get { return _ShowPrintButton; }
            set { _ShowPrintButton = value; }
        }

        private bool _ShowExportButton;
        public bool ShowExportButton
        {
            get { return _ShowExportButton; }
            set { _ShowExportButton = value; }
        }

    }
}

