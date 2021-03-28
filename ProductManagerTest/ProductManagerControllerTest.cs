using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProductManager;
using ProductManager.Controllers;
using Xunit;

namespace ProductManagerTest
{
    public class ProductSeedDataFixture 
    {
        public List<Product> cacheProducts { get; private set; }
        public MockDataBase dataBase { get; private set; }
        public ProductManagerController Controller { get; private set; }


        public ProductSeedDataFixture()
        {
            Product produt_pomme = new Product()
            {
                ProductId = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                Name = "Pomme",
                StartDate = new DateTime(2021, 01, 11, 3, 5, 6),
                EndDate = new DateTime(2023, 05, 11, 3, 5, 6)
            };
            Product product_apple = new Product
            {
                ProductId = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                Name = "Apple",
                StartDate = new DateTime(2011, 03, 11, 3, 5, 6),
                EndDate = new DateTime(2013, 05, 11, 3, 8, 1)
            };
            Product product_orange = new Product()
            {
                ProductId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                Name = "Orange",
                StartDate = new DateTime(2001, 05, 11, 3, 5, 6),
                EndDate = new DateTime(2003, 05, 11, 3, 5, 6)
            };

            cacheProducts = new List<Product>() { produt_pomme, product_apple, product_orange };


            dataBase = new MockDataBase();
            dataBase.Products = new List<Product>(new []{ produt_pomme, product_apple, product_orange });

            DataBaseRespository dataBaseRespository = new DataBaseRespository(dataBase);
            ProductModule productModule = new ProductModule(dataBaseRespository);

            Controller = new ProductManagerController(productModule);

        }

    }

    public class ProductManagerControllerTest : IClassFixture<ProductSeedDataFixture>
    {

        ProductSeedDataFixture _fixture;

        public ProductManagerControllerTest(ProductSeedDataFixture fixture)
        {

            this._fixture = fixture;

        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult =  _fixture.Controller.GetProducts();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<Product>>>(okResult);

        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult =  _fixture.Controller.GetProducts();

            // Assert
            var items = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void Get_WhenCalled_AllItemsTheExcpectedOnes()
        {
            // Act
            var okResult =  _fixture.Controller.GetProducts();

            // Assert
            var items = Assert.IsType<List<Product>>(okResult.Value);
            for (int i = 0; i < _fixture.cacheProducts.Count; i++)
            {
                Assert.Equal(_fixture.cacheProducts[i].Name, items[i].Name);
                Assert.Equal(_fixture.cacheProducts[i].StartDate, items[i].StartDate);
                Assert.Equal(_fixture.cacheProducts[i].EndDate, items[i].EndDate);
            }
        }

        [Fact]
        public void Post_WhenCalled_SavesTheNewObjectInTheDB()
        {
            Product produt_tomate = new Product()
            {
                Name = "tomate",
                StartDate = new DateTime(2021, 01, 11, 3, 5, 6),
                EndDate = new DateTime(4023, 05, 11, 3, 5, 6)
            };
            // Act
            var okResult =  _fixture.Controller.PostProduct(produt_tomate);// as ActionResult<IEnumerable<Product>>;



            // Assert
            var dbItem = _fixture.dataBase.Products.LastOrDefault();
            Assert.Equal(dbItem.Name,produt_tomate.Name);
            Assert.Equal(dbItem.StartDate,produt_tomate.StartDate);
            Assert.Equal(dbItem.EndDate,produt_tomate.EndDate);
        }
    }
}