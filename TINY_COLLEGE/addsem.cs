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
    public partial class addsem: Form
    {
        public addsem()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sem form = new sem();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate if the fields contain only the required characters
            if (!IsDigitsOnly(textBox1.Text))
            {
                MessageBox.Show("SEMESTER_CODE must contain only numbers.");
                return;
            }

            if (!int.TryParse(textBox2.Text, out int year))
            {
                MessageBox.Show("SEMESTER_YEAR must be a valid year (integer).");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("SEMESTER_TERM cannot be empty.");
                return;
            }

            if (!DateTime.TryParse(textBox4.Text, out DateTime startDate))
            {
                MessageBox.Show("SEMESTER_START_DATE must be a valid date.");
                return;
            }

            if (!DateTime.TryParse(textBox5.Text, out DateTime endDate))
            {
                MessageBox.Show("SEMESTER_END_DATE must be a valid date.");
                return;
            }

            if (startDate > endDate)
            {
                MessageBox.Show("SEMESTER_START_DATE cannot be after SEMESTER_END_DATE.");
                return;
            }

            // Define the connection string
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "INSERT INTO SEMESTER (SEMESTER_CODE, SEMESTER_YEAR, SEMESTER_TERM, SEMESTER_START_DATE, SEMESTER_END_DATE) " +
                           "VALUES (@SEMESTER_CODE, @SEMESTER_YEAR, @SEMESTER_TERM, @SEMESTER_START_DATE, @SEMESTER_END_DATE)";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SEMESTER_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@SEMESTER_YEAR", textBox2.Text);
                    cmd.Parameters.AddWithValue("@SEMESTER_TERM", textBox3.Text);
                    cmd.Parameters.AddWithValue("@SEMESTER_START_DATE", startDate);
                    cmd.Parameters.AddWithValue("@SEMESTER_END_DATE", endDate);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Semester added successfully!");
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
