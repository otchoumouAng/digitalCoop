using ServiceHote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tms.Components.Settings;
using Tms.LocalService;

namespace Tms.LocalService
{
    public partial class frmLocalService : Form
    {
        Form currentForm;
        bool bCloseWindow = false;
        Thread mThread;
        bool bStarted = false;
        DataConfig df;
        ServiceHost m_svcHost;

        static string strClasseService = "Services";

        const string cAppControlsSettings = "AppControlsSettings.config";
        private static string mFile;
        private static XMLSettingsFileManager conf = new XMLSettingsFileManager();

        public frmLocalService()
        {
            InitializeComponent();

            currentForm = this;
            this.ShowInTaskbar = false;
            currentForm.WindowState = FormWindowState.Minimized;
            currentForm.Visible = false;
            btnStartService.Click += btnStartService_Click;
            btnStopService.Click += btnStopService_Click;
            btnQuit.Click += btnQuitter_Click;
            btnChangeProfile.Click += mnuChangeProfile_Click;
            DemarrerService();
        }

        public static void ReinitAppControlsSettingsParams()
        {
            //Détermination de l'emplacement du fichier AppControlsSettings.config
            const string cTmsDirectory = "TMS";
            string repertoire = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + cTmsDirectory;
            if (!Directory.Exists(repertoire))
            {
                Directory.CreateDirectory(repertoire);
            }
            string pathAppCS = repertoire + "\\" + cAppControlsSettings;
            if (!File.Exists(pathAppCS))
            {
                File.Delete(pathAppCS);
            }

            if (!File.Exists(pathAppCS))
            {
                string defAppCS = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + cAppControlsSettings;
                //System.Windows.Forms.MessageBox.Show("file : " + defAppCS);
                if (File.Exists(defAppCS))
                {
                    File.Copy(defAppCS, pathAppCS);
                }
            }
            if (File.Exists(pathAppCS))
            {
                conf.cfgFile = pathAppCS;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Error when creating file. " + cAppControlsSettings);
            }
        }

        public static void CopyAppControlsSettingsParams()
        {
            const string cTmsDirectory = "TMS";
            string repertoire = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + cTmsDirectory;
            if (!Directory.Exists(repertoire))
            {
                Directory.CreateDirectory(repertoire);
            }
            string pathAppCS = repertoire + "\\App.config";
            if (!File.Exists(pathAppCS))
            {
                File.Delete(pathAppCS);
            }

            if (!File.Exists(pathAppCS))
            {
                string defAppCS = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\App.config";
                //System.Windows.Forms.MessageBox.Show("file : " + defAppCS);
                if (File.Exists(defAppCS))
                {
                    File.Copy(defAppCS, pathAppCS);
                }
            }
            if (File.Exists(pathAppCS))
            {
                conf = new XMLSettingsFileManager();
                conf.cfgFile = pathAppCS;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Error when creating file. " + cAppControlsSettings);
            }
        }


        public void CreateConfigFileIntoUserAppData()
        {
            try
            {
                //df = new DataConfig();
                //Tms.Components.Data.DataConfig _DataConfig;                
                //_DataConfig.Server = df.Server;
                //_DataConfig.DataBase = df.DataBase;
                //_DataConfig.UserName = df.UserName;
                //_DataConfig.Password = df.Password;
                //_DataConfig.Others = df.Others;
                //_DataConfig.Language = df.Language;                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void DemarrerService()
        {
            try
            {
                if (!bStarted)
                {
                    // Doit  on conserver le fichier AppControlsSettings ? 
                    ReinitAppControlsSettingsParams();                  
                    //CopyAppControlsSettingsParams();
                    CreateConfigFileIntoUserAppData();
                    StartTcpService();
                    ResetFormButtons();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("frmBackGroundService : StartTcpService : {0}", ex.Message), "Erreur");
                this.Close();
            }
        }

        private void ResetFormButtons()
        {
            notifyIcon1.ShowBalloonTip(15000, "Service", "Service has successfully started.", ToolTipIcon.Info);
            bStarted = true;
            btnStartService.Enabled = !bStarted;
            btnStopService.Enabled = bStarted;
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            bCloseWindow = true;
            currentForm.Close();
        }

        public void ArreterService()
        {
            if (m_svcHost != null)
            {
                m_svcHost.Close();
                m_svcHost = null;
            }

            notifyIcon1.ShowBalloonTip(15000, "Service", "Service is stopped.", ToolTipIcon.Info);
            bStarted = false;
            btnStartService.Enabled = !bStarted;
            btnQuit.Enabled = !bStarted;

        }

        // Démarrage du service Wams Service
        private void StartTcpService()
        {

            try
            {

                string portService = "9000";//(new DefaultParameters()).PortRunningService + "";
                //System.Net.
                string strAdrTCP = "net.tcp://localhost:" + portService + "/ServiceLib";

                m_svcHost = new ServiceHost(typeof(Services), new Uri(strAdrTCP));
                m_svcHost.Faulted += new EventHandler(m_svcHost_Faulted);


                ServiceMetadataBehavior mBehave = new ServiceMetadataBehavior();
                m_svcHost.Description.Behaviors.Add(mBehave);



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

                ServiceEndpoint endPoint = m_svcHost.AddServiceEndpoint(typeof(ServiceLib.IService), tcpb, strClasseService);



                m_svcHost.AddServiceEndpoint(typeof(IMetadataExchange),
                MetadataExchangeBindings.CreateMexTcpBinding(), "mex");


                // Gestion des erreurs
                ServiceDebugBehavior debugBehavior = m_svcHost.Description.Behaviors.Find<ServiceDebugBehavior>();
                if (debugBehavior == null)
                {
                    debugBehavior = new ServiceDebugBehavior();
                    m_svcHost.Description.Behaviors.Add(debugBehavior);
                }
                debugBehavior.IncludeExceptionDetailInFaults = true;

                m_svcHost.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("frmBackGroundService : StartTcpService : {0}", ex.Message), "Erreur");
            }
        }

        public void m_svcHost_Faulted(object sender, EventArgs e)
        {
            MessageBox.Show("Erreur", String.Format("frmBackGroundService: m_svcHost_Faulted : StartTcpService :  {0}", e.ToString()));

            this.Close();
        }

        private void btnStartService_Click(object sender, EventArgs e)
        {            
            DemarrerService();
        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            ArreterService();
        }

        private void mnuChangeProfile_Click(object sender, EventArgs e)
        {
            ChangeProfile();
        }

        private void ChangeProfile()
        {
            try
            {
                FrmProfile fProfile = new FrmProfile();
                fProfile.frmService = this;
                fProfile.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Profile Error");
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {

        }
    }
}
