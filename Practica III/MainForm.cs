using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica_III
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
            lblMsg.Text = $"Authenticated as {System.Security.Principal.WindowsIdentity.GetCurrent().Name}";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var userForm = new UserForm();
            userForm.MdiParent = this;
            userForm.Show();
        }

        private void userTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var oForm = new UserTypeForm();
            oForm.MdiParent = this;
            oForm.Show();
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void newPatientsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var oForm = new PatientForm();
            oForm.MdiParent = this;
            oForm.Show();
        }

        private void maritalStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var oForm = new MaritalStatuForm();
            oForm.MdiParent = this;
            oForm.Show();
        }

        private void professionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var oForm = new ProfessionForm();
            oForm.MdiParent = this;
            oForm.Show();
        }

        private void provincesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var oForm = new ProvinceForm();
            oForm.MdiParent = this;
            oForm.Show();
        }

        private void municipalitysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var oForm = new MunicipalityForm();
            oForm.MdiParent = this;
            oForm.Show();
        }
    }
}
