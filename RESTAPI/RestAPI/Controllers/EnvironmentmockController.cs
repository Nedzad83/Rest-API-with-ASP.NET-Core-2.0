using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using RestAPI.Data.Models;
using RestAPI.Data.Repositories;
using RestAPI.Helpers;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    public class EnvironmentmockController : Controller
    {

        #region Initialize
        private readonly EnvironmentMockupRepository _mockRepository;
        private readonly IConfiguration _configuration;

        public EnvironmentmockController(EnvironmentMockupRepository mockRepository, IConfiguration configuration)
        {
            _mockRepository = mockRepository;
            _configuration = configuration;
        }
        #endregion

        #region GetMockDeployments
        [HttpGet("Mockenvironments")]
        public IEnumerable<EnvDeployment> GetMockDeployments()
        {
            IEnumerable<EnvDeployment> result = _mockRepository.Get();
            return result;
        }
        #endregion

        #region GetTableMock
        [HttpGet("MockTable")]
        [Produces("text/html")]
        [ProducesResponseType(404)]
        public ContentResult GetMockTable()
        {
            IEnumerable<EnvDeployment> result = _mockRepository.Get();
            StringBuilder sb = GenerateGrid.GenerateDeployment(result);
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = sb.ToString()
            };
        }
        #endregion
    }
}