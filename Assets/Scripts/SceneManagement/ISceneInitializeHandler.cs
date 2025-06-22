/*
 * Author: CharSui
 * Created On: 2024.12.11
 * Description: 通常一个模块都需要挂载这个再去做加载。
 * 场景中不允许直接使用Awake和Start，需要纳入管理。但是Update里面拿数据随便。
 * 战局中即加深控制，Update也要归入管理。
 *
 * 之前有做法是对于初始化分批处理，可以看实际需求。基本所有的SceneInitializeHandler都需要MonoBehaviour
 */

using System;
using UnityEngine;

namespace SceneManagement
{
    public abstract class ISceneInitializeHandler : MonoBehaviour, IComparable<ISceneInitializeHandler>
    {
        /// <summary>
        /// 用于指定排序。
        /// </summary>
        public uint priority;

        public abstract void AwakeInitializeImplementation();
        
        public abstract void StartInitializeImplementation();
        
        //TODO : 是否需要添加卸载部分。
        
        public int CompareTo(ISceneInitializeHandler other)
        {
            if (other.priority > priority) return -1;
            return other.priority < priority ? 1 : 0;
        }
    }
}