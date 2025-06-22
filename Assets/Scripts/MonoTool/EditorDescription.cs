/*
 * Author: CharSui
 * Created On: 2024.12.17
 * Description: 仅Editor用，用于挂载部分备注于MonoBehaviour上，不会有任何其他逻辑
 */

using UnityEngine;

namespace MonoTool
{
    public class EditorDescription : MonoBehaviour
    {
        #if UNITY_EDITOR
        
        [SerializeField]
        public string description;
        
        #endif
    }
}
