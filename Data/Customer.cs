using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CPSY200FinalRentalProject.Data
{
    public class Customer
    {
        int customerId;
        string lastName;
        string firstName;
        string phone;
        string email;
        bool isBanned;

        [PrimaryKey, AutoIncrement]
        public int CustomerId { get => customerId; set => customerId = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public bool IsBanned { get => isBanned; set => isBanned = value; }

        public Customer()
        {
             
        }

        public Customer(int customerId, string lastName, string firstName, string phone, string email, bool isBanned)
        {
            this.customerId = customerId;
            this.lastName = lastName;
            this.firstName = firstName;
            this.phone = phone;
            this.email = email;
            this.isBanned = isBanned;
        }
    }
}
