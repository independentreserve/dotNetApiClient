using System;
using System.Collections.Generic;
using IndependentReserve.DotNetClientApi.Data.Common;

namespace IndependentReserve.DotNetClientApi.Data
{
    public class CancelOrdersResult : Dictionary<Guid, CheckResult>
    {
    }
}