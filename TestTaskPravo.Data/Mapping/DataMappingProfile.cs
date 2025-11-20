using AutoMapper;
using TestTaskPravo.Core.Models;
using TestTaskPravo.Data.Models;

namespace TestTaskPravo.Data.Mapping;

public class DataMappingProfile : Profile
{
    public DataMappingProfile()
    {
        CreateMap<ArticleDbo, Article>().ReverseMap();
        CreateMap<ArticleTagDbo, ArticleTag>();
        CreateMap<ArticleTag, ArticleTagDbo>()
            .ForMember(src => src.Article, opt => opt.Ignore())
            .ForMember(src => src.Tag, opt => opt.Ignore());
        
        CreateMap<SectionDbo, Section>().ReverseMap();
        CreateMap<SectionTagDbo, SectionTag>().ReverseMap();
        CreateMap<SectionTag, SectionTagDbo>()
            .ForMember(src => src.Section, opt => opt.Ignore())
            .ForMember(src => src.Tag, opt => opt.Ignore());
        
        CreateMap<TagDbo, Tag>().ReverseMap();
    }
}