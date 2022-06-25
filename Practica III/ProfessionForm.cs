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
    public partial class ProfessionForm : Form
    {
        public bool Adding { get; set; } = true;
        public ProfessionForm()
        {
            InitializeComponent();
            GetRecords();
        }

        private void GetRecords()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\professions.json";
            var professions = new List<Profession>();
            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                professions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Profession>>(json);
            }

            txtId.Text = (professions.Count + 1).ToString();

            dgvRecords.DataSource = professions;

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
            var professions = new List<Profession>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\professions.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                professions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Profession>>(json);
            }

            var profession = new Profession();
            if (Adding == true)
            {
                profession = new Profession
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
                profession = professions.FirstOrDefault(x => x.Id == Id);
                if (profession != null)
                {
                    professions.Remove(profession);
                    profession.Name = txtName.Text;
                    profession.Description = txtDescription.Text;


                }
            }

            professions.Add(profession);

            json = Newtonsoft.Json.JsonConvert.SerializeObject(professions);

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
