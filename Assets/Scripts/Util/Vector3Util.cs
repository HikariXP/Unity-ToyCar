/*
* Author: CharSui
* Created On: 2025.02.27
* Description: 提供一些常用的三维判断方法。
*/

using UnityEngine;

public class Vector3Util : MonoBehaviour
{
    public float rotateSpeed = 1f;

    public Transform lookAtTransform;

    public Transform rotateTransform;

    public void Update()
    {
        var deltaTime = Time.deltaTime;
        if (rotateTransform == null || lookAtTransform == null) return;

        var nextFrameRotate = GetFrameRotation(rotateTransform, lookAtTransform.position, rotateSpeed, deltaTime);

        rotateTransform.rotation = nextFrameRotate;
    }

    /// <summary>
    ///     传入目标,通过目标当前的位置获取方向后，给出下一帧旋转的四元数值
    /// </summary>
    /// <param name="localTransform"></param>
    /// <param name="targetPosition"></param>
    /// <param name="rotationSpeed"></param>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public static Quaternion GetFrameRotation(Transform localTransform, Vector3 targetPosition,
        float rotationSpeed, float deltaTime)
    {
        // 计算目标方向
        var directionToTarget = (targetPosition - localTransform.position).normalized;

        // 计算当前Z轴和目标方向之间的旋转
        var targetRotation = Quaternion.LookRotation(directionToTarget);

        // 计算最大旋转角度（根据旋转速度和时间）
        var maxDegreesDelta = rotationSpeed * deltaTime;

        // 使用Quaternion.RotateTowards进行平滑旋转
        return Quaternion.RotateTowards(localTransform.rotation, targetRotation, maxDegreesDelta);
    }

    /// <summary>
    /// 将世界坐标方向转换到局部空间的方向。
    /// </summary>
    /// <param name="localTransform"></param>
    /// <param name="worldDirection"></param>
    /// <returns></returns>
    public static Vector2 GetLocalDirection(Transform localTransform, Vector3 worldDirection)
    {
        // 将世界方向转换到局部空间
        var localDirection = localTransform.InverseTransformDirection(worldDirection);

        // 将局部方向投影到XZ平面（忽略Y轴）
        var localDirection2D = new Vector2(localDirection.x, localDirection.z).normalized;

        return localDirection2D;
    }

    /// <summary>
    /// 将世界坐标方向转换到局部空间的方向。
    /// </summary>
    /// <param name="localTransform"></param>
    /// <param name="worldDirection"></param>
    /// <returns></returns>
    public static Vector2 GetLocalDirection(Vector3 myPosition, Quaternion myRotation, Vector3 targetPosition)
    {
        // 计算从 myPosition 指向 targetPosition 的世界方向
        var worldDirection = (targetPosition - myPosition).normalized;

        // 将世界方向转换到局部空间
        var localDirection = Quaternion.Inverse(myRotation) * worldDirection;

        // 将局部方向投影到XZ平面（忽略Y轴）
        var localDirection2D = new Vector2(localDirection.x, localDirection.z).normalized;

        return localDirection2D;
    }
}