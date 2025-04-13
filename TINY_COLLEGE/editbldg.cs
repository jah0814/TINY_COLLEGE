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
    public partial class editbldg : Form
    {
        private string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
        private string originalBldgCode;
        public editbldg(string bldgCode, string bldgName, string bldgLocation)
        {
            InitializeComponent();
            originalBldgCode = bldgCode;

            textBox1.Text = bldgCode;
            textBox2.Text = bldgName;
            textBox3.Text = bldgLocation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("BLDG_CODE and BLDG_NAME cannot be empty.");
                return;
            }

            if (!int.TryParse(textBox1.Text, out _))
            {
                MessageBox.Show("BLDG_CODE must contain numbers only.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("BLDG_LOCATION cannot be empty.");
                return;
            }

            string query = "UPDATE BUILDING SET BLDG_CODE = @BLDG_CODE, BLDG_NAME = @BLDG_NAME, BLDG_LOCATION = @BLDG_LOCATION " +
                           "WHERE BLDG_CODE = @originalBldgCode";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BLDG_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@BLDG_NAME", textBox2.Text);
                    cmd.Parameters.AddWithValue("@BLDG_LOCATION", textBox3.Text);
                    cmd.Parameters.AddWithValue("@originalBldgCode", originalBldgCode);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Building updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // or this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bldg form = new bldg();
            form.Show();
            this.Hide();
        }
    }
}
    

