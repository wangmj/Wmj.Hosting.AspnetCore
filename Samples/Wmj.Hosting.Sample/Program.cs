using Wmj.Hosting;
using Wmj.Hosting.Sample;

HostingContext.ApplicationInitWithNLog(appbuilder => {
    Startup.ConfigService(appbuilder.Services);
    var app= appbuilder.Build();
    Startup.Config(app,app.Environment);
    app.Run();
});