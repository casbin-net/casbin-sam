using System.Collections.Generic;

namespace Casbin.Sam.Core
{
    public struct FilterParameter
    {
        public FilterParameter(int startIndex, IEnumerable<string> values)
        {
            StartIndex = startIndex;
            Values = values;
        }

        public int StartIndex { get; }
        public IEnumerable<string> Values { get; }
    }
}
