using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionSystem.ORM
{
    public class Category
    {
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //artificial columns
        public int ItemsCount { get; set; }
        public bool CanBeDeleted { get { return this.ItemsCount == 0; } }
    }
}