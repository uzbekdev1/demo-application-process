using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Demo.ApplicationProcess.Domain.Entities;
using Demo.ApplicationProcess.Domain.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Demo.ApplicationProcess.UnitTest
{
    /// <summary>
    /// News repo
    /// </summary>
    [TestClass]
    public class NewsRepositoryTest
    {

        private readonly CancellationTokenSource _source;

        private static NewsEntity MakeMockEntity(int id, string name)
        {
            return new()
            {
                Id = id,
                Title = name,
                Description = name,
                PostDate = DateTime.Now,
                Price = 100m
            };
        }

        public NewsRepositoryTest()
        {
            _source = new CancellationTokenSource();
        }

        /// <summary>
        /// Add News
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Add_One()
        {
            // Arrange
            var token = _source.Token;
            var entity = MakeMockEntity(1, "AAAAAA");

            // Act 
            var repo = new Mock<INewsRepository>();
            repo.Setup(c => c.AddAsync(entity, token)).ReturnsAsync(entity);

            // Assert
            var item = await repo.Object.AddAsync(entity, token);
            Assert.IsNotNull(item);
            Assert.AreEqual(entity.Title, item.Title);
            Assert.AreEqual(entity.Description, item.Description);
            Assert.AreEqual(entity.Price, item.Price);
        }


        /// <summary>
        /// Get News
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Get_All()
        {
            // Arrange
            var token = _source.Token;
            var entities = new List<NewsEntity>
            {
                MakeMockEntity(1, "AAAAAA")
            };

            // Act 
            var repo = new Mock<INewsRepository>();
            repo.Setup(c => c.GetAllAsync("", "Id", "Desc", 10, 1, token)).ReturnsAsync(entities);

            // Assert
            var items = await repo.Object.GetAllAsync("", "Id", "Desc", 10, 1, token);
            Assert.IsNotNull(items);
            Assert.AreEqual(entities.Count, items.Count);
            Assert.AreEqual(entities[0].Title, items[0].Title);
            Assert.AreEqual(entities[0].Description, items[0].Description);
            Assert.AreEqual(entities[0].Price, items[0].Price);
        }

        /// <summary>
        /// Get News by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [TestMethod]
        [DataRow(1, "AAAAAA")]
        public async Task Get_By_Id(int id, string name)
        {
            // Arrange
            var token = _source.Token;
            var entity = MakeMockEntity(id, name);

            // Act 
            var repo = new Mock<INewsRepository>();
            repo.Setup(c => c.GetByIdAsync(id, token)).ReturnsAsync(entity);

            // Assert
            var item = await repo.Object.GetByIdAsync(id, token);
            Assert.IsNotNull(item);
            Assert.AreEqual(entity.Title, item.Title);
            Assert.AreEqual(entity.Description, item.Description);
            Assert.AreEqual(entity.Price, item.Price);
        }


        /// <summary>
        /// Update News
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [DataRow(1)]
        public async Task Update_One(int id)
        {
            // Arrange
            var token = _source.Token;
            var entity = MakeMockEntity(id, "AAAAAA");

            // Act 
            var repo = new Mock<INewsRepository>();
            repo.Setup(c => c.UpdateAsync(entity, token));

            // Assert
            await repo.Object.UpdateAsync(entity, token);
        }

        /// <summary>
        /// Delete News
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [DataRow(1)]
        public async Task Delete_One(int id)
        {
            // Arrange
            var token = _source.Token;
            var entity = MakeMockEntity(id, "AAAAAA");

            // Act 
            var repo = new Mock<INewsRepository>();
            repo.Setup(c => c.UpdateAsync(entity, token));

            // Assert
            await repo.Object.DeleteAsync(entity, token);
        }
    }
}