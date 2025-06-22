using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class BattleEventDefine
{
    public const uint PLAYER_GROUNDED_START = 0;
    
    public const uint PLAYER_GROUNDED_END = 1;

    /// <summary>
    /// 有可互动内容进入范围
    /// </summary>
    public const uint PLAYER_ITEM_IN_INTERACT_AREA = 2;
    
    /// <summary>
    /// 有可互动内容退出范围
    /// </summary>
    public const uint PLAYER_ITEM_OUT_INTERACT_AREA = 3;

    
    /// <summary>
    /// 有实体生成
    /// </summary>
    public const uint ENTITY_SPAWN = 10;
    
    /// <summary>
    /// 有实体被摧毁
    /// </summary>
    public const uint ENTITY_DIE = 10;
    
    

    // =======================================================================================================================================================================================
    // 1000开始是互动，前面预留一千个单位给战局活动
    
    /// <summary>
    /// 玩家跟角色互动
    /// </summary>
    public const uint PLAYER_INTERACT_ROLE = 1000;


    public const uint PLAYER_TELEPORT = 1010;


    /// <summary>
    /// 单个战局结束，无论失败与否都会触发
    /// </summary>
    public const uint ON_GAME_OVER = 1100;
    
    /// <summary>
    /// 单个战局胜利
    /// </summary>
    public const uint ON_GAME_VICTORY = 1101;
    
    /// <summary>
    /// 单个战局失败
    /// </summary>
    public const uint ON_GAME_FAILED = 1102;

    /// <summary>
    /// 摄像机事件
    /// </summary>
    public const uint VIRTUAL_CAMERA_SET_FOLLOW_TARGET = 2000;

}
