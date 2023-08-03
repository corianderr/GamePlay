using System.Collections;
using System.Linq.Expressions;
using AutoMapper;
using GamePlay.BLL.Helpers;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Exceptions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Collection;
using GamePlay.Domain.Models.Game;
using GamePlay.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GamePlay.BLL.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly ICollectionService _collectionService;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper, ICollectionService collectionService, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRepository = userRepository;
        _mapper = mapper;
        _collectionService = collectionService;
        _configuration = configuration;
    }

    public async Task<BaseModel> RegisterAsync(CreateUserModel createUserModel)
    {
        var user = _mapper.Map<ApplicationUser>(createUserModel);
        user.PhotoPath = "/avatars/default-user-avatar.jpg";

        var emailUniq = _userRepository.IsEmailUnique(user.Email);
        if (!emailUniq) throw new BadRequestException("User with this email already exists");
        var usernameUniq = _userRepository.IsUsernameUnique(user.UserName);
        if (!usernameUniq) throw new BadRequestException("User with this username already exists");

        var result = await _userManager.CreateAsync(user, createUserModel.Password);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        result = await _userManager.AddToRoleAsync(user, "user");
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        var userId = (await _userManager.FindByNameAsync(user.UserName)).Id;
        await _collectionService.CreateAsync(new CreateCollectionModel() { Name = "My Collection", UserId = userId });
        
        return new BaseModel
        {
            Id = Guid.Parse(userId)
        };
    }

    public async Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginUserModel.EmailOrUsername) 
                   ?? await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginUserModel.EmailOrUsername);
        
        if (user == null)
            throw new NotFoundException("Username/email or password is incorrect");

        var signInResult =
            await _signInManager.PasswordSignInAsync(user, loginUserModel.Password, loginUserModel.RememberMe, false);
        if (!signInResult.Succeeded)
            throw new BadRequestException("Username/email or password is incorrect");

        var roles = await _userManager.GetRolesAsync(user);
        var token = JwtHelper.GenerateToken(user, _configuration, roles);
        
        return new LoginResponseModel
        {
            Username = user.UserName,
            Email = user.Email,
            Token = token,
            Roles = roles
        };
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync(Expression<Func<UserModel, bool>>? predicate = null)
    {
        var games = await _userRepository.GetAllAsync(_mapper.Map<Expression<Func<ApplicationUser, bool>>?>(predicate));
        return _mapper.Map<IEnumerable<UserModel>>(games);
    }
    
    public async Task<UserModel> GetFirstAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetFirstAsync(u => u.Id.Equals(userId));
        return _mapper.Map<UserModel>(user);
    }
    
    public async Task<UserModel> UpdateAsync(string id, UserModel updateUserModel,
        CancellationToken cancellationToken = default)
    {
        var game = await _userRepository.GetFirstAsync(e => e.Id == id);
        _mapper.Map(updateUserModel, game);
        return new UserModel
        {
            Id = (await _userRepository.UpdateAsync(game)).Id
        };
    }
}