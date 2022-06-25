using Practica_III.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica_III
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
            GetRecords();
            GetUserTypes();
        }
        private void GetRecords()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\users.json";
            var usertypes = new List<User>();
            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json);
            }

            txtId.Text = (usertypes.Count + 1).ToString();

            dgvRecords.DataSource = usertypes;
        }

        private void GetUserTypes()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\usertypes.json";
            var usertypes = new List<UserType>();
            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserType>>(json);
            }

            cmbUserType.DataSource = usertypes.Where(x=> x.Enabled).ToList();
            cmbUserType.DisplayMember = "Name";
            cmbUserType.ValueMember = "Id";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            gbPanel.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;   
            btnNew.Enabled = false;

            txtId.Text = Guid.NewGuid().ToString();
            txtCreatedDate.Text = DateTime.Now.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            gbPanel.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnNew.Enabled = true;

            ClearFields();
        }

        private void ClearFields()
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
            chkEnabled.Enabled = false;
            cmbUserType.SelectedIndex = 0;
            txtCreatedDate.Text = String.Empty;
        }
    }
}
