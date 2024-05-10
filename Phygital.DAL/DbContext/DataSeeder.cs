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

namespace Data_Access_Layer.DbContext
{
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
                    Choice choice21 = new Choice("Community-based initiatives and grassroots movements", singleQuestion4);
                    Choice choice22 = new Choice("Private sector involvement and innovation", singleQuestion4);
                    Choice choice23 = new Choice("International cooperation and collaboration", singleQuestion4);

                    dbContext.ChoiceQuestions.AddRange(singleQuestion4);
                    dbContext.Choices.AddRange(choice19, choice20, choice21, choice22, choice23);

                    // Add to step
                    QuestionStep step4 = new QuestionStep(stepIndexFlow, singleQuestion4, flow);
                    flow.Steps.Add(step4);
                    dbContext.QuestionSteps.Add(step4);
                    break;
                case 5:
                    SingleChoiceQuestion singleQuestion5 = new SingleChoiceQuestion("Which area of technology do you believe will revolutionize daily life the most in the next decade? Choose one.");
                    
                    Choice choice24 = new Choice("Artificial Intelligence and Machine Learning", singleQuestion5);
                    Choice choice25 = new Choice("Augmented Reality and Virtual Reality", singleQuestion5);
                    Choice choice26 = new Choice("Blockchain and Cryptocurrency", singleQuestion5);
                    Choice choice27 = new Choice("Internet of Things", singleQuestion5);
                    Choice choice28 = new Choice("Biotechnology and Genetic Engineering", singleQuestion5);
                    Choice choice29 = new Choice("Renewable Energy Technology", singleQuestion5);

                    dbContext.ChoiceQuestions.AddRange(singleQuestion5);
                    dbContext.Choices.AddRange(choice24, choice25, choice26, choice27, choice28, choice29);

                    // Add to step
                    QuestionStep step5 = new QuestionStep(stepIndexFlow, singleQuestion5, flow);
                    flow.Steps.Add(step5);
                    dbContext.QuestionSteps.Add(step5);
                    break;
                case 6:
                    SingleChoiceQuestion singleQuestion6 = new SingleChoiceQuestion("When considering the environmental impact of technology, which factor do you prioritize the most? Choose one.");
                    
                    Choice choice30 = new Choice("Energy efficiency and sustainability of manufacturing processes", singleQuestion6);
                    Choice choice31 = new Choice("Use of recyclable materials and eco-friendly packaging", singleQuestion6);
                    Choice choice32 = new Choice("Reduction of carbon emissions and greenhouse gases", singleQuestion6);
                    Choice choice33 = new Choice("Preservation of natural resources and biodiversity", singleQuestion6);
                    Choice choice34 = new Choice("Implementation of sustainable waste management practices", singleQuestion6);
                    Choice choice35 = new Choice("Promotion of renewable energy sources in technology infrastructure", singleQuestion6);

                    dbContext.ChoiceQuestions.AddRange(singleQuestion6);
                    dbContext.Choices.AddRange(choice30, choice31, choice32, choice33, choice34, choice35);

                    // Add to step
                    QuestionStep step6 = new QuestionStep(stepIndexFlow, singleQuestion6, flow);
                    flow.Steps.Add(step6);
                    dbContext.QuestionSteps.Add(step6);
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
                    Choice choice2 = new Choice("Activities in my youth club, sports club... related to elections", multipleChoiceQuestions1);
                    Choice choice3 = new Choice("A visit from the parties to my school, youth/sports club, ...", multipleChoiceQuestions1);
                    Choice choice4 = new Choice("A conversation with my parents about municipal politics", multipleChoiceQuestions1);
                    Choice choice5 = new Choice("A debate organized by a youth center with the various parties", multipleChoiceQuestions1);

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
                    
