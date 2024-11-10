using System;
using System.Collections;
using System.Collections.Generic;
using common.Interface;
using Unity.VisualScripting;

namespace common.EventCenter.Class
{
    public class Subscriber : IComparable<Subscriber>, IWork
    {
        public int priority { get; } = -1;
        public long subscriptionAccount { get; } = -1;
        protected Func<dynamic[], Null> callback = null;

        public Subscriber(long _subscriptionAccount, int _priority, Func<dynamic[], Null> _callback)
        {
            this.subscriptionAccount = _subscriptionAccount;
            this.priority = _priority;
            this.callback = _callback;
        }

        public int CompareTo(Subscriber other)
        {
            return this.priority - other.priority;
        }
        
        public void Work(dynamic[] args)
        {
            if (callback != null)
            {
                callback.Invoke(args);
            }
        }
    }
}