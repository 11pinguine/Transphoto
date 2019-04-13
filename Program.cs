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
            int count = CountryTable.Select().Count;
            Console.Write("Pocet vsech statu ve DB do pridavani: ");
            Console.WriteLine(count);

            Console.WriteLine("Pridavani statu");
            CountryTable.Insert(new Country(6, "USA", "Amerika"));
            count = CountryTable.Select().Count;
            Console.Write("Pocet vsech statu ve DB po pridavani: ");
            Console.WriteLine(count);
            CountryTable.Delete(6);
            
            Console.WriteLine("----");

            Console.WriteLine("Seznam vsech mest ve DB");
            foreach (City c in CityTable.Select())
            {
                Console.WriteLine(c.Id + " " + c.Name + " " + c.Region + " STAT: " + c.Country.Name);
            }
            Console.WriteLine("----");

            /*
            Console.WriteLine("Seznam vsech vozidel ve DB");
            foreach (Vehicle v in VehicleTable.Select())
            {
                Console.WriteLine(v.Id + "  Znacka:" + v.vehicleModel.Manufacturer + " Model:" + v.vehicleModel.Model + " Podtyp:" + v.Podtyp + " City:" + v.city.Name + " Depo:" + v.depot.Name );
            }
            */
            Console.WriteLine("----");
            Console.WriteLine("Seznam vsech firem ve DB");
            foreach (Company c in CompanyTable.Select())
            {
                Console.WriteLine(c.Id + " " + c.Name);
            }

            Console.WriteLine("----");
            Console.WriteLine("Test procedury UpdateAndArchiveVehicle2");
            VehicleTable.UpdateAndArchiveVehicle(VehicleTable.Select(1),new DateTime(1999,01,01),new DateTime(2019,04,12));

            Console.WriteLine("----");
            Console.WriteLine("Test procedury UploadNewMainPhoto22");
            VehicleTable.UploadNewMainPhoto(VehicleTable.Select(1), "", PlaceTable.Select(1), 10.5, 12.2);




            db.Close();
            Console.ReadKey();
        }
    }
}
