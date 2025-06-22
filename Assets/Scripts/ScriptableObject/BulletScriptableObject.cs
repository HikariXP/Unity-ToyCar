/*
 * Author: CharSui
 * Created On: 2025.02.07
 * Description: 定义可以使用的子弹，对于伤害的判断就是从这里的数据获取。
 */

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EFT_Configuration/Bullet")]
public class BulletScriptableObject : ScriptableObject
{
 public List<BulletProperty> data;
}

[Serializable]
public struct BulletProperty
{
 public uint uid;
 
 [Tooltip("索引的子弹预制体的Key，这决定了子弹以什么形式发射(仅供管理器加载)")]
 public string bulletPrefabAssetName;

 [Tooltip("基础伤害")]
 public float damage;

 [Tooltip("穿透性")]
 public float penetration;

 [Tooltip("初速")]
 public float bulletSpeed;
}