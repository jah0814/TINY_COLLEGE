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
    public partial class addcourse: Form
    {
        public addcourse()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            course form = new course();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate if the fields contain only the required characters
            if (!IsDigitsOnly(textBox1.Text) || !IsDigitsOnly(textBox2.Text))
            {
                MessageBox.Show("CRS_CODE and DEPT_CODE must contain only numbers.");
                return;
            }

            if (!IsLettersOnly(textBox3.Text))
            {
                MessageBox.Show("CRS_TITLE must contain only letters.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("CRS_DESCRIPTION cannot be empty.");
                return;
            }

            if (!int.TryParse(textBox5.Text, out int result))
            {
                MessageBox.Show("CRS_CREDIT must be a valid integer.");
                return;
            }

            // Define the connection string
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "INSERT INTO COURSE (CRS_CODE, DEPT_CODE, CRS_TITLE, CRS_DESCRIPTION, CRS_CREDIT) " +
                           "VALUES (@CRS_CODE, @DEPT_CODE, @CRS_TITLE, @CRS_DESCRIPTION, @CRS_CREDIT)";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CRS_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@DEPT_CODE", textBox2.Text);
                    cmd.Parameters.AddWithValue("@CRS_TITLE", textBox3.Text);
                    cmd.Parameters.AddWithValue("@CRS_DESCRIPTION", textBox4.Text);
                    cmd.Parameters.AddWithValue("@CRS_CREDIT", textBox5.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Course added successfully!");
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
            textBox4.Clear();
            textBox5.Clear();
            this.Hide();
        }
    }
}
