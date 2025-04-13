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
    public partial class editdept: Form
    {
        private string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
        private string originalDeptCode;

        public editdept(string deptCode, string deptName, string schoolCode, string profNum)
        {
            InitializeComponent();
            originalDeptCode = deptCode;

            textBox1.Text = deptCode;
            textBox2.Text = deptName;
            textBox3.Text = schoolCode;
            textBox4.Text = profNum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out _) || !int.TryParse(textBox4.Text, out _))
            {
                MessageBox.Show("DEPT_CODE and PROF_NUM must contain only numbers.");
                return;
            }

            if (!IsLettersOnly(textBox2.Text))
            {
                MessageBox.Show("DEPT_NAME must contain only letters.");
                return;
            }

            if (!int.TryParse(textBox3.Text, out _))
            {
                MessageBox.Show("SCHOOL_CODE must contain only numbers.");
                return;
            }

            string query = "UPDATE DEPARTMENT SET " +
                           "DEPT_CODE = @DEPT_CODE, " +
                           "DEPT_NAME = @DEPT_NAME, " +
                           "SCHOOL_CODE = @SCHOOL_CODE, " +
                           "PROF_NUM = @PROF_NUM " +
                           "WHERE DEPT_CODE = @originalDeptCode";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DEPT_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@DEPT_NAME", textBox2.Text);
                    cmd.Parameters.AddWithValue("@SCHOOL_CODE", textBox3.Text);
                    cmd.Parameters.AddWithValue("@PROF_NUM", textBox4.Text);
                    cmd.Parameters.AddWithValue("@originalDeptCode", originalDeptCode);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Department updated successfully!");
                        this.Close(); // Close the form after editing
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
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
            dept form = new dept();
            form.Show();
            this.Hide();
        }
    }
}
