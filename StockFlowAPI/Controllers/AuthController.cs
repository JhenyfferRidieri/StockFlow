using Microsoft.AspNetCore.Mvc;
using StockFlowAPI.Models.Dto;
using StockFlowAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace StockFlowAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto.Name, dto.Email, dto.Password, dto.Role);
            if (!result) return BadRequest("E-mail já cadastrado.");
            return Ok("Usuário registrado com sucesso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var token = await _authService.LoginAsync(dto.Email, dto.Password);
            if (token == null) return Unauthorized("Credenciais inválidas.");
            return Ok(new { token });
        }
    }
}
