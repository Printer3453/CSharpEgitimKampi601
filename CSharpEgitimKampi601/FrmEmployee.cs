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
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }
        string connectionString = "server=localhost;port=5432;Database=CustomerDb;user Id=postgres;password=1234;";
       void EmployeeList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Employees";
            var command= new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();

        }

        void DepartmentList()
        {
            
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM departments";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbEmployeeDepartment.DisplayMember = "departmentName";
            cmbEmployeeDepartment.ValueMember = "departmentId";
            cmbEmployeeDepartment.DataSource = dataTable;
            connection.Close();
          
        }


        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeeList();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            EmployeeList();
            DepartmentList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            decimal employeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int departmentId = (int)cmbEmployeeDepartment.SelectedValue;
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO Employees (EmployeeName, EmployeeSurname, EmployeeSalary, DepartmentId)" +
                " VALUES (@name, @surname, @salary, @deptId)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", employeeName);
            command.Parameters.AddWithValue("@surname", employeeSurname);
            command.Parameters.AddWithValue("@salary", employeeSalary);
            command.Parameters.AddWithValue("@deptId", departmentId);
            command.ExecuteNonQuery();
            connection.Close();
            EmployeeList();
        }
    }
}
