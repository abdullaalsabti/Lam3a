using FirebaseAdmin.Auth;
using Lam3a.Data;
using Lam3a.Data.Entities;
using Lam3a.Data.ValueObjects;
using Lam3a.Dto;
using Lam3a.Services.Authentication;
using Lam3a.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

namespace Lam3a.Controllers;

[ApiController]
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

    //SECTION: REGISTER

    [HttpPost("register", Name = "Register")]
    public async Task<IActionResult> Register([FromBody] RegisterCredentialsDto registerDto)
    {
        try
        {
            // Check if user already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == registerDto.Email
            );
            if (existingUser != null)
                return Conflict(new { message = "Email is already registered." });

            // Generate password hash and salt
            var salt = AuthService.GeneratePasswordSalt();
            var hash = AuthService.GeneratePasswordHash(registerDto.Password, salt);

            var returnedId = "";

            // Create new user entity
            if (registerDto.Role == Role.Client)
            {
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
                    UserAccountStatus = UserAccountStatus.Unverified,
                    Address = new Address { BuildingNumber = "", Street = "" },
                };

                returnedId = _context.Clients.Add(newUser).Entity.UserId.ToString();
            }
            else if (registerDto.Role == Role.Provider)
            {
                var newUser = new ServiceProvider
                {
                    Email = registerDto.Email,
                    Role = registerDto.Role,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    FirstName = "",
                    LastName = "",
                    Phone = "",
                    Gender = Gender.Male,
                    UserAccountStatus = UserAccountStatus.Unverified,
                    Address = new Address { BuildingNumber = "", Street = "" },
                };

                returnedId = _context.ServiceProviders.Add(newUser).Entity.UserId.ToString();
            }

            await _context.SaveChangesAsync();

            return Ok(
                new
                {
                    message = "User registered successfully. Please verify your phone number.",
                    userId = returnedId,
                }
            );
        }
        catch (DbUpdateException dbEx)
        {
            // Handle database-specific errors
            return StatusCode(500, new { message = "Database error: " + dbEx.Message });
        }
        catch (Exception ex)
        {
            // Handle any other errors
            return StatusCode(500, new { message = "An unexpected error occurred: " + ex.Message });
        }
    }

    //SECTION: SEND OTP
    //TODO: CHECK THIS AFTER FRONTEND IS IMPLEMENTED!!!
    [HttpPost("verify-phone")]
    public async Task<IActionResult> VerifyPhone([FromBody] VerifyPhoneDto dto)
    {
        try
        {
            // Verify the Firebase ID token
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(
                dto.FirebaseIdToken
            );

            if (!decodedToken.Claims.TryGetValue("phone_number", out var phoneNumberObject))
                return BadRequest(new { message = "Phone number not found in Firebase token." });

            var phoneNumber = phoneNumberObject.ToString();

            if (dto.Phone != phoneNumber)
                return BadRequest(new { message = "Phone number mismatch." });

            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest(new { message = "Phone number not verified in Firebase." });

            // Find user in & Update hos mobile phone number DB
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
                return NotFound(new { message = "User not found." });

            // Update user record
            user.Phone = phoneNumber;
            user.UserAccountStatus = UserAccountStatus.Verified;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Phone verified successfully." });
        }
        catch (FirebaseAuthException ex)
        {
            return BadRequest(new { message = $"Invalid Firebase token: {ex.Message}" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    //SECTION: LOGIN
    [HttpPost("login", Name = "Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (
            user == null
            || !AuthService.VerifyPasswordHash(
                loginDto.Password,
                user.PasswordHash,
                user.PasswordSalt
            )
        )
            return Unauthorized(new { message = "Invalid credentials" });

        // Generate JWT
        var jwt = AuthService.GenerateJwtToken(
            user.UserId.ToString(),
            user.Email,
            user.Role.ToString()
        );

        // Generate Refresh Token
        var refreshToken = AuthService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            UserId = user.UserId,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(30), //duration of refresh token
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false,
            IsUsed = false,
        };

        await _context.RefreshTokens.AddAsync(refreshTokenEntity);
        await _context.SaveChangesAsync();

        return Ok(new { token = jwt, refreshToken });
    }

    //SECTION: REFRESH TOKEN
    [HttpPost("refreshToken", Name = "RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
    {
        var refreshTokenEntity = await _context.RefreshTokens.FirstOrDefaultAsync(rt =>
            rt.Token == dto.RefreshToken
        );

        if (
            refreshTokenEntity == null
            || refreshTokenEntity.IsRevoked
            || refreshTokenEntity.IsUsed
            || refreshTokenEntity.ExpiresAt < DateTime.UtcNow
        )
            return Unauthorized("Invalid or expired refresh token");

        // Mark as used
        refreshTokenEntity.IsUsed = true;

        // Generate new JWT
        var user = await _context.Users.FindAsync(refreshTokenEntity.UserId);

        if (user == null)
            BadRequest("User not found." + "");
        var jwt = AuthService.GenerateJwtToken(
            user.UserId.ToString(),
            user.Email,
            user.Role.ToString()
        );

        // Optionally generate a new refresh token and save
        var newRefreshToken = AuthService.GenerateRefreshToken();
        var newRefreshTokenEntity = new RefreshToken
        {
            UserId = user.UserId,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false,
            IsUsed = false,
        };
        await _context.RefreshTokens.AddAsync(newRefreshTokenEntity);

        await _context.SaveChangesAsync();

        return Ok(new { token = jwt, refreshToken = newRefreshToken });
    }
}