using AutoMapper;
using DailyQuoteManager.Application.DTOs.Auth.RefreshToken;
using DailyQuoteManager.Application.DTOs.Auth.Register;
using DailyQuoteManager.Domain.Entities;

namespace DailyQuoteManager.Application.Common.Mapping.Profiles
{
    public class AuthMappingProfiles : Profile
    {

        #region Public Constructors

        public AuthMappingProfiles()
        {
            CreateMap<RegisterUserInputRequestDto, ApplicationUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower().Trim()))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
          
            CreateMap<RefreshTokenDto, RefreshToken>()
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.Expires))
                .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore()) 
                .ForMember(dest => dest.AppUserId, opt => opt.Ignore()) 
                .ForMember(dest => dest.RefreshTokenId, opt => opt.Ignore());
        }

        #endregion Public Constructors
    }
}
 