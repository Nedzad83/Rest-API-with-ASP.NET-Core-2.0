using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace RestAPI.Data
{
    public static class ParameterHandler
    {
        private static DbParameterCollection GetParameters(IDbCommand cmd)
        {
            if ((typeof(MySqlCommand)).IsInstanceOfType(cmd))
            {
                MySqlCommandBuilder.DeriveParameters((MySqlCommand)cmd);
                return (DbParameterCollection)cmd.Parameters;
            }
            else if ((typeof(SqlCommand)).IsInstanceOfType(cmd))
            {
                SqlCommandBuilder.DeriveParameters((SqlCommand)cmd);
                return (DbParameterCollection)cmd.Parameters;
            }
            else throw new Exception("Implementation not found");
        }

        public static IDbCommand MapInputParameters<T>(this IDbCommand cmd, T parameter)
        {
            DbParameterCollection parameters = GetParameters(cmd);
            PropertyInfo[] InputProperties = parameter.GetType().GetProperties();

            foreach (IDbDataParameter sqlParameter in parameters)
            {
                if (sqlParameter.Direction == ParameterDirection.Input || sqlParameter.Direction == ParameterDirection.InputOutput)
                {
                    PropertyInfo param = InputProperties.FirstOrDefault(x => x.Name == sqlParameter.ParameterName.Replace("@", ""));

                    if (param != null)
                    {
                        sqlParameter.Value = param.GetValue(parameter);
                    }
                }
            }
            return cmd;
        }

        public static S MapOutputParameter<S>(this IDbCommand cmd)
        {
            DbParameterCollection parameters = GetParameters(cmd);
            var returnType = Activator.CreateInstance<S>();
            PropertyInfo[] OutputProperties = returnType.GetType().GetProperties();

            foreach (IDbDataParameter sqlParameter in parameters)
            {
                if (sqlParameter.Direction == ParameterDirection.Output || sqlParameter.Direction == ParameterDirection.InputOutput)
                {
                    PropertyInfo param = OutputProperties.FirstOrDefault(x => x.Name == sqlParameter.ParameterName.Replace("@", ""));

                    if (param != null)
                    {
                        param.SetValue(returnType, sqlParameter.Value);
                    }
                }
            }
            return returnType;
        }

        public static object MapOutputParameter(this IDbCommand cmd)
        {
            object SingleOutput = null;
            DbParameterCollection parameters = GetParameters(cmd);

            foreach (IDbDataParameter sqlParameter in parameters)
            {
                if (sqlParameter.Direction == ParameterDirection.Output || sqlParameter.Direction == ParameterDirection.InputOutput)
                {
                    SingleOutput = sqlParameter.Value;
                    break;
                }
            }
            return SingleOutput;
        }

        public static IDbCommand MapSingleInputParameter(this IDbCommand cmd, params object[]  parameter)
        {
            int index = 0;
            DbParameterCollection parameters = GetParameters(cmd);

            foreach (IDbDataParameter sqlParameter in parameters)
            {
                if (sqlParameter.Direction == ParameterDirection.Input || sqlParameter.Direction == ParameterDirection.InputOutput)
                {
                    if (parameter.Length > index)
                    {
                        sqlParameter.Value = parameter[index];
                    }
                    else
                    {
                        break;
                    }
                }
                index++;
            }
            return cmd;
        }
        
        
    }
}
