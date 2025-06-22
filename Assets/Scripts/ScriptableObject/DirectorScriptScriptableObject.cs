/*
 * Author: CharSui
 * Created On: 2025.01.12
 * Description: 导演剧本，定义着做什么，和条件
 */

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EFT_Configuration/DirectorScript")]
public class DirectorScriptScriptableObject : BaseScriptableObject<DirectorNode>
{
}

[Serializable]
public class DirectorNode
{
    /// <summary>
    /// 需要全部达成的条件
    /// </summary>
    public List<Condition> conditions_And;
    
    /// <summary>
    /// 只需达成其中一项的条件
    /// </summary>
    public List<Condition> conditions_Or;

    // public bool IsConditionsSatisfy()
    // {
    //     
    // }
}

[Serializable]
public struct Condition
{
    /// <summary>
    /// 约束的Key
    /// </summary>
    public enum ConditionKey
    {
        // 持续时间到达什么时间的时候结束(tick)
        BattleDuration,
        
        // 袭击波次
        RaidWave,
        
        // 存活玩家数量
        PlayerAlive,
    }
    
    /// <summary>
    /// Key值的比较
    /// </summary>
    public enum ConditionCheck
    {
        greater,
        equal,
        less
    }

    public ConditionKey conditionKey;

    public ConditionCheck conditionCheck;

    public int conditionValue;
}
