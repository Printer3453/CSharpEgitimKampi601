using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi601.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var document = new BsonDocument
            {
                { "CustomerName", customer.CustomerName },
                { "CustomerSurname", customer.CustomerSurname },
                { "CustomerCity", customer.CustomerCity },
                { "CustomerBalance", customer.CustomerBalance },
                { "CustomerShoppingCount", customer.CustomerShoppingCount }
                };
            customerCollection.InsertOne(document);
            Console.WriteLine("Müşteri Ekleme İşlemi");
        }

        public List<Customer> GetAllCustomer()
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var customers = customerCollection.Find(new BsonDocument()).ToList();
            List<Customer> customerList = new List<Customer>();
            foreach (var c in customers)
            {
                customerList.Add(new Customer
                {
                    CustomerId = c["_id"].ToString(),
                    CustomerName = c["CustomerName"].ToString(),
                    CustomerSurname = c["CustomerSurname"].ToString(),
                    CustomerCity = c["CustomerCity"].ToString(),
                    CustomerBalance = decimal.Parse(c["CustomerBalance"].ToString()),
                    CustomerShoppingCount = int.Parse(c["CustomerShoppingCount"].ToString())
                });
            }
            return customerList;
        }

        public void DeleteCustomer(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var deletedValue = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            customerCollection.DeleteOne(deletedValue);
            Console.WriteLine("Müşteri Silme İşlemi Gerçekleşti");
        }

        public void UpdateCustomer(Customer customer)
        {
            
            var connection= new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var updateFilter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerId));
            var updatedValue = Builders<BsonDocument>.Update
                .Set("CustomerName", customer.CustomerName )
            .Set( "CustomerSurname", customer.CustomerSurname)
            .Set( "CustomerCity", customer.CustomerCity)
            .Set( "CustomerValance", customer.CustomerBalance)
            .Set( "CustomerShoppingCount", customer.CustomerShoppingCount);
            
            customerCollection.UpdateOne(updateFilter, updatedValue);//MongoDb de güncelleme işlemi
            Console.WriteLine("Müşteri Güncelleme İşlemi Gerçekleşti");
            
        }

        public Customer GetCustomerById(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomerCollection();
            var filter= Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var customer = customerCollection.Find(filter).FirstOrDefault();
            return new Customer
            {
                CustomerId = customer["_id"].ToString(),
                CustomerName = customer["CustomerName"].ToString(),
                CustomerSurname = customer["CustomerSurname"].ToString(),
                CustomerCity = customer["CustomerCity"].ToString(),
                CustomerBalance = decimal.Parse(customer["CustomerBalance"].ToString()),
                CustomerShoppingCount = int.Parse(customer["CustomerShoppingCount"].ToString())

            };
            

        }
    }
}
