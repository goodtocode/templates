using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservice.Tests
{
    public interface ICrudSteps<T>
    {
        IList<T> Suts { get; }
        T Sut { get; }
        Guid SutKey { get; }
        IList<T> RecycleBin { get; }
        Task Cleanup();
    }
}
