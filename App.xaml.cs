using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Reflection;

namespace TextBoxIME;

public partial class App : Application
{
    public static Window? CurrentWindow { get; private set; }

    public App()
    {
        this.InitializeComponent();
        LoadUnoEditTheme();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var window = new Window();
        CurrentWindow = window;

        // Show Uno runtime package/assembly version in the main window title
        try
        {
            string unoVersion = GetUnoVersion();
            window.Title = $"TextBoxIME — Uno {unoVersion}";
        }
        catch
        {
            // Ignore failures; title is not critical
        }

        if (window.Content is not Frame rootFrame)
        {
            rootFrame = new Frame();
            rootFrame.NavigationFailed += (_, e) =>
                throw new InvalidOperationException($"Failed to navigate to {e.SourcePageType.FullName}: {e.Exception}");
            window.Content = rootFrame;
        }

        if (rootFrame.Content is null)
        {
            rootFrame.Navigate(typeof(MainPage), args.Arguments);
        }

        window.Activate();
    }

    private static string GetUnoVersion()
    {
        try
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // Prefer an assembly named Uno.UI, then any assembly containing Uno.UI, then any assembly starting with "Uno"
            var asm = assemblies.FirstOrDefault(a => string.Equals(a.GetName().Name, "Uno.UI", StringComparison.OrdinalIgnoreCase))
                      ?? assemblies.FirstOrDefault(a => a.GetName().Name?.IndexOf("Uno.UI", StringComparison.OrdinalIgnoreCase) >= 0)
                      ?? assemblies.FirstOrDefault(a => a.GetName().Name?.StartsWith("Uno", StringComparison.OrdinalIgnoreCase) == true);

            if (asm is null)
                return "unknown";

            var info = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            if (!string.IsNullOrEmpty(info))
                return info;

            var fileVer = asm.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
            if (!string.IsNullOrEmpty(fileVer))
                return fileVer;

            var ver = asm.GetName().Version;
            return ver?.ToString() ?? "unknown";
        }
        catch
        {
            return "unknown";
        }
    }

    private void LoadUnoEditTheme()
    {
        foreach (var uri in new[]
        {
            "ms-appx:///ICSharpCode.AvalonEdit/Themes/generic.xaml",
            "ms-appx:///UnoEdit/Themes/generic.xaml"
        })
        {
            try
            {
                Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(uri) });
                return;
            }
            catch
            {
            }
        }
    }
}
