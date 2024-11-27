namespace QASandbox
{
    public class ExportPlannerAPITests
    {
        private ApplicationLauncher? launcher;
        private static readonly HttpClient client = new();

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
        public async Task GivenExportPlannerAPI_WhenPageIsLaunched_ThenReturnHttp200Result()
        {
            string url = launcher.GetApplicationUrl();
            launcher.LaunchApplication(url);

            HttpResponseMessage response = await client.GetAsync(url);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), $"Failed to launch {url} with HTTP 200 status code.");
        }
    }
}
