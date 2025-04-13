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
    public partial class editskul: Form
    {
        private string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
        private string originalSchoolCode;

        public editskul(string schoolCode, string schoolName, string profNum)
        {
            InitializeComponent();
            originalSchoolCode = schoolCode;

            textBox1.Text = schoolCode;
            textBox2.Text = schoolName;
            textBox3.Text = profNum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

            string query = "UPDATE SCHOOL SET SCHOOL_CODE = @SCHOOL_CODE, SCHOOL_NAME = @SCHOOL_NAME, PROF_NUM = @PROF_NUM " +
                           "WHERE SCHOOL_CODE = @originalSchoolCode";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SCHOOL_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@SCHOOL_NAME", textBox2.Text);
                    cmd.Parameters.AddWithValue("@PROF_NUM", textBox3.Text);
                    cmd.Parameters.AddWithValue("@originalSchoolCode", originalSchoolCode);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("School updated successfully!");
                        this.Close(); // Close the edit form after saving
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
            skul form = new skul();
            form.Show();
            this.Hide();
        }
    }
}
  
