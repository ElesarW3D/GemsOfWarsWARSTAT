using System;
using System.Collections.Generic;

namespace GemsOfWarsWARSTAT.TextAnalysis
{
    public interface IFullIEnumerator<T> : IEnumerator<T>, IEnumerable<T> where T : IDisposable
    {
        bool MovePrevious();
    }
}
