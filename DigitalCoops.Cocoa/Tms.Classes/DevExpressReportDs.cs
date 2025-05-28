using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Classes
{
    public static class DevExpressReportDs
    {
        //string conn = ConfigurationManager.ConnectionStrings["TMS2017_ReportServer"].ConnectionString;

        public static SqlDataSource SetDataSource(XtraReport report)
        {
            //CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(ConfigurationManager.ConnectionStrings["TMS2017_ReportServer"].ConnectionString);           
            SqlDataSource ds = report.DataSource as SqlDataSource;
            //MsSqlConnectionParameters connectionParameters = new MsSqlConnectionParameters("ABTECHLT001\\SQL2014", "TMS2017", "tmsAdmin", "tmsAdmin@17", MsSqlAuthorizationType.SqlServer);
            //MsSqlConnectionParameters connectionParameters = new MsSqlConnectionParameters("BTECHSVR2017\\SQL2014", "TMS2017", "tmsAdmin", "tmsAdmin@2017", MsSqlAuthorizationType.SqlServer);
            //MsSqlConnectionParameters connectionParameters = new MsSqlConnectionParameters("HYPER-V4\\MSSQLSERVER2014", "TMS2017UAT", "tmsAdmin", "tmsAdmin@17", MsSqlAuthorizationType.SqlServer);
            MsSqlConnectionParameters connectionParameters = new MsSqlConnectionParameters("HYPER-V4\\MSSQLSERVER2014", "TMS2017LIVE", "tmsAdmin", "tmsAdmin@17", MsSqlAuthorizationType.SqlServer);
            //MsSqlConnectionParameters connectionParameters = new MsSqlConnectionParameters("DESKTOP-EPDGN66", "TMS2017LIVE", "tmsAdmin", "tmsAdmin@17", MsSqlAuthorizationType.SqlServer);

            ds.ConnectionName = "TMS2017_ReportServer";            
            ds.ConnectionParameters = connectionParameters;

            return ds;
        }

        //string connectionString = @"XpoProvider=MSSqlServer;Data Source=(local);User ID=username;Password=password;Initial Catalog=database;Persist Security Info=true;";
        
    }
}
