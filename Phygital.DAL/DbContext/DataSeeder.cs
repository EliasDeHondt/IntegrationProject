﻿/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.Accounts;
using Domain.Platform;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Data_Access_Layer.DbContext;

public static class DataSeeder
{
    private static void GenerateSingleQuestions(CodeForgeDbContext ctx, Flow flow)
    { 

        SingleChoiceQuestion question1 = new SingleChoiceQuestion(
            "Als jij de begroting van je stad of gemeente zou opmaken, waar zou je dan in de komende jaren vooral op inzetten? Maak 1 keuze.");

        Choice choice = new Choice("Natuur en ecologie", question1);
        Choice choice1 = new Choice("Vrije tijd, sport, cultuur", question1);
        Choice choice2 = new Choice("Huisvesting", question1);
        Choice choice3 = new Choice("Onderwijs en kinderopvang", question1);
        Choice choice4 = new Choice("Gezondheidszorg en welzijn", question1);
        Choice choice5 = new Choice("Verkeersveiligheid en mobiliteit", question1);
        Choice choice6 = new Choice("Ondersteunen van lokale handel", question1);

  

        SingleChoiceQuestion question2 = new SingleChoiceQuestion(
            "Er moet meer geïnvesteerd worden in overdekte fietsstallingen aan de bushaltes in onze gemeente.\" Wat vind jij van dit voorstel?");

        Choice choice7 = new Choice("Eens", question2);
        Choice choice8 = new Choice("Oneens", question2);
        
        SingleChoiceQuestion question3 = new SingleChoiceQuestion(
            "Waarop wil jij dat de focus wordt gelegd in het nieuwe stadspark? Maak 1 keuze.");

        Choice choice9 = new Choice("Sportinfrastructuur", question3);
        Choice choice10 = new Choice("Speeltuin voor kinderen", question3);
        Choice choice11 = new Choice("Zitbanken en picknickplaatsen", question3);
        Choice choice12 = new Choice("Ruimte voor kleine evenementen", question3);
        Choice choice13 = new Choice("Drank- en eetmogelijkheden", question3);
        

        SingleChoiceQuestion question4 = new SingleChoiceQuestion(
            "Hoe sta jij tegenover deze stelling? “Mijn stad moet meer investeren in fietspaden.");

        Choice choice14 = new Choice("Akkoord", question4);
        Choice choice15 = new Choice("Niet Akkoord", question4);

        SingleChoiceQuestion question5 = new SingleChoiceQuestion(
            "Om ons allemaal veilig en vlot te verplaatsen, moet er in jouw stad of gemeente vooral meer aandacht gaan naar:");

        Choice choice16 = new Choice("Verplaatsingen met de fiets", question5);
        Choice choice17 = new Choice("Verplaatsingen met de auto/moto", question5);
        Choice choice18 = new Choice("Verplaatsingen te voet", question5);
        Choice choice19 = new Choice("Deelmobiliteit", question5);
        Choice choice20 = new Choice("Openbaar vervoer", question5);
        


        SingleChoiceQuestion question6 = new SingleChoiceQuestion(
            "Wat vind jij van het idee om alle leerlingen van de scholen in onze stad een gratis fiets aan te bieden?");

        Choice choice21 = new Choice("Goed idee", question6);
        Choice choice22 = new Choice("Slecht idee", question6);
        
        ctx.ChoiceQuestions.AddRange(question1, question2, question3, question4, question5, question6);
        ctx.Choices.AddRange(choice1, choice2, choice3, choice4, choice5, choice6, choice7, choice8, choice9, choice10,
            choice11, choice12, choice13, choice14, choice15, choice16, choice17, choice18, choice19, choice20,
            choice21, choice22);

        //add to step
        QuestionStep step1 = new QuestionStep(1, question1, flow);
        QuestionStep step2 = new QuestionStep(2, question2, flow);
        QuestionStep step3 = new QuestionStep(3, question3, flow);
        QuestionStep step4 = new QuestionStep(4, question4, flow);
        QuestionStep step5 = new QuestionStep(5, question5, flow);
        QuestionStep step6 = new QuestionStep(6, question6, flow);

        flow.Steps.Add(step1);
        flow.Steps.Add(step2);
        flow.Steps.Add(step3);
        flow.Steps.Add(step4);
        flow.Steps.Add(step5);
        flow.Steps.Add(step6);

        ctx.QuestionSteps.AddRange(step1, step2, step3, step4, step5, step6);
    }

