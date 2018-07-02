using IndependentReserve.DotNetClientApi;

namespace SampleApplication
{
    public interface IConfigProvider
    {
        ApiConfig Get();
    }
}