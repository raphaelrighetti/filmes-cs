using Filmes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private static List<Filme> filmes = new();

    [HttpPost]
    public void AdicionaFilme([FromBody] Filme filme)
    {
        filmes.Add(filme);

        Console.WriteLine(filmes[filmes.IndexOf(filme)]);
    }
}
