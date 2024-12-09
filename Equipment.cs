using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CPSY200FinalRentalProject.Data
{
    public class Equipment
    {
        int equipmentId;
        string category;
        string name;
        string description;
        double rentalCost;
        string availability;

        [PrimaryKey, AutoIncrement]
        public int EquipmentId { get => equipmentId; set => equipmentId = value; }
        public string Category { get => category; set => category = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public double RentalCost { get => rentalCost; set => rentalCost = value; }
        public string Availability { get => availability; set => availability = value; }

        public Equipment(int equipmentId, string category, string name, string description, double rentalCost, string availability)
        {
            this.equipmentId = equipmentId;
            this.category = category;
            this.name = name;
            this.description = description;
            this.rentalCost = rentalCost;
            this.availability = availability;
        }
        public Equipment()
        {
            
        }
    }
}
