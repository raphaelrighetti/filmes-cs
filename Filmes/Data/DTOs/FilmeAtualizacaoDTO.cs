using System.ComponentModel.DataAnnotations;

namespace Filmes.Data.DTOs;

public class FilmeAtualizacaoDTO
{
    public FilmeAtualizacaoDTO(string titulo, string genero, int duracao)
    {
        Titulo = titulo;
        Genero = genero;
        Duracao = duracao;
    }

    [Required(ErrorMessage = "O título do filme é obrigatório!")]
    [StringLength(500, ErrorMessage = "O título deve ter no máximo 500 caracteres!")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O gênero do filme é obrigatório!")]
    [StringLength(50, ErrorMessage = "O gênero deve ter no máximo 50 caracteres!")]
    public string Genero { get; set; }
    [Required(ErrorMessage = "A duração do filme é obrigatória!")]
    [Range(70, 600, ErrorMessage = "A duração deve ter entre 70 e 600 minutos!")]
    public int Duracao { get; set; }
}