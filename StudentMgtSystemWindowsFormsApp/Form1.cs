using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentMgtSystemWebForm
{
    public partial class Form1 : Form
    {
        int i = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            i++;
            if (textBox1.Text == "" || textBox1.Text != "admin")
            {
                MessageBox.Show("Please enter the correct username.", "Incorrect Username!", MessageBoxButtons.OK);

            }
            else if (textBox2.Text == "" || textBox2.Text != "admin")
            {
                MessageBox.Show("Please enter the correct password.", "Incorrect Password!", MessageBoxButtons.OK);
            }

            else
            {
                Welcome welObj = new Welcome();
                welObj.Show();
                this.Hide();
            }

            if (i > 2)
            {
                MessageBox.Show("You log in attempt failed in 3 tries. Application will close now.", "Login Authentication Failed", MessageBoxButtons.OK);
                this.Close();

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Application will close now.");
            this.Close();
        }
    }
}
