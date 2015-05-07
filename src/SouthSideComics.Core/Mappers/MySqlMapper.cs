using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Framework.OptionsModel;
using SouthSideComics.Core.Common;

namespace SouthSideComics.Core.Mappers
{

    public abstract class MySqlMapper
    {
        ////////////////////////////////////////////////////////////
        /// CONSTRUCTORS ///////////////////////////////////////////
        ////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Creates a MySqlMapper using the default connection string from the application settings
        /// </summary>
        public MySqlMapper(IOptions<ConnectionConfig> config)
        {
            this.ConnectionString = config.Options.ConnectionString;
        }

        #endregion

        /////////////////////////////////////////////////////////////
        /// PROPERTIES //////////////////////////////////////////////
        /////////////////////////////////////////////////////////////
        #region Properties
        
        public string ConnectionString { get; set; }

        #endregion Properties


        /////////////////////////////////////////////////////////////
        /// METHODS /////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////
        #region Methods

        /// <summary>
        /// Provides generic execute scalar wrapper that returns the scalar object value
        /// </summary>
        /// <param name="cmdText">The sql text or stored proc name</param>
        /// <param name="commandType">The type of the command</param>
        /// <param name="parameters">The parameters that need to be added</param>        
        /// <returns>Returns an object of the scalar result</returns>
        public async virtual Task<object> ExecuteScalarAsync(string cmdText, CommandType commandType, MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            using (MySqlCommand command = new MySqlCommand(cmdText, conn))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                command.CommandType = commandType;
                command.Connection.Open();
                return await command.ExecuteScalarAsync();
            }
        }        

        /// <summary>
        /// Provides a generic ExecuteNonQuery wrapper that returns the LastInsertId
        /// </summary>
        /// <param name="cmdText">The sql text or stored proc name</param>
        /// <param name="commandType">The type of the command</param>
        /// <param name="parameters">The parameters that need to be added</param>        
        /// <param name="lastInsertId">Out parameter for the last inserted identifier</param>
        /// <returns>Returns the number of rows affected</returns>
        public async virtual Task<long> ExecuteNonQueryAsync(string cmdText, CommandType commandType, MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            using (MySqlCommand command = new MySqlCommand(cmdText, conn))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                command.CommandType = commandType;
                command.Connection.Open();
                await command.ExecuteNonQueryAsync();
                return command.LastInsertedId;                
            }
        }        

        /// <summary>
        /// Provides generic data reader execution that returns a single object
        /// </summary>
        /// <param name="cmdText">The sql text or stored proc name</param>
        /// <param name="commandType">The type of the command</param>
        /// <param name="parameters">The parameters that need to be added</param>
        /// <param name="parser">The parsing method that should execute for each row</param>
        /// <returns></returns>
        public async virtual Task<T> ExecuteReaderAsync<T>(string cmdText, CommandType commandType, MySqlParameter[] parameters, Func<MySqlDataReader, T> parser)
        {
            T result;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            using (MySqlCommand command = new MySqlCommand(cmdText, conn))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                command.CommandType = commandType;
                command.Connection.Open();
                using (var reader = await command.ExecuteReaderAsync() as MySqlDataReader)
                {
                    reader.Read();
                    result = parser(reader);
                }
            }
            return result;
        }        

        /// <summary>
        /// Provides generic data reader execution that returns a List of objects
        /// </summary>
        /// <param name="cmdText">The sql text or stored proc name</param>
        /// <param name="commandType">The type of the command</param>
        /// <param name="parameters">The parameters that need to be added</param>
        /// <param name="parser">The parsing method that should execute for each row</param>
        /// <param name="rowcount">Outputs the row count found through the FOUND_ROWS MySql function</param>
        /// <returns></returns>
        public async virtual Task<PagedList<T>> ExecuteReaderListAsync<T>(string cmdText, CommandType commandType, MySqlParameter[] parameters, Func<MySqlDataReader, T> parser)
        {
            PagedList<T> results = new PagedList<T>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            using (MySqlCommand command = new MySqlCommand(cmdText, conn))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                command.CommandType = commandType;
                command.Connection.Open();
                using (var reader = await command.ExecuteReaderAsync() as MySqlDataReader)
                {
                    while (reader.Read())
                    {
                        T result = parser(reader);
                        if (result != null)
                            results.Add(result);
                    }
                }
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT FOUND_ROWS() `rowcount`;";
                results.TotalCount = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
            return results;
        }

        /// <summary>
        /// Provides generic data reader execution that returns an enumerable collection for large record sets
        /// </summary>
        /// <param name="cmdText">The sql text or stored proc name</param>
        /// <param name="commandType">The type of the command</param>
        /// <param name="parameters">The parameters that need to be added</param>
        /// <param name="parser">The parsing method that should execute for each row</param>
        /// <returns></returns>
        public virtual IEnumerable<T> ExecuteReaderEnumerator<T>(string cmdText, CommandType commandType, MySqlParameter[] parameters, Func<MySqlDataReader, T> parser)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            using (MySqlCommand command = new MySqlCommand(cmdText, conn))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                command.CommandType = commandType;
                command.Connection.Open();
                using (var reader = command.ExecuteReader() as MySqlDataReader)
                {
                    while (reader.Read())
                    {
                        T result = parser(reader);
                        if (result != null)
                            yield return result;
                    }
                }
            }
        }

        #endregion Methods    
    }
}
