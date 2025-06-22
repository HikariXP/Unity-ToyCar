/*
 * Author: CharSui
 * Created On: 2024.12.25
 * Description: 对于MonoBeahviour类的对象池实现，对于MonoBehaviour而言，调用SetActive并不是很直接的"关闭对象"，所以应当视情况做不一样的处理：比如丢到很远的高空或者很低的地方。
 * 此对象池只适用于Prefab自带初始化的情况。如果类型的初始化还需要有别的操作，需要看情况进行拓展
 */

using CharSui.ObjectTool;
using UnityEngine;

namespace CharSui.ObjectPool.Variant
{
    public class GameObjectPool : ObjectPool<GameObject>
    {
        /// <summary>
        /// 用于挂载对象
        /// </summary>
        private readonly Transform _parentTransform;

        private readonly GameObject _prefab;
    
        public GameObjectPool(int capacity, GameObject prefab, Transform parentTransform = null) : base(capacity)
        {
            if (prefab == null)
            {
                Debug.LogError($"[{GetType().Name}]Prefab can't be null for a objectPool");
            }

            _prefab = prefab;
            _parentTransform = parentTransform;
        }

        protected override GameObject CreateObject()
        {
            var newObject = Object.Instantiate(_prefab, _parentTransform);
            newObject.SetActive(false);
            return newObject;
        }
        
        protected override void BeforeObjectGet(GameObject objectToGet)
        {
            // objectToGet.SetActive(true);
        }

        protected override void BeforeObjectReturn(GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);
        }

        protected override void DestroyObject(GameObject objectToDestroy)
        {
            Object.Destroy(objectToDestroy);
        }
    }
}
