using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace AuctionSystem.ORM.DAO.Sqls
{
    public class VehicleTable
    {
        private static String SQL_SELECT = "SELECT * FROM Vehicle";
        private static String SQL_SELECT_ID = "SELECT * FROM Vehicle WHERE id=@id";
        private static String SQL_INSERT = "INSERT INTO Vehicle VALUES (@construction_year, @state, @evidence_id, @main_photo_path, @car_license_plate, @podtyp)";
        private static String SQL_DELETE_ID = "DELETE FROM Vehicle WHERE id=@id";
        private static String SQL_UPDATE = "UPDATE Vehicle SET construction_year=@construction_year, state=@state, evidence_id=@evidence_id, main_photo_path=@main_photo_path, car_license_plate=@car_license_plate, podtyp=@podtyp WHERE idVehicle=@id";
        private static string SQL_EXEC_UpdateAndArchiveVehicle2 = "exec UpdateAndArchiveVehicle2 @p_Id, @p_start_date,@p_end_date, "+
                                                  "@p_city_id,@p_depot_id, @p_evidence_id, @p_plate_number, @p_state";
        private static string SQL_EXEC_UploadNewMainPhoto = "exec UploadNewMainPhoto @p_vehicle_Id, @p_photo, @p_place_id, @p_coordN, @p_coordE";


        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Vehicle vehicle, Database pDb = null)
        {
            Database db = checkDB(pDb);

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, vehicle);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
             
        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(Vehicle vehicle, Database pDb = null)
        {
            Database db = checkDB(pDb);

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, vehicle);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<Vehicle> Select(Database pDb = null)
        {
            Database db = checkDB(pDb);

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<Vehicle> vehicles = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return vehicles;
        }

        /// <summary>
        /// Select the record.
        /// </summary>
        /// <param name="id">user id</param>
        public static Vehicle Select(int id, Database pDb = null)
        {
            Database db = checkDB(pDb);
            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Vehicle> Vehicles = Read(reader);
            Vehicle Vehicle = null;
            if (Vehicles.Count == 1)
            {
                Vehicle = Vehicles[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Vehicle;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        /// <param name="idVehicle">user id</param>
        /// <returns></returns>
        public static int Delete(int idVehicle, Database pDb = null)
        {
            Database db = checkDB(pDb);
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@id", idVehicle);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        /// <summary>
        ///  Prepare a command.
        /// </summary>
        private static void PrepareCommand(SqlCommand command, Vehicle Vehicle)
        {
            command.Parameters.AddWithValue("@id", Vehicle.Id);
            command.Parameters.AddWithValue("@construction_year", Vehicle.ConstructionYear);
            command.Parameters.AddWithValue("@state", Vehicle.State);
            command.Parameters.AddWithValue("@evidence_id", Vehicle.EvidenceId);
            command.Parameters.AddWithValue("@main_photo_path", Vehicle.MainPhotoPath);
            command.Parameters.AddWithValue("@car_license_plate", Vehicle.CarLicensePlate);
            command.Parameters.AddWithValue("@podtyp", Vehicle.Podtyp);


        }

        private static Collection<Vehicle> Read(SqlDataReader reader)
        {
            Collection<Vehicle> vehicles = new Collection<Vehicle>();

            while (reader.Read())
            {
                int i = -1;
                Vehicle vehicle = new Vehicle();
                vehicle.Id = reader.GetInt32(++i);
                vehicle.ConstructionYear = reader.GetInt32(++i);
                vehicle.State = reader.GetString(++i);
                vehicle.EvidenceId = reader.GetString(++i);
                vehicle.MainPhotoPath =  (reader.IsDBNull(++i))? "": reader.GetString(i);
                vehicle.CarLicensePlate = (reader.IsDBNull(++i)) ? "" : reader.GetString(i);
                vehicle.vehicleModel = VehicleModelTable.Select(reader.GetInt32(++i));
                vehicle.city = CityTable.Select(reader.GetInt32(++i));
                vehicle.depot = DepotTable.Select(reader.GetInt32(++i));
                vehicle.Podtyp = reader.GetString(++i);               


                vehicles.Add(vehicle);
            }
            return vehicles;
        }

        public static int UpdateAndArchiveVehicle(Vehicle v, DateTime StartDate, DateTime EndDate, Database pDb = null)
        {
            Database db = checkDB(pDb);

            SqlCommand command = db.CreateCommand(SQL_EXEC_UpdateAndArchiveVehicle2);
            command.Parameters.AddWithValue("@p_Id",v.Id);
            command.Parameters.AddWithValue("@p_start_date", StartDate.Date);
            command.Parameters.AddWithValue("@p_end_date", EndDate.Date);
            command.Parameters.AddWithValue("@p_city_id", v.city.Id);
            command.Parameters.AddWithValue("@p_depot_id", v.depot.Id);
            command.Parameters.AddWithValue("@p_depot_id", v.EvidenceId);
            command.Parameters.AddWithValue("@p_plate_number", v.CarLicensePlate);
            command.Parameters.AddWithValue("@p_state", v.State);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;

        }

        public static int UploadNewMainPhoto(Vehicle v, string path, Place place, double N, double E, Database pDb = null)
        {
            Database db = checkDB(pDb);
            SqlCommand command = db.CreateCommand(SQL_EXEC_UploadNewMainPhoto);

            command.Parameters.AddWithValue("@p_vehicle_Id", v.Id);
            command.Parameters.AddWithValue("@p_photo", path);
            command.Parameters.AddWithValue("@p_place_id", place.Id);
            command.Parameters.AddWithValue("@p_coordN", N);
            command.Parameters.AddWithValue("@p_coordE", E);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static Database checkDB(Database pDb = null)
        {
            if (pDb == null)
            {
                Database db = new Database();
                db.Connect();
                return db;
            }
            else
            {
                return (Database)pDb;
            }
        }
        
        // It is not a function from the functional analysis
        // it is an example how to work with stored procedures
        public static string CheckVehicle(int idVehicle, Database pDb)
        {
            Database db = checkDB(pDb);

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("CheckVehicle");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@idVehicle";
            input.DbType = DbType.Int32;
            input.Value = idVehicle;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            // 3. create output parameters
            SqlParameter output = new SqlParameter();
            output.ParameterName = "@result";
            output.DbType = DbType.String;
            output.Direction = ParameterDirection.Output;
            command.Parameters.Add(output);

            // 4. execute procedure
            int ret = db.ExecuteNonQuery(command);

            // 5. get values of the output parameters
            string result = command.Parameters["@result"].Value.ToString();

            if (pDb == null)
            {
                db.Close();
            }
            return result;
        }
    }
}