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
    // 最大马力
    public float maxHorsePower = 20000f; 
    // 马力加速度(每秒)
    public float powerAcceleration = 10000f;
    // 马力不足时的爆发增加马力
    public float initialHorsePower = 5000f;
    // 最高速度
    public float maxSpeed = 5f; // 最大移动速度
    // 转弯性能
    public float rotationSpeed = 10f; // 旋转速度（可选）

    [ShowInInspector]
    private float _runtimeHorsePower = 0f;

    [ShowInInspector]
    private float currentSpeed => rb.velocity.magnitude;
    
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
        var fixedDetlaTime = Time.deltaTime;
        var moving = rb.velocity.magnitude > 0.1f;
        
        // 模拟轮胎的摩擦力
        if (!bil.Fire && moving && gc.isGrounded)
        {
            rb.velocity -= rb.velocity * (0.5f * fixedDetlaTime); // 简单的减速
        }
        
        // 如果着地并且踩油门，那就给一个前向的力
        if (gc.isGrounded)
        {
            if (bil.Fire)
            {
                // 添加一个马力让车立即可以走
                if (_runtimeHorsePower < initialHorsePower)
                {
                    _runtimeHorsePower += initialHorsePower;
                }

                // 马力还没到最大
                if (_runtimeHorsePower < maxHorsePower)
                {
                    _runtimeHorsePower += fixedDetlaTime * powerAcceleration;
                    _runtimeHorsePower = _runtimeHorsePower > maxHorsePower ? maxHorsePower : _runtimeHorsePower;
                }

                
                if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            else
            {
                // 1秒下降到0
                _runtimeHorsePower -= fixedDetlaTime * _runtimeHorsePower;
                if (_runtimeHorsePower <= 0) _runtimeHorsePower = 0;
            }
            rb.AddForce(transform.forward * _runtimeHorsePower, ForceMode.Force);
        }

        // 如果旋转有输入，那么就尝试将车转向目标
        if (moveDirection != Vector3.zero)
        {
            // 如果速度不够，那么转弯速度将下降，但不至于为0，会卡死
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            if (moving)
            {
                
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * fixedDetlaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed* 0.2f * fixedDetlaTime);
            }
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