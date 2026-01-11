using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

public static class Dependencies
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        // 1. Register the WebDriver (Scoped means one per scenario)
        services.AddScoped<IWebDriver>(sp => {
            var options = new ChromeOptions();
            // Add options here (e.g., options.AddArgument("--headless"))
            return new ChromeDriver(options);
        });

        // 2. Register your Page Objects here
        // services.AddScoped<LoginPage>();

        return services;
    }
}