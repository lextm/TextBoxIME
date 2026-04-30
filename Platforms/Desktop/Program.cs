using System;
using System.Threading.Tasks;
using Uno.UI.Hosting;

namespace TextBoxIME;

internal class Program
{
    [STAThread]
    public static async Task Main(string[] args)
    {
        var host = UnoPlatformHostBuilder.Create()
            .App(() => new App())
            .UseMacOS()
            .UseWin32()
            .UseX11()
            .Build();

        await host.RunAsync();
    }
}
