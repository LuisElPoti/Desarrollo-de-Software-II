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
    public partial class MaritalStatuForm : Form
    {
        public bool Adding { get; set; } = true;
        public MaritalStatuForm()
        {
            InitializeComponent();
            GetRecords();
        }

        private void GetRecords()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\maritalstatus.json";
            var maritalstatus = new List<MaritalStatu>();
            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                maritalstatus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MaritalStatu>>(json);
            }

            txtId.Text = (maritalstatus.Count + 1).ToString();

            dgvRecords.DataSource = maritalstatus;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            gbPanel.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            btnNew.Enabled = false;

            txtCreatedDate.Text = DateTime.Now.ToString();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            gbPanel.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnNew.Enabled = true;

            SaveRecord();

        }

        private void SaveRecord()
        {
            var json = string.Empty;
            var maritalstatus = new List<MaritalStatu>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\maritalstatus.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                maritalstatus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MaritalStatu>>(json);
            }

            var maritalstatu = new MaritalStatu();
            if (Adding == true)
            {
                maritalstatu = new MaritalStatu
                {
                    Id = int.Parse(txtId.Text),
                    Name = txtName.Text,
                    Description = txtDescription.Text,    
                    CreatedDate = DateTime.Now

                };

            }
            else
            {
                var Id = int.Parse(txtId.Text);
                maritalstatu = maritalstatus.FirstOrDefault(x => x.Id == Id);
                if (maritalstatu != null)
                {
                    maritalstatus.Remove(maritalstatu);
                    maritalstatu.Name = txtName.Text;
                    maritalstatu.Description = txtDescription.Text;
                    

                }
            }

            maritalstatus.Add(maritalstatu);

            json = Newtonsoft.Json.JsonConvert.SerializeObject(maritalstatus);

            var sw = new StreamWriter(pathFile, false, Encoding.UTF8);
            sw.WriteLine(json);
            sw.Close();

            MessageBox.Show("Registro exitoso", "Patient Manage", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearFields();
            GetRecords();
        }

        private void ClearFields()
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;           
            txtDescription.Text = string.Empty;
            txtCreatedDate.Text = String.Empty;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        
    }
}
