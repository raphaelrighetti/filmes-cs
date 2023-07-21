﻿using System.ComponentModel.DataAnnotations;

namespace Filmes.Data.DTOs;

public record CinemaCadastroDTO
{
    public CinemaCadastroDTO(string nome)
    {
        Nome = nome;
    }

    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Nome { get; init; }
}
