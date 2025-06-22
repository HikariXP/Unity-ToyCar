/*
 * Author: CharSui
 * Created On: 2024.12.04
 * Description: 
 */

using Sirenix.OdinInspector;
using UnityEngine;

namespace MonoTool
{
    public class LookAtTool : MonoBehaviour
    {
        #if UNITY_EDITOR
        
        public Transform target;
    
        [Button]
        public void LookAt()
        {
            gameObject.transform.LookAt(target);
        }
        
        #endif
    }
}
