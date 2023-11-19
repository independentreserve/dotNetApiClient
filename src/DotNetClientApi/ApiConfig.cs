namespace IndependentReserve.DotNetClientApi
{
    public class ApiConfig
    {
        public string BaseUrl { get; set; }

        public ApiCredential Credential { get; set; }

        public NonceExpiryMode NonceExpiryMode { get; set; }

        public bool HasCredential => Credential?.IsValid ?? false;

        public ApiConfig()
        {
            NonceExpiryMode = NonceExpiryMode.Nonce;
        }

        public ApiConfig(string baseUrl, string key = null, string secret = null, NonceExpiryMode nonceExpiryMode = NonceExpiryMode.Nonce)
        {
            BaseUrl = baseUrl;
            Credential = new ApiCredential(key, secret);
            NonceExpiryMode = nonceExpiryMode;
        }

        public override string ToString()
        {
            return $"Cred={Credential}";
        }
    }
}