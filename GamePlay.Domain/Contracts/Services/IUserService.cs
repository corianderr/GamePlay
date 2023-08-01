using System.Linq.Expressions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;
using GamePlay.Domain.Models.User;

namespace GamePlay.Domain.Contracts.Services;

public interface IUserService
{
    Task<BaseModel> RegisterAsync(CreateUserModel createUserModel);
    Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);
    Task<IEnumerable<UserModel>> GetAllAsync(Expression<Func<UserModel, bool>>? predicate = null);
    Task<UserModel> GetFirstAsync(string userId, CancellationToken cancellationToken = default);

    Task<UserModel> UpdateAsync(string id, UserModel updateUserModel,
        CancellationToken cancellationToken = default);
}