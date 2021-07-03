using System.Collections.Generic;
using AutoMapper;
using Demo.ApplicationProcess.Domain.Dtos;
using Demo.ApplicationProcess.Domain.Entities;
using Demo.ApplicationProcess.Domain.Mappers.Profiles;
using Demo.ApplicationProcess.Domain.Models;

namespace Demo.ApplicationProcess.Domain.Mappers
{
    public static class NewsMapper
    {

        /// <summary>
        /// Mapper instance
        /// </summary>
        private static readonly IMapper Mapper;

        static NewsMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<NewsProfile>();
            });

            Mapper = config.CreateMapper();
        }

        /// <summary>
        /// Entities to DTO
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static IEnumerable<NewsDto> ToCollection(this IEnumerable<NewsEntity> entities)
        {
            return Mapper.Map<IEnumerable<NewsDto>>(entities);
        }

        /// <summary>
        /// Form to Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static NewsEntity ToEntity(this NewsModel model)
        {
            return Mapper.Map<NewsEntity>(model);
        }

        /// <summary>
        /// Entity to DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static NewsDto ToDto(this NewsEntity entity)
        {
            return Mapper.Map<NewsDto>(entity);
        }

    }
}
