using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.DAL.Repositories;

public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository {
    public UserRepository(ApplicationDbContext context) : base(context) {
    }

    public bool IsEmailUnique(string email) {
        ApplicationUser? user = null;
        user = DbSet.FirstOrDefault(u => u.Email.Equals(email));
        return user == default;
    }

    public bool IsUsernameUnique(string username) {
        ApplicationUser? user = null;
        user = DbSet.FirstOrDefault(u => u.UserName.Equals(username));
        return user == default;
    }
}