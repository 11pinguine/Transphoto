using System;
using AuctionSystem.ORM;
using AuctionSystem.ORM.DAO.Sqls;

namespace AuctionSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Database db = new Database();
            db.Connect();

            Vehicle u = new Vehicle();
            u.Id = 14;
           // u.Manufacturer = "Stadler";
            //u.Model = "Tango NF2 kopie";
           

            VehicleTable.Insert(u, db);

            int count1 = VehicleTable.Select(db).Count;
            int dltCount = VehicleTable.Delete(7, db);
            int count2 = VehicleTable.Select(db).Count;

            Console.WriteLine("#C: " + count1);
            Console.WriteLine("#D: " + dltCount);
            Console.WriteLine("#C: " + count2);

            db.Close();
        }
    }
}
