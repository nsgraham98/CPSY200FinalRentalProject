using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSY200FinalRentalProject.Data
{
    public class CustomerManager
    {
        public static List<Customer> customers = new List<Customer>();

        // maybe change return
        public static string AddCustomer(string lastName, string firstName, string phone, string email)
        {
            DBHandler db = new DBHandler();
            db.InsertCustomerDB(lastName, firstName, phone, email);
            return "Added Successfully";
        }

        public static List<Customer> GetCustomers()
        {
            DBHandler db = new DBHandler();
            //List<Customer> customers = new List<Customer>();
            customers = db.LoadCustomersFromDB();
            return customers;
        }

        public static string EditCustomer(int custId, string lastName, string firstName, string phone, string email, bool isBanned)
        {
            string message = DBHandler.UpdateCustomerDB(custId, lastName, firstName, phone, email, isBanned); 
            GetCustomers();
            return message;
        }
    }
}