                    Choice choice11 = new Choice("Providing online educational resources and remote learning platforms", multipleChoiceQuestions3);
                    Choice choice12 = new Choice("Offering mentorship programs and digital skills training initiatives", multipleChoiceQuestions3);
                    Choice choice13 = new Choice("Facilitating access to affordable internet and digital devices", multipleChoiceQuestions3);
                    Choice choice14 = new Choice("Supporting community-based organizations and grassroots initiatives", multipleChoiceQuestions3);
                    Choice choice15 = new Choice("Developing innovative solutions for healthcare and sanitation in underserved areas", multipleChoiceQuestions3);
                    Choice choice16 = new Choice("Empowering local entrepreneurs and small businesses through e-commerce and digital marketplaces", multipleChoiceQuestions3);
                    
                    dbContext.ChoiceQuestions.AddRange(multipleChoiceQuestions3);
                    dbContext.Choices.AddRange(choice11, choice12, choice13, choice14, choice15, choice16);
                    
                    // Add to step
                    QuestionStep step3 = new QuestionStep(stepIndexFlow, multipleChoiceQuestions3, flow);
                    flow.Steps.Add(step3);
                    dbContext.QuestionSteps.Add(step3);
                    break;
                case 4:
                    MultipleChoiceQuestion multipleChoiceQuestions4 = new MultipleChoiceQuestion("How do you think technology can best contribute to environmental sustainability and combating climate change? (Select all that apply)");
                    
                    Choice choice17 = new Choice("Developing renewable energy sources and energy-efficient technologies", multipleChoiceQuestions4);
                    Choice choice18 = new Choice("Monitoring and managing environmental resources through IoT and sensor networks", multipleChoiceQuestions4);
                    Choice choice19 = new Choice("Encouraging sustainable consumption and waste reduction through digital platforms", multipleChoiceQuestions4);
                    Choice choice20 = new Choice("Enhancing climate modeling and prediction capabilities using big data and AI", multipleChoiceQuestions4);
                    Choice choice21 = new Choice("Facilitating global collaboration and knowledge sharing on climate solutions through digital platforms", multipleChoiceQuestions4);
                    Choice choice22 = new Choice("Supporting eco-conscious lifestyles and green initiatives through social media and online communities", multipleChoiceQuestions4);
                    
                    dbContext.ChoiceQuestions.AddRange(multipleChoiceQuestions4);
                    dbContext.Choices.AddRange(choice17, choice18, choice19, choice20, choice21, choice22);
                    
                    // Add to step
                    QuestionStep step4 = new QuestionStep(stepIndexFlow, multipleChoiceQuestions4, flow);
                    flow.Steps.Add(step4);
                    dbContext.QuestionSteps.Add(step4);
                    break;
                case 5:
                    MultipleChoiceQuestion multipleChoiceQuestions5 = new MultipleChoiceQuestion("What measures do you think are necessary to ensure the ethical development and use of emerging technologies? (Select all that apply)");
                    
                    Choice choice23 = new Choice("Establishing clear guidelines and regulations for data privacy and security", multipleChoiceQuestions5);
                    Choice choice24 = new Choice("Promoting transparency and accountability in algorithmic decision-making processes", multipleChoiceQuestions5);
                    Choice choice25 = new Choice("Fostering interdisciplinary collaboration between technologists, ethicists, and policymakers", multipleChoiceQuestions5);
                    Choice choice26 = new Choice("Encouraging public dialogue and engagement on ethical issues related to technology", multipleChoiceQuestions5);
                    Choice choice27 = new Choice("Investing in research and education on the ethical implications of emerging technologies", multipleChoiceQuestions5);
                    Choice choice28 = new Choice("Holding technology companies accountable for ethical violations through legal and regulatory mechanisms", multipleChoiceQuestions5);
                    
                    dbContext.ChoiceQuestions.AddRange(multipleChoiceQuestions5);
                    dbContext.Choices.AddRange(choice23, choice24, choice25, choice26, choice27, choice28);
                    
                    // Add to step
                    QuestionStep step5 = new QuestionStep(stepIndexFlow, multipleChoiceQuestions5, flow);
                    flow.Steps.Add(step5);
                    dbContext.QuestionSteps.Add(step5);
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
                case 3:
                    RangeQuestion rangeQuestions3 = new RangeQuestion("How optimistic are you about the potential of technology to address social and environmental challenges?");
                    
