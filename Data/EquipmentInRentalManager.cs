using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSY200FinalRentalProject.Data
{
    public class EquipmentInRentalManager
    {
        public static List<EquipmentInRental> eqInRental = new List<EquipmentInRental>();

        // maybe change return
        public static string AddEquipmentInRental(int rentalId, int equipmentId)
        {
            DBHandler db = new DBHandler();
            db.InsertEquipmentInRentalDB(rentalId, equipmentId);
            return "Added Successfully";
        }

        public static List<EquipmentInRental> GetEquipmentInRentals()
        {
            DBHandler db = new DBHandler();
            //List<Category> categories = new List<Category>();
            eqInRental = db.LoadEquipmentInRentalsFromDB();
            return eqInRental;
        }

        public static string EditEquipmentInRental(int rentalId, int equipmentId, bool isReturned)
        {
            string message = DBHandler.UpdateEquipmentInRentalDB(rentalId, equipmentId, isReturned);
            GetEquipmentInRentals();
            return message;
        }
    }
}
