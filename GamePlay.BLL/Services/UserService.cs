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

        var signInResult = await _signInManager.PasswordSignInAsync(user, loginUserModel.Password, false, false);
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

    public async Task<UserRelation> SubscribeAsync(Guid subscriberId, Guid userId)
    {
        var createRelation = new CreateUserRelationModel
        {
            SubscriberId = default,
            UserId = default,
            IsFriend = false
        };
        var relation = _mapper.Map<UserRelation>(createRelation);
        await _userRepository.AddSubscriptionAsync(relation);
        return relation;
    }

    public Task<UserRelation> BecomeFriendsAsync(Guid firstUserId, Guid secondUserId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserRelation>> GetAllRelationsAsync(Guid userId, bool isFriend)
    {
        throw new NotImplementedException();
    }
}