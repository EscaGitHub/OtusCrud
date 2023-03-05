using Otus.Auth.Models.Account;
using Otus.Auth.Repositories;
using OtusCrud.Data.DataModels;

namespace Otus.Auth.Services;

/// <summary>
/// Сервис работы с аккаунтом пользователя.
/// </summary>
public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    /// <summary>
    /// Конструктор <see cref="AccountService"/>
    /// </summary>
    /// <param name="accountRepository"></param>
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<UserModel?> GetAsync(long userId, CancellationToken cancellationToken)
    {
        var entity = await _accountRepository.GetAsync(userId, cancellationToken);

        if (entity is not null)
        {
            return new UserModel
            {
                UserName = entity.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Phone = entity.Phone,
                Email = entity.Email
            };
        }

        return null;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<long?> UpdateAsync(long userId, UserModel user, CancellationToken cancellationToken)
    {
        return await _accountRepository.UpdateAsync(userId,
            new UserEntity
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone
            },
            cancellationToken);
    }
}