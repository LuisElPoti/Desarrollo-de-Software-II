using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CitasHospital
{
    public partial class Form1 : Form
    {
        List<Appoiment> Appoiments = new List<Appoiment>();
        List<string> Horas = new List<string>();
        public Form1()
        {
            InitializeComponent();
            fillHours();
        }

        private void fillHours()
        {   
            Horas = Enumerable.Range(08, 15).Select(i => (DateTime.MinValue.AddHours(i)).ToString("hh.mm tt")).ToList();
            
            cbHora.DataSource = Horas;
        }


        private void btnAñadir_Click(object sender, EventArgs e)
        {
            SaveAppoiment();
            EmptyControls();
            DefaultValues();


        }

        private void SaveAppoiment()
        {
            var rnd = new Random();

            var appoiment = new Appoiment
            {
                Id = Guid.NewGuid(),
                Code = rnd.Next(100000, 999999).ToString(),
                Nombre = txtNombre.Text,
                Telefono = txtNumero.Text,
                Doctor = txtDoctor.Text,
                Centro = txtCentro.Text,
                ScheduleDate = dtpDate.Value,
                ScheduleHour = cbHora.SelectedValue.ToString(),
                CreateDate = DateTime.Now



            };

            var existAppoiment = Appoiments.Count(x => x.ScheduleDate.ToString("dd/mm/yyyy") == dtpDate.Value.ToString("dd/mm/yyyy") && x.ScheduleHour == cbHora.SelectedValue.ToString());
            if (existAppoiment > 0)
            {
                MessageBox.Show("Horario no disponible");
                return;
            }
            Appoiments.Add(appoiment);

            var selectedHora = Horas.FirstOrDefault(x => x == cbHora.SelectedValue.ToString());
            Horas.Remove(selectedHora);
            cbHora.DataSource = null;
            cbHora.DataSource = Horas;  

            GetAppoiments();
            EmptyControls();
        }

        
        

            private void GetAppoiments()
        {
            dgDatos.DataSource = null;
            dgDatos.DataSource = Appoiments;

            dgDatos.Columns["Id"].Visible = false;
        }

        private void DefaultValues()
        {
            dtpDate.Value = DateTime.Now;
           
            
        }

        private void EmptyControls()
        {
            txtCentro.Text = String.Empty;
            txtDoctor.Text = String.Empty;  
            txtNombre.Text = String.Empty;  
            txtNumero.Text = String.Empty;
        }

        
    }

    public class Appoiment
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
        public string Nombre { get; set;}
        public string Telefono { get; set;}
        public string Centro { get; set;}
        public string Doctor { get; set;}
        public string ScheduleHour { get; set;}
        
        public DateTime ScheduleDate { get; set;}
        public DateTime CreateDate { get; set;}




    }

    
}
