using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PozitronDev.BagTrack;
using PozitronDev.BagTrack.Infrastructure.MQ;
using PozitronDev.BagTrack.Setup;
using PozitronDev.BagTrack.Setup.Jobs;
using PozitronDev.BagTrack.Setup.Middleware;
using PozitronDev.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add middleware services
builder.Logging.AddLogging(builder.Services, builder.Configuration, builder.Environment);
builder.Services.AddTransient<IApplicationModelProvider, ResponseTypeModelProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddHostedService<DbInitializerJob>();
builder.Services.AddHostedService<ConfigurationReloadJob>();
//builder.Services.AddHostedService<MQSubscriberService>();

builder.Services.AddSingleton<MQSubscriberService>();

// Add application services
builder.Services.AddSingleton(services => Clock.Initialize());
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.AddBagTrackServices();

// Build application
var app = builder.Build();
app.ConfigureExceptionHandler(app.Environment);
app.UseHangfire();
app.UseCustomSwagger();
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.MapFallbackToFile("index.html");
//app.MapGet("/", () => "Pozitron BagTrack v1");

await TestApp.RunJob(app.Services);

//app.Run();
