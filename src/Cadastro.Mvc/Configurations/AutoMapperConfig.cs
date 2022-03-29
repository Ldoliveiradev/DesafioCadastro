using AutoMapper;
using Cadastro.Domain.Models;
using Cadastro.Mvc.ViewModels;

namespace Cadastro.Mvc.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UsuarioViewModel, Usuario>();

            CreateMap<Usuario, UsuarioViewModel>().ForMember(d => d.Email, o => o.MapFrom(x => x.Email.Endereco));
        }
    }
}
