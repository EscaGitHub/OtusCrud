using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Otus.Auth.Controllers;

/// <summary>
/// Account
/// </summary>
[ApiController]
[Route("api/v1/account")]
[Authorize]
public class AccountController : ControllerBase
{
    
}