    private static void GenerateMultipleCQuestions(CodeForgeDbContext ctx, Flow flow)
    {
        

        MultipleChoiceQuestion mquestion1 = new MultipleChoiceQuestion(
            "Wat zou jou helpen om een keuze te maken tussen de verschillende partijen?");

        Choice choice = new Choice("Meer lessen op school rond de partijprogramma’s", mquestion1);
        Choice choice1 = new Choice("Activiteiten in mijn jeugdclub, sportclub… rond de verkiezingen", mquestion1);
        Choice choice2 = new Choice("Een bezoek van de partijen aan mijn school, jeugd/sportclub, …", mquestion1);
        Choice choice3 = new Choice("Een gesprek met mijn ouders rond de gemeentepolitiek", mquestion1);
        Choice choice4 = new Choice("Een debat georganiseerd door een jeugdhuis met de verschillende partijen", mquestion1);
        
        MultipleChoiceQuestion mquestion2 = new MultipleChoiceQuestion(
            "Welke sportactiviteit(en) zou jij graag in je eigen stad of gemeente kunnen beoefenen?");

        Choice choice5 = new Choice("Tennis", mquestion2);
        Choice choice6 = new Choice("Hockey", mquestion2);
        Choice choice7 = new Choice("Padel", mquestion2);
        Choice choice8 = new Choice("Voetbal", mquestion2);
        Choice choice9 = new Choice("Fitness", mquestion2);

        

        MultipleChoiceQuestion mquestion3 = new MultipleChoiceQuestion(
            "Aan welke van deze activiteiten zou jij meedoen, om mee te wegen op het beleid van jouw stad of gemeente?");

        Choice choice10 = new Choice("Deelnemen aan gespreksavonden met schepenen en burgemeester", mquestion3);
        Choice choice11 = new Choice("Bijwonen van een gemeenteraad", mquestion3);
        Choice choice12 = new Choice("Deelnemen aan een survey uitgestuurd door de stad of gemeente", mquestion3);
        Choice choice13 =
            new Choice("Een overleg waarbij ik onderwerpen kan aandragen die voor jongeren belangrijk zijn", mquestion3);
        Choice choice14 = new Choice("Mee brainstormen over oplossingen voor problemen waar jongeren mee worstelen", mquestion3);

        

        MultipleChoiceQuestion mquestion4 = new MultipleChoiceQuestion(
            "Jij gaf aan dat je waarschijnlijk niet zal gaan stemmen. Om welke reden(en) zeg je dit?");

        Choice choice15 = new Choice("Ik heb geen interesse", mquestion4);
        Choice choice16 = new Choice("Ik heb geen tijd om te gaan stemmen", mquestion4);
        Choice choice17 = new Choice("Ik kan niet naar het stemkantoor gaan", mquestion4);
        Choice choice18 = new Choice("Ik denk niet dat mijn stem een verschil zal uitmaken", mquestion4);
        Choice choice19 = new Choice("Ik heb geen idee voor wie ik zou moeten stemmen", mquestion4);



        MultipleChoiceQuestion mquestion5 = new MultipleChoiceQuestion(
            "Wat zou jou (meer) zin geven om te gaan stemmen?");

        Choice choice20 = new Choice("Kunnen gaan stemmen op een toffere locatie", mquestion5);
        Choice choice21 = new Choice("Online, van thuis uit kunnen stemmen", mquestion5);
        Choice choice22 = new Choice("Betere inhoudelijke voorstellen van de politieke partijen", mquestion5);
        Choice choice23 = new Choice("Meer aandacht voor jeugd in de programma’s van de partijen", mquestion5);
        Choice choice24 = new Choice("Beter weten of mijn stem echt impact heeft", mquestion5);

        ctx.ChoiceQuestions.AddRange(mquestion1, mquestion2, mquestion3, mquestion4, mquestion5);
        ctx.Choices.AddRange(choice, choice1, choice2, choice3, choice4, choice5, choice6, choice7, choice8, choice9,
            choice10, choice11, choice12, choice13, choice14, choice15, choice16, choice17, choice18, choice19,
            choice20, choice21, choice22, choice23, choice24);

        //add to step
        QuestionStep step1 = new QuestionStep(7, mquestion1, flow);
        QuestionStep step2 = new QuestionStep(8, mquestion2, flow);
        QuestionStep step3 = new QuestionStep(9, mquestion3, flow);
        QuestionStep step4 = new QuestionStep(10, mquestion4, flow);
        QuestionStep step5 = new QuestionStep(11, mquestion5, flow);

        flow.Steps.Add(step1);
        flow.Steps.Add(step2);
        flow.Steps.Add(step3);
        flow.Steps.Add(step4);
        flow.Steps.Add(step5);

        ctx.QuestionSteps.AddRange(step1, step2, step3, step4, step5);
    }

