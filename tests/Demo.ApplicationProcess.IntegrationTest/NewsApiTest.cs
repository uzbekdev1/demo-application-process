using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Demo.ApplicationProcess.Api;
using Demo.ApplicationProcess.Domain.Dtos;
using Demo.ApplicationProcess.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Demo.ApplicationProcess.IntegrationTest
{
    /// <summary>
    /// News api
    /// </summary>
    public class NewsApiTest
    {
        /// <summary>
        /// Client factory
        /// </summary>
        private readonly HttpClient _client;
        private readonly CancellationTokenSource _source;

        private static NewsModel MakeMockModel(string name)
        {
            return new()
            {
                Title = name,
                Description = name
            };
        }

        private static NewsDto MakeMockDto(string name)
        {
            return new()
            {
                Title = name,
                Description = name,
                PostDate = DateTime.Now
            };
        }

        public NewsApiTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            _client = server.CreateClient();

            _source = new CancellationTokenSource();
        }

        // POST: api/News
        [Theory]
        [InlineData("AAAAAA")]
        [Order(1)]
        public async Task Add_Many(string name)
        {
            // Arrange
            var model = MakeMockModel(name);
            var token = CancellationToken.None;

            // Act
            var request = await _client.PostAsJsonAsync("/api/News", model, token);

            // Assert
            Assert.Equal(HttpStatusCode.Created, request.StatusCode);
            var response = await request.Content.ReadFromJsonAsync<NewsDto>(cancellationToken: token);
            Assert.NotNull(response);
            Assert.Equal(model.Title, response.Title);
            Assert.Equal(model.Description, response.Description);
        }


        // GET: api/News/1
        [Fact]
        [Order(2)]
        public async Task Get_By_Id()
        {
            // Arrange
            var dto = MakeMockDto("AAAAAA");
            var token = _source.Token;
            var id = 1;
            await Add_Many("AAAAAA");

            // Act 
            var item = await _client.GetFromJsonAsync<NewsDto>($"/api/News/{id}", token);

            // Assert
            Assert.NotNull(item);
            Assert.Equal(dto.Title, item.Title);
            Assert.Equal(dto.Description, item.Description);
        }

        // GET: api/News
        [Fact]
        [Order(3)]
        public async Task Get_All()
        {
            // Arrange
            var list = new List<NewsDto>
            {
                MakeMockDto("AAAAAA")
            };
            var token = _source.Token;
            await Add_Many("AAAAAA");

            // Act 
            var items = await _client.GetFromJsonAsync<List<NewsDto>>("/api/News?Sort=Id&Order=Desc", token);

            // Assert
            Assert.NotNull(items);
            Assert.Equal(list[0].Title, items[0].Title);
            Assert.Equal(list[0].Description, items[0].Description);
        }

        // PUT: api/News
        [Theory]
        [InlineData(1, "AAAAAA")]
        [Order(4)]
        public async Task Update_One(int id, string name)
        {
            // Arrange
            var model = MakeMockModel(name);
            var token = CancellationToken.None;

            // Act
            var request = await _client.PutAsJsonAsync($"/api/News/{id}", model, token);

            // Assert
            Assert.True(request.IsSuccessStatusCode);
        }

        // DELETE: api/News
        [Theory]
        [InlineData(1)]
        [Order(5)]
        public async Task Delete_One(int id)
        {
            // Arrange
            var token = CancellationToken.None;

            // Act
            var request = await _client.DeleteAsync($"/api/News/{id}", token);

            // Assert
            Assert.True(request.IsSuccessStatusCode);
        }

    }
}
