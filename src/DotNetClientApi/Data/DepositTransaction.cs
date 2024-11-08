namespace IndependentReserve.DotNetClientApi.Data
{
    public class DepositTransaction : TransactionBase
    {
        /// <summary>
        /// id of network transaction linked to this bank transaction
        /// </summary>
        public string BlockchainTransactionId { get; set; }

        /// <summary>
        /// output index of bitcoin-family transaction linked to this bank transaction
        /// </summary>
        public int? BlockchainTransactionOutputIndex { get; set; }

        /// <summary>
        /// blockchain network of the crypto transaction
        /// </summary>
        public string BlockchainNetwork { get; set; }

        /// <summary>
        /// funds has been credited via the address
        /// </summary>
        public string BlockchainDepositAddress { get; set; }

        /// <summary>
        /// Destination tag or memo. Returned for cryptocurrencies that support tags (for example: Xrp, Xlm)
        /// </summary>
        public string BlockchainDepositTag { get; set; }
    }
}
