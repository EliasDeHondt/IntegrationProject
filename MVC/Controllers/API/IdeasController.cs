using Business_Layer;
using Domain.Accounts;
using Domain.WebApp;
using Microsoft.AspNetCore.Authorization;
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
        try
        {
            _uow.BeginTransaction();
            var user = await _userManager.GetUserAsync(User) as WebAppUser;
            var feed = _feedManager.GetFeed(feedId);
            var idea = _manager.AddIdea(model.Text, user, feed, model.image);
            _uow.Commit();
            return CreatedAtAction(nameof(PostIdea), CreateIdeaModel(idea));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPost("likeIdea/{ideaId}")]
    [Authorize]
    public async Task<IActionResult> LikeIdea(long ideaId)
    {
        try
        {
            _uow.BeginTransaction();
            var user = await _userManager.GetUserAsync(User) as WebAppUser;
            var idea = _manager.GetIdea(ideaId);
            var like = _manager.LikeIdea(idea, user!);
            _uow.Commit();

            return CreatedAtAction(nameof(LikeIdea), new LikeModel
            {
                liker = new AuthorModel
                {
                    Email = like.WebAppUser.Email!,
                    Name = like.WebAppUser.UserName!
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpDelete("unlikeIdea/{ideaId}")]
    [Authorize]
    public async Task<IActionResult> UnlikeIdea(long ideaId)
    {
        try
        {
            _uow.BeginTransaction();
            var user = await _userManager.GetUserAsync(User) as WebAppUser;
            var idea = _manager.GetIdea(ideaId);
            _manager.UnlikeIdea(idea, user!);
            _uow.Commit();
        
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
    private static IdeaModel CreateIdeaModel(Idea idea)
    {
        return new IdeaModel
        {
            image = idea.Image?.Base64,
            Id = idea.Id,
            Text = idea.Text,
            author = new AuthorModel
            {
                Email = idea.Author?.Email,
                Name = idea.Author?.UserName
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