using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using product_api.Models;

namespace product_api.Controllers
{
    public class ProductsController : Controller
{
    static List<Product> _products = new List<Product>();
    static ProductsController()
    {
        _products.Add(new Product { Id = 1, Name = "Tea", Price = 12.56m, UnitOfMeasure = "lbs", Category = "Dry Food" });
        _products.Add(new Product { Id = 2, Name = "Banana", Price = 6.49m, UnitOfMeasure = "lbs", Category = "Fruits" });
        _products.Add(new Product { Id = 3, Name = "Apple", Price = 2.99m, UnitOfMeasure = "lbs", Category = "Fruits" });
        _products.Add(new Product { Id = 4, Name = "Ocra", Price = 4.98m, UnitOfMeasure = "lbs", Category = "Vegetable" });
        _products.Add(new Product { Id = 5, Name = "Broccoli", Price = 3.79m, UnitOfMeasure = "lbs", Category = "Vegetable" });
        _products.Add(new Product { Id = 6, Name = "Celery", Price = 3.19m, UnitOfMeasure = "lbs", Category = "Vegetable" });
    }

    [Route("Products")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
    public IActionResult GetProducts()
    {
        List<Product> retVal = new List<Product>();

        lock (_products)
        {
            retVal.AddRange(_products);
        }

        return Ok(retVal);
    }

    [Route("Products/{id}")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
    public IActionResult GetProductsById(int id)
    {
        //lock (_products)
        //{
        //    var product = _products.Where(x => x.Id == id).FirstOrDefault();
        //    return Ok(product);
        //}
        return Ok(new Product { Id = 3, Name = "Apple", Price = 2.99m, UnitOfMeasure = "lbs", Category = "Fruits" });
    }

    [Route("Products")]
    [HttpPost]
    public IActionResult Post([FromBody]Product product)
    {
        product.Id = _products.Count() + 100;
        _products.Add(product);
        return Ok(product);
    }

    [Route("Products")]
    [HttpPut]
    public IActionResult Put([FromBody]Product product)
    {
        var productToUpdate = _products.Where(x => x.Id == product.Id).FirstOrDefault();
        if (productToUpdate == null)
        {
            return NotFound();
        }
        productToUpdate.Name = product.Name;
        productToUpdate.Category = product.Category;
        productToUpdate.Price = product.Price;
        productToUpdate.UnitOfMeasure = product.UnitOfMeasure;

        return Ok(productToUpdate);
    }
}
}
