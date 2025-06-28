using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("Raycast 设置")]
    [SerializeField] private float raycastDistance = 0.2f;  // 检测距离
    [SerializeField] private float yOffset = -0.5f;        // Y 轴偏移（负值表示向下偏移）
    [SerializeField] private LayerMask groundLayer;        // 地面层级
    [SerializeField] private bool showDebugRay = true;     // 显示调试射线

    [Header("状态输出")]
    [SerializeField] public bool isGrounded;              // 是否触地

    void Update()
    {
        CheckGroundedWithYOffset();
    }

    void CheckGroundedWithYOffset()
    {
        // 计算射线起点（物体位置 + Y 轴偏移）
        Vector3 rayStart = transform.position + new Vector3(0, yOffset, 0);

        // 发射射线检测地面
        isGrounded = Physics.Raycast(
            rayStart,
            Vector3.down,
            raycastDistance,
            groundLayer
        );

        // 调试日志（可选）
        if (isGrounded)
        {
            Debug.Log($"触地！射线起点 Y 轴偏移: {yOffset}");
        }
    }

    // 可视化射线和偏移起点
    void OnDrawGizmos()
    {
        if (!showDebugRay) return;

        Vector3 rayStart = transform.position + new Vector3(0, yOffset, 0);
        Vector3 rayEnd = rayStart + Vector3.down * raycastDistance;

        // 绘制射线
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(rayStart, rayEnd);

        // 标记射线起点（黄色立方体）
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(rayStart, new Vector3(0.1f, 0.1f, 0.1f));
    }
}