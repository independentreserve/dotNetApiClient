namespace IndependentReserve.DotNetClientApi
{
    public class ApiConfig
    {
        public string BaseUrl { get; set; }

        public ApiCredential Credential { get; set; }

        public bool HasCredential => Credential?.IsValid ?? false;

        public ApiConfig()
        {

        }

        public ApiConfig(string baseUrl, string key = null, string secret = null)
        {
            BaseUrl = baseUrl;
            Credential = new ApiCredential(key, secret);
        }

        public override string ToString()
        {
            return $"Cred={Credential}";
        }
    }
}