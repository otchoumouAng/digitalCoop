using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using CrystalDecisions.Shared;

namespace Tms.Components.Reports
{
    public class ReportParameter
    {
        private string _Name;
        private ParameterValueKind _ValueKind;

        private object _Value;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public ParameterValueKind ValueKind
        {
            get { return _ValueKind; }
            set { _ValueKind = value; }
        }

        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }

}
