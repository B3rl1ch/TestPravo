using AutoMapper;
using TestTaskPravo.Core.Models;
using TestTaskPravo.Model.Models;

namespace TestTaskPravo.Mapping;

public class SectionMappingProfile : Profile
{
    public SectionMappingProfile()
    {
        CreateMap<Section, SectionDto>()
            .ForMember(
                dest => dest.Tags,
                opt => opt.MapFrom(src =>
                    src.Tags
                        .Select(t => t.Tag.Name)
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                        .ToList()
                )
            );
    }
}