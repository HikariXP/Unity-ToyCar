/*
 * Author: CharSui
 * Created On: 2024.12.11
 * Description: 每个场景都挂载一个，作为整个场景加载后的局部初始化控制器
 * 对于写的时候遇到了一个想法：BattleManager管理了整个场景的加载，那这个场景加载器有什么用？
 * 可以做一些非主玩法相关的初始化。
 */

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SceneManagement
{
    public class SceneInitializer : MonoBehaviour
    {
        /// <summary>
        /// 场景中的初始化器
        /// </summary>
        /// <returns></returns>
        [SerializeField]
        public List<ISceneInitializeHandler> sceneInitializeHandlers = new List<ISceneInitializeHandler>(64);

        public void Awake()
        {
            var count = sceneInitializeHandlers.Count;
            for (int i = 0; i < count; i++)
            {
                sceneInitializeHandlers[i].AwakeInitializeImplementation();
            }
        }

        public void Start()
        {
            var count = sceneInitializeHandlers.Count;
            for (int i = 0; i < count; i++)
            {
                sceneInitializeHandlers[i].StartInitializeImplementation();
            }
        }

#if UNITY_EDITOR

        [Button("Collect SceneInitializeHandler")]
        public void CollectISceneInitializeHandlerInScene()
        {
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                foreach (var component in obj.GetComponents<MonoBehaviour>())
                {
                    if (component is ISceneInitializeHandler handler)
                    {
                        if (!sceneInitializeHandlers.Contains(handler))
                        {
                            sceneInitializeHandlers.Add(handler);
                        }
                    }
                }
            }

            foreach (var component in sceneInitializeHandlers)
            {
                if (component == null)
                {
                    Debug.LogError($"[{nameof(SceneInitializer)}]sceneInitializeHandler is null");
                    continue;
                }

                // 在这里处理每个接口组件
                Debug.Log($"[{nameof(SceneInitializer)}]sceneInitialize {component.name}");
            }
        }

        /// <summary>
        /// 给初始化器进行排序
        /// </summary>
        [Button]
        public void Sort()
        {
            if(sceneInitializeHandlers == null || sceneInitializeHandlers.Count == 0)return;
            
            sceneInitializeHandlers.Sort();
        }

#endif
    }
}
