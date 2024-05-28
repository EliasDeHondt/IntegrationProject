using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.userModels;

namespace MVC.Controllers;

[Route("WebApp")]
public class WebAppController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public WebAppController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [Route("Feed")]
    [Authorize(Roles = UserRoles.Respondent)]
    public IActionResult Feed()
    {
        return View(null);
    }

    [Route("Feed/{id}")]
    public IActionResult Feed(long id)
    {
        return View(id);
    }

    [HttpGet]
    public IActionResult Register([FromQuery]long id)
    {
        return View(new RegisterDto
        {
            FeedId = id
        });
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        var user = new WebAppUser
        {
            UserName = dto.Name,
            Email = dto.Email,
            FeedIds = new List<LongValue> { new() { Value = dto.FeedId } },
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return View(dto);
        
        await _userManager.AddToRoleAsync(user, UserRoles.Respondent);
        return RedirectToPage("/Account/Login", new { area = "Identity" });
    }
}