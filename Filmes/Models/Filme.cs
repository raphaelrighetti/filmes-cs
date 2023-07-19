using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Filmes.Models;

public class Filme
{
    public Filme()
    {
    }

    public Filme(string titulo, string genero, int duracao)
    {
        Titulo = titulo;
        Genero = genero;
        Duracao = duracao;
    }

    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(500)]
    public string Titulo { get; set; }
    [Required]
    [StringLength(50)]
    public string Genero { get; set; }
    [Required]
    [Range(70, 600)]
    public int Duracao { get; set; }

    public override string ToString()
    {
        return $"Título: {Titulo}\n" +
            $"Gênero: {Genero}\n" +
            $"Duração: {Duracao}";
    }
}
