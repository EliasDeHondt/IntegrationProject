/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Domain.Accounts;
using Domain.FacilitatorFunctionality;
using Domain.Platform;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
using Domain.WebApp;

namespace Data_Access_Layer.DbContext;

public static class DataSeeder
{
    private static void GenerateSingleQuestions(CodeForgeDbContext dbContext, Flow flow, int caseIndex, int stepIndexFlow)
    {
        switch (caseIndex)
        {
            case 1:
                SingleChoiceQuestion singleQuestions1 = new SingleChoiceQuestion("If you were to prepare the budget for your city or municipality, where would you mainly focus on in the coming years? Choose one.");

                Choice choice1 = new Choice("Nature and ecology", singleQuestions1);
                Choice choice2 = new Choice("Leisure, sports, culture", singleQuestions1);
                Choice choice3 = new Choice("Housing", singleQuestions1);
                Choice choice4 = new Choice("Education and childcare", singleQuestions1);
                Choice choice5 = new Choice("Healthcare and welfare", singleQuestions1);
                Choice choice6 = new Choice("Traffic safety and mobility", singleQuestions1);

                dbContext.ChoiceQuestions.AddRange(singleQuestions1);
                dbContext.Choices.AddRange(choice1, choice2, choice3, choice4, choice5, choice6);

                // Add to step
                QuestionStep step1 = new QuestionStep(stepIndexFlow, singleQuestions1, flow);
                flow.Steps.Add(step1);
                dbContext.QuestionSteps.Add(step1);
                break;
            case 2:
                SingleChoiceQuestion singleQuestions2 = new SingleChoiceQuestion("What do you think is the most important thing that the city park should offer? Choose one.");

                Choice choice7 = new Choice("A place to play and relax", singleQuestions2);
                Choice choice8 = new Choice("A place to meet people", singleQuestions2);
                Choice choice9 = new Choice("A place to exercise", singleQuestions2);
                Choice choice10 = new Choice("A place to learn about nature", singleQuestions2);
                Choice choice11 = new Choice("A place to organize events", singleQuestions2);
                Choice choice12 = new Choice("A place to enjoy art and culture", singleQuestions2);

                dbContext.ChoiceQuestions.AddRange(singleQuestions2);
                dbContext.Choices.AddRange(choice7, choice8, choice9, choice10, choice11, choice12);

                // Add to step
                QuestionStep step2 = new QuestionStep(stepIndexFlow, singleQuestions2, flow);
                flow.Steps.Add(step2);
                dbContext.QuestionSteps.Add(step2);
                break;
            case 3:
                SingleChoiceQuestion singleQuestion3 = new SingleChoiceQuestion("What aspect of sustainable development do you consider most crucial for the future? Choose one.");

                Choice choice14 = new Choice("Environmental conservation", singleQuestion3);
                Choice choice15 = new Choice("Social equity and justice", singleQuestion3);
                Choice choice16 = new Choice("Economic prosperity", singleQuestion3);
                Choice choice17 = new Choice("Cultural preservation", singleQuestion3);
                Choice choice18 = new Choice("Technological innovation", singleQuestion3);
                    
                dbContext.ChoiceQuestions.AddRange(singleQuestion3);
                dbContext.Choices.AddRange(choice14, choice15, choice16, choice17, choice18);
                    
                // Add to step
                QuestionStep step3 = new QuestionStep(stepIndexFlow, singleQuestion3, flow);
                flow.Steps.Add(step3);
                dbContext.QuestionSteps.Add(step3);
                break;
            case 4:
                SingleChoiceQuestion singleQuestion4 = new SingleChoiceQuestion("Which of the following do you believe is the most effective approach towards achieving sustainable development? Choose one.");
                    
                Choice choice19 = new Choice("Education and awareness-raising", singleQuestion4);
                Choice choice20 = new Choice("Policy and governance reforms", singleQuestion4);
                Choice choice21 = new Choice("Community-based initiatives", singleQuestion4);
                Choice choice22 = new Choice("Private sector involvement and innovation", singleQuestion4);
                Choice choice23 = new Choice("International cooperation and collaboration", singleQuestion4);

                dbContext.ChoiceQuestions.AddRange(singleQuestion4);
                dbContext.Choices.AddRange(choice19, choice20, choice21, choice22, choice23);

                // Add to step
                QuestionStep step4 = new QuestionStep(stepIndexFlow, singleQuestion4, flow);
                flow.Steps.Add(step4);
                dbContext.QuestionSteps.Add(step4);
                break;
        }
    }

