using Reviver.Logging;
using Reviver.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.AddFileLogger();

builder.Services.AddHostedService<ReviverWorker>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
