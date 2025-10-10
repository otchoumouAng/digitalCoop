using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Tms.Components.Settings
{
    public class Setting
    {

        #region "Fields"

        private string _Key;

        private string _Value;
        #endregion
        private const string _ConcatString = "&";

        #region "Property"
        //Get/Set Key of the setting
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        //Get/Set value of the setting
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        #endregion

        #region "Members"

        //Concat Key and Value of the setting using &
        public override string ToString()
        {
            return _Key + _ConcatString + _Value;
        }

        //Extract Key and Value from the setting string
        public bool InString(string mSetting)
        {
            int intPosition = 0;
            bool bolReturn = false;

            intPosition = Strings.InStr(mSetting, _ConcatString);
            if (intPosition > 0)
            {
                _Key = Strings.Mid(mSetting, 1, intPosition - 1);
                _Value = Strings.Mid(mSetting, intPosition + 1);
                bolReturn = true;
            }
            else {
                _Key = "";
                _Value = "";
                bolReturn = false;
            }
            return bolReturn;
        }
        #endregion
    }
}
