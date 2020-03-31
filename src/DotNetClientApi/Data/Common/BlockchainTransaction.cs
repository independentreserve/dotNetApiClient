namespace IndependentReserve.DotNetClientApi.Data.Common
{
    public class BlockchainTransaction
    {
        /// <summary>
        /// An identifier used to uniquely identify a particular transaction
        /// </summary>
        public string Hash { get; set; }
        /// <summary>
        /// This field is returned only for 
        /// Output index of bitcoin transaction linked to this bank transaction
        /// </summary>
        public int? OutputIndex { get; set; }
    }
}
