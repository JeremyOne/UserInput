using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput {

    public static class RngExtentions {

        public static int Next(this System.Security.Cryptography.RNGCryptoServiceProvider _rng, int minValue, int maxValue) {

            if (minValue > maxValue) {
                throw new ArgumentOutOfRangeException("minValue");
            }

            if (minValue == maxValue) {
                return minValue;
            }

            Int64 diff = maxValue - minValue;
            var _buffer = new byte[4];

            while (true) {
                _rng.GetBytes(_buffer);
                UInt32 rand = BitConverter.ToUInt32(_buffer, 0);
                Int64 max = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = max % diff;

                if (rand < max - remainder) {
                    return (int)(minValue + (rand % diff));
                }
            }
        }

        public static bool NextBoolean(this System.Security.Cryptography.RNGCryptoServiceProvider _rng) {
            var _buffer = new byte[1];
            _rng.GetBytes(_buffer);
            
            if (_buffer[0] > 128) {
                return true;
            } else {
                return false;
            }
        }

        public static double NextDouble(this System.Security.Cryptography.RNGCryptoServiceProvider _rng) {
            
            var bytes = new Byte[8];
            _rng.GetBytes(bytes);
            
            var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
            double d = ul / (double)(1UL << 53);
            return d;
        }
    }
}
