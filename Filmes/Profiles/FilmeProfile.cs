using AutoMapper;
using Filmes.Data.DTOs;
using Filmes.Models;

namespace Filmes.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<FilmeCadastroDTO, Filme>();
        CreateMap<FilmeAtualizacaoDTO, Filme>();
        CreateMap<Filme, FilmeAtualizacaoDTO>();
        CreateMap<Filme, FilmeLeituraDTO>();
    }
}
