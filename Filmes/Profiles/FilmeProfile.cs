using AutoMapper;
using Filmes.Data.DTOs;
using Filmes.Models;

namespace Filmes.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<FilmeRegistroDTO, Filme>();
    }
}
