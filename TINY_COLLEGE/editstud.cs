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
    public partial class editstud: Form
    {
        private string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
        private string originalStuNum;
        public editstud(string stuNum, string deptCode, string stuLName, string stuFName, string stuInitial, string stuEmail, string profNum)
        {
            InitializeComponent();
            originalStuNum = stuNum;

            textBox1.Text = stuNum;
            textBox2.Text = deptCode;
            textBox3.Text = stuLName;
            textBox4.Text = stuFName;
            textBox5.Text = stuInitial;
            textBox6.Text = stuEmail;
            textBox7.Text = profNum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

            string query = "UPDATE STUDENT SET " +
                           "STU_NUM = @STU_NUM, " +
                           "DEPT_CODE = @DEPT_CODE, " +
                           "STU_LNAME = @STU_LNAME, " +
                           "STU_FNAME = @STU_FNAME, " +
                           "STU_INITIAL = @STU_INITIAL, " +
                           "STU_EMAIL = @STU_EMAIL, " +
                           "PROF_NUM = @PROF_NUM " +
                           "WHERE STU_NUM = @originalStuNum";

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
                    cmd.Parameters.AddWithValue("@originalStuNum", originalStuNum);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Student updated successfully!");
                        this.Close(); // Close the edit form
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
            student form = new student();
            form.Show();
            this.Hide();
        }
    }
}
