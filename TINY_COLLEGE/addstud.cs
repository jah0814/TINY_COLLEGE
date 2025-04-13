using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace TINY_COLLEGE
{
    public partial class addstud : Form
    {
        public addstud()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Simple input validation
            if (!IsDigitsOnly(textBox1.Text) || !IsDigitsOnly(textBox2.Text) || !IsDigitsOnly(textBox7.Text))
            {
                MessageBox.Show("STU_NUM, DEPT_CODE, and PROF_NUM must contain only numbers.");
                return;
            }

            if (!IsLettersOnly(textBox3.Text) || !IsLettersOnly(textBox4.Text) || !IsLettersOnly(textBox5.Text))
            {
                MessageBox.Show("STU_LNAME, STU_FNAME, and STU_INITIAL must contain only letters.");
                return;
            }

            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "INSERT INTO student (STU_NUM, DEPT_CODE, STU_LNAME, STU_FNAME, STU_INITIAL, STU_EMAIL, PROF_NUM) " +
                           "VALUES (@STU_NUM, @DEPT_CODE, @STU_LNAME, @STU_FNAME, @STU_INITIAL, @STU_EMAIL, @PROF_NUM)";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@STU_NUM", textBox1.Text);
                    cmd.Parameters.AddWithValue("@DEPT_CODE", textBox2.Text);
                    cmd.Parameters.AddWithValue("@STU_LNAME", textBox3.Text);
                    cmd.Parameters.AddWithValue("@STU_FNAME", textBox4.Text);
                    cmd.Parameters.AddWithValue("@STU_INITIAL", textBox5.Text);
                    cmd.Parameters.AddWithValue("@STU_EMAIL", textBox6.Text);
                    cmd.Parameters.AddWithValue("@PROF_NUM", textBox7.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Student added successfully!");
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
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            student form = new student();
            form.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void addstud_Load(object sender, EventArgs e)
        {

        }
    }
}



