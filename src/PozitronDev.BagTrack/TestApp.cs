using Microsoft.EntityFrameworkCore;
using PozitronDev.BagTrack.Infrastructure.MQ;
using PozitronDev.BagTrack.Infrastructure.MQ.Handlers;
using System.Diagnostics;

namespace PozitronDev.BagTrack;

public class TestApp
{
    public static async Task Run(IServiceProvider services)
    {
        var xml = File.ReadAllText("d:\\Projects\\NET\\PozApps\\PozitronDev.BagTrack\\Extracted.xml");

        using var scope = services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<BagTrackDbContext>();
        var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler>();

        _ = await dbContext.Flights.FirstOrDefaultAsync();

        var stopWatch = new Stopwatch();
        stopWatch.Start();

        for (var i = 0; i < 10_000; i++)
        {
            await handler.HandleAsync(dbContext, xml, CancellationToken.None);
            await dbContext.SaveChangesAsync();
        }

        await Console.Out.WriteLineAsync($"Elapsed time: {stopWatch.ElapsedMilliseconds}");
    }

    public static async Task RunJob(IServiceProvider services)
    {
        var xml = File.ReadAllText("d:\\Projects\\NET\\PozApps\\PozitronDev.BagTrack\\Extracted.xml");

        using var scope = services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<BagTrackDbContext>();
        using var job = scope.ServiceProvider.GetRequiredService<MQSubscriberService>();

        _ = await dbContext.Flights.FirstOrDefaultAsync();

        var stopWatch = new Stopwatch();
        stopWatch.Start();

        for (var i = 0; i < 10_000; i++)
        {
            await job.MessageHandler(xml, CancellationToken.None);
        }

        await Console.Out.WriteLineAsync($"Elapsed time: {stopWatch.ElapsedMilliseconds}");
    }
}
