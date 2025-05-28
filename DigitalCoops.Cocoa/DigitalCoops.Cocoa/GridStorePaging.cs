using Ext.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tms.Components.Data;

namespace Tms2017.MVC
{
    public static class GridStorePaging
    {
        public static Paging<DataPersist> mSetRangePlants(StoreRequestParameters parameters, List<DataPersist> mListe)
        {
            int start = parameters.Start;

            int limit = parameters.Limit;

            if ((start + limit) > mListe.Count)
            {
                limit = mListe.Count - start;
            }

            List<DataPersist> rangePlants = (start < 0 || limit < 0) ? mListe : mListe.GetRange(start, limit);


            return new Paging<DataPersist>(rangePlants, mListe.Count);
        }

        public static Paging<DataPersist> SetRangePlants(StoreRequestParameters parameters , List<DataPersist> mListe, string filterheader = null)
        {            
            return PlantsPaging(parameters.Start, parameters.Limit, parameters.SimpleSort, parameters.SimpleSortDirection, filterheader, mListe);
        }

        public static Paging<DataPersist> PlantsPaging(int start, int limit, string sort, SortDirection dir, string filter, List<DataPersist> mListe)
        {
            List<DataPersist> plants = mListe;            

            FilterHeaderConditions fhc = new FilterHeaderConditions(filter);

            foreach (FilterHeaderCondition condition in fhc.Conditions)
            {
                string dataIndex = condition.DataIndex;
                FilterType type = condition.Type;
                string op = condition.Operator;
                object value = null;

                switch (condition.Type)
                {
                    case FilterType.Boolean:
                        value = condition.Value<bool>();
                        break;

                    case FilterType.Date:
                        switch (condition.Operator)
                        {
                            case "=":
                                value = condition.Value<DateTime>();
                                break;

                            case "compare":
                                value = FilterHeaderComparator<DateTime>.Parse(condition.JsonValue);
                                break;
                        }
                        break;

                    case FilterType.Number:
                        bool isInt = mListe.Count > 0 && mListe[0].GetType().GetProperty(dataIndex).PropertyType == typeof(int);
                        switch (condition.Operator)
                        {
                            case "=":
                                if (isInt)
                                {
                                    value = condition.Value<int>();
                                }
                                else
                                {
                                    value = condition.Value<decimal>();
                                }
                                break;

                            case "compare":
                                if (isInt)
                                {
                                    value = FilterHeaderComparator<int>.Parse(condition.JsonValue);
                                }
                                else
                                {
                                    value = FilterHeaderComparator<decimal>.Parse(condition.JsonValue);
                                }

                                break;
                        }

                        break;
                    case FilterType.String:
                        value = condition.Value<string>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                mListe.RemoveAll(item =>
                {
                    object oValue = item.GetType().GetProperty(dataIndex).GetValue(item, null);

                    string nullable = item.GetType().GetProperty(dataIndex).PropertyType.FullName;

                    if (oValue == null)
                    {
                        return true;
                    }

                    string matchValue = null;
                    string itemValue = null;

                    if (type == FilterType.String)
                    {
                        matchValue = (string)value;
                        itemValue = oValue as string;
                    }

                    switch (op)
                    {
                        case "=":
                            return oValue == null || !oValue.Equals(value);
                        case "compare":
                            return !((IEquatable<IComparable>)value).Equals((IComparable)oValue);
                        case "+":
                            return itemValue == null || !itemValue.ToLowerInvariant().Contains(matchValue.ToLowerInvariant());
                        case "-":
                            return itemValue == null || !itemValue.ToLowerInvariant().EndsWith(matchValue.ToLowerInvariant());
                        case "!":
                            return itemValue == null || itemValue.ToLowerInvariant().IndexOf(matchValue.ToLowerInvariant()) >= 0;
                        case "*":
                            return itemValue == null || itemValue.ToLowerInvariant().IndexOf(matchValue.ToLowerInvariant()) < 0;
                        default:
                            throw new Exception("Not supported operator");
                    }
                });
            }
            //-- end filtering   ------------------------------------------------------------------------------------------------------------------------------
                        
            if (!string.IsNullOrEmpty(sort))
            {
                plants.Sort(delegate (DataPersist x, DataPersist y)
                {
                    object a;
                    object b;

                    int direction = dir == SortDirection.DESC ? -1 : 1;

                    a = x.GetType().GetProperty(sort).GetValue(x, null);
                    b = y.GetType().GetProperty(sort).GetValue(y, null);

                    return CaseInsensitiveComparer.Default.Compare(a, b) * direction;
                });
            }

            if ((start + limit) > plants.Count)
            {
                limit = plants.Count - start;
            }

            List<DataPersist> rangePlants = (start < 0 || limit < 0) ? plants : plants.GetRange(start, limit);

            return new Paging<DataPersist>(rangePlants, plants.Count);
        }
    }
}