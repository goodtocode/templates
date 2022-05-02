using System;

namespace GoodToCode.Shared.Patterns.Cqrs
{
    public class GenericQueryByKey
    {
        public Guid Key { get; set; }

        public GenericQueryByKey() { }

        public GenericQueryByKey(Guid key)
        {
            Key = key;
        }
    }
}
