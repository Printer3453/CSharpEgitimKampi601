using CSharpEgitimKampi601.Entities;
using CSharpEgitimKampi601.Services;
using MongoDB.Bson;
using MongoDB.Driver;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CustomerOperations customerOperations = new CustomerOperations();
        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            var customer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = decimal.Parse(txtCustomerBalance.Text),
                CustomerShoppingCount = int.Parse(txtCustomerShoppingCount.Text)

            };
            customerOperations.AddCustomer(customer);
            MessageBox.Show("Müşteri Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        private void btnList_Click(object sender, EventArgs e)
        {
           List<Customer> customers= customerOperations.GetAllCustomer();
            dataGridView1.DataSource = customers;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text;
            var updateCustomer= new Customer()
            {
                CustomerId= id,
                CustomerName = txtCustomerName.Text,
                CustomerSurname= txtCustomerSurname.Text,
                CustomerCity= txtCustomerCity.Text,
                CustomerBalance= decimal.Parse(txtCustomerBalance.Text),
                CustomerShoppingCount= int.Parse(txtCustomerShoppingCount.Text)
            };

            customerOperations.UpdateCustomer(updateCustomer);
            MessageBox.Show("Müşteri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text;
            customerOperations.DeleteCustomer(id);
            MessageBox.Show("Müşteri Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnCustomerGetById_Click(object sender, EventArgs e)
        {
            string id= txtCustomerId.Text;
            var customer = customerOperations.GetCustomerById(id);
            List<Customer> customerList = new List<Customer>();
            //dataGridView1.DataSource = new List<Customer>{customer};
            if (customer!=null)
            {
                customerList.Add(customer);
                dataGridView1.DataSource = customerList;
            }
            else
            {
                MessageBox.Show("Müşteri Bulunamadı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }

}