    private static void GenerateMultipleChoiceQuestions(CodeForgeDbContext dbContext, Flow flow, int caseIndex, int stepIndexFlow)
    {
        switch (caseIndex)
        {
            case 1:
                MultipleChoiceQuestion multipleChoiceQuestions1 = new MultipleChoiceQuestion("What would help you make a choice between the different parties?");

                Choice choice1 = new Choice("More lessons at school about party programs", multipleChoiceQuestions1);
                Choice choice2 = new Choice("Activities related to elections", multipleChoiceQuestions1);
                Choice choice3 = new Choice("A visit from the parties to my school", multipleChoiceQuestions1);
                Choice choice4 = new Choice("A conversation with my parents", multipleChoiceQuestions1);
                Choice choice5 = new Choice("A debate organized by a youth center", multipleChoiceQuestions1);

                dbContext.ChoiceQuestions.AddRange(multipleChoiceQuestions1);
                dbContext.Choices.AddRange(choice1, choice2, choice3, choice4, choice5);

                // Add to step
                QuestionStep step1 = new QuestionStep(stepIndexFlow, multipleChoiceQuestions1, flow);
                flow.Steps.Add(step1);
                dbContext.QuestionSteps.Add(step1);
                break;
            case 2:
                MultipleChoiceQuestion multipleChoiceQuestions2 = new MultipleChoiceQuestion("What do you think is the most important thing that the city park should offer?");
                    
                Choice choice6 = new Choice("A place to play and relax", multipleChoiceQuestions2);
                Choice choice7 = new Choice("A place to meet people", multipleChoiceQuestions2);
                Choice choice8 = new Choice("A place to exercise", multipleChoiceQuestions2);
                Choice choice9 = new Choice("A place to learn about nature", multipleChoiceQuestions2);
                Choice choice10 = new Choice("A place to organize events", multipleChoiceQuestions2);
                    
                dbContext.ChoiceQuestions.AddRange(multipleChoiceQuestions2);
                dbContext.Choices.AddRange(choice6, choice7, choice8, choice9, choice10);
                    
                // Add to step
                QuestionStep step2 = new QuestionStep(stepIndexFlow, multipleChoiceQuestions2, flow);
                flow.Steps.Add(step2);
                dbContext.QuestionSteps.Add(step2);
                break;
            case 3:
                MultipleChoiceQuestion multipleChoiceQuestions3 = new MultipleChoiceQuestion("How can technology best contribute to promoting equal access to education and opportunities for underprivileged communities? (Select all that apply)");
                    
                Choice choice11 = new Choice("Providing online educational resources", multipleChoiceQuestions3);
                Choice choice12 = new Choice("Offering mentorship programs and digital skills", multipleChoiceQuestions3);
                Choice choice13 = new Choice("Facilitating access to affordable internet", multipleChoiceQuestions3);
                Choice choice14 = new Choice("Supporting community-based organizations", multipleChoiceQuestions3);
                Choice choice15 = new Choice("Developing innovative solutions for healthcare", multipleChoiceQuestions3);
                Choice choice16 = new Choice("Empowering local entrepreneurs and small business", multipleChoiceQuestions3);
                    
                dbContext.ChoiceQuestions.AddRange(multipleChoiceQuestions3);
                dbContext.Choices.AddRange(choice11, choice12, choice13, choice14, choice15, choice16);
                    
                // Add to step
                QuestionStep step3 = new QuestionStep(stepIndexFlow, multipleChoiceQuestions3, flow);
                flow.Steps.Add(step3);
                dbContext.QuestionSteps.Add(step3);
                break;
        }
    }

    private static void GenerateRangeQuestions(CodeForgeDbContext dbContext, Flow flow, int caseIndex, int stepIndexFlow)
    {
        switch (caseIndex)
        {
            case 1:
                RangeQuestion rangeQuestions1 = new RangeQuestion("Are you planning to vote in the upcoming local elections?");

                Choice choice1 = new Choice("Definitely not", rangeQuestions1);
                Choice choice2 = new Choice("Probably not", rangeQuestions1);
                Choice choice3 = new Choice("I'm not sure yet", rangeQuestions1);
                Choice choice4 = new Choice("Probably yes", rangeQuestions1);
                Choice choice5 = new Choice("Definitely yes", rangeQuestions1);
                    
                dbContext.ChoiceQuestions.AddRange(rangeQuestions1);
                dbContext.Choices.AddRange(choice1, choice2, choice3, choice4, choice5);

                // Add to step
                QuestionStep step1 = new QuestionStep(stepIndexFlow, rangeQuestions1, flow);
                flow.Steps.Add(step1);
                dbContext.QuestionSteps.Add(step1);
                break;
            case 2:
                RangeQuestion rangeQuestions2 = new RangeQuestion("How important do you think it is that the new city park is a place where everyone feels welcome?");
                    
                Choice choice6 = new Choice("Not important at all", rangeQuestions2);
                Choice choice7 = new Choice("Not so important", rangeQuestions2);
                Choice choice8 = new Choice("Neutral", rangeQuestions2);
                Choice choice9 = new Choice("Important", rangeQuestions2);
                Choice choice10 = new Choice("Very important", rangeQuestions2);
                    
                dbContext.ChoiceQuestions.AddRange(rangeQuestions2);
                dbContext.Choices.AddRange(choice6, choice7, choice8, choice9, choice10);
                    
                // Add to step
                QuestionStep step2 = new QuestionStep(stepIndexFlow, rangeQuestions2, flow);
                flow.Steps.Add(step2);
                dbContext.QuestionSteps.Add(step2);
                break;
        }
    }

