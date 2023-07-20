using System.Linq.Expressions;
using AutoMapper;
using GamePlay.BLL.Services.Interfaces;
using GamePlay.Domain.Contracts;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Exceptions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.BLL.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IUserRepository userRepository,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel> RegisterAsync(CreateUserModel createUserModel)
    {
        var user = _mapper.Map<ApplicationUser>(createUserModel);
        user.PhotoPath = "/avatars/default-user-avatar.jpg";
        
        var emailUniq = _userRepository.IsEmailUnique(user.Email);
        if (!emailUniq) throw new BadRequestException("User with this email already exists");
        var usernameUniq = _userRepository.IsUsernameUnique(user.UserName);
        if (!usernameUniq) throw new BadRequestException("User with this username already exists" );
        
        var result = await _userManager.CreateAsync(user, createUserModel.Password);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);
        
        return new BaseResponseModel()
        {
            Id = Guid.Parse((await _userManager.FindByNameAsync(user.UserName)).Id)
        };
    }

    public async Task<LoginUserModel> LoginAsync(LoginUserModel loginUserModel)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginUserModel.Username);
        if (user == null)
            throw new NotFoundException("Username or password is incorrect");

        var signInResult = await _signInManager.PasswordSignInAsync(user, loginUserModel.Password, loginUserModel.RememberMe, false);
        if (!signInResult.Succeeded)
            throw new BadRequestException("Username or password is incorrect");
        
        return new LoginUserModel
        {
            Username = user.UserName
        };
    }

    public async Task AddGameToUserAsync(Guid gameId, string userId)
    {
        await _userRepository.AddGameAsync(gameId, userId);
    }

    public async Task<BaseResponseModel> SubscribeAsync(string subscriberId, string userId)
    {
        var createRelation = new CreateUserRelationModel
        {
            SubscriberId = subscriberId,
            UserId = userId,
            IsFriend = false
        };
        var relation = _mapper.Map<UserRelation>(createRelation);
        return new BaseResponseModel{ Id = (await _userRepository.AddSubscriptionAsync(relation)).Id };
    }

    public async Task<UserRelationResponseModel> BecomeFriendsAsync(string subscriberId, string userId)
    {
        var newRelation = await _userRepository.BecomeFriendsAsync(subscriberId, userId);
        return _mapper.Map<UserRelationResponseModel>(newRelation);
    }

    public async Task<IEnumerable<UserRelationResponseModel>> GetAllRelationsAsync(string userId, bool? isFriend = null)
    {
        IEnumerable<UserRelation> relations;
        if (isFriend == null)
        {
            relations = await _userRepository.GetAllRelationsAsync(
                r => r.UserId.Equals(userId), r => r.Subscriber);
        }
        else if ((bool)isFriend)
        {
            relations = await _userRepository.GetAllRelationsAsync(
                r => (r.UserId.Equals(userId) || r.SubscriberId.Equals(userId)) && r.IsFriend == isFriend, r => r.Subscriber, r => r.User);
        }
        else
        {
            relations = await _userRepository.GetAllRelationsAsync(
                r => r.UserId.Equals(userId) && r.IsFriend == isFriend, r => r.Subscriber);
        }
        return _mapper.Map<IEnumerable<UserRelationResponseModel>>(relations);
    }
    
    public async Task<IEnumerable<UserResponseModel>> GetAllAsync(Expression<Func<UserResponseModel, bool>>? predicate = null)
    {
        var games = await _userRepository.GetAllAsync(_mapper.Map<Expression<Func<ApplicationUser, bool>>?>(predicate));
        return _mapper.Map<IEnumerable<UserResponseModel>>(games);
    }
    
    public async Task<UserRelationResponseModel?> GetRelationByUsersIdAsync(string subscriberId, string userId, CancellationToken cancellationToken = default)
    {
        var userRelation = await _userRepository.GetFirstRelationAsync(
            r => r.UserId.Equals(userId) && r.SubscriberId.Equals(subscriberId));
        return _mapper.Map<UserRelationResponseModel>(userRelation);
    }
    public async Task<UserResponseModel> GetFirstAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetFirstAsync(u => u.Id.Equals(userId));
        return _mapper.Map<UserResponseModel>(user);
    }
    
    public async Task<UserResponseModel> UpdateAsync(string id, UserResponseModel updateUserModel, CancellationToken cancellationToken = default)
    {
        var game = await _userRepository.GetFirstAsync(e => e.Id == id);
        _mapper.Map(updateUserModel, game);
        return new UserResponseModel
        {
            Id = (await _userRepository.UpdateAsync(game)).Id
        };
    }
}