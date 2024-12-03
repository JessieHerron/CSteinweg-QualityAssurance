using OpenQA.Selenium.Chrome;
using System.Configuration;

namespace QASandbox.HttpHelpers
{
    public class ApplicationLauncher
    {
        private ChromeDriver? driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        public void LaunchApplication(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url), "URL cannot be null or empty.");
            }
            driver?.Navigate().GoToUrl(url);
        }

        public string GetApplicationUrl()
        {
            var configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "web.config") };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            return config.AppSettings.Settings["ExportPlannerAPI"]?.Value ?? throw new ConfigurationErrorsException("URL not found in configuration.");
        }

        public string? GetCurrentUrl()
        {
            return driver?.Url;
        }
    }

    [TestFixture]
    public class ApplicationLauncherTests
    {
        private ApplicationLauncher launcher;

        [SetUp]
        public void Setup()
        {
            launcher = new ApplicationLauncher();
            launcher.Setup();
        }

        [TearDown]
        public void Teardown()
        {
            launcher.Teardown();
        }

        [Test]
        public void TestLaunchApplication()
        {
            string url = launcher.GetApplicationUrl();
            launcher.LaunchApplication(url);
            Assert.IsTrue(launcher.GetCurrentUrl()?.Contains(url) ?? false, $"Failed to launch {url}");
        }
    }
}
