using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;

namespace GamePlay.BLL.Services;

public class RecalculationService : IRecalculationService {
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserRelationRepository _relationRepository;
    private readonly ApplicationDbContext _context;

    public RecalculationService(IUserRepository userRepository, IGameRepository gameRepository, IUserRelationRepository relationRepository,
        ApplicationDbContext context) {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _relationRepository = relationRepository;
        _context = context;
    }

    public async Task RecalculateUserRelations() {
        var users = await _userRepository.GetAllAsync();
        foreach (var user in users) {
            user.FollowersCount = await _relationRepository.GetAllCountAsync(r => r.UserId.Equals(user.Id) && !r.IsFriend);
            user.FriendsCount =
                await _relationRepository.GetAllCountAsync(r => r.IsFriend && (r.UserId.Equals(user.Id) || r.SubscriberId.Equals(user.Id)));
        }

        _context.SaveChanges();
    }

    public async Task RecalculateAverageRating() {
        throw new NotImplementedException();
    }
}