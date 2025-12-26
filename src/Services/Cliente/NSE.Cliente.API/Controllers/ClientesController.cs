using Microsoft.AspNetCore.Mvc;
using NSE.Cliente.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebApi.Core.Controllers;

namespace NSE.Cliente.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;

    public ClientesController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    [HttpGet("/")]
    public async Task<IActionResult> Obter()
    {
        var resultado = await _mediatorHandler.EnviarComando(new RegistrarClienteCommand(Guid.NewGuid(), "Reinan", "r.guilherme@gmail.com", "02345418060"));

        return CustomResponse(resultado);
    }
}
