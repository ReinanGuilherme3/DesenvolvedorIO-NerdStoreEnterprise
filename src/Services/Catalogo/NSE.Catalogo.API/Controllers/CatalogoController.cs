using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;

namespace NSE.Catalogo.API.Controllers;

[Route("[controller]")]
[ApiController]
public class CatalogoController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public CatalogoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet("produtos")]
    public async Task<IEnumerable<Produto>> GetProdutos()
    {
        return await _produtoRepository.ObterTodos();
    }

    [HttpGet("produtos/{id}")]
    public async Task<Produto?> GetProdutoById(Guid id)
    {
        return await _produtoRepository.ObterPorId(id);
    }
}
