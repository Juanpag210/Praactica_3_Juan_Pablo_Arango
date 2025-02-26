using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRACTICA_AEAE_3_Juan_Pablo_AG
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             Form2 clientes = new Form2();
             clientes.Show();
             this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 seguridad = new Form3();
            seguridad.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 productos = new Form4();
            productos.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
