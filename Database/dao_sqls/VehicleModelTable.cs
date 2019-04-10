using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace AuctionSystem.ORM.DAO.Sqls
{
    public class VehicleModelTable
    {
        public static String SQL_SELECT = "SELECT * FROM Vehicle_Model";
        public static String SQL_SELECT_ID = "SELECT * FROM Vehicle_Model WHERE id=@id";
        public static String SQL_INSERT = "INSERT INTO Vehicle_Model VALUES (@manufacturer, @model, @capacity_standing, @capacity_seating, )" +
            "@vehicle_type, @length, @width, @height, @max_speed, @low_floor, @powered_by)";
        public static String SQL_DELETE_ID = "DELETE FROM Vehicle_Model WHERE id=@id";
        public static String SQL_UPDATE = "UPDATE Vehicle_Model SET manufacturer=@manufacturer, model=@model, capacity_standing=@capacity_standing, " +
            "capacity_seating=@capacity_seating, vehicle_type=@vehicle_type, length=@length, width=@width, height=@height, max_speed=@max_speed, " +
            "low_floor=@low_floor, powered_by=@powered_by WHERE id=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(VehicleModel vehicleModel, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, vehicleModel);
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
        public static int Update(VehicleModel vehicleModel, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, vehicleModel);
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
        public static Collection<VehicleModel> Select(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<VehicleModel> vehicleModels = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return vehicleModels;
        }

        /// <summary>
        /// Select the record.
        /// </summary>
        /// <param name="id">user id</param>
        public static VehicleModel Select(int id, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<VehicleModel> VehicleModels = Read(reader);
            VehicleModel VehicleModel = null;
            if (VehicleModels.Count == 1)
            {
                VehicleModel = VehicleModels[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return VehicleModel;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        /// <param name="idVehicleModel">user id</param>
        /// <returns></returns>
        public static int Delete(int idVehicleModel, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@id", idVehicleModel);
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
        private static void PrepareCommand(SqlCommand command, VehicleModel VehicleModel)
        {
            command.Parameters.AddWithValue("@id", VehicleModel.Id);
            command.Parameters.AddWithValue("@manufacturer", VehicleModel.Manufacturer);
            command.Parameters.AddWithValue("@model", VehicleModel.Model);
            command.Parameters.AddWithValue("@capacity_seating", VehicleModel.CapacitySeating);
            command.Parameters.AddWithValue("@capacity_standing", VehicleModel.CapacityStanding);
            command.Parameters.AddWithValue("@vehicle_type", VehicleModel.VehicleType);
            command.Parameters.AddWithValue("@length", VehicleModel.Length);
            command.Parameters.AddWithValue("@width", VehicleModel.Width);
            command.Parameters.AddWithValue("@height", VehicleModel.Height);
            command.Parameters.AddWithValue("@weight", VehicleModel.Weight);
            command.Parameters.AddWithValue("@max_speed", VehicleModel.MaxSpeed);
            command.Parameters.AddWithValue("@low_floor", VehicleModel.LowFloor);
            command.Parameters.AddWithValue("@powered_by", VehicleModel.PoweredBy);
     


        }

        private static Collection<VehicleModel> Read(SqlDataReader reader)
        {
            Collection<VehicleModel> vehicleModels = new Collection<VehicleModel>();

            while (reader.Read())
            {
                int i = -1;
                VehicleModel vehicleModel = new VehicleModel();
                vehicleModel.Id = reader.GetInt32(++i);
                vehicleModel.Manufacturer = reader.GetString(++i);
                vehicleModel.Model = reader.GetString(++i);
                vehicleModel.CapacitySeating = reader.GetInt32(++i);
                vehicleModel.CapacityStanding = reader.GetInt32(++i);
                vehicleModel.VehicleType = reader.GetString(++i);
                vehicleModel.Length = (float)reader.GetDouble(++i);
                vehicleModel.Width = (float)reader.GetDouble(++i);
                vehicleModel.Height = (float)reader.GetDouble(++i);
                vehicleModel.Weight = !reader.IsDBNull(++i)? reader.GetInt32(i) : 0;
                vehicleModel.MaxSpeed = reader.GetInt32(++i);
                vehicleModel.LowFloor = reader.GetBoolean(++i);
                vehicleModel.PoweredBy = reader.GetString(++i);





                vehicleModels.Add(vehicleModel);
            }
            return vehicleModels;
        }

        // It is not a function from the functional analysis
        // it is an example how to work with stored procedures
        public static string CheckVehicleModel(int idVehicleModel, Database pDb)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("CheckVehicleModel");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@idVehicleModel";
            input.DbType = DbType.Int32;
            input.Value = idVehicleModel;
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