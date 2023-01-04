using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using API.Base;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AccountsController : BaseController<AccountRepositories, Account, string>
{
  private AccountRepositories _repo;
  private IConfiguration _con;

  public AccountsController(AccountRepositories repo, IConfiguration con) : base(repo)
  {
    _repo = repo;
    _con = con;
  }

  [HttpPost]
  [Route("Register")]
  public ActionResult Register(RegisterVM registerVM)
  {
    try
    {
      var result = _repo.Register(registerVM);
      switch (result)
      {
        case 0: return Ok(new { statusCode = 204, message = "Email or Phone is already Registered!" });
        case 3: return Ok(new { statusCode = 201, message = "Your password must be at least 8 characters long, contain at least one number and have a mixture of uppercase and lowercase letters and special character!" });
        default:
          return Ok(new { statusCode = 201, message = "Register Succesfully!" });
      }
      /*return result == 0 ? Ok(new { statusCode = 204, message = "Email or Phone is already Registered!" }) :
      Ok(new { statusCode = 201, message = "Register Succesfully!" });*/
    }

    catch (Exception e)
    {
      return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
    }
  }

  [HttpPost]
  [Route("Login")]
  public ActionResult Login(LoginVM loginVM)
  {
    try
    {
      var result = _repo.Login(loginVM);
      switch (result)
      {
        case 0:
          return Ok(new { statusCode = 201, message = "Email Not Found!" });
        case 1:
          return Ok(new { statusCode = 201, message = "Wrong Password!" });
        default:
          // method  to get role user login
          var roles = _repo.UserRoles(loginVM.Email);

          var claims = new List<Claim>(){
              //new Claim(ClaimTypes.Email, loginVM.Email)
              new Claim("email", loginVM.Email)

            };
          foreach (var item in roles)
          {
            claims.Add(new Claim("role", item));
          }

          var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_con["JWT:KEY"]));
          var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
          var token = new JwtSecurityToken(
            _con["JWT:Issuer"],
            _con["JWT:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signIn
          );

          var generateToken = new JwtSecurityTokenHandler().WriteToken(token);
          claims.Add(new Claim("Token Security", generateToken.ToString()));

          return Ok(new { statusCode = 200, message = "Login Success!", data = generateToken });
      }

    }
    catch (Exception e)
    {
      return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
    }
  }


  /*[HttpGet]
  public ActionResult GetAll()
  {
    try
    {
      var result = _repo.Get();
      return result.Count() == 0
      ? Ok(new { statusCode = 204, message = "Data Not Found!" })
      : Ok(new { statusCode = 201, message = "Success", data = result });
    }
    catch (Exception e)
    {
      return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
    }

  }

  [HttpGet]
  [Route("{id}")]
  public ActionResult GetById(string id)
  {
    try
    {
      var result = _repo.Get(id);
      return result == null
      ? Ok(new { statusCode = 204, message = $"Data With Id {id} Not Found!" })
      : Ok(new { statusCode = 201, message = $"Account with NIK {id}", data = result });
    }
    catch (Exception e)
    {
      return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
    }
  }

  [HttpPost]
  public ActionResult Insert(Account account)
  {
    try
    {
      var result = _repo.Insert(account);
      return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
      Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
    }
    catch
    {
      return BadRequest(new { statusCode = 500, message = "" });
    }
  }

  [HttpPut]
  public ActionResult Update(Account account)
  {
    try
    {
      var result = _repo.Update(account);
      return result == 0 ?
      Ok(new { statusCode = 204, message = $"NIK {account.NIK} not found!" }) :
      Ok(new { statusCode = 201, message = "Update Succesfully!" });
    }
    catch
    {
      return BadRequest(new { statusCode = 500, message = "Something Wrong!" });
    }
  }

  [HttpDelete]

  public ActionResult Delete(string id)
  {
    try
    {
      var result = _repo.Delete(id);
      return result == 0 ? Ok(new { statusCode = 204, message = $"Id {id} Data Not Found" }) :
      Ok(new { statusCode = 201, message = "Data Delete Succesfully!" });
    }
    catch (Exception e)
    {
      return BadRequest(new { statusCode = 500, message = $"Something Wrong {e.Message}" });
    }
  }*/
}
