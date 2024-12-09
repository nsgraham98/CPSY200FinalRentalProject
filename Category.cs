using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CPSY200FinalRentalProject.Data
{
    public class Category
    {
        int categoryId;
        string name;

        public Category(int categoryId, string name)
        {
            this.categoryId = categoryId;
            this.name = name;
        }
        public Category()
        {
            
        }

        [PrimaryKey, AutoIncrement]
        public int CategoryId { get => categoryId; set => categoryId = value; }
        public string Name { get => name; set => name = value; }
    }
}
