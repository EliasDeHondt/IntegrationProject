using Business_Layer;
using Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.feedModels;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class ReactionsController : Controller
{

    private readonly ReactionManager _manager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IdeaManager _ideaManager;
    private readonly UnitOfWork _uow;
    
    
    public ReactionsController(ReactionManager manager, UnitOfWork uow, UserManager<IdentityUser> userManager, IdeaManager ideaManager)
    {
        _manager = manager;
        _uow = uow;
        _userManager = userManager;
        _ideaManager = ideaManager;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddReaction(ReactionDto model)
    {
        _uow.BeginTransaction();
        var idea = _ideaManager.GetIdea(model.IdeaId);
        var user = await _userManager.GetUserAsync(User) as WebAppUser;
        var reaction = _manager.AddReaction(idea, user!, model.Text);
        _uow.Commit();
        ReactionModel reactionModel = new ReactionModel
        {
            Author = new AuthorModel
            {
                Name = reaction.WebAppUser.UserName!
            },
            Text = reaction.Text
        };
        return CreatedAtAction(nameof(AddReaction), reactionModel);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetReactionsForIdea(long id)
    {
        var reactions = _manager.GetReactionsForIdea(id);
        return Ok(reactions.Select(reaction => new ReactionModel
        {
            Author = new AuthorModel
            {
                Name = reaction.WebAppUser.UserName!
            },
            Text = reaction.Text
        }));
    }
    
    
}