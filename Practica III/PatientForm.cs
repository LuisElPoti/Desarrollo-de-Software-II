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
    public partial class PatientForm : Form
    {
        public PatientForm()
        {
            InitializeComponent();
            GetMedicalHistory();

        }

        private void GetMedicalHistory()
        {
            var list = new List<string>
            {
                "Es buena su salud en general",
                "Visita periodicamente al dentista",
                "Siente devilidad en sus encias",
                "Siente algunos dientes flojos",
                "Problemas con la anestesia",
                "Diabetico",
                "Presión alta"
            };
            for (int i = 0; i < list.Count; i++)
            {
                MedicalHistory.Items.Add(list[i]);
            }
            

        }
    }
}
