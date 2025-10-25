using Lam3a.Data;
using Lam3a.Data.Entities;
using Lam3a.Data.ValueObjects;
using Lam3a.Dto;
using Lam3a.Services.Authentication;
using Lam3a.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lam3a.Controllers;

[Controller]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly DataContextEf _context;
    private readonly ILogger _logger;

    public AuthController(DataContextEf context, ILogger<AuthController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost("register", Name = "Register")]
    public async Task<IActionResult> Register([FromBody] RegisterCredentialsDto registerDto)
    {
        //Start registration and create a pending user (status = “unverified”)

        var existingUser = await _context.Users.FirstOrDefaultAsync(u =>
            u.Email == registerDto.Email
        );

        if (existingUser != null)
            return Conflict(new { message = "Email is already registered." });

        var salt = AuthService.GeneratePasswordSalt();
        var hash = AuthService.GeneratePasswordHash(registerDto.Password, salt);

        var newUser = new Client
        {
            Email = registerDto.Email,
            Role = registerDto.Role,
            PasswordHash = hash,
            PasswordSalt = salt,
            FirstName = "",
            LastName = "",
            Phone = "",
            Gender = Gender.Male,
            Address = new Address { BuildingNumber = "", Street = "" },
        };

        var newlyAddedUser = _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(
            new
            {
                message = $"User registered successfully. UserId = {newlyAddedUser.Entity.UserId}, Please verify your phone number",
            }
        );
    }

    [HttpPost("send-otp", Name = "send-otp")]
    public async Task<IActionResult> SendOtp()
    {
        //Associate phone number with that user and send OTP
        return Ok();
    }

    [HttpPost("verify-otp", Name = "verify-otp")]
    public async Task<IActionResult> VerifyOtp()
    {
        //Verify phone number and activate account
        return Ok();
    }

    [HttpPost("login", Name = "Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        //do some login stuff
        return Ok();
    }

    [HttpPost("refreshToken", Name = "RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
    {
        // your logic here
        return Ok();
    }
}
