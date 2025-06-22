/*
 * Author: CharSui
 * Created On: 2024.12.15
 * Description: 由于C#原本的Queue不支持检测队列是否已满，但目的是扩展到支持去选择"当队列已满，新指令是覆写旧指令还是不允许写入"所以需要此类进行一次功能封装
 */

using System.Collections.Generic;

namespace ValueExtension
{
    public sealed class FixedQueue<T>
    {
        private readonly Queue<T> _data;

        private readonly bool _overwriteWhenCapacityFull;

        /// <summary>
        /// </summary>
        /// <param name="capacity">定额的容量，触及的时候不会扩容</param>
        /// <param name="overwriteWhenCapacityFull">到达最大容量的时候怎么处置</param>
        public FixedQueue(int capacity, bool overwriteWhenCapacityFull = true)
        {
            _data = new Queue<T>(capacity);
            this.capacity = capacity;
            _overwriteWhenCapacityFull = overwriteWhenCapacityFull;
        }

        /// <summary>
        /// 容器能容纳的数据量
        /// </summary>
        public int capacity { get; }

        /// <summary>
        /// 当前容器内数据量
        /// </summary>
        public int count => _data.Count;

        /// <summary>
        /// 是否当前容器已满
        /// </summary>
        public bool isFull => capacity == count;
        
        public void Enqueue(T data)
        {
            if (isFull)
            {
                // 如果达到最大值的时候重写，那么会抛弃第一个元素，否则不写入
                if(_overwriteWhenCapacityFull)_data.Dequeue();
                else return;;
            }

            


            _data.Enqueue(data);
        }
        
        public bool TryEnqueue(T data)
        {
            // 由于本设计是限制最大容量的队列，所以超出队列的时候不会写入
            if (count >= capacity) return false;

            _data.Enqueue(data);
            return true;
        }
        
        public T Dequeue()
        {
            if (_data.Count == 0)
            {
                return default;
            }

            return _data.Dequeue();
        }
        
        public bool TryDequeue(out T data)
        {
            data = default;

            // 如果容量不足以取，那么返回false
            if (count <= 0) return false;

            data = Dequeue();
            return true;
        }
        
        /// <summary>
        /// 清除当前队列中的所有数据
        /// </summary>
        public void Clear()
        {
            _data.Clear();
        }
    }
}