                    Choice choice11 = new Choice("Not optimistic at all", rangeQuestions3);
                    Choice choice12 = new Choice("Not so optimistic", rangeQuestions3);
                    Choice choice13 = new Choice("Neutral", rangeQuestions3);
                    Choice choice14 = new Choice("Optimistic", rangeQuestions3);
                    Choice choice15 = new Choice("Very optimistic", rangeQuestions3);
                    
                    dbContext.ChoiceQuestions.AddRange(rangeQuestions3);
                    dbContext.Choices.AddRange(choice11, choice12, choice13, choice14, choice15);
                    
                    // Add to step
                    QuestionStep step3 = new QuestionStep(stepIndexFlow, rangeQuestions3, flow);
                    flow.Steps.Add(step3);
                    dbContext.QuestionSteps.Add(step3);
                    break;
                case 4:
                    RangeQuestion rangeQuestions4 = new RangeQuestion("How concerned are you about the ethical implications of emerging technologies such as artificial intelligence and biotechnology?");
                    
                    Choice choice16 = new Choice("Not concerned at all", rangeQuestions4);
                    Choice choice17 = new Choice("Not so concerned", rangeQuestions4);
                    Choice choice18 = new Choice("Neutral", rangeQuestions4);
                    Choice choice19 = new Choice("Concerned", rangeQuestions4);
                    Choice choice20 = new Choice("Very concerned", rangeQuestions4);
                    
                    dbContext.ChoiceQuestions.AddRange(rangeQuestions4);
                    dbContext.Choices.AddRange(choice16, choice17, choice18, choice19, choice20);
                    
