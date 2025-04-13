using MySql.Data.MySqlClient;
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
    public partial class addskul: Form
    {
        public addskul()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate if the fields contain only the required characters
            if (!IsDigitsOnly(textBox1.Text))
            {
                MessageBox.Show("SCHOOL_CODE must contain only numbers.");
                return;
            }

            if (!IsLettersOnly(textBox2.Text))
            {
                MessageBox.Show("SCHOOL_NAME must contain only letters.");
                return;
            }

            if (!IsDigitsOnly(textBox3.Text))
            {
                MessageBox.Show("PROF_NUM must contain only numbers.");
                return;
            }

            // Define the connection string
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "INSERT INTO SCHOOL (SCHOOL_CODE, SCHOOL_NAME, PROF_NUM) " +
                           "VALUES (@SCHOOL_CODE, @SCHOOL_NAME, NULL)";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SCHOOL_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@SCHOOL_NAME", textBox2.Text);
                    cmd.Parameters.AddWithValue("@PROF_NUM", textBox3.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("School added successfully!");
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        // Helper functions inside the form
        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }

        private bool IsLettersOnly(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsLetter(c)) return false;
            }
            return true;
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            skul form = new skul();
            form.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void addskul_Load(object sender, EventArgs e)
        {

        }
    }
}
