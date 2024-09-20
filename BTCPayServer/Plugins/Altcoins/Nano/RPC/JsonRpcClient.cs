using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BTCPayServer.Services.Altcoins.Nano.RPC
{
    public class JsonRpcClient
    {
        private readonly Uri _address;
        private readonly HttpClient _httpClient;

        public JsonRpcClient(Uri address, HttpClient client = null)
        {
            _address = address;
            _httpClient = client ?? new HttpClient();
        }
        
        public async Task<TResponse> SendCommandAsync<TRequest, TResponse>(TRequest request, CancellationToken cts = default)
            where TRequest : INanoRequest
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = _address,
                Content = new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8, "application/json")
            };

            var rawResult = await _httpClient.SendAsync(httpRequest, cts);
            var rawJson = await rawResult.Content.ReadAsStringAsync();
            
            rawResult.EnsureSuccessStatusCode();
            
            var errorResponse = JsonConvert.DeserializeObject<NanoErrorResponse>(rawJson);
            if (errorResponse?.Error != null)
            {
                throw new NanoRpcException(errorResponse.Error);
            }
            
            var response = JsonConvert.DeserializeObject<TResponse>(rawJson);
            return response;
        }
    }
    
    public interface INanoRequest
    {
        string Action { get; }
    }
    
    public class NanoErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
    
    public class NanoRpcException : Exception
    {
        public NanoRpcException(string message) : base(message)
        {
        }
    }
}
