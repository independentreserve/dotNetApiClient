namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Represents IR bank transactions
    /// </summary>
    public class Transaction : TransactionBase
    {
        /// <summary>
        /// id of network transaction linked to this bank transaction
        /// </summary>
        public string BitcoinTransactionId { get; set; }

        /// <summary>
        /// output index of bitcoin transaction linked to this bank transaction
        /// </summary>
        public int? BitcoinTransactionOutputIndex { get; set; }

        /// <summary>
        /// id of ethereum transaction linked to this bank transaction
        /// </summary>
        public string EthereumTransactionId { get; set; }

        /// <summary>
        /// Used to correlate against other entities that triggered this transaction
        /// </summary>
        public string CorrelationId { get; set; }
    }
}
