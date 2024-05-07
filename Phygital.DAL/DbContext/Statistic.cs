using Data_Access_Layer;
using Data_Access_Layer.DbContext;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.Platform;
using FileHelpers;
public class Statistic
{
    //todo: sprint 3
    //public AnswerRepository AnswerRepository;
   // private CodeForgeDbContext _ctx;

    // public Flow[] geta()
    // {
    //     return _ctx.Flows.ToArray();
    // }
     
    public void testPacket()
    {
        //AnswerRepository.
        // Person[] persons = new Person[]
        // {
        //     new Person { Id = 1, Name = "John", Age = 30 },
        //     new Person { Id = 2, Name = "Alice", Age = 25 },
        //     new Person { Id = 3, Name = "Bob", Age = 40 }
        // };

        //Flow f = new Flow(FlowType.Linear, new MainTheme());
        QuestionBase[] flows = new QuestionBase[]{new OpenQuestion(),new RangeQuestion()};
        var fileHelperEngine = new FileHelperEngine<QuestionBase>();
        fileHelperEngine.WriteFile("testoutput.csv", flows);
    }
}