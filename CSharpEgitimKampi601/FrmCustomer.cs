using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }
        //pgsql://localhost:5432/mydatabase?user=myuser&password=mypassword";
        string connectionString = "server=localhost;port=5432;Database=CustomerDb;user Id=postgres;password=1234;";

        void GetAllCustomers()
        {
            var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM customers order by CustomerId";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO Customers (CustomerName, CustomerSurname, CustomerCity) VALUES (@name, @surname, @city)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", customerName);
            command.Parameters.AddWithValue("@surname", customerSurname);
            command.Parameters.AddWithValue("@city", customerCity);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Ekleme İşlemi Başarılı");
            GetAllCustomers();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCustomerId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "DELETE FROM Customers WHERE CustomerId = @id";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Silme İşlemi Başarılı");
            GetAllCustomers();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            int id = int.Parse(txtCustomerId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Update Customers set CustomerName=@name, CustomerSurname=@surname, CustomerCity=@city where CustomerId=@id ";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", customerName);
            command.Parameters.AddWithValue("@surname", customerSurname);
            command.Parameters.AddWithValue("@city", customerCity);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Güncelleme İşlemi Başarılı");
            GetAllCustomers();

        }
    }
}
