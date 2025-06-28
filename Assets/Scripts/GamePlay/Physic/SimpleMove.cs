using System.Collections;
using System.Collections.Generic;
using Input;
using Sirenix.OdinInspector;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public BattleInputListener bil;
    
    public Transform cameraTransform; // 摄像机的Transform
    public float moveForce = 10f; // 移动力大小
    public float maxSpeed = 5f; // 最大移动速度
    public float rotationSpeed = 10f; // 旋转速度（可选）
    
    private Rigidbody rb;
    [ShowInInspector]
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 获取输入
        float horizontalInput = bil.move.x;
        float verticalInput = bil.move.y;

        // 计算基于摄像机朝向的移动方向
        // moveDirection = GetCameraRelativeMovement(horizontalInput, verticalInput);
        moveDirection = GetCameraRelativeDirection(bil.move);
        
        // 可选：让物体朝向移动方向旋转
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // 在FixedUpdate中应用物理力
        if (moveDirection != Vector3.zero && rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(moveDirection * moveForce, ForceMode.Force);
        }
        
        // 可选：当没有输入时施加阻力
        if (moveDirection == Vector3.zero && rb.velocity.magnitude > 0.1f)
        {
            rb.velocity *= 0.95f; // 简单的减速
        }
    }

    Vector3 GetCameraRelativeMovement(float horizontal, float vertical)
    {
        // 获取摄像机正前方和正右方方向（忽略Y轴变化）
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        
        // 确保在XZ平面移动（忽略Y轴变化）
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();
        
        // 组合移动方向
        return cameraForward * vertical + cameraRight * horizontal;
    }
    
    Vector3 GetCameraRelativeDirection(Vector2 input)
    {
        // 获取摄像机正前方和正右方方向（忽略Y轴变化）
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
    
        // 确保在XZ平面移动（忽略Y轴变化）
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();
    
        // 组合移动方向（使用Vector2的x和y分量）
        return (cameraForward * input.y + cameraRight * input.x).normalized;
    }
}
