using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Controllers
{
  [Route("api/[controller]")]
  public class TokenController : Controller
  {
    private IConfiguration _config;
    private IPlayerRepository _playerRepository;

    public TokenController(IConfiguration config, IPlayerRepository playerRepository)
    {
      _config = config;
      _playerRepository = playerRepository;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult CreateToken([FromBody]LoginModel login)
    {
      IActionResult response = Unauthorized();
      var user = Authenticate(login);

      if (user != null)
      {
        var tokenString = BuildToken(user);
        response = Ok(new { token = tokenString });
      }

      return response;
    }

    private string BuildToken(Player user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          expires: DateTime.Now.AddHours(12),
          signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
     }

     private Player Authenticate(LoginModel login)
     {
        Player user = null;
        user = _playerRepository.GetPlayerByUserName(login.Username);
        if(user.Password != login.Password){
            return null;
        }

        return user;
     }

    public class LoginModel
    {
      public string Username { get; set; }
      public string Password { get; set; }
    }
  }
}