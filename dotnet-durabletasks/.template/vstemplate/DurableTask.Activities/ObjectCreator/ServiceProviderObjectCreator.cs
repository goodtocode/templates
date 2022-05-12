using DurableTask.Core;
using System;

namespace $safeprojectname$
{
    public class ServiceProviderObjectCreator<T> : ObjectCreator<T>
    {
        readonly Type prototype;
        readonly IServiceProvider serviceProvider;

        public ServiceProviderObjectCreator(Type type, IServiceProvider serviceProvider)
        {
            this.prototype = type;
            this.serviceProvider = serviceProvider;
            Initialize(type);
        }

        public override T Create()
        {
            return (T)serviceProvider.GetService(this.prototype);
        }

        void Initialize(object obj)
        {
            Name = NameVersionHelper.GetDefaultName(obj);
            Version = NameVersionHelper.GetDefaultVersion(obj);
        }
    }
}