    private static void GenerateOpenQuestions(CodeForgeDbContext dbContext, Flow flow, int caseIndex, int stepIndexFlow)
    {
        switch (caseIndex)
        {
            case 1:
                OpenQuestion openQuestions1 = new OpenQuestion("What do you think is the most important issue in your municipality? Please provide your thoughts.");
                QuestionStep step1 = new QuestionStep(stepIndexFlow, openQuestions1, flow);
                flow.Steps.Add(step1);
                dbContext.OpenQuestions.Add(openQuestions1);
                dbContext.QuestionSteps.Add(step1);
                break;
            case 2:
                OpenQuestion openQuestions2 = new OpenQuestion("If you could wish for one thing for the new city park, what would your dream city park definitely include? Please provide your thoughts.");
                QuestionStep step2 = new QuestionStep(stepIndexFlow, openQuestions2, flow);
                flow.Steps.Add(step2);
                dbContext.OpenQuestions.Add(openQuestions2);
                dbContext.QuestionSteps.Add(step2);
                break;
            case 3:
                OpenQuestion openQuestions3 = new OpenQuestion("In your opinion, what role should businesses play in promoting sustainable development? Please provide your thoughts.");
                QuestionStep step3 = new QuestionStep(stepIndexFlow, openQuestions3, flow);
                flow.Steps.Add(step3);
                dbContext.OpenQuestions.Add(openQuestions3);
                dbContext.QuestionSteps.Add(step3);
                break;
            case 4:
                OpenQuestion openQuestions4 = new OpenQuestion("In your opinion, what role should businesses play in promoting sustainable development? Please provide your thoughts.");
                QuestionStep step4 = new QuestionStep(stepIndexFlow, openQuestions4, flow);
                flow.Steps.Add(step4);
                dbContext.OpenQuestions.Add(openQuestions4);
                dbContext.QuestionSteps.Add(step4);
                break;
            case 5:
                OpenQuestion openQuestions5 = new OpenQuestion("How can communities better integrate environmental sustainability into urban planning and development? Share your ideas.");
                QuestionStep step5 = new QuestionStep(stepIndexFlow, openQuestions5, flow);
                flow.Steps.Add(step5);
                dbContext.OpenQuestions.Add(openQuestions5);
                dbContext.QuestionSteps.Add(step5);
                break;
            case 6:
                OpenQuestion openQuestions6 = new OpenQuestion("What measures do you believe are necessary to address the challenges of climate change adaptation in vulnerable regions? Provide your insights.");
                QuestionStep step6 = new QuestionStep(stepIndexFlow, openQuestions6, flow);
                flow.Steps.Add(step6);
                dbContext.OpenQuestions.Add(openQuestions6);
                dbContext.QuestionSteps.Add(step6);
                break;
            case 7:
                OpenQuestion openQuestions7 = new OpenQuestion("How can technology be harnessed to promote sustainable agriculture and food security? Share your thoughts.");
                QuestionStep step7 = new QuestionStep(stepIndexFlow, openQuestions7, flow);
                flow.Steps.Add(step7);
                dbContext.OpenQuestions.Add(openQuestions7);
                dbContext.QuestionSteps.Add(step7);
                break;
            case 8:
                OpenQuestion openQuestions8 = new OpenQuestion("In what ways do you believe sustainable consumption and production patterns can be encouraged among individuals and communities? Offer your perspectives.");
                QuestionStep step8 = new QuestionStep(stepIndexFlow, openQuestions8, flow);
                flow.Steps.Add(step8);
                dbContext.OpenQuestions.Add(openQuestions8);
                dbContext.QuestionSteps.Add(step8);
                break;
        }
    }

