using AutoMapper;
using ContatosApi.Data.Dtos;
using ContatosDomain.Entidades;

namespace ContatosApi.Profiles
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<CreateContatoDto, Contato>();
            CreateMap<UpdateContatoDto, Contato>();
            CreateMap<Contato, UpdateContatoDto>();
            CreateMap<Contato, ReadContatoDto>();
            CreateMap<ReadContatoDto, Contato>();
        }
    }
}
