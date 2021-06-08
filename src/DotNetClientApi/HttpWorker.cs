using IndependentReserve.DotNetClientApi.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IndependentReserve.DotNetClientApi
{
    class HttpWorker : IDisposable, IHttpWorker
    {
        private HttpClient _client;
        
        static string _version;

        public string ApiSecret { get; set; }

        public string LastRequestUrl { get; private set; }
        public string LastRequestHttpMethod { get; private set; }
        public string LastRequestParameters { get; private set; }
        public string LastResponseRaw { get; private set; }

        

        static HttpWorker()
        {
            _version = typeof(HttpWorker).Assembly.GetName().Version.ToString();
        }

        public HttpWorker(Uri baseUri)
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _client = new HttpClient(handler);
            _client.BaseAddress = baseUri;
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("irDotNetClient " + _version);
            _client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip");
            _client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("deflate");
        }

        /// <summary>
        /// Awaitable helper method to call public api url with set of specified get parameters
        /// </summary>
        /// <typeparam name="T">type to which response should be deserialized</typeparam>
        /// <param name="url">api url (without base url part)</param>
        /// <param name="parameters">set of get parameters</param>
        public async Task<T> QueryPublicAsync<T>(string url, params Tuple<string, string>[] parameters)
        {
            LastRequestParameters = string.Empty;
            LastRequestUrl = url;
            LastRequestHttpMethod = "GET";

            //if we have get parameters - append them to the url
            if (parameters.Any())
            {
                string queryString = parameters.Aggregate(string.Empty, (current, parameter) => current + $"{parameter.Item1}={parameter.Item2}&").TrimEnd('&');

                LastRequestParameters = queryString;

                url = $"{url}?{queryString}";
            }

            var response = await _client.GetAsync(url).ConfigureAwait(false);

            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            LastResponseRaw = result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ErrorMessage errorMessage;
                try
                {
                    errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(result);
                }
                catch(Exception)
                {
                    // The response wasn't a JSON formatted result
                    throw new Exception(result.Substring(0, 100));
                }
                
                throw BuildException(response.StatusCode, errorMessage.Message);
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// Awaitable helper method to call private api url posting specified request object as json content
        /// </summary>
        /// <typeparam name="T">type to which response should be deserialized</typeparam>
        /// <param name="url">api url (without base url part)</param>
        /// <param name="request">object to post</param>
        public async Task<T> QueryPrivateAsync<T>(string url, dynamic request)
        {
            HttpContent content = CreateRequestContent(url, request);

            HttpResponseMessage response = await _client.PostAsync(url, content).ConfigureAwait(false);

            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            LastResponseRaw = result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(result);
                throw BuildException(response.StatusCode, errorMessage?.Message);
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// Awaitable helper method to call private api url posting specified request object as json content
        /// </summary>
        /// <param name="url">api url (without base url part)</param>
        /// <param name="request">object to post</param>
        public async Task QueryPrivateAsync(string url, dynamic request)
        {
            HttpContent content = CreateRequestContent(url, request);

            HttpResponseMessage response = await _client.PostAsync(url, content).ConfigureAwait(false);

            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            LastResponseRaw = result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(result);
                throw BuildException(response.StatusCode, errorMessage.Message);
            }
        }

        /// <summary>
        /// Prepares post request content.
        /// </summary>
        public HttpContent CreateRequestContent(string url, dynamic request)
        {
            // Calculate signature against all request parameters
            request.signature = GetSignature(url, request as IDictionary<string, object>);

            string parameters = JsonConvert.SerializeObject(request);

            LastRequestParameters = parameters;
            LastRequestUrl = url;
            LastRequestHttpMethod = "POST";

            return new StringContent(parameters, Encoding.UTF8, "application/json");
        }


        /// <summary>
        /// Helper method to get signature which can be used to 'sign' private api request.
        /// </summary>
        private string GetSignature(string url, IDictionary<String, Object> requestParameters)
        {
            var input = new StringBuilder(new Uri(_client.BaseAddress, url).ToString());

            foreach (string key in requestParameters.Keys)
            {
                input.Append(',');
                var value = requestParameters[key];
                object resultValue;

                if (value != null && value.GetType().IsArray && typeof(string).IsAssignableFrom(value.GetType().GetElementType()))
                {
                    var array = value as string[];
                    resultValue = array == null || !array.Any() ? string.Empty : string.Join(",", array.Select(tt => string.Format("{0}", tt)));
                }
                else
                {
                    if (value is string)
                    {
                        resultValue = ((string)value).Replace("\r", "").Replace("\n", "");
                    }
                    else
                    {
                        resultValue = value;
                    }
                }

                input.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}", key, resultValue);
            }

            return GetHmacSha256Hash(input.ToString(), ApiSecret);
        }


        /// <summary>
        /// Calculates HMACSHA256 hash of specified message with specified key
        /// </summary>
        internal static string GetHmacSha256Hash(string message, string key)
        {
            byte[] keyBytes = StringToAscii(key);
            byte[] messageBytes = StringToAscii(message);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                return BitConverter.ToString(hmac.ComputeHash(messageBytes)).Replace("-", "");
            }
        }


        /// <summary>
        /// Helper method to convert string into byte array uasing ASCII encoding
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static byte[] StringToAscii(string s)
        {
            var retval = new byte[s.Length];
            for (int ix = 0; ix < s.Length; ++ix)
            {
                char ch = s[ix];
                if (ch <= 0x7f) retval[ix] = (byte)ch;
                else retval[ix] = (byte)'?';
            }
            return retval;
        }

        private Exception BuildException(HttpStatusCode responseCode, string message)
        {
            var exception = new Exception(message);
            exception.Data[Client.ExceptionDataHttpStatusCode] = responseCode;
            return exception;
        }

        #region IDisposable

        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (_client != null)
                {
                    _client.Dispose();
                    _client = null;
                }
            }

            _isDisposed = true;
        }

        ~HttpWorker()
        {
            Dispose(false);
        } 
        #endregion
    }
}
