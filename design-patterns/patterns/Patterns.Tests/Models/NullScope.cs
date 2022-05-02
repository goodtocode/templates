using System;

namespace GoodToCode.Shared.Patterns.Tests
{
    public class NullScope : IDisposable
    {
        public static NullScope Instance { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
