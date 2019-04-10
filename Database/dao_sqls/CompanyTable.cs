using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace AuctionSystem.ORM.DAO.Sqls
{
    public class CompanyTable
    {
        public static String SQL_SELECT = "SELECT * FROM Company";
        public static String SQL_SELECT_ID = "SELECT * FROM Company WHERE id=@id";
        public static String SQL_INSERT = "INSERT INTO Company VALUES (@name)";
        public static String SQL_DELETE_ID = "DELETE FROM Company WHERE id=@id";
        public static String SQL_UPDATE = "UPDATE Company SET name=@name WHERE id=@id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Company company, Database pDb = null)
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
            PrepareCommand(command, company);
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
        public static int Update(Company company, Database pDb = null)
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
            PrepareCommand(command, company);
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
        public static Collection<Company> Select(Database pDb = null)
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

            Collection<Company> companies = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return companies;
        }

        /// <summary>
        /// Select the record.
        /// </summary>
        /// <param name="id">user id</param>
        public static Company Select(int id, Database pDb = null)
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

            Collection<Company> Companies = Read(reader);
            Company Company = null;
            if (Companies.Count == 1)
            {
                Company = Companies[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Company;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>
        /// <param name="idCompany">user id</param>
        /// <returns></returns>
        public static int Delete(int idCompany, Database pDb = null)
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

            command.Parameters.AddWithValue("@id", idCompany);
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
        private static void PrepareCommand(SqlCommand command, Company Company)
        {
            command.Parameters.AddWithValue("@id", Company.Id);
            command.Parameters.AddWithValue("@name", Company.Name);
           

        }

        private static Collection<Company> Read(SqlDataReader reader)
        {
            Collection<Company> companies = new Collection<Company>();

            while (reader.Read())
            {
                int i = -1;
                Company company = new Company();
                company.Id = reader.GetInt32(++i);
                company.Name = reader.GetString(++i);
                



                companies.Add(company);
            }
            return companies;
        }

        // It is not a function from the functional analysis
        // it is an example how to work with stored procedures
        public static string CheckCompany(int idCompany, Database pDb)
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
            SqlCommand command = db.CreateCommand("CheckCompany");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@idCompany";
            input.DbType = DbType.Int32;
            input.Value = idCompany;
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