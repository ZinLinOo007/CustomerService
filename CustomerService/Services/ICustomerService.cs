using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CustomerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomerService" in both code and config file together.
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        List<Customer> GetCustomers();

        [OperationContract]
        Customer GetCustomerById(int id);

        [OperationContract]
        bool AddCustomer(Customer customer);

        [OperationContract]
        bool UpdateCustomer(Customer customer);

        [OperationContract]
        bool DeleteCustomer(int id);
    }
}
