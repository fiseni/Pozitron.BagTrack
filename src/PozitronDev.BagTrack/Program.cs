using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PozitronDev.BagTrack.Setup;
using PozitronDev.Extensions.Logging;
using PozitronDev.SharedKernel.Contracts;
using PozitronDev.SharedKernel.Data;

var builder = WebApplication.CreateBuilder(args);

// Add middleware services
builder.BindConfigurations();
builder.Logging.AddLogging(builder.Services, builder.Configuration, builder.Environment);
builder.Services.AddTransient<IApplicationModelProvider, ResponseTypeModelProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
// Add application services
builder.Services.AddSingleton(services => Clock.Initialize());
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.AddBagTrackServices();

// Build application
var app = builder.Build();
app.ConfigureExceptionHandler(app.Environment);
app.UseCustomSwagger();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Pozitron BagTrack API v1");

await app.Initialize();

app.Run();
