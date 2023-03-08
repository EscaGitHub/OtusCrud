using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otus.Auth.Models.Account;
using Otus.Auth.Services;

namespace Otus.Auth.Controllers;

/// <summary>
/// Account
/// </summary>
[ApiController]
[Route("api/v1/account")]
//[Authorize]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    /// <summary>
    /// Конструктор <see cref="AccountController"/>
    /// </summary>
    /// <param name="accountService"></param>
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    /// <summary>
    /// Информация о пользователе.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetUserInfo(CancellationToken cancellationToken)
    {
        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        UserModel? user = null;
        
        if (long.TryParse(claimValue, out var userId))
        {
            user = await _accountService.GetAsync(userId, cancellationToken);
        }

        return user is null ? NotFound() : Ok(user);
    }

    /// <summary>
    /// Обновление данных пользователя.
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateUserInfo(UserRequest request, CancellationToken cancellationToken)
    {
        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        long? userUpdated = null;

        if (long.TryParse(claimValue, out var userId))
        {
            userUpdated = await _accountService.UpdateAsync(userId,
                new UserModel
                {
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Phone = request.Phone,
                    Email = request.Email
                },
                cancellationToken);
        }

        return userUpdated is null ? NotFound() : Ok();
    }
}