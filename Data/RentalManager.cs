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
            DBHandler db = new DBHandler();
            db.InsertRentalDB(startDate, endDate, customerId);
            return "Added Successfully";
        }

        public static List<Rental> GetRentals()
        {
            DBHandler db = new DBHandler();
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
    }
}
