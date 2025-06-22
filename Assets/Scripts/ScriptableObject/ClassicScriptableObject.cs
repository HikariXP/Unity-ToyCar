using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Tank/ClassicSO")]
public class ClassicScriptableObject : BaseScriptableObject<ClassicConfig>
{
}

[Serializable]
public struct ClassicConfig
{
    /// <summary>
    /// 用于索引
    /// </summary>
    public uint uid;

    /// <summary>
    /// 所使用的预制体的名字
    /// </summary>
    public string asset_name;
    
    /// <summary>
    /// 默认1500
    /// </summary>
    public int weight;

    public float health_max;

    public float armor_physical;
    
    public float armor_energy;

    /// <summary>
    /// 最大稳定性
    /// 如果达到的话，维持2秒的失衡，之后失衡条清空
    /// </summary>
    public float stability_max;

    /// <summary>
    /// 稳定性恢复速度，在还没达到最大值的失衡的恢复速度
    /// </summary>
    public float stability_recovery_rate;
}