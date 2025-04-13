using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINY_COLLEGE
{
    public partial class addprof : Form
    {
        public addprof()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate if the fields contain only the required characters
            if (!IsDigitsOnly(textBox1.Text) || !IsDigitsOnly(textBox2.Text))
            {
                MessageBox.Show("PROF_NUM and DEPT_CODE must contain only numbers.");
                return;
            }

            if (!IsLettersOnly(textBox3.Text) || !IsLettersOnly(textBox4.Text) || !IsLettersOnly(textBox5.Text))
            {
                MessageBox.Show("PROF_LNAME, PROF_FNAME, and PROF_INITIAL must contain only letters.");
                return;
            }

            // Define the connection string
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "INSERT INTO PROFESSOR (PROF_NUM, DEPT_CODE, PROF_SPECIALTY, PROF_RANK, PROF_LNAME, PROF_FNAME, PROF_INITIAL, PROF_EMAIL) " +
                           "VALUES (@PROF_NUM, NULL, @PROF_SPECIALTY, @PROF_RANK, @PROF_LNAME, @PROF_FNAME, @PROF_INITIAL, @PROF_EMAIL)";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PROF_NUM", textBox1.Text);
                    cmd.Parameters.AddWithValue("@DEPT_CODE", textBox2.Text);
                    cmd.Parameters.AddWithValue("@PROF_SPECIALTY", textBox3.Text);
                    cmd.Parameters.AddWithValue("@PROF_RANK", textBox4.Text);
                    cmd.Parameters.AddWithValue("@PROF_LNAME", textBox5.Text);
                    cmd.Parameters.AddWithValue("@PROF_FNAME", textBox6.Text);
                    cmd.Parameters.AddWithValue("@PROF_INITIAL", textBox7.Text);
                    cmd.Parameters.AddWithValue("@PROF_EMAIL", textBox8.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Professor added successfully!");
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
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            prof form = new prof();
            form.Show();
            this.Hide();
        }
    }
}
