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
public class CinemaController : ControllerBase
{
    private FilmesContext context;
    private IMapper mapper;

    public CinemaController(FilmesContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpPost]
    public IActionResult CadastrarCinema([FromBody] CinemaCadastroDTO dto)
    {
        var cinema = mapper.Map<Cinema>(dto);

        context.Cinemas.Add(cinema);
        context.SaveChanges();

        var dtoLeitura = mapper.Map<CinemaLeituraDTO>(cinema);

        return CreatedAtAction(nameof(DetalharCinema), new {id = cinema.Id}, dtoLeitura);
    }

    [HttpGet]
    public IEnumerable<CinemaLeituraDTO> ListarCinemas([FromQuery][Range(0, int.MaxValue, ErrorMessage = "A página não pode ser negativa")] int pagina = 0,
        [FromQuery][Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")] int quantidade = 10)
    {
        if (pagina == 1) pagina = 0;

        return mapper.Map<List<CinemaLeituraDTO>>(context.Cinemas.Skip(quantidade * pagina).Take(quantidade));
    }

    [HttpGet("{id}")]
    public IActionResult DetalharCinema(int id)
    {
        var cinema = context.Cinemas.FirstOrDefault(c => c.Id == id);
        if (cinema == null) return NotFound();

        var dto = mapper.Map<CinemaLeituraDTO>(cinema);

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarCinema(int id, [FromBody] CinemaAtualizacaoDTO dto)
    {
        var cinema = context.Cinemas.FirstOrDefault(c => c.Id == id);
        if (cinema == null) return NotFound();

        mapper.Map(dto, cinema);
        context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizarCinemaParcialmente(int id, [FromBody] JsonPatchDocument<CinemaAtualizacaoDTO> patch)
    {
        var cinema = context.Cinemas.FirstOrDefault(c => c.Id == id);
        if (cinema == null) return NotFound();

        var cinemaParaAtualizar = mapper.Map<CinemaAtualizacaoDTO>(cinema);

        patch.ApplyTo(cinemaParaAtualizar, ModelState);

        if (!TryValidateModel(cinemaParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        mapper.Map(cinemaParaAtualizar, cinema);
        context.SaveChanges();

        return NoContent();
    }
}
