namespace IndependentReserve.DotNetClientApi
{
    public class ApiConfig
    {
        public string BaseUrl { get; set; }

        public ApiCredential Credential { get; set; }

        public ExpiryMode ExpiryMode { get; set; }

        public bool HasCredential => Credential?.IsValid ?? false;

        public ApiConfig()
        {
            ExpiryMode = ExpiryMode.Nonce;
        }

        public ApiConfig(string baseUrl, string key = null, string secret = null, ExpiryMode expiryMode = ExpiryMode.Nonce)
        {
            BaseUrl = baseUrl;
            Credential = new ApiCredential(key, secret);
            ExpiryMode = expiryMode;
        }

        public override string ToString()
        {
            return $"Cred={Credential}";
        }
    }
}