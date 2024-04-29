using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using Store.Service.Services.TokenService;
using Store.Service.Services.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<UserDto> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user is null)
                return null;
            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password,false);
            if(!result.Succeeded)
                throw new Exception("Login Failed");
            return new UserDto
            {
                Email = input.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateToken(user)
            };

        }

        public async Task<UserDto> Register(RegisterDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user is not null)
                return null;
            var appUser = new AppUser
            {
                DisplayName = input.DisplayName,
                Email = input.Email,
                UserName = input.Email.Split('@')[0],
            };
            
            var result = await _userManager.CreateAsync(appUser, input.Password);
            if (!result.Succeeded)
                throw new Exception(result.Errors.Select(x=>x.Description).FirstOrDefault());

            return new UserDto
            {
                Email = input.Email,
                DisplayName = input.DisplayName,
                Token = _tokenService.GenerateToken(appUser)
            };

        }
    }
}
