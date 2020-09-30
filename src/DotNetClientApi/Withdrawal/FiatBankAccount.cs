using System;

namespace IndependentReserve.DotNetClientApi.Withdrawal
{
    public class FiatBankAccount
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public string Country { get; set; }
        public string Currency { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        
        public string SwiftCode { get; set; }
        public string BeneficiaryAddress { get; set; }

        public string Bsb { get; set; }

        public PayIdAccount PayId { get; set; }
    }
}
