using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.LocalService
{
    public static class PrintMethod
    {
        public static void Print(XtraReport rep)
        {
            FormPrint frm = new FormPrint();

            frm.PrintReport(rep);
        }

        public static SqlDataSource SetDataSource(XtraReport report)
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["TMS2017_ReportServer"].ConnectionString;

            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(connection);
            SqlDataSource ds = report.DataSource as SqlDataSource;
            //MsSqlConnectionParameters connectionParameters = new MsSqlConnectionParameters("BTECH001\\SQL2014", "TMS2017", "tmsAdmin", "tmsAdmin@2017", MsSqlAuthorizationType.SqlServer);            
            ds.ConnectionName = "TMS2017_ReportServer";
            ds.ConnectionParameters = connectionParameters;

            return ds;
        }
    }
}
