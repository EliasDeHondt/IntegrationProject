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
    public static void generateTextInfo()
    {
        
    }
    public static void Seed(CodeForgeDbContext ctx)
    {  
        // Seed Project and Main theme
        MainTheme mainTheme1 = new MainTheme("Lokale Verkiezingen");
        Flow flow = new Flow(FlowType.LINEAR, mainTheme1);
        Text textInfo = new Text("This is an information text");
        Image imageInfo = new Image("../MVC/Assets/Images/TestImage.jpg");
        Video videoInfo = new Video("/Assets/Videos/Rocket league in Wheelchair meme.mp4");
        
        SingleChoiceQuestion question1 = new SingleChoiceQuestion("Als jij de begroting van je stad of gemeente zou opmaken, waar zou je dan in de komende jaren vooral op inzetten? Maak 1 keuze.", new List<Choice>());
        SingleChoiceQuestion question2 = new SingleChoiceQuestion("Er moet meer geïnvesteerd worden in overdekte fietsstallingen aan de bushaltes in onze gemeente.\" Wat vind jij van dit voorstel?", new List<Choice>());
        SingleChoiceQuestion question3 = new SingleChoiceQuestion("Waarop wil jij dat de focus wordt gelegd in het nieuwe stadspark? Maak 1 keuze.", new List<Choice>());
        SingleChoiceQuestion question4 = new SingleChoiceQuestion("Hoe sta jij tegenover deze stelling? “Mijn stad moet meer investeren in fietspaden.", new List<Choice>());
        SingleChoiceQuestion question5 = new SingleChoiceQuestion("Om ons allemaal veilig en vlot te verplaatsen, moet er in jouw stad of gemeente vooral meer aandacht gaan naar:", new List<Choice>());
        SingleChoiceQuestion question6 = new SingleChoiceQuestion("Wat vind jij van het idee om alle leerlingen van de scholen in onze stad een gratis fiets aan te bieden?", new List<Choice>());
        
        MultipleChoiceQuestion mquestion1 = new MultipleChoiceQuestion("Wat zou jou helpen om een keuze te maken tussen de verschillende partijen?", new List<Choice>());
        MultipleChoiceQuestion mquestion2 = new MultipleChoiceQuestion("Welke sportactiviteit(en) zou jij graag in je eigen stad of gemeente kunnen beoefenen?", new List<Choice>());
        MultipleChoiceQuestion mquestion3 = new MultipleChoiceQuestion("Aan welke van deze activiteiten zou jij meedoen, om mee te wegen op het beleid van jouw stad of gemeente?", new List<Choice>());
        MultipleChoiceQuestion mquestion4 = new MultipleChoiceQuestion("Jij gaf aan dat je waarschijnlijk niet zal gaan stemmen. Om welke reden(en) zeg je dit?", new List<Choice>());
        MultipleChoiceQuestion mquestion5 = new MultipleChoiceQuestion("Wat zou jou (meer) zin geven om te gaan stemmen?", new List<Choice>());
        
        RangeQuestion rquestion1 = new RangeQuestion("Ben jij van plan om te gaan stemmen bij de aankomende lokale verkiezingen?", new List<Choice>());
        RangeQuestion rquestion2 = new RangeQuestion("Voel jij je betrokken bij het beleid dat wordt uitgestippeld door je gemeente of stad?", new List<Choice>());
        RangeQuestion rquestion3 = new RangeQuestion("In hoeverre ben jij tevreden met het vrijetijdsaanbod in jouw stad of gemeente?", new List<Choice>());
        RangeQuestion rquestion4 = new RangeQuestion("In welke mate ben jij het eens met de volgende stelling: “Mijn stad of gemeente doet voldoende om betaalbare huisvesting mogelijk te maken voor iedereen.”", new List<Choice>());
        RangeQuestion rquestion5 = new RangeQuestion("In welke mate kun jij je vinden in het voorstel om de straatlichten in onze gemeente te doven tussen 23u en 5u?", new List<Choice>());

        OpenQuestion oquesstion1 = new OpenQuestion("Je bent schepen van onderwijs voor een dag: waar zet je dan vooral op in?",50);
        OpenQuestion oquesstion2 = new OpenQuestion("Als je één ding mag wensen voor het nieuwe stadspark, wat zou jouw droomstadspark dan zeker bevatten?",50);
            
        InformationStep step1 = new InformationStep(1, textInfo, flow);
        InformationStep step2 = new InformationStep(2, imageInfo, flow);
        CombinedStep step3 = new CombinedStep(3, textInfo, question1, flow);
        InformationStep step4 = new InformationStep(4, videoInfo, flow);
        
        
        
        flow.Steps.Add(step1);
        flow.Steps.Add(step2);
        flow.Steps.Add(step3);
        flow.Steps.Add(step4);
        mainTheme1.Flows.Add(flow);
        Project project1 = new Project(mainTheme1);

        ctx.MainThemes.Add(mainTheme1);
        ctx.Flows.Add(flow);
        ctx.Projects.Add(project1);
        ctx.Texts.Add(textInfo);
        ctx.Images.Add(imageInfo);
        ctx.SingleChoiceQuestions.Add(question1);
        ctx.InformationSteps.Add(step1);
        ctx.InformationSteps.Add(step2);
        ctx.CombinedSteps.Add(step3);
        ctx.InformationSteps.Add(step4);
        ctx.SaveChanges();
        ctx.ChangeTracker.Clear();
    }
}