using System;
using System.Globalization;
using System.Numerics;
using ExtendedNumerics;

namespace BTCPayServer.Services.Altcoins.Nano.Utils
{
    public class NanoMoney
    {
        private static readonly BigDecimal _factor = BigDecimal.Pow(10, 30);
        
        public static decimal Convert(BigInteger raw)
        {
            BigDecimal result = new BigDecimal(raw) / _factor;
            BigDecimal roundedResult = BigDecimal.Round(result, 12); 
            return (decimal)roundedResult;
        }

        public static BigInteger Convert(decimal nano)
        {
            BigDecimal result = new BigDecimal(nano) * _factor;
            return (BigInteger)result;
        }
    }
}

