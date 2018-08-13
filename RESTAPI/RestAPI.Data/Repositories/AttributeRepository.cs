using RestAPI.Data.Constants;
using RestAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPI.Data.Repositories
{
    public class AttributeRepository
    {
        private readonly string _stringConnection;
        public AttributeRepository(string connection)
        {
            _stringConnection = connection;
        }
        
        public IEnumerable<AttributeModel> GetEnvAttributes(string env)
        {
            var dataAccess = new DataAccess<AttributeModel>(_stringConnection, ConnectionType.MySql);
            return dataAccess.ExecuteStoredProcedureCollectionResult(StoredProcedures.getAttribute, env);
        }

        public int DeleteAttribute(string num)
        {
            var dataAccess = new DataAccess<AttributeModel>(_stringConnection, ConnectionType.MySql);
            return dataAccess.ExecuteStoredProcedure(StoredProcedures.delAttribute, num);
        }

        public int AddAttribute(string env, string attribute, string name)
        {
            var dataAccess = new DataAccess<AttributeModel>(_stringConnection, ConnectionType.MySql);
            return dataAccess.ExecuteStoredProcedure(StoredProcedures.addAttribute, env, attribute, name);
        }
        
        public int UpdateAttribute(int id, string value)
        {
            var dataAccess = new DataAccess<AttributeModel>(_stringConnection, ConnectionType.MySql);
            return dataAccess.ExecuteStoredProcedure(StoredProcedures.updAttribute, id, value);
        }
    }
}
