using Domain.ProjectLogics;

namespace Data_Access_Layer.DbContext;

public class DataSeeder
{
    public static void Seed(CodeForgeDbContext ctx)
    {
        //Seed Project and Main theme
        MainTheme mainTheme1 = new MainTheme(1, "Climate Change");
        Project project1 = new Project(1, mainTheme1);
    }
    
}