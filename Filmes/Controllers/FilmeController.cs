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
    private readonly FilmesContext context;
    private readonly IMapper mapper;

    public FilmeController(FilmesContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Action responsável por cadastrar um filme no sistema.
    /// </summary>
    /// <param name="dto">Objeto com as informações do filme a ser cadastrado.</param>
    /// <returns>IActionResult <see cref="IActionResult"/></returns>
    /// <response code="201">Se o cadastro do filme for feito com sucesso.</response>
    /// <response code="400">Se o objeto recebido não estiver válido.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarFilme([FromBody] FilmeCadastroDTO dto)
    {
        var filme = mapper.Map<Filme>(dto);

        context.Filmes.Add(filme);
        context.SaveChanges();

        return CreatedAtAction(nameof(DetalharFilme), new { id = filme.Id }, filme);
    }

    /// <summary>
    /// Action responsável por listar os filmes cadastrados aplicando paginação.
    /// </summary>
    /// <param name="pagina">Um inteiro que representa o número da página.</param>
    /// <param name="quantidade">Um inteiro que representa a quantidade de filmes em cada página.</param>
    /// <returns>IEnumerable<see cref="IEnumerable{T}"/></returns>
    /// <response code="200">Se a API retornar uma resposta.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<FilmeLeituraDTO> ListarFilmes([FromQuery] [Range(0, int.MaxValue, ErrorMessage = "A página não pode ser negativa")] int pagina = 0,
        [FromQuery] [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")] int quantidade = 10)
    {
        if (pagina == 1) pagina = 0;

        return mapper.Map<List<FilmeLeituraDTO>>(context.Filmes.Skip(quantidade * pagina).Take(quantidade));
    }

    /// <summary>
    /// Actions responsável por retornar um filme específico a partir do seu id.
    /// </summary>
    /// <param name="id">Um inteiro que representa o id do filme desejado.</param>
    /// <returns>IActionResult <see cref="IActionResult"/></returns>
    /// <response code="200">Se o filme com o id passado for encontrado.</response>
    /// <response code="404">Se nenhum filme com o id passado for encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DetalharFilme(int id)
    {
        var filme = context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        var dto = mapper.Map<FilmeLeituraDTO>(filme);

        return Ok(dto);
    }

    /// <summary>
    /// Action responsável por atualizar todas as informações de um filme.
    /// </summary>
    /// <param name="id">Um inteiro que representa o id do filme a ser atualizado.</param>
    /// <param name="dto">Objeto contendo as informações novas do filme.</param>
    /// <returns>IActionResult <see cref="IActionResult"/></returns>
    /// <response code="204">Se o filme for atualizado com sucesso.</response>
    /// <response code="404">Se o filme com o id passado não for encontrado.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizarFilme(int id, [FromBody] FilmeAtualizacaoDTO dto)
    {
        var filme = context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        mapper.Map(dto, filme);
        context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Action responsável por atualizar parcialmente um filme no sistema.
    /// </summary>
    /// <param name="id">Um inteiro que representa o id do filme a ser atualizado.</param>
    /// <param name="patch">Objeto de patch seguindo o padrão dos objetos JSON de PATCH.</param>
    /// <returns>IActionResult <see cref="IActionResult"/></returns>
    /// <response code="204">Se o filme for atualizado com sucesso.</response>
    /// <response code="400">Se o objeto recebido não estiver válido para aplicar a atualização</response>
    /// <response code="404">Se o filme com o id passado não for encontrado.</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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

    /// <summary>
    /// Action responsável por deletar um filme do sistema.
    /// </summary>
    /// <param name="id">Um inteiro que representa o id do filme a ser deletado.</param>
    /// <returns>IActionResult <see cref="IActionResult"/></returns>
    /// <response code="204">Se o filme for deletado com sucesso.</response>
    /// <response code="404">Se o filme com o id passado não for encontrado.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletarFilme(int id)
    {
        var filme = context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        context.Remove(filme);
        context.SaveChanges();

        return NoContent();
    }
}
