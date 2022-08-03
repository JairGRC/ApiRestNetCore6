using WebApiAutores;

var builder = WebApplication.CreateBuilder(args);

var startUp = new StartUp(builder.Configuration);
startUp.ConfigureServices(builder.Services);

var app = builder.Build();

var servicioLogger = (ILogger<StartUp>)app.Services.GetService(typeof(ILogger<StartUp>));

startUp.Configure(app,app.Environment,servicioLogger);

app.Run();
