using System;
using System.Collections.Generic;

namespace GoodToCode.Shared.Patterns.Cqrs
{
    public class CommandResponse<T> where T : new()
    {
        public CommandResponse() { }

        public T Result { get; set; } = new T();

        public ICollection<KeyValuePair<string, string>> Errors { get; set; } = new List<KeyValuePair<string, string>>();

        public ICollection<KeyValuePair<string, string>> Warnings { get; set; } = new List<KeyValuePair<string, string>>();

        public Exception ThrownException { get; set; }
    }
}

