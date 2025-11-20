using AutoMapper;
using TestTaskPravo.Core.Models;
using TestTaskPravo.Model.Models;

namespace TestTaskPravo.Mapping;

public class ArticleMappingProfile : Profile
{
    public ArticleMappingProfile()
    {
        CreateMap<Article, ArticleDto>()
            .ForMember(
                dest => dest.Tags,
                opt => opt.MapFrom(src => 
                    src.Tags
                        .OrderBy(t => t.Order)
                        .Select(t => t.Tag.Name)
                        .ToList()
                )
            );
        
        CreateMap<ArticleCreateDto, Article>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Tags, opt => opt.Ignore());
        
        CreateMap<ArticleUpdateDto, Article>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Tags, opt => opt.Ignore());
    }
}