namespace IndependentReserve.DotNetClientApi
{
    public class ApiCredential
    {
        public string Key { get; set; }

        public string Secret { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Secret);

        public ApiCredential()
        {

        }

        public ApiCredential(string key, string secret)
        {
            Key = key;
            Secret = secret;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Key) ? "<nil>" : Key.Length > 4 ? Key.Substring(0, 4) : Key;
        }
    }
}