using Wmj.Hosting.AspnetCore;
using Wmj.Hosting.AspnetCore.LifeTimes;
using Wmj.Hosting.Sample;

HostingContext.InitApplicationWithNLog(appbuilder =>
{
    Startup.ConfigService(appbuilder.Services);
    //Build to application
    var app = appbuilder.Build();

    Startup.BuildService(app, app.Environment);

    app.UseBuildinLifeTimes(app.Lifetime);

    app.Run();
});