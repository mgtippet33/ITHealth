using System.Net.Http.Json;

namespace ITHealth.Domain.Http;

public class BaseHttpClient
{
    private readonly HttpClient _httpClient;

    protected BaseHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected async Task<TResponse> ExecuteGetRequestAsync<TResponse, TException>(string url)
        where TException : Exception, new()
    {
        var response = await _httpClient.GetAsync(url);

        EnsureResponseSuccessStatusCode<TException>(response);

        return await GetResponseContentFromJsonAsync<TResponse, TException>(response);
    }

    protected async Task<TResponse> ExecuteGetRequestAsync<TResponse, TException>(string url, RequestHeader header)
        where TException : Exception, new()
    {
        using (var request = new HttpRequestMessage(HttpMethod.Get, url))
        {
            request.Headers.Add(header.Name, header.Value);
            var response = await _httpClient.SendAsync(request);

            EnsureResponseSuccessStatusCode<TException>(response);

            return await GetResponseContentFromJsonAsync<TResponse, TException>(response);
        }
    }

    private void EnsureResponseSuccessStatusCode<TException>(HttpResponseMessage response)
        where TException : Exception, new()
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new TException();
        }
    }

    private async Task<TResponse> GetResponseContentFromJsonAsync<TResponse, TException>(HttpResponseMessage response)
        where TException : Exception, new()
    {
        var responseObject = await response.Content.ReadFromJsonAsync<TResponse>();
        if (responseObject == null)
        {
            throw new TException();
        }

        return responseObject;
    }
}