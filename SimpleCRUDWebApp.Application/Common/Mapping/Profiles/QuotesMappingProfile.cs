using AutoMapper;
using DailyQuoteManager.Application.DTOs.Quote.Quotes;
using DailyQuoteManager.Client.DTOs.Quotes;
using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Enums;

namespace DailyQuoteManager.Application.Common.Mapping.Profiles
{
    public class QuotesMappingProfile : Profile
    {
        public QuotesMappingProfile()
        {
            CreateMap<QuotesOutputDto, QuotesInputReqDto>();

            CreateMap<Quotes, QuotesOutputDto>()
                .ForMember(dest => dest.IsPublic, opt => opt.Ignore());

            CreateMap<Quotes, QuotesInputReqDto>();
        }
    }
}
