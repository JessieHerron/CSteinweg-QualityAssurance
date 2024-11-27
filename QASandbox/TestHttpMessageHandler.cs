
public class TestHttpMessageHandler : HttpMessageHandler
{
    public HttpRequestMessage? LastRequest { get; private set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Store the last request for inspection
        LastRequest = request;

        // Mock a successful response
        return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
    }
}
