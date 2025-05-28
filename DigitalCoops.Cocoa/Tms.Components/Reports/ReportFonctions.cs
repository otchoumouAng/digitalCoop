using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using CrystalDecisions.Shared;

namespace Tms.Components.Reports
{
    public class ReportFonctions
    {
        public static ParameterField CreateParameter(string mName, ParameterValueKind mValueKind, object mValue)
        {
            ParameterDiscreteValue dval = new ParameterDiscreteValue();
            dval.Value = mValue;
            ParameterField ParamField = new ParameterField();
            ParamField.Name = mName;
            ParamField.ParameterValueType = mValueKind;
            ParamField.PromptingType = DiscreteOrRangeKind.DiscreteValue;
            ParamField.CurrentValues.Add(dval);
            return ParamField;
        }
    }

}
