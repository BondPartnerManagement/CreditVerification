using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WealthGrowthCreditVerification.Common
{
    public static class ExtensionMethods
    {
        public static bool ValidateSaIdNumber(this string idNumberString)
        {
            int d = -1;
            try
            {
                int a = 0;
                for (int i = 0; i < 6; i++)
                {
                    a += int.Parse(idNumberString[2 * i].ToString());
                }
                int b = 0;
                for (int i = 0; i < 6; i++)
                {
                    b = b * 10 + int.Parse(idNumberString[2 * i + 1].ToString());
                }
                b *= 2;
                int c = 0;
                do
                {
                    c += b % 10;
                    b = b / 10;
                }
                while (b > 0);
                c += a;
                d = 10 - (c % 10);
                if (d == 10) d = 0;
            }
            catch {/*ignore*/}

            if (d == int.Parse(idNumberString.Last().ToString()))
                return true;
            else
                return false;
        }
    }
}