    private static void GenerateConditonalQuestions(CodeForgeDbContext dbContext, Flow flow)
    {
        // Add single choice question
        SingleChoiceQuestion singleChoiceQuestion = new SingleChoiceQuestion("Which area of technology do you believe will revolutionize daily life the most in the next decade? Choose one.");
                    
        Choice choice1 = new Choice("Artificial Intelligence and Machine Learning", singleChoiceQuestion);
        Choice choice2 = new Choice("Augmented Reality and Virtual Reality", singleChoiceQuestion);
        Choice choice3 = new Choice("Blockchain and Cryptocurrency", singleChoiceQuestion);
        Choice choice4 = new Choice("Internet of Things", singleChoiceQuestion);
        Choice choice5 = new Choice("Biotechnology and Genetic Engineering", singleChoiceQuestion);
        Choice choice6 = new Choice("Renewable Energy Technology", singleChoiceQuestion);

        dbContext.ChoiceQuestions.AddRange(singleChoiceQuestion);
        dbContext.Choices.AddRange(choice1, choice2, choice3, choice4, choice5, choice6);

            QuestionStep step1 = new QuestionStep(1, singleChoiceQuestion, flow, true);
            flow.Steps.Add(step1);
            dbContext.QuestionSteps.Add(step1);
            
        // Add multiple choice questions
        MultipleChoiceQuestion multipleChoiceQuestion1 = new MultipleChoiceQuestion("How do you think technology can best contribute to environmental sustainability and combating climate change? (Select all that apply)");
                    
        Choice choice7 = new Choice("Developing renewable energy sources", multipleChoiceQuestion1);
        Choice choice8 = new Choice("Monitoring and managing environmental resources", multipleChoiceQuestion1);
        Choice choice9 = new Choice("Encouraging sustainable consumption", multipleChoiceQuestion1);
        Choice choice10 = new Choice("Enhancing climate modeling", multipleChoiceQuestion1);
        Choice choice11 = new Choice("Facilitating global collaboration and knowledge", multipleChoiceQuestion1);
        Choice choice12 = new Choice("Supporting eco-conscious lifestyles", multipleChoiceQuestion1);
                    
        dbContext.ChoiceQuestions.AddRange(multipleChoiceQuestion1);
        dbContext.Choices.AddRange(choice7, choice8, choice9, choice10, choice11, choice12);
                    
            QuestionStep step2 = new QuestionStep(2, multipleChoiceQuestion1, flow, true);
            flow.Steps.Add(step2);
            dbContext.QuestionSteps.Add(step2);
                    
        MultipleChoiceQuestion multipleChoiceQuestion2 = new MultipleChoiceQuestion("What measures do you think are necessary to ensure the ethical development and use of emerging technologies? (Select all that apply)");
                    
        Choice choice13 = new Choice("Establishing clear guidelines and regulations", multipleChoiceQuestion2);
        Choice choice14 = new Choice("Promoting transparency and accountability", multipleChoiceQuestion2);
        Choice choice15 = new Choice("Fostering interdisciplinary collaboration", multipleChoiceQuestion2);
        Choice choice16 = new Choice("Encouraging public dialogue and engagement", multipleChoiceQuestion2);
        Choice choice17 = new Choice("Investing in research and education", multipleChoiceQuestion2);
        Choice choice18 = new Choice("Holding technology companies accountable", multipleChoiceQuestion2);
                    
        dbContext.ChoiceQuestions.AddRange(multipleChoiceQuestion2);
        dbContext.Choices.AddRange(choice13, choice14, choice15, choice16, choice17, choice18);
                    
            QuestionStep step3 = new QuestionStep(3, multipleChoiceQuestion2, flow, false);
            flow.Steps.Add(step3);
            dbContext.QuestionSteps.Add(step3);
            
            // Add range question
            RangeQuestion rangeQuestion = new RangeQuestion("How concerned are you about the ethical implications of emerging technologies such as artificial intelligence and biotechnology?");
                    
        Choice choice19 = new Choice("Not concerned at all", rangeQuestion);
        Choice choice20 = new Choice("Not so concerned", rangeQuestion);
        Choice choice21 = new Choice("Neutral", rangeQuestion);
        Choice choice22 = new Choice("Concerned", rangeQuestion);
        Choice choice23 = new Choice("Very concerned", rangeQuestion);
                    
        dbContext.ChoiceQuestions.AddRange(rangeQuestion);
        dbContext.Choices.AddRange(choice19, choice20, choice21, choice22, choice23);
                    
            QuestionStep step4 = new QuestionStep(4, rangeQuestion, flow, false);
            flow.Steps.Add(step4);
            dbContext.QuestionSteps.Add(step4);
            
            // Add open questions
            OpenQuestion openQuestions1 = new OpenQuestion("What role do you think education should play in fostering a culture of sustainability among future generations? Share your ideas.");
            QuestionStep step5 = new QuestionStep(5, openQuestions1, flow, true);
            flow.Steps.Add(step5);
            dbContext.OpenQuestions.Add(openQuestions1);
            dbContext.QuestionSteps.Add(step5);
            
            OpenQuestion openQuestions2 = new OpenQuestion("In what ways do you think technology can be harnessed to empower marginalized communities and promote social justice? Share your thoughts.");
            QuestionStep step6 = new QuestionStep(6, openQuestions2, flow, false);
            flow.Steps.Add(step6);
            dbContext.OpenQuestions.Add(openQuestions2);
            dbContext.QuestionSteps.Add(step6);

            OpenQuestion openQuestions3 = new OpenQuestion("How can we ensure that technological innovation is guided by ethical principles and values that prioritize the well-being of society and the environment? Provide your insights.");
            QuestionStep step7 = new QuestionStep(7, openQuestions3, flow, true);
            flow.Steps.Add(step7);
            dbContext.OpenQuestions.Add(openQuestions3);
            dbContext.QuestionSteps.Add(step7);
            
        // Add conditional points
        choice18.NextStep = step7;
        
        // Add notes
        step1.Notes.Add(new Note("The user is optimistic about the transformative potential of AI. They emphasize a balanced perspective, acknowledging both the benefits and challenges of AI integration into daily life."));
        step4.Notes.Add(new Note("They recognize both the potential benefits and risks associated with these technologies."));
        step4.Notes.Add(new Note("The user advocates for proactive measures to address ethical concerns and ensure responsible development and deployment of emerging technologies."));
        step5.Notes.Add(new Note("The user views education as a crucial driver for fostering a culture of sustainability. They highlight a comprehensive approach, integrating sustainability into various aspects of education and community involvement."));
    }

