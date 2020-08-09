using System;
using System.Collections.Generic;
using System.Text;

namespace Casbin.Sam.Core.Services
{
    // See http://tools.ietf.org/html/rfc3548#section-5
    public static class Rfc3548Base32Service
    {
        private static readonly string _base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        public static string ToBase32(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            StringBuilder sb = new StringBuilder();
            for (int offset = 0; offset < input.Length;)
            {
                int numCharsToOutput = GetNextGroup(input, ref offset, 
                    out byte a, out byte b, out byte c,
                    out byte d, out byte e, out byte f, 
                    out byte g, out byte h);

                sb.Append((numCharsToOutput >= 1) ? _base32Chars[a] : '=');
                sb.Append((numCharsToOutput >= 2) ? _base32Chars[b] : '=');
                sb.Append((numCharsToOutput >= 3) ? _base32Chars[c] : '=');
                sb.Append((numCharsToOutput >= 4) ? _base32Chars[d] : '=');
                sb.Append((numCharsToOutput >= 5) ? _base32Chars[e] : '=');
                sb.Append((numCharsToOutput >= 6) ? _base32Chars[f] : '=');
                sb.Append((numCharsToOutput >= 7) ? _base32Chars[g] : '=');
                sb.Append((numCharsToOutput >= 8) ? _base32Chars[h] : '=');
            }

            return sb.ToString();
        }

        public static byte[] FromBase32(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            input = input.TrimEnd('=').ToUpperInvariant();
            if (input.Length == 0)
            {
                return new byte[0];
            }

            var output = new byte[input.Length * 5 / 8];
            int bitIndex = 0;
            int inputIndex = 0;
            int outputBits = 0;
            int outputIndex = 0;
            while (outputIndex < output.Length)
            {
                int byteIndex = _base32Chars.IndexOf(input[inputIndex]);
                if (byteIndex < 0)
                {
                    throw new FormatException();
                }

                int bits = Math.Min(5 - bitIndex, 8 - outputBits);
                output[outputIndex] <<= bits;
                output[outputIndex] |= (byte)(byteIndex >> (5 - (bitIndex + bits)));

                bitIndex += bits;
                if (bitIndex >= 5)
                {
                    inputIndex++;
                    bitIndex = 0;
                }

                outputBits += bits;
                if (outputBits >= 8)
                {
                    outputIndex++;
                    outputBits = 0;
                }
            }
            return output;
        }

        // returns the number of bytes that were output
        private static int GetNextGroup(IReadOnlyList<byte> input, ref int offset, 
            out byte a, out byte b, out byte c, 
            out byte d, out byte e, out byte f, 
            out byte g, out byte h)
        {
            int retVal = (input.Count - offset) switch
            {
                1 => 2,
                2 => 4,
                3 => 5,
                4 => 7,
                _ => 8
            };

            uint b1 = (offset < input.Count) ? input[offset++] : 0U;
            uint b2 = (offset < input.Count) ? input[offset++] : 0U;
            uint b3 = (offset < input.Count) ? input[offset++] : 0U;
            uint b4 = (offset < input.Count) ? input[offset++] : 0U;
            uint b5 = (offset < input.Count) ? input[offset++] : 0U;

            a = (byte)(b1 >> 3);
            b = (byte)(((b1 & 0x07) << 2) | (b2 >> 6));
            c = (byte)((b2 >> 1) & 0x1f);
            d = (byte)(((b2 & 0x01) << 4) | (b3 >> 4));
            e = (byte)(((b3 & 0x0f) << 1) | (b4 >> 7));
            f = (byte)((b4 >> 2) & 0x1f);
            g = (byte)(((b4 & 0x3) << 3) | (b5 >> 5));
            h = (byte)(b5 & 0x1f);

            return retVal;
        }
    }
}
