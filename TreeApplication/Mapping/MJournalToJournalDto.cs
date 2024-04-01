using AutoMapper;
using TreeApplication.Models;
using TreeApplication.Models.ModelDto.JournalDto;

namespace TreeApplication.Mapping
{
    public class MJournalToJournalDto : Profile
    {
        public MJournalToJournalDto() 
        {
            CreateMap<MJournal, JournalItemDto>()
                .ForMember(j => j.Id, m => m.MapFrom(s => s.Id))
                .ForMember(j => j.EventId, m => m.MapFrom(s => s.EventId))
                .ForMember(j => j.CreatedAt, m => m.MapFrom(s => s.CreatedAt));
        }
    }
}
