using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using CrystalDecisions.Shared;
namespace Tms.Components.Reports
{
    public class ReportParameterCollection : CollectionBase
    {

        public ReportParameter this[int Index]
        {
            get { return (ReportParameter)List[Index]; }
        }

        public void @add(ReportParameter mParameter)
        {
            List.Add(mParameter);
        }

        public void @add(string mName, ParameterValueKind mValueKind, object mValue)
        {
            ReportParameter mParameter = new ReportParameter();
            mParameter.Name = mName;
            mParameter.ValueKind = mValueKind;
            mParameter.Value = mValue;
            List.Add(mParameter);
        }
    }

}
