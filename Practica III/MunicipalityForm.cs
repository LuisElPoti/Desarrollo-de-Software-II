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
    public partial class MunicipalityForm : Form
    {
        public bool Adding { get; set; } = true;
        public MunicipalityForm()
        {
            InitializeComponent();
            GetRecords();
        }
        private void GetRecords()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\municipalities.json";
            var municipalities = new List<Municipality>();
            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                municipalities = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Municipality>>(json);
            }

            txtId.Text = (municipalities.Count + 1).ToString();

            dgvRecords.DataSource = municipalities;

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
            var municipalities = new List<Municipality>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\municipalities.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                municipalities = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Municipality>>(json);
            }

            var municipality = new Municipality();
            if (Adding == true)
            {
                municipality = new Municipality
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
                municipality = municipalities.FirstOrDefault(x => x.Id == Id);
                if (municipality != null)
                {
                    municipalities.Remove(municipality);
                    municipality.Name = txtName.Text;
                    municipality.Description = txtDescription.Text;


                }
            }

            municipalities.Add(municipality);

            json = Newtonsoft.Json.JsonConvert.SerializeObject(municipalities);

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
