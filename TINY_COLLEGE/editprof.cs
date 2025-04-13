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
    public partial class editprof: Form
    {
        private string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
        private string originalProfNum;
        public editprof(string profNum, string deptCode, string specialty, string rank, string lname, string fname, string initial, string email)
        {
            InitializeComponent();
            originalProfNum = profNum;

            textBox1.Text = profNum;
            textBox2.Text = deptCode;
            textBox3.Text = specialty;
            textBox4.Text = rank;
            textBox5.Text = lname;
            textBox6.Text = fname;
            textBox7.Text = initial;
            textBox8.Text = email;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsDigitsOnly(textBox1.Text) || !IsDigitsOnly(textBox2.Text))
            {
                MessageBox.Show("PROF_NUM and DEPT_CODE must contain only numbers.");
                return;
            }

            if (!IsLettersOnly(textBox5.Text) || !IsLettersOnly(textBox6.Text) || !IsLettersOnly(textBox7.Text))
            {
                MessageBox.Show("PROF_LNAME, PROF_FNAME, and PROF_INITIAL must contain only letters.");
                return;
            }

            string query = "UPDATE PROFESSOR SET " +
                           "PROF_NUM = @PROF_NUM, " +
                           "DEPT_CODE = @DEPT_CODE, " +
                           "PROF_SPECIALTY = @PROF_SPECIALTY, " +
                           "PROF_RANK = @PROF_RANK, " +
                           "PROF_LNAME = @PROF_LNAME, " +
                           "PROF_FNAME = @PROF_FNAME, " +
                           "PROF_INITIAL = @PROF_INITIAL, " +
                           "PROF_EMAIL = @PROF_EMAIL " +
                           "WHERE PROF_NUM = @originalProfNum";

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
                    cmd.Parameters.AddWithValue("@originalProfNum", originalProfNum);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Professor updated successfully!");
                        this.Close(); // Close the form after editing
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
            prof form = new prof();
            form.Show();
            this.Hide();
        }

        private void editprof_Load(object sender, EventArgs e)
        {

        }
    }
}
