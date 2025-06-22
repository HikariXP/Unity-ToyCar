/*
 * Author: CharSui
 * Created On: 2024.12.25
 * Description: 对象池基础逻辑：初始化、借取、归还、以及对于过度获取的处理
 */

using System.Collections.Generic;
using UnityEngine;

namespace CharSui.ObjectTool
{
    public abstract class ObjectPool<T>
    {
        private readonly Queue<T> _pool;

        /// <summary>
        ///     初始化函数
        /// </summary>
        /// <param name="capacity">初始化容量</param>
        protected ObjectPool(int capacity)
        {
            if (capacity <= 0)
            {
                Debug.LogError(
                    $"[{GetType().Name}]You are trying to create a ObjectPool with capacity less than zero. It will return nothing");
                return;
            }

            _pool = new Queue<T>(capacity);
        }

        // ~ObjectPool()
        // {
        //     var count = _pool.Count;
        //     for (var i = 0; i < count; i++) DestroyObject(_pool.Dequeue());
        // }

        /// <summary>
        ///     预热创建对象
        /// </summary>
        /// <param name="count"></param>
        public void Prewarm(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var newObject = CreateObject();
                BeforeObjectReturn(newObject);
                _pool.Enqueue(newObject);
            }
        }

        public T Get()
        {
            // 如果对象池内有对象，则返回
            T objectToGet = _pool.Count > 0 ? _pool.Dequeue() : CreateObject();
            BeforeObjectGet(objectToGet);
            return objectToGet;
        }

        /// <summary>
        ///     对象池取出前处理，比如SetActive(true)
        /// </summary>
        /// <param name="objectToGet">准备借出的对象</param>
        protected virtual void BeforeObjectGet(T objectToGet)
        {
        }

        /// <summary>
        ///     对象池内容归还
        /// </summary>
        /// <param name="objectToReturn">需要归还的对象</param>
        public void Return(T objectToReturn)
        {
            BeforeObjectReturn(objectToReturn);
            _pool.Enqueue(objectToReturn);
        }

        /// <summary>
        ///     对象池归还内容前处理，比如SetActive(false)
        /// </summary>
        /// <param name="objectToReturn"></param>
        protected virtual void BeforeObjectReturn(T objectToReturn)
        {
        }

        /// <summary>
        ///     定义如何创建对象，从设计上来讲，最好只包含对象的创建，比只有Instantiate部分
        /// </summary>
        /// <returns></returns>
        protected abstract T CreateObject();

        /// <summary>
        ///     销毁对象，从设计上来讲，最好只包含对象的销毁部分，比如只有Destroy部分
        /// </summary>
        protected abstract void DestroyObject(T objectToDestroy);
    }
}