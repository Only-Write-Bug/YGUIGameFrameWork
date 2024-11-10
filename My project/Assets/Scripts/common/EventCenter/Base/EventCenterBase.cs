using System;
using System.Collections.Generic;
using common.EventCenter.Enum;
using Unity.VisualScripting;

namespace common.EventCenter.Base
{
    public class EventCenterBase
    {
        protected Dictionary<string, EventBase> _events = new Dictionary<string, EventBase>();

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventKey">事件唯一标志</param>
        /// <param name="priority">订阅优先级</param>
        /// <returns>订阅序号：订阅者关于此次订阅的唯一标识</returns>
        public long Subscribe(string eventKey, EEventPriority priority, Func<dynamic[], Null> callback)
        {
            Subscribe(eventKey, (int)priority, callback);
            return -1;
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventKey">事件唯一标志</param>
        /// <param name="customPriority">自定义优先级，如果需要明确哪个订阅者响应顺序，可以使用这个接口订阅</param>
        /// <returns>订阅序号：订阅者关于此次订阅的唯一标识</returns>
        public long Subscribe(string eventKey, int customPriority, Func<dynamic[], Null> callback)
        {
            if(!_events.TryGetValue(eventKey, out var curEvent))
            {
                return -1;
            }
            
            return curEvent.ApplySubscription(customPriority, callback);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="eventKey">事件唯一标志</param>
        /// <param name="subscriptionID">订阅序号</param>
        public void Unsubscribe(string eventKey, long subscriptionID)
        {
            if (_events.TryGetValue(eventKey, out var curEvent))
            {
                curEvent.Unsubscription(subscriptionID);
            }
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="eventKey">事件唯一标志</param>
        /// <param name="parameters">自己维护的参数数组</param>
        public void Publish(string eventKey, params dynamic[] parameters)
        {
            
        }
    }
}