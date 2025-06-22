/*
 * Author: CharSui
 * Created On: 2025.01.07
 * Description: 不再提供单例入口，自己在别的地方写单例。
 * 之所以暂时只支持两种Event，是因为觉得Event的通知就传递太多的参数，传递的参数过多就需要考虑一下实现方式了。
 * 如果你要传的数据多于一个，则需要写成数据结构传入T
 */

using System.Collections.Generic;
using UnityEngine;

namespace Module.EventManager
{
    public class EventManager
    {
        private readonly Dictionary<uint,NoArgEvent> _noArgEvents = new(256);
    
        // 这种无法满足需求
        // private Dictionary<uint,ArgEvent<int>> test = new Dictionary<uint,ArgEvent<int>>();
        // 多人协助的话，需要注意对Object做约束。
        private readonly Dictionary<uint,object> _argEvents = new(256);
        
        public NoArgEvent TryGetNoArgEvent(uint eventId)
        {
            // 如果获取到了事件
            if (_noArgEvents.TryGetValue(eventId, out var noArgEvent)) return noArgEvent;
        
            //如果获取不了就创建
            noArgEvent = new NoArgEvent();
            _noArgEvents.Add(eventId, noArgEvent);
            return noArgEvent;
        }

        public ArgEvent<T> TryGetArgEvent<T>(uint id)
        {
            if (_argEvents.TryGetValue(id, out var eventObject))
            {
                if (eventObject is ArgEvent<T> castedEvent)
                {
                    return castedEvent;
                }
                Debug.LogError($"[{nameof(EventManager)}]InvalidCastException, wrong Type");
                return null;
            }
        
            var newEventTrigger = new ArgEvent<T>();
            _argEvents.Add(id, newEventTrigger);
            return newEventTrigger;
        }
    }
}
