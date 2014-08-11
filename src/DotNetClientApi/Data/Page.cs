using System.Collections.Generic;

namespace IndependentReserve.DotNetClientApi.Data
{
    /// <summary>
    /// Generic class representing page of data of specified type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T> : PageBase
    {
        public IEnumerable<T> Data { get; set; }
    }
}
