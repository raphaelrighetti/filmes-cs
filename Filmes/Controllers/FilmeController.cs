using AutoMapper;
using Filmes.Data;
using Filmes.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Filmes.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private readonly FilmeContext context;
    private readonly IMapper mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] Filme filmeDTO)
    {
        var filme = mapper.Map<Filme>(filmeDTO);

        context.Filmes.Add(filme);
        context.SaveChanges();

        return CreatedAtAction(nameof(DetalharFilme), new { id = filme.Id }, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> ListarFilmes([FromQuery] [Range(0, int.MaxValue, ErrorMessage = "A página não pode ser negativa")] int pagina = 0,
        [FromQuery] [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")] int quantidade = 10)
    {
        if (pagina == 1) pagina = 0;

        return context.Filmes.Skip(quantidade * pagina).Take(quantidade);
    }

    [HttpGet("{id}")]
    public IActionResult? DetalharFilme(int id)
    {
        var filme = context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();

        return Ok(filme);
    }
}
