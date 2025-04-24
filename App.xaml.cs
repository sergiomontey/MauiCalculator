using Microsoft.UI.Windowing;
using Microsoft.Maui.Platform;
using Microsoft.UI;

namespace MauiCalculator;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
#if WINDOWS
            var mauiWindow = handler.PlatformView;
            var nativeWindow = mauiWindow.GetWindowHandle();
            var windowId = Win32Interop.GetWindowIdFromWindow(nativeWindow);
            var appWindow = AppWindow.GetFromWindowId(windowId);

            if (appWindow is not null)
            {
                appWindow.MoveAndResize(new Windows.Graphics.RectInt32(0, 0, 450, 1850));
            }
#endif
        });

        MainPage = new AppShell();
    }
}
