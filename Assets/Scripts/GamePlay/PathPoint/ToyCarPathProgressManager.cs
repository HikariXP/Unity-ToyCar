// 赛道的检查点应该考虑赛道的特性。
// 一种是条状，直接到达终点：看有没有到达重点
// 一种是环状，没有终点：看通过的检查点是否足够

// 所以当前玩家可以通过的有效检查点是有限的两个，下一个要去的点，上一个经过的点。如果reset， 就回到上一个经过的点。

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public struct ToyCarPathProgress
{
    /// <summary>
    /// ToyCarId
    /// </summary>
    public int toyCarRigidBodyIndex;

    /// <summary>
    /// 已通过的路径点数量
    /// </summary>
    private int passedPathPointCount;
    
    /// <summary>
    /// 上一个检查点(已通过)
    /// </summary>
    public int previousPathPointIndex;
    
    /// <summary>
    /// 下一个检查点(未通过)
    /// </summary>
    public int nextPathPointIndex;
}

public class ToyCarPathProgressManager : MonoBehaviour
{
    public static ToyCarPathProgressManager instance;
    [Title("对局参数")]
    // 最多玩家
    public int maxToyCar = 64;
    // 圈数
    public int lap = 3;
    
    
    [ShowInInspector,ReadOnly]
    private Dictionary<int, int> toyCarPathProgress;
    
    public void Awake()
    {
        instance = this;
        toyCarPathProgress = new Dictionary<int, int>(maxToyCar);
    }

    /// <summary>
    /// 把ToyCar的这个组件的InstanceID当作识别符
    /// </summary>
    /// <param name="rigidbody"></param>
    public void RegisterToyCar(Rigidbody rigidbody)
    {
        if (toyCarPathProgress.Count == maxToyCar)
        {
            Debug.LogError($"[{nameof(ToyCarPathProgressManager)}]Toy car in game is max, Can't add");
            return;
        }

        var rbInstanceId = rigidbody.GetInstanceID();
        if (toyCarPathProgress.TryAdd(rbInstanceId, 0)) return;
        
        // 如果没加上就会报错
        Debug.LogError($"[{nameof(ToyCarPathProgressManager)}]Toy car already registry");
        return;
    }

    /// <summary>
    /// 当ToyCar通过检查点之后的行为
    /// </summary>
    /// <param name="rigidBodyId"></param>
    /// <param name="pathPointId"></param>
    public void OnToyCarThroughPathPoint(int rigidBodyId, int pathPointId)
    {
        toyCarPathProgress[rigidBodyId]++;
    }

    /// <summary>
    /// 当ToyCar飞出去或者翻车之后的行为
    /// </summary>
    /// <param name="rigidBodyId"></param>
    public void ToyCarReset(int rigidBodyId)
    {
        
    }
    

    public void Clear()
    {
        toyCarPathProgress.Clear();
    }
}
