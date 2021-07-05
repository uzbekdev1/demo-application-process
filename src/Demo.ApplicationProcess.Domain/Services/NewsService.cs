using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.ApplicationProcess.Domain.Mappers;
using Demo.ApplicationProcess.Domain.Dtos;
using Demo.ApplicationProcess.Domain.Infrastructure;
using Demo.ApplicationProcess.Domain.Models;

namespace Demo.ApplicationProcess.Domain.Services
{
    /// <summary>
    /// News service
    /// </summary>
    public class NewsService : INewsService
    {

        /// <summary>
        /// News repo instance
        /// </summary>
        private readonly INewsRepository _repository;

        public NewsService(INewsRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all News
        /// </summary>
        /// <param name="search"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync(string search, string sort, string order, int size, int page)
        {
            var source = await _repository.GetAllAsync(search, sort, order, size, page);

            return source.ToCollection();
        }

        /// <summary>
        /// Get News by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<NewsDto> GetNewsByIdAsync(int id)
        {
            var source = await _repository.GetByIdAsync(id);

            return source.ToDto();
        }

        /// <summary>
        /// Add News 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<NewsDto> AddNewsAsync(NewsModel model)
        {
            var entity = model.ToEntity();

            entity.PostDate = DateTime.Now;

            await _repository.AddAsync(entity);

            return await GetNewsByIdAsync(entity.Id);
        }

        /// <summary>
        /// Update news
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateNewsAsync(int id, NewsModel model)
        {
            var entity = model.ToEntity();

            entity.Id = id;
            entity.PostDate = DateTime.Now;

            await _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// Delete news
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteNewsAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            await _repository.DeleteAsync(entity);
        }

    }
}