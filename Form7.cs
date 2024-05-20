using System;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            SetInfoText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void SetInfoText()
        {
            labelInfo.Text = "Application Name\n" +
                             "JammuGems\n\n" +
                             "Version\n" +
                             "Version 1.0\n\n" +
                             "Description\n" +
                             "JammuGems is a user-friendly application designed to help you explore, manage, and favorite places in Jammu\n city. " +
                             "Whether you are a resident or a visitor, JammuGems offers an easy way to discover the gems of Jammu,\n from popular attractions to hidden spots.\n\n" +
                             "Features\n" +
                             "●Explore Places: Browse through a variety of places categorized for easy navigation.\n" +
                             "●Favorite Management: Add places to your favorites for quick access and future visits.\n" +
                             "●Database Integration: Powered by MySQL, ensuring reliable and efficient data management.\n\n" +
                             "How to Use\n" +
                             "●Browsing Places: Use the main menu to navigate through different categories of places.\n" +
                             "●Adding to Favorites: Select a place and mark it as a favorite to add it to your personal list.\n" +
                             "●Viewing Favorites: Access your list of favorite places from the 'Favorite Places' section.\n" +
                             "●Deleting Favorites: Remove places from your favorites list easily if you change your preferences.";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
