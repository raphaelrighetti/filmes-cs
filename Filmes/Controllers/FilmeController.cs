using Filmes.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Filmes.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private readonly static List<Filme> filmes = new();
    private static int id = 0;

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] Filme filme)
    {
        id++;

        filme.Id = id;
        filmes.Add(filme);

        return CreatedAtAction(nameof(DetalharFilme), new { id = filme.Id }, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> ListarFilmes([FromQuery] [Range(0, int.MaxValue, ErrorMessage = "A página não pode ser negativa")] int pagina = 0,
        [FromQuery] [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")] int quantidade = 10)
    {
        if (pagina == 1) pagina = 0;

        return filmes.Skip(quantidade * pagina).Take(quantidade);
    }

    [HttpGet("{id}")]
    public IActionResult? DetalharFilme(int id)
    {
        var filme = filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();

        return Ok(filme);
    }
}
