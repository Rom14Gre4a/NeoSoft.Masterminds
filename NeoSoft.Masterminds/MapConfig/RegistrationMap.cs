using AutoMapper;
using NeoSoft.Masterminds.Domain.Models.Models.Auth;
using NeoSoft.Masterminds.Models.Registration;

namespace NeoSoft.Masterminds.MapConfig
{
    public class RegistrationMap : Profile
    {
        public RegistrationMap()
        {
            CreateMap<UserRegistration, IncomUserRegistration>();   
            CreateMap<MentorRegistration, IncomMentorRegistration>();
        }
    }


}
