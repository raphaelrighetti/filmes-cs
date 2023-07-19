using System.ComponentModel.DataAnnotations;

namespace Filmes.Data.DTOs;

public record FilmeLeituraDTO
{
    public FilmeLeituraDTO()
    {
        
    }

    public FilmeLeituraDTO(int id, string titulo, string genero, int duracao)
    {
        Id = id;
        Titulo = titulo;
        Genero = genero;
        Duracao = duracao;
    }

    public int Id { get; init; }
    public string Titulo { get; init; }
    public string Genero { get; init; }
    public int Duracao { get; init; }
    public DateTime DataDaConsulta => DateTime.Now;
}
