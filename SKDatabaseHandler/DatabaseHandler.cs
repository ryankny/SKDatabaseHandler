using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SKDatabaseHandler
{
    public static class DatabaseHandler
    {
        /// <summary>
        /// Creates a MSSQL connection object based on a specific database
        /// </summary>
        private static SqlConnection CreateConnection(string DatabaseName)
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings[DatabaseName].ConnectionString);
        }

        /// <summary>
        /// Test the MSSQL database connection by opening and closing a single connection in the connection pool
        /// </summary>
        public static DatabaseConnectionTest TestConnection(string DatabaseName)
        {
            try
            {
                using (SqlConnection Connection = CreateConnection(DatabaseName))
                {
                    Connection.Open();
                    Connection.Close();

                    return new DatabaseConnectionTest(true);
                }
            }
            catch (Exception exception)
            {
                return new DatabaseConnectionTest(false, exception);
            }
        }


        /// <summary>
        /// Execute a query within the database that returns a data set in the form of a table and this is returned as a DatabaseResponse
        /// </summary>
        public static DatabaseResponse ExecuteQuery(string DatabaseName, string Query, CommandType Type = CommandType.Text, List<SqlParameter> Parameters = null)
        {
            DatabaseResponse Response;

            try
            {
                using (SqlConnection Connection = CreateConnection(DatabaseName))
                {
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        if (Parameters != null)
                        {
                            Command.Parameters.AddRange(Parameters.ToArray());
                        }
                        Command.CommandType = Type;

                        using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                        {
                            DataTable Data = new DataTable();
                            DataAdapter.Fill(Data);
                            Response = new DatabaseResponse(Data);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Response = new DatabaseResponse(null, exception);
            }

            return Response;
        }

        /// <summary>
        /// Execute a scalar query within the database that returns a value in the form of an integer and this is returned as a DatabaseResponse
        /// </summary>
        public static DatabaseResponse ExecuteScalarQuery(string DatabaseName, string Query, CommandType Type = CommandType.Text, List<SqlParameter> Parameters = null)
        {
            DatabaseResponse Response;

            try
            {
                using (SqlConnection Connection = CreateConnection(DatabaseName))
                {
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Connection.Open();

                        if (Parameters != null)
                        {
                            Command.Parameters.AddRange(Parameters.ToArray());
                        }
                        Command.CommandType = Type;

                        Response = new DatabaseResponse(Convert.ToInt32(Command.ExecuteScalar()));

                        Connection.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                Response = new DatabaseResponse(null, exception);
            }

            return Response;
        }


        /// <summary>
        /// Execute a non-query command within the database that is typically a INSERT/UPDATE/DELETE and this is returned as a DatabaseResponse
        /// </summary>
        public static DatabaseResponse ExecuteOperation(string DatabaseName, string Query, CommandType Type = CommandType.Text, List<SqlParameter> Parameters = null)
        {
            DatabaseResponse Response;

            try
            {
                using (SqlConnection Connection = CreateConnection(DatabaseName))
                {
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Connection.Open();

                        if (Parameters != null)
                        {
                            Command.Parameters.AddRange(Parameters.ToArray());
                        }
                        Command.CommandType = Type;

                        Command.ExecuteNonQuery();
                        Response = new DatabaseResponse(true);

                        Connection.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                Response = new DatabaseResponse(false, exception);
            }

            return Response;
        }
    }
}
