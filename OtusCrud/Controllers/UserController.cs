using Microsoft.AspNetCore.Mvc;
using OtusCrud.Models;
using OtusCrud.Services;

namespace OtusCrud.Controllers;

/// <summary>
/// Users management
/// </summary>
[ApiController]
[Route("api/v1/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor <see cref="UserController"/>
    /// </summary>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllAsync(cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Get user
    /// </summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(long userId, CancellationToken cancellationToken)
    {
        var result = await _userService.GetUserAsync(userId, cancellationToken);

        return Ok(result);
    }
    
    /// <summary>
    /// Create user
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(UserModel user, CancellationToken cancellationToken)
    {
        var addedUser = await _userService.CreateUserAsync(user, cancellationToken);

        return Ok(addedUser);
    }

    /// <summary>
    /// Delete user
    /// </summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(long userId, CancellationToken cancellationToken)
    {
        await _userService.DeleteUserAsync(userId, cancellationToken);

        return Ok();
    }
    
    /// <summary>
    /// Update user
    /// </summary>
    [HttpPut("{userId}")]
    public async Task<IActionResult> Update(long userId, UserModel user, CancellationToken cancellationToken)
    {
        var updatedUser = await _userService.UpdateUserAsync(userId, user, cancellationToken);

        return Ok(updatedUser);
    }
}