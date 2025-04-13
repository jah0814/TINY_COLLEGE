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
    public partial class room: Form
    {
        public room()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            select form = new select();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addroom form = new addroom();
            form.Show();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM room";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Room data: " + ex.Message);
                }
            }
        }

        private void room_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row's ROOM_CODE
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                string roomCodeToDelete = dataGridView1.Rows[selectedRowIndex].Cells["ROOM_CODE"].Value.ToString();

                // Ask the user for confirmation before deleting
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this room?", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Delete the record from the database
                    string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
                    string query = "DELETE FROM room WHERE ROOM_CODE = @roomCode";

                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@roomCode", roomCodeToDelete);
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Room deleted successfully.");
                                // Refresh the DataGridView to reflect the change
                                LoadRoomData();
                            }
                            else
                            {
                                MessageBox.Show("Error deleting record.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void LoadRoomData()
        {
            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM room";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Room data: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                string roomCode = dataGridView1.Rows[rowIndex].Cells["ROOM_CODE"].Value.ToString();
                string roomType = dataGridView1.Rows[rowIndex].Cells["ROOM_TYPE"].Value.ToString();
                string bldgCode = dataGridView1.Rows[rowIndex].Cells["BLDG_CODE"].Value.ToString();

                editroom form = new editroom(roomCode, roomType, bldgCode);
                form.ShowDialog();

                LoadRoomData(); // Refresh DataGridView after editing
            }
            else
            {
                MessageBox.Show("Please select a room to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadRoomData(); // Load all rooms if search field is empty
                return;
            }

            string connString = "server=localhost;user=root;database=tinycollege;password=jaaaahz023;";
            string query = "SELECT * FROM room WHERE ROOM_CODE LIKE @keyword OR ROOM_TYPE LIKE @keyword OR BLDG_CODE LIKE @keyword";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Search error: " + ex.Message);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
