using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestAPI.Data.Models;
using RestAPI.Data.Repositories;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductsRepository _repository;
        private readonly IConfiguration _configuration;

        public ProductsController(ProductsRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        #region snippet_Get
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var co = _configuration.GetSection("ConnectionStrings")["DefaultConnection"];
            var c2 = _configuration.GetConnectionString("DefaultConnection");
            return _repository.GetProducts();
        }
        #endregion

        #region snippet_GetById
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            if (!_repository.TryGetProduct(id, out var product))
            {
                return NotFound();
            }

            return Ok(product);
        }
        #endregion

        #region snippet_CreateAsync
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddProductAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        #endregion
    }

}
