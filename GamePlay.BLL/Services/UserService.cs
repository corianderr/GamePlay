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

    public async Task AddGameToUserAsync(Guid gameId, Guid userId)
    {
        await _userRepository.AddGameAsync(gameId, userId);
    }

    public async Task<BaseResponseModel> SubscribeAsync(Guid subscriberId, Guid userId)
    {
        var createRelation = new CreateUserRelationModel
        {
            SubscriberId = default,
            UserId = default,
            IsFriend = false
        };
        var relation = _mapper.Map<UserRelation>(createRelation);
        return new BaseResponseModel{ Id = (await _userRepository.AddSubscriptionAsync(relation)).Id };
    }

    public async Task<UserRelationResponseModel> BecomeFriendsAsync(Guid subscriberId, Guid userId)
    {
        var relation = await _userRepository.BecomeFriendsAsync(subscriberId, userId);
        return _mapper.Map<UserRelationResponseModel>(relation);
    }

    public async Task<IEnumerable<UserRelationResponseModel>> GetAllRelationsAsync(Guid userId, bool isFriend)
    {
        var relations = await _userRepository.GetAllRelationsAsync(
            r => r.UserId.Equals(userId) && r.IsFriend == isFriend, r => r.Subscriber);
        return _mapper.Map<IEnumerable<UserRelationResponseModel>>(relations);
    }
    
    public async Task<IEnumerable<UserResponseModel>> GetAllAsync(Expression<Func<UserResponseModel, bool>>? predicate = null)
    {
        var games = await _userRepository.GetAllAsync(_mapper.Map<Expression<Func<ApplicationUser, bool>>>(predicate));
        return _mapper.Map<IEnumerable<UserResponseModel>>(games);
    }
}