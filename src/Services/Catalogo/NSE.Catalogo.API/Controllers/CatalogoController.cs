using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using NSE.WebApi.Core.Controllers;
using NSE.WebApi.Core.Identidade;

namespace NSE.Catalogo.API.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class CatalogoController : MainController
{
    private readonly IProdutoRepository _produtoRepository;

    public CatalogoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [AllowAnonymous]
    [HttpGet("produtos")]
    public async Task<IEnumerable<Produto>> GetProdutos()
    {
        return await _produtoRepository.ObterTodos();
    }

    [ClaimsAuthorize("Catalogo", "Ler")]
    [HttpGet("produtos/{id}")]
    public async Task<Produto?> GetProdutoById(Guid id)
    {
        return await _produtoRepository.ObterPorId(id);
    }
}
