using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSY200FinalRentalProject.Data
{
    public class EquipmentManager
    {
        public static List<Equipment> equipment = new List<Equipment>();

        // maybe change return
        public static string AddEquipment(int category, string name, string description, double rentalCost, string availability)
        {
            DBHandler db = new DBHandler();
            db.InsertEquipmentDB(category, name, description, rentalCost, availability);
            return "Added Successfully";
        }

        public static List<Equipment> GetEquipment()
        {
            DBHandler db = new DBHandler();
            //List<Equipment> equipment = new List<Equipment>();
            equipment = db.LoadEquipmentFromDB();
            return equipment;
        }

        public static string EditEquipment(int equipmentId, int category, string name, string description, double rentalCost, string availability)
        {
            string message = DBHandler.UpdateEquipmentDB(equipmentId, category, name, description, rentalCost, availability);
            GetEquipment();
            return message;
        }
    }
}
