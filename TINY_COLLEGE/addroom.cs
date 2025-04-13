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
    public partial class addroom: Form
    {
        public addroom()
        {
            InitializeComponent();
        }

        private void addroom_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            room form = new room();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("ROOM_CODE and ROOM_TYPE cannot be empty.");
                return;
            }

            if (!int.TryParse(textBox1.Text, out _))
            {
                MessageBox.Show("ROOM_CODE must contain only numbers.");
                return;
            }

            if (!int.TryParse(textBox3.Text, out _))
            {
                MessageBox.Show("BLDG_CODE must contain only numbers.");
                return;
            }

            // Define the connection string
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "INSERT INTO ROOM (ROOM_CODE, ROOM_TYPE, BLDG_CODE) " +
                           "VALUES (@ROOM_CODE, @ROOM_TYPE, @BLDG_CODE)";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ROOM_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@ROOM_TYPE", textBox2.Text);
                    cmd.Parameters.AddWithValue("@BLDG_CODE", textBox3.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Room added successfully!");
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

// Clear the form
private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            this.Hide();
        }
    }
}
