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
    public partial class editsem: Form
    {
        private string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
        private string originalSemesterCode;

        public editsem(string semesterCode, string semesterYear, string semesterTerm, string startDate, string endDate)
        {
            InitializeComponent();
            originalSemesterCode = semesterCode;

            textBox1.Text = semesterCode;
            textBox2.Text = semesterYear;
            textBox3.Text = semesterTerm;
            textBox4.Text = startDate;
            textBox5.Text = endDate;

        }

        private void button1_Click(object sender, EventArgs e)
        {
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

            // Update query
            string query = "UPDATE SEMESTER SET " +
                           "SEMESTER_CODE = @SEMESTER_CODE, " +
                           "SEMESTER_YEAR = @SEMESTER_YEAR, " +
                           "SEMESTER_TERM = @SEMESTER_TERM, " +
                           "SEMESTER_START_DATE = @SEMESTER_START_DATE, " +
                           "SEMESTER_END_DATE = @SEMESTER_END_DATE " +
                           "WHERE SEMESTER_CODE = @originalSemesterCode";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SEMESTER_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@SEMESTER_YEAR", year);
                    cmd.Parameters.AddWithValue("@SEMESTER_TERM", textBox3.Text);
                    cmd.Parameters.AddWithValue("@SEMESTER_START_DATE", startDate);
                    cmd.Parameters.AddWithValue("@SEMESTER_END_DATE", endDate);
                    cmd.Parameters.AddWithValue("@originalSemesterCode", originalSemesterCode);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Semester updated successfully!");
                        this.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            sem form = new sem();
            form.Show();
            this.Hide();
        }
    }
}
