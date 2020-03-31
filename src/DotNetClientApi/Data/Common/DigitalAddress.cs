namespace IndependentReserve.DotNetClientApi.Data.Common
{
    public class DigitalAddress
    {
        public string Address { get; set; }

        public string Tag { get; set; }

        public bool HasTag => !string.IsNullOrWhiteSpace(Tag);
    }
}
