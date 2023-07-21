using AutoMapper;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.User;

namespace GamePlay.BLL.Services;

public class UserRelationService : IUserRelationService
{
    private readonly IMapper _mapper;
    private readonly IUserRelationRepository _relationRepository;
    private readonly IUserRepository _userRepository;

    public UserRelationService(IMapper mapper, IUserRelationRepository relationRepository, IUserRepository userRepository)
    {
        _mapper = mapper;
        _relationRepository = relationRepository;
        _userRepository = userRepository;
    }
    public async Task<BaseModel> SubscribeAsync(string subscriberId, string userId)
    {
        var user = await _userRepository.GetFirstAsync(u => u.Id.Equals(userId));
        user.FollowersCount++;
        
        var createRelation = new CreateUserRelationModel
        {
            SubscriberId = subscriberId,
            UserId = userId,
            IsFriend = false
        };
        var relation = _mapper.Map<UserRelation>(createRelation);
        return new BaseModel { Id = (await _relationRepository.AddAsync(relation)).Id };
    }

    public async Task<UserRelationModel> BecomeFriendsAsync(string subscriberId, string userId)
    {
        var subscriber = await _userRepository.GetFirstAsync(u => u.Id.Equals(subscriberId));
        subscriber.FriendsCount++;

        var user = await _userRepository.GetFirstAsync(u => u.Id.Equals(userId));
        user.FriendsCount++;
        user.FollowersCount--;

        var newRelation = await _relationRepository.BecomeFriendsAsync(subscriber, user);
        return _mapper.Map<UserRelationModel>(newRelation);
    }

    public async Task<IEnumerable<UserRelationModel>> GetAllAsync(string userId, bool? isFriend = null)
    {
        IEnumerable<UserRelation> relations;
        if (isFriend == null)
        {
            relations = await _relationRepository.GetAllAsync(
                r => r.UserId.Equals(userId), r => r.Subscriber);
        }
        else if ((bool)isFriend)
        {
            relations = await _relationRepository.GetAllAsync(
                r => (r.UserId.Equals(userId) || r.SubscriberId.Equals(userId)) && r.IsFriend == isFriend,
                r => r.Subscriber, r => r.User);
        }
        else
        {
            relations = await _relationRepository.GetAllAsync(
                r => r.UserId.Equals(userId) && r.IsFriend == isFriend, r => r.Subscriber);
        }
        return _mapper.Map<IEnumerable<UserRelationModel>>(relations);
    }

    public async Task<UserRelationModel?> GetByUsersIdAsync(string subscriberId, string userId,
        CancellationToken cancellationToken = default)
    {
        var userRelation = await _relationRepository.GetFirstAsync(
            r => r.UserId.Equals(userId) && r.SubscriberId.Equals(subscriberId));
        return _mapper.Map<UserRelationModel>(userRelation);
    }
}