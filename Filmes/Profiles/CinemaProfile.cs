using AutoMapper;
using Filmes.Data.DTOs;
using Filmes.Models;

namespace Filmes.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CinemaCadastroDTO, Cinema>();
        CreateMap<CinemaAtualizacaoDTO, Cinema>();
        CreateMap<Cinema, CinemaLeituraDTO>();
        CreateMap<Cinema, CinemaAtualizacaoDTO>();
    }
}
