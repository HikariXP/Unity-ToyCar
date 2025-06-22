/*
 * Author: CharSui
 * Created On: 2024.12.25
 * Description: 专门用作值类型的对象池，很少会有需要这样用，倒是可以给你的逻辑进行润滑一下。
 * 不对啊，他妈的对象池，处理对象的啊，值类型没有这种问题。
 */

using System;
using CharSui.ObjectTool;

namespace CharSui.ObjectPool.Variant
{
    [Obsolete("他妈的对象池，处理对象的啊，值类型没有这种问题")]
    public class ValuePool<T> : ObjectPool<T> where T : struct
    {
        public ValuePool(int capacity) : base(capacity)
        {
        }

        protected override T CreateObject()
        {
            return default;
        }

        protected override void DestroyObject(T objectToDestroy)
        {
            // 值类型直接恢复默认就没了。
        }
    }
}