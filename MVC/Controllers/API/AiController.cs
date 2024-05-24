using System.Text.Json.Nodes;
using Business_Layer;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class AiController : Controller
{

    private readonly IGroqApiClient _client;
    
    public AiController()
    {
        _client = new GroqApiClient(Environment.GetEnvironmentVariable("GROQ_API_KEY")!);
    }
    
    [HttpPost("CheckIdeaText")]
    public async Task<IActionResult> CheckIdeaText(IdeaTextDto text)
    {
        JsonObject request = new()
        {
            ["model"] = "llama3-70b-8192",
            ["temperature"] = 0,
            ["messages"] = new JsonArray
            {
                new JsonObject
                {
                  ["role"] = "system",
                  ["content"] = "Your role is to act as a content moderator for an online platform. Your task is to label comments as 'Toxicity', 'Hate Speech', 'Threats' or 'Safe' based on if they contain rude, discriminatory, or threatening language. Use the following criteria: Toxicity - Rude, disrespectful, overly negative comments, Hate Speech - Racist, sexist, homophobic, discriminatory language, Threats - Violent, graphic, or directly harmful statement. " +
                                "You can be pretty laid back with this. " +
                                "People are allowed to use different languages. " +
                                "Only answer with the label. Do not add any other text. "
                },
                new JsonObject
                {
                    ["role"] = "user",
                    ["content"] = "Validate this message: " + text.Text
                }
            }
        };

        JsonObject? result = await _client.CreateChatCompletionAsync(request);
        string response = result?["choices"]?[0]?["message"]?["content"]?.ToString() ?? "No response found";
        return Ok(response);    
    }

}