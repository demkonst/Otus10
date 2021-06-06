using System;

namespace Otus10
{
    public class ElapseResult<TResult>
    {
        public TimeSpan Elapsed { get; set; }
        public TResult Result { get; set; }
    }
}
