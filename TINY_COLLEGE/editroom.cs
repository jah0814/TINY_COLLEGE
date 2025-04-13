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
    public partial class editroom: Form
    {
        private string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
        private string originalRoomCode;
        public editroom(string roomCode, string roomType, string bldgCode)
        {
            InitializeComponent();
            originalRoomCode = roomCode;

            textBox1.Text = roomCode;
            textBox2.Text = roomType;
            textBox3.Text = bldgCode;
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

            string query = "UPDATE ROOM SET ROOM_CODE = @ROOM_CODE, ROOM_TYPE = @ROOM_TYPE, BLDG_CODE = @BLDG_CODE " +
                           "WHERE ROOM_CODE = @originalRoomCode";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ROOM_CODE", textBox1.Text);
                    cmd.Parameters.AddWithValue("@ROOM_TYPE", textBox2.Text);
                    cmd.Parameters.AddWithValue("@BLDG_CODE", textBox3.Text);
                    cmd.Parameters.AddWithValue("@originalRoomCode", originalRoomCode);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Room updated successfully!");
                        this.Close(); // or this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            room form = new room();
            form.Show();
            this.Hide();
        }
    }
}
   
