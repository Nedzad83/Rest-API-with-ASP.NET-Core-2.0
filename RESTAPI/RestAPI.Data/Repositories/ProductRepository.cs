using Microsoft.EntityFrameworkCore;
using RestAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Data.Repositories
{
    public class ProductsRepository
    {
        private readonly ProductContext _context;

        public ProductsRepository(ProductContext context)
        {
            _context = context;
            if (context.Products.Count() == 0)
            {
                _context.Products.AddRange(
                new Product
                {
                    Name = "Product 1",
                    Description = "Description of product 1"
                },
                new Product
                {
                    Name = "Product 2",
                    Description = "Description of product 2"
                });
                        _context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        public bool TryGetProduct(int id, out Product product)
        {
            product = _context.Products.Find(id);

            return (product != null);
        }

        public async Task<int> AddProductAsync(Product product)
        {
            int rowsAffected = 0;

            _context.Products.Add(product);
            rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected;
        }
    }
}
