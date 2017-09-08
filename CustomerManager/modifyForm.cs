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
    public partial class modifyForm : Form
    {

        SqlConnection connection;
        string connectionString;
        DataTable peopleTable = new DataTable();


        public modifyForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["CustomerManager.Properties.Settings.CustomerDatabaseConnectionString"].ConnectionString;
        }

        private void modifyForm_Load(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Customers", connection))
            {
                adapter.Fill(peopleTable);
            }
            
            nameTxt.Text = String.Empty;
            surnameTxt.Text = String.Empty;
            emailTxt.Text = String.Empty;
            phoneTxt.Text = String.Empty;
            companyTxt.Text = String.Empty;
            cityTxt.Text = String.Empty;
            addressTxt.Text = String.Empty;
            capTxt.Text = String.Empty;
            refreeTxt.Text = String.Empty;
            websiteTxt.Text = String.Empty;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {

        }


    }
}
