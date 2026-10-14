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
using CustomerService.Repositories;

namespace CustomerService
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerService()
        {
            this._customerRepository = new CustomerRepository();
        }
        public CustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public bool AddCustomer(Customer customer)
        {
            if(customer == null 
                || string.IsNullOrWhiteSpace(customer.Name) 
                || string.IsNullOrWhiteSpace(customer.Email) 
                || string.IsNullOrWhiteSpace(customer.Phone))   
            { 
                return false; 
            }
            return _customerRepository.AddCustomer(customer);
        }

        public bool DeleteCustomer(int id)
        {
             return _customerRepository.DeleteCustomer(id);
        }

        public Customer GetCustomerById(int id)
        {
             return _customerRepository.GetCustomerById(id);
        }

        public List<Customer> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }

        public bool UpdateCustomer(Customer customer)
        {
            if(customer == null 
                || string.IsNullOrWhiteSpace(customer.Name)
                || string.IsNullOrWhiteSpace(customer.Email)
                || string.IsNullOrWhiteSpace(customer.Phone)) 
            { 
                return false; 
            }
            return _customerRepository.UpdateCustomer(customer);
        }
    }
}
