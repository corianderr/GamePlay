using System.Linq.Expressions;
using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Contracts.Repositories;

public interface IUserRepository : IBaseRepository<ApplicationUser> {
    bool IsEmailUnique(string email);
    public bool IsUsernameUnique(string username);
}