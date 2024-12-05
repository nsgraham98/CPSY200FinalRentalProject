using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CPSY200FinalRentalProject.Data
{
    public class EquipmentInRental
    {
        int rentalId;
        int equipmentId;
        bool isReturned;

        public EquipmentInRental(int rentalId, int equipmentId)
        {
            this.rentalId = rentalId;
            this.equipmentId = equipmentId;
        }
        public EquipmentInRental()
        {
            
        }

        public int RentalId { get => rentalId; set => rentalId = value; }
        public int EquipmentId { get => equipmentId; set => equipmentId = value; }
    }
}
