// 路径点，当触碰的时候获取当前的赛车id，发送其进度给进度管理器

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TrackPathPoint : MonoBehaviour
{
    public int pathPointId;

    private ToyCarPathProgressManager cached;

    private void Start()
    {
        cached = ToyCarPathProgressManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        var colliderInstanceId = other.attachedRigidbody.GetInstanceID();
        cached.OnToyCarThroughPathPoint(colliderInstanceId, pathPointId);
    }
    
#if UNITY_EDITOR
    
    void OnDrawGizmos()
    {
        // 设置Gizmo颜色
        Gizmos.color = Color.cyan;
        
        // 在对象位置上方显示文本
        Vector3 textPosition = transform.position + Vector3.up * 5f;
        
        // Unity 2019.3+ 可以直接使用Handles.Label
        UnityEditor.Handles.Label(textPosition, pathPointId.ToString(), 
            new GUIStyle() { 
                fontSize = 16, 
                normal = new GUIStyleState() { textColor = Color.cyan } 
            });
    }
    
#endif
}
