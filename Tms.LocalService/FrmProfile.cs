
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tms.Components.Data;
using Tms.LocalService;

namespace ServiceHote
{
    public partial class FrmProfile : Form
    {
        Tms.Components.Data.DataConfig cf;
        public frmLocalService frmService;

        public FrmProfile()
        {
            InitializeComponent();
            ConfigureEvents();
            GetCurrentProfile();
        }



        private void ConfigureEvents()
        {
            try
            {
                btnOk.Click += new EventHandler(btnOk_Click);
                btnCancel.Click += new EventHandler(btnCancel_Click);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configure Events");
            }

        }


        private void GetCurrentProfile()
        {
            try
            {
                cf = new Tms.Components.Data.DataConfig();
                txtServer.Text = cf.Server;
                txtDatabase.Text = cf.DataBase;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Profile Error");
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.TargetSite.DeclaringType.Name + " : " + ex.TargetSite.Name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                frmService.ArreterService();
                cf.Server = txtServer.Text;
                cf.DataBase = txtDatabase.Text;
                //Add By FK for test
                cf.UserName = "tmsAdmin";
                cf.Password = "tmsAdmin@17";
                cf.Save();
                frmService.DemarrerService();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.TargetSite.DeclaringType.Name + " : " + ex.TargetSite.Name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