    public static void GenerateKdgSteps(CodeForgeDbContext dbContext, Flow flowKdg)
    {
        GenerateSingleQuestionsKdg(dbContext, flowKdg, 1, 1);
        GenerateSingleQuestionsKdg(dbContext, flowKdg, 3, 2);
        GenerateSingleQuestionsKdg(dbContext, flowKdg, 2, 3);
        GenerateMultipleChoiceQuestionsKdg(dbContext, flowKdg, 3, 4);
        GenerateMultipleChoiceQuestionsKdg(dbContext, flowKdg, 2, 5);
        GenerateMultipleChoiceQuestionsKdg(dbContext, flowKdg, 1, 6);
        GenerateRangeQuestionsKdg(dbContext, flowKdg, 7);
        GenerateOpenQuestionsKdg(dbContext, flowKdg, 8);
            
        Image imageInfoKdg = new Image(ImageUrls.Elections);
        Hyperlink hyperlinkInfoKdg = new Hyperlink("https://studentkdg.sharepoint.com/sites/intranet-nl/SitePages/verkiezingen-StuRa.aspx?from=SendByEmail&e=zIeoHeJOsUW7zyhNtRfGkQ&at=9");
            
        flowKdg.Steps.Add(new InformationStep(9, new List<InformationBase> {imageInfoKdg}, flowKdg));
        flowKdg.Steps.Add(new InformationStep(10, new List<InformationBase> {hyperlinkInfoKdg}, flowKdg));
    }
        
    private static void GenerateSingleQuestionsKdg(CodeForgeDbContext dbContext, Flow flow, int caseIndex, int stepNumber)
    {
        switch (caseIndex)
        {
            case 1:
                SingleChoiceQuestion singleQuestions1 = new SingleChoiceQuestion("Welke van de volgende gebieden zou jij prioriteit geven voor verbetering in onze hogeschool voor het komende academische jaar? Kies er een.");

                Choice choice1 = new Choice("Het verbeteren van de diensten voor studentenondersteuning", singleQuestions1);
                Choice choice2 = new Choice("Het bevorderen van de betrokkenheid van studenten en buitenschoolse activiteiten", singleQuestions1);
                Choice choice3 = new Choice("Het verbeteren van de faciliteiten en infrastructuur van de campus", singleQuestions1);
                Choice choice4 = new Choice("Het verbeteren van academische programma's en middelen", singleQuestions1);
                Choice choice5 = new Choice("Het waarborgen van studentenvertegenwoordiging en belangenbehartiging", singleQuestions1);
                Choice choice6 = new Choice("Het bevorderen van diversiteit en inclusieve initiatieven", singleQuestions1);

                dbContext.ChoiceQuestions.AddRange(singleQuestions1);
                dbContext.Choices.AddRange(choice1, choice2, choice3, choice4, choice5, choice6);

                // Add to Step
                QuestionStep step1 = new QuestionStep(stepNumber, singleQuestions1, flow);
                flow.Steps.Add(step1);
                dbContext.QuestionSteps.Add(step1);
                break;

            case 2:
                SingleChoiceQuestion singleQuestions2 = new SingleChoiceQuestion("Welke kwaliteiten acht jij het belangrijkst voor een succesvolle vertegenwoordiger van de studentenraad? Kies er een.");

                Choice choice7 = new Choice("Leiderschap", singleQuestions2);
                Choice choice8 = new Choice("Communicatievaardigheden", singleQuestions2);
                Choice choice9 = new Choice("Samenwerking", singleQuestions2);
                Choice choice10 = new Choice("Probleemoplossende vaardigheden", singleQuestions2);
                Choice choice11 = new Choice("Toewijding aan het welzijn van de studenten", singleQuestions2);
                Choice choice12 = new Choice("Initiatief en drive", singleQuestions2);

                dbContext.ChoiceQuestions.AddRange(singleQuestions2);
                dbContext.Choices.AddRange(choice7, choice8, choice9, choice10, choice11, choice12);

                // Add to Step
                QuestionStep step2 = new QuestionStep(stepNumber, singleQuestions2, flow);
                flow.Steps.Add(step2);
                dbContext.QuestionSteps.Add(step2);
                break;

            case 3:
                SingleChoiceQuestion singleQuestions3 = new SingleChoiceQuestion("Hoe zou jij de prioritering van de toewijzing van fondsen voor studentenactiviteiten en -evenementen zien? Kies er een.");

                Choice choice13 = new Choice("Gelijkmatig over alle soorten evenementen", singleQuestions3);
                Choice choice14 = new Choice("Gebaseerd op de voorkeuren en interesses van studenten", singleQuestions3);
                Choice choice15 = new Choice("Prioriteit geven aan grote jaarlijkse evenementen", singleQuestions3);
                Choice choice16 = new Choice("Ondersteuning van nieuwe en innovatieve initiatieven", singleQuestions3);
                Choice choice17 = new Choice("Fondsen toewijzen op basis van eerdere successen van evenementen", singleQuestions3);
                Choice choice18 = new Choice("Anders", singleQuestions3);

                dbContext.ChoiceQuestions.AddRange(singleQuestions3);
                dbContext.Choices.AddRange(choice13, choice14, choice15, choice16, choice17, choice18);

                // Add to Step
                QuestionStep step3 = new QuestionStep(stepNumber, singleQuestions3, flow);
                flow.Steps.Add(step3);
                dbContext.QuestionSteps.Add(step3);
                break;
        }
    }