    private static void GenerateRangeQuestions(CodeForgeDbContext ctx, Flow flow)
    {

        //make questions
        RangeQuestion rquestion1 = new RangeQuestion(
            "Ben jij van plan om te gaan stemmen bij de aankomende lokale verkiezingen?");

        Choice choice = new Choice("Zeker niet", rquestion1);
        Choice choice1 = new Choice("Eerder niet", rquestion1);
        Choice choice2 = new Choice("Ik weet het nog niet", rquestion1);
        Choice choice3 = new Choice("Eerder wel", rquestion1);
        Choice choice4 = new Choice("Zeker wel", rquestion1);
        
        RangeQuestion rquestion2 = new RangeQuestion(
            "Voel jij je betrokken bij het beleid dat wordt uitgestippeld door je gemeente of stad?");

        Choice choice5 = new Choice("Ik voel me weinig tot niet betrokken", rquestion2);
        Choice choice6 = new Choice("Ik voel me weinig betrokken", rquestion2);
        Choice choice7 = new Choice("Ik voel me betrokken", rquestion2);
        Choice choice8 = new Choice("Ik voel me zeer betrokken", rquestion2);
        
        RangeQuestion rquestion3 = new RangeQuestion(
            "In hoeverre ben jij tevreden met het vrijetijdsaanbod in jouw stad of gemeente?");

        Choice choice9 = new Choice("Zeer ontevreden", rquestion3);
        Choice choice10 = new Choice("Ontevreden", rquestion3);
        Choice choice11 = new Choice("Niet tevreden en niet ontevreden", rquestion3);
        Choice choice12 = new Choice("Tevreden", rquestion3);
        Choice choice13 = new Choice("Zeer tevreden", rquestion3);
        
        RangeQuestion rquestion4 = new RangeQuestion(
            "In welke mate ben jij het eens met de volgende stelling: “Mijn stad of gemeente doet voldoende om betaalbare huisvesting mogelijk te maken voor iedereen.”");

        Choice choice14 = new Choice("Helemaal oneens", rquestion4);
        Choice choice15 = new Choice("Oneens", rquestion4);
        Choice choice16 = new Choice("Noch eens, noch oneens", rquestion4);
        Choice choice17 = new Choice("Eens", rquestion4);
        Choice choice18 = new Choice("Helemaal eens", rquestion4);

        RangeQuestion rquestion5 = new RangeQuestion(
            "In welke mate kun jij je vinden in het voorstel om de straatlichten in onze gemeente te doven tussen 23u en 5u?");

        Choice choice19 = new Choice("Ik sta hier volledig achter", rquestion5);
        Choice choice20 = new Choice("Ik sta hier een beetje achter", rquestion5);
        Choice choice21 = new Choice("Ik sta hier niet achter", rquestion5);
        Choice choice22 = new Choice("Ik sta hier helemaal niet achter", rquestion5);
        
        ctx.ChoiceQuestions.AddRange(rquestion1, rquestion2, rquestion3, rquestion4, rquestion5);
        ctx.Choices.AddRange(choice19, choice20, choice21, choice22, choice1, choice2, choice3, choice4, choice5,
            choice6, choice7, choice8, choice9, choice10, choice11, choice12, choice13, choice14, choice15, choice16,
            choice17, choice18, choice);
       

        //add to step
        QuestionStep step1 = new QuestionStep(12, rquestion1, flow);
        QuestionStep step2 = new QuestionStep(13, rquestion2, flow);
        QuestionStep step3 = new QuestionStep(14, rquestion3, flow);
        QuestionStep step4 = new QuestionStep(15, rquestion4, flow);
        QuestionStep step5 = new QuestionStep(16, rquestion5, flow);

        flow.Steps.Add(step1);
        flow.Steps.Add(step2);
        flow.Steps.Add(step3);
        flow.Steps.Add(step4);
        flow.Steps.Add(step5);

        ctx.QuestionSteps.AddRange(step1, step2, step3, step4, step5);
    }

