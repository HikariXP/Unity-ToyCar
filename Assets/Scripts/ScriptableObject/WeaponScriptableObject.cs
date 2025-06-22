/*
 * Author: CharSui
 * Created On: 2025.01.11
 * Description: 定义武器数据
 * 
 */

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "EFT_Configuration/Weapon")]
public class WeaponScriptableObject : BaseScriptableObject<WeaponConfig>
{
}


[Serializable]
public struct WeaponConfig
{
    public uint uid;

    public string asset_name;

    /// <summary>
    /// 默认发射的子弹类型，如果是塔科夫模式则不限制，限制可使用的子弹类型
    /// </summary>
    public uint bulletUid;

    /// <summary>
    /// 开火模式主要影响的是武器如何响应输入。按住，还是按住后需要松手，还是单点。
    /// </summary>

    public WeaponFireMode fireMode;

    /// <summary>
    /// 可补充的最大弹容量
    /// </summary>
    public int total_ammo_max;

    /// <summary>
    /// 弹匣内能装填的最大数量
    /// </summary>
    public int magazine_capacity;

    /// <summary>
    /// 每执行一次更换弹药后补充的弹药
    /// </summary>
    public int reload_amount;

    /// <summary>
    /// 如果FireMode用到了hold，就需要用到
    /// </summary>
    public float preheatTime;

    /// <summary>
    /// 开火后，需要多久才能开下一次火：如果单点，但是fireRate没有限制，那么点的越快射的越快。
    /// </summary>
    public float fire_interval;

    /// <summary>
    /// 对于常规枪械使用，开火会在一个圆锥的范围进行散射。这里的单位是度数，圆锥的度数。
    /// </summary>
    public float fireAccuracy;
}

public enum WeaponFireMode
{
    Automatic,          // 自动
    // PreheatAutomatic, // 预热自动
    SemiAutomatic,         // 半自动
    // PreheatSemiAutomatic // 预热半自动
}

public enum WeaponPreheatMode
{
    NoNeedPreheat,
    NeedPreheat
}