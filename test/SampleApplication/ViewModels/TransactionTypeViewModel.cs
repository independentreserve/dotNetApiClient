using IndependentReserve.DotNetClientApi.Data;

namespace SampleApplication.ViewModels
{
    public class TransactionTypeViewModel
    {
        public TransactionType Type { get; set; }
        public bool IsSelected { get; set; }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
