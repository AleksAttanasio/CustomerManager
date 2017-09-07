using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CustomerManager
{
    public partial class mainForm : Form
    {
        SqlConnection connection;
        string connectionString;

        public mainForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["CustomerManager.Properties.Settings.CustomerDatabaseConnectionString"].ConnectionString;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            UpdateMainForm();
        }

        public void UpdateMainForm()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Customers", connection))
            {
                DataTable peopleTable = new DataTable();
                adapter.Fill(peopleTable);

                customersGrid.DataSource = peopleTable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Customers VALUES (@name, @surname, @email, @phone, @company, @city, @address, @refree, @website)";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@name", nameTxt.Text);
                command.Parameters.AddWithValue("@surname", surnameTxt.Text);
                command.Parameters.AddWithValue("@email", emailTxt.Text);
                command.Parameters.AddWithValue("@phone", phoneTxt.Text);
                command.Parameters.AddWithValue("@company", companyTxt.Text);
                command.Parameters.AddWithValue("@city", cityTxt.Text);
                command.Parameters.AddWithValue("@address", addressTxt.Text);
                command.Parameters.AddWithValue("@refree", refreeTxt.Text);
                command.Parameters.AddWithValue("@website", websiteTxt.Text);

                command.ExecuteNonQuery();
            }

            UpdateMainForm();

        }
    }
}
