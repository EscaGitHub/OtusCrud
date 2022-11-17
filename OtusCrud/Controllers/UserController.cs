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
    public async Task<IActionResult> Get(User user)
    {
        return await Task.FromResult(Ok(user));
    }

    /// <summary>
    /// Delete user
    /// </summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(long userId, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Ok());
    }
    
    /// <summary>
    /// Update user
    /// </summary>
    [HttpPut("{userId}")]
    public async Task<IActionResult> Update(long userId)
    {
        return await Task.FromResult(Ok(new User()));
    }
}