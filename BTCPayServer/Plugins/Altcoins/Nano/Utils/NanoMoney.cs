using System;
using System.Globalization;

namespace BTCPayServer.Services.Altcoins.Nano.Utils
{
    public class NanoMoney
    {
        public static decimal Convert(UInt128 raw)
        {
            var amt = raw.ToString(CultureInfo.InvariantCulture).PadLeft(8, '0');
            amt = amt.Length == 8 ? $"0.{amt}" : amt.Insert(amt.Length - 8, ".");

            return decimal.Parse(amt, CultureInfo.InvariantCulture);
        }

        public static UInt128 Convert(decimal Nano)
        {
            // todo: implement 
            return UInt128.One;
        }
        
    }
}

