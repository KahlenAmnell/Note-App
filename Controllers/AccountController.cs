using Microsoft.AspNetCore.Mvc;
using Note_App_API.Services;

namespace Note_App_API.Controllers;

public class AccountController: ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }
}