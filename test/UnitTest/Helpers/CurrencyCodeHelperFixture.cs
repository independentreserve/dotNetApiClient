using IndependentReserve.DotNetClientApi.Data;
using IndependentReserve.DotNetClientApi.Helpers;
using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTest.Helpers
{
    [TestFixture]
    public class CurrencyCodeHelperFixture : FixtureBase
    {
        [TestCase("Doge", CurrencyCode.Doge)]
        [TestCase("Usd", CurrencyCode.Usd)]
        [TestCase("Unknown", CurrencyCode.Unknown)]
        public void Convert(string ticker, CurrencyCode expected)
        {
            var currencyCode = CurrencyCodeHelper.Parse(ticker);

            Assert.AreEqual(expected, currencyCode);
        }

        [Test]
        public void ConvertNonExisting()
        {
            var currencyCode = CurrencyCodeHelper.Parse("NonExisting");
            var expected = 0xc7e86c21;

            Assert.AreEqual(expected, (uint)currencyCode);
        }

        [Test]
        public void NoHashCollisionsWithDefinedCodes()
        {
            var currencies = Enum.GetValues(typeof(CurrencyCode))
                .Cast<CurrencyCode>()
                .Select(code => new
                {
                    Code = code,
                    Hash = Fnv1a.ComputeHash(code.ToString())
                })
                .ToList();

            //Reserve codes for 200 currencies
            var minAllowedHash = Math.Max(200, (uint)currencies.Select(c => c.Code).Max());

            //Ensure that hashes doesn't have any collisions with codes
            var collisionsWithCodes = currencies
                .Where(c => c.Hash < minAllowedHash)
                .ToList();

            CollectionAssert.IsEmpty(collisionsWithCodes, $"Collisions with codes: {string.Join(", ", collisionsWithCodes.Select(c => $"{c.Code} - {c.Hash}"))}");

            var collisionsWithHashes = currencies
                .GroupBy(c => c.Hash, c => c.Code)
                .Where(g => g.Count() > 1)
                .ToList();

            CollectionAssert.IsEmpty(collisionsWithHashes, $"Hash collision is detected for {string.Join(", ", collisionsWithHashes.SelectMany(g => g))}");
        }
    }
}
