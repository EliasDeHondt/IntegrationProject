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
        
        Text textInfo = new Text("This is an information text");
        Image imageInfo = new Image("../MVC/Assets/Images/TestImage.jpg");
        Video videoInfo = new Video("/Assets/Videos/Rocket league in Wheelchair meme.mp4");
        SingleChoiceQuestion question = new SingleChoiceQuestion("", new List<Choice>());
        
        Project project1 = new Project(mainTheme1);
        
        Flow flow2 = new Flow(FlowType.LINEAR, mainTheme1);
        Text textInfo2 = new Text("This is more information text");
        Image imageInfo2 = new Image("../MVC/Assets/Images/TestImage2.png");
        Video videoInfo2 = new Video("/Assets/Videos/Rocket league in Wheelchair meme.mp4");
        SingleChoiceQuestion question2 = new SingleChoiceQuestion("", new List<Choice>());
        InformationStep step5 = new InformationStep(1, textInfo2, flow2);
        InformationStep step6 = new InformationStep(2, imageInfo2, flow2);
        CombinedStep step7 = new CombinedStep(3, textInfo2, question2, flow2);
        InformationStep step8 = new InformationStep(4, videoInfo2, flow2);
        flow2.Steps.Add(step5);
        flow2.Steps.Add(step6);
        flow2.Steps.Add(step7);
        flow2.Steps.Add(step8);

        ctx.MainThemes.Add(mainTheme1);
        

        // Seed subtheme and extra main theme
        SubTheme subTheme1 = new SubTheme("Causes", mainTheme1);
        Flow flow = new Flow(FlowType.LINEAR, subTheme1);
        InformationStep step1 = new InformationStep(1, textInfo, flow);
        InformationStep step2 = new InformationStep(2, imageInfo, flow);
        CombinedStep step3 = new CombinedStep(3, textInfo, question, flow);
        InformationStep step4 = new InformationStep(4, videoInfo, flow);
        ctx.Flows.Add(flow);
        ctx.Projects.Add(project1);
        ctx.Texts.Add(textInfo);
        ctx.Images.Add(imageInfo);
        ctx.SingleChoiceQuestions.Add(question);
        ctx.InformationSteps.Add(step1);
        ctx.InformationSteps.Add(step2);
        ctx.CombinedSteps.Add(step3);
        ctx.InformationSteps.Add(step4);
        flow.Steps.Add(step1);
        flow.Steps.Add(step2);
        flow.Steps.Add(step3);
        flow.Steps.Add(step4);
        subTheme1.MainTheme = mainTheme1;
        subTheme1.Flows.Add(flow);
        flow.Theme = subTheme1;
        subTheme1.Flows.Add(flow2);
        flow2.Theme = subTheme1;
        mainTheme1.Themes.Add(subTheme1);
        ctx.SubThemes.Add(subTheme1);
        MainTheme mainTheme2 = new MainTheme("Renewable energy");
        Project project2 = new Project(mainTheme2);
        ctx.MainThemes.Add(mainTheme2);
        ctx.Projects.Add(project2);
        
        ctx.SaveChanges();
        ctx.ChangeTracker.Clear();
    }
}