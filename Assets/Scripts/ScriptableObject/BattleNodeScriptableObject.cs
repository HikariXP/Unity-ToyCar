/*
 * Author: CharSui
 * Created On: 2025.04.06
 * Description: 战斗节点，比如生成敌人，进入下一个节点的目标是什么，触发什么故事剧情等。
 * 节点应有多个走向，每个走向有各自的判定。
 * Sensor用于提供检查是否达到Condition。
 */
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EFT_Configuration/BattleNode")]
public class BattleNodeScriptableObject : ScriptableObject
{
    public List<BattleNode> data;
}

[Serializable]
public struct BattleNode : IComparable<BattleNode>
{
    public int index { get; set; }

    public BattleNodeCondition conditionType;

    public string conditionValue;

    public int CompareTo(BattleNode other)
    {
        if (index > other.index) return 1;
        if (index == other.index) return 0;
        return -1;
    }
}

[Flags]
public enum BattleNodeAction
{
    none = 0,
    spawnPlayer = 1,
    spawnEnemy = 1<<1,
    // spawn
}

[Flags]
public enum BattleNodeCondition
{
    none = 0,   // 无约束
    arraiveUnitl = 1, // 时间内存活(有倒计时) 毫秒
    allEnemyLimited = 1<<1, // 清除所有敌人->指定队伍没有存活单位
}
