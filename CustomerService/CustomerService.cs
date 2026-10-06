using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Data;

namespace CustomerService
{
    public class CustomerService : ICustomerService
    {

        string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public bool AddCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                using(SqlCommand cmd = new SqlCommand("sp_CreateCustomer", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Phone",customer.Phone);
                    connection.Open();
                    int rowEffected =  cmd.ExecuteNonQuery();
                    return rowEffected > 0;
                }
            }
        }

        public bool DeleteCustomer(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteCustomer",connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    int deletedRow = cmd.ExecuteNonQuery();
                    return deletedRow > 0;
                }
            }
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using(SqlCommand cmd = new SqlCommand("sp_GetCustomers",connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customer()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"]),
                                Email = Convert.ToString(reader["Email"]),
                                Phone = Convert.ToString(reader["Phone"])

                            };
                        }
                        
                    }
                }
            }
            return customer;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> list = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                using (SqlCommand cmd = new SqlCommand("sp_GetCustomers", connection)) 
                {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader()) 
                        {

                            while (reader.Read())
                            {
                                Customer customer = new Customer();
                                customer.Id = Convert.ToInt32(reader["Id"]);
                                customer.Name = Convert.ToString(reader["Name"]);
                                customer.Email = Convert.ToString(reader["Email"]);
                                customer.Phone = Convert.ToString(reader["Phone"]);
                                list.Add(customer);
                            }
                        } 
                       
 
                }
                   
                return list;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                using(SqlCommand cmd  = new SqlCommand("sp_UpdateCustomer", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("Email", customer.Email);
                    cmd.Parameters.AddWithValue("Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("Id",customer.Id);
                    connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    return rowEffected > 0;
                }
            }
        }
    }
}
