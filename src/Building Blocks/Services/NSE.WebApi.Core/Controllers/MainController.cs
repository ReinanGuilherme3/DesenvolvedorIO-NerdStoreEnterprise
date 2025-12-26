using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NSE.WebApi.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class MainController : ControllerBase
{
    protected ICollection<string> Erros = new List<string>();

    protected IActionResult CustomResponse(object? result = null)
    {
        if (OperacaoValida())
            return Ok(result);


        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            {  "Mensagens", Erros.ToArray()   }
        }));
    }

    protected IActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            AdicionarErroProcessamento(erro.ErrorMessage);
        }
        return CustomResponse();
    }

    protected bool OperacaoValida()
    {
        return !Erros.Any();
    }

    protected void AdicionarErroProcessamento(string mensagem)
    {
        Erros.Add(mensagem);
    }

    protected void LimparErros()
    {
        Erros.Clear();
    }
}
