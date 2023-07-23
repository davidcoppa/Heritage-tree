using AutoMapper;
using Events.Core.Common.Captcha;
using Events.Core.Common.Helpers;
using Events.Core.Common.Messages;
using Events.Core.Common.Validators;
using Events.Core.DTOs;
using Events.Core.Model;
using EventsManager.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;
        private readonly IMessages messages;
        private readonly IHelper helper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountController(EventsContext context,
            IMapper mapper,
            IDataValidator validator,
            IMessages messages,
            IHelper helper,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration

            )
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = validator;
            this.messages = messages;
            this.helper = helper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;

        }

        private bool ValidateCaptha(string token, out string error)
        {
            try
            {
                error = "";
                new CaptchaAssessment().createAssessment("events-1687673175101", "6Lc368kmAAAAACRDU-wPhh0L-YkMZMdjZgXluv2J", token, "importantAction");
            }
            catch (ApplicationException ex)
            {
                error = ex.Message;
                return false;
            }
            return true;
        }

        //create account
        [HttpPost("create")]
        public async Task<ActionResult<UserTokenDTO>> Create(UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("No data");
            }
            if (user.Password2==null|| string.Compare(user.Password2,user.Password)!=0)
            {
                return BadRequest("Passwords don't match!");
            }
            if (!ValidateCaptha(user.CaptchaToken, out string error))
            {
                BadRequest("Invalid captcha: " + error);
            }

            var newUser = new IdentityUser { UserName = user.Email, Email = user.Email };
            var resultUserCreation = await userManager.CreateAsync(newUser, user.Password);

            if (!resultUserCreation.Succeeded)
            {
                return BadRequest(resultUserCreation.Errors);
            }

            return Ok();// await BuildToken(user);

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDTO>> Login(UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("No data");
            }

            if (!ValidateCaptha(user.CaptchaToken,out string error))
            {
                BadRequest("Invalid captcha: " + error);
            }

            var login = await signInManager.PasswordSignInAsync(user.Email, user.Password, isPersistent: false, lockoutOnFailure: false);
            if (!login.Succeeded)
            {
                return BadRequest("Invalid Username or Password");
            }

            return await BuildToken(user);
        }

        [HttpPost("AddRol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> AddRoles([FromBody] UserRoleDTO userDTO)
        {

            var user = await userManager.FindByEmailAsync(userDTO.Email);
            if (userDTO.Rol != null || userDTO.Rol == "IsAdmin")
            {
                await userManager.AddClaimAsync(user, new Claim("IsAdmin", "1"));
            }
            else
            {
                await userManager.AddClaimAsync(user, new Claim("User", "1"));
            }


            return NoContent();
        }
        [HttpPost("removeRol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> RemoveRol([FromBody] UserRoleDTO userDTO)
        {

            var user = await userManager.FindByEmailAsync(userDTO.Email);
            if (userDTO.Rol != null || userDTO.Rol == "IsAdmin")
            {
                await userManager.RemoveClaimAsync(user, new Claim("IsAdmin", "0"));
            }
            else
            {
                await userManager.RemoveClaimAsync(user, new Claim("User", "0"));
            }
            return NoContent();
        }

        [HttpGet("RefreshToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserTokenDTO>> RefreshToken()
        {
            var emailClaims = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

            if (emailClaims == null)
            {
                return BadRequest("User doesn't exist");
            }
            var userCredentials = new UserDTO()
            {
                Email = emailClaims.Value
            };

            return await BuildToken(userCredentials);
        }
        private async Task<UserTokenDTO> BuildToken(UserDTO userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim("email", userInfo.Email),
                new Claim("TokenUser", "a6sA21sd5a1fa,5d1f665akfpwobpef.nNPBDFNABPKD"+Guid.NewGuid().ToString()+"FNBS6954LASNFBALSN55SDF5W6A1SZ6FDSasdasd.sdfergfadfvasdf,B4DF"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var usuario = await userManager.FindByEmailAsync(userInfo.Email);
            var roles = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(roles);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Tiempo de expiración del token. 
            var expiration = DateTime.UtcNow.AddMonths(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

    }
}
