/*
 * Author: CharSui
 * Created On: 2025.06.29
 * Description: 漂移检查，获取当前移动的矢量和车前方的角度，如果大于某个角度则判断为正在漂移
 */

using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundCheck))]
public class DriftCheck : MonoBehaviour
{
    [Header("一些检查参数")]

    // 漂移需要的最小角度
    [SerializeField]
    private float driftMinAngle = 10f;

    // 漂移需要的最小速度
    [SerializeField] private float driftMinSpeed = 5f;

    private GroundCheck _gc;

    // [Title("拓展功能 - 差速漂移")]
    // private bool isActiveDifferentialDrift;

    private Rigidbody _rb;

    [Title("是否正在漂移")] [ShowInInspector] public bool isDrifting { get; private set; }

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _gc = gameObject.GetComponent<GroundCheck>();
    }

    private void FixedUpdate()
    {
        // 不在地面怎么漂移
        if (!_gc.isGrounded)
        {
            isDrifting = false;
            return;
        }

        // 获取当前速度方向（水平面，忽略Y轴）
        var velocity = _rb.velocity;
        var horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z);

        // 如果速度太小，不计算漂移
        if (horizontalVelocity.magnitude < driftMinSpeed)
        {
            isDrifting = false;
            return;
        }

        // 计算速度方向与车辆前方的夹角（0°~180°）
        var angle = Vector3.Angle(transform.forward, horizontalVelocity);

        // 检测是否在漂移
        isDrifting = angle > driftMinAngle;
    }
}