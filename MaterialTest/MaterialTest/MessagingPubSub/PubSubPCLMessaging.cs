using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PubSub;

namespace MaterialTest
{
    /// <summary>
    /// Implementation of the IMessagingService for PubSubPCL nuget package
    /// </summary>
    public class PubSubPCLMessaging : IMessagingService
    {
        private ISubscriptionManager _subManager;

        public PubSubPCLMessaging(ISubscriptionManager manager)
        {
            _subManager = manager;
        }

        /// <summary>
        /// Subscribe to message of type T
        /// </summary>
        /// <typeparam name="T">Type of message being subscribed to</typeparam>
        /// <param name="subscriber">The object that is subscribing to the message of T</param>
        /// <param name="callback">Action to preform when message is received</param>
        public void SubscribeMessage<T>(object subscriber, Action<T> callback ) where T : IMessage
        {
            if (!_subManager.IsAlreadySubscribed(typeof(T), subscriber))
            {
                subscriber.Subscribe<T>(callback);
            }

        }

        /// <summary>
        /// Publish message of type T
        /// </summary>
        /// <typeparam name="T">Type of message you are publishing</typeparam>
        /// <param name="arg">Object of type T being published</param>
        public void PublishMessage<T>(object subscriber, T arg) where T : IMessage
        {
			subscriber.Publish<T>(arg);
        }

        /// <summary>
        /// Unsubscribe to message of type T
        /// </summary>
        /// <typeparam name="T">Type of message you are unsubscribing to</typeparam>
        /// <param name="unsubscriber">The object that is unsubscribing to the message of T</param>
        public void UnsubscribeMessage<T>(object unsubscriber) where T : IMessage
        {
            _subManager.RemoveSubscription(typeof(T), unsubscriber);
            unsubscriber.Unsubscribe<T>();
        }
    }
}
