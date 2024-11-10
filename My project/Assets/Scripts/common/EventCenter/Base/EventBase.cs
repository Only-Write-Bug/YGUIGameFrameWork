using System;
using System.Collections.Generic;
using common.CustomDataStruct;
using common.EventCenter.Class;
using Unity.VisualScripting;

namespace common.EventCenter.Base
{
    public class EventBase
    {
        public string eventName { get; } = "";
        private static long _subscriptionAccountMechine = 0;

        public EventBase(string name)
        {
            eventName = name;
        }
        
        private MinHeap<Subscriber> _subscriberHeap = new MinHeap<Subscriber>();
        private Dictionary<long, Subscriber> _subscriberDic = new Dictionary<long, Subscriber>();

        public long ApplySubscription(int priority, Func<dynamic[], Null> callback)
        {
            var subscriptionAccount = _subscriptionAccountMechine++;
            var subscriber = new Subscriber(subscriptionAccount, priority, callback);
            
            _subscriberHeap.Add(subscriber);
            _subscriberDic[subscriptionAccount] = subscriber;

            return subscriptionAccount;
        }

        public bool Unsubscription(long subscriptionAccount)
        {
            if (!_subscriberDic.TryGetValue(subscriptionAccount, out var subscriber))
            {
                return false;
            }

            _subscriberDic.Remove(subscriptionAccount);
            return _subscriberHeap.TryRemove(subscriber);
        }
    }
}