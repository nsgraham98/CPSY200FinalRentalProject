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
            DBHandler db = new DBHandler("customer");
            db.InsertCustomerDB(lastName, firstName, phone, email);
            return "Added Successfully";
        }

        public static List<Customer> GetCustomers()
        {
            DBHandler db = new DBHandler("customer");
            //List<Customer> customers = new List<Customer>();
            customers = db.LoadCustomersFromDB();
            return customers;
        }

        public static void EditCustomer(int custId, string lastName, string firstName, string phone, string email, bool isBanned)
        {
            string message = DBHandler.UpdateCustomerDB(custId, lastName, firstName, phone, email, isBanned); 
        }

        public static List<Customer> SearchCustomers(bool isId, bool isLastName, bool isPhone, bool isEmail, string searchField )
        {
            DBHandler db = new DBHandler("customer");
            List<Customer> customerList = db.LoadCustomersFromDB();
            List<Customer> foundList = new List<Customer> ();

            foreach (Customer customer in customerList)
            {
                if (isId)
                {
                    if (Convert.ToInt32(searchField) == customer.CustomerId)
                    {
                        foundList.Add(customer);
                        return foundList;
                    }
                }
                if (isPhone) 
                {
                    if (searchField == customer.Phone)
                    {
                        foundList.Add(customer);
                        return foundList;
                    }
                }
                if (isEmail)
                {
                    if (searchField == customer.Email)
                    {
                        foundList.Add(customer);
                        return foundList;
                    }
                }
                if (isLastName)
                {
                    if (searchField.ToLower() == customer.LastName.ToLower())
                    {
                        foundList.Add(customer);
                    }
                }

            }
            return foundList;
        }

        public static Customer GetCustomerFromId(int id)
        {
            DBHandler db = new DBHandler("customer");
            List<Customer> customers = db.LoadCustomersFromDB();

            foreach (Customer customer in customers)
            {
                if (customer.CustomerId == id)
                { return customer; }
            }
            return new Customer();         
        }
    }
}