    private static void GenerateMultipleChoiceQuestionsKdg(CodeForgeDbContext dbContext, Flow flow, int caseIndex, int stepNumber)
    {
        switch (caseIndex)
        {
            case 1:
                MultipleChoiceQuestion multipleQuestions1 = new MultipleChoiceQuestion("Wat beschouw jij als de belangrijkste verantwoordelijkheden van een studentenraadslid?");

                Choice choice1 = new Choice("Vertegenwoordigen van studentenbelangen", multipleQuestions1);
                Choice choice2 = new Choice("Organiseren van evenementen en activiteiten", multipleQuestions1);
                Choice choice3 = new Choice("Bevorderen van diversiteit en inclusie op de campus", multipleQuestions1);
                Choice choice4 = new Choice("Bijdragen aan de academische besluitvorming", multipleQuestions1);
                Choice choice5 = new Choice("Samenwerken met de schooladministratie voor verbeteringen", multipleQuestions1);


                dbContext.ChoiceQuestions.AddRange(multipleQuestions1);
                dbContext.Choices.AddRange(choice1, choice2, choice3, choice4, choice5);

                // Add to Step
                QuestionStep step1 = new QuestionStep(stepNumber, multipleQuestions1, flow);
                flow.Steps.Add(step1);
                dbContext.QuestionSteps.Add(step1);
                break;

            case 2:
                MultipleChoiceQuestion multipleQuestions2 = new MultipleChoiceQuestion("Welke eigenschappen acht jij essentieel voor een effectieve studentenraadsvertegenwoordiger?");

                    Choice choice7 = new Choice("Leiderschap", multipleQuestions2);
                    Choice choice8 = new Choice("Communicatievaardigheden", multipleQuestions2);
                    Choice choice9 = new Choice("Samenwerking", multipleQuestions2);
                    Choice choice10 = new Choice("Probleemoplossend vermogen", multipleQuestions2);
                    Choice choice11 = new Choice("Toewijding aan het welzijn van de studenten", multipleQuestions2);
                    Choice choice12 = new Choice("Initiatief tonen en vastberadenheid", multipleQuestions2);


                dbContext.ChoiceQuestions.AddRange(multipleQuestions2);
                dbContext.Choices.AddRange(choice7, choice8, choice9, choice10, choice11, choice12);

                // Add to Step
                QuestionStep step2 = new QuestionStep(stepNumber, multipleQuestions2, flow);
                flow.Steps.Add(step2);
                dbContext.QuestionSteps.Add(step2);
                break;

            case 3:
                MultipleChoiceQuestion multipleQuestions3 = new MultipleChoiceQuestion("Op welke gebieden zou jij als studentenraadslid prioriteit geven aan verbeteringen binnen de school?");

                Choice choice13 = new Choice("Verbeteren van studie- en academische ondersteuningsdiensten", multipleQuestions3);
                Choice choice14 = new Choice("Bevorderen van studentenwelzijn en geestelijke gezondheid", multipleQuestions3);
                Choice choice15 = new Choice("Uitbreiden van de betrokkenheid van studenten bij de gemeenschap", multipleQuestions3);
                Choice choice16 = new Choice("Verbeteren van faciliteiten en voorzieningen op de campus", multipleQuestions3);
                Choice choice17 = new Choice("Bevorderen van duurzaamheid en milieubewustzijn", multipleQuestions3);

                dbContext.ChoiceQuestions.AddRange(multipleQuestions3);
                dbContext.Choices.AddRange(choice13, choice14, choice15, choice16, choice17);

                // Add to Step
                QuestionStep step3 = new QuestionStep(stepNumber, multipleQuestions3, flow);
                flow.Steps.Add(step3);
                dbContext.QuestionSteps.Add(step3);
                break;
        }
    }


    private static void GenerateRangeQuestionsKdg(CodeForgeDbContext dbContext, Flow flow, int stepNumber)
    {
        RangeQuestion rangeQuestion = new RangeQuestion("Studenten zouden meer invloed moeten hebben binnen het schoolbestuur");
            
        Choice choice1 = new Choice("Zeker niet", rangeQuestion);
        Choice choice2 = new Choice("Waarschijnlijk niet", rangeQuestion);
        Choice choice3 = new Choice("Onzeker", rangeQuestion);
        Choice choice4 = new Choice("Waarschijnlijk wel", rangeQuestion);
        Choice choice5 = new Choice("Zeker wel", rangeQuestion);
            
        dbContext.ChoiceQuestions.AddRange(rangeQuestion);
        dbContext.Choices.AddRange(choice1, choice2, choice3, choice4, choice5);

        // Add to step
        QuestionStep step1 = new QuestionStep(stepNumber, rangeQuestion, flow);
        flow.Steps.Add(step1);
        dbContext.QuestionSteps.Add(step1);
    }

