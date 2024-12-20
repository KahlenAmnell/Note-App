﻿using Microsoft.AspNetCore.Mvc;
using Note_App_API.Models;
using Note_App_API.Services;

namespace Note_App_API.Controllers;
[ApiController]
[Route("api/account")]
public class AccountController: ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] CreateAccountDto dto)
    {
        var newUserName = await _service.registerAccount(dto);

        return Created($"User {newUserName} account created", null);
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDto dto)
    {
        var token = _service.GenerateJwt(dto);
        return Ok(token);
    }

}