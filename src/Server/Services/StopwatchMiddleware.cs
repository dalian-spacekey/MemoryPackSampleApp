using System.Diagnostics;

namespace SampleApp.Server.Services;

public class StopwatchMiddleware
{
    readonly RequestDelegate next;

    public StopwatchMiddleware(RequestDelegate requestDelegate)
    {
        next = requestDelegate;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        await next.Invoke(context);
        stopwatch.Stop();

        Console.WriteLine($"[{context.Request.Headers.Accept}]{context.Request.Path}:{stopwatch.ElapsedMilliseconds}ms");
    }
}