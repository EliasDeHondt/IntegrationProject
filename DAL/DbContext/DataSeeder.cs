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
    public static void generateSingleQuestions(CodeForgeDbContext ctx)
    {
        SingleChoiceQuestion question1 = new SingleChoiceQuestion("Als jij de begroting van je stad of gemeente zou opmaken, waar zou je dan in de komende jaren vooral op inzetten? Maak 1 keuze.", 
            new List<Choice>
            {
                new Choice("Natuur en ecologie"),
                new Choice("Vrije tijd, sport, cultuur"),
                new Choice("Huisvesting"),
                new Choice("Onderwijs en kinderopvang"),
                new Choice("Gezondheidszorg en welzijn"),
                new Choice("Verkeersveiligheid en mobiliteit"),
                new Choice("Ondersteunen van lokale handel")
            });
        SingleChoiceQuestion question2 = new SingleChoiceQuestion("Er moet meer geïnvesteerd worden in overdekte fietsstallingen aan de bushaltes in onze gemeente.\" Wat vind jij van dit voorstel?", 
            new List<Choice>
            {
                new Choice("Eens"),
                new Choice("Oneens") 
            });
        SingleChoiceQuestion question3 = new SingleChoiceQuestion("Waarop wil jij dat de focus wordt gelegd in het nieuwe stadspark? Maak 1 keuze.", 
            new List<Choice>{
                new Choice("Sportinfrastructuur"),
                new Choice("Speeltuin voor kinderen"),
                new Choice("Zitbanken en picknickplaatsen"),
                new Choice("Ruimte voor kleine evenementen"),
                new Choice("Drank- en eetmogelijkheden")
                
            });
        SingleChoiceQuestion question4 = new SingleChoiceQuestion("Hoe sta jij tegenover deze stelling? “Mijn stad moet meer investeren in fietspaden.", 
            new List<Choice>
            {
                new Choice("Akkoord"),
                new Choice("Niet Akkoord") 
            });
        SingleChoiceQuestion question5 = new SingleChoiceQuestion("Om ons allemaal veilig en vlot te verplaatsen, moet er in jouw stad of gemeente vooral meer aandacht gaan naar:",
            new List<Choice>
            {
                new Choice("Verplaatsingen met de fiets"),
                new Choice("Verplaatsingen met de auto/moto"),
                new Choice("Verplaatsingen te voet"),
                new Choice("Deelmobiliteit"),
                new Choice("Openbaar vervoer")
            });
        SingleChoiceQuestion question6 = new SingleChoiceQuestion("Wat vind jij van het idee om alle leerlingen van de scholen in onze stad een gratis fiets aan te bieden?", 
            new List<Choice>
            {
                new Choice("Goed idee"),
                new Choice("Slecht idee") 
            });
        
        ctx.SingleChoiceQuestions.Add(question1);
        ctx.SingleChoiceQuestions.Add(question2);
        ctx.SingleChoiceQuestions.Add(question3);
        ctx.SingleChoiceQuestions.Add(question4);
        ctx.SingleChoiceQuestions.Add(question5);
        ctx.SingleChoiceQuestions.Add(question6);
    }

    public static void generateMultipleCQuestions(CodeForgeDbContext ctx)
    {
        MultipleChoiceQuestion mquestion1 = new MultipleChoiceQuestion("Wat zou jou helpen om een keuze te maken tussen de verschillende partijen?", 
            new List<Choice> 
            {
                new Choice("Meer lessen op school rond de partijprogramma’s"),
                new Choice("Activiteiten in mijn jeugdclub, sportclub… rond de verkiezingen"),
                new Choice("Een bezoek van de partijen aan mijn school, jeugd/sportclub, …"),
                new Choice("Een gesprek met mijn ouders rond de gemeentepolitiek"),
                new Choice("Een debat georganiseerd door een jeugdhuis met de verschillende partijen")
            });
        MultipleChoiceQuestion mquestion2 = new MultipleChoiceQuestion("Welke sportactiviteit(en) zou jij graag in je eigen stad of gemeente kunnen beoefenen?", 
            new List<Choice>{
                new Choice("Tennis"),
                new Choice("Hockey"),
                new Choice("Padel"),
                new Choice("Voetbal"),
                new Choice("Fitness")
                
            });
        MultipleChoiceQuestion mquestion3 = new MultipleChoiceQuestion("Aan welke van deze activiteiten zou jij meedoen, om mee te wegen op het beleid van jouw stad of gemeente?", 
            new List<Choice>
            {
                new Choice("Deelnemen aan gespreksavonden met schepenen en burgemeester"),
                new Choice("Bijwonen van een gemeenteraad"),
                new Choice("Deelnemen aan een survey uitgestuurd door de stad of gemeente"),
                new Choice("Een overleg waarbij ik onderwerpen kan aandragen die voor jongeren belangrijk zijn"),
                new Choice("Mee brainstormen over oplossingen voor problemen waar jongeren mee worstelen")
            });
        MultipleChoiceQuestion mquestion4 = new MultipleChoiceQuestion("Jij gaf aan dat je waarschijnlijk niet zal gaan stemmen. Om welke reden(en) zeg je dit?", 
            new List<Choice>
            {
                new Choice("Ik heb geen interesse"),
                new Choice("Ik heb geen tijd om te gaan stemmen"),
                new Choice("Ik kan niet naar het stemkantoor gaan"),
                new Choice("Ik denk niet dat mijn stem een verschil zal uitmaken"),
                new Choice("Ik heb geen idee voor wie ik zou moeten stemmen")
            });
        MultipleChoiceQuestion mquestion5 = new MultipleChoiceQuestion("Wat zou jou (meer) zin geven om te gaan stemmen?", 
            new List<Choice>
            {
                new Choice("Kunnen gaan stemmen op een toffere locatie"),
                new Choice("Online, van thuis uit kunnen stemmen"),
                new Choice("Betere inhoudelijke voorstellen van de politieke partijen"),
                new Choice("Meer aandacht voor jeugd in de programma’s van de partijen"),
                new Choice("Beter weten of mijn stem echt impact heeft")
            });

        ctx.MultipleChoiceQuestions.Add(mquestion1);
        ctx.MultipleChoiceQuestions.Add(mquestion2);
        ctx.MultipleChoiceQuestions.Add(mquestion3);
        ctx.MultipleChoiceQuestions.Add(mquestion4);
        ctx.MultipleChoiceQuestions.Add(mquestion5);
    }

    public static void generateRangeQuestions(CodeForgeDbContext ctx)
    {
        RangeQuestion rquestion1 = new RangeQuestion("Ben jij van plan om te gaan stemmen bij de aankomende lokale verkiezingen?", 
            new List<Choice>
            {
                new Choice("Zeker niet"),
                new Choice("Eerder niet"),
                new Choice("Ik weet het nog niet"),
                new Choice("Eerder wel"),
                new Choice("Zeker wel")
            });
        RangeQuestion rquestion2 = new RangeQuestion("Voel jij je betrokken bij het beleid dat wordt uitgestippeld door je gemeente of stad?", 
            new List<Choice>
            {
                new Choice("Ik voel me weinig tot niet betrokken"),
                new Choice("Ik voel me weinig betrokken"),
                new Choice("Ik voel me betrokken"),
                new Choice("Ik voel me zeer betrokken")
            });
        RangeQuestion rquestion3 = new RangeQuestion("In hoeverre ben jij tevreden met het vrijetijdsaanbod in jouw stad of gemeente?", 
            new List<Choice>
            {
                new Choice("Zeer ontevreden"),
                new Choice("Ontevreden"),
                new Choice("Niet tevreden en niet ontevreden"),
                new Choice("Tevreden"),
                new Choice("Zeer tevreden")
            });
        RangeQuestion rquestion4 = new RangeQuestion("In welke mate ben jij het eens met de volgende stelling: “Mijn stad of gemeente doet voldoende om betaalbare huisvesting mogelijk te maken voor iedereen.”", 
            new List<Choice>
            {
                new Choice("Helemaal oneens"),
                new Choice("Oneens"),
                new Choice("Noch eens, noch oneens"),
                new Choice("Eens"),
                new Choice("Helemaal eens")
            });
        RangeQuestion rquestion5 = new RangeQuestion("In welke mate kun jij je vinden in het voorstel om de straatlichten in onze gemeente te doven tussen 23u en 5u?", 
            new List<Choice>
            {
                new Choice("Ik sta hier volledig achter"),
                new Choice("Ik sta hier een beetje achter"),
                new Choice("Ik sta hier niet achter"),
                new Choice("Ik sta hier helemaal niet achter")
            });

        ctx.RangeQuestions.Add(rquestion1);
        ctx.RangeQuestions.Add(rquestion2);
        ctx.RangeQuestions.Add(rquestion3);
        ctx.RangeQuestions.Add(rquestion4);
        ctx.RangeQuestions.Add(rquestion5);
    }

    public static void generateOpenQuestions(CodeForgeDbContext ctx)
    {
        OpenQuestion oquesstion1 = new OpenQuestion("Je bent schepen van onderwijs voor een dag: waar zet je dan vooral op in?",50);
        OpenQuestion oquesstion2 = new OpenQuestion("Als je één ding mag wensen voor het nieuwe stadspark, wat zou jouw droomstadspark dan zeker bevatten?",60);

        ctx.OpenQuestions.Add(oquesstion1);
        ctx.OpenQuestions.Add(oquesstion2);
    }
    public static void Seed(CodeForgeDbContext ctx)
    {  
        // Seed Project and Main theme
        MainTheme mainTheme1 = new MainTheme("Lokale Verkiezingen");
        Flow flow = new Flow(FlowType.LINEAR, mainTheme1);
        Text textInfo = new Text("This is example text");
        Image imageInfo = new Image("../MVC/Assets/Images/TestImage.jpg");
        Video videoInfo = new Video("/Assets/Videos/Rocket league in Wheelchair meme.mp4");
        
        generateSingleQuestions(ctx);
        generateMultipleCQuestions(ctx);
        generateRangeQuestions(ctx);
        generateOpenQuestions(ctx);
        
        // ctx.SaveChanges();
        // ctx.ChangeTracker.Clear();
        
        InformationStep step1 = new InformationStep(1, textInfo, flow);
        InformationStep step2 = new InformationStep(2, imageInfo, flow);
        CombinedStep step3 = new CombinedStep(3, textInfo, ctx.SingleChoiceQuestions.First(), flow);//no elements
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
        
        ctx.InformationSteps.Add(step1);
        ctx.InformationSteps.Add(step2);
        ctx.CombinedSteps.Add(step3);
        ctx.InformationSteps.Add(step4);
        ctx.SaveChanges();
        ctx.ChangeTracker.Clear();
    }
}