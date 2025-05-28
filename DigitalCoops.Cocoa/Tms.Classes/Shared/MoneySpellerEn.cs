using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Classes.Shared
{
    public static class MoneySpellerEn
    {
        public static string Spell(string Number)
        {
            string Dollars = string.Empty;
            string Cents = string.Empty;
            string Temp = string.Empty;
            int DecimalPlace;
            int Count;
            string[] Place = new string[9];
            string strCurrency = "CFA francs";
            string strCurrencyCents = "Cents";

            //string Number = Number.ToString();

            Place[2] = " Thousand ";
            Place[3] = " Million ";
            Place[4] = " Billion ";
            Place[5] = " Trillion ";

            //String representation of amount
            //Number = CStr(Number)
            Number = Convert.ToString(Convert.ToDecimal(Number));

            // Position of decimal place 0 if none
            //Remove this because decimal sign can be either . or ,
            //'***************************************
            //'    DecimalPlace = InStr(Number, ".")
            //'***************************************
            //'Replace by That
            //'***********************************************************
            DecimalPlace = Number.IndexOf(".");
            if (DecimalPlace != -1)
            {
                DecimalPlace = int.Parse(Number.Substring(DecimalPlace + 1));
            }

            if (DecimalPlace > 0)
            {
                Cents = GetTens((Number.Substring(DecimalPlace + 1)).PadLeft(2, '0'));
                Number = Number.Trim().PadLeft(DecimalPlace - 1);
            }

            Count = 1;

            do
            {
                if (Number.Length == 1)
                {
                    Number = "00" + Number;
                }
                if (Number.Length == 2 )
                {
                    Number = "0" + Number;
                }
                Temp = GetHundreds(Number.Substring(Number.Length - 3));

                if (Temp != "") Dollars = Temp + Place[Count] + Dollars;

                if (Number.Length > 3) Number = Number.Substring(0, Number.Length - 3);
                else Number = "";

                Count += 1;
            }
            while (Number != "");

            switch (Dollars)
            {
                case "":
                    Dollars = "zero " + strCurrency;
                    break;
                case "One":
                    Dollars = "One " + strCurrency;
                    break;
                default:
                    Dollars = Dollars + " " + strCurrency;
                    break;

            }

            if (Cents != "")
            {

                switch (Cents)
                {
                    case "":
                        Cents = " and zero " + strCurrencyCents.Substring(0, strCurrency.Length - 1);
                        break;
                    case "One":
                        Cents = " and One " + strCurrencyCents.Substring(0, strCurrency.Length - 1);
                        break;
                    default:
                        Cents = " and " + Cents + " " + strCurrencyCents;
                        break;

                }
            }

            return Dollars + Cents;
        }

        public static string SpellEuro(string Number)
        {
            string Dollars = string.Empty;
            string Cents = string.Empty;
            string Temp = string.Empty;
            int DecimalPlace;
            int Count;
            string[] Place = new string[9];
            string strCurrency = " €";
            string strCurrencyCents = "Cents";

            //string Number = Number.ToString();

            Place[2] = " Thousand ";
            Place[3] = " Million ";
            Place[4] = " Billion ";
            Place[5] = " Trillion ";

            //String representation of amount
            //Number = CStr(Number)
            Number = Convert.ToString(Convert.ToDecimal(Number));

            // Position of decimal place 0 if none
            //Remove this because decimal sign can be either . or ,
            //'***************************************
            //'    DecimalPlace = InStr(Number, ".")
            //'***************************************
            //'Replace by That
            //'***********************************************************
            DecimalPlace = Number.IndexOf(",");
            if (DecimalPlace != -1)
            {
                DecimalPlace = int.Parse(Number.Substring(DecimalPlace + 1));
            }

            if (DecimalPlace > 0)
            {
                Cents = GetTens((Number.Substring(DecimalPlace + 1)).PadLeft(2, '0'));
                Number = Number.Trim().PadLeft(DecimalPlace - 1);
            }

            Count = 1;

            do
            {
                if (Number.Length == 1)
                {
                    Number = "00" + Number;
                }
                if (Number.Length == 2)
                {
                    Number = "0" + Number;
                }
                Temp = GetHundreds(Number.Substring(Number.Length - 3));

                if (Temp != "") Dollars = Temp + Place[Count] + Dollars;

                if (Number.Length > 3) Number = Number.Substring(0, Number.Length - 3);
                else Number = "";

                Count += 1;
            }
            while (Number != "");

            switch (Dollars)
            {
                case "":
                    Dollars = "zero " + strCurrency;
                    break;
                case "One":
                    Dollars = "One " + strCurrency;
                    break;
                default:
                    Dollars = Dollars + " " + strCurrency;
                    break;

            }

            if (Cents != "")
            {

                switch (Cents)
                {
                    case "":
                        Cents = " and zero " + strCurrencyCents.Substring(0, strCurrency.Length - 1);
                        break;
                    case "One":
                        Cents = " and One " + strCurrencyCents.Substring(0, strCurrency.Length - 1);
                        break;
                    default:
                        Cents = " and " + Cents + " " + strCurrencyCents;
                        break;

                }
            }

            return Dollars + Cents;
        }


        private static string GetHundreds(string Number)
        {
            string result = string.Empty;

            if (int.Parse(Number) == 0 )
            {
                return string.Empty;
            }
            Number = "000" + Number;
            Number = Number.Substring(Number.Length - 3, 3 );

            if (Number.Substring(0,1) != "0")
            {
                result = GetDigit(Number.Substring(0, 1)) + " Hundred ";
            }

            if (Number.Substring(1, 1) != "0")
            {
                result = result + GetTens(Number.Substring(1));
            }

            if (Number.Substring(2) != "0"  && (Number.Substring(1, 1) != "1") )
            {
                result = result + GetDigit(Number.Substring(2));
            }

            return result;
        }

        private static string GetTens(string Number)
        {
            string result = string.Empty;

            if (int.Parse(Number.Substring(0,1)) == 1)
            {
                switch (int.Parse(Number)){
                    case 10:
                        result = "Ten";
                        break;
                    case 11:
                        result = "Eleven";
                        break;
                    case 12:
                        result = "Twelve";
                        break;
                    case 13:
                        result = "Thirteen";
                        break;
                    case 14:
                        result = "Fourteen";
                        break;
                    case 15:
                        result = "Fifteen";
                        break;
                    case 16:
                        result = "Sixteen";
                        break;
                    case 17:
                        result = "Seventeen";
                        break;
                    case 18:
                        result = "Eighteen";
                        break;
                    case 19:
                        result = "Nineteen";
                        break;                    
                }
            }
            else
            {
                switch (int.Parse(Number.Substring(0,1)))
                {
                    case 2:
                        result = "Twenty ";
                        break;
                    case 3:
                        result = "Thirty ";
                        break;
                    case 4:
                        result = "Forty ";
                        break;
                    case 5:
                        result = "Fifty ";
                        break;
                    case 6:
                        result = "Sixty ";
                        break;
                    case 7:
                        result = "Seventy ";
                        break;
                    case 8:
                        result = "Eighty ";
                        break;
                    case 9:
                        result = "Ninety ";
                        break;                    
                }
            }

            return result;
        }

        private static string GetDigit(string Digit)
        {
            if (Digit != "")
            {
                switch (int.Parse(Digit))
                {
                    case 1: return "One";
                    case 2: return "Two";
                    case 3: return "Three";
                    case 4: return "Four";
                    case 5: return "Five";
                    case 6: return "Six";
                    case 7: return "Seven";
                    case 8: return "Eight";
                    case 9: return "Nine";
                    default: return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}
