using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.ApplicationProcess.Domain.Entities;

namespace Demo.ApplicationProcess.Domain.Infrastructure
{
    /// <summary>
    /// News impl
    /// </summary>
    public interface INewsRepository
    {
        /// <summary>
        /// Get News
        /// </summary>
        /// <param name="page"></param>
        /// <param name="token"></param>
        /// <param name="search"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<List<NewsEntity>> GetAllAsync(string search = "", string sort = "Id", string order = "Asc", int size = 10, int page = 1);

        /// <summary>
        /// Get News by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<NewsEntity> GetByIdAsync(int id);

        /// <summary>
        /// Add News
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<NewsEntity> AddAsync(NewsEntity entity);

        /// <summary>
        /// Update News
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateAsync(NewsEntity entity);

        /// <summary>
        /// Update News
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteAsync(NewsEntity entity);

    }
}
