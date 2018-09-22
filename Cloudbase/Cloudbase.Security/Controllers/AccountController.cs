using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cloudbase.Entities;
using Cloudbase.Security.Filters;
using Cloudbase.Security.Models;
using CloudBase.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class AccountController : ControllerBase
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;


        public AccountController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           IConfiguration configuration,
           ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }


        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel loginModel)
        {
            //var origin = HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Origin").Value.ToString();
            var host = HttpContext.Request.Host.Value;

            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, isPersistent: false, lockoutOnFailure: false);

                if (!loginResult.Succeeded)
                {
                    return BadRequest();
                }

                var user = await _userManager.FindByNameAsync(loginModel.Username);

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

            return Ok();

            //if (ModelState.IsValid)
            //{
            //    var user = new ApplicationUser
            //    {
            //        //TODO: Use Automapper instaed of manual binding  

            //        UserName = registerModel.Username,
            //        FirstName = registerModel.FirstName,
            //        LastName = registerModel.LastName,
            //        Email = registerModel.Email
            //    };

            //    var identityResult = await _userManager.CreateAsync(user, registerModel.Password);
            //    if (identityResult.Succeeded)
            //    {
            //        await _signInManager.SignInAsync(user, isPersistent: false);
            //        return Ok(GetToken(user));
            //    }

            //    return BadRequest(identityResult.Errors);
            //}
            //return BadRequest(ModelState);


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
