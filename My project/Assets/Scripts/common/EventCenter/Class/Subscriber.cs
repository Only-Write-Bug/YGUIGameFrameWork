using System;
using System.Collections;
using System.Collections.Generic;
using common.Interface;

namespace common.EventCenter.Class
{
    public class Subscriber : IComparable<Subscriber>, IWork
    {
        public int priority { get; } = -1;
        public long subscriptionAccount { get; } = -1;

        public Subscriber(long _subscriptionAccount, int _priority)
        {
            this.subscriptionAccount = _subscriptionAccount;
            this.priority = _priority;
        }

        public int CompareTo(Subscriber other)
        {
            return this.priority - other.priority;
        }
        
        public void Work()
        {
            
        }
    }
}