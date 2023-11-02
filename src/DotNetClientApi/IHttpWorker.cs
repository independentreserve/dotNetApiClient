using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IndependentReserve.DotNetClientApi
{
    public interface IHttpWorker
    {
        string ApiSecret { get; set; }
        string LastRequestHttpMethod { get; }
        string LastRequestParameters { get; }
        string LastRequestUrl { get; }
        string LastResponseRaw { get; }
        TimeSpan LastRequestDuration { get; }

        HttpContent CreateRequestContent(string url, dynamic request);
        void Dispose();
        Task QueryPrivateAsync(string url, dynamic request);
        Task<T> QueryPrivateAsync<T>(string url, dynamic request);
        Task<T> QueryPublicAsync<T>(string url, params Tuple<string, string>[] parameters);
    }
}