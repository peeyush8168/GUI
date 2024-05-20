using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment1
{
      
    public partial class Form5 : Form
    {
        private string table;
        private string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=places";
        private string favConnectionString = "datasource=localhost;port=3306;username=root;password=root;database=fav";

        public Form5(string data)
        {
            InitializeComponent();
            this.table = data;
        }

        private void LoadData()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                string query = $"SELECT name FROM {table}";

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data from {table}: " + ex.Message);
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string itemName = label1.Text;
            if (!string.IsNullOrEmpty(itemName))
            {
                bool isFavourite = CheckIfFavourite(itemName);
                checkBox1.Checked = isFavourite;
                if (isFavourite)
                {
                    label4.Text = "Added to favourite";
                }
            }
        }

        public void SetItemName(string itemName)
        {
            label1.Text = itemName;
            var (place, image) = GetPlaceForItem(itemName);
            if (!string.IsNullOrEmpty(place))
            {
                label3.Text = place;
                checkBox1.Checked = CheckIfFavourite(itemName);

                if (image != null)
                {
                    using (var ms = new System.IO.MemoryStream(image))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            else
            {
                MessageBox.Show($"Item '{itemName}' not found in table '{table}'.");
            }
        }

        private (string place, byte[] image) GetPlaceForItem(string itemName)
        {
            string place = null;
            byte[] image = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT place, image FROM `{table}` WHERE `name` = @itemName";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@itemName", itemName);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            place = reader["place"].ToString();
                            image = reader["image"] as byte[];
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving place and image for item '{itemName}': " + ex.Message);
                }
            }

            return (place, image);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string itemName = label1.Text;
            string place = label3.Text;

            if (checkBox1.Checked)
            {
                label4.Text = "Added to favourites ✓";
                if (!string.IsNullOrEmpty(itemName) && !CheckIfFavourite(itemName))
                {
                    AddToFavourite(itemName);
                }
            }
            else
            {
                label4.Text = "";
            }
        }

        private bool CheckIfFavourite(string itemName)
        {
            using (MySqlConnection connection = new MySqlConnection(favConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM areas WHERE name = @itemName";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@itemName", itemName);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking favourite status for item '{itemName}': " + ex.Message);
                    return false;
                }
            }
        }

        private void AddToFavourite(string itemName)
        {
            using (MySqlConnection connection = new MySqlConnection(favConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO areas (name) VALUES (@itemName)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@itemName", itemName);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding item '{itemName}' to favourites: " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(table); // Create an instance of Form4
            form4.fun(table.ToUpper());
            form4.Show(); // Call Show on the instance
            this.Hide(); // Hide the current form
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
