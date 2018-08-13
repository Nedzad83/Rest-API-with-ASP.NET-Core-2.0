using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Data.Models;
using RestAPI.Data.Repositories;
using RestAPI.Helpers;
using Newtonsoft.Json;
using RestAPI.Data.Services;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    public class AttributeController : Controller
    {
        private AttributeService _attributeService;

        public AttributeController(AttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpGet("list/{env}")]
        public IEnumerable<AttributeModel> GetEnvAttributes(string env)
        {
            return _attributeService.GetAttribute(env).Result;
        }

        [HttpDelete("deleteAtt/{num}")]
        public IActionResult DeleteAttribute(string num)
        {
            return _attributeService.DeleteAttribute(num).Result.GetStatusResult();
        }

        [HttpPost("addAttrib/{env}/{attribute}/{name}")]
        public IActionResult AddAttribute(string env, string attribute, string name)
        {
            return _attributeService.AddAttribute(env, attribute, name).Result.GetStatusResult();
        }

        [HttpPost("UpdateAttribute")]
        public IActionResult UpdateAttribute([FromBody]AttributeModel attribute)
        {
            return _attributeService.UpdateAttribute(attribute.Id, attribute.AttributeValue).Result.GetStatusResult();
        }
    }
}