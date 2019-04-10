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


            Console.WriteLine("Seznam vsech statu ve DB");
            foreach ( Country c in CountryTable.Select())
            {
                Console.WriteLine(c.Id + " " + c.Name + " " +c.Continent);
            }
            Console.WriteLine("----");

            Console.WriteLine("Seznam vsech mest ve DB");
            foreach (City c in CityTable.Select())
            {
                Console.WriteLine(c.Id + " " + c.Name + " " + c.Region + " STAT: " + c.Country.Name);
            }
            Console.WriteLine("----");


            Console.WriteLine("Seznam vsech vozidel ve DB");
            foreach (Vehicle v in VehicleTable.Select())
            {
                Console.WriteLine(v.Id + "  Znacka:" + v.vehicleModel.Manufacturer + " Model:" + v.vehicleModel.Model + " Podtyp:" + v.Podtyp + " City:" + v.city.Name + " Depo:" + v.depot.Name );
            }

            //VehicleTable.Insert(u, db);
            /*
            int count1 = VehicleTable.Select(db).Count;
            int dltCount = VehicleTable.Delete(7, db);
            int count2 = VehicleTable.Select(db).Count;

            Console.WriteLine("#C: " + count1);
            Console.WriteLine("#D: " + dltCount);
            Console.WriteLine("#C: " + count2);
            */
            db.Close();
            Console.ReadKey();
        }
    }
}
