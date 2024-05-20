using Business_Layer;
using Domain.Accounts;
using Domain.WebApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.feedModels;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class IdeasController : Controller
{
    
    private readonly IdeaManager _manager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly FeedManager _feedManager;
    private readonly UnitOfWork _uow;

    public IdeasController(IdeaManager manager, UserManager<IdentityUser> userManager, FeedManager feedManager, UnitOfWork uow)
    {
        _manager = manager;
        _userManager = userManager;
        _feedManager = feedManager;
        _uow = uow;
    }
    
    [HttpPost("createIdea/{feedId}")]
    public async Task<IActionResult> PostIdea(IdeaModel model, long feedId)
    {
        _uow.BeginTransaction();
        var user = await _userManager.GetUserAsync(User) as WebAppUser;
        var feed = _feedManager.GetFeed(feedId);
        var idea = _manager.AddIdea(model.Text, user!, feed);
        _uow.Commit();
        
        return CreatedAtAction(nameof(PostIdea), CreateIdeaModel(idea));
    }

    private static IdeaModel CreateIdeaModel(Idea idea)
    {
        return new IdeaModel
        {
            Id = idea.Id,
            Text = idea.Text,
            author = new AuthorModel
            {
                Email = idea.Author.Email!,
                Name = idea.Author.UserName!
            },
            likes = idea.Likes.Select(like => new LikeModel
            {
                liker = new AuthorModel
                {
                    Email = like.WebAppUser.Email!,
                    Name = like.WebAppUser.UserName!
                }
            })
        };
    }
    
}