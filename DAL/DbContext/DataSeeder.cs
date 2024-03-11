/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Data_Access_Layer.DbContext;

public class DataSeeder
{
    public static void Seed(CodeForgeDbContext ctx)
    {  
        // Seed Project and Main theme
        MainTheme mainTheme1 = new MainTheme("Climate Change");
        Flow flow = new Flow(FlowType.LINEAR, mainTheme1);
        Text textInfo = new Text("This is an information text");
        Image imageInfo = new Image("../MVC/Assets/Images/TestImage.jpg");
        SingleChoiceQuestion question = new SingleChoiceQuestion("", new List<Choice>());
        InformationStep step1 = new InformationStep(1, textInfo, flow);
        InformationStep step2 = new InformationStep(2, imageInfo, flow);
        CombinedStep step3 = new CombinedStep(3, textInfo, question, flow);
        flow.Steps.Add(step1);
        flow.Steps.Add(step2);
        flow.Steps.Add(step3);
        mainTheme1.Flows.Add(flow);
        Project project1 = new Project(mainTheme1);

        ctx.MainThemes.Add(mainTheme1);
        ctx.Flows.Add(flow);
        ctx.Projects.Add(project1);
        ctx.Texts.Add(textInfo);
        ctx.Images.Add(imageInfo);
        ctx.SingleChoiceQuestions.Add(question);
        ctx.InformationSteps.Add(step1);
        ctx.InformationSteps.Add(step2);
        ctx.CombinedSteps.Add(step3);
        ctx.SaveChanges();
        ctx.ChangeTracker.Clear();
    }
}