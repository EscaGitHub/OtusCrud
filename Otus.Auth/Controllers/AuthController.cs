using System.Net;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Otus.Auth.Models.Auth;
using Otus.Auth.Services;

namespace Otus.Auth.Controllers;

/// <summary>
/// Auth
/// </summary>
[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    /// <summary>
    /// Конструктор <see cref="AuthController"/>
    /// </summary>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// User register
    /// </summary>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _authService.RegisterUserAsync(request.Login, request.Password, cancellationToken);

            return Ok(result);
        }
        catch (Exception e)
        {
            throw new ProblemDetailsException((int)HttpStatusCode.BadRequest, e.Message, e);
        }
    }

    /// <summary>
    /// Get auth token
    /// </summary>
    /// <returns></returns>
    [HttpPost("token")]
    public async Task<IActionResult> GetToken(TokenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _authService.GetUserTokenAsync(request.Login, request.Password, cancellationToken);

            return Ok(token);
        }
        catch (Exception e)
        {
            throw new ProblemDetailsException((int)HttpStatusCode.BadRequest, e.Message, e);
        }
    }
}