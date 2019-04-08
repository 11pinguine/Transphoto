using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.ORM
{
    public class Photo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float CoordN { get; set; }
        public float CoordE { get; set; }
        public String Path { get; set; }

       
    }
}