                    // Add to step
                    QuestionStep step4 = new QuestionStep(stepIndexFlow, rangeQuestions4, flow);
                    flow.Steps.Add(step4);
                    dbContext.QuestionSteps.Add(step4);
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
                case 9:
                    OpenQuestion openQuestions9 = new OpenQuestion("What role do you think education should play in fostering a culture of sustainability among future generations? Share your ideas.");
                    QuestionStep step9 = new QuestionStep(stepIndexFlow, openQuestions9, flow);
                    flow.Steps.Add(step9);
                    dbContext.OpenQuestions.Add(openQuestions9);
                    dbContext.QuestionSteps.Add(step9);
                    break;
                case 10:
                    OpenQuestion openQuestions1O = new OpenQuestion("In what ways do you think technology can be harnessed to empower marginalized communities and promote social justice? Share your thoughts.");
                    QuestionStep step1O = new QuestionStep(stepIndexFlow, openQuestions1O, flow);
                    flow.Steps.Add(step1O);
                    dbContext.OpenQuestions.Add(openQuestions1O);
                    dbContext.QuestionSteps.Add(step1O);
                    break;
                case 11:
                    OpenQuestion openQuestions11 = new OpenQuestion("How can we ensure that technological innovation is guided by ethical principles and values that prioritize the well-being of society and the environment? Provide your insights.");
                    QuestionStep step11 = new QuestionStep(stepIndexFlow, openQuestions11, flow);
                    flow.Steps.Add(step11);
                    dbContext.OpenQuestions.Add(openQuestions11);
                    dbContext.QuestionSteps.Add(step11);
                    break;
            }
        }

        public static void Seed(CodeForgeDbContext dbContext)
        {
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

            // Create Main Themes & Sub Themes (2)
            MainTheme mainTheme3 = new MainTheme("Technology");
            SubTheme subTheme4 = new SubTheme("Impact On Society", mainTheme3);
            Flow flow4 = new Flow(FlowType.Linear, subTheme4);

            // Add steps to flow 1 (Linear Flow)
            GenerateSingleQuestions(dbContext, flow1, 1, 1);
            GenerateMultipleChoiceQuestions(dbContext, flow1, 1, 2);
            GenerateRangeQuestions(dbContext, flow1, 1, 3);
            GenerateOpenQuestions(dbContext, flow1, 1, 4);

            // Add steps to flow 2 (Circular Flow)
            GenerateSingleQuestions(dbContext, flow2, 2, 1);
            GenerateMultipleChoiceQuestions(dbContext, flow2, 2, 2);
            GenerateRangeQuestions(dbContext, flow2, 2, 3);
            GenerateOpenQuestions(dbContext, flow2, 2, 4);
            
            // Add steps to flow 3 (Linear Flow)
            GenerateSingleQuestions(dbContext, flow3, 3, 1);
            GenerateSingleQuestions(dbContext, flow3, 4, 2);
            GenerateOpenQuestions(dbContext, flow3, 5, 3);
            GenerateOpenQuestions(dbContext, flow3, 6, 4);
            GenerateOpenQuestions(dbContext, flow3, 7, 5);
            GenerateOpenQuestions(dbContext, flow3, 8, 6);
            GenerateOpenQuestions(dbContext, flow3, 9, 7);
            
            // Add steps to flow 4 (Linear Flow)
            GenerateSingleQuestions(dbContext, flow4, 5, 1);
            GenerateSingleQuestions(dbContext, flow4, 6, 2);
            GenerateMultipleChoiceQuestions(dbContext, flow4, 3, 3);
            GenerateMultipleChoiceQuestions(dbContext, flow4, 4, 4);
            GenerateMultipleChoiceQuestions(dbContext, flow4, 5, 5);
            GenerateRangeQuestions(dbContext, flow4, 3, 6);
            GenerateRangeQuestions(dbContext, flow4, 4, 7);
            GenerateOpenQuestions(dbContext, flow4, 10, 8);
            GenerateOpenQuestions(dbContext, flow4, 11, 9);
            
            Image imageInfo = new Image(ImageUrls.Elections);
            Video videoInfo = new Video("?"); // TODO: Google Bucket URL
            
            flow1.Steps.Add(new InformationStep(5, new List<InformationBase> {imageInfo}, flow1));
            flow2.Steps.Add(new InformationStep(5, new List<InformationBase> {videoInfo}, flow2));

            // Create Shared Platform & Projects
            SharedPlatform sharedPlatform = new SharedPlatform("CodeForge", ImageUrls.Favicon);
            SharedPlatform sharedPlatform1 = new SharedPlatform("Karel de Grote", "../Phygital.DAL//Assets/kdg.png");
            SharedPlatform sharedPlatform2 = new SharedPlatform("Tree company", "../Phygital.DAL/Assets/tree.png");
            Project project1 = new Project(mainTheme1.Subject, mainTheme1, sharedPlatform);
            Project project2 = new Project(mainTheme2.Subject, mainTheme2, sharedPlatform);
            Project project3 = new Project(mainTheme3.Subject, mainTheme3, sharedPlatform);

            ((SpAdmin)dbContext.Users.Single(u => u.Email == "Henk@CodeForge.com")).SharedPlatform = sharedPlatform;
            ((SpAdmin)dbContext.Users.Single(u => u.Email == "CodeForge.noreply@gmail.com")).SharedPlatform =
                sharedPlatform;
            
            dbContext.SharedPlatforms.RemoveRange(dbContext.SharedPlatforms.Where(p => p.OrganisationName == string.Empty));
            dbContext.SharedPlatforms.AddRange(sharedPlatform1, sharedPlatform2);
            // Create Project Organizers
            ProjectOrganizer projectOrganizer1 = new ProjectOrganizer(project1, (Facilitator)dbContext.Users.Single(user => user.Email == "Tom@CodeForge.com"));
            
            // Add users to shared platform
            ((SpAdmin)dbContext.Users.Single(u => u.Email == "Henk@CodeForge.com")).SharedPlatform = sharedPlatform;
            ((SpAdmin)dbContext.Users.Single(u => u.Email == "CodeForge.noreply@gmail.com")).SharedPlatform = sharedPlatform;
            
            // Remove empty shared platforms
            dbContext.SharedPlatforms.RemoveRange(dbContext.SharedPlatforms.Where(p => p.OrganisationName == string.Empty));
            
            // Add to database
            dbContext.ProjectOrganizers.Add(projectOrganizer1);
            sharedPlatform.Projects.Add(project1);
            sharedPlatform.Projects.Add(project2);
            sharedPlatform.Projects.Add(project3);
            
            // Save changes
            dbContext.SaveChanges();
        }
    }
}
