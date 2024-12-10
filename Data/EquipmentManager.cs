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
            DBHandler db = new DBHandler("equipment");
            db.InsertEquipmentDB(category, name, description, rentalCost, availability);
            return "Added Successfully";
        }

        public static List<Equipment> GetEquipment()
        {
            DBHandler db = new DBHandler("equipment");
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
        public static List<Equipment> SearchEquipment(bool isId, bool isName, bool isCategory, string searchField)
        {
            DBHandler db = new DBHandler("equipment");
            List<Equipment> equipmentList = db.LoadEquipmentFromDB();
            List<Equipment> foundList = new List<Equipment>();

            foreach (Equipment equipment in equipmentList)
            {
                if (isId)
                {
                    if (Convert.ToInt32(searchField) == equipment.EquipmentId)
                    {
                        foundList.Add(equipment);
                        return foundList;
                    }
                }
                if (isName)
                {
                    if (searchField == equipment.Name)
                    {
                        foundList.Add(equipment);
                        return foundList;
                    }
                }
                if (isCategory)
                {
                    if (searchField == CategoryManager.GetCategoryName(equipment.Category))
                    {
                        foundList.Add(equipment);
                    }
                }
            }
            return foundList;
        }
    }
}
