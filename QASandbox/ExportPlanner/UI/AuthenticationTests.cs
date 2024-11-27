using NUnit.Framework;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QASandbox.ExportPlanner.UI
{
    public class ExportPlannerAPITests
    {
        private ApplicationLauncher? launcher;
        private TestHttpMessageHandler? testHandler;
        private HttpClient? client;
        private const string BearerToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL2RldmFwaS5wb3J0YWwuemEuc3RlaW53ZWcuY29tOjcwNTAvYXBpL2F1dGgvY2FsbGJhY2siLCJpYXQiOjE3MzEwNTIyODUsImV4cCI6MTczMTA1NTg4NSwibmJmIjoxNzMxMDUyMjg1LCJqdGkiOiJDZkpTRTlvU1hYNllscFBWIiwic3ViIjoiMjQiLCJwcnYiOiIyM2JkNWM4OTQ5ZjYwMGFkYjM5ZTcwMWM0MDA4NzJkYjdhNTk3NmY3In0.f2kn3onds059umW63qliiy51-u2Kh1T0QdUD7Pv9YEk";

        [SetUp]
        public void Setup()
        {
            launcher = new ApplicationLauncher();
            launcher.Setup();
            testHandler = new TestHttpMessageHandler();
            client = new HttpClient(testHandler);
        }

        [TearDown]
        public void Teardown()
        {
            launcher.Teardown();
            client?.Dispose();
            testHandler?.Dispose();
        }

        [Test]
        public async Task GivenExportPlannerAPI_WhenPageIsLaunchedWithoutBearerToken_ThenReturnHttpError()
        {
            string url = launcher.GetApplicationUrl();
            launcher.LaunchApplication(url);

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage response = await client.SendAsync(request);
            Assert.That(response.StatusCode, Is.Not.EqualTo(System.Net.HttpStatusCode.OK), $"The page {url} should not be accessible without a bearer token.");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized).Or.EqualTo(System.Net.HttpStatusCode.Forbidden), $"Expected HTTP 401 or 403 status code, but got {response.StatusCode}.");
        }

        [Test]
        public async Task GivenExportPlannerAPI_WhenPageIsLaunchedWithBearerToken_ThenCheckBearerTokenExists()
        {
            string url = launcher.GetApplicationUrl();
            launcher.LaunchApplication(url);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

            HttpResponseMessage response = await client.SendAsync(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), $"Failed to launch {url} with HTTP 200 status code.");

            Assert.Multiple(() =>
            {
                Assert.That(testHandler.LastRequest, Is.Not.Null);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), $"Failed to launch {url} with HTTP 200 status code.");
                Assert.That(testHandler.LastRequest.Headers.Authorization?.Scheme, Is.EqualTo("Bearer"), "Bearer token is missing in the request header.");
            });
        }
    }
}
