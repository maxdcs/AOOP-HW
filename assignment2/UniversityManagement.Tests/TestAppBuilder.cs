using Avalonia;
using Avalonia.Headless;
using Avalonia.Headless.XUnit;

[assembly: AvaloniaTestApplication(typeof(UniversityManagement.Tests.TestAppBuilder))]

namespace UniversityManagement.Tests
{
    public class TestAppBuilder
    {
        public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder.Configure<App>()
                      .UseHeadless(new AvaloniaHeadlessPlatformOptions());
    }
}