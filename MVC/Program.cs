/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/
using Data_Access_Layer.DbContext;
using Domain.Platform;
using MVC;

var builder = WebApplication.CreateBuilder(args);

CreateFavicon();

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();

Statistic a = new Statistic();
a.testPacket();

return;


void CreateFavicon()
{
    byte[] bytes = Convert.FromBase64String(ImageUrls.Favicon);
    File.WriteAllBytes("./wwwroot/favicon.ico", bytes);
}