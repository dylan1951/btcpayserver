using System;
using System.Globalization;
using System.Numerics;
using ExtendedNumerics;

namespace BTCPayServer.Services.Altcoins.Nano.Utils
{
    public class NanoMoney
    {
        public static decimal Convert(BigInteger raw)
        {
            var amt = raw.ToString(CultureInfo.InvariantCulture).PadLeft(8, '0');
            amt = amt.Length == 8 ? $"0.{amt}" : amt.Insert(amt.Length - 8, ".");

            return decimal.Parse(amt, CultureInfo.InvariantCulture);
        }

        public static BigInteger Convert(decimal nano)
        {
            // Step 1: Convert nano to BigDecimal
            BigDecimal nanoBigDecimal = new BigDecimal(nano);
    
            // Step 2: Multiply by 10^30
            BigInteger factor = BigInteger.Pow(10, 30);
            BigDecimal result = nanoBigDecimal * new BigDecimal(factor);
    
            // Step 3: Convert the result to UInt128
            return (BigInteger)result;
        }
        
    }
}

