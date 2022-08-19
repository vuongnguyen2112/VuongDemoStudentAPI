using Microsoft.AspNetCore.Mvc;
using VuongDemoAPI.DTO;
using VuongDemoAPI.Services.AuthService;

namespace VuongDemoAPI.Controllers
{
    [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
      _authRepository = authRepository;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Response<int>>> Register(UserRegisterDTO request)
    {
      var response = await _authRepository.Register(
        new User() { UserName = request.Username }, request.Password
        );
      if (!response.Success)
      {
        return BadRequest(response);
      }
      return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<Response<string>>> Login(UserLoginDTO request)
    {
      var response = await _authRepository.Login(request.Username, request.Password);
      if (!response.Success)
      {
        return BadRequest(response);
      }
      return Ok(response);
    }
  }
}