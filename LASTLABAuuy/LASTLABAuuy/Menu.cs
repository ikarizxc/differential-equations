using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LASTLABAuuy
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Methods methods = new Methods(Convert.ToDouble(x0.Text), Convert.ToDouble(y0.Text), Convert.ToDouble(xn.Text), Convert.ToDouble(h.Text));
            methods.Show();
        }
    }
}