    private static void GenerateOpenQuestions(CodeForgeDbContext ctx, Flow flow)
    {
        //make questions
        OpenQuestion oquesstion1 =
            new OpenQuestion("Je bent schepen van onderwijs voor een dag: waar zet je dan vooral op in?");
        OpenQuestion oquesstion2 =
            new OpenQuestion(
                "Als je één ding mag wensen voor het nieuwe stadspark, wat zou jouw droomstadspark dan zeker bevatten?");

        ctx.OpenQuestions.AddRange(oquesstion1, oquesstion2);

        //add to step
        QuestionStep step1 = new QuestionStep(17, oquesstion1, flow);
        QuestionStep step2 = new QuestionStep(18, oquesstion2, flow);

        flow.Steps.Add(step1);
        flow.Steps.Add(step2);

        ctx.QuestionSteps.AddRange(step1, step2);
    }

    public static void Seed(CodeForgeDbContext ctx)
    {
        // Seed Project and Main theme
        MainTheme mainTheme1 = new MainTheme("Lokale Verkiezingen");
        SubTheme subTheme1 = new SubTheme("Causes", mainTheme1);
        Flow flow = new Flow(FlowType.Linear, subTheme1);
        Flow flow1 = new Flow(FlowType.Circular, subTheme1);
        Participation participation1 = new Participation(flow); //for respondents
        participation1.Respondents.Add(new Respondent("test@mail.com",participation1));
        flow.Participations.Add(participation1);
        
        Text textInfo = new Text("Lokale Verkiezingen");
        Image imageInfo = new Image(ImageUrls.Verkiezingen);
        Video videoInfo = new Video("screensaver.mp4");

        GenerateSingleQuestions(ctx, flow);
        GenerateMultipleCQuestions(ctx, flow);
        GenerateRangeQuestions(ctx, flow);
        GenerateOpenQuestions(ctx, flow);
        GenerateSingleQuestions(ctx, flow1);
        GenerateMultipleCQuestions(ctx, flow1);
        GenerateRangeQuestions(ctx, flow1);
        GenerateOpenQuestions(ctx, flow1);

        InformationStep step1 = new InformationStep(19, imageInfo, flow);
        InformationStep step2 = new InformationStep(20, videoInfo, flow);
        InformationStep step3 = new InformationStep(19, imageInfo, flow1);
        InformationStep step4 = new InformationStep(20, videoInfo, flow1);

        flow.Steps.Add(step1);
        flow.Steps.Add(step2);
        flow1.Steps.Add(step3);
        flow1.Steps.Add(step4);

        SharedPlatform sp = new SharedPlatform("CodeForge");
        
        subTheme1.Flows.Add(flow);
        subTheme1.Flows.Add(flow1);
        Project project1 = new Project(mainTheme1, sp);
        ctx.MainThemes.Add(mainTheme1);
        ctx.Flows.Add(flow);
        ctx.Flows.Add(flow1);
        ctx.Projects.Add(project1);
        ctx.Texts.Add(textInfo);
        ctx.Images.Add(imageInfo);

        // Seed subtheme and extra main theme
        flow.Theme = subTheme1;
        flow1.Theme = subTheme1;
        mainTheme1.Themes.Add(subTheme1);
        ctx.SubThemes.Add(subTheme1);

        // Seed main theme 2
        MainTheme mainTheme2 = new MainTheme("Lokale Verkiezingen - circulair");
        Project project2 = new Project(mainTheme2, sp);
        Flow flow2 = new Flow(FlowType.Circular, mainTheme2);

        GenerateSingleQuestions(ctx, flow2); 

        ((SpAdmin)ctx.Users.Single(user => user.Email == "Henk@CodeForge.com")).SharedPlatform = sp;
        sp.Projects.Add(project1);
        sp.Projects.Add(project2);

        flow2.Theme = mainTheme2;
        ctx.SharedPlatforms.Add(sp);
        ctx.MainThemes.Add(mainTheme2);
        ctx.Flows.Add(flow2);
        ctx.Projects.Add(project2);


        ctx.SaveChanges();
        ctx.ChangeTracker.Clear();
    }
}