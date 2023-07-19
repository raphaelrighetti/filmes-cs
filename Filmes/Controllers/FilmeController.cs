using AutoMapper;
using Filmes.Data;
using Filmes.Data.DTOs;
using Filmes.Models;
using Microsoft.AspNetCore.JsonPatch;
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
    public IActionResult AdicionarFilme([FromBody] Filme dto)
    {
        var filme = mapper.Map<Filme>(dto);

        context.Filmes.Add(filme);
        context.SaveChanges();

        return CreatedAtAction(nameof(DetalharFilme), new { id = filme.Id }, filme);
    }

    [HttpGet]
    public IEnumerable<FilmeLeituraDTO> ListarFilmes([FromQuery] [Range(0, int.MaxValue, ErrorMessage = "A página não pode ser negativa")] int pagina = 0,
        [FromQuery] [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")] int quantidade = 10)
    {
        if (pagina == 1) pagina = 0;

        return mapper.Map<List<FilmeLeituraDTO>>(context.Filmes.Skip(quantidade * pagina).Take(quantidade));
    }

    [HttpGet("{id}")]
    public IActionResult DetalharFilme(int id)
    {
        var filme = context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        var dto = mapper.Map<FilmeLeituraDTO>(filme);

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] FilmeAtualizacaoDTO dto)
    {
        var filme = context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        mapper.Map(dto, filme);
        context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParcialmente(int id, [FromBody] JsonPatchDocument<FilmeAtualizacaoDTO> patch)
    {
        var filme = context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        var filmeParaAtualizar = mapper.Map<FilmeAtualizacaoDTO>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        mapper.Map(filmeParaAtualizar, filme);
        context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        var filme = context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        context.Remove(filme);
        context.SaveChanges();

        return NoContent();
    }
}
