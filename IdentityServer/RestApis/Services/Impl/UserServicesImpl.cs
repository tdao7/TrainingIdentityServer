using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.Models;
using IdentityServer.Models.Form;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.RestApis.Services.Impl
{
    public class UserServicesImpl : IUserServices
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IdentityServerTools _tools;

        public UserServicesImpl(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IdentityServerTools tools)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tools = tools;
        }

        public async Task<string> login(LoginForm loginForm)
        {
            var result = await _signInManager.PasswordSignInAsync(loginForm.Username, loginForm.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginForm.Username);
                return await GetToken(user);
            }
            throw new Exception("Username Or Password Incorrect!");
        }
        
        private async Task<string> GetToken(ApplicationUser user)
        {

            var scopeList = new List<string> { "openid", "profile"};
            
            var token = await _tools.IssueClientJwtAsync(
                clientId: "WebApis",
                lifetime: 360000000,
                audiences: new[] {"WebApis"},
                scopes: scopeList
                );

            return token;
        }
    }
}