using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class Form6 : Form
    {
        string table;
        private string connectionString1 = "datasource=localhost;port=3306;username=root;password=root;database=fav";

        public Form6()
        {
            InitializeComponent();
            LoadFavourites();
        }

        // Load favourite places from the database into the ListBox
        private void LoadFavourites()
        {
            listBox1.Items.Clear();

            using (MySqlConnection connection = new MySqlConnection(connectionString1))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT name FROM areas";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listBox1.Items.Add(reader["name"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading favourites: {ex.Message}");
                }
            }
        }

        // Event handler for delete button click
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string selectedItem = listBox1.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedItem))
            {
                DialogResult result = MessageBox.Show($"Do you want to delete '{selectedItem}' from your favourites?", "Delete Favourite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteFromFavourites(selectedItem);
                    listBox1.Items.Remove(selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.");
            }
        }

        // Delete selected favourite place from the database
        private void DeleteFromFavourites(string itemName)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString1))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM areas WHERE name = @itemName";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@itemName", itemName);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting from favourites: {ex.Message}");
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Optionally, handle label click events here
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Update_list.Visible = true;  
        }

        private void Update_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string itemname = Update_list.SelectedItem.ToString();
            string tablename= Update_list.SelectedItem.ToString().ToLower();
            Form3 form3 = new Form3();
            OpenForm4WithItemName(itemname,tablename);
        }
        private void OpenForm4WithItemName(string itemName, string tablename)
        {
            Form4 form4 = new Form4(tablename);
            form4.SetItemName(itemName);
            form4.Show();
            this.Hide();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void delete_all()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString1))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM areas";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} items deleted from favourites.");
                    listBox1.Items.Clear(); // Clear the ListBox after deletion
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting from favourites: {ex.Message}");
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete all items from your favourites?", "Delete All Favourites", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                delete_all();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3=new Form3();
            form3.Show();
            this.Hide();
        }
    }
}