//using CTBC.Network;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PayrollPartnerAPI.DataAccessLayer;
using PayrollPartnerAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PayrollPartnerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        //public LoginController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        //{
        //    this.userManager = userManager;
        //    this.roleManager = roleManager;
        //    _configuration = configuration;
        //}

        public readonly IAuthDL _authDL;
        public LoginController(IAuthDL authDL)
        {
            _authDL = authDL;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Model.LoginModel model)
        {
            //var user = await userManager.FindByNameAsync(model.userName);
            var response = new Response();

            string strNetworkID = model.userName;
            string strPassword = model.password;

            string sTestUser = "gelo";
            string sTestPass = "gelopass";

            try
            {
                response = await _authDL.Login(model);
                return Ok(new Model.Response { IsSuccess = true, Message = "Login success!" });
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return Unauthorized();
            }

            //if (sTestUser == model.userName && sTestPass == model.password)
            //{
            //    var authClaims = new List<Claim>
            //            {
            //                new Claim(ClaimTypes.Name, user.UserName),
            //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //            };

            //    var claimIdentity = new ClaimsIdentity(authClaims, model.userName);
            //    var ClaimsPrincipal = new ClaimsPrincipal(claimIdentity);
            //    var authenticationProperty = new AuthenticationProperties
            //    {
            //        IsPersistent = false
            //    };

            //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            //    var token = new JwtSecurityToken(
            //    issuer: _configuration["JWT:ValidIssuer"],
            //    audience: _configuration["JWT:ValidAudience"],
            //    expires: DateTime.Now.AddMinutes(5),
            //    claims: authClaims,
            //    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            //    );

               // return Ok(new Model.Response { Status = "Success", Message = "Login success!" });
            //}

            //CTBC.Cryptography.AES crypto = new CTBC.Cryptography.AES(SystemCore.SecurityKey);

            //string strLDAPPath = "LDAP://chinatrust.com.ph";
            //string strLDAPUsername = "57kfZnu8PbRQDLMcXz+EJg==";
            //string strLDAPPassword = "PrMYssXe9/K59cERN3IMeA==";

            //ActiveDirectory ad = new ActiveDirectory(strLDAPPath, crypto.Decrypt(strLDAPUsername), crypto.Decrypt(strLDAPPassword));
            //if(ad.ErrorException != null)
            //{
            //    ActiveDirectory.UserDetails details = ad.GetUserDetailsSingle(strNetworkID);
            //    if (!details.IsLockout && !details.IsAccountExpired && !details.IsAccountDisabled)
            //    {
            //        if (!CTBC.Network.Credential.Logon(strNetworkID, "CTCBPH_GL2", strPassword))
            //        {
            //            //response.ResponseStatus = SystemCore.ResponseStatus.FAILED;
            //            //response.Description = "Authentication Failed.";
            //        }
            //        else
            //        {
            //            var authClaims = new List<Claim>
            //            {
            //                new Claim(ClaimTypes.Name, user.UserName),
            //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //            };

            //            var claimIdentity = new ClaimsIdentity(authClaims, model.UserName);
            //            var ClaimsPrincipal = new ClaimsPrincipal(claimIdentity);
            //            var authenticationProperty = new AuthenticationProperties
            //            {
            //                IsPersistent = false
            //            };

            //            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            //            var token = new JwtSecurityToken(
            //            issuer: _configuration["JWT:ValidIssuer"],
            //            audience: _configuration["JWT:ValidAudience"],
            //            expires: DateTime.Now.AddMinutes(5),
            //            claims: authClaims,
            //            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            //            );


            //            return Ok(new Model.Response { Status = "Success", Message = "User created successfully!" });
            //        }
            //    }
            //}
           // return Unauthorized();
        }
    }
}