using System.Security.Claims;
using Business_Layer;
using Domain.WebApp;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.feedModels;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class FeedsController : Controller
{

    private readonly FeedManager _manager;
    private readonly CustomUserManager _userManager;
    
    public FeedsController(FeedManager feedManager, CustomUserManager userManager)
    {
        _manager = feedManager;
        _userManager = userManager;
    }
    
    [HttpGet("{id}")]
    public IActionResult GetFeed(long id)
    {
        var feed = _manager.GetFeedFromIdWithIdeas(id);

        var feedModel = new FeedModel
        {
            Ideas = CreateIdeaModels(feed.Ideas),
            Title = feed.Project.Title
        };

        return Ok(feedModel);
    }

    [HttpGet("random")]
    public IActionResult GetRandomFeedForUser()
    {
        var randomId = _userManager.GetRandomFeedIdForUser(User.FindFirstValue(ClaimTypes.Email)!);
        return RedirectToAction("GetFeed", new { id = randomId});
    }
    
    [HttpGet("ids")]
    public IActionResult GetFeedIdsForUser()
    {
        var feeds = _userManager.GetFeedForUserWithProject(User.FindFirstValue(ClaimTypes.Email)!);
        return Ok(feeds.Select(feed => new FeedModel
        {
            Title = feed.Project.Title,
            Id = feed.Id
        }));
    }
    
    private IEnumerable<IdeaModel> CreateIdeaModels(ICollection<Idea> ideas)
    {
        return ideas.Select(idea => new IdeaModel
        {
            Id = idea.Id,
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
            }),
            Text = idea.Text
        });
    }
    
    
}