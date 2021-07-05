using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.ApplicationProcess.Data.Extensions;
using Demo.ApplicationProcess.Domain.Entities;
using Demo.ApplicationProcess.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Demo.ApplicationProcess.Data.Repositories
{
    /// <summary>
    /// News repository
    /// </summary>
    public class NewsRepository : INewsRepository
    {

        /// <summary>
        ///  db instance
        /// </summary>
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

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
        public Task<List<NewsEntity>> GetAllAsync(string search = "", string sort = "Id", string order = "Asc", int size = 10, int page = 1) => _context.News
            .Where(w => w.Title.Contains(search) || w.Description.Contains(search))
            .OrderByFx(sort, order)
            .Skip((page - 1) * size).Take(size)
            .ToListAsync();

        /// <summary>
        /// Get News by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<NewsEntity> GetByIdAsync(int id) => _context.News.FirstOrDefaultAsync(a => a.Id == id);

        /// <summary>
        /// Add News
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<NewsEntity> AddAsync(NewsEntity entity)
        {
            await _context.News.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Update news
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateAsync(NewsEntity entity)
        {
            _context.News.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete news
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteAsync(NewsEntity entity)
        {
            _context.News.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
