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
    
    public partial class ProvinceForm : Form
    {
        public bool Adding { get; set; } = true;
        public ProvinceForm()
        {
            InitializeComponent();
        }
        private void GetRecords()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\provinces.json";
            var provinces = new List<Province>();
            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                provinces = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Province>>(json);
            }

            txtId.Text = (provinces.Count + 1).ToString();

            dgvRecords.DataSource = provinces;

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
            var provinces = new List<Province>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\provinces.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                provinces = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Province>>(json);
            }

            var province = new Province();
            if (Adding == true)
            {
                province = new Province
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
                province = provinces.FirstOrDefault(x => x.Id == Id);
                if (province != null)
                {
                    provinces.Remove(province);
                    province.Name = txtName.Text;
                    province.Description = txtDescription.Text;


                }
            }

            provinces.Add(province);

            json = Newtonsoft.Json.JsonConvert.SerializeObject(provinces);

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
