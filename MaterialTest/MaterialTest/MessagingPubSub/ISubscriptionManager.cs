using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialTest
{
    public interface ISubscriptionManager
    {
        bool IsAlreadySubscribed(Type subType, object sender);
        void RemoveSubscription(Type subType, object sender);
    }
}
