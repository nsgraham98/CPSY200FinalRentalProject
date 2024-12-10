using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSY200FinalRentalProject.Data
{
    public class RentalManager
    {
        public static List<Rental> rentals = new List<Rental>();

        // maybe change return
        public static string AddRental(DateTime startDate, DateTime endDate, int customerId)
        {
            DBHandler db = new DBHandler("rental");
            db.InsertRentalDB(startDate, endDate, customerId);
            return "Added Successfully";
        }

        public static List<Rental> GetRentals()
        {
            DBHandler db = new DBHandler("rental");
            //List<Rental> rentals = new List<Rental>();
            rentals = db.LoadRentalsFromDB();
            return rentals;
        }

        public static string EditRental(int rentalId, DateTime startDate, DateTime endDate, int customerId)
        {
            string message = DBHandler.UpdateRentalDB(rentalId, startDate, endDate, customerId);
            GetRentals();
            return message;
        }

        public static List<Rental> SearchRentals(bool isCustomerId, bool isEquipmentId, bool isCustomerPhone, bool isCustomerEmail, string searchField)
        {
            DBHandler db = new DBHandler("equipment");
            List<Rental> rentalList = db.LoadRentalsFromDB();
            List<Customer> custList = db.LoadCustomersFromDB();
            List<Equipment> equipmentList = db.LoadEquipmentFromDB();
            List<Rental> foundList = new List<Rental>();

            foreach (Rental rental in rentalList)
            {
                if (isCustomerId)
                {
                    if (Convert.ToInt32(searchField) == rental.CustomerId)
                    {
                        foundList.Add(rental);
                    }
                }
                if (isCustomerEmail) 
                {
                    foreach (Customer customer in custList) 
                    {
                        if (searchField == customer.Email )
                        {
                            foundList.Add(rental);
                        }
                    }      
                }
                if (isCustomerPhone)
                {
                    foreach (Customer customer in custList)
                    {
                        if (searchField == customer.Phone)
                        {
                            foundList.Add(rental);
                        }
                    }     
                }
                if (isEquipmentId)
                {
                    foreach(Equipment equipment in equipmentList)
                    {
                        if (Convert.ToInt32(searchField) == equipment.EquipmentId)
                        {
                            foundList.Add(rental);
                        }
                    }                   
                }
            }
            return foundList;
        }
    }
}
