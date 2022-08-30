var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaStaticFiles(configure => configure.RootPath = "wwwroot");
builder.Services.AddSingleton<IToDoRepo, InMemoryToDoRepo>();
builder.Services.AddSignalR();  

//builder.Logging.AddFilter("Microsoft.AspNetCore.SignalR", LogLevel.Trace);
//builder.Logging.AddFilter("Microsoft.AspNetCore.Http.Connections", LogLevel.Trace);

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapHub<ToDoHub>("/hubs/todo");
});

app.UseSpaStaticFiles();
app.UseSpa(config => {
    config.UseProxyToSpaDevelopmentServer("http://localhost:8080");
});

app.Run();
