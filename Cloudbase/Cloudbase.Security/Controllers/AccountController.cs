﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Cloudbase.Entities;
using Cloudbase.Security.Filters;
using Cloudbase.Security.Models;
using CloudBase.Core.Extensions;
using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Cloudbase.Security.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[Tenant]
    public class AccountController : BaseController
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        IConfiguration _configuration;
        private ILogger<AccountController> _logger;
        private IHttpContextAccessor _httpContextAccessor;


        public AccountController(
            SecurityDbContext securityDbContext,
            TenantDbContext tenantDbContext,
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           IConfiguration configuration,
           ILogger<AccountController> logger,
            IHttpContextAccessor httpContextAccessor) : base(securityDbContext, tenantDbContext, httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel loginModel)
        {

            //SecurityDbContext = DbContextFactory.Create(Tenant.DatabaseConnectionString);

            var optionsBuilder = new DbContextOptionsBuilder<SecurityDbContext>();
            SecurityDbContext = new SecurityDbContext(optionsBuilder.Options, Tenant);
            //var user = SecurityDbContext.Users.FirstOrDefault(x => x.UserName == loginModel.Username);

            var userstore = new UserStore<ApplicationUser>(SecurityDbContext);
            IPasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

            var validator = new UserValidator<ApplicationUser>();
            var validators = new List<UserValidator<ApplicationUser>> { validator };

            _userManager = new UserManager<ApplicationUser>(userstore, new OptionsProvider(), hasher, validators,null , null, null, null, null);
            //var roleManager = new RoleManager<ApplicationUser>(userstore, new OptionsProvider(), hasher, validators, null, null, null, null, null);
            _signInManager = new SignInManager<ApplicationUser>(_userManager, _httpContextAccessor,new UserClaimsPrincipalFactory<ApplicationUser>(_userManager, new OptionsProvider()), new OptionsProvider(), null, null);

            //return Ok();

            /*            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

                        if (hasher.VerifyHashedPassword(user, user.PasswordHash, loginModel.Password) != PasswordVerificationResult.Failed)
                        {
                            return Ok(GetToken(user));
                        }

                        return BadRequest();*/

            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, isPersistent: false, lockoutOnFailure: false);
                if (!loginResult.Succeeded)
                {
                    return BadRequest();
                }
                //var user = await _userManager.FindByNameAsync(loginModel.Username);
                return Ok(GetToken(user));
            }
            return BadRequest(ModelState);

        }

        [Authorize]
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var user = await _userManager.FindByNameAsync(
                User.Identity.Name ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
                );
            return Ok(GetToken(user));

        }


        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [Tenant]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    //TODO: Use Automapper instaed of manual binding  

                    UserName = registerModel.Username,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Email = registerModel.Email
                };


                //SecurityDbContext = DbContextFactory.Create(Tenant.DatabaseConnectionString);


                PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();


                user.PasswordHash = hasher.HashPassword(user, registerModel.Password);

                SecurityDbContext.Users.Add(user);
                SecurityDbContext.SaveChanges();

                return Ok(GetToken(user));

                /*                var userStore = new UserStore<ApplicationUser>(SecurityDbContext);
                                var userManager = new UserManager<ApplicationUser>(userStore, null, null, null, null, null, null, null, null);

                                var identityResult = await userManager.CreateAsync(user, registerModel.Password);*/

                /*                var identityResult = await _userManager.CreateAsync(user, registerModel.Password);
                                if (identityResult.Succeeded)
                                {
                                    await _signInManager.SignInAsync(user, isPersistent: false);
                                    return Ok(GetToken(user));
                                }*/

                //return BadRequest(identityResult.Errors);
            }
            return BadRequest(ModelState);


        }

        private String GetToken(IdentityUser user)
        {
            var utcNow = DateTime.UtcNow;

            using (RSA privateRsa = RSA.Create())
            {
                privateRsa.FromXmlFile(Path.Combine(Directory.GetCurrentDirectory(),
                    "Keys",
                    this._configuration.GetValue<String>("Tokens:PrivateKey")
                ));
                var privateKey = new RsaSecurityKey(privateRsa);
                SigningCredentials signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);


                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
                };

                var jwt = new JwtSecurityToken(
                    signingCredentials: signingCredentials,
                    claims: claims,
                    notBefore: utcNow,
                    expires: utcNow.AddSeconds(this._configuration.GetValue<int>("Tokens:Lifetime")),
                    audience: this._configuration.GetValue<String>("Tokens:Audience"),
                    issuer: this._configuration.GetValue<String>("Tokens:Issuer")
                );

                return new JwtSecurityTokenHandler().WriteToken(jwt);
            }
        }


    }
}
