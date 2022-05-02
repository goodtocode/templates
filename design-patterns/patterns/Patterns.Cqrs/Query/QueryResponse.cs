using System;
using System.Collections.Generic;

namespace GoodToCode.Shared.Patterns.Cqrs
{
    [Serializable]
    public class QueryResponse<T>
    {
        public QueryResponse()
        {
        }

        public IEnumerable<T> Result { get; set; }

        public ICollection<KeyValuePair<string, string>> Errors { get; set; } = new List<KeyValuePair<string, string>>();

        public Exception ThrownException { get; set; }
    }
}
