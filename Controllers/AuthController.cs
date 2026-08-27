using Library_management_system.DTOs;
using Library_management_system.Models.Database;
using Library_management_system.Models.Response;
using Library_management_system.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using customExceptionNS = Library_management_system.Models.Exceptions;

namespace Library_management_system.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly UserServices _userServices;

    public AuthController(UserServices services) {
        _userServices= services;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto) {
        try
        {
           
            var result = await _userServices.RegisterAsync(dto);
            return Ok(new Response
            {
                Data = result,
                ResponseCode = 200,
                Success = true,
            });

        }
        catch(customExceptionNS.CustomExceptions ex) {
            return StatusCode(ex.StatusCode, new Response
            {
                Data = null,
                ResponseCode = 400,
                Success = false,
                ResponseMessage=ex.Message
            });
        }
    
    }
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto) {

        try {
            var result = await _userServices.LoginAsync(dto);
            return Ok(new Response
            {
                Data = result,
                ResponseCode = 200,
                Success = true

            });

        } catch(customExceptionNS.CustomExceptions ex) {
            return StatusCode(ex.StatusCode ,new Response
            {
                Data = null,
                ResponseCode = 400,
                Success = false,
                ResponseMessage=ex.Message

            });
        }
    }
}

