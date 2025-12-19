using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSE.Identidade.API.Models;

namespace NSE.Identidade.API.Controllers;

[Route("api/identidade")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("nova-conta")]
    public async Task<IActionResult> Registrar(UsuarioRegistro request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Senha);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Ok();
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("autenticar")]
    public async Task<IActionResult> Login(UsuarioLogin request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest("Usuário ou senha inválidos");
    }
}
