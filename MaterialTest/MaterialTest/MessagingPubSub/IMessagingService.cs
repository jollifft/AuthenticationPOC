using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialTest
{
    public interface IMessagingService
    {
        void SubscribeMessage<T>(object subscriber, Action<T> callback) where T : IMessage;
        void UnsubscribeMessage<T>(object unsubscriber) where T : IMessage;
        void PublishMessage<T>(object subscriber, T arg) where T : IMessage;
    }
}
