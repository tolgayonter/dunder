using api.DTOs;
using api.Entities;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

public class AccountController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

        var user = _mapper.Map<User>(registerDto);

        user.UserName = registerDto.Username.ToLower();

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        var roleResult = await _userManager.AddToRoleAsync(user, "Member");

        if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

        return new UserDto
        {
            Username = user.UserName,
            KnownAs = user.KnownAs,
            Token = await _tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

        if (user == null) return Unauthorized("Invalid username");

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!result) return Unauthorized("Invalid password");

        return new UserDto
        {
            Username = user.UserName,
            KnownAs = user.KnownAs,
            Token = await _tokenService.CreateToken(user),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}