    private static void GenerateOpenQuestionsKdg(CodeForgeDbContext dbContext, Flow flow, int stepNumber)
    {
        OpenQuestion openQuestion = new OpenQuestion("Leg in je eigen woorden uit wat jij zou willen veranderen aan KdG");
            
        QuestionStep step = new QuestionStep(stepNumber, openQuestion, flow);
        flow.Steps.Add(step);
        dbContext.OpenQuestions.Add(openQuestion);
        dbContext.QuestionSteps.Add(step);
    }

    public static void Seed(CodeForgeDbContext dbContext)
    {
        //=======================
        // Create Main Themes & Sub Themes
        //=======================
            
        // Create Main Themes & Sub Themes (1)
        MainTheme mainTheme1 = new MainTheme("Local Elections");
        SubTheme subTheme1 = new SubTheme("Causes", mainTheme1);
        Flow flow1 = new Flow(FlowType.Linear, subTheme1);

        // Create Main Themes & Sub Themes (2)
        MainTheme mainTheme2 = new MainTheme("Environmental Policies");
        SubTheme subTheme2 = new SubTheme("Climate Change", mainTheme2);
        Flow flow2 = new Flow(FlowType.Circular, subTheme2);
            
        Participation participation1 = new Participation(flow1); //for respondents
        participation1.Respondents.Add(new Respondent("test@mail.com", participation1));
        flow1.Participations.Add(participation1);
            
        SubTheme subTheme3 = new SubTheme("Sustainable Development", mainTheme2);
        Flow flow3 = new Flow(FlowType.Linear, subTheme3);

        // Create Main Themes & Sub Themes (3)
        MainTheme mainTheme3 = new MainTheme("Technology");
        SubTheme subTheme4 = new SubTheme("Impact On Society", mainTheme3);
        Flow flow4 = new Flow(FlowType.Linear, subTheme4);
            
        // Create Main Themes & Sub Themes (4)
        MainTheme mainTheme4 = new MainTheme("Studenten Verkiezingen");
        SubTheme subTheme5 = new SubTheme("Verkiezingen Campus Groenplaats", mainTheme4);
        Flow flow5 = new Flow(FlowType.Circular, subTheme5);
        Flow flow6 = new Flow(FlowType.Linear, subTheme5);

        //=======================
        // Add steps to flow 1 (Linear Flow)
        //=======================
        GenerateSingleQuestions(dbContext, flow1, 1, 1);
        GenerateMultipleChoiceQuestions(dbContext, flow1, 1, 2);
        GenerateRangeQuestions(dbContext, flow1, 1, 3);
        GenerateOpenQuestions(dbContext, flow1, 1, 4);

        //=======================
        // Add steps to flow 2 (Circular Flow)
        //=======================
        GenerateSingleQuestions(dbContext, flow2, 2, 1);
        GenerateMultipleChoiceQuestions(dbContext, flow2, 2, 2);
        GenerateRangeQuestions(dbContext, flow2, 2, 3);
        GenerateOpenQuestions(dbContext, flow2, 2, 4);
            
        //=======================
        // Add steps to flow 3 (Linear Flow)
        //=======================
        GenerateSingleQuestions(dbContext, flow3, 3, 1);
        GenerateSingleQuestions(dbContext, flow3, 4, 2);
        GenerateOpenQuestions(dbContext, flow3, 5, 3);
        GenerateOpenQuestions(dbContext, flow3, 6, 4);
        GenerateOpenQuestions(dbContext, flow3, 7, 5);
        GenerateOpenQuestions(dbContext, flow3, 8, 6);
        GenerateOpenQuestions(dbContext, flow3, 9, 7);
            
        //=======================
        // Add steps to flow 4 (Linear Flow)
        //=======================
        GenerateConditonalQuestions(dbContext, flow4);
            
        //=======================
        // Add steps to flow 5 (Linear Flow)
        //=======================
        GenerateSingleQuestionsKdg(dbContext, flow5, 3, 1);
        GenerateSingleQuestionsKdg(dbContext, flow5, 1, 2);
        GenerateOpenQuestionsKdg(dbContext, flow5, 3);
            
        Image imageInfoKdg = new Image(ImageUrls.Elections);
        Hyperlink hyperlinkInfoKdg = new Hyperlink("https://studentkdg.sharepoint.com/sites/intranet-nl/SitePages/verkiezingen-StuRa.aspx?from=SendByEmail&e=zIeoHeJOsUW7zyhNtRfGkQ&at=9");
            
        flow5.Steps.Add(new InformationStep(4, new List<InformationBase> {imageInfoKdg}, flow5));
        flow5.Steps.Add(new InformationStep(5, new List<InformationBase> {hyperlinkInfoKdg}, flow5));
            
        GenerateKdgSteps(dbContext, flow6);
            
        //=======================
        // Add Information Steps
        //=======================
        Image imageInfo = new Image(ImageUrls.Elections);
        Video videoInfo = new Video("video.mp4");
        Hyperlink hyperlinkInfo = new Hyperlink("https://levuur.be/");
            
        flow1.Steps.Add(new InformationStep(5, new List<InformationBase> {imageInfo}, flow1));
        flow1.Steps.Add(new InformationStep(6, new List<InformationBase> {hyperlinkInfo}, flow1));
        flow2.Steps.Add(new InformationStep(5, new List<InformationBase> {videoInfo}, flow2));

        //=======================
        // Create Shared Platforms & Projects
        //=======================
        SharedPlatform sharedPlatform = new SharedPlatform("CodeForge", ImageUrls.Favicon);
        SharedPlatform sharedPlatform1 = new SharedPlatform("Karel de Grote", ImageUrls.kdg);
        SharedPlatform sharedPlatform2 = new SharedPlatform("Tree company", ImageUrls.tree);
            
        Project project1 = new Project(mainTheme1.Subject, mainTheme1, sharedPlatform, "Local elections let community members choose local officials like mayors and city council members. These elections impact local policies, public services, and community development. Voting in local elections helps shape the leadership and direction of the community, addressing everyday issues like education, safety, and infrastructure.");
        Project project2 = new Project(mainTheme2.Subject, mainTheme2, sharedPlatform);
        Project project3 = new Project(mainTheme3.Subject, mainTheme3, sharedPlatform);
        Project project4 = new Project(mainTheme4.Subject, mainTheme4, sharedPlatform1);

        //=======================
        // Add Ideas
        //=======================
        var author = (WebAppUser)dbContext.Users.Single(u => u.Email == "Bib@CodeForge.com");
            
        Idea idea = new Idea("This is a test idea", author, project1.Feed, null);
        Idea idea1 = new Idea("This is another test idea", author, project1.Feed, null);
        Idea idea2 = new Idea("This is yet another test idea", author, project2.Feed, null);
        Idea idea3 = new Idea("Man I got so many ideas.", author, project1.Feed, null);    
        
        //=======================
        // Add Project Organizers
        //=======================
        ProjectOrganizer projectOrganizer1 = new ProjectOrganizer(project1, (Facilitator)dbContext.Users.Single(user => user.Email == "Tom@CodeForge.com"));
        ProjectOrganizer projectOrganizer2 = new ProjectOrganizer(project2, (Facilitator)dbContext.Users.Single(user => user.Email == "Tom@CodeForge.com"));
        ProjectOrganizer projectOrganizer3 = new ProjectOrganizer(project4, (Facilitator)dbContext.Users.Single(user => user.Email == "Fred@kdg.be"));      
        ProjectOrganizer projectOrganizer4 = new ProjectOrganizer(project3, (Facilitator)dbContext.Users.Single(user => user.Email == "Tom@CodeForge.com"));
            
        //=======================
        // Add users to shared platform
        //=======================
        ((SpAdmin)dbContext.Users.Single(u => u.Email == "Henk@CodeForge.com")).SharedPlatform = sharedPlatform;
        ((SpAdmin)dbContext.Users.Single(u => u.Email == "CodeForge.noreply@gmail.com")).SharedPlatform = sharedPlatform;
            
        ((SpAdmin)dbContext.Users.Single(u => u.Email == "Thomas@kdg.be")).SharedPlatform = sharedPlatform1;
        ((SpAdmin)dbContext.Users.Single(u => u.Email == "kdg.noreply@kdg.be")).SharedPlatform = sharedPlatform1;
                    
        //=======================
        // Remove empty shared platforms
        //=======================
        dbContext.SharedPlatforms.RemoveRange(dbContext.SharedPlatforms.Where(p => p.OrganisationName == string.Empty));
                    
        //=======================
        // Add to database
        //=======================
        dbContext.ProjectOrganizers.Add(projectOrganizer1);
        dbContext.ProjectOrganizers.Add(projectOrganizer2);
        dbContext.ProjectOrganizers.Add(projectOrganizer3);
        dbContext.ProjectOrganizers.Add(projectOrganizer4);
        sharedPlatform.Projects.Add(project1);
        sharedPlatform.Projects.Add(project2);
        sharedPlatform.Projects.Add(project3);
        sharedPlatform1.Projects.Add(project4);
        dbContext.AddRange(idea, idea1, idea2, idea3);
        dbContext.Projects.AddRange(project1, project2, project3, project4);            
        
        //=======================
        // Save changes
        //=======================
        dbContext.SaveChanges();
        ((Facilitator)dbContext.Users.Single(user => user.Email == "Tom@CodeForge.com")).SharedPlatformId = sharedPlatform.Id;
        ((Facilitator)dbContext.Users.Single(u => u.Email == "Fred@kdg.be")).SharedPlatformId = sharedPlatform1.Id;
        ((WebAppUser)dbContext.Users.Single(u => u.Email == "Bib@CodeForge.com")).FeedIds.Add(new LongValue
        {
            Value = dbContext.Feeds.Find(1L)!.Id
        });
        ((WebAppUser)dbContext.Users.Single(u => u.Email == "Bib@CodeForge.com")).FeedIds.Add(new LongValue
        {
            Value = dbContext.Feeds.Find(2L)!.Id
        });
        dbContext.SaveChanges();
        dbContext.ChangeTracker.Clear();
    }
}