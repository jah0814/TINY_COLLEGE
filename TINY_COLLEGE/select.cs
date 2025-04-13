using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINY_COLLEGE
{
    public partial class select: Form
    {
        
       
        public select()
        {
            InitializeComponent();
            
        }
        public void UpdateLabel(string text)
        {
            label1.Text = AppData.UserName; // Display the stored name
        }

        private void button1_Click(object sender, EventArgs e)
        {
            student form = new student();
            form.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            prof form = new prof();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dept form = new dept();
            form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            skul form = new skul();
            form.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            course form = new course();
            form.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            @class form = new @class();
            form.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            sem form = new sem();
            form.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            room form = new room();
            form.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bldg form = new bldg();
            form.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            enroll form = new enroll();
            form.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void select_Load(object sender, EventArgs e)
        {
            label1.Text = AppData.UserName;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
