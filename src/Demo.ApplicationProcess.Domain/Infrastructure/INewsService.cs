using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.ApplicationProcess.Domain.Dtos;
using Demo.ApplicationProcess.Domain.Models;

namespace Demo.ApplicationProcess.Domain.Infrastructure
{
    /// <summary>
    /// Front-end impl
    /// </summary>
    public interface INewsService
    {

        /// <summary>
        /// Get News
        /// </summary>
        /// <param name="search"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<NewsDto>> GetAllNewsAsync(string search = "", string sort = "Id", string order = "Asc", int size = 10, int page = 1 );

        /// <summary>
        /// Get News by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<NewsDto> GetNewsByIdAsync(int id);

        /// <summary>
        /// Add News 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<NewsDto> AddNewsAsync(NewsModel model);

        /// <summary>
        /// Update News 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateNewsAsync(int id, NewsModel model);

        /// <summary>
        /// Update News 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteNewsAsync(int id);

    }
}