/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Business_Layer;
using Domain.Accounts;
using Domain.ProjectLogics.Steps.Questions;
using Domain.ProjectLogics.Steps.Questions.Answers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class AnswersController : Controller
{

    private readonly AnswerManager _answerManager;
    private readonly QuestionManager _questionManager;
    private readonly UnitOfWork _uow;
    
    public AnswersController(AnswerManager answerManager, QuestionManager questionManager, UnitOfWork uow)
    {
        _answerManager = answerManager;
        _questionManager = questionManager;
        _uow = uow;
    }
    
    [HttpPost("addanswer/{flowId:long}/{stepNumber:int}")]
    [Authorize(Roles = UserRoles.Facilitator)]
    public IActionResult AddAnswer(long flowId, int stepNumber, AnswerDto answerDto)
    {
        try
        {
            _uow.BeginTransaction();
            var question = _questionManager.GetQuestionByStep(flowId, stepNumber);

            var answer = answerDto.AnswerText.Length == 0 ? CreateChoiceAnswer(question, answerDto.Answers) : _answerManager.AddAnswer(question, answerDto.AnswerText);
        
            _uow.Commit();
            return CreatedAtAction("AddAnswer", answer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    private Answer CreateChoiceAnswer(QuestionBase question, ICollection<string> answers)
    {
        var choices = _questionManager.GetChoicesForQuestion(question.Id);
        
        var selectedChoices = answers.Select(answer => choices.Single(c => c.Text == answer)).ToList();
        
        var answer = _answerManager.AddAnswer(question);
        
        var selections = selectedChoices.Select(choice => new Selection
        {
            Choice = choice,
            ChoiceAnswer = (ChoiceAnswer)answer
        }).ToList();

        _answerManager.AddSelections(selections);
        return answer;
    }
}