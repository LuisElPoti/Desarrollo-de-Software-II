using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Salario_neto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            double sueldoBruto = int.Parse(txtSueldoBruto.Text);
            double SFS = Math.Round((sueldoBruto * 0.0304), 2);
            double AFP = Math.Round((sueldoBruto * 0.0287), 2);
            double ISR = 0;
            double sueldoNeto = 0;
            double resul = (sueldoBruto - SFS - AFP) * 12;
            double excedente = 0;

            if (resul < 416221.01)
            {

                ISR = 0;

            }
            else if (resul > 416221 && resul < 624329.01)
            {
                excedente = resul - 416220;
                ISR = Math.Round(((excedente * 0.15) / 12), 2);
            }

            else if (resul > 624329 && resul < 867123.01)
            {

                excedente = resul - 624329;
                ISR = Math.Round((((excedente * 0.20) + 31216) / 12), 2);
            }
            else
            {
                excedente = resul - 867123;
                ISR = Math.Round(((79776 + (excedente * 0.25)) / 12), 2);
            }

            sueldoNeto = Math.Round((sueldoBruto - SFS - AFP - ISR), 2);

            txtSFS.Text = SFS.ToString();
            txtAFP.Text = AFP.ToString();
            txtISR.Text = ISR.ToString();
            txtSueldoNeto.Text = sueldoNeto.ToString();



        }

    }
}
