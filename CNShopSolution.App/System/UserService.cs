using CNShopSolution.App.Exceptions;
using CNShopSolution.Data.Entities;
using CNShopSolution.ViewModel.Common;
using CNShopSolution.ViewModel.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CNShopSolution.App.System
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = configuration;
        }

        public async Task<string> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.PassWord, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddHours(2)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName == request.Keyword || x.PhoneNumber == request.Keyword);
            }

            //totalPage
            int total = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    UserName = x.UserName,

                }).ToListAsync();

            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecord = total,
                Items = data,
            };

            return pagedResult;
               

        }

        public async Task<bool> Register(RegisterRequest request)
        {
            //var user = _userManager.FindByNameAsync(request.UserName);

            //if (user !=null)
            //{
            //    if (await _userManager.FindByEmailAsync(request.Email)!=null)
            //    {

            //    }
            //}

            var user = new AppUser()
            {
                Dob = DateTime.Now,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.PassWord);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}