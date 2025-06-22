/*
 * Author: CharSui
 * Created On: 2025.01.11
 * Description: 定义实体数据
 * 移动速度和实际做的行为有关，在具体的行为里面定义
 */

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "EFT_Configuration/Entity")]
public class EntityScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<Entity> _data;

    /// <summary>
    /// 做uid去重，资源存在与否的检测等。
    /// </summary>
    [Button]
    public void Valid()
    {
        if (_data == null) return;
    }
}

[Serializable]
public struct Entity
{
    [Tooltip("实体uid")]
    public uint uid;

    [Tooltip("引用的预制体在Addressable的名字")]
    public string perfabAssetName;
    
    [Tooltip("最大生命值")]
    public float maxHealth;

    [Tooltip("护甲值")]
    public float armor;
}