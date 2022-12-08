using System.Diagnostics;
using SampleApp.Shared;

var app = ConsoleApp.Create(args);
app.AddAllCommandType();
app.Run();

//[Command("json")]
//public class JsonClientCommands : ConsoleAppBase
//{
//    protected Client Client => new JsonClient();

//    [Command("messages")]
//    public async Task GetMessages()
//    {
//        var stopwatch = new Stopwatch();

//        stopwatch.Restart();
//        var messages = await Client.GetMessagesAsync();
//        stopwatch.Stop();
//        Console.WriteLine($"receive {messages.Length} messages");
//        Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
//    }

//    [Command("persons")]
//    public async Task GetPersons()
//    {
//        var stopwatch = new Stopwatch();

//        stopwatch.Restart();
//        var persons = await Client.GetPersonsAsync();
//        stopwatch.Stop();
//        Console.WriteLine($"receive {persons.Length} persons");
//        Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
//    }
//}

//[Command("memorypack")]
//public class MemoryPackClientCommands : ConsoleAppBase
//{
//    protected Client Client => new MemoryPackClient();

//    [Command("messages")]
//    public async Task GetMessages()
//    {
//        var stopwatch = new Stopwatch();

//        stopwatch.Restart();
//        var messages = await Client.GetMessagesAsync();
//        stopwatch.Stop();
//        Console.WriteLine($"receive {messages.Length} messages");
//        Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
//    }

//    [Command("persons")]
//    public async Task GetPersons()
//    {
//        var stopwatch = new Stopwatch();

//        stopwatch.Restart();
//        var persons = await Client.GetPersonsAsync();
//        stopwatch.Stop();
//        Console.WriteLine($"receive {persons.Length} persons");
//        Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
//    }
//}
