/*
 * Author: CharSui
 * Created On: 2024.12.25
 * Description: 对引用类型的基本实现，用于处理非MonoBehaviour系列的
 */

using CharSui.ObjectTool;

namespace CharSui.ObjectPool.Variant
{
    public class ClassPool<T> : ObjectPool<T> where T : class, new()
    {
        public ClassPool(int capacity) : base(capacity)
        {
        }

        /// <summary>
        /// 如果对象有默认数值，请写在对象里面，又或者在beforeObjectGet里面获取
        /// </summary>
        /// <returns></returns>
        protected override T CreateObject()
        {
            return new T();
        }

        protected override void DestroyObject(T objectToDestroy)
        {
            // 没什么经验不知道这里写什么，直接释放有感觉有点怪。
        }
    }
}