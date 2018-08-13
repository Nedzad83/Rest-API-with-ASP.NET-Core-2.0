using RestAPI.Data.Models;
using RestAPI.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPI.Data.Services
{
    public class AttributeService
    {
        #region Fields 

        private readonly AttributeRepository _attributeRepository;
        private readonly string _stringConnection;

        #endregion

        #region  C'tors 

        public AttributeService(string stringConnection, AttributeRepository attributeRepository)
        {
            _stringConnection = stringConnection;
            _attributeRepository = attributeRepository;
        }

        #endregion

        #region Methods 

        public Task<int> AddAttribute(string env, string attribute, string name)
        {
            return Task.FromResult(_attributeRepository.AddAttribute(env, attribute, name));
        }

        public async Task<dynamic> AddAttributeAsync(string env, string attribute, string name)
        {
            return await AddAttribute(env, attribute, name);
        }

        public Task<int> UpdateAttribute(int id, string attributeValue)
        {
            return Task.FromResult(_attributeRepository.UpdateAttribute(id, attributeValue));
        }

        public async Task<dynamic> UpdateAttributeAsync(int id, string attributeValue)
        {
            return await UpdateAttribute(id, attributeValue);
        }

        public Task<int> DeleteAttribute(string num)
        {
            return Task.FromResult(_attributeRepository.DeleteAttribute(num));
        }

        public async Task<dynamic> DeleteAttributeAsync(string num)
        {
            return await DeleteAttribute(num);
        }

        public Task<IEnumerable<AttributeModel>> GetAttribute(string env)
        {
            return Task.FromResult(_attributeRepository.GetEnvAttributes(env));
        }

        public async Task<dynamic> GetAttributeAsync(string env)
        {
            return await GetAttribute(env);
        }

        #endregion
    }
}
