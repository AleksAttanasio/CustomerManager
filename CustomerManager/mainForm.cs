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
        public int mousePosition = 0;

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
            string query = "INSERT INTO Customers VALUES (@name, @surname, @email, @phone, @company, @city, @address, @cap, @refree, @website)";
            string missingData = "I seguenti campi non sono stati riempiti:\n";
            bool missingBool = false;

            // Check if all the fields are empty
            if (nameTxt.TextLength == 0 && surnameTxt.TextLength == 0 && emailTxt.TextLength == 0 && phoneTxt.TextLength == 0 &&
                companyTxt.TextLength == 0 && cityTxt.TextLength == 0 && addressTxt.TextLength == 0 && refreeTxt.TextLength == 0 &&
                websiteTxt.TextLength == 0)
            {
                MessageBox.Show("Nessun dato inserito.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check of blank fields
            if (nameTxt.TextLength == 0)
            {
                missingData = missingData + "\t - Nome\n";
                missingBool = true;
            }
            if (surnameTxt.TextLength == 0)
            {
                missingData = missingData + "\t - Cognome\n";
                missingBool = true;
            }
            if (emailTxt.TextLength == 0)
            {
                missingData = missingData + "\t - email\n";
                missingBool = true;
            }
            if (phoneTxt.TextLength == 0)
            {
                missingData = missingData + "\t - Telefono\n";
                missingBool = true;
            }
            if (companyTxt.TextLength == 0)
            {
                missingData = missingData + "\t - Azienda\n";
                missingBool = true;
            }
            if (cityTxt.TextLength == 0)
            {
                missingData = missingData + "\t - Città\n";
                missingBool = true;
            }
            if (addressTxt.TextLength == 0)
            {
                missingData = missingData + "\t - Indirizzo\n";
                missingBool = true;
            }
            if (capTxt.TextLength == 0)
            {
                missingData = missingData + "\t - CAP\n";
                missingBool = true;
            }
            if (refreeTxt.TextLength == 0)
            {
                missingData = missingData + "\t - Referente\n";
                missingBool = true;
            }
            if (websiteTxt.TextLength == 0)
            {
                missingData = missingData + "\t - Website\n";
                missingBool = true;
            }

            // If a field is missing show the message
            if (missingBool)
            {
                if (MessageBox.Show(missingData + "\nContinuare?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
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
                        command.Parameters.AddWithValue("@cap", capTxt.Text);
                        command.Parameters.AddWithValue("@refree", refreeTxt.Text);
                        command.Parameters.AddWithValue("@website", websiteTxt.Text);

                        command.ExecuteNonQuery();
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
                else
                {
                    // user clicked no
                }
            }
            else // if the fields are filled up just add the new customer to the list
            {
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
                    command.Parameters.AddWithValue("@cap", capTxt.Text);
                    command.Parameters.AddWithValue("@refree", refreeTxt.Text);
                    command.Parameters.AddWithValue("@website", websiteTxt.Text);

                    command.ExecuteNonQuery();
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

            UpdateMainForm();
        }

        private void customersGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip rcMenu = new System.Windows.Forms.ContextMenuStrip();
                mousePosition = customersGrid.HitTest(e.X, e.Y).RowIndex;

                if (mousePosition >= 0)
                {
                    rcMenu.Items.Add("Cancella").Name = "Cancella";
                    rcMenu.Items.Add("Modifica").Name = "Modifica";
                    rcMenu.Items.Add("Dettagli").Name = "Dettagli";
                }

                rcMenu.Show(customersGrid, new Point(e.X, e.Y));

                rcMenu.ItemClicked += new ToolStripItemClickedEventHandler(rcMenu_Clicked);

            }
        }

        private void rcMenu_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name.ToString())
            {
                case "Cancella":
                    MessageBox.Show("Cancella cliccato");
                    break;
                case "Modifica":
                    modifyForm modForm = new modifyForm();
                    modForm.Show();
                    break;
                case "Dettagli":
                    MessageBox.Show("Dettagli Cliccato");
                    break;
            }
        }

    }
}
