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
    public partial class addenroll: Form
    {
        public addenroll()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            enroll form = new enroll();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate if the fields contain only the required characters
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("CLASS_CODE and STU_NUM cannot be empty.");
                return;
            }

            if (!DateTime.TryParse(textBox3.Text, out DateTime enrollDate))
            {
                MessageBox.Show("ENROLL_DATE must be a valid date.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("ENROLL_GRADE cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("ENROLL_GRADE cannot be empty.");
                return;
            }

            if (textBox4.Text.Length > 5)
            {
                MessageBox.Show("ENROLL_GRADE must be 5 characters or fewer.");
                return;
            }

            // Define the connection string
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "INSERT INTO ENROLL (CLASS_CODE, STU_NUM, ENROLL_DATE, ENROLL_GRADE) " +
                           "VALUES (@CLASS_CODE, @STU_NUM, @ENROLL_DATE, @ENROLL_GRADE)";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CLASS_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@STU_NUM", textBox2.Text);
                    cmd.Parameters.AddWithValue("@ENROLL_DATE", enrollDate);
                    cmd.Parameters.AddWithValue("@ENROLL_GRADE", textBox4.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Enrollment added successfully!");
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        // Helper function inside the form
        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            this.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
