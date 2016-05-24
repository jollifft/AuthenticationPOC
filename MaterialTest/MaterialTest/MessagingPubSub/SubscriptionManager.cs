using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialTest
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private List<Subscription> _subscriptions = new List<Subscription>();

        /// <summary>
        /// Checks to see if a subscription already exists for the object that is trying to subscribe
        /// </summary>
        /// <param name="subType">The type of message object being subscribed to</param>
        /// <param name="subscriber">The current subscription object that is subscribing</param>
        /// <returns>Whether subscription is already tracked</returns>
        public bool IsAlreadySubscribed(Type subType, object subscriber)
        {
//            bool isSubscribed = _subscriptions.Any(
//                    subscription =>
//                        subscription.MessageObject == subType &&
//                        subscription.SubscriberView.GetType() == subscriber.GetType());
			bool isSubscribed = _subscriptions.Any(
				subscription =>
				subscription.MessageObject == subType &&
				subscription.SubscriberView == subscriber);

            //if subscription is not in the list, then add it to the list of subscriptions
            //else return isSuscribed = true;
            if (!isSubscribed)
            {
                Subscription sub = new Subscription { MessageObject = subType, SubscriberView = subscriber };
                _subscriptions.Add(sub);
            }

            return isSubscribed;
        }

        /// <summary>
        /// Removes subscription entry in the subscription list
        /// </summary>
        /// <param name="subType">The type of message object being unsubscribed to</param>
        /// <param name="unsubscriber">The current subscription object that is unsubscribing</param>
        public void RemoveSubscription(Type subType, object unsubscriber)
        {
            _subscriptions.RemoveAll(s => s.MessageObject == subType && s.SubscriberView.GetType() == unsubscriber.GetType());
        }
    }
}
