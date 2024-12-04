using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CPSY200FinalRentalProject.Data
{
    public class Rental
    {
        int rentalId;
        DateTime startDate;
        DateTime endDate;
        int customerId;

        [PrimaryKey, AutoIncrement]
        public int RentalId { get => rentalId; set => rentalId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public int CustomerId { get => customerId; set => customerId = value; }

        public Rental(int rentalId, DateTime startDate, DateTime endDate, int customerId)
        {
            this.RentalId = rentalId;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.CustomerId = customerId;
        }
        public Rental()
        {
            
        }
    }
}
