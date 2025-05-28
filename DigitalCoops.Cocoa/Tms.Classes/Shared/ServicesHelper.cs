using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Classes.Shared
{

    public class ServicesHelper
    {
        public static ServiceLib.IService CreateClientServiceInstance(string ip_address)
        {
            //System.Web.HttpContext context = currentContext;//System.Web.HttpContext.Current;
            //string lblAddresseIP = context.Request.ServerVariables["REMOTE_ADDR"].Equals("::1") ? "localhost" : context.Request.ServerVariables["REMOTE_ADDR"];
            string portService = "9000";//(new lbc_DefaultParameters()).PortRunningService + "";
            EndpointAddress ep = new EndpointAddress("net.tcp://" + (ip_address.Equals("::1") ? "localhost" : ip_address )+ ":" + portService + "/ServiceLib/Services");
            NetTcpBinding tcpb = CreateTcpBinding();
            ServiceLib.IService proxy = ChannelFactory<ServiceLib.IService>.CreateChannel(tcpb, ep);            
            return proxy;
        }

        public static NetTcpBinding CreateTcpBinding()
        {
            NetTcpBinding tcpb = new NetTcpBinding();
            tcpb.TransferMode = TransferMode.Streamed;
            tcpb.MaxBufferPoolSize = 2147483647;
            tcpb.MaxBufferSize = 2147483647;
            tcpb.MaxReceivedMessageSize = 2147483647;

            tcpb.ReaderQuotas.MaxDepth = 2000000;
            tcpb.ReaderQuotas.MaxStringContentLength = 2147483647;
            tcpb.ReaderQuotas.MaxArrayLength = 2147483647;
            tcpb.ReaderQuotas.MaxBytesPerRead = 2147483647;
            tcpb.ReaderQuotas.MaxNameTableCharCount = 2147483647;

            tcpb.OpenTimeout = TimeSpan.FromMinutes(3);
            tcpb.CloseTimeout = TimeSpan.FromMinutes(3);
            tcpb.ReceiveTimeout = TimeSpan.FromMinutes(10);
            tcpb.SendTimeout = TimeSpan.FromMinutes(3);

            return tcpb;
        }
    }
}
