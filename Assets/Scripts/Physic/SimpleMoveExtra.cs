using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using Sirenix.OdinInspector;
using UnityEngine;

public class SimpleMoveExtra : MonoBehaviour
{
    [Header("逻辑组件")]
    public BattleInputListener bil;
    
    public Transform cameraTransform; // 摄像机的Transform
    public Rigidbody rb;
    
    [Header("赛车参数")]
    public float moveForce = 10f; // 移动力大小
    public float maxSpeed = 5f; // 最大移动速度
    public float rotationSpeed = 10f; // 旋转速度（可选）

    [Header("插值")] public float lerpValue = 0.5f;
    
    [ShowInInspector]
    private Vector3 moveDirection;

    [SerializeField]
    private Transform vehicle;

    [SerializeField] private GroundCheck gc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //  return;

        // 计算基于摄像机朝向的移动方向
        if (gc.isGrounded)
        {
            moveDirection = GetCameraRelativeDirection(bil.move);
        }
        
        
        
        // 可选：让物体朝向移动方向旋转
        vehicle.position = Vector3.Lerp(this.transform.position, vehicle.position, Time.deltaTime);
        vehicle.rotation = Quaternion.Lerp(this.transform.rotation, vehicle.rotation, Time.deltaTime);
    }

    void FixedUpdate()
    {
                
        // if (moveDirection == Vector3.zero && rb.velocity.magnitude > 0.1f)
        if (!bil.Fire && rb.velocity.magnitude > 0.1f && gc.isGrounded)
        {
            rb.velocity *= 0.95f; // 简单的减速
        }
        
        // 在FixedUpdate中应用物理力（现在基于物体自身的正前方）
        // if (moveDirection != Vector3.zero && rb.velocity.magnitude < maxSpeed)
        // if (moveDirection != Vector3.zero && gc.isGrounded && bil.Fire)
        if (gc.isGrounded && bil.Fire)
        {
            rb.AddForce(transform.forward * moveForce, ForceMode.Force);
            if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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