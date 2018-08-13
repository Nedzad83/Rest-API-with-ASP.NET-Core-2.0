using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RestAPI.Data
{
    public partial class DataAccess<T> : DataAccessBase where T : class
    {
        public DataAccess(string connectionString, ConnectionType connectionType = ConnectionType.SqlServer) : base()
        {
            _connectionType = connectionType;
            _connectionString = connectionString;
        }

        public int ExecuteStoredProcedure(string Procedure, params object[] parameter)
        {
            using (var sCon = GetConnection())
            {
                if (sCon.State == ConnectionState.Closed)
                    sCon.Open();

                using (IDbCommand cmd = sCon.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedure;

                    cmd.MapSingleInputParameter(parameter);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int ExecuteStoredProcedure(string Procedure, T parameter)
        {
            using (var sCon = GetConnection())
            {
                if (sCon.State == ConnectionState.Closed)
                    sCon.Open();

                using (IDbCommand cmd = sCon.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedure;

                    cmd.MapInputParameters(parameter);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int ExecuteStoredProcedureSingleOutput(string Procedure, T parameter, out object output)
        {
            using (var sCon = GetConnection())
            {
                if (sCon.State == ConnectionState.Closed)
                    sCon.Open();

                using (IDbCommand cmd = sCon.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedure;

                    cmd.MapInputParameters(parameter);

                    int result = cmd.ExecuteNonQuery();

                    output = cmd.MapOutputParameter();

                    return result;
                }
            }
        }

        public T ExecuteStoredProcedureSingleResult(string Procedure, params object[] parameter)
        {
            using (var sCon = GetConnection())
            {
                if (sCon.State == ConnectionState.Closed)
                    sCon.Open();

                using (IDbCommand cmd = sCon.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedure;

                    cmd.MapSingleInputParameter(parameter);

                    IDbDataAdapter adapter = GetDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {

                        var data = new Helpers.DataNamesMapper<T>();
                        var result = data.Map(ds.Tables[0].Rows[0]);
                        return result;
                    }
                    else
                    {
                        return Activator.CreateInstance<T>();
                    }
                }
            }
        }

        public IEnumerable<T> ExecuteStoredProcedureCollectionResult(string Procedure)
        {
            return _executeStoredProcedureCollectionResult(Procedure, new object[0]);
        }

        public IEnumerable<T> ExecuteStoredProcedureCollectionResult(string Procedure, params object[] parameter)
        {
            return _executeStoredProcedureCollectionResult(Procedure, parameter);
        }

        private IEnumerable<T> _executeStoredProcedureCollectionResult(string Procedure, params object[] parameter)
        {
            using (var sCon = GetConnection())
            {
                if (sCon.State == ConnectionState.Closed)
                    sCon.Open();

                using (IDbCommand cmd = sCon.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedure;

                    if (parameter.Count() > 0)
                    {
                        cmd.MapSingleInputParameter(parameter);
                    }

                    IDbDataAdapter adapter = GetDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {

                        var data = new Helpers.DataNamesMapper<T>();
                        var result = data.Map(ds.Tables[0]);

                        return result;
                    }
                    else
                    {
                        return Activator.CreateInstance<List<T>>();
                    }
                }
            }
        }
    }

    public partial class DataAccess<T, R> : DataAccessBase where T : class where R : class
    {

        public DataAccess(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Execute procedure
        /// </summary>
        /// <param name="Procedure">Store procedure name</param>
        /// <param name="parameter">Concrete type parameter</param>
        /// <returns>Generic concrete class</returns>
        public R ExecuteStoredProcedureSingleResult(string Procedure, T parameter)
        {
            using (var sCon = GetConnection())
            {
                if (sCon.State == ConnectionState.Closed)
                    sCon.Open();
                using (IDbCommand cmd = sCon.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedure;

                    cmd.MapInputParameters(parameter);
                    IDbDataAdapter adapter = GetDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if(ds.Tables.Count > 0)
                    { 
                        var data = new Helpers.DataNamesMapper<R>();
                        var result = data.Map(ds.Tables[0].Rows[0]);
                        return result;
                    }
                    else
                        return Activator.CreateInstance<R>();
                }
            }
        }

        /// <summary>
        /// Execute procedure
        /// </summary>
        /// <param name="Procedure">Store procedure name</param>
        /// <param name="parameter">Concrete type parameter</param>
        /// <returns>Return a collection<R></returns>
        public IEnumerable<R> ExecuteStoredProcedureCollectionResult(string Procedure, T parameter)
        {
            using (var sCon = GetConnection())
            {
                if (sCon.State == ConnectionState.Closed)
                    sCon.Open();
                using (IDbCommand cmd = sCon.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedure;

                    cmd.MapInputParameters(parameter);

                    IDbDataAdapter adapter = GetDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if(ds.Tables.Count > 0)
                    {
                        var data = new Helpers.DataNamesMapper<R>();
                        var result = data.Map(ds.Tables[0]);

                        return result;
                    }
                    else
                    {
                        return Activator.CreateInstance<IEnumerable<R>>();
                    }
                }
            }
        }
    }

    public partial class DataAccess<T, R, S> : DataAccessBase where T : class where R : class where S : class
    {
        public DataAccess(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Execure stored procedures with a single object result and output parameters
        /// </summary>
        /// <param name="Procedure">Stored procedure name</param>
        /// <param name="parameter">object parameter</param>
        /// <param name="outputParams">ouput parameter</param>
        /// <returns>Return a concrete object Type<R></returns>
        public R ExecuteStoredProcedureSingleResult(string Procedure, T parameter, out S outputParams)
        {
            using (var sCon = GetConnection())
            {
                if (sCon.State == ConnectionState.Closed)
                    sCon.Open();
                using (IDbCommand cmd = sCon.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedure;

                    cmd.MapInputParameters(parameter);
                  
                    cmd.MapInputParameters(parameter);

                    IDbDataAdapter adapter = GetDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if(ds.Tables.Count > 0)
                    {
                        var data = new Helpers.DataNamesMapper<R>();
                        var result = data.Map(ds.Tables[0].Rows[0]);

                        outputParams = cmd.MapOutputParameter<S>();

                        return result;
                    }
                    else
                    {
                        outputParams = Activator.CreateInstance<S>();
                        return Activator.CreateInstance<R>();
                    }
                }
            }
        }

        /// <summary>
        /// Execure stored procedures with a collection result and output parameters
        /// </summary>
        /// <param name="Procedure">Stored procedure name</param>
        /// <param name="parameter">object parameter</param>
        /// <param name="outputParams">ouput parameter</param>
        /// <returns>Resutn a collection Type<ICollection<R></R>></returns>
        public IEnumerable<R> ExecuteStoredProcedureCollectionResult(string Procedure, T parameter, out S outputParams)
        {
            using (var sCon = GetConnection())
            {
                if (sCon.State == ConnectionState.Closed)
                    sCon.Open();
                using (IDbCommand cmd = sCon.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Procedure;

                    cmd.MapInputParameters(parameter);
                    IDbDataAdapter adapter = GetDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        var data = new Helpers.DataNamesMapper<R>();
                        var result = data.Map(ds.Tables[0]);

                        outputParams = cmd.MapOutputParameter<S>();
                        return result;
                    }
                    else
                    {
                        outputParams = Activator.CreateInstance<S>();
                        return Activator.CreateInstance<IEnumerable<R>>();
                    }
                }
            }
        }
    }
}