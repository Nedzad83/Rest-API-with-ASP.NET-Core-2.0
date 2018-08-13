using RestAPI.Data.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using RestAPI.Data.Models;

namespace RestAPI.Data.DataAccess
{
    public static class StoredProcedureResolver
    {
        private static Dictionary<Type, string> _settersDictionary = new Dictionary<Type, string> {
        };

        private static Dictionary<Type, string> _gettersDictionary = new Dictionary<Type, string> {
        };

        public static string GetProcedureName<T>(ProcedureType procedureType)
        {
            string result;

            switch (procedureType)
            {
                case ProcedureType.Insert:
                    result = _settersDictionary[typeof(T)];
                    break;
                default:
                    result = _gettersDictionary[typeof(T)];
                    break;
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                throw new Exception("Procedure name was not found for this type.");
            }

            return result;
        }
    }
}
