using Microsoft.AspNetCore.Mvc;
using Note_App_API.Models;
using Note_App_API.Services;

namespace Note_App_API.Controllers;
[ApiController]
[Route("api/noteapp/acccount")]
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
        var userId = _service.registerAccount(dto);

        return Created($"{userId}", null);
    }

}