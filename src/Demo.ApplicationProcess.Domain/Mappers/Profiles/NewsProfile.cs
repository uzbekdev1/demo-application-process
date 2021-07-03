using AutoMapper;
using Demo.ApplicationProcess.Domain.Dtos;
using Demo.ApplicationProcess.Domain.Entities;
using Demo.ApplicationProcess.Domain.Models;

namespace Demo.ApplicationProcess.Domain.Mappers.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {  

            // News table to form mapping
            CreateMap<NewsEntity, NewsModel>(MemberList.Destination)
                .ReverseMap();

            // News table to listing mapping
            CreateMap<NewsEntity, NewsDto>(MemberList.Destination)
                .ReverseMap();

        }
    }
}
