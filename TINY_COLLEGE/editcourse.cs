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
    public partial class editcourse: Form
    {
        private string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
        private string originalCrsCode;
        public editcourse(string crsCode, string deptCode, string crsTitle, string crsDescription, string crsCredit)
        {
            InitializeComponent();
            originalCrsCode = crsCode;

            textBox1.Text = crsCode;
            textBox2.Text = deptCode;
            textBox3.Text = crsTitle;
            textBox4.Text = crsDescription;
            textBox5.Text = crsCredit;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

            if (!int.TryParse(textBox5.Text, out int crsCredit))
            {
                MessageBox.Show("CRS_CREDIT must be a valid integer.");
                return;
            }

            string query = "UPDATE COURSE SET " +
                           "CRS_CODE = @CRS_CODE, " +
                           "DEPT_CODE = @DEPT_CODE, " +
                           "CRS_TITLE = @CRS_TITLE, " +
                           "CRS_DESCRIPTION = @CRS_DESCRIPTION, " +
                           "CRS_CREDIT = @CRS_CREDIT " +
                           "WHERE CRS_CODE = @originalCrsCode";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CRS_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@DEPT_CODE", textBox2.Text);
                    cmd.Parameters.AddWithValue("@CRS_TITLE", textBox3.Text);
                    cmd.Parameters.AddWithValue("@CRS_DESCRIPTION", textBox4.Text);
                    cmd.Parameters.AddWithValue("@CRS_CREDIT", crsCredit);
                    cmd.Parameters.AddWithValue("@originalCrsCode", originalCrsCode);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Course updated successfully!");
                        this.Close(); // Close edit form
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

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

        private void button2_Click(object sender, EventArgs e)
        {
            course form = new course();
            form.Show();
            this.Hide();
        }
    }
}
