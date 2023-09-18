using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;

namespace GamePlay.BLL.Services;

public class RecalculationService : IRecalculationService {
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IGameRatingRepository _gameRatingRepository;
    private readonly IUserRelationRepository _relationRepository;
    private readonly ApplicationDbContext _context;

    public RecalculationService(IUserRepository userRepository, IGameRepository gameRepository, IUserRelationRepository relationRepository,
        ApplicationDbContext context, IGameRatingRepository gameRatingRepository) {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _relationRepository = relationRepository;
        _context = context;
        _gameRatingRepository = gameRatingRepository;
    }

    public async Task RecalculateUserRelationsAsync() {
        var users = await _userRepository.GetAllAsync();
        foreach (var user in users) {
            user.FollowersCount = await _relationRepository.GetAllCountAsync(r => r.UserId.Equals(user.Id) && !r.IsFriend);
            user.FriendsCount =
                await _relationRepository.GetAllCountAsync(r => r.IsFriend && (r.UserId.Equals(user.Id) || r.SubscriberId.Equals(user.Id)));
        }

        _context.SaveChanges();
    }

    public async Task RecalculateAverageRatingAsync() {
        var games = await _gameRepository.GetAllAsync();
        var ratings = (await _gameRatingRepository.GetAllAsync()).ToList();
        foreach (var game in games) {
            var gameRatings = ratings.Where(r => r.GameId == game.Id).ToList();
            game.AverageRating = gameRatings.Any() ? gameRatings.Average(r => r.Rating) : 0;
        }
        
        _context.SaveChanges();
    }
}