using WebSocketsSample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<Publisher>();

var app = builder.Build();

// <snippet_UseWebSockets>
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};



app.UseWebSockets(webSocketOptions);
// </snippet_UseWebSockets>

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

var FakeDb = Task.Run(async () =>
{
    Thread.Sleep(6000);
    var publisher = app.Services.GetService<Publisher>();
    for (var i = 0; i < 100; i++)
    {
        publisher.StartProcess();
        Thread.Sleep(6000);
    }
});

app.